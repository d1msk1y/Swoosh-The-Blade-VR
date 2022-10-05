using FMODUnity;
using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour
{
	[Header("Sword Parameters")]
	[SerializeField] private float _launchForce;
	[SerializeField] private float _springAmplifier;
	[SerializeField] private float _rechargeTime;

	[Header("Audio")]
	[SerializeField] private StudioEventEmitter _eventEmitter;
	[SerializeField] private EventReference _swooshSound;
	
	[Header("Components")]
	[SerializeField] private Blade _blade;
	[SerializeField] private LineRenderer _lineRenderer;
	[SerializeField] private Transform _firePos;
	[SerializeField] private GameObject _parentJointObject;

	private Vector3 _initialBladePos;
	private Blade _bladePf;
	private SpringJoint _springJoint;
	private bool _canShoot = true;
	public Transform FirePos { get => _firePos; set => _firePos = value; }
	public Blade Blade { get => _blade; set => _blade = value; }

	private void Start()
	{
		_bladePf = Blade;
		_initialBladePos = Blade.transform.localPosition;
		Blade.OnAttach += SetJoint;
	}

	private void Update()
	{
		if(Blade != null)
			SetLineRenderer(FirePos.position, Blade.Origin.transform.position);
	}

	public bool IsGrappling() => Blade != null && _canShoot == false;

	public void LaunchBlade()
	{
		if(!_canShoot) return;
		Blade.OnAttach += SetJoint;
		Blade.Launch(_launchForce);
		AudioManager.PlayerJoinedEvent(_eventEmitter, _swooshSound);
		_canShoot = false;
	}
	
	public void ReleaseBlade()
	{
		SetLineRenderer(Vector3.zero, Vector3.zero);
		Blade.OnAttach -= SetJoint;
		if(_canShoot) return;
		Destroy(_springJoint);
		_eventEmitter.Stop();
		StartCoroutine(Recharge());
		Blade = null;
	}

	private void SetJoint()
	{
		_springJoint = _parentJointObject.AddComponent<SpringJoint>();
		_springJoint.spring = 500;
		_springJoint.damper = 400;
		_springJoint.autoConfigureConnectedAnchor = false;
		_springJoint.maxDistance = Vector3.Distance(_springJoint.transform.position, Blade.transform.position) * _springAmplifier;
		_springJoint.connectedBody = Blade.Rigidbody;
	}

	private void SetLineRenderer(Vector3 a, Vector3 b)
	{
		// _lineRenderer.SetPosition(0, a);
		// _lineRenderer.SetPosition(1, b);
	}

	private IEnumerator Recharge()
	{
		yield return new WaitForSeconds(_rechargeTime);
		CreateNewBlade();
		_canShoot = true;
	}
	private void CreateNewBlade()
	{
		Blade = Instantiate(_bladePf, _initialBladePos, new Quaternion(0, 0, 0, 0), transform);
		Blade.transform.localPosition = _initialBladePos;
	}
}

using FMODUnity;
using System.Collections;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
	[Header("Sword Parameters")]
	[SerializeField] private float _launchForce;
	[SerializeField] private float _springAmplifier;
	[SerializeField] private float _staticSpringAmplifier;
	[SerializeField] private float _rechargeTime;

	[Header("Audio")]
	[SerializeField] private StudioEventEmitter _eventEmitter;
	[SerializeField] private EventReference _swooshSound;
	
	[Header("Components")]
	[SerializeField] private Hook hook;
	[SerializeField] private Transform _firePos;
	[SerializeField] private GameObject _parentJointObject;

	private Vector3 _initialBladePos;
	private Vector3 _initialBladeRotation;
	private Vector3 _initialBladeScale;
	private Transform _initialBladeTransform;
	private Hook _hookPf;
	private SpringJoint _springJoint;
	private bool _canShoot = true;
	
	public Transform FirePos => _firePos;
	public Hook Hook { get => hook; private set => hook = value; }
	
	private float SpringAmplifier {
		get {
			if (PlayerController.instance.GroundCheck.IsGrounded()) return _springAmplifier * _staticSpringAmplifier;
			return _springAmplifier;
		}
	}

	private void Start()
	{
		_hookPf = Hook;
		_initialBladePos = Hook.transform.localPosition;
		_initialBladeRotation = Hook.transform.localEulerAngles;
		_initialBladeScale = Hook.transform.localScale;
		_initialBladeTransform = Hook.transform;
		Hook.OnAttach += SetJoint;
	}

	public bool IsGrappling() => Hook != null && _canShoot == false;

	public void LaunchHook()
	{
		if(!_canShoot) return;
		Hook.OnAttach += SetJoint;
		Hook.Launch(_launchForce);
		AudioManager.PlayerJoinedEvent(_eventEmitter, _swooshSound);
		_canShoot = false;
	}
	
	public void ReleaseHook()
	{
		Hook.OnAttach -= SetJoint;
		if(_canShoot) return;
		Destroy(_springJoint);
		_eventEmitter.Stop();
		StartCoroutine(Recharge());
		Hook = null;
	}

	private void SetJoint()
	{
		_springJoint = _parentJointObject.AddComponent<SpringJoint>();
		_springJoint.spring = 500;
		_springJoint.damper = 100;
		_springJoint.autoConfigureConnectedAnchor = false;
		_springJoint.maxDistance = Vector3.Distance(_springJoint.transform.position, Hook.transform.position) * SpringAmplifier;
		_springJoint.connectedBody = Hook.Rigidbody;
	}
	private IEnumerator Recharge()
	{
		yield return new WaitForSeconds(_rechargeTime);
		CreateNewHook();
		_canShoot = true;
	}
	private void CreateNewHook()
	{
		Hook = Instantiate(_hookPf, _initialBladePos, Quaternion.identity, transform);
		Hook.transform.localPosition = _initialBladePos;
		Hook.transform.localScale = _initialBladeScale;
		Hook.transform.localEulerAngles = _initialBladeRotation;
	}
}

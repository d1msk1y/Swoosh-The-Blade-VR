using FMODUnity;
using System;
using System.Collections;
using UnityEngine;

public class GrapplingGun : MonoBehaviour {
	[Header("Sword Parameters")]
	[SerializeField] private float _launchForce;
	[SerializeField] private float _springAmplifier;
	[SerializeField] private float _staticSpringAmplifier;
	[SerializeField] private float _springTolerance;
	[SerializeField] private float _rechargeTime;

	[Header("Audio")]
	[SerializeField] private StudioEventEmitter _eventEmitter;
	[SerializeField] private EventReference _swooshSound;
	[SerializeField] private EventReference _stressSound;
	
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
	public bool IsGrappling() => Hook != null && _canShoot == false;

	private float SpringAmplifier {
		get {
			if (PlayerController.instance.GroundCheck.IsGrounded()) return _springAmplifier * _staticSpringAmplifier;
			return _springAmplifier;
		}
	}


	private void Start() {
		_hookPf = Hook;
		_initialBladePos = Hook.transform.localPosition;
		_initialBladeRotation = Hook.transform.localEulerAngles;
		_initialBladeScale = Hook.transform.localScale;
		_initialBladeTransform = Hook.transform;
	}

	public void LaunchHook() {
		if(!_canShoot) return;
		Hook.OnAttach += OnAttach;
		Hook.Launch(_launchForce);
		AudioManager.PlayerJoinedEvent(_eventEmitter, _swooshSound);
		_canShoot = false;
	}
	
	private void OnAttach() {
		SetJoint();
		CheckRopeStress();
		Debug.Log("OnAttach blade");
	}
	
	public void ReleaseHook() {
		Hook.OnAttach -= OnAttach;
		if(_canShoot || Hook == null) return;
		Destroy(_springJoint);
		_eventEmitter.Stop();
		StartCoroutine(Recharge());
		Debug.Log("Release!");
		Hook = null;
		_canShoot = true;
	}

	private void CheckRopeStress() {
		if(IsGrappling())
			AudioManager.PlayerJoinedEvent(_eventEmitter, _stressSound);
	}
	
	private void SetJoint() {
		_springJoint = _parentJointObject.AddComponent<SpringJoint>();
		_springJoint.spring = 500;
		_springJoint.damper = 300;
		_springJoint.tolerance = _springTolerance;
		_springJoint.autoConfigureConnectedAnchor = false;
		_springJoint.maxDistance = Vector3.Distance(_springJoint.transform.position, Hook.transform.position)*SpringAmplifier;
		_springJoint.connectedBody = Hook.Rigidbody;
	}
	private IEnumerator Recharge() {
		yield return new WaitForSeconds(_rechargeTime);
		CreateNewHook();
		_canShoot = true;
	}
	private void CreateNewHook() {
		Debug.Log("Created blade");
		Hook = Instantiate(_hookPf, _initialBladePos, Quaternion.identity, transform);
		Hook.transform.localPosition = _initialBladePos;
		Hook.transform.localScale = _initialBladeScale;
		Hook.transform.localEulerAngles = _initialBladeRotation;
		/*Hook.transform.position = _initialBladeTransform.position;
		Hook.transform.localScale = _initialBladeTransform.lossyScale;
		Hook.transform.localEulerAngles = _initialBladeTransform.eulerAngles;*/
	}
}

using System;
using UnityEngine;
public class Swinging : MonoBehaviour {
	[Header("Parameters")]
	[SerializeField] private float _swingSpeed;
	[SerializeField] private float _angleTolerance;
	[Space(10)]
	[SerializeField] private float _negativeAngle;
	[SerializeField] private float _positiveAngle;

	private float _targetAngle;
	private Vector3 _velocity;
	private float _startRotation;
	private float _swingAngle;

	private void Start() {
		_startRotation = transform.localEulerAngles.z;
		_swingAngle = _positiveAngle;
	}
	
	private void Update() {
		CalculateRotation();
		Swing();
	}

	private void CalculateRotation() {
		if (transform.localEulerAngles.z + _angleTolerance > _startRotation + _positiveAngle) {
			_swingAngle = _negativeAngle;
		}
		if (transform.localEulerAngles.z - _angleTolerance < _startRotation + _negativeAngle) {
			_swingAngle = _positiveAngle;
		}
	}

	private void Swing() {
		var swingRotation = new Vector3(transform.eulerAngles.x, transform.localEulerAngles.y,  transform.localEulerAngles.z + _swingAngle);
		transform.localEulerAngles = Vector3.SmoothDamp(transform.localEulerAngles,  swingRotation, ref _velocity, _swingSpeed);
	}
}
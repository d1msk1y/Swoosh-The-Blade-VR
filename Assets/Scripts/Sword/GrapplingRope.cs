using UnityEngine;

public class GrapplingRope : MonoBehaviour {
	private Spring _spring;
	private LineRenderer _lr;
	private Vector3 _currentGrapplePosition;
	public GrapplingGun grapplingGun;
	public int quality;
	public float damper;
	public float strength;
	public float velocity;
	public float waveCount;
	public float waveHeight;
	public AnimationCurve affectCurve;

	private void Awake() {
		_lr = GetComponent<LineRenderer>();
		_spring = new Spring();
		_spring.SetTarget(0);
	}
	
	private void LateUpdate() {
		DrawRope();
	}

	private void DrawRope() {
		//If not grappling, don't draw rope
		if (!grapplingGun.IsGrappling()) {
			_currentGrapplePosition = grapplingGun.FirePos.position;
			_spring.Reset();
			if (_lr.positionCount > 0)
				_lr.positionCount = 0;
			return;
		}

		if (_lr.positionCount == 0) {
			_spring.SetVelocity(velocity);
			_lr.positionCount = quality + 1;
		}

		_spring.SetDamper(damper);
		_spring.SetStrength(strength);
		_spring.Update(Time.deltaTime);

		var grapplePoint = grapplingGun.Hook.transform.position;
		var gunTipPosition = grapplingGun.FirePos.position;
		var up = Quaternion.LookRotation((grapplePoint - gunTipPosition).normalized)*Vector3.up;

		_currentGrapplePosition = Vector3.Lerp(_currentGrapplePosition, grapplePoint, Time.deltaTime*12f);

		for (var i = 0; i < quality + 1; i++) {
			var delta = i/(float)quality;
			var offset = up*waveHeight*Mathf.Sin(delta*waveCount*Mathf.PI)*_spring.Value*
				affectCurve.Evaluate(delta);

			_lr.SetPosition(i, Vector3.Lerp(gunTipPosition, _currentGrapplePosition, delta) + offset);
		}
	}
}

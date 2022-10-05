using UnityEngine;
public class PlayerController : MonoBehaviour
{
	[Header("Parameters")]
	[SerializeField] private float _maxVelocity;
	[Space(10)]
	[SerializeField] private Rigidbody _rigidbody;

	private float VelocityPercentage => _rigidbody.velocity.magnitude / _maxVelocity;
	private void Update()
	{
		_rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _maxVelocity);
		Wind.WindForce = VelocityPercentage;
	}
}
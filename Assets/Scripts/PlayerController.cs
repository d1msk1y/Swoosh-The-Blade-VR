using UnityEngine;
public class PlayerController : MonoBehaviour {
	[Header("Parameters")]
	[SerializeField] private float _maxVelocity;
	[Space(10)]
	[SerializeField] private Rigidbody _rigidbody;
	[SerializeField] private GroundCheck _groundCheck;

	#region Properties
	public GroundCheck GroundCheck => _groundCheck;
	private float VelocityPercentage => Rigidbody.velocity.magnitude / _maxVelocity;
	public Rigidbody Rigidbody => _rigidbody;
	#endregion

	public static PlayerController instance;

	private void Awake() => instance = this;

	private void Update() {
		Rigidbody.velocity = Vector3.ClampMagnitude(Rigidbody.velocity, _maxVelocity);
		Wind.WindForce = VelocityPercentage; 
		PostProcessingManager.Vignette.intensity.value = VelocityPercentage;
	}
}
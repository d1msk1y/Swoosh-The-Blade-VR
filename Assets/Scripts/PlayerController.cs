using UnityEngine;
public class PlayerController : MonoBehaviour {
	[Header("Parameters")]
	[SerializeField] private float _maxVelocity;
	[Header("Parameters")]
	[SerializeField] private Rigidbody _rigidbody;
	[SerializeField] private EntityHealth _entityHealth;
	[SerializeField] private GroundCheck _groundCheck;

	#region Properties
	public GroundCheck GroundCheck => _groundCheck;
	private float VelocityPercentage => Rigidbody.velocity.magnitude / _maxVelocity;
	public Rigidbody Rigidbody => _rigidbody;
	public EntityHealth EntityHealth { get => _entityHealth; private set => _entityHealth = value; }
	#endregion

	public static PlayerController instance;

	private void Awake() {
		instance = this;
		EntityHealth = GetComponent<EntityHealth>();
	}
	
	private void Update() {
		Rigidbody.velocity = Vector3.ClampMagnitude(Rigidbody.velocity, _maxVelocity);
		Wind.WindForce = VelocityPercentage; 
		PostProcessingManager.Vignette.intensity.value = VelocityPercentage;
	}
}
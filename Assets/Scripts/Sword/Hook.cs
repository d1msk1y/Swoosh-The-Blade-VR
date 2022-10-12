using FMODUnity;
using UnityEngine;

public class Hook : MonoBehaviour {
	[Header("Ray Parameters")]
	[SerializeField] private float _rayLength;
	[SerializeField] private LayerMask _attachLayer;
	[SerializeField] private Transform _origin;

	[Header("Audio")]
	[SerializeField] private StudioEventEmitter _soundEmitter;
	[SerializeField] private EventReference _hitSound;
	[SerializeField] private EventReference _launchSound;
	
	[Header("Graphics")]
	[SerializeField] private ParticleSystem _attachParticle;
	[SerializeField] private ParticleSystem _launchParticle;

	public delegate void BladeHandler();
	public event BladeHandler OnAttach;

	public Rigidbody Rigidbody { get; private set; }

	private void Start() {
		Rigidbody = GetComponent<Rigidbody>();
		Rigidbody.isKinematic = true;
	}

	private void Update() => CheckCollision();

	public void Launch(float force) {
		transform.parent = null;
		Rigidbody.isKinematic = false;
		AudioManager.PlayerJoinedEvent(_soundEmitter, _launchSound);
		Instantiate(_launchParticle, _origin.position, Quaternion.identity, transform);
		Rigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
	}

	private void CheckCollision() {
		if (!Physics.Raycast(_origin.position, transform.forward, out var hit, _rayLength, _attachLayer) || Rigidbody.isKinematic) return;
		
		Rigidbody.isKinematic = true;
		Instantiate(_attachParticle, hit.point, Quaternion.identity, transform);
		AudioManager.PlayerJoinedEvent(_soundEmitter, _hitSound);
		OnAttach?.Invoke();
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawRay(_origin.position, transform.forward * _rayLength);
	}
}
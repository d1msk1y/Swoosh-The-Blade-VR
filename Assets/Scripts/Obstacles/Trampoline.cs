using UnityEngine;
public class Trampoline : MonoBehaviour {
	[SerializeField] private float _jumpForce;
	[SerializeField] private ParticleSystem _actionParticle;
	
	private void OnCollisionEnter (Collision collision) {
		collision.gameObject.TryGetComponent(out Rigidbody rigidbody);
		rigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
		if (_actionParticle != null) Instantiate(_actionParticle, transform.position, Quaternion.identity, transform);
	}
}
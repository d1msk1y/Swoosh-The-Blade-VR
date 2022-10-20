using Unity.Mathematics;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	[SerializeField] private int _damage;
	[SerializeField] private ParticleSystem _hitParticle;
	[SerializeField] private ParticleSystem _dieParticle;
	private void OnCollisionEnter (Collision collision) {
		Instantiate(_hitParticle, collision.transform.position, quaternion.identity, transform);
		
		if (collision.gameObject.CompareTag("Player")) {
			PlayerController.instance.EntityHealth.TakeDamage(_damage);
			//Some graphics
		}
		else {
			//Do basic stuff
		}
	}
}
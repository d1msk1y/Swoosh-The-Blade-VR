using UnityEngine;
public class Blade : MonoBehaviour {
	[Header("Blade Parameters")]
	[SerializeField] private int _damage;
	[SerializeField] private int _staticDamage;
	private int Damage => transform.parent != null ? _staticDamage : _damage;

	private void OnTriggerEnter (Collider other) {
		if(other.TryGetComponent(out EntityHealth entityHealth))
			entityHealth.TakeDamage(Damage);
	}
}
using UnityEngine;
using UnityEngine.Events;

public class EntityHealth : MonoBehaviour {
	[Header("Health")]
	[SerializeField] private int _maxHealth;
	private int _health;
	private int Health {
		get => _health;
		set {
			_health = value;
			if(_health <= 0) 
				Die();
		}
	}
	
	public UnityEvent OnDamage;
	public UnityEvent OnDie;

	private void Start() => Health = _maxHealth;

	public void TakeDamage (int damage) => Health -= damage;

	private void Die() {
		OnDie?.Invoke();
		OnDamage?.Invoke();
		if(OnDie?.GetPersistentEventCount() <= 0) 
			Destroy(gameObject);
	}
}
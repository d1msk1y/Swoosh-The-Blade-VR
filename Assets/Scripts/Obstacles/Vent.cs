using UnityEngine;
public class Vent : MonoBehaviour {
	[Header("Parameters")]
	[SerializeField] private float _blowForce;

	private Rigidbody _caughtRigidbody;

	private void OnTriggerEnter (Collider other) => _caughtRigidbody = InitializeRigidbody(other.gameObject);
	private void OnTriggerStay (Collider other) {
		if(_caughtRigidbody != null)
			_caughtRigidbody.AddForce(transform.up * _blowForce, ForceMode.Force);
	} 
	private static Rigidbody InitializeRigidbody (GameObject other) => other.CompareTag("Player") ? PlayerController.instance.Rigidbody : other.GetComponent<Rigidbody>();
}
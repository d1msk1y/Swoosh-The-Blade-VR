using UnityEngine;
public class Vent : MonoBehaviour
{
	[Header("Parameters")]
	[SerializeField] private float _blowForce;
	
	private void OnTriggerStay (Collider other)
	{
		other.TryGetComponent(out Rigidbody rigidbody);
		rigidbody.AddForce(transform.up*_blowForce*Time.fixedDeltaTime, ForceMode.Force);
	}
}
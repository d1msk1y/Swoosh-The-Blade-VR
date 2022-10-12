using UnityEngine;

public class GroundCheck : MonoBehaviour {
	[Header("Parameters")]
	[SerializeField] private LayerMask _groundLayer;
	[SerializeField] private float _rayLength;
	[SerializeField] private Transform _rayOrigin;
	
	public bool IsGrounded() => Physics.Raycast(_rayOrigin.position, Vector3.down, _rayLength, _groundLayer);

	private void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position, Vector3.down * _rayLength);
	}
}
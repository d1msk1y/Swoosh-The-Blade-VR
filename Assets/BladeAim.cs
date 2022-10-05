using System;
using UnityEngine;

public class BladeAim : MonoBehaviour
{
	[Header("Aim Parameters")]
	[SerializeField] private float _aimLength;
	[SerializeField] private LayerMask _aimInteractionMask;
	[SerializeField] private Transform _aimPosition;
	
	private LineRenderer _lineRenderer;

	private void Start() => _lineRenderer = GetComponent<LineRenderer>();

	private void Update() => RaycastAim();

	private void RaycastAim()
	{
		RaycastHit hit;
		Physics.Raycast(_aimPosition.position, transform.up, out hit, _aimLength, _aimInteractionMask);

		SetLineRenderer(_aimPosition.position, hit.collider != null ? hit.point : _aimPosition.position + transform.up * _aimLength);
	}
	private void SetLineRenderer (Vector3 a, Vector3 b)
	{
		_lineRenderer.SetPosition(0, a);
		_lineRenderer.SetPosition(1, b);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay(_aimPosition.position, _aimPosition.up * _aimLength);
	}
}

using System;
using UnityEngine;

public class BodyCollision : MonoBehaviour
{
	[SerializeField] private Transform _head;

	private void Update()
	{
		transform.position = new Vector3(_head.position.x, transform.position.y, _head.position.z);
	}
}

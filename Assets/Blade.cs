using FMODUnity;
using System;
using UnityEngine;

public class Blade : MonoBehaviour
{
	[Header("Ray Parameters")]
	[SerializeField] private float _rayLength;
	[SerializeField] private LayerMask _attachLayer;
	[SerializeField] private Transform _origin;

	[Header("Audio")]
	[SerializeField] private StudioEventEmitter _soundEmitter;
	[SerializeField] private EventReference _hitSound;
	[SerializeField] private EventReference _wireStressSound;
	[SerializeField] private EventReference _launchSound;
	
	[Header("Graphics")]
	[SerializeField] private ParticleSystem _attachParticle;
	[SerializeField] private ParticleSystem _launchParticle;

	public delegate void BladeHandler();
	public event BladeHandler OnAttach;
	public event BladeHandler OnLaunch;

	public Rigidbody Rigidbody { get; private set; }
	public Transform Origin { get => _origin; set => _origin = value; }

	private void Start()
	{
		Rigidbody = GetComponent<Rigidbody>();
		Rigidbody.isKinematic = true;
	}

	public void Launch(float force)
	{
		OnLaunch?.Invoke();
		transform.parent = null;
		Rigidbody.isKinematic = false;
		AudioManager.PlayerJoinedEvent(_soundEmitter, _launchSound);
		Instantiate(_launchParticle, _origin.position, Quaternion.identity, transform);
		Rigidbody.AddForce(transform.up * force, ForceMode.Impulse);
	}

	private void Update()
	{
		CheckCollision();
	}

	private void CheckCollision()
	{
		RaycastHit hit;
		if (!Physics.Raycast(Origin.position, transform.up, out hit, _rayLength, _attachLayer) || Rigidbody.isKinematic == true) return;
		
		Rigidbody.isKinematic = true;
		Debug.Log("Attached to " + hit.collider);
		Instantiate(_attachParticle, hit.point, Quaternion.identity, transform);
		AudioManager.PlayerJoinedEvent(_soundEmitter, _hitSound);
		OnAttach?.Invoke();
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay(Origin.position, transform.up * _rayLength);
	}

}

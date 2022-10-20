using System.Collections;
using UnityEngine;
public class PatrolMovement : MonoBehaviour {
	[SerializeField] private Transform[] _patrolPoints;
	[SerializeField] private float _patrolSpeed;
	[SerializeField] private float _smooth;
	[SerializeField] private float _waypointStep;
	[SerializeField] private float _stepDelay;

	private Transform _currentDestination;
	private Vector3 _velocity;

	private float WaypointDistance => Vector3.Distance(transform.position, _patrolPoints[_currentPointIndex].position);

	private bool _isMoving;


	private int _currentPointIndex;
	private int CurrentPointIndex {
		get => _currentPointIndex;
		set {
			_currentPointIndex = value;
			if (_currentPointIndex >= _patrolPoints.Length) _currentPointIndex = 0;
		}
	}

	private void Start() {
		_currentDestination = GetPatrolPoint();
		// StartCoroutine(Move());
		_isMoving = true;
	}

	private void Update() {
		Move();
	}

	private void Move() {
		if (WaypointDistance < _waypointStep) return;
			transform.position = Vector3.SmoothDamp(transform.position, _currentDestination.position, ref _velocity, _smooth, _patrolSpeed * Time.deltaTime);
			if(_isMoving)StartCoroutine(SetDestination());
	}

	private IEnumerator SetDestination() {
		_isMoving = false;
		// Debug.Log("Recharging");
		yield return new WaitForSeconds(_stepDelay);
		_currentDestination = GetPatrolPoint();
		_isMoving = true;
		Debug.Log("Recharged");
		// StartCoroutine(Move());
	}

	private Transform GetPatrolPoint() {
		if (WaypointDistance <= _waypointStep) {
			CurrentPointIndex += 1;
			return _patrolPoints[CurrentPointIndex];
		}
		return _patrolPoints[CurrentPointIndex];
	}
}

using UnityEngine;
using UnityEngine.Events;
public class ActionTrigger : MonoBehaviour {
	public UnityEvent onTriggerEnterEvent;
	
	private void OnTriggerEnter (Collider other) {
		if(other.CompareTag("Player"))
			onTriggerEnterEvent?.Invoke();
	}
}
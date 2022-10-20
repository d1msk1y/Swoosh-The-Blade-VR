using UnityEngine;
using UnityEngine.Events;
public class HitButton : MonoBehaviour, IAction {

	[SerializeField] private UnityEvent _onHit;
	
	public void Action() {
		_onHit?.Invoke();
	}
}
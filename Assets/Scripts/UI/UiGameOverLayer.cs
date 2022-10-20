using TMPro;
using UnityEngine;
public class UiGameOverLayer : MonoBehaviour {

	[SerializeField] private Timer _timer;
	[SerializeField] private TextMeshProUGUI _passTime;
	
	private void OnEnable() => SetTimerText();
	private void SetTimerText() => _passTime.text = "Pass time: " + _timer.PassedTime;
}
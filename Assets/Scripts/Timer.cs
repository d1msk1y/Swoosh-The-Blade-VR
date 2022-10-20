using UnityEngine;
public class Timer : MonoBehaviour {
	private int _countRate;
	public float PassedTime { get; set; }

	private void Start() {
		GameManager.instance.OnGameOver += PauseTimer;
		StartTimer();
	}

	private void Update() => Count();
	
	private void Count() => PassedTime += Time.deltaTime * _countRate; 
	private void PauseTimer() => _countRate = 0;
	private void StartTimer() => _countRate = 1;
	private void ResetTimer() => PassedTime = 0;
}
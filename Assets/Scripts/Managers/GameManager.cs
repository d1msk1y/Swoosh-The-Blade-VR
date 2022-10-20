using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	[SerializeField] private Transform _startPosition;
	
	public static GameManager instance;
	public delegate void GameHandler();
	public event GameHandler OnGameOver;
	public event GameHandler OnRestart;

	private void Awake() {
		instance = this;
	}

	public void GameOver() {
		PlayerController.instance.Rigidbody.isKinematic = true;
		UiManager.instance.ToggleGameOver(true);
		OnGameOver?.Invoke();
	}

	public void LoadScene (int index) => SceneManager.LoadScene(index);
	public void LoadNextLevel () => SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings + 1);

	public void Restart() {
		PlayerController.instance.Rigidbody.position = _startPosition.position;
		PlayerController.instance.Rigidbody.isKinematic = false;
		OnRestart?.Invoke();
	}
}
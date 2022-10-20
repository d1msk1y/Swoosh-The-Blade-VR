using UnityEngine;
public class UiManager : MonoBehaviour {
	[SerializeField] private GameObject _gameOverLayer;

	public static UiManager instance;

	private void Awake() {
		instance = this;
	}

	public void ToggleGameOver(bool toggle) => _gameOverLayer.SetActive(toggle);
}
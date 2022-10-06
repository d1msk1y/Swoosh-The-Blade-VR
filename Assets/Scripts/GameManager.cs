using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	
	public delegate void GameHandler();
	public event GameHandler OnGameOver;

	private void Awake()
	{
		instance = this;
	}

	public void GameOver()
	{
		OnGameOver?.Invoke();
		PlayerController.instance.Rigidbody.isKinematic = true;
	}

	public void Restart()
	{
		PlayerController.instance.Rigidbody.position = Vector3.zero;
		PlayerController.instance.Rigidbody.isKinematic = false;
	}
}

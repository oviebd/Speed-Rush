using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;
	
	private PlayerController _pllayerController;

	private void Start()
	{
		UIManager.instance.ShowInitialMenu();
	}

	public void StartNewGame()
	{
		EnvironmentController.instance.PrepareForNewGame();
		GetPlayerController()?.GetPlayerMovement().StartMovement();
	}

	public void EndGame()
	{
		GetPlayerController()?.GetPlayerMovement().StopMovement();
		UIManager.instance.ShowEndGamePanel();
	}

	public void PauseGame()
	{
		GetPlayerController()?.GetPlayerMovement().StopMovement();
	}

	public void RestartGame()
	{
		StartNewGame();
	}

	public void ResumeGame()
	{
		GetPlayerController()?.GetPlayerMovement().StartMovement();
	}

	void Awake()
	{
		if (instance == null)
			instance = this;
	}

	public void SetPlayerController(PlayerController controller)
	{
		this._pllayerController = controller;
	}

	public PlayerController GetPlayerController()
	{
		return this._pllayerController;
	}
}

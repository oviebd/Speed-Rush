using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;
	
	private PlayerController _pllayerController;

	private void Start()
	{
		UIManager.instance.ShowHomeUi();
	}

	public void StartNewGame()
	{
		EnvironmentController.instance.PrepareForNewGame();
		GetPlayerController()?.GetPlayerMovement().StartMovement();
		UIManager.instance.ShowOnGameMenu();
	}

	public void EndGame()
	{
		GetPlayerController()?.GetPlayerMovement().StopMovement();
		UIManager.instance.ShowGameOverMenu();
	}

	public void PauseGame()
	{
		GetPlayerController()?.GetPlayerMovement().StopMovement();
		UIManager.instance.ShowPauseGameMenu();
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

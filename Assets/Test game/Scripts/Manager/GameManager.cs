﻿using SaveSystem;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;

	public delegate void OnGameStateChanged(GameStateEnum.GAME_STATE gameState);
	public static event OnGameStateChanged onGameStateChanged;

	private PlayerController _pllayerController;
	private GameStateEnum.GAME_STATE _currentGameState;

	void Awake()
	{
		if (instance == null)
			instance = this;
	}

	private void Start()
	{
		UIManager.instance.ShowHomeUi();
		SetGameState(GameStateEnum.GAME_STATE.PREPARE);
	}

	public void StartNewGame()
	{
		EnvironmentController.instance.PrepareForNewGame();
		GetPlayerController()?.GetPlayerMovement().StartMovement();
		UIManager.instance.ShowOnGameMenu();
		GameAudioManager.instance.PlayBackgroundSound();

		SetGameState(GameStateEnum.GAME_STATE.RUNNING);
	}

    public void PlayerDied(Vector3 diedPosition)
    {
		GetPlayerController()?.GetPlayerMovement().StopMovement();
		SetGameState(GameStateEnum.GAME_STATE.PLAYER_DIED);
		UIManager.instance.ShowExtraLifePanel();
		//RespawnPlayer();
	}

    public void RespawnPlayer()
    {
        if(GetPlayerController() != null)
        {
			float speed = GetPlayerController().GetPlayerMovement().GetCurrentSpeed();
			Vector3 playerPosition = GetPlayerController().transform.position;
			EnvironmentController.instance.RespawnPlayer(playerPosition);
			ResumeGame();
			GetPlayerController().GetPlayerMovement().SetSpeedData(speed);

		}
        else
        {
			EndGame();
        }

    }

	public void EndGame()
	{
		GetPlayerController()?.GetPlayerMovement().StopMovement();
		UIManager.instance.ShowGameOverMenu();
		GameAudioManager.instance.StopBackgroundSound();

		_pllayerController = null;

		SetGameState(GameStateEnum.GAME_STATE.GAME_OVER);
	}

	public void PauseGame()
	{
		GetPlayerController()?.GetPlayerMovement().StopMovement();
		UIManager.instance.ShowPauseGameMenu();

		SetGameState(GameStateEnum.GAME_STATE.PAUSE);
	}
	public void ResumeGame()
	{
		GetPlayerController()?.GetPlayerMovement().StartMovement();
		UIManager.instance.ShowOnGameMenu();

		SetGameState(GameStateEnum.GAME_STATE.RUNNING);
	}
	public void RestartGame()
	{
		StartNewGame();
	}

	private void SetGameState(GameStateEnum.GAME_STATE gameState)
	{
		this._currentGameState = gameState;
		onGameStateChanged?.Invoke(gameState);
	}

	public GameStateEnum.GAME_STATE GetCurrentGameState()
	{
		return _currentGameState;
	}
	public void SetPlayerController(PlayerController controller)
	{
		this._pllayerController = controller;
	}

    public void OnRewardAdCompleted()
    {
		RespawnPlayer();
    }

	public PlayerController GetPlayerController()
	{
		/*if(GetCurrentGameState() != GameStateEnum.GAME_STATE.RUNNING)
		{
			return null;
		}*/
		return this._pllayerController;
	}
}

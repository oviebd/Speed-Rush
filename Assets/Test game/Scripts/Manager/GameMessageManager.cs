using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMessageManager : MonoBehaviour
{
	public static GameMessageManager instance;

	[SerializeField] MessagePanel messagePanel;

	private string tapMessage = "Tap to change player direction";
	private string collectItemMessage = "Go through the portal and you can break the obstacles";

	private bool _isTapMessageShown = false;
	private bool _isportalTutorialShown = false;
	private void Awake()
	{
		if (instance == null)
			instance = this;
		
		GameManager.onGameStateChanged += OnGameStateChanged;
	}

	private void OnDestroy()
	{
		GameManager.onGameStateChanged -= OnGameStateChanged;
	}

	private void OnGameStateChanged(GameStateEnum.GAME_STATE gameState)
	{
		if(_isTapMessageShown == false && gameState == GameStateEnum.GAME_STATE.RUNNING)
		{
			ShowInitialTutorialMessage();
		}

		if(gameState == GameStateEnum.GAME_STATE.GAME_OVER)
		{
			HideMessagePanel();
		}
	}

	public void ShowInitialTutorialMessage()
	{
		ShowMessagePanel(tapMessage);
		_isTapMessageShown = true;
	}

	public void ShowPortalTutorialMessage()
	{
		ShowMessagePanel(collectItemMessage);
		_isportalTutorialShown = true;
	}

	private void ShowMessagePanel(string message)
	{
		HideMessagePanel();
		messagePanel.ShowMessage(message);

		Invoke("HideMessagePanel",5.0f);
	}

	private void HideMessagePanel()                                                                                                                                                                                                                                                                                                         
	{
		CancelInvoke();
		messagePanel.Hide();
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager instance;

	[SerializeField] private Panel startGamePanel;
	[SerializeField] private Panel endGamePanel;
	[SerializeField] private Panel pauseGamePanel;
	[SerializeField] private Panel gameRunningPanel;

	private void Awake()
	{
		if (instance == null)
			instance = this;
	}


	public void ShowInitialMenu()
	{
		HideAllExceptOne(startGamePanel);
	}

	public void OnStartNewGameButtonClicked()
	{
		GameManager.instance.StartNewGame();
		HideAllExceptOne(gameRunningPanel);
	}

	public void OnPauseGameButtonClicked()
	{
		Debug.Log("Pause Game ");
		GameManager.instance.PauseGame();
		HideAllExceptOne(pauseGamePanel);
	}

	public void OnResumeButtonClicked()
	{
		GameManager.instance.RestartGame();
		HideAllExceptOne(gameRunningPanel);
	}

	public void OnReStartGameButtonClicked()
	{
		OnStartNewGameButtonClicked();
	}

	public void ShowEndGamePanel()
	{
		HideAllExceptOne(endGamePanel);
	}


	private void HideAllExceptOne( Panel activePanel)
	{
		startGamePanel?.Hide();
		endGamePanel?.Hide();
		pauseGamePanel?.Hide();
		gameRunningPanel?.Hide();

		activePanel?.Show();
	}
}

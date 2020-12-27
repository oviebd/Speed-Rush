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


	public void ShowHomeUi()
	{
		startGamePanel.Show();
	}
	public void ShowOnGameMenu()
	{
		gameRunningPanel.Show();
	}
	public void ShowPauseGameMenu()
	{
		pauseGamePanel.Show();
	}

	public void ShowGameOverMenu()
	{
		gameRunningPanel.Hide();
		endGamePanel.Show();
	}

	private void HideAllExceptOne( Panel activePanel)
	{
		//startGamePanel?.Hide();
		endGamePanel?.Hide();
	//	pauseGamePanel?.Hide();
		gameRunningPanel?.Hide();

		activePanel?.Show();
	}
}

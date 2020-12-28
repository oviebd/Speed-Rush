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
		HideAllExceptOne(startGamePanel);
	}
	public void ShowOnGameMenu()
	{
		gameRunningPanel.Show();
		HideAllExceptOne(gameRunningPanel);
	}
	public void ShowPauseGameMenu()
	{
		pauseGamePanel.Show();
		HideAllExceptOne(pauseGamePanel);
	}

	public void ShowGameOverMenu()
	{
		//gameRunningPanel.Hide();
		endGamePanel.Show();
		HideAllExceptOne(endGamePanel);
	}

	private void HideAllExceptOne( Panel activePanel)
	{
		if(activePanel != startGamePanel && startGamePanel.isActiveAndEnabled)
			startGamePanel?.Hide();
		if (activePanel != endGamePanel && endGamePanel.isActiveAndEnabled )
			endGamePanel.Hide();
		if (activePanel != pauseGamePanel && pauseGamePanel.isActiveAndEnabled)
			pauseGamePanel.Hide();
		if (activePanel != gameRunningPanel && gameRunningPanel.isActiveAndEnabled)
			gameRunningPanel.Hide();
	}
}

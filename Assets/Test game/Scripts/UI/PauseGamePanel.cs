using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGamePanel : AnimatorPanel
{
	public void OnResumeButtonClicked()
	{
		GameManager.instance.RestartGame();
		Hide();
	}

	public void OnReStartGameButtonClicked()
	{
		GameManager.instance.StartNewGame();
		Hide();
	}

	public void OnHomeButtonClicked()
	{
		UIManager.instance.ShowHomeUi();
		Hide();
	}
}

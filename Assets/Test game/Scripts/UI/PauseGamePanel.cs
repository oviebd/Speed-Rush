using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGamePanel : AnimatorPanel
{
	public void OnResumeButtonClicked()
	{
		GameManager.instance.ResumeGame();
	}

	public void OnReStartGameButtonClicked()
	{
		GameManager.instance.StartNewGame();
	}

	public void OnHomeButtonClicked()
	{
		UIManager.instance.ShowHomeUi();
	}
}

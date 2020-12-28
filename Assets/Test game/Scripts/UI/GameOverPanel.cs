using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : AnimatorPanel
{
  public void OnHomeButtonClicked()
	{
		UIManager.instance.ShowHomeUi();
	}

	public void OnRestartGameButtinClicked()
	{
		GameManager.instance.RestartGame();
	}

	public void OnLeaderboardButtonClicked()
	{
	}
}

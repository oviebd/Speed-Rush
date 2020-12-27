using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : AnimatorPanel
{
  public void OnHomeButtonClicked()
	{
		UIManager.instance.ShowHomeUi();
		Hide();
	}

	public void OnRestartGameButtinClicked()
	{
		Debug.Log("Restart Button Clicked................................");
		GameManager.instance.RestartGame();
		Hide();
	}

	public void OnLeaderboardButtonClicked()
	{
	}
}

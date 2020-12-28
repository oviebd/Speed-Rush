using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : AnimatorPanel
{
	[SerializeField] private Text currenrScoreText;

	public override void Show()
	{
		base.Show();
		currenrScoreText.text = ScoreManager.instance.GetScore() + "";
	}

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

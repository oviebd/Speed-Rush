using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : AnimatorPanel
{
	[SerializeField] private Text currenrScoreText;
	[SerializeField] private Text highScoreText;

	private void Awake()
	{
		PlayerDataSaver.onPlayerDataUpdated += OnPlayerUpdated;
	}

	private void OnDestroy()
	{
		PlayerDataSaver.onPlayerDataUpdated -= OnPlayerUpdated;
	}

	public override void Show()
	{
		base.Show();
		currenrScoreText.text = ScoreManager.instance.GetScore() + "";
		GooglePlayServiceManager.instance.AddScoreToLeaderBoard(ScoreManager.instance.GetScore());
		PlayerDataSaver.instance.SetHighScore(ScoreManager.instance.GetScore());
	}

	private void OnPlayerUpdated(PlayerDataModel data)
	{
		highScoreText.text = data.highScore.ToString();
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
		GooglePlayServiceManager.instance.ShowLeaderboardUi();
	}

	public void OnRateButtonClicked()
	{
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.AshToy.ImmuneSystem");
	}
	public void OnMoreGameButtonClicked()
	{
		Application.OpenURL("https://play.google.com/store/apps/developer?id=Ash+Toy" );
	}

	public void OnShareButtonClicked()
	{

	}
}

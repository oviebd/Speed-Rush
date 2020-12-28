using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnGameUiPanel : AnimatorPanel
{
	[SerializeField] private Text scoreText;
	private void Awake()
	{
		ScoreManager.onSCoreChanged += OnScoreChanged;
	}

	private void OnDestroy()
	{
		ScoreManager.onSCoreChanged -= OnScoreChanged;
	}

	private void OnScoreChanged(int score)
	{
		scoreText.text = score + "";
	}

	public void OnPauseGameButtonClicked()
	{
		GameManager.instance.PauseGame();
	}
}

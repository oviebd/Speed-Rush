using System.Collections;
using System.Collections.Generic;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using UnityEngine;

public class GooglePlayServiceManager : MonoBehaviour
{
	public static GooglePlayServiceManager instance;

	private void Awake()
	{
		if (instance == null)
			instance = this;
	}

	private void Start()
	{
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.Activate();

		SignIn();
	}

	void SignIn()
	{
		Social.localUser.Authenticate(success => {
			Debug.Log("Unity>> Login Succed.... ");
		});
	}


	public  void AddScoreToLeaderBoard(long score)
	{
		string leaderBordId = GPGSIds.leaderboard_hall_of_honor;
		Social.ReportScore(score, leaderBordId,success => {
			Debug.Log("Unity>>  Score Updated .... " + score);
		});
	}

	public void ShowLeaderboardUi()
	{
		Debug.Log("Unity>>  Try to show  LeaderBoard .... " );
		Social.ShowLeaderboardUI();
	}
}

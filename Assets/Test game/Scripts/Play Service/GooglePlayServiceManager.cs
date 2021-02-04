using System.Collections;
using System.Collections.Generic;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using UnityEngine;
using SaveSystem;

public class GooglePlayServiceManager : MonoBehaviour
{
	public static GooglePlayServiceManager instance;

	public delegate void OnPlayServiceSuccessfullyAuthenticated(PlayerDataModel userData);
	public static event OnPlayServiceSuccessfullyAuthenticated onAuthenticatedSuccessfully;


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
			PlayerDataModel data = new PlayerDataModel();
			data.playerName = Social.localUser.userName;
			data.playerID = Social.localUser.id;
		   UpdateUserWithHighScoreFromLeaderBoard(data);
		});
	}

	public  void AddScoreToLeaderBoard(long score)
	{
		string leaderBordId = GPGSIds.leaderboard_hall_of_honor;
	
		Social.ReportScore(score, leaderBordId,success => {
		});
	}

	public void ShowLeaderboardUi()
	{
		bool isNetworkAvilable = Utility.isNetworkAvilable();
		if(isNetworkAvilable == true)
		{
			Social.ShowLeaderboardUI();
		}
		else
		{
			DialogClass alertDialogClass = new DialogBuilder().
						 Title("Failed to connect with server").
						 Message("Please check your network connection.").
						 PositiveButtonText("Ok").
						 PositiveButtonAction(ShowLeaderBoard).
						 build();

			DialogManager.instance.SpawnDialogBasedOnDialogType(DialogTypeEnum.DialogType.AlertDialog, alertDialogClass);
		}
	}

	private void ShowLeaderBoard(IDialog iDialog)
	{
		iDialog.HideDialog();
	}

	public long UpdateUserWithHighScoreFromLeaderBoard( PlayerDataModel user)
	{
		long highScore = -1;
		Social.LoadScores(GPGSIds.leaderboard_hall_of_honor, scores =>
		{
			if (scores.Length > 0)
			{
				for (int i = 0; i < scores.Length; i++)
				{
					if (scores[i].userID == user.playerID)
					{
						highScore = scores[i].value;
						user.highScore = highScore;
						onAuthenticatedSuccessfully?.Invoke(user);
						break;
					}
				}
			}
			else
				Debug.Log("Unity>> Failed to retrieved score");
		});

		return highScore;
	}


}

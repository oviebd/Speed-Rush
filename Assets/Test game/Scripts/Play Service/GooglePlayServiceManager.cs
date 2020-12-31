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
		bool isNetworkAvilable = Utility.isNetworkAvilable();
		if(isNetworkAvilable == true)
		{
			Social.ShowLeaderboardUI();
		}
		else
		{
			DialogClass alertDialogClass = new DialogBuilder().
						 Title("Failed to connect with server").
						 Message(" Please check your network connection.").
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


}

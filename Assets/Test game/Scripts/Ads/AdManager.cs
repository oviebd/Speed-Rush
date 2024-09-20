using System;
using GoogleMobileAds.Api;
using SmileSoft_Ads_Manager;
using UnityEngine;

public class AdManager : MonoBehaviour
{
	[Header("Made it true for release build. If this is true then it will show real ads not test ads")]
	[SerializeField] private bool _isPublish = false;
	[Header("How many gameOver state needs for showing a single interstitial ad")]
	[SerializeField] private int _gameOverStateNumberForInterstitialAd = 2;
	private int _currentGameOverStateNumber = 0;

	

	public static AdManager instance;

	private void Awake()
	{
		if (instance == null)
			instance = this;

		GameManager.onGameStateChanged += OnGameStateChange;
		_currentGameOverStateNumber = 0;
	}

	private void OnDestroy()
	{
		GameManager.onGameStateChanged -= OnGameStateChange;
	}

	public void Start()
	{
		//string appId = AdUtility.GetAppId(_isPublish);

		//MobileAds.Initialize(appId);

		//	RequestBanner();
		//	InterstitialAdController.instance.SetupAd();
		//RewardAdController.instance.SetupAd();

		ShowBannerAD();


	}

	public bool GetAppPublishMode()
	{
		return _isPublish;
	}

	#region Banner Ad

	
	private void HideBannerAD()
	{
		BannerAdsController.instance.HideAD();
	}
	private void ShowBannerAD()
	{
		SmileSoftAdManager.instance.ShowBannerAd(AdSize.Banner, AdPosition.Bottom);
	}

	#endregion Banner Ad



	public void ShowRewardAd()
	{
		bool isNetworkAvilable = Utility.isNetworkAvilable();
		if (isNetworkAvilable == false)
        {
			ShowErrorDialogue("Failed to connect with internet",
				"Please check your network connection.");
			return;
		}

		SmileSoftAdManager.instance.ShowRewardAd((receivedRewardType, receivedRewardAmount, isSuccess) =>
		{
			if (isSuccess == false)
            {
				ShowErrorDialogue("Ad Server went wrong!",
				"Currently we can not show any ads! Sorry.");
            }
            else
            {
				GameManager.instance.OnRewardAdCompleted();
			}
		});
	}

	public void ShowErrorDialogue(string title, string message)
	{
		DialogClass alertDialogClass = new DialogBuilder().
						 Title(title).
						 Message(message).
						 PositiveButtonText("Ok").
						 PositiveButtonAction((IDialog dialog) =>
						 {
							 dialog.HideDialog();
							 GameManager.instance.EndGame();
						 }).
						 build();

		DialogManager.instance.SpawnDialogBasedOnDialogType(DialogTypeEnum.DialogType.AlertDialog, alertDialogClass);
	}

	public void ShowInterstitialAd()
	{
		bool isNetworkAvilable = Utility.isNetworkAvilable();
		if (isNetworkAvilable == false)
		{
			ShowErrorDialogue("Failed to connect with internet",
				"Please check your network connection.");
			return;
		}

		SmileSoftAdManager.instance.ShowInterstitialAd(isSuccess =>
		{
			if (isSuccess == false)
			{
				ShowErrorDialogue("Ad Server went wrong!",
				"Currently we can not show any ads! Sorry.");
			}
		});
	}


	private void OnGameStateChange(GameStateEnum.GAME_STATE state)
	{
		if (state == GameStateEnum.GAME_STATE.GAME_OVER)
		{
			if (_currentGameOverStateNumber != 0 && (_currentGameOverStateNumber % _gameOverStateNumberForInterstitialAd) == 0)
			{
				ShowInterstitialAd();
			}
			_currentGameOverStateNumber = _currentGameOverStateNumber + 1;
		}
	}
}
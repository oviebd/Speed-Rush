using System;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdManager : MonoBehaviour
{
	[Header("Made it true for release build. If this is true then it will show real ads not test ads")]
	[SerializeField] private bool _isPublish = false;
	[Header("How many gameOver state needs for showing a single interstitial ad")]
	[SerializeField] private int _gameOverStateNumberForInterstitialAd = 2;
	private int _currentGameOverStateNumber = 0;

	private BannerView bannerView;

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
		string appId = AdUtility.GetAppId(_isPublish);

		MobileAds.Initialize(appId);

		RequestBanner();
		InterstitialAdController.instance.SetupAd();
		RewardAdController.instance.SetupAd();

		ShowBannerAD();
	}

	public bool GetAppPublishMode()
	{
		return _isPublish;
	}

	#region Banner Ad

	public void RequestBanner()
	{
		string adUnitId = AdUtility.GetBannerAdId(_isPublish);
		bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
		AdRequest request = new AdRequest.Builder().Build();
		bannerView.LoadAd(request);
	}

	private void DestroyBanner()
	{
		if (bannerView != null)
			bannerView.Destroy();
	}
	private void HideBannerAD()
	{
		if (bannerView != null)
			bannerView.Hide();
	}
	private void ShowBannerAD()
	{
		if (bannerView != null)
			bannerView.Show();
	}

	#endregion Banner Ad



	public void ShowRewardAd()
	{
		RewardAdController.instance.ShowRewardAd();
	}

	public void ShowInterstitialAd()
	{
		InterstitialAdController.instance.ShowInterstitialAd();
	}


	private void OnGameStateChange(GameStateEnum.GAME_STATE  state)
	{
		if (state == GameStateEnum.GAME_STATE.GAME_OVER)
		{
			if ((_currentGameOverStateNumber % _gameOverStateNumberForInterstitialAd) == 0)
			{
				ShowInterstitialAd();
			}
			_currentGameOverStateNumber = _currentGameOverStateNumber + 1;
		}
	}
}
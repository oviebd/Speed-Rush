using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class RewardAdController : MonoBehaviour
{
	public static RewardAdController instance;
	private RewardedAd rewardedAd;
	[Header("how much point you get after watch a reward ad")]
	[SerializeField] private int rewardPoint = 30;

	public delegate void RewardAdLoaded(bool isLoaded);
	public static event RewardAdLoaded onRewardAdLoaded;

	private void Awake()
	{
		if (instance == null)
			instance = this;
	}

	public int GetRewardPoint()
	{
		return rewardPoint;
	}

	public void SetupAd()
	{
		string adUnitId = AdUtility.GetRewardAdId(AdManager.instance.GetAppPublishMode());
		this.rewardedAd = new RewardedAd(adUnitId);

		SetCallBacks();
		RequestRewardAd();

		if (onRewardAdLoaded != null)
			onRewardAdLoaded(false);
	}


	public void ShowRewardAd()
	{
		if (Utility.isNetworkAvilable() == false && rewardedAd.IsLoaded() == false)
		{
			/*IDialog dialog = DialogManager.instance.SpawnDialogBasedOnType(GameEnum.DialogType.ErrorDialog);
			dialog.SetTitle("No Internet!");
			dialog.SetMessage("Please check your Network connection..");*/
		}
		else
		{
			if (IsRewardAdLoaded())
			{
				rewardedAd.Show();
			}
			/*else
			{
				IDialog dialog = DialogManager.instance.SpawnDialogBasedOnType(GameEnum.DialogType.ErrorDialog);
				dialog.SetTitle("Sorry!");
				dialog.SetMessage("Can not show ad right now \n Please try again later");
			}*/
		}
	}

	public bool IsRewardAdLoaded()
	{
		bool isLoaded = false;
		if (rewardedAd != null)
			isLoaded = rewardedAd.IsLoaded();
		return isLoaded;
	}

	private void RequestRewardAd()
	{
		AdRequest request = new AdRequest.Builder().Build();
		this.rewardedAd.LoadAd(request);
	}


	#region CallBacks
	private void SetCallBacks()
	{

		// Called when an ad request has successfully loaded.
		this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
		// Called when an ad request failed to load.
		this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
		// Called when an ad is shown.
		this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
		// Called when an ad request failed to show.
		this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
		// Called when the user should be rewarded for interacting with the ad.
		this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
		// Called when the ad is closed.
		this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

	}

	public void HandleRewardedAdLoaded(object sender, EventArgs args)
	{
		//MonoBehaviour.print("HandleRewardedAdLoaded event received");
		if (onRewardAdLoaded != null)
			onRewardAdLoaded(true);
	}

	public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
	{
		/* MonoBehaviour.print(
			 "HandleRewardedAdFailedToLoad event received with message: "
							  + args.Message);*/
		SetupAd();
	}

	public void HandleRewardedAdOpening(object sender, EventArgs args)
	{
		// MonoBehaviour.print("HandleRewardedAdOpening event received");
	}

	public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
	{
		/* MonoBehaviour.print(
			 "HandleRewardedAdFailedToShow event received with message: "
							  + args.Message);*/
		SetupAd();
	}

	public void HandleRewardedAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdClosed event received");
		SetupAd();
	}

	public void HandleUserEarnedReward(object sender, Reward args)
	{
		/* string type = args.Type;
		 double amount = args.Amount;
		 MonoBehaviour.print(
			 "HandleRewardedAdRewarded event received for "
						 + amount.ToString() + " " + type);*/

		/*PlayerAchivedDataHandler.instance.SetTotalScore(PlayerAchivedDataHandler.instance.GetTotalScore() + GetRewardPoint());

		IDialog dialog = DialogManager.instance.SpawnDialogBasedOnType(GameEnum.DialogType.InfoDialog);
		dialog.SetTitle("Success!");
		dialog.SetMessage("You Get " + GetRewardPoint() + " point. \n your current point is " + PlayerAchivedDataHandler.instance.GetTotalScore());*/

	}

	#endregion CallBacks
}
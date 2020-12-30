using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class InterstitialAdController : MonoBehaviour
{
	public static InterstitialAdController instance;
	private InterstitialAd interstitial;


	private void Awake()
	{
		if (instance == null)
			instance = this;
	}

	public void SetupAd()
	{
		string adUnitId = AdUtility.GetInterstitialAdId(AdManager.instance.GetAppPublishMode());
		this.interstitial = new InterstitialAd(adUnitId);

		SetCallBacks();
		RequestAd();
	}


	public void ShowInterstitialAd()
	{
		if (interstitial.IsLoaded())
		{
			interstitial.Show();
		}
	}
	private void RequestAd()
	{
		AdRequest request = new AdRequest.Builder().Build();
		this.interstitial.LoadAd(request);
	}
	private void DestroyAd()
	{
		interstitial.Destroy();
	}


	#region CallBacks
	private void SetCallBacks()
	{
		// Called when an ad request has successfully loaded.
		this.interstitial.OnAdLoaded += HandleOnAdLoaded;
		// Called when an ad request failed to load.
		this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
		// Called when an ad is shown.
		this.interstitial.OnAdOpening += HandleOnAdOpened;
		// Called when the ad is closed.
		this.interstitial.OnAdClosed += HandleOnAdClosed;
		// Called when the ad click caused the user to leave the application.
		this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

	}

	public void HandleOnAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLoaded event received");
	}

	public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
							+ args.Message);
		DestroyAd();
		SetupAd();
	}

	public void HandleOnAdOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdOpened event received");
	}

	public void HandleOnAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdClosed event received");
		DestroyAd();
		SetupAd();
	}

	public void HandleOnAdLeavingApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLeavingApplication event received");
		DestroyAd();
	}
	#endregion CallBacks

}

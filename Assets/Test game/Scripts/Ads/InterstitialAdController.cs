using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class InterstitialAdController : MonoBehaviour
{
    public static InterstitialAdController instance;
    private InterstitialAd interstitialAd;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void SetupAd()
    {
        string adUnitId = AdUtility.GetInterstitialAdId(AdManager.instance.GetAppPublishMode());

        AdRequest request = new AdRequest();
        InterstitialAd.Load(adUnitId, request, (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogWarning("Interstitial ad failed to load: " + error);
                return;
            }

            interstitialAd = ad;
            RegisterAdCallbacks();
        });
    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            interstitialAd.Show();
        }
        else
        {
            Debug.LogWarning("Interstitial ad not ready.");
            SetupAd(); // Preload next
        }
    }

    private void RegisterAdCallbacks()
    {
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad opened.");
        };

        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad closed.");
            interstitialAd.Destroy();
            SetupAd(); // Load next ad
        };

        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogWarning("Interstitial ad failed to show: " + error.GetMessage());
            SetupAd();
        };

        interstitialAd.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad impression recorded.");
        };

        interstitialAd.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad clicked.");
        };

        interstitialAd.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log("Ad paid: " + adValue.Value);
        };
    }

    private void OnDestroy()
    {
        interstitialAd?.Destroy();
    }
}

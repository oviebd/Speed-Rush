using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class RewardAdController : MonoBehaviour
{
    public static RewardAdController instance;
    private RewardedAd rewardedAd;

    [Header("How much point you get after watching a reward ad")]
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

        AdRequest request = new AdRequest();

        RewardedAd.Load(adUnitId, request, (ad, error) =>
        {
            if (error != null)
            {
                Debug.LogError("Reward ad failed to load: " + error.GetMessage());
                onRewardAdLoaded?.Invoke(false);
                return;
            }

            Debug.Log("Reward ad loaded successfully.");

            rewardedAd = ad;

            // Setup callbacks
            rewardedAd.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("Reward ad opened.");
            };

            rewardedAd.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Reward ad closed.");
                SetupAd(); // Preload next
            };

            rewardedAd.OnAdFullScreenContentFailed += (adError) =>
            {
                Debug.LogError("Reward ad failed to show: " + adError.GetMessage());
                SetupAd();
            };

            rewardedAd.OnAdPaid += (adValue) =>
            {
                Debug.Log("Ad Paid: " + adValue.Value);
            };

            rewardedAd.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Impression recorded.");
            };

            rewardedAd.OnAdClicked += () =>
            {
                Debug.Log("Ad clicked.");
            };

            onRewardAdLoaded?.Invoke(true);
        });

        onRewardAdLoaded?.Invoke(false); // Initial state
    }

    public void ShowRewardAd()
    {
        bool isNetworkAvailable = Utility.isNetworkAvilable();

        if (isNetworkAvailable && rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((reward) =>
            {
                Debug.Log("User earned reward: " + reward.Amount);
                GameManager.instance.OnRewardAdCompleted();
            });
        }
        else
        {
            string title, message;

            if (!isNetworkAvailable)
            {
                title = "Failed to connect with internet!";
                message = "Please check your network connection.";
            }
            else
            {
                title = "Ad Server went wrong!";
                message = "Currently we cannot show any ads! Sorry.";
            }

            DialogClass alertDialogClass = new DialogBuilder()
                .Title(title)
                .Message(message)
                .PositiveButtonText("Ok")
                .PositiveButtonAction((IDialog dialog) =>
                {
                    dialog.HideDialog();
                    GameManager.instance.EndGame();
                })
                .build();

            DialogManager.instance.SpawnDialogBasedOnDialogType(DialogTypeEnum.DialogType.AlertDialog, alertDialogClass);
        }
    }

    public bool IsRewardAdLoaded()
    {
        return rewardedAd != null && rewardedAd.CanShowAd();
    }
}

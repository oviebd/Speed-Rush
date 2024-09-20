using System;
using UnityEngine;

#if SMILE_SOFT_ADMOB
using GoogleMobileAds.Api;
#endif

namespace SmileSoft_Ads_Manager
{
    public class RewardAdController : MonoBehaviour
    {
        public static RewardAdController instance;
        private string adId = "";

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

#if SMILE_SOFT_ADMOB
        private RewardedAd _rewardedAd;
#endif

        public void RequestAd(string adId, Action<bool> isAdRequestSuccess)
        {
            DestroyAd();
            this.adId = adId;

#if SMILE_SOFT_ADMOB
            var adRequest = new AdRequest();

            RewardedAd.Load(adId, adRequest,
                (RewardedAd ad, LoadAdError error) =>
                {
                    if (error != null || ad == null)
                    {
                        Debug.LogError("U>> Rewarded ad failed to load an ad " +
                                    "with error : " + error);
                        isAdRequestSuccess(false);
                        return;
                    }

                    Debug.Log("U>> Rewarded ad loaded with response : "
                            + ad.GetResponseInfo());


                    _rewardedAd = ad;
                    RegisterEventHandlers(_rewardedAd);
                    isAdRequestSuccess(true);
                });
#endif
        }


        public void ShowAd(Action<string, Double, bool> addAction)
        {
#if SMILE_SOFT_ADMOB
            if (IsAdAvailable())
            {
                _rewardedAd.Show((Reward reward) =>
                {
                    addAction(reward.Type, reward.Amount, true);
                });
            }
            else
            {
                addAction("", 0.0, false);
            }
#endif
        }

        public void HideAd()
        {
            DestroyAd();
        }


        public bool IsAdAvailable()
        {
#if SMILE_SOFT_ADMOB
            return _rewardedAd != null && _rewardedAd.CanShowAd();
#endif
            return false;
        }



        #region Private Functions


        private void DestroyAd()
        {
#if SMILE_SOFT_ADMOB
            _rewardedAd?.Destroy();
            _rewardedAd = null;
#endif
        }
#if SMILE_SOFT_ADMOB
        private void RegisterEventHandlers(RewardedAd ad)
        {
            // Raised when the ad is estimated to have earned money.
            ad.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };
            // Raised when an impression is recorded for an ad.
            ad.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Rewarded ad recorded an impression.");
            };
            // Raised when a click is recorded for an ad.
            ad.OnAdClicked += () =>
            {
                Debug.Log("Rewarded ad was clicked.");
            };
            // Raised when an ad opened full screen content.
            ad.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("Rewarded ad full screen content opened.");
            };
            // Raised when the ad closed full screen content.
            ad.OnAdFullScreenContentClosed += () =>
            {
                RequestAd(adId, _ => { });
                Debug.Log("Rewarded ad full screen content closed.");
            };
            // Raised when the ad failed to open full screen content.
            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                RequestAd(adId, _ => { });
                Debug.LogError("Rewarded ad failed to open full screen content " +
                               "with error : " + error);
            };
        }

#endif
        #endregion Private Functions
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if SMILE_SOFT_ADMOB
using GoogleMobileAds.Api;
#endif

namespace SmileSoft_Ads_Manager
{
    public class InterstitialAdController : MonoBehaviour
    {
        public static InterstitialAdController instance;
      
        private string adId = "";

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

#if SMILE_SOFT_ADMOB
        private InterstitialAd _interstitialAd;
#endif

        public void RequestAd(string adId, Action<bool> isAdRequestSuccess)
        {
            DestroyAd();
            this.adId = adId;
#if SMILE_SOFT_ADMOB

            var adRequest = new AdRequest();

            InterstitialAd.Load(adId, adRequest,
                (InterstitialAd ad, LoadAdError error) =>
                {
                    if (error != null || ad == null)
                    {
                        Debug.LogError("interstitial ad failed to load an ad " +
                                       "with error : " + error);
                        isAdRequestSuccess(false);
                        return;
                    }

                    Debug.Log("Interstitial ad loaded with response : "
                              + ad.GetResponseInfo());

                    _interstitialAd = ad;
                    RegisterEventHandlers(_interstitialAd);
                    isAdRequestSuccess(true);
                });
#endif
        }

        public void ShowAd(Action<bool> isAddLoadedSuccess = null)
        {
#if SMILE_SOFT_ADMOB
            if (IsAdAvailable())
            {
                Debug.Log("Showing interstitial ad.");
                _interstitialAd.Show();
            }

            if (isAddLoadedSuccess != null)
                isAddLoadedSuccess(IsAdAvailable());
#endif

        }

        public void HideAd()
        {
            DestroyAd();
        }


        public bool IsAdAvailable()
        {
#if SMILE_SOFT_ADMOB
            return _interstitialAd != null && _interstitialAd.CanShowAd();
#endif
            return false;
        }



        #region Private Functions


        private void DestroyAd()
        {
#if SMILE_SOFT_ADMOB
            _interstitialAd?.Destroy();
            _interstitialAd = null;
#endif
        }

#if SMILE_SOFT_ADMOB
        private void RegisterEventHandlers(InterstitialAd interstitialAd)
        {
            // Raised when the ad is estimated to have earned money.
            interstitialAd.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };

            // Raised when an impression is recorded for an ad.
            interstitialAd.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Interstitial ad recorded an impression.");
            };

            // Raised when a click is recorded for an ad.
            interstitialAd.OnAdClicked += () =>
            {
                Debug.Log("Interstitial ad was clicked.");
            };

            // Raised when an ad opened full screen content.
            interstitialAd.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("Interstitial ad full screen content opened.");
            };

            // Raised when the ad closed full screen content.
            interstitialAd.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Interstitial ad full screen content closed.");
                RequestAd(adId, _ => { });
            };
            // Raised when the ad failed to open full screen content.
            interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("Interstitial ad failed to open full screen content " +
                               "with error : " + error);
                RequestAd(adId, _ => { });
            };
        }

#endif
        #endregion Private Functions

    }
}


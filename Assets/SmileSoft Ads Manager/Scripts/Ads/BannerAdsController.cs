using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if SMILE_SOFT_ADMOB
using GoogleMobileAds.Api;
#endif

namespace SmileSoft_Ads_Manager
{
    public class BannerAdsController : MonoBehaviour
    {

        [HideInInspector] public static BannerAdsController instance;

  
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

#if SMILE_SOFT_ADMOB
        private BannerView _bannerView;
#endif
        private bool isBannerAdAvailable = false;

#if SMILE_SOFT_ADMOB
        public void ShowAD(string adId, AdSize size, AdPosition position)
        {
            if (isBannerAdAvailable)
            {
                _bannerView?.Show();
                return;
            }

            LoadAD(adId, size, position);
        }
#endif
        public void HideAD()
        {
#if SMILE_SOFT_ADMOB
            _bannerView?.Hide();
#endif
        }

        public void DestroyAD()
        {
            DestroyBannerView();
        }


#if SMILE_SOFT_ADMOB

        // bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
        private void LoadAD(string adId, AdSize size, AdPosition position)
        {
            CreateBannerView(adId, size, position);
            AdRequest request = new AdRequest();
            _bannerView.LoadAd(request);
            SetCallbacks();
        }

        private void CreateBannerView(string adUnitId, AdSize size, AdPosition position)
        {
            DestroyBannerView();
            _bannerView = new BannerView(adUnitId, size, position);
        }
#endif
        private void DestroyBannerView()
        {
#if SMILE_SOFT_ADMOB
            _bannerView?.Destroy();
            _bannerView = null;
#endif
        }

#if SMILE_SOFT_ADMOB
        private void SetCallbacks()
        {
            _bannerView.OnBannerAdLoaded += () =>
            {
                isBannerAdAvailable = true;
                Debug.Log("Banner view loaded an ad with response : "
                    + _bannerView.GetResponseInfo());
            };

            // Raised when an ad fails to load into the banner view.
            _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
            {
                Debug.LogError("Banner view failed to load an ad with error : "
                    + error);
            };
            // Raised when the ad is estimated to have earned money.
            _bannerView.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log(String.Format("Banner view paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };
            // Raised when an impression is recorded for an ad.
            _bannerView.OnAdImpressionRecorded += () =>
            {
                // Debug.Log("Banner view recorded an impression.");
            };
            // Raised when a click is recorded for an ad.
            _bannerView.OnAdClicked += () =>
            {
                // Debug.Log("Banner view was clicked.");
            };
            // Raised when an ad opened full screen content.
            _bannerView.OnAdFullScreenContentOpened += () =>
            {
                //   Debug.Log("Banner view full screen content opened.");
            };
            // Raised when the ad closed full screen content.
            _bannerView.OnAdFullScreenContentClosed += () =>
            {
                // Debug.Log("Banner view full screen content closed.");
            };

        }
#endif
    }

}




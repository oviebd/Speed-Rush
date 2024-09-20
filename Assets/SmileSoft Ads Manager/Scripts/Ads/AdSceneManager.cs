using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if SMILE_SOFT_ADMOB
using GoogleMobileAds.Api;
#endif

namespace SmileSoft_Ads_Manager
{

    public class AdSceneManager : MonoBehaviour
    {

        public void ShowBannerAd()
        {
#if SMILE_SOFT_ADMOB
            SmileSoftAdManager.instance.ShowBannerAd(AdSize.Banner, AdPosition.Bottom);
#endif
        }

        public void HideBannerAd()
        {
            BannerAdsController.instance.HideAD();
        }

        public void ShowInterstitialAd()
        {
            SmileSoftAdManager.instance.ShowInterstitialAd(isSuccess =>
            {

            });
        }

        public void HideInterstitialAd()
        {
            SmileSoftAdManager.instance.HideInterstitialAd();
        }

        public void ShowRewardAd()
        {

            SmileSoftAdManager.instance.ShowRewardAd((receivedRewardType, receivedRewardAmount, isSuccess) =>
            {

            });
        }

        public void HideRewardAd()
        {
            SmileSoftAdManager.instance.HideRewardlAd();
        }

    }

}

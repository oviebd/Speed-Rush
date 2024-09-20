using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if SMILE_SOFT_ADMOB
using GoogleMobileAds.Api;
#endif

namespace SmileSoft_Ads_Manager
{
    public class SmileSoftAdManager : MonoBehaviour
    {

        [Header("Made it true for release build. If this is true then it will show real ads not test ads")]
        [SerializeField] private bool _isPublish = false;


        [HideInInspector] public static SmileSoftAdManager instance;

        public AdsDataScriptableObject adData;


        public AdData GetAdsData()
        {
            return adData.GetAdData(_isPublish);
        }

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }



        public void Start()
        {
            RequestAds();
        }

        private void RequestAds()
        {
            string interstitialAdI = GetAdsData().interstitialAdId;
            InterstitialAdController.instance.RequestAd(interstitialAdI, isSuccess => { });

            string rewardAdId = GetAdsData().rewardAdId;
            RewardAdController.instance.RequestAd(rewardAdId, isSuccess => { });

        }

        public bool GetAppPublishMode()
        {
            return _isPublish;
        }

        public bool IsInPublishMode()
        {
            return _isPublish;
        }


#if SMILE_SOFT_ADMOB

        public void ShowBannerAd(AdSize size, AdPosition position)
        {
            string adId = GetAdsData().bannerAdId;
            BannerAdsController.instance.ShowAD(adId, size, position);
        }
#endif

        public void HideBannerAd()
        {
            BannerAdsController.instance.HideAD();
        }



        public void ShowInterstitialAd(Action<bool> adsAction)
        {
            string adId = GetAdsData().interstitialAdId;

            if (InterstitialAdController.instance.IsAdAvailable())
            {
                InterstitialAdController.instance.ShowAd(isSuccess => { adsAction(isSuccess); });
            }
            else
            {
                InterstitialAdController.instance.RequestAd(adId, isSuccess =>
                {
                    if (isSuccess)
                    {
                        InterstitialAdController.instance.ShowAd(isSuccess => { adsAction(isSuccess); });
                    }
                    else { adsAction(isSuccess); }

                });
            }
        }

        public void HideInterstitialAd()
        {
            InterstitialAdController.instance.HideAd();
        }


        public void ShowRewardAd(Action<string, Double, bool> adsAction)
        {
            string adId = GetAdsData().rewardAdId;

            if (RewardAdController.instance.IsAdAvailable())
            {
                RewardAdController.instance.ShowAd((receivedRewardType, receivedRewardAmount, isSuccess) =>
                {
                    adsAction(receivedRewardType, receivedRewardAmount, isSuccess);
                });
            }
            else
            {
                RewardAdController.instance.RequestAd(adId, isSuccess =>
                {
                    if (isSuccess)
                    {
                        RewardAdController.instance.ShowAd((receivedRewardType, receivedRewardAmount, isSuccess) =>
                        {
                            adsAction(receivedRewardType, receivedRewardAmount, isSuccess);
                        });
                    }
                    else
                    {
                        adsAction("", 0.0, isSuccess);
                    }

                });
            }
        }


        public void HideRewardlAd()
        {
            RewardAdController.instance.HideAd();
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SmileSoft_Ads_Manager {
    [Serializable]
    public class AdData
    {
        public string appId;
        public string bannerAdId;
        public string rewardAdId;
        public string interstitialAdId;
    }

}

namespace SmileSoft_Ads_Manager
{
    //[CreateAssetMenu(fileName = "AdData", menuName = "SmileSoft/AdData", order = 1)]
    public class AdsDataScriptableObject : ScriptableObject
    {
        [Header("Production Data")]
        public AdData androidData_production;
        public AdData iosData_production;

        [Header("Test Data")]
        public AdData androidData_test;
        public AdData iOSData_test;



        public AdData GetAdData(bool isPublished)
        {

#if UNITY_ANDROID
            if (isPublished) { return androidData_production; }
            else { return androidData_test; }

#elif UNITY_IPHONE
        if (isPublished) { return iosData_production; }
        else { return iOSData_test; }
#endif
            return androidData_test;

        }
    }
}







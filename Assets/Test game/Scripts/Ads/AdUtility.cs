using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AdUtility
{
	public static string GetAppId()
	{
		string appId = "";

#if UNITY_ANDROID
		//appId = "ca-app-pub-7034086702288798~4371294271";

#elif UNITY_IPHONE
         //appId = "ca-app-pub-7034086702288798~4330291049";
#endif
		return appId;

	}

	public static string GetBannerAdId(bool isPublished)
	{
		string adUnitId = "";

		if (isPublished == true)
		{
#if UNITY_ANDROID
			adUnitId = "ca-app-pub-7034086702288798/3619597188";
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-7034086702288798/3479124249";    
#endif
		}

		else
		{
			// Test Ad
#if UNITY_ANDROID
			adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/2934735716";        
#endif

		}
		return adUnitId;
	}

	public static string GetInterstitialAdId(bool isPublished)
	{
		string adUnitId = "";

		if (isPublished == true)
		{
#if UNITY_ANDROID
			adUnitId = "ca-app-pub-7034086702288798/7748266652";
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-7034086702288798/8539879235";    
#endif
		}

		else
		{
			//All Test Ad
#if UNITY_ANDROID
			adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/4411468910";        
#endif

		}
		return adUnitId;
	}

	public static string GetRewardAdId(bool isPublished)
	{
		string adUnitId = "";

		if (isPublished == true)
		{
#if UNITY_ANDROID
			adUnitId = "ca-app-pub-7034086702288798/2085161840";
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-7034086702288798/3287552554";    
#endif
		}

		else
		{
			//All Test Ad
#if UNITY_ANDROID
			adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313";        
#endif

		}
		return adUnitId;
	}

}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;




public class NativeXCore : MonoBehaviour {
	
	public static bool isDebugLogEnabled = true;
	
#if UNITY_ANDROID
	private static AndroidJavaObject instance;
	
	//Create link to our SDK
	private static AndroidJavaClass publisherPlug = new AndroidJavaClass("com.nativex.NativeXMonetizationUnity");
	//Create link to Unity Player
	public static AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
	//Ge t the current activity to use as context
	public static AndroidJavaObject currentAct = jc.GetStatic<AndroidJavaObject>("currentActivity");
#endif
	
	static NativeXCore()
	{
#if UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android){
			Debug.Log("NativeX - Android Inititialization called");
			instance = publisherPlug.CallStatic<AndroidJavaObject>("getInstance");
		}
#endif
	}
#if UNITY_IPHONE
	[DllImport ("__Internal")]
	public static extern void uStartWithNameAndApplicationId(string name, string applicationId, string publisherId);
#endif
	public static void intitialization(NativeXAndroid android, NativeXiOS iOS)
	{
#if UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android){
			Debug.Log("W3i - Initialization called");
			instance.Call("init", currentAct, android.appId, android.displayName, android.packageName, android.publisherUserId);	
		}
#elif UNITY_IPHONE	
		if(Application.platform == RuntimePlatform.IPhonePlayer){
				uStartWithNameAndApplicationId(iOS.appName, iOS.appId, iOS.publisherUserId);	
				if(isDebugLogEnabled){
					Debug.Log("initialization has been hit");
				}
			}
#endif
		return;		
	}
#if UNITY_IPHONE
	[DllImport ("__Internal")]
	public static extern void uShowOfferWall();
#endif
	public static void showRewardOfferWall()
	{
#if UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android){
			instance.Call("showOfferWall", currentAct);
		}
#endif
	
#if UNITY_IPHONE
		if(Application.platform == RuntimePlatform.IPhonePlayer){
				uShowOfferWall();
				if(isDebugLogEnabled){
					Debug.Log("showOfferWall has been hit");
				}
		}
#endif
		return;
	}
		
#if UNITY_IPHONE
	[DllImport ("__Internal")]
	public static extern void uShowIncentOfferWall();
#endif
	
	public static void showRewardWebOfferwall()
	{
#if UNITY_ANDROID
			if(Application.platform == RuntimePlatform.Android){
				instance.Call("showWebOfferWall", currentAct);
			}
#endif
	
#if UNITY_IPHONE
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uShowIncentOfferWall();	
			if(isDebugLogEnabled){
				Debug.Log("showWebOfferWall has been hit");
			}
		}
#endif
		return;
	}

#if UNITY_IPHONE
	[DllImport ("__Internal")]
	public static extern void uShowNonIncentOfferWall();
#endif

	public static void showNonRewardWebOfferwall()
	{
#if UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android){
			instance.Call("showNonRewardWebOfferWall", currentAct);
		}
#elif UNITY_IPHONE
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uShowNonIncentOfferWall();	
			if(isDebugLogEnabled){
				Debug.Log("showNonIncentWebOfferWall has been hit");
			}
		}
#endif
	}

#if UNITY_IPHONE
	[DllImport ("__Internal")]
	public static extern void uGetAndCacheFeaturedOffer();
#endif

	public static void getAndCacheFeaturedOffer()
	{
#if UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android){
			instance.Call("getFeaturedOffer", currentAct);
		}
#elif UNITY_IPHONE
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uGetAndCacheFeaturedOffer();
			if(isDebugLogEnabled){
				Debug.Log("getAndCacheFeaturedOffer has been hit");
			}
		}
#endif
	}

#if UNITY_IPHONE
	[DllImport ("__Internal")]
	public static extern void uShowCachedFeaturedOffer();
#endif

	public static void showCachedFeaturedOffer()
	{
#if UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android){
			instance.Call("showCachedFeaturedOffer", currentAct);
		}
#elif UNITY_IPHONE
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uShowCachedFeaturedOffer();	
			if(isDebugLogEnabled){
				Debug.Log("iOS - showCachedFeaturedOffer has been hit");
			}
		}
#endif
	}

#if UNITY_IPHONE
	[DllImport ("__Internal")]
	public static extern void uShowFeaturedOffer();
#endif

	public static void showFeaturedOffer()
	{
#if UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android){
			instance.Call("showFeaturedOffer", currentAct);
		}
#elif UNITY_IPHONE
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uShowFeaturedOffer();
			if(isDebugLogEnabled){
				Debug.Log("showFeaturedOffer has been hit");
			}
		}
#endif
	}

#if UNITY_IPHONE
	[DllImport ("__Internal")]
	public static extern void uGetAndCacheInterstitial();
#endif

	public static void getAndCacheInterstitial()
	{
#if UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android){
			instance.Call("getAndCacheInterstitial", currentAct);
		}
#elif UNITY_IPHONE
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uGetAndCacheInterstitial();
			if(isDebugLogEnabled){
				Debug.Log("showInterstitial has been hit");
			}
		}
#endif
	}

#if UNITY_IPHONE
	[DllImport ("__Internal")]
	public static extern void uShowCachedInterstitial();
#endif

	public static void showCachedInterstitial()
	{
#if UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android){
			instance.Call("showCachedInterstitial", currentAct);
		}
#elif UNITY_IPHONE
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uShowCachedInterstitial();
			if(isDebugLogEnabled){
				Debug.Log("showInterstitial has been hit");
			}
		}
#endif
	}

#if UNITY_IPHONE
	[DllImport ("__Internal")]
	public static extern void uShowInterstitial();
#endif

	public static void showInterstitial()
	{
#if UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android){
			instance.Call("showNonRewardInterstitial", currentAct);
		}
#elif UNITY_IPHONE
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uShowInterstitial();
			if(isDebugLogEnabled){
				Debug.Log("showInterstitial has been hit");
			}
		}
#endif
	}


}

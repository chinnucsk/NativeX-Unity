using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

//Version:4.2.2

public class NativeXCore : MonoBehaviour {
	
	public static bool isDebugLogEnabled = true;
	public static NativeXiOS iOSDevice;
	public static NativeXAndroid androidDevice;
	
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
	public static extern void uStartWithNameAndApplicationId(string name, string applicationId, string publisherId, bool enableLogging);
	[DllImport ("__Internal")]
	public static extern void uSetCoordinates(float bannerX, float bannerY, float bannerHeight, float bannerWidth, float offerWallX, float offerWallY);
#endif
	public static void initialization(NativeXAndroid android, NativeXiOS iOS)
	{
#if UNITY_ANDROID
		//if(android != null){
			androidDevice = android;
			if(Application.platform == RuntimePlatform.Android){
				Debug.Log("W3i - Initialization called");
				Debug.Log("Android Device: "+androidDevice.ToString());
				instance.Call("init", currentAct, android.appId, android.displayName, android.packageName, android.publisherUserId, android.enableLogging);	
			}
		//}else{
		//	Debug.Log("No NativeXAndroid object exists");
		//}
#elif UNITY_IPHONE	
		//if(iOS!=null){
			iOSDevice = iOS;
			if(Application.platform == RuntimePlatform.IPhonePlayer){
				uStartWithNameAndApplicationId(iOS.appName, iOS.appId.ToString(), iOS.publisherUserId, iOS.enableLogging);
				uSetCoordinates(iOS.bannerX, iOS.bannerY, iOS.bannerHeight, iOS.bannerWidth, iOS.offerWallX, iOS.offerWallY);
			}
		//}else{
		//	Debug.Log("No NativeXiOS object exists.");
		//}
#endif
		if(isDebugLogEnabled){
			Debug.Log("initialization has been hit");
		}
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
				
		}
#endif
		if(isDebugLogEnabled){
			Debug.Log("showOfferWall has been hit");
		}
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
			
		}
#endif
		if(isDebugLogEnabled){
			Debug.Log("showWebOfferWall has been hit");
		}
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
			
		}
#endif
		if(isDebugLogEnabled){
			Debug.Log("showNonIncentWebOfferWall has been hit");
		}
	}

#if UNITY_IPHONE
	[DllImport ("__Internal")]
	public static extern void uGetAndCacheFeaturedOffer();
#endif
	/**
 	* Fetch a Featured Offer
 	* 
 	*/
	public static void fetchFeaturedOffer()
	{
#if UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android){
			instance.Call("getFeaturedOffer", currentAct);
		}
#elif UNITY_IPHONE
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uGetAndCacheFeaturedOffer();
			
		}
#endif
		if(isDebugLogEnabled){
			Debug.Log("getAndCacheFeaturedOffer has been hit");
		}
	}

#if UNITY_IPHONE
	[DllImport ("__Internal")]
	public static extern void uShowCachedFeaturedOffer();
#endif
	/**
 	* Shows the already fetched Featured Offer
 	* 
 	*/
	public static void showFetchedFeaturedOffer()
	{
#if UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android){
			instance.Call("showCachedFeaturedOffer", currentAct);
		}
#elif UNITY_IPHONE
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uShowCachedFeaturedOffer();	
			
		}
#endif
		if(isDebugLogEnabled){
			Debug.Log("iOS - showCachedFeaturedOffer has been hit");
		}
	}

#if UNITY_IPHONE
	[DllImport ("__Internal")]
	public static extern void uShowFeaturedOffer();
#endif
	/**
 	* Show a Features Offer
 	* 
 	*/
	public static void showFeaturedOffer()
	{
#if UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android){
			instance.Call("showFeaturedOffer", currentAct);
		}
#elif UNITY_IPHONE
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uShowFeaturedOffer();
		}
#endif
		if(isDebugLogEnabled){
			Debug.Log("showFeaturedOffer has been hit");
		}
	}

#if UNITY_IPHONE
	[DllImport ("__Internal")]
	public static extern void uFetchInterstitial(string name);
#endif
	/**
 	* Fetch an enhanced interstitial
 	* 
 	* @param name      string representation of placement name (optional)
 	*/
	public static void fetchInterstitial(string name)
	{
#if UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android){
			if(name == null){
				name = "";
			}
			instance.Call("fetchInterstitial", currentAct, name);
		}
#elif UNITY_IPHONE
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uFetchInterstitial(name);	
		}
#endif
		if(isDebugLogEnabled){
			Debug.Log("getandCacheInterstitial has been hit");
		}
	}

//#if UNITY_IPHONE
//	[DllImport ("__Internal")]
//	public static extern void uShowCachedInterstitial(string name);
//#endif
//
//	public static void showCachedInterstitial(string name)
//	{
//				
//#if UNITY_ANDROID
//		if(Application.platform == RuntimePlatform.Android){
//			if(name == null){
//				name = "";
//			}
//			instance.Call("showCachedInterstitial", currentAct, name);
//		}
//#elif UNITY_IPHONE
//		if(Application.platform == RuntimePlatform.IPhonePlayer){
//			uShowCachedInterstitial(name);		
//		}
//#endif
//		if(isDebugLogEnabled){
//				Debug.Log("showCachedInterstitial has been hit");
//			}
//	}

#if UNITY_IPHONE
	[DllImport ("__Internal")]
	public static extern void uShowInterstitial(string name);
#endif
	/**
 	* Show an enhanced interstitial with placement name from key window, 
 	* used for targeting certain ads for cetain in app placements.
 	* 
 	* @param name   string representation of placement name(optional)
 	*/
	public static void showInterstitial(string name)
	{
#if UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android){
			if(name == null){
				name = "";
			}
			instance.Call("showInterstitial",currentAct, name);	
		}
#elif UNITY_IPHONE
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uShowInterstitial(name);
		}
#endif
		if(isDebugLogEnabled){
			Debug.Log("showInterstitial has been hit");
		}
	}
	
//	public static void showBanner(string name, Rect position)
//	{
//#if UNITY_ANDROID
//	if(Application.platform == RuntimePlatform.Android){
//			if(name == null){
//				name = "";
//			}
//			instance.Call("showBanner" , currentAct, name, Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y), Mathf.RoundToInt(position.height), Mathf.RoundToInt(position.width));
//		}
//#elif UNITY_IPHONE
//		
//#endif
//		if(isDebugLogEnabled){
//			Debug.Log("showBanner has been hit");
//		}
//	}

#if UNITY_IPHONE
	[DllImport ("__Internal")]
	public static extern void uRedeemCurrency();
#endif
	public static void redeemCurrency()
	{
#if UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android){
			instance.Call("redeemCurrency", currentAct, true);
		}
#elif UNITY_IPHONE
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uRedeemCurrency();	
			
		}
#endif
		if(isDebugLogEnabled){
			Debug.Log("redeemCurrency has been hit");
		}
	}

//These methods were added as a request from a specific client and should not be used in typical integrations!
//	public static void checkBalances(){
//#if UNITY_ANDROID
//		if(Application.platform == RuntimePlatform.Android){
//			instance.Call("checkBalances", currentAct);
//		}
//#endif
//		return;
//	}
//
//	public static void transferBalances(bool show){
//		#if UNITY_ANDROID
//		if(Application.platform == RuntimePlatform.Android){
//			if(balance != null){balance.Clear();}
//			instance.Call("transferBalances", currentAct, show);
//		}
//#endif
//		return;
//	}


#if UNITY_IPHONE
	[DllImport ("__Internal")]	
	public static extern void uConnectWithAppId(string appId);
#endif
	public static void appWasRun()
	{
#if UNITY_ANDROID
		if(androidDevice.appId!=null)
		{
			if(Application.platform == RuntimePlatform.Android){
				instance.Call("appWasRun", androidDevice.appId);
			}
		}
#elif UNITY_IPHONE
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			if(null!=iOSDevice.appId)
			{
				uConnectWithAppId(iOSDevice.appId.ToString());
				
			}
		}
#endif
		if(isDebugLogEnabled){
			Debug.Log("appWasRun has been hit");
		}
	}

	//This Call this to perform an Action Taken call.
#if UNITY_IPHONE
	[DllImport ("__Internal")]
	public static extern void uActionTakenWithActionId(string actionId);
#endif
	public static void actionTaken()
	{
#if UNITY_ANDROID
		if(androidDevice.actionId!=null)
		{
			if(Application.platform == RuntimePlatform.Android){
				instance.Call("actionTaken", androidDevice.actionId);
			}
		}
#elif UNITY_IPHONE
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			if(iOSDevice.actionId!=null)
			{
				uActionTakenWithActionId(iOSDevice.actionId.ToString());
				
			}
		}
#endif
		if(isDebugLogEnabled){
			Debug.Log("actionTaken has been hit");
		}
	}

	//This is an Android only function used to aler the consumer to turn on Auto-matic updates
	public static void upgradeAndroidApp(string currency, int amount){
#if UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android){
			instance.Call("upgradeApp", currentAct, currency, amount);
		}
#endif
		return;
	}

	//This is an Android only function to alert the consumer to rate your app
	public static void rateAndroidApp(string currency, int amount){
#if UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android){
			instance.Call("rateApp", currentAct, currency, amount);
		}
#endif
		return;
	}
	
#if UNITY_IPHONE
	[DllImport ("__Internal")]
	public static extern void uTrackInAppPurchase(string storeProductId, string storeTransactionId,
	                                              float costPerItem, int quantity, string productTitle);
#endif

	//Called to track an In-App-Purchase
	//Raises event Action<bool> W3iHandler.e_didTrackInAppPurchaseSucceed on completion
	public static void trackInAppPurchase(string storeProductId, string storeTransactionId,
	                                      float costPerItem, int quantity, string productTitle)
	{
#if UNITY_IPHONE
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			uTrackInAppPurchase(storeProductId, storeTransactionId, costPerItem,quantity,productTitle);	
		}
#endif
		return;
	}

#if UNITY_IPHONE
	[DllImport ("__Internal")]
	public static extern void uSelectServer(string url);
#endif
	public static void selectServer(string server)
	{
#if UNITY_IPHONE
		switch(server)
		{
		case "BETA":
			uSelectServer("http://beta.api.w3i.com");
			break;
		case "DEV":
			uSelectServer("http://api.w3i.teamfreeze.com");
			break;
		case "PROD":
			uSelectServer("http://appclick.co");
			break;
		default:
			uSelectServer("http://appclick.co");
			break;
		}
#endif
		return;
		
	}

	
}

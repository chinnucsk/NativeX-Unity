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
	public static void showOfferWall()
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
		
}

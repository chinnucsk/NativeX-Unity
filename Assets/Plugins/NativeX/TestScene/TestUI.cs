using UnityEngine;
using System.Collections.Generic;


public class TestUI: MonoBehaviour
{
	public static string resultText = "default";
	public static NativeXAndroid android;
	public static NativeXiOS iOS;
	
	void Start()
	{
		iOS = new NativeXiOS("12198", "TestApp", null);
		android = new NativeXAndroid(5077, "android.PackName", "TestApp", "Test Pub");
		Debug.Log("NativeX - Unity has Started");
		NativeXCore.intitialization(android, iOS);
	}
	
	void OnGUI()
	{
		float yPos = 5.0f;
		float xPos = 5.0f;
		float width = ((Screen.width/2) - (Screen.width/15));
		float height = Screen.height/10;
		float heightPlus = height + 10.0f;
	
		
		if( GUI.Button( new Rect( xPos, yPos, width, height ), "Show Offer Wall" ) )
		{
			NativeXCore.showRewardOfferWall();
			Debug.Log("Show Offer Wall has been clicked");
		}
		
		if( GUI.Button( new Rect( xPos, yPos+=heightPlus, width, height ), "Show Reward Web Offer Wall" ) )
		{
			NativeXCore.showRewardWebOfferwall();
			Debug.Log("Show Reward Web Offer Wall has been clicked");
		}

		if( GUI.Button( new Rect( xPos, yPos+=heightPlus, width, height ), "Show Non-Reward Web Offer Wall" ) )
		{
			NativeXCore.showNonRewardWebOfferwall();
			Debug.Log("Show Non-Reward Web Offer Wall has been clicked");
		}

		if( GUI.Button( new Rect( xPos, yPos+=heightPlus, width, height ), "Get And Cache Featured Offer" ) )
		{
			NativeXCore.getAndCacheFeaturedOffer();
			Debug.Log("Get And Cache Featured Offer has been clicked");
		}

		if( GUI.Button( new Rect( xPos, yPos+=heightPlus, width, height ), "Show Cached Featured Offer" ) )
		{
			NativeXCore.showCachedFeaturedOffer();
			Debug.Log("Show Cached Featured Offer has been clicked");
		}

		if( GUI.Button( new Rect( xPos, yPos+=heightPlus, width, height ), "Show Featured Offer" ) )
		{
			NativeXCore.showFeaturedOffer();
			Debug.Log("Show Featured Offer has been clicked");
		}

		if( GUI.Button( new Rect( xPos, yPos+=heightPlus, width, height ), "Get And Cache Interstitial" ) )
		{
			NativeXCore.getAndCacheInterstitial();
			Debug.Log("Get And Cache Interstitial has been clicked");
		}

		if( GUI.Button( new Rect( xPos, yPos+=heightPlus, width, height ), "Show Cached Interstitial" ) )
		{
			NativeXCore.showCachedInterstitial();
			Debug.Log("Show Cached Interstitial has been clicked");
		}

		if( GUI.Button( new Rect( xPos, yPos+=heightPlus, width, height ), "Show Interstitial" ) )
		{
			NativeXCore.showInterstitial();
			Debug.Log("Show Interstitial has been clicked.");
		}
		
		GUI.Label(new Rect(xPos, yPos += heightPlus, width, height), resultText);
		
	}
	
}



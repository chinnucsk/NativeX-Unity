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
			NativeXCore.showOfferWall();
			Debug.Log("Show Offer Wall has been clicked");
		}
		
		if( GUI.Button( new Rect( xPos, yPos+=heightPlus, width, height ), "Show Web Offer Wall" ) )
		{
			NativeXCore.showRewardWebOfferwall();
			Debug.Log("Show Web Offer Wall has been clicked");
		}
		
		GUI.Label(new Rect(xPos, yPos += heightPlus, width, height), resultText);
		
	}
	
}



using UnityEngine;
using System.Collections;

public class Initialize : MonoBehaviour {
	
	public static NativeXAndroid android;
	public static NativeXiOS iOS;
	
	void OnClick()
	{
		iOS = new NativeXiOS(VariableHolder.appId, "TestApp", null, 200, 300, Screen.height/20, Screen.width/2, 100, 200);
		android = new NativeXAndroid(VariableHolder.appId, "android.PackName", "TestApp", "Test Pub");
		iOS.enableLogging = true;
		android.enableLogging = true;
#if UNITY_IPHONE
		iOS.actionId = VariableHolder.actionId;
		Debug.Log("NativeX - Unity  Is being Inititialized with iOSDevice:" + iOS.ToString());
#elif UNITY_ANDROID
		android.actionId = VariableHolder.actionId;
		Debug.Log("NativeX - Unity  Is being Inititialized with androidDevice:" + android.ToString());
#endif
		NativeXCore.initialization(android, iOS);
		
	}
}

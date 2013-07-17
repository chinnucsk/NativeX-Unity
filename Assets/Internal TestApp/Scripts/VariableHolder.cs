using UnityEngine;
using System.Collections;

public class VariableHolder : MonoBehaviour{
	
	public static int appId;
	public static int actionId;
	public static string interstitialName = "";
	
	static VariableHolder () 
	{
		
	}
	
	void Start(){
		Debug.Log("Variables have been set to defaults");
#if UNITY_IPHONE
		if(appId == 0){
			appId = 12198;
		}
		if(actionId == 0){
			actionId = 17;
		}
#elif UNITY_ANDROID
		if(appId == 0){
			appId = 5077;
		}
		if(actionId == 0){
			actionId = 16;
		}
#endif
	}
	
}

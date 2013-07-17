using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

	// Use this for initialization
	void Start () {
#if UNITY_IPHONE
		if(PlayerPrefs.GetInt("appId") == 0){
			PlayerPrefs.SetInt("appId", 12198);
		}
		if(PlayerPrefs.GetInt("actionId") == 0){
			PlayerPrefs.SetInt("actionId", 17);
		}
#elif UNITY_ANDROID
		if(PlayerPrefs.GetInt("appId") == 0){
			PlayerPrefs.SetInt("appId", 5077);
		}
		if(PlayerPrefs.GetInt("actionId") == 0){
			PlayerPrefs.SetInt("actionId", 16);
		}
#endif
		if(PlayerPrefs.GetString("InterstitialName") == "")
		{
			PlayerPrefs.SetString("InterstitialName", "HEHE");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class SetInterstitialName : MonoBehaviour {

	public UIInput input;
	
	void OnSubmit()
	{
//		PlayerPrefs.SetString("InterstitialName", input.text);
//		Debug.Log("InterstitialName has been set to: "+PlayerPrefs.GetString("InterstitialName"));
		VariableHolder.interstitialName = input.text;
		Debug.Log("ActionId has been set to: "+VariableHolder.interstitialName);
	}
}

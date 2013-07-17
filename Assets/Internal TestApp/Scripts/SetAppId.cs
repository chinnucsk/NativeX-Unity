using UnityEngine;
using System.Collections;

public class SetAppId : MonoBehaviour {
	
	public UIInput input;
	
	void OnSubmit()
	{
//		PlayerPrefs.SetInt("appId", int.Parse(input.text));	
//		Debug.Log("AppId has been set to: "+PlayerPrefs.GetInt("appId"));
		VariableHolder.appId = int.Parse(input.text);
		Debug.Log("AppId has been set to: "+VariableHolder.appId);
	}
}

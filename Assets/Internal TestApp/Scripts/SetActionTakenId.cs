using UnityEngine;
using System.Collections;

public class SetActionTakenId : MonoBehaviour {
	public UIInput input;
	
	void OnSubmit()
	{
//		PlayerPrefs.SetInt("actionId", int.Parse(input.text));
//		Debug.Log("ActionId has been set to: "+PlayerPrefs.GetInt("actionId"));
		VariableHolder.actionId = int.Parse(input.text);
		Debug.Log("ActionId has been set to: "+VariableHolder.actionId);
	}
}

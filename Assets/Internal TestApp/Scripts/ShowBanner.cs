using UnityEngine;
using System.Collections;

public class ShowBanner : MonoBehaviour {

	void OnClick()
	{
//		NativeXCore.showBanner("", new Rect(10,10,100,350));
		Debug.Log("Height: "+Screen.height +" Width: "+Screen.width);
	}
}

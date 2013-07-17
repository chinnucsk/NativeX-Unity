using UnityEngine;
using System.Collections;

public class ShowInterstitial : MonoBehaviour {

	void OnClick()
	{
		NativeXCore.showInterstitial(VariableHolder.interstitialName);
		
	}
}

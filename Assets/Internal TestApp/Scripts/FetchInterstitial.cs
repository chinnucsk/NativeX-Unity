using UnityEngine;
using System.Collections;

public class FetchInterstitial : MonoBehaviour {

	void OnClick()
	{
		NativeXCore.fetchInterstitial(VariableHolder.interstitialName);	
	}
}

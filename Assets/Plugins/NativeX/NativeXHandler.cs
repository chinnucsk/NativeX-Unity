using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class NativeXHandler : MonoBehaviour {

	public static event Action<bool> e_didSDKinitialize;
	public static event Action<string> e_didInterstitialLoad;
	public static event Action<bool> e_didFeaturedOfferLoad;
	public static event Action<bool> e_didBannerLoad;
	public static event Action<string> e_actionCompleted;
	public static event Action<string> e_actionFailed;
	public static event Action<bool> e_userLeavingApplication;
	public static event Action<List<NativeXBalance>> e_balanceTransfered;
	public static event Action<string> e_receiptId;
	public static event Action<bool> e_didPerformAction;
	public static bool buttonEnabled = false;


	void OnGUI()
	{
			if (buttonEnabled) {
						if (GUI.Button (new Rect( 0, 0, Screen.width, Screen.height ), "")) {
								Debug.Log ("You pushed my button!");
						}
			}
	}

	public void didSDKinitialize(string init)
	{
		if(e_didSDKinitialize!=null){
			if(init == "1"){
				e_didSDKinitialize(true);
			}
			else{
				e_didSDKinitialize(false);
			}
		}
	}

	public void didInterstitialLoad(string load)
	{
		if(e_didInterstitialLoad!=null){
			if(load == "1"){
				e_didInterstitialLoad(true);
			}
			else if (load == "0"){
				e_didInterstitialLoad(false);
			}
			else{
				e_didInterstitialLoad(load);	
			}
		}

	}

	public void didFeaturedOfferLoad(string load)
	{
		if(e_didFeaturedOfferLoad!=null){
			if(load == "1"){
				e_didFeaturedOfferLoad(true);
			}
			else{
				e_didFeaturedOfferLoad(false);
			}
		}
	}

	public void didBannerLoad(string load)
	{
		if(e_didBannerLoad!=null){
			if(load == "1"){
				e_didBannerLoad(true);
			}
			else{
				e_didBannerLoad(false);
			}
		}
	}

	public void actionComplete(string type)
	{
		if(e_actionCompleted!=null){
			if(type!=null){
				e_actionCompleted(type);
			}
		}
		if (type == "6") {
			buttonEnabled = false;
		}
	}

	public void actionFailed(string type)
	{
		if(e_actionFailed!=null){
			if(type !=null){
				e_actionFailed(type);
			}
		}
	}

	public void userLeavingApplication(string leaving)
	{
		if(e_userLeavingApplication!=null){
						if (leaving == "0") {
								e_userLeavingApplication (false);
						} else {
								e_userLeavingApplication (true);
						}
		}
	}

	public void balanceTransfered(string json)
	{
		if(e_balanceTransfered!=null)
		{
			Debug.Log("We have hit the balanceTransfered()");
			if(json != null){
				if(json.Length > 0){
					Debug.Log("Found a Balance");
					Debug.Log(json);
					e_balanceTransfered(NativeXBalance.convertJson(json));}
				else{
					e_balanceTransfered(new List<NativeXBalance>());}
				
			}
		}
	}

	public void receiptId(string myReceipt)
	{
		if(e_receiptId!=null){
			if(myReceipt!=null){
			e_receiptId(myReceipt);
			}
		}
	}

	public void didPerformAction(string action)
		{
				if (e_didPerformAction != null) {
						if (action == "1") {
								e_didPerformAction (true);
						} else {
								e_didPerformAction (false);
						}
				}
		}

}

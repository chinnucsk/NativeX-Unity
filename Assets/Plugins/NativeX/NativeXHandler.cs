using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class NativeXHandler : MonoBehaviour {

	public static event Action<bool> e_didInterstitialLoad;
	public static event Action<bool> e_didFeaturedOfferLoad;
	public static event Action<bool> e_didBannerLoad;
	public static event Action<string> e_actionCompleted;
	public static event Action<string> e_actionFailed;
	public static event Action<bool> e_userLeavingApplication;
	public static event Action<List<NativeXBalance>> e_balanceTransfered;
	public static event Action<string> e_receiptId;


	public static void didInterstitialLoad(string load)
	{
		if(e_didInterstitialLoad!=null){
			if(load == "1"){
				e_didInterstitialLoad(true);
			}
			else{
				e_didInterstitialLoad(false);
			}
		}

	}

	public static void didFeaturedOfferLoad(string load)
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

	public static void didBannerLoad(string load)
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

	public static void actionComplete(string type)
	{
		if(e_actionCompleted!=null){
			if(type!=null){
				e_actionCompleted(type);
			}
		}
	}

	public static void actionFailed(string type)
	{
		if(e_actionFailed!=null){
			if(type !=null){
				e_actionFailed(type);
			}
		}
	}

	public static void userLeavingApplication(string leaving)
	{
		if(e_userLeavingApplication!=null){
			if(leaving == "1")
			{
				e_userLeavingApplication(true);
			}else{
				e_userLeavingApplication(false);
			}
		}
	}

	public static void balanceTransfered(string json)
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

	public static void receiptId(string myReceipt)
	{
		if(e_receiptId!=null){
			if(myReceipt){
			e_receiptId(myReceipt);
			}
		}
	}

}

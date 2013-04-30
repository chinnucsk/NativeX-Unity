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


	public static void didInterstitialLoad(string load)
	{
		if(load == "1"){
			e_didInterstitialLoad(true);
		}
		else{
			e_didInterstitialLoad(false);
		}

	}

	public static void didFeaturedOfferLoad(string load)
	{
		if(load == "1"){
			e_didFeaturedOfferLoad(true);
		}
		else{
			e_didFeaturedOfferLoad(false);
		}
	}

	public static void didBannerLoad(string load)
	{
		if(load == "1"){
			e_didBannerLoad(true);
		}
		else{
			e_didBannerLoad(false);
		}
	}
}

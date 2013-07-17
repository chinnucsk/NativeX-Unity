using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResultsManager : MonoBehaviour {
	public string resultText;
	public UILabel label;
	
	void OnEnable()
	{
		NativeXHandler.e_didSDKinitialize += didSDKInititialize;
		NativeXHandler.e_didFeaturedOfferLoad += didFeaturedOfferLoad;
		NativeXHandler.e_didInterstitialLoad += didInterstitialLoad;
		NativeXHandler.e_actionCompleted += actionComplete;
		NativeXHandler.e_actionFailed += actionFailed;
		NativeXHandler.e_userLeavingApplication += userLeavingApplication;
		NativeXHandler.e_balanceTransfered += balanceTransfered;
		NativeXHandler.e_receiptId += receiptId;
		NativeXHandler.e_didPerformAction += didPerformAction;	
	}
	

	void  OnDisable() 
	{
		NativeXHandler.e_didSDKinitialize -= didSDKInititialize;
		NativeXHandler.e_didFeaturedOfferLoad -= didFeaturedOfferLoad;
		NativeXHandler.e_didInterstitialLoad -= didInterstitialLoad;
		NativeXHandler.e_actionCompleted -= actionComplete;
		NativeXHandler.e_actionFailed -= actionFailed;
		NativeXHandler.e_userLeavingApplication -= userLeavingApplication;
		NativeXHandler.e_balanceTransfered -= balanceTransfered;
		NativeXHandler.e_receiptId -= receiptId;
		NativeXHandler.e_didPerformAction -= didPerformAction;
	}
	
	void Update()
	{
		label.text = resultText;
	}
	
	void userLeavingApplication (bool obj)
	{
		resultText = "userLeavingApplication:" +obj;
	}
	
	void receiptId (string obj)
	{
		resultText = "receiptId:" +obj;
	}
	
	void balanceTransfered (List<NativeXBalance> obj)
	{
		resultText = "";
		foreach(var me in obj){
			resultText += "balanceTransfered:" +me.ToString()+"\n";
			Debug.Log("Amout: "+ me.Amount + "-- Display Name: "+me.DisplayName+" -- External: "+me.ExternalCurrencyId+" -- ID: "+me.Id);
		}

	}
	
	void actionFailed (string obj)
	{
		resultText = "actionFailed:" +obj;
	}
	
	void actionComplete (string obj)
	{
		resultText = "actionComplete:" +obj;
	}
	
	void didInterstitialLoad (string obj)
	{
		resultText = "didInterstitialLoad:" +obj;
		Debug.Log(resultText);
	}
	
	void didFeaturedOfferLoad (bool obj)
	{
		resultText = "didFeaturedOfferLoad:" +obj;
	}
	
	void didSDKInititialize (bool obj)
	{
		resultText = "didSDKInititialize:" +obj;
	}
	
	void didPerformAction(bool action)
	{
			resultText = "didPerformAction: " + action;
	}
	
}

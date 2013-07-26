using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class NativeXHandler : MonoBehaviour {
	
	/** Called when SDK has finished initializing
 	*
 	* @param bool - Will return true if the SDK has successfully initialized
 	* 				Well return false if there was an error
	*/
	public static event Action<bool> e_didSDKinitialize;
	/** Called when the interstitial is loaded and ready to be displayed
 	*  If showInterstitial() was called this will fire immediately before the ad is shown
 	*  If using fetchInterstitial() use this method to know 
 	*  when you have an interstitial ready to show instantly
 	*
 	* @param string - this string will be returned with the name of the interstitial that has been loaded
	 *                 if the interstitial has no name it will return "INTERSTITIAL_LOADED"
 	*                 if no interstitial was loaded this event will return "NO_INTERSTITIAL_LOADED"
 	*/
	public static event Action<string> e_didInterstitialLoad;
	/** Called when the featured offer is loaded and ready to be displayed
	*  If showFeaturedOffer() was called this will fire immediately before the ad is shown
 	*  If using getAndCacheFeaturedOffer() use this method to know 
 	*  when you have a featured offer ready to show instantly
 	*
 	* @param bool - Will return true if we have successfully cached a featured offer
 	* 				Well return false if there was an error
	*/
	public static event Action<bool> e_didFeaturedOfferLoad;
	/** Called when the Ad Unit has been closed by the user after being displayed
 	*
 	* @param string - This string will return the name of the interstitial that has been closed
 	*                 if the interstitial has no name it will return "INTERSTITIAL_LOADED"
	*/
	public static event Action<string> e_actionCompleted;
	/** Called when the Ad Unit has failed to either load or display
 	*
	 * @param string - This string will return the name of the interstitial that has failed to load
 	*                 if the interstitial has no name it will return "INTERSTITIAL_LOADED"
 	*/
	public static event Action<string> e_actionFailed;
	/** Called when the User has chosen to go through with the presented offer and will be redirected from you application
 	*	
 	* @param string - This string will return the name of the interstitial that has redirected the user
 	*                 if the interstitial has no name it will return "INTERSTITIAL_LOADED"
 	*/
	public static event Action<bool> e_userLeavingApplication;
	/** Called when a balance is returned from calling redeemBalance()
	 * 
	 *  @param List<NativeXBalance> - This List will return any number of NativeXBalance objects containing information that 
	 * 								  should be used to award the player with the credits they earned
	 */
	public static event Action<List<NativeXBalance>> e_balanceTransfered;
	/** Called when a balance is returned from calling redeemBalance()
	 * 
	 * @param string - This string will return with the receipt Id for that currency redemption
	 * 
	 */
	public static event Action<string> e_receiptId;
	/** Called when the SDK has successfully intitialized
	 * 
	 * @param string - Will contain the current Session Id
	 * 
	 */
	public static event Action<string> e_sessionId;
	/** Called when a user clicks through to either rate your app or sign up for automatic updates(Android only)
	 * 
	 * @param bool - Will return true of they continue, false if the close or cancel
	 * 
	 */
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

	public void didInterstitialLoad(string i_name)
	{
		if(e_didInterstitialLoad!=null){
			e_didInterstitialLoad(i_name);
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
	
	public void sessionId(string sessionId)
	{
		if(e_sessionId!=null){
			if(sessionId!=null){
			e_sessionId(sessionId);
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

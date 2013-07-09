using UnityEngine;
using System.Collections;

public class NativeXiOS : MonoBehaviour {

	public int appId;
	public string publisherUserId;
	public string appName;
	public int actionId;
	public float bannerX=0f;
	public float bannerY=0f;
	public float bannerHeight = Screen.height/20f;
	public float bannerWidth = Screen.width/2f;
	public float offerWallX=0f;
	public float offerWallY=0f;
	
	public NativeXiOS(int applicationId, string applicationName, string pubName)
	{
		appId = applicationId;
		appName = applicationName;
		publisherUserId = pubName;
	}

	public NativeXiOS(int applicationId, string applicationName, string pubName, int bannerPointX, int bannerPointY, int heightOfBanner, int widthOfBanner)
	{
		appId = applicationId;
		appName = applicationName;
		publisherUserId = pubName;
		bannerX = bannerPointX;
		bannerY = bannerPointY;
		bannerHeight = heightOfBanner;
		bannerWidth = widthOfBanner;
	}

	public NativeXiOS(int applicationId, string applicationName, string pubName, int offerWallPointX, int offerWallPointY, bool offerWall)
	{
		appId = applicationId;
		appName = applicationName;
		publisherUserId = pubName;
		offerWallX = offerWallPointX;
		offerWallY = offerWallPointY;
	}

	public NativeXiOS(int applicationId, string applicationName, string pubName, int bannerPointX, int bannerPointY,
	                  int heightOfBanner, int widthOfBanner, int offerWallPointX, int offerWallPointY)
	{
		appId = applicationId;
		appName = applicationName;
		publisherUserId = pubName;
		bannerX = bannerPointX;
		bannerY = bannerPointY;
		bannerHeight = heightOfBanner;
		bannerWidth = widthOfBanner;
		offerWallX = offerWallPointX;
		offerWallY = offerWallPointY;
	}

	public override string ToString()
	{
		return "AppId:"+appId+" ApplicationName:"+appName+" PublisherUserId:"+publisherUserId+" ActionId:"+actionId;
	}
}

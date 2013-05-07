using UnityEngine;
using System.Collections;

public class NativeXiOS : MonoBehaviour {

	public int appId;
	public string publisherUserId;
	public string appName;
	public int actionId;
	public int bannerX=0;
	public int bannerY=0;
	public int offerWallX=0;
	public int offerWallY=0;
	
	public NativeXiOS(int applicationId, string applicationName, string pubName)
	{
		appId = applicationId;
		appName = applicationName;
		publisherUserId = pubName;
	}

	public NativeXiOS(int applicationId, string applicationName, string pubName, int bannerPointX, int bannerPointY)
	{
		appId = applicationId;
		appName = applicationName;
		publisherUserId = pubName;
		bannerX = bannerPointX;
		bannerY = bannerPointY;
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
	                  int offerWallPointX, int offerWallPointY)
	{
		appId = applicationId;
		appName = applicationName;
		publisherUserId = pubName;
		bannerX = bannerPointX;
		bannerY = bannerPointY;
		offerWallX = offerWallPointX;
		offerWallY = offerWallPointY;
	}
}

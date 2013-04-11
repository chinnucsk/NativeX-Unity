using UnityEngine;
using System.Collections;

public class NativeXiOS : MonoBehaviour {

	public string appId;
	public string publisherUserId;
	public string appName;
	
	public NativeXiOS(string applicationId, string applicationName, string pubName)
	{
		appId = applicationId;
		appName = applicationName;
		publisherUserId = pubName;
	}
}

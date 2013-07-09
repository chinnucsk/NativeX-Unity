using UnityEngine;
using System.Collections;

public class NativeXAndroid : MonoBehaviour {

	public int appId;
	public string packageName;
	public string displayName;
	public string publisherUserId;
	public int actionId;
	
	public NativeXAndroid(int applicationId, string packName, string disName, string pubName)
	{
		appId = applicationId;
		packageName = packName;
		displayName = disName;
		publisherUserId = pubName;
	}
	
	public string ToString()
	{
		return "appId: "+appId.ToString()+" - packageName: "+packageName+" - displayName: "+displayName+" - publisherUserId:"+publisherUserId;	
	}
}

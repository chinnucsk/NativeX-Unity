using UnityEngine;
using System.Collections;

public class ServerSelection : MonoBehaviour {

	public UIPopupList list;
	
	void OnSelectionChange()
	{
		NativeXCore.selectServer(list.selection);
	}
}

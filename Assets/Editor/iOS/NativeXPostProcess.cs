using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;
using System.Collections;

//#if UNITY_IPHONE
namespace UnityEditor.XCodeEditor
{
	public static class NativeXPostProcess {

		[PostProcessBuild]
		public static void OnPostprocessBuild( BuildTarget target, string path )
				{
						if (BuildTarget.iPhone == target) {
								// Create a new project object from build target
								XCodeEditor.XCProject project = new XCodeEditor.XCProject (path);
								//Debug.Log ("project has been created using path: "+path);
								// Find and run through all projmods files to patch the project
								//Debug.Log ("searching for projmods files in:" +Application.dataPath);
								var files = System.IO.Directory.GetFiles (Application.dataPath+"/Editor/iOS", "*.projmods", SearchOption.AllDirectories);
								foreach (var file in files) {
										Debug.Log (file.ToString());
										project.ApplyMod (file);
								}
								if (files.Length <= 0) {
										Debug.Log ("no .projmods files found");
								}

								// Finally save the xcode project
								project.Save();
						
						}
				}

	}
}
//#endif
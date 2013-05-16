using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using JsonFx;

namespace UnityEditor.XCodeEditor 
{
	public class XCMod 
	{
//		private string group;
//		private ArrayList patches;
//		private ArrayList libs;
//		private ArrayList frameworks;
//		private ArrayList headerpaths;
//		private ArrayList files;
//		private ArrayList folders;
//		private ArrayList excludes;
		private Hashtable _datastore;
		private List<XCModFile> _libs;
		
		public string name { get; private set; }
		public string path { get; private set; }
		
		public string group {
			get {
				return (string)_datastore["group"];
			}
		}
		
		public ArrayList patches {
			get {
				return (ArrayList)_datastore["patches"];
			}
		}
		
		public List<XCModFile> libs {
			get {
				if( _libs == null ) {
					_libs = new List<XCModFile>();
					foreach( string f in (IEnumerable)_datastore["libs"]) {
						_libs.Add (new XCModFile( f ) );
					}

				}
//				if( _libs == null ) {
//					_libs = new ArrayList( ((ArrayList)_datastore["libs"]).Count );
//					foreach( string fileRef in (ArrayList)_datastore["libs"] ) {
//						_libs.Add( new XCModFile( fileRef ) );
//					}
//				}
				return _libs;
			}
		}
		
		public ArrayList frameworks {
			get {
				ArrayList b = new ArrayList();
				foreach(string a in (IEnumerable)_datastore["frameworks"])
				{
					b.Add(a);
				}


				return b;
			}
		}
		
		public ArrayList headerpaths {
			get {
				ArrayList b = new ArrayList();
				foreach(string a in (IEnumerable)_datastore["headerpaths"])
				{
					b.Add(a);
				}
				
				
				return b;
			}
		}
		
		public ArrayList files {
			get {
				ArrayList b = new ArrayList();
				foreach(string a in (IEnumerable)_datastore["files"])
				{
					b.Add(a);
				}
				
				
				return b;
			}
		}
		
		public ArrayList folders {
			get {
				ArrayList b = new ArrayList();
				foreach(string a in (IEnumerable)_datastore["folders"])
				{
					b.Add(a);
				}
				
				
				return b;
			}
		}
		
		public ArrayList excludes {
			get {
				ArrayList b = new ArrayList();
				foreach(string a in (IEnumerable)_datastore["excludes"])
				{
					b.Add(a);
				}
				
				
				return b;
			}
		}
		
		public XCMod( string filename )
		{	
			FileInfo projectFileInfo = new FileInfo( filename );
			if( !projectFileInfo.Exists ) {
				Debug.LogWarning( "File does not exist." );
			}
			
			name = System.IO.Path.GetFileNameWithoutExtension( filename );
			path = System.IO.Path.GetDirectoryName( filename );
						//Debug.Log ("PATH: "+path);
			string removePath = "/Editor/iOS";
			path = path.Substring(0, path.LastIndexOf(removePath));
			string contents = projectFileInfo.OpenText().ReadToEnd();
			_datastore = JsonFx.Json.JsonReader.Deserialize<Hashtable>(contents);
			
//			group = (string)_datastore["group"];
//			patches = (ArrayList)_datastore["patches"];
//			libs = (ArrayList)_datastore["libs"];
//			frameworks = (ArrayList)_datastore["frameworks"];
//			headerpaths = (ArrayList)_datastore["headerpaths"];
//			files = (ArrayList)_datastore["files"];
//			folders = (ArrayList)_datastore["folders"];
//			excludes = (ArrayList)_datastore["excludes"];
		}
		
			
//	"group": "GameCenter",
//	"patches": [],
//	"libs": [],
//	"frameworks": ["GameKit.framework"],
//	"headerpaths": ["Editor/iOS/GameCenter/**"],					
//	"files":   ["Editor/iOS/GameCenter/GameCenterBinding.m",
//				"Editor/iOS/GameCenter/GameCenterController.h",
//				"Editor/iOS/GameCenter/GameCenterController.mm",
//				"Editor/iOS/GameCenter/GameCenterManager.h",
//				"Editor/iOS/GameCenter/GameCenterManager.m"],
//	"folders": [],	
//	"excludes": ["^.*\\.meta$", "^.*\\.mdown^", "^.*\\.pdf$"]
		
	}
	
	public class XCModFile
	{
		public string filePath { get; private set; }
		public bool isWeak { get; private set; }
		
		public XCModFile( string inputString )
		{
			isWeak = false;
			
			if( inputString.Contains( ":" ) ) {
				string[] parts = inputString.Split( ':' );
				filePath = parts[0];
				isWeak = ( parts[1].CompareTo( "weak" ) == 0 );	
			}
			else {
				filePath = inputString;
			}
		}
	}
}
#if UNITY_EDITOR
using UnityEngine;

namespace Carbon.Native
{
	/// <summary>
	/// Editor's Path
	/// </summary>
	public sealed class EditorPath : IPath
	{
		/// <summary>
		/// Internal persistent data path. Use it for save-data, output, etc..
		/// </summary>
		public string DataPath { get { return Application.persistentDataPath + UnityEditor.EditorPrefs.GetString("___ExtraSaveDataFolderLocalName", ""); } }

		/// <summary>
		/// Internal cache path. Files in this directory could be cleared by OS without notice.
		/// </summary>
		public string CachePath { get { return Application.temporaryCachePath; } }

		/// <summary>
		/// External persistent data path. Files in this directory could be accessed by other apps.
		/// </summary>
		public string ExternalDataPath { get { return Application.persistentDataPath; } }

		/// <summary>
		/// External cache path. Files in this directory could be accessed by other apps.
		/// </summary>
		public string ExternalCachePath { get { return Application.temporaryCachePath; } }
	}
}
#endif

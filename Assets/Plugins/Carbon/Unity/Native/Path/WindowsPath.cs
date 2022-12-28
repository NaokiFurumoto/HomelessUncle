#if UNITY_STANDALONE_WIN
using UnityEngine;

namespace Carbon.Native
{
	/// <summary>
	/// Path
	/// </summary>
	public sealed class WindowsPath : IPath
	{
		/// <summary>
		/// Internal persistent data path. Use it for save-data, output, etc..
		/// </summary>
		public string DataPath { get { return Application.persistentDataPath; } }

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
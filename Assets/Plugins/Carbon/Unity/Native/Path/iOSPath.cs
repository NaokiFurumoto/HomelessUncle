#if UNITY_IOS
using UnityEngine;

namespace Carbon.Native
{
	/// <summary>
	/// iOS's Path
	/// </summary>
	public sealed class IOSPath : IPath
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
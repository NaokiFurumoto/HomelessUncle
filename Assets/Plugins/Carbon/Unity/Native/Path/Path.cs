using UnityEngine;

namespace Carbon.Native
{
	/// <summary>
	/// Path Manager
	/// </summary>
	public static class Path
	{
		/*
			IOS:
				Application.dataPath            : Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/xxx.app/Data
				Application.streamingAssetsPath : Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/xxx.app/Data/Raw
				Application.persistentDataPath  : Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/Documents
				Application.temporaryCachePath  : Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/Library/Caches

			----------------
			Android:
				Application.dataPath            : /data/app/xxx.xxx.xxx.apk
				Application.streamingAssetsPath : jar:file:///data/app/xxx.xxx.xxx.apk/!/assets

				External 拒否(他APPアクセス不可) == Internal Path:
					~ 6.0.0
					Application.persistentDataPath  : /data/data/xxx.xxx.xxx/files
					Application.temporaryCachePath  : /data/data/xxx.xxx.xxx/cache
					6.0.0 ~
					Application.persistentDataPath  : /data/user/0/xxx.xxx.xxx/files
					Application.temporaryCachePath  : /data/user/0/xxx.xxx.xxx/cache

				External 許可(他APPアクセス可能. キャッシュクリアで削除されるなど.) == External Path:
					Application.persistentDataPath  : /storage/emulated/0/Android/data/xxx.xxx.xxx/files
					Application.temporaryCachePath  : /storage/emulated/0/Android/data/xxx.xxx.xxx/cache

			----------------
			Windows Editor:
				Application.dataPath            : /Assets
				Application.streamingAssetsPath : /Assets/StreamingAssets
				Application.persistentDataPath  : C:/Users/xxxx/AppData/LocalLow/CompanyName/ProductName
				Application.temporaryCachePath  : C:/Users/xxxx/AppData/Local/Temp/CompanyName/ProductName

			----------------
			Mac Editor:
				Application.dataPath            : /Assets
				Application.streamingAssetsPath : /Assets/StreamingAssets
				Application.persistentDataPath  : /Users/xxxx/Library/Caches/CompanyName/Product Name
				Application.temporaryCachePath  : /var/folders/57/6b4_9w8113x2fsmzx_yhrhvh0000gn/T/CompanyName/Product Name
		*/
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Private Field
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Object
		/// </summary>
		private static readonly IPath ms_Path = null;

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Constructor
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 静的コンストラクター
		/// </summary>
		static Path()
		{
#if UNITY_EDITOR
			ms_Path = new EditorPath();
#elif UNITY_STANDALONE_WIN
			ms_Path = new WindowsPath();
#elif UNITY_STANDALONE_OSX
			ms_Path = new MacOSPath();
#elif UNITY_ANDROID
			ms_Path = new AndroidPath();
#elif UNITY_IOS
			ms_Path = new IOSPath();
#else
			ms_Path = new EditorPath();
#endif
			DebugUtils.Log("static Path()");
		}

		/// <summary>
		/// 構築
		/// </summary>
		public static void Construct()
		{
			// do nothing
		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Public Property
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Internal persistent data path. Use it for save-data, output, etc..
		/// </summary>
		public static string DataPath { get { return ms_Path.DataPath; } }

		/// <summary>
		/// Internal cache(temporary) data path. Files in this directory could be cleared by OS without notice.
		/// </summary>
		public static string CachePath { get { return ms_Path.CachePath; } }

		/// <summary>
		/// External persistent data path. Files in this directory could be accessed by other apps.
		/// </summary>
		public static string ExternalDataPath { get { return ms_Path.ExternalDataPath; } }

		/// <summary>
		/// External cache(temporary) data path. Files in this directory could be accessed by other apps.
		/// </summary>
		public static string ExternalCachePath { get { return ms_Path.ExternalCachePath; } }

		/// <summary>
		/// Same as Application.dataPath. Files(application data/resources) are ReadOnly in this directory.
		/// </summary>
		public static string AssetsPath { get { return Application.dataPath; } }

		/// <summary>
		/// Same as Application.streamingAssetsPath.
		/// </summary>
		public static string StreamingAssetsPath { get { return Application.streamingAssetsPath; } }
	}
}
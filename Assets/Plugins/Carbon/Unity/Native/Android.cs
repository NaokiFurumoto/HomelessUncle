using System;
using UnityEngine;

namespace Carbon.Native
{
	public static class Android
	{
		#region SDK Infomation
		/// <summary>
		/// 端末 SDK バージョン
		/// </summary>
		private static int ms_DeviceSdkVersion = 0;
		/// <summary>
		/// 端末 API レベル
		/// </summary>
		public static int DeviceSdkVersion {
			get {
				if (ms_DeviceSdkVersion <= 0) {
					using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
						using (AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
							ms_DeviceSdkVersion = activity.Call<int>("GetSdkVersion");
							Debug.Log("Android Device SDK Version = " + ms_DeviceSdkVersion);
						}
					}
				}
				return ms_DeviceSdkVersion;
			}
		}

		/// <summary>
		/// 端末 OS バージョン
		/// </summary>
		private static string ms_DeviceOsVersion = string.Empty;
		/// <summary>
		/// 端末 OS バージョン
		/// </summary>
		public static string DeviceOsVersion {
			get {
				if (ms_DeviceOsVersion.IsNullOrWhiteSpace()) {
					using (var androidJavaClass = new AndroidJavaClass("android.os.Build$VERSION")) {
						ms_DeviceOsVersion = androidJavaClass.GetStatic<string>("RELEASE");
					}
				}
				return ms_DeviceOsVersion;
			}
		}
		#endregion

		#region Path
		/// <summary>
		/// 内部データパス
		/// </summary>
		private static string ms_InternalDataPath = null;
		/// <summary>
		/// 内部データパス
		/// </summary>
		public static string InternalDataPath {
			get {
				if (ms_InternalDataPath == null) {
					ms_InternalDataPath = GetPath(activity => activity.Call<AndroidJavaObject>("getFilesDir"));
				}
				return ms_InternalDataPath;
			}
		}

		/// <summary>
		/// 内部キャッシュパス
		/// </summary>
		private static string ms_InternalCachePath = null;
		/// <summary>
		/// 内部キャッシュパス
		/// </summary>
		public static string InternalCachePath {
			get {
				if (ms_InternalCachePath == null) {
					ms_InternalCachePath = GetPath(activity => activity.Call<AndroidJavaObject>("getCacheDir"));
				}
				return ms_InternalCachePath;
			}
		}

		/// <summary>
		/// 外部データパス
		/// </summary>
		private static string ms_ExternalDataPath = null;
		/// <summary>
		/// 外部データパス
		/// </summary>
		public static string ExternalDataPath {
			get {
				if (ms_ExternalDataPath == null) {
					ms_ExternalDataPath = GetPath(activity => activity.Call<AndroidJavaObject>("getExternalFilesDir"));
				}
				return ms_ExternalDataPath;
			}
		}

		/// <summary>
		/// 外部キャッシュパス
		/// </summary>
		private static string ms_ExternalCachePath = null;
		/// <summary>
		/// 外部キャッシュパス
		/// </summary>
		public static string ExternalCachePath {
			get {
				if (ms_ExternalCachePath == null) {
					ms_ExternalCachePath = GetPath(activity => activity.Call<AndroidJavaObject>("getExternalCacheDir"));
				}
				return ms_ExternalCachePath;
			}
		}

		/// <summary>
		/// パス取得
		/// </summary>
		private static string GetPath(Func<AndroidJavaObject, AndroidJavaObject> func)
		{
			string path = null;
			ProcessActivity(activity => {
				using (AndroidJavaObject file = func(activity)) {
					if (file != null) {
						// getAbsolutePathまたはgetCanonicalPathで絶対パスを取得
						path = file.Call<string>("getAbsolutePath");
					}
				}
			});
			return path;
		}
		#endregion

		/// <summary>
		/// クリップボードにコピー
		/// </summary>
		/// <param name="text"></param>
		public static void Clipboard(string text)
		{
			using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using (AndroidJavaObject activity = jc.GetStatic<AndroidJavaObject>("currentActivity"))
				{
					AndroidJavaObject clipboardManager = activity.Call<AndroidJavaObject>("getSystemService", "clipboard");
					{
						activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
							using (AndroidJavaClass clipDataClass = new AndroidJavaClass("android.content.ClipData"))
							{
								using (AndroidJavaObject clipData = clipDataClass.CallStatic<AndroidJavaObject>("newPlainText", "Copied text", text))
								{
									clipboardManager.Call("setPrimaryClip", clipData);
								}
							}
						}));
					}
				}
			}
		}

		/// <summary>
		/// Activity 実行
		/// </summary>
		private static void ProcessActivity(Action<AndroidJavaObject> func)
		{
			using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
				using (AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
					func(activity);
				}
			}
		}
	}
}
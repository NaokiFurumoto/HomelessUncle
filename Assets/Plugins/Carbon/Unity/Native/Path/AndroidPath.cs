#if UNITY_ANDROID
namespace Carbon.Native
{
	/// <summary>
	/// Android's Path
	/// </summary>
	public sealed class AndroidPath : IPath
	{
		/// <summary>
		/// Data path.
		/// </summary>
		public string DataPath { get { return Android.InternalDataPath; } }

		/// <summary>
		/// Cache path.
		/// </summary>
		public string CachePath { get { return Android.InternalCachePath; } }

		/// <summary>
		/// Data path.
		/// </summary>
		public string ExternalDataPath { get { return Android.ExternalDataPath; } }

		/// <summary>
		/// Cache path.
		/// </summary>
		public string ExternalCachePath { get { return Android.ExternalCachePath; } }
	}
}
#endif
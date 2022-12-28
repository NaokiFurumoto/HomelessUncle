namespace Carbon.Native
{
	/// <summary>
	/// Path
	/// </summary>
	public interface IPath
	{
		/// <summary>
		/// Internal persistent data path. Use it for save-data, output, etc..
		/// </summary>
		string DataPath { get; }

		/// <summary>
		/// Internal cache path. Files in this directory could be cleared by OS without notice.
		/// </summary>
		string CachePath { get; }

		/// <summary>
		/// External persistent data path. Files in this directory could be accessed by other apps.
		/// </summary>
		string ExternalDataPath { get; }

		/// <summary>
		/// External cache path. Files in this directory could be accessed by other apps.
		/// </summary>
		string ExternalCachePath { get; }
	}
}
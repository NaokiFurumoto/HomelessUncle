using System;

namespace Carbon
{
	public static class EnumUtils
	{
		/// <summary>
		/// Create a list of values for Enum.
		/// </summary>
		/// <typeparam name="T">Enum's type.</typeparam>
		/// <returns>Values of Enum.</returns>
		public static T[] CreateValueList<T>() where T : struct
		{
			return Enum.GetValues(typeof(T)) as T[];
		}

		/// <summary>
		/// Fetch the count of Enum.
		/// </summary>
		/// <typeparam name="T">Enum's type.</typeparam>
		/// <returns>Count of Enum.</returns>
		public static int FetchCount<T>() where T : struct
		{
			return Enum.GetValues(typeof(T)).Length;
		}

		/// <summary>
		/// Try to convert the string representation to Enum. Return the defaultValue if parse failed.
		/// </summary>
		/// <typeparam name="T">Enum's type.</typeparam>
		/// <param name="value">String representation.</param>
		/// <param name="ignoreCase">Ignore upper/lower case.</param>
		/// <param name="defaultValue">Default value.</param>
		/// <returns>Enum.</returns>
		public static T ParseOrDefault<T>(string value, bool ignoreCase, T defaultValue) where T : struct
		{
			return Enum.TryParse(value, ignoreCase, out T result) ? result : defaultValue;
		}

		/// <summary>
		/// Try to convert the string representation to Enum. Return the defaultValue if parse failed.
		/// </summary>
		/// <typeparam name="T">Enum's type.</typeparam>
		/// <param name="value">String representation.</param>
		/// <param name="defaultValue">Default value.</param>
		/// <returns>Enum.</returns>
		public static T ParseOrDefault<T>(string value, T defaultValue = default) where T : struct
		{
			return ParseOrDefault(value, true, defaultValue);
		}
	}
}
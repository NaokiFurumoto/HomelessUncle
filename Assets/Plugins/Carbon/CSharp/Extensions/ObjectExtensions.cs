namespace Carbon
{
	public static class ObjectExtensions
	{
		/// <summary>
		/// Cast object to type T.
		/// </summary>
		/// <typeparam name="T">Target type.</typeparam>
		/// <returns>Cast result.</returns>
		public static T As<T>(this object self) where T : class
		{
			return self as T;
		}

		/// <summary>
		/// Cast object to type T. Return null if self is null or invalid-casting.
		/// </summary>
		/// <typeparam name="T">Target type.</typeparam>
		/// <returns>Cast result.</returns>
		public static T AsOrNull<T>(this object self) where T : class
		{
			if (self == null) {
				return null;
			}

			return self as T;
		}

		/// <summary>
		/// Cast object to type T. Return defaultValue if self is null or invalid-casting.
		/// </summary>
		/// <typeparam name="T">Target type.</typeparam>
		/// <param name="defaultValue">Value for failure.</param>
		/// <returns>Cast result.</returns>
		public static T AsOrDefault<T>(this object self, T defaultValue = default(T)) where T : class
		{
			if (self == null) {
				return defaultValue;
			}

			T result = self as T;
			return ((result != null) ? result : null);
		}
	}
}

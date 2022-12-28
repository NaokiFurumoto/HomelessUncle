namespace Carbon
{
	/// <summary>
	/// Serialization Extensions
	/// </summary>
	public static class SerializationExtensions
	{
		/// <summary>
		/// Deep-copy a serializable class
		/// </summary>
		public static T SerializablyDeepCopy<T>(this T self)
		{
			return SerializationUtils.DeepCopy(self);
		}

		/// <summary>
		/// Compare serializable classes
		/// </summary>
		public static bool SerializablyEquals<T>(this T self, T other)
		{
			return SerializationUtils.Equals(self, other);
		}
	}
}

namespace Carbon
{
	public static class FloatExtensions
	{
		/// <summary>
		/// Return the float itself if it's valid. Return defaultValue, otherwise.
		/// </summary>
		public static float ValueOrDefault(this float self, float defaultValue = 0)
		{
			if (float.IsInfinity(self) || float.IsNaN(self)) {
				return defaultValue;
			}
			return self;
		}
	}
}

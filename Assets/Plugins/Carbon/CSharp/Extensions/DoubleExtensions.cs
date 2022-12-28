namespace Carbon
{
	public static class DoubleExtensions
	{
		/// <summary>
		/// Return the double itself if it's valid. Return defaultValue, otherwise.
		/// </summary>
		public static double ValueOrDefault(this double self, double defaultValue = 0)
		{
			if (double.IsInfinity(self) || double.IsNaN(self)) {
				return defaultValue;
			}
			return self;
		}
	}
}

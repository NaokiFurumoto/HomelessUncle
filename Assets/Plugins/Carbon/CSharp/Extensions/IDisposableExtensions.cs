using System;

namespace Carbon
{
	/// <summary>
	/// IDisposable Extensions
	/// </summary>
	public static class IDisposableExtensions
	{
		/// <summary>
		/// Try dispose.
		/// </summary>
		/// <param name="self">IDisposable itself.</param>
		public static bool TryDispose(this IDisposable self)
		{
			if (self == null) {
				return false;
			}

			self.Dispose();
			return true;
		}
	}
}

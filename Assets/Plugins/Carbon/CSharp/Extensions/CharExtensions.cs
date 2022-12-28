namespace Carbon
{
	/// <summary>
	/// Char Extensions
	/// </summary>
	public static class CharExtensions
	{
		/// <summary>
		/// ASCII 英数字かどうかをチェックする.
		/// </summary>
		/// <param name="self">文字</param>
		/// <returns>ASCII 英数字の場合は true を返します.</returns>
		public static bool IsAsciiAlphanumeric(this char self)
		{
			// ASCII : 0x30 - 0x39, 0x41 - 0x5A, 0x61 - 0x7A
			return (self >= 0x30 && self <= 0x39) || (self >= 0x41 && self <= 0x5A) || (self >= 0x61 && self <= 0x7A);
		}

		/// <summary>
		/// UTF-8 文字が半角かどうか.
		/// </summary>
		/// <param name="self">文字</param>
		/// <returns>半角の場合は true を返します.</returns>
		public static bool IsHalfWidth(this char self)
		{
			// UTF-8 : 0x00 - 0x7F, 0xF8F0, 0xFF61 - 0xFF9F, 0xF8F1 - 0xF8F3
			return (self >= 0x00 && self <= 0x7F) || (self == 0xF8F0) || (self >= 0xFF61 && self <= 0xFF9F) || (self >= 0xF8F1 && self <= 0xF8F3);
		}

		/// <summary>
		/// UTF-8 文字が全角かどうか.
		/// </summary>
		/// <param name="c">文字</param>
		/// <returns>全角の場合は true を返します.</returns>
		public static bool IsFullWidth(this char self)
		{
			return !IsHalfWidth(self);
		}
	}
}

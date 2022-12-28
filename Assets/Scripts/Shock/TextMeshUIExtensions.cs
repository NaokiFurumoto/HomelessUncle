﻿namespace Shock
{
	/// <summary>
	/// TextMeshUIの拡張メソッド
	/// </summary>
	public static class TextMeshUIExtensions
	{
		/// <summary>
		/// テキストを設定します.
		/// </summary>
		public static void TrySetText(this TextMeshUI self, string text)
		{
			if (self) {
				self.SetText(text);
			}
		}

		/// <summary>
		/// テキストを設定します.
		/// </summary>
		public static void TrySetText(this TextMeshUI self, long longText)
		{
			if (self) {
				self.SetText(longText);
			}
		}

		/// <summary>
		/// テキストを設定します.
		/// </summary>
		public static void TrySetText(this TextMeshUI self, float floatText)
		{
			if (self) {
				self.SetText(floatText);
			}
		}
	}
}

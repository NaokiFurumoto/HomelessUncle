using Shock;

public static class TextmeshExtension 
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

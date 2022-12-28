using Shock;

public static class TextmeshExtension 
{
  /// <summary>
		/// �e�L�X�g��ݒ肵�܂�.
		/// </summary>
		public static void TrySetText(this TextMeshUI self, string text)
		{
			if (self) {
				self.SetText(text);
			}
		}

		/// <summary>
		/// �e�L�X�g��ݒ肵�܂�.
		/// </summary>
		public static void TrySetText(this TextMeshUI self, long longText)
		{
			if (self) {
				self.SetText(longText);
			}
		}

		/// <summary>
		/// �e�L�X�g��ݒ肵�܂�.
		/// </summary>
		public static void TrySetText(this TextMeshUI self, float floatText)
		{
			if (self) {
				self.SetText(floatText);
			}
		}
}

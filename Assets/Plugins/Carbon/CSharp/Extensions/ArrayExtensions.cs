using System;

namespace Carbon
{
	/// <summary>
	/// Array Extensions
	/// </summary>
	public static class ArrayExtensions
	{
		/*
		 * データ構造の拡張メソッドを軽く作るんではありません.
		 *
		 * 1. データ構造には其々の特性と設計コンセプトがあります.
		 *    他のデータ構造の特性を利用したい場合, 該当データ構造を使ってください.
		 *    強引な真似はバグと処理負荷の源になりますので, 厳守しましょう.
		 *
		 * 2. 配列のスキャン系と検索系拡張メソッドを IReadOnlyList の拡張メソッド (IReadOnlyListExtensions) に追加してください.
		 *
		 * 3. 配列の操作系拡張メソッドを IList の拡張メソッド (IListExtensions) に追加してください.
		 */

		/// <summary>
		/// Clear.
		/// </summary>
		/// <typeparam name="T">Element's type of the array.</typeparam>
		/// <param name="self">Array itself.</param>
		public static void Clear<T>(this T[] self)
		{
			Array.Clear(self, 0, self.Length);
		}

		/// <summary>
		/// Clear with value.
		/// </summary>
		/// <typeparam name="T">Element's type of the array.</typeparam>
		/// <param name="self">Array itself.</param>
		/// <param name="value">Value to fill.</param>
		public static void Clear<T>(this T[] self, T value)
		{
			for (int i = 0, iLength = self.Length; i < iLength; i++)
			{
				self[i] = value;
			}
		}
	}
}

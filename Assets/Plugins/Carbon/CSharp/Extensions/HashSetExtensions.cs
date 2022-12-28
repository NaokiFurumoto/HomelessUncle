using System.Collections.Generic;

namespace Carbon
{
	public static class HashSetExtensions
	{
		/*
		 * データ構造の拡張メソッドを軽く作るんではありません.
		 *
		 * 1. データ構造には其々の特性と設計コンセプトがあります.
		 *    他のデータ構造の特性を利用したい場合, 該当データ構造を使ってください.
		 *    強引な真似はバグと処理負荷の源になりますので, 厳守しましょう.
		 *
		 * 2. LINQ を使えば殆どの事ができます.
		 *
		 *    using System.Linq;
		 */

		/// <summary>
		/// Add collection into the HashSet. Return added count.
		/// </summary>
		/// <typeparam name="T">Element's type of the HashSet.</typeparam>
		/// <param name="self">HashSet itself.</param>
		/// <param name="collection">Element collection to add.</param>
		/// <returns>Added count.</returns>
		public static int AddRange<T>(this HashSet<T> self, IEnumerable<T> collection)
		{
			int addedCount = 0;
			foreach (var key in collection) {
				if (self.Add(key)) {
					addedCount++;
				}
			}
			return addedCount;
		}

		/// <summary>
		/// Clear and AddRange.
		/// </summary>
		/// <typeparam name="T">Element's type of the HashSet.</typeparam>
		/// <param name="self">HashSet itself.</param>
		/// <param name="collection">Element collection to set.</param>
		public static void Set<T>(this HashSet<T> self, IEnumerable<T> collection)
		{
			self.Clear();
			self.AddRange(collection);
		}
	}
}

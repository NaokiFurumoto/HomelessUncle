using System;
using System.Collections.Generic;

namespace Carbon
{
	/// <summary>
	/// ICollection Extensions
	/// </summary>
	public static class ICollectionExtensions
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
		/// AddRange.
		/// </summary>
		public static void AddRange<T>(this ICollection<T> self, IEnumerable<T> other)
		{
			var castList = other as IReadOnlyList<T>;
			if (castList != null)
			{
				for (int i = 0; i < castList.Count; i++)
				{
					self.Add(castList[i]);
				}
				return;
			}

			foreach (var o in other)
			{
				self.Add(o);
			}
		}

		/// <summary>
		/// Clear and then Add.
		/// </summary>
		public static void Set<T>(this ICollection<T> self, T element)
		{
			self.Clear();
			self.Add(element);
		}

		/// <summary>
		/// Clear and then AddRange.
		/// </summary>
		public static void Set<T>(this ICollection<T> self, IEnumerable<T> other)
		{
			self.Clear();
			self.AddRange(other);
		}
	}
}

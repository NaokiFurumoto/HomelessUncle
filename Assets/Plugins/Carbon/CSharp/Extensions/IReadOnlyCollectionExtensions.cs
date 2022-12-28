using System.Collections.Generic;

namespace Carbon
{
	/// <summary>
	/// IReadOnlyCollection Extensions
	/// </summary>
	public static class IReadOnlyCollectionExtensions
	{
		/*
		 * データ構造の拡張メソッドを軽く作るんではありません.
		 *
		 * 1. データ構造には其々の特性と設計コンセプトがあります.
		 *    他のデータ構造の特性を利用したい場合, 該当データ構造を使ってください.
		 *    強引な真似はバグと処理負荷の源になりますので, 厳守しましょう.
		 *
		 * 2. GC 避けが目的ではなければ, LINQ を使えば殆どの事ができます.
		 *
		 *    using System.Linq;
		 */

		/// <summary>
		/// Get a value indicating whether the IReadOnlyCollection<T> itself contains any element.
		/// </summary>
		public static bool Any<T>(this IReadOnlyCollection<T> self)
		{
			return self.Count > 0;
		}

		/// <summary>
		/// Get a value indicating whether the IReadOnlyCollection<T> itself contains no element.
		/// </summary>
		public static bool NotAny<T>(this IReadOnlyCollection<T> self)
		{
			return self.Count <= 0;
		}

		/// <summary>
		/// Get a value indicating whether the IReadOnlyCollection<T> itself exists and contains any element.
		/// </summary>
		public static bool ExistsAny<T>(this IReadOnlyCollection<T> self)
		{
			return (self != null && self.Count > 0);
		}

		/// <summary>
		/// Get a value indicating whether the IReadOnlyCollection<T> itself is empty.
		/// </summary>
		public static bool IsEmpty<T>(this IReadOnlyCollection<T> self)
		{
			return self.Count <= 0;
		}

		/// <summary>
		/// Get a value indicating whether the IReadOnlyCollection<T> itself is null or empty.
		/// </summary>
		public static bool IsNullOrEmpty<T>(this IReadOnlyCollection<T> self)
		{
			return (self == null || self.Count <= 0);
		}
	}
}

using System;
using System.Collections.Generic;

namespace Carbon
{
	/// <summary>
	/// IList Extensions
	/// </summary>
	public static class IListExtensions
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


		/*
		 * 配列や List の操作系メソッド.
		 * ただし, 配列に使って行けないメソッド (要素数変化など) は List 専用拡張 ListExtensions の処に追加してください.
		 */

		/// <summary>
		/// Common rand generator.
		/// </summary>
		private static Random ms_Random = new Random((int)GameTime.Now.UnixTime);

		/// <summary>
		/// Shuffle the IList.
		/// </summary>
		/// <typeparam name="T">Element's type of the IList.</typeparam>
		/// <param name="self">IList itself.</param>
		public static void Shuffle<T>(this IList<T> self, int? randomSeed = null)
		{
			var random = randomSeed.HasValue ? new Random(randomSeed.Value) : ms_Random;

			int i = self.Count - 1;
			while (i > 0) {
				int k = random.Next(i + 1);
				T tmp = self[k];
				self[k] = self[i];
				self[i] = tmp;
				i--;
			}
		}

		/// <summary>
		/// Shuffle the sub-sequence of IList.
		/// </summary>
		/// <typeparam name="T">Element's type of the IList.</typeparam>
		/// <param name="self">IList itself.</param>
		/// <param name="startIndex">Head index of the sub-sequence.</param>
		/// <param name="length">Length of the sub-sequence.</param>
		public static void Shuffle<T>(this IList<T> self, int startIndex, int length)
		{
			int i = startIndex + length - 1;
			while (i > startIndex) {
				int k = ms_Random.Next(startIndex, i + 1);
				T tmp = self[k];
				self[k] = self[i];
				self[i] = tmp;
				i--;
			}
		}

		/// <summary>
		/// Swap elements for the IList.
		/// </summary>
		/// <typeparam name="T">Element's type of the IList.</typeparam>
		/// <param name="self">IList itself.</param>
		/// <param name="index1">Index of element-1 to swap.</param>
		/// <param name="index2">Index of element-2 to swap.</param>
		public static void Swap<T>(this IList<T> self, int index1, int index2)
		{
			if (index1 == index2) {
				return;
			}

			T tmp = self[index1];
			self[index1] = self[index2];
			self[index2] = tmp;
		}
	}
}

using System;
using System.Collections.Generic;

namespace Carbon
{
	/// <summary>
	/// IReadOnlyList Extensions
	/// </summary>
	public static class IReadOnlyListExtensions
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
		 * Array や List のスキャン系と検索系メソッド
		 */
		/// <summary>
		/// Common rand generator.
		/// </summary>
		private static Random ms_Random = new Random((int)GameTime.Now.UnixTime);

		/// <summary>
		/// Return a value that indicates whether the given index is valid for the IReadOnlyList.
		/// </summary>
		public static bool HasIndex<T>(this IReadOnlyList<T> self, int index)
		{
			return 0 <= index && index < self.Count;
		}

		/// <summary>
		/// Return a value that indicates whether the given index is invalid for the IReadOnlyList.
		/// </summary>
		public static bool NotHasIndex<T>(this IReadOnlyList<T> self, int index)
		{
			return index < 0 || self.Count <= index;
		}

		/// <summary>
		/// Return the last index of the IReadOnlyList. Return -1 if the IReadOnlyList is empty.
		/// </summary>
		public static int GetLastIndex<T>(this IReadOnlyList<T> self)
		{
			return self.Count - 1;
		}

		/// <summary>
		/// Count elements that satisfy the given predicate.
		/// </summary>
		public static int Count<T>(this IReadOnlyList<T> self, Predicate<T> predicate)
		{
			int count = 0;
			for (int i = 0; i < self.Count; ++i) {
				if (predicate(self[i])) {
					count++;
				}
			}
			return count;
		}

		/// <summary>
		/// Find the first index that satisfies the given predicate. Return -1 if not found.
		/// </summary>
		public static int FindIndex<T>(this IReadOnlyList<T> self, Predicate<T> predicate)
		{
			for (int i = 0; i < self.Count; ++i) {
				if (predicate(self[i])) {
					return i;
				}
			}
			return -1;
		}
		/// <summary>
		/// Find the first index by the given keyComparer. Return -1 if not found.
		/// </summary>
		public static int FindIndex<T, TKey>(this IReadOnlyList<T> self, Func<T, TKey, bool> keyComparer, TKey targetKey)
		{
			for (int i = 0; i < self.Count; ++i) {
				if (keyComparer(self[i], targetKey)) {
					return i;
				}
			}
			return -1;
		}


		/// <summary>
		/// Find the last index that satisfies the given predicate. Return -1 if not found.
		/// </summary>
		public static int FindLastIndex<T>(this IReadOnlyList<T> self, Predicate<T> predicate)
		{
			for (int i = self.Count - 1; i >= 0; --i) {
				if (predicate(self[i])) {
					return i;
				}
			}
			return -1;
		}
		/// <summary>
		/// Find the last index by the given keyComparer. Return -1 if not found.
		/// </summary>
		public static int FindLastIndex<T, TKey>(this IReadOnlyList<T> self, Func<T, TKey, bool> keyComparer, TKey targetKey)
		{
			for (int i = self.Count - 1; i >= 0; --i) {
				if (keyComparer(self[i], targetKey)) {
					return i;
				}
			}
			return -1;
		}

		/// <summary>
		/// Find the first index of the given element with the given IEqualityComparer. Return -1 if not found.
		/// </summary>
		public static int IndexOf<T>(this IReadOnlyList<T> self, T element, IEqualityComparer<T> comparer)
		{
			for (int i = 0; i < self.Count; ++i) {
				if (comparer.Equals(self[i], element)) {
					return i;
				}
			}
			return -1;
		}
		/// <summary>
		/// Find the first index of the given element. Return -1 if not found.
		/// </summary>
		public static int IndexOf<T>(this IReadOnlyList<T> self, T element)
		{
			return IndexOf(self, element, EqualityComparer<T>.Default);
		}

		/// <summary>
		/// Find the last index of the given element with the given IEqualityComparer. Return -1 if not found.
		/// </summary>
		public static int LastIndexOf<T>(this IReadOnlyList<T> self, T element, IEqualityComparer<T> comparer)
		{
			for (int i = self.Count - 1; i >= 0; --i) {
				if (comparer.Equals(self[i], element)) {
					return i;
				}
			}
			return -1;
		}
		/// <summary>
		/// Find the last index of the given element with the given IEqualityComparer. Return -1 if not found.
		/// </summary>
		public static int LastIndexOf<T>(this IReadOnlyList<T> self, T element)
		{
			return LastIndexOf(self, element, EqualityComparer<T>.Default);
		}

		/// <summary>
		/// Get a value indicating whether the IReadOnlyList<T> itself contains any element that satisfies the given predicate.
		/// </summary>
		public static bool Any<T>(this IReadOnlyList<T> self, Predicate<T> predicate)
		{
			return FindIndex(self, predicate) >= 0;
		}
		/// <summary>
		/// Get a value indicating whether the IReadOnlyList<T> itself contains any element found by the given key-comparer and target-key.
		/// </summary>
		public static bool Any<T, TKey>(this IReadOnlyList<T> self, Func<T, TKey, bool> keyComparer, TKey targetKey)
		{
			return FindIndex(self, keyComparer, targetKey) >= 0;
		}

		/// <summary>
		/// Inversion of 'IReadOnlyList.Any()'.
		/// </summary>
		public static bool NotAny<T>(this IReadOnlyList<T> self, Predicate<T> predicate)
		{
			return !Any(self, predicate);
		}
		/// <summary>
		/// Get a value indicating whether the IReadOnlyList<T> itself contains any element found by the given key-comparer and target-key.
		/// </summary>
		public static bool NotAny<T, TKey>(this IReadOnlyList<T> self, Func<T, TKey, bool> keyComparer, TKey targetKey)
		{
			return !Any(self, keyComparer, targetKey);
		}

		/// <summary>
		/// Get a value indicating whether the IReadOnlyList<T> itself exists and contains any element that satisfies the given predicate.
		/// </summary>
		public static bool ExistsAny<T>(this IReadOnlyList<T> self, Predicate<T> predicate)
		{
			return (self != null && Any(self, predicate));
		}
		/// <summary>
		/// Get a value indicating whether the IReadOnlyList<T> itself exists and contains any element found by the given key-comparer and target-key.
		/// </summary>
		public static bool ExistsAny<T, TKey>(this IReadOnlyList<T> self, Func<T, TKey, bool> keyComparer, TKey targetKey)
		{
			return (self != null && Any(self, keyComparer, targetKey));
		}

		/// <summary>
		/// Get a value indicating whether the IReadOnlyList<T> itself contains the given element with the given comparer.
		/// </summary>
		public static bool Contains<T>(this IReadOnlyList<T> self, T element, IEqualityComparer<T> comparer)
		{
			return (IndexOf(self, element, comparer) >= 0);
		}
		/// <summary>
		/// Get a value indicating whether the IReadOnlyList<T> itself contains the given element.
		/// </summary>
		public static bool Contains<T>(this IReadOnlyList<T> self, T element)
		{
			return (IndexOf(self, element) >= 0);
		}
		/// <summary>
		/// Inversion of 'IReadOnlyList.Contains()'.
		/// </summary>
		public static bool NotContains<T>(this IReadOnlyList<T> self, T element, IEqualityComparer<T> comparer)
		{
			return !Contains(self, element, comparer);
		}
		/// <summary>
		/// Inversion of 'IReadOnlyList.Contains()'.
		/// </summary>
		public static bool NotContains<T>(this IReadOnlyList<T> self, T element)
		{
			return !Contains(self, element);
		}

		/// <summary>
		/// Find the first element that satisfies the given predicate. Throw InvalidOperationException if not found.
		/// </summary>
		public static T First<T>(this IReadOnlyList<T> self, Predicate<T> predicate)
		{
			int index = FindIndex(self, predicate);
			if (index < 0) {
				throw new InvalidOperationException();
			}
			return self[index];
		}

		/// <summary>
		/// Get the first element. Return the given defaultValue if empty.
		/// </summary>
		public static T FirstOrDefault<T>(this IReadOnlyList<T> self, T defaultValue = default)
		{
			return self.Count > 0 ? self[0] : defaultValue;
		}
		/// <summary>
		/// Find the first element that satisfies the given predicate. Return the given defaultValue if not found.
		/// </summary>
		public static T FirstOrDefault<T>(this IReadOnlyList<T> self, Predicate<T> predicate, T defaultValue = default)
		{
			int index = FindIndex(self, predicate);
			if (index < 0) {
				return defaultValue;
			}
			return self[index];
		}
		/// <summary>
		/// Find the first element by the given key-comparer and target-key. Return defaultValue if not found.
		/// </summary>
		public static T FirstOrDefault<T, TKey>(this IReadOnlyList<T> self, Func<T, TKey, bool> keyComparer, TKey targetKey, T defaultValue = default)
		{
			int index = FindIndex(self, keyComparer, targetKey);
			if (index < 0) {
				return defaultValue;
			}
			return self[index];
		}

		/// <summary>
		/// Find the last element that satisfies the given predicate. Throw InvalidOperationException if not found.
		/// </summary>
		public static T Last<T>(this IReadOnlyList<T> self)
		{
			int index = GetLastIndex(self);
			if (index < 0) {
				throw new InvalidOperationException();
			}
			return self[index];
		}

		/// <summary>
		/// Find the last element that satisfies the given predicate. Throw InvalidOperationException if not found.
		/// </summary>
		public static T Last<T>(this IReadOnlyList<T> self, Predicate<T> predicate)
		{
			int index = FindLastIndex(self, predicate);
			if (index < 0) {
				throw new InvalidOperationException();
			}
			return self[index];
		}

		/// <summary>
		/// Get the last element. Return the given defaultValue if empty.
		/// </summary>
		public static T LastOrDefault<T>(this IReadOnlyList<T> self, T defaultValue = default)
		{
			return self.Count > 0 ? self[self.Count - 1] : defaultValue;
		}
		/// <summary>
		/// Find the last element that satisfies the given predicate. Return the given defaultValue if not found.
		/// </summary>
		public static T LastOrDefault<T>(this IReadOnlyList<T> self, Predicate<T> predicate, T defaultValue = default)
		{
			int index = FindLastIndex(self, predicate);
			if (index < 0) {
				return defaultValue;
			}
			return self[index];
		}
		/// <summary>
		/// Find the last element by the given key-comparer and target-key. Return defaultValue if not found.
		/// </summary>
		public static T LastOrDefault<T, TKey>(this IReadOnlyList<T> self, Func<T, TKey, bool> keyComparer, TKey targetKey, T defaultValue = default)
		{
			int index = FindLastIndex(self, keyComparer, targetKey);
			if (index < 0) {
				return defaultValue;
			}
			return self[index];
		}

		/// <summary>
		/// Get the element with the given index. Return the given defaultValue if not found.
		/// </summary>
		public static T ElementAtOrDefault<T>(this IReadOnlyList<T> self, int index, T defaultValue = default)
		{
			return HasIndex(self, index) ? self[index] : defaultValue;
		}

		/// <summary>
		/// Get a random element. Return the given defaultValue if empty.
		/// </summary>
		public static T ElementAtRandom<T>(this IReadOnlyList<T> self, T defaultValue = default)
		{
			var count = self.Count;
			if (count <= 0) {
				return defaultValue;
			}
			return self[ms_Random.Next(0, count)];
		}

		/// <summary>
		/// foreach
		/// </summary>
		public static void ForEach<T>(this IReadOnlyList<T> self, Action<T> action)
		{
			for (int i = 0; i < self.Count; i++) {
				action(self[i]);
			}
		}
		/// <summary>
		/// foreach
		/// </summary>
		public static void ForEach<T>(this IReadOnlyList<T> self, Action<T, int> action)
		{
			for (int i = 0; i < self.Count; i++) {
				action(self[i], i);
			}
		}
	}
}

using System;
using System.Collections.Generic;

namespace Carbon
{
	/// <summary>
	/// List Extensions
	/// </summary>
	public static class ListExtensions
	{
		/*
		 * データ構造の拡張メソッドを軽く作るんではありません.
		 *
		 * 1. データ構造には其々の特性と設計コンセプトがあります.
		 *    他のデータ構造の特性を利用したい場合, 該当データ構造を使ってください.
		 *    強引な真似はバグと処理負荷の源になりますので, 厳守しましょう.
		 *
		 * 2. List のスキャン系と検索系拡張メソッドを IReadOnlyList の拡張メソッド (IReadOnlyListExtensions) に追加してください.
		 *
		 * 3. List の操作系拡張メソッドを IList の拡張メソッド (IListExtensions) に追加してください.
		 *    ただし, 配列に使って行けないメソッド (要素数変化など) はこちらが良い.
		 */

		/// <summary>
		/// Remove the first element.
		/// </summary>
		public static void Remove<T>(this List<T> self)
		{
			if (self.Count > 0) {
				self.RemoveAt(0);
			}
		}

		/// <summary>
		/// Remove the first one which matches the given predicate.
		/// </summary>
		public static void Remove<T>(this List<T> self, Predicate<T> match)
		{
			int idx = self.FindIndex(match);
			if (idx >= 0) {
				self.RemoveAt(idx);
			}
		}

		/// <summary>
		/// Remove the first one found by the given keyComparer.
		/// </summary>
		public static void Remove<T, TKey>(this List<T> self, Func<T, TKey, bool> keyComparer, TKey targetKey)
		{
			int idx = self.FindIndex(keyComparer, targetKey);
			if (idx >= 0) {
				self.RemoveAt(idx);
			}
		}

		/// <summary>
		/// Remove the last element.
		/// </summary>
		public static void RemoveLast<T>(this List<T> self)
		{
			int idx = self.Count - 1;
			if (idx >= 0) {
				self.RemoveAt(idx);
			}
		}

		/// <summary>
		/// Remove the last one which equals to the given element.
		/// </summary>
		public static void RemoveLast<T>(this List<T> self, T element)
		{
			int idx = self.LastIndexOf(element);
			if (idx >= 0) {
				self.RemoveAt(idx);
			}
		}

		/// <summary>
		/// Remove the last one which satisfies the given predicate.
		/// </summary>
		public static void RemoveLast<T>(this List<T> self, Predicate<T> predicate)
		{
			int idx = self.FindLastIndex(predicate);
			if (idx >= 0) {
				self.RemoveAt(idx);
			}
		}

		/// <summary>
		/// Remove the last one found by the given keyComparer.
		/// </summary>
		public static void RemoveLast<T, TKey>(this List<T> self, Func<T, TKey, bool> keyComparer, TKey targetKey)
		{
			int idx = self.FindLastIndex(keyComparer, targetKey);
			if (idx >= 0) {
				self.RemoveAt(idx);
			}
		}

		/// <summary>
		/// Remove all elements found by the given key-comparer and target-key.
		/// </summary>
		public static void RemoveAll<T, TKey>(this List<T> self, Func<T, TKey, bool> keyComparer, TKey targetKey)
		{
			for (int i = self.Count - 1; i >= 0; --i) {
				if (keyComparer(self[i], targetKey)) {
					self.RemoveAt(i);
				}
			}
		}

		/// <summary>
		/// Find the first element that satisfies the given predicate. Return default(T) if not found.
		/// </summary>
		/// <remarks>List.Find() scans its internal array directly.</remarks>
		public static T FirstOrDefault<T>(this List<T> self, Predicate<T> predicate)
		{
			return self.Find(predicate);
		}

		/// <summary>
		/// Find the first element that satisfies the given predicate. Return defaultValue if not found.
		/// </summary>
		public static T FirstOrDefault<T>(this List<T> self, Predicate<T> predicate, T defaultValue)
		{
			return ((IReadOnlyList<T>)self).FirstOrDefault(predicate, defaultValue);
		}

		/// <summary>
		/// Find the last element that satisfies the given predicate. Return default(T) if not found.
		/// </summary>
		/// <remarks>List.FindLast() scans its internal array directly.</remarks>
		public static T LastOrDefault<T>(this List<T> self, Predicate<T> predicate)
		{
			return self.FindLast(predicate);
		}

		/// <summary>
		/// Find the last element that satisfies the given predicate. Return defaultValue if not found.
		/// </summary>
		public static T LastOrDefault<T>(this List<T> self, Predicate<T> predicate, T defaultValue)
		{
			return ((IReadOnlyList<T>)self).LastOrDefault(predicate, defaultValue);
		}

		#region Pop := Remove + Return
		/// <summary>
		/// Remove and return the index-th elemnt. Return defaultValue if not found.
		/// </summary>
		public static T PopAt<T>(this List<T> self, int index, T defaultValue = default(T))
		{
			if (index < 0 || index >= self.Count) {
				return defaultValue;
			}

			T ret = self[index];
			self.RemoveAt(index);
			return ret;
		}

		/// <summary>
		/// Remove and return the first element. TimeComplexity = O(n).
		/// </summary>
		public static T PopFirst<T>(this List<T> self)
		{
			return PopAt(self, 0);
		}

		/// <summary>
		/// Remove and return the first one which equals to the given element. Return defaultValue if not found.
		/// </summary>
		public static T PopFirst<T>(this List<T> self, T element, T defaultValue = default)
		{
			return PopAt(self, self.IndexOf(element), defaultValue);
		}

		/// <summary>
		/// Remove and return the first element that satisfies the given predicate. Return defaultValue if not found.
		/// </summary>
		public static T PopFirst<T>(this List<T> self, Predicate<T> predicate, T defaultValue = default)
		{
			return PopAt(self, self.FindIndex(predicate), defaultValue);
		}

		/// <summary>
		/// Remove and return the first element found by the given key-comparer and targetKey. Return defaultValue if not found.
		/// </summary>
		public static T PopFirst<T, TKey>(this List<T> self, Func<T, TKey, bool> keyComparer, TKey targetKey, T defaultValue = default)
		{
			return PopAt(self, self.FindIndex(keyComparer, targetKey), defaultValue);
		}

		/// <summary>
		/// Remove and return the last element. TimeComplexity = O(1).
		/// </summary>
		public static T PopLast<T>(this List<T> self)
		{
			return PopAt(self, self.Count - 1);
		}

		/// <summary>
		/// Remove and return the last one which equals to the given element. Return defaultValue if not found.
		/// </summary>
		public static T PopLast<T>(this List<T> self, T element, T defaultValue = default)
		{
			return PopAt(self, self.LastIndexOf(element), defaultValue);
		}

		/// <summary>
		/// Remove and return the last element that satisfies the given predicate. Return defaultValue if not found.
		/// </summary>
		public static T PopLast<T>(this List<T> self, Predicate<T> predicate, T defaultValue = default)
		{
			return PopAt(self, self.FindLastIndex(predicate), defaultValue);
		}

		/// <summary>
		/// Remove and return the last element found by the given key-comparer and targetKey. Return defaultValue if not found.
		/// </summary>
		public static T PopLast<T, TKey>(this List<T> self, Func<T, TKey, bool> keyComparer, TKey targetKey, T defaultValue = default)
		{
			return PopAt(self, self.FindLastIndex(keyComparer, targetKey), defaultValue);
		}

		/// <summary>
		/// Remove and put all elements that satisfy the given predicate into the given buffer.
		/// </summary>
		public static void PopAll<T>(this List<T> self, Predicate<T> predicate, List<T> buffer)
		{
			buffer.Clear();
			for (int i = 0; i < self.Count; ++i) {
				T element = self[i];
				if (predicate(element)) {
					buffer.Add(element);
				}
			}
			self.RemoveAll(predicate);
		}

		/// <summary>
		/// Remove and return all elements that satisfy the given predicate.
		/// </summary>
		public static List<T> PopAll<T>(this List<T> self, Predicate<T> predicate)
		{
			// avoid multiple allocation
			List<T> list = new List<T>(self.Count);
			PopAll(self, predicate, list);
			return list;
		}

		/// <summary>
		/// Remove and put all elements found by the given key-comparer and target-key into the given buffer.
		/// </summary>
		public static void PopAll<T, TKey>(this List<T> self, Func<T, TKey, bool> keyComparer, TKey targetKey, List<T> buffer)
		{
			buffer.Clear();
			for (int i = 0; i < self.Count; ++i) {
				if (keyComparer(self[i], targetKey)) {
					buffer.Add(self[i]);
				}
			}
			RemoveAll(self, keyComparer, targetKey);
		}

		/// <summary>
		/// Remove and return all elements found by the given key-comparer and target-key.
		/// </summary>
		public static List<T> PopAll<T, TKey>(this List<T> self, Func<T, TKey, bool> keyComparer, TKey targetKey)
		{
			// avoid multiple allocation
			List<T> list = new List<T>(self.Count);
			PopAll(self, keyComparer, targetKey, list);
			return list;
		}
		#endregion

		#region circular-list operation
		/// <summary>
		/// Move the head-element to tail.
		/// </summary>
		public static void RotateForward<T>(this List<T> self)
		{
			if (self.Count < 2) {
				return;
			}

			T temp = self[0];
			self.RemoveAt(0);
			self.Add(temp);
		}

		/// <summary>
		/// Move the tail-element to head.
		/// </summary>
		public static void RotateBackward<T>(this List<T> self)
		{
			if (self.Count < 2) {
				return;
			}

			int idx = self.Count - 1;
			T temp = self[idx];
			self.RemoveAt(idx);
			self.Insert(0, temp);
		}
		#endregion
	}
}

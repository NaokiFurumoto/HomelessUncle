using System;
using System.Collections.Generic;

namespace Carbon
{
	/// <summary>
	/// IDictionary Extensions
	/// </summary>
	public static class IDictionaryExtensions
	{
		/// <summary>
		/// Set the value for the given key. Adds KeyValuePair and returns true if the dictionary does not contain the given key.
		/// </summary>
		public static bool SetValue<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, TValue value)
		{
			if (self.ContainsKey(key)) {
				self[key] = value;
				return false;
			}

			self.Add(key, value);
			return true;
		}

		/// <summary>
		/// Remove given keys.
		/// </summary>
		public static int RemoveAll<TKey, TValue>(this IDictionary<TKey, TValue> self, IEnumerable<TKey> keyList)
		{
			int removeCount = 0;
			foreach (TKey key in keyList) {
				if (self.Remove(key)) {
					removeCount++;
				}
			}
			return removeCount;
		}

		/// <summary>
		/// Remove given keys.
		/// </summary>
		public static void RemoveAll<TKey, TValue>(this IDictionary<TKey, TValue> self, params TKey[] keyList)
		{
			for (int i = 0; i < keyList.Length; i++) {
				self.Remove(keyList[i]);
			}
		}

		/// <summary>
		/// Remove elements which satisfies predicate.
		/// </summary>
		public static int RemoveAll<TKey, TValue>(this IDictionary<TKey, TValue> self, Predicate<KeyValuePair<TKey, TValue>> predicate)
		{
			List<TKey> keyList = new List<TKey>(self.Count);
			foreach (KeyValuePair<TKey, TValue> pair in self) {
				if (predicate(pair)) {
					keyList.Add(pair.Key);
				}
			}
			return RemoveAll(self, keyList);
		}

		/// <summary>
		/// 'Dictionary.ContainsValue()'.
		/// </summary>
		public static bool ContainsValue<TKey, TValue>(this IDictionary<TKey, TValue> self, TValue value)
		{
			return self.Values.Contains(value);
		}

		/// <summary>
		/// Inversion of 'Dictionary.ContainsValue()'.
		/// </summary>
		public static bool NotContainsValue<TKey, TValue>(this IDictionary<TKey, TValue> self, TValue value)
		{
			return !self.Values.Contains(value);
		}
	}
}

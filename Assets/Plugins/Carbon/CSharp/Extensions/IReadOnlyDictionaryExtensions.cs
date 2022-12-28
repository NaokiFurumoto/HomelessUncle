using System;
using System.Collections.Generic;

namespace Carbon
{
	/// <summary>
	/// IReadOnlyDictionary Extensions
	/// </summary>
	public static class IReadOnlyDictionaryExtensions
	{
		/// <summary>
		/// Get the value maps from given "key". If not found, "defaultValue" will be returned.
		/// </summary>
		public static TValue GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> self, TKey key, TValue defaultValue = default(TValue))
		{
			if (self.Count <= 0) {
				return defaultValue;
			}

			return self.TryGetValue(key, out TValue value) ? value : defaultValue;
		}

		/// <summary>
		/// ForEach Key.
		/// </summary>
		public static void ForEachKey<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> self, Action<TKey> action)
		{
			self.ForEach(pair => action(pair.Key));
		}

		/// <summary>
		/// ForEach Value.
		/// </summary>
		public static void ForEachValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> self, Action<TValue> action)
		{
			self.ForEach(pair => action(pair.Value));
		}

		/// <summary>
		/// Inversion of 'IReadOnlyDictionary.ContainsKey()'.
		/// </summary>
		public static bool NotContainsKey<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> self, TKey key)
		{
			return !self.ContainsKey(key);
		}
	}
}

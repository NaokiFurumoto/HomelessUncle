using System;
using System.Collections.Generic;
using System.Linq;

namespace Carbon
{
	/// <summary>
	/// IEnumerable Extensions
	/// </summary>
	public static class IEnumerableExtensions
	{
		/*
		 * LINQ を使ってください.
		 *
		 * using System.Linq;
		 */

		#region implementations for IEnumerable<T> interface
		/// <summary>
		/// Class implements IEqualityComparer<T> to compare elements by key selected by keySelector.
		/// </summary>
		public sealed class CompareKeySelector<T, TKey> : IEqualityComparer<T>
		{
			private Func<T, TKey> keySelector;

			public CompareKeySelector(Func<T, TKey> keySelector)
			{
				this.keySelector = keySelector;
			}

			public bool Equals(T x, T y)
			{
				return keySelector(x).Equals(keySelector(y));
			}

			public int GetHashCode(T obj)
			{
				return keySelector(obj).GetHashCode();
			}
		}
		#endregion

		#region variables for implementing extensions
		/// <summary>
		/// Common rand generator.
		/// </summary>
		private static Random ms_Random = new Random((int)GameTime.Now.UnixTime);
		#endregion

		public static T ElementAtOrDefault<T>(this IEnumerable<T> self, int index, T defaultValue)
		{
			return 0 <= index && index < self.Count() ? self.ElementAt(index) : defaultValue;
		}

		/// <summary>
		/// Fetch a random element from the IEnumerable<T> itself.
		/// </summary>
		/// <typeparam name="T">Element's type of IEnumerable<T>.</typeparam>
		/// <param name="self">IEnumerable itself.</param>
		/// <returns>Fetched element.</returns>
		public static T ElementAtRandom<T>(this IEnumerable<T> self)
		{
			var count = self.Count();
			if (count <= 0) {
				return default(T);
			}
			return self.ElementAt(ms_Random.Next(0, count));
		}

		/// <summary>
		/// Inversion of 'IEnumerable.Contains()'.
		/// </summary>
		/// <typeparam name="T">Element's type of IEnumerable<T>.</typeparam>
		/// <param name="self">IEnumerable itself.</param>
		/// <param name="element">Element to check.</param>
		/// <returns>Inversion of Contains().</returns>
		public static bool NotContains<T>(this IEnumerable<T> self, T element)
		{
			return !self.Contains(element);
		}

		/// <summary>
		/// Inversion of 'IEnumerable.Any(predicate)'.
		/// </summary>
		/// <typeparam name="T">Element's type of IEnumerable<T>.</typeparam>
		/// <param name="self">IEnumerable itself.</param>
		/// <param name="predicate">Predicate function.</param>
		/// <returns>Inversion of Any(predicate).</returns>
		public static bool NotAny<T>(this IEnumerable<T> self, Func<T, bool> predicate)
		{
			return !self.Any(predicate);
		}

		/// <summary>
		/// Inversion of 'IEnumerable.Any()'.
		/// </summary>
		/// <typeparam name="T">Element's type of IEnumerable<T>.</typeparam>
		/// <param name="self">IEnumerable itself.</param>
		/// <returns>Inversion of Any().</returns>
		public static bool NotAny<T>(this IEnumerable<T> self)
		{
			return !self.Any();
		}

		/// <summary>
		/// Get a value indicating whether the IEnumerable<T> itself is empty.
		/// </summary>
		/// <typeparam name="T">Element's type of IEnumerable<T>.</typeparam>
		/// <param name="self">IEnumerable itself.</param>
		/// <returns>Return true if the IEnumerable<T> itself is empty.</returns>
		public static bool IsEmpty<T>(this IEnumerable<T> self)
		{
			return !self.Any();
		}

		/// <summary>
		/// Get a value indicating whether the IEnumerable<T> itself is null or empty.
		/// </summary>
		/// <typeparam name="T">Element's type of IEnumerable<T>.</typeparam>
		/// <param name="self">IEnumerable itself.</param>
		/// <returns>Return true if the IEnumerable<T> itself is null or empty.</returns>
		public static bool IsNullOrEmpty<T>(this IEnumerable<T> self)
		{
			return (self == null || self.IsEmpty());
		}

		/// <summary>
		/// Get a value indicating whether the IEnumerable<T> itself exists and contains any element.
		/// </summary>
		/// <typeparam name="T">Element's type of IEnumerable<T>.</typeparam>
		/// <param name="self">IEnumerable itself.</param>
		/// <returns>Return true if the IEnumerable<T> itself exists and contains any element.</returns>
		public static bool ExistsAny<T>(this IEnumerable<T> self)
		{
			return (self != null && self.Any());
		}

		/// <summary>
		/// Return a value that indicates whether this IEnumerable contains all elements in the given IEnumerable.
		/// </summary>
		/// <param name="other">IEnumerable to check.</param>
		/// <returns>True if contains all.</returns>
		public static bool ContainsAll<T>(this IEnumerable<T> self, IEnumerable<T> other)
		{
			foreach (T key in other) {
				if (self.NotContains(key)) {
					return false;
				}
			}
			return true;
		}
		/// <summary>
		/// Return a value that indicates whether this IEnumerable contains all given elements.
		/// </summary>
		/// <param name="indices">Elements to check.</param>
		/// <returns>True if contains all.</returns>
		public static bool ContainsAll<T>(this IEnumerable<T> self, params T[] elements)
		{
			foreach (T key in elements) {
				if (self.NotContains(key)) {
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Return a value that indicates whether this IEnumerable contains any element in the given IEnumerable.
		/// </summary>
		/// <param name="other">IEnumerable to check.</param>
		/// <returns>True if contains any.</returns>
		public static bool ContainsAny<T>(this IEnumerable<T> self, IEnumerable<T> other)
		{
			foreach (T key in other) {
				if (self.Contains(key)) {
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// Return a value that indicates whether this IEnumerable contains any given elements.
		/// </summary>
		/// <param name="indices">Elements to check.</param>
		/// <returns>True if contains any.</returns>
		public static bool ContainsAny<T>(this IEnumerable<T> self, params T[] elements)
		{
			foreach (T key in elements) {
				if (self.Contains(key)) {
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Return a value that indicates whether this IEnumerable contains elements in the given BitSet only.
		/// </summary>
		/// <param name="other">IEnumerable to check.</param>
		/// <returns>True if contains only.</returns>
		public static bool ContainsOnly<T>(this IEnumerable<T> self, IEnumerable<T> other)
		{
			// boost
			// this is empty -> this should not contains what other contains.
			if (self.IsEmpty()) {
				return true;
			}

			// boost
			// other is empty -> this should contains other bits.
			if (other.IsEmpty()) {
				return false;
			}

			foreach (T key in self) {
				if (other.NotContains(key)) {
					return false;
				}
			}
			return true;
		}
		/// <summary>
		/// Return a value that indicates whether this IEnumerable contains given elements only.
		/// </summary>
		/// <param name="indices">Elements to check.</param>
		/// <returns>True if contains only.</returns>
		public static bool ContainsOnly<T>(this IEnumerable<T> self, params T[] elements)
		{
			// boost
			// this is empty -> this should not contains what other contains.
			if (self.IsEmpty()) {
				return true;
			}

			// boost
			// other is empty -> this should contains other bits.
			if (elements.IsEmpty()) {
				return false;
			}

			foreach (T key in self) {
				if (elements.NotContains(key)) {
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// ForEach.
		/// </summary>
		/// <typeparam name="T">Element's type of the IEnumerable.</typeparam>
		/// <param name="self">IEnumerable itself.</param>
		/// <param name="action">Action to call.</param>
		public static void ForEach<T>(this IEnumerable<T> self, Action<T> action)
		{
			foreach (T item in self) {
				action(item);
			}
		}

		/// <summary>
		/// ForEach.
		/// </summary>
		/// <typeparam name="T">Element's type of the IEnumerable.</typeparam>
		/// <param name="self">IEnumerable itself.</param>
		/// <param name="action">Action with index to call.</param>
		public static void ForEach<T>(this IEnumerable<T> self, Action<T, int> action)
		{
			int index = 0;
			foreach (T item in self) {
				action(item, index);
				index++;
			}
		}

		/// <summary>
		/// Distinct IEnumerable by key selected by keySelector.
		/// </summary>
		/// <typeparam name="T">Element's type of the IEnumerable.</typeparam>
		/// <typeparam name="TKey">Key's type.</typeparam>
		/// <param name="self">IEnumerable itself.</param>
		/// <param name="keySelector">Function to select key.</param>
		/// <returns>Distincted IEnumerable.</returns>
		public static IEnumerable<T> Distinct<T, TKey>(this IEnumerable<T> self, Func<T, TKey> keySelector)
		{
			return self.Distinct(new CompareKeySelector<T, TKey>(keySelector));
		}

		/// <summary>
		/// Check if IEnumerable itself is SequenceEqual to the other one by key selected by keySelector.
		/// </summary>
		/// <typeparam name="T">Element's type of the IEnumerable.</typeparam>
		/// <typeparam name="TKey">Key's type.</typeparam>
		/// <param name="self">IEnumerable itself.</param>
		/// <param name="other">The other IEnumerable.</param>
		/// <param name="keySelector">Function to select key.</param>
		/// <returns>True if itself is SequenceEqual to the other.</returns>
		public static bool SequenceEqual<T, TKey>(this IEnumerable<T> self, IEnumerable<T> other, Func<T, TKey> keySelector)
		{
			return self.SequenceEqual(other, new CompareKeySelector<T, TKey>(keySelector));
		}

		/// <summary>
		/// Create Queue for given IEnumerable
		/// </summary>
		/// <typeparam name="T">Element's type of the IEnumerable.</typeparam>
		/// <param name="self">IEnumerable itself.</param>
		/// <returns>Queue.</returns>
		public static Queue<T> ToQueue<T>(this IEnumerable<T> self)
		{
			return new Queue<T>(self);
		}

		/// <summary>
		/// Create Stack for given IEnumerable
		/// </summary>
		/// <typeparam name="T">Element's type of the IEnumerable.</typeparam>
		/// <param name="self">IEnumerable itself.</param>
		/// <returns>Stack.</returns>
		public static Stack<T> ToStack<T>(this IEnumerable<T> self)
		{
			return new Stack<T>(self);
		}

		/// <summary>
		/// Create HashSet for given IEnumerable
		/// </summary>
		/// <typeparam name="T">Element's type of the IEnumerable.</typeparam>
		/// <param name="self">IEnumerable itself.</param>
		/// <returns>HashSet.</returns>
		public static HashSet<T> ToHashSet<T>(this IEnumerable<T> self)
		{
			return new HashSet<T>(self);
		}

		/// <summary>
		/// Create HashSet for given IEnumerable with comparer.
		/// </summary>
		/// <typeparam name="T">Element's type of the IEnumerable.</typeparam>
		/// <param name="self">IEnumerable itself.</param>
		/// <param name="comparer">Comparer.</param>
		/// <returns>HashSet.</returns>
		public static HashSet<T> ToHashSet<T>(this IEnumerable<T> self, IEqualityComparer<T> comparer)
		{
			return new HashSet<T>(self, comparer);
		}

		/// <summary>
		/// Create ListDictionary for given IEnumerable
		/// </summary>
		/// <typeparam name="T">Element's type of the IEnumerable.</typeparam>
		/// <param name="self">IEnumerable itself.</param>
		/// <param name="keySelector">Function to select key.</param>
		/// <returns>HashSet.</returns>
		public static ListDictionary<TKey, TValue> ToListDictionary<TKey, TValue>(this IEnumerable<TValue> self, Func<TValue, TKey> keySelector)
		{
			ListDictionary<TKey, TValue> listDictionary = new ListDictionary<TKey, TValue>();
			foreach (var value in self)
			{
				listDictionary.Add(keySelector(value), value);
			}
			listDictionary.TrimExcess();
			return listDictionary;
		}

		/// <summary>
		/// Create ListDictionary for given IEnumerable with key-comparer.
		/// </summary>
		/// <typeparam name="T">Element's type of the IEnumerable.</typeparam>
		/// <param name="self">IEnumerable itself.</param>
		/// <param name="keySelector">Function to select key.</param>
		/// <param name="keyComparer">Comparer for key.</param>
		/// <returns>HashSet.</returns>
		public static ListDictionary<TKey, TValue> ToListDictionary<TKey, TValue>(this IEnumerable<TValue> self, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> keyComparer)
		{
			ListDictionary<TKey, TValue> listDictionary = new ListDictionary<TKey, TValue>(keyComparer);
			foreach (var value in self)
			{
				listDictionary.Add(keySelector(value), value);
			}
			listDictionary.TrimExcess();
			return listDictionary;
		}

		/// <summary>
		/// Find minimum element according to given keySelector. If IEnumerable itself is empty, returns default(T).
		/// </summary>
		/// <typeparam name="T">Element's type of the IEnumerable.</typeparam>
		/// <typeparam name="TKey">Key's type.</typeparam>
		/// <param name="self">IEnumerable itself.</param>
		/// <param name="keySelector">Function to select key.</param>
		/// <returns>Found minimum element.</returns>
		public static T FindMin<T, TKey>(this IEnumerable<T> self, Func<T, TKey> keySelector)
		{
			if (self.IsEmpty()) {
				return default(T);
			}
			TKey max = self.Min(keySelector);
			return self.First(n => keySelector(n).Equals(max));
		}

		/// <summary>
		/// Find maximum element according to given keySelector. If IEnumerable itself is empty, returns default(T).
		/// </summary>
		/// <typeparam name="T">Element's type of the IEnumerable.</typeparam>
		/// <typeparam name="TKey">Key's type.</typeparam>
		/// <param name="self">IEnumerable itself.</param>
		/// <param name="keySelector">Function to select key.</param>
		/// <returns>Found maximum element.</returns>
		public static T FindMax<T, TKey>(this IEnumerable<T> self, Func<T, TKey> keySelector)
		{
			if (self.IsEmpty()) {
				return default(T);
			}
			TKey max = self.Max(keySelector);
			return self.First(n => keySelector(n).Equals(max));
		}
	}
}

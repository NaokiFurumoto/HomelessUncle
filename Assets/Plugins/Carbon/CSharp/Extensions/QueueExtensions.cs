using System.Collections.Generic;

namespace Carbon
{
	/// <summary>
	/// Queue Extensions
	/// </summary>
	public static class QueueExtensions
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
		 *    using System.Ling;
		 */

		/// <summary>
		/// Remove and get the first element of the queue. Return defaultValue if the queue is empty.
		/// </summary>
		/// <typeparam name="T">Element's type of the queue.</typeparam>
		/// <param name="self">Queue itself.</param>
		/// <param name="defaultValue">Value for failure.</param>
		/// <returns>The first element of the queue. Return defaultValue if the queue is null or empty.</returns>
		public static T DequeueOrDefault<T>(this Queue<T> self, T defaultValue = default)
		{
			if (self.Count <= 0)
			{
				return defaultValue;
			}

			return self.Dequeue();
		}

		/// <summary>
		/// Get the first element of the queue. Return defaultValue if the queue is empty.
		/// </summary>
		/// <typeparam name="T">Element's type of the queue.</typeparam>
		/// <param name="self">Queue itself.</param>
		/// <param name="defaultValue">Value for failure.</param>
		/// <returns>The first element of the queue. Return defaultValue if the queue is null or empty.</returns>
		public static T PeekOrDefault<T>(this Queue<T> self, T defaultValue = default)
		{
			if (self.Count <= 0)
			{
				return defaultValue;
			}

			return self.Peek();
		}

		/// <summary>
		/// Return the first element of the queue and then rotate the queue.
		/// </summary>
		/// <typeparam name="T">Element's type of the queue.</typeparam>
		/// <param name="self">Queue itself.</param>
		/// <returns>The first element of the queue before rotation.</returns>
		public static T Rotate<T>(this Queue<T> self)
		{
			if (self.Count <= 1)
			{
				return self.Peek();
			}

			T ret = self.Dequeue();
			self.Enqueue(ret);
			return ret;
		}

		/// <summary>
		/// Return the first element of the queue and then rotate the queue. Return defaultValue if the queue is empty.
		/// </summary>
		/// <typeparam name="T">Element's type of the queue.</typeparam>
		/// <param name="self">Queue itself.</param>
		/// <param name="defaultValue">Value for failure.</param>
		/// <returns>The first element of the queue before rotation. Return defaultValue if the queue is empty.</returns>
		public static T RotateOrDefault<T>(this Queue<T> self, T defaultValue = default)
		{
			if (self.Count <= 0)
			{
				return defaultValue;
			}

			return self.Rotate();
		}

		/// <summary>
		/// Enqueue a sequence of elements.
		/// </summary>
		/// <typeparam name="T">Element's type of the queue.</typeparam>
		/// <param name="self">Queue itself.</param>
		/// <param name="elementSequence">The sequence of elements to enqueue.</param>
		public static void EnqueueRange<T>(this Queue<T> self, IEnumerable<T> elementSequence)
		{
			foreach (T element in elementSequence)
			{
				self.Enqueue(element);
			}
		}

		/// <summary>
		/// Clear and then Enqueue.
		/// </summary>
		public static void Set<T>(this Queue<T> self, T element)
		{
			self.Clear();
			self.Enqueue(element);
		}

		/// <summary>
		/// Clear and then EnqueueRange.
		/// </summary>
		public static void Set<T>(this Queue<T> self, IEnumerable<T> elementSequence)
		{
			self.Clear();
			self.EnqueueRange(elementSequence);
		}
	}
}

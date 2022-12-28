using System.Linq;
using System.Collections.Generic;

namespace Carbon
{
	/// <summary>
	/// Stack Extensions
	/// </summary>
	public static class StackExtensions
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
		/// Remove and get the top element of the stack. Return defaultValue if the stack is empty.
		/// </summary>
		/// <typeparam name="T">Element's type of the stack.</typeparam>
		/// <param name="self">Stack itself.</param>
		/// <param name="defaultValue">Value for failure.</param>
		/// <returns>Remove and return the top element of the stack. Return defaultValue if the stack is null or empty.</returns>
		public static T PopOrDefault<T>(this Stack<T> self, T defaultValue = default(T))
		{
			if (self.Count <= 0) {
				return defaultValue;
			}

			return self.Pop();
		}

		/// <summary>
		/// Get the top element of the stack. Return defaultValue if the stack is empty.
		/// </summary>
		/// <typeparam name="T">Element's type of the stack.</typeparam>
		/// <param name="self">Stack itself.</param>
		/// <param name="defaultValue">Value for failure.</param>
		/// <returns>Return the top element of the stack. Return defaultValue if the stack is null or empty.</returns>
		public static T PeekOrDefault<T>(this Stack<T> self, T defaultValue = default(T))
		{
			if (self.Count <= 0) {
				return defaultValue;
			}

			return self.Peek();
		}

		/// <summary>
		/// Get the bottom element of the stack. Return defaultValue if the stack is empty.
		/// </summary>
		/// <typeparam name="T">Element's type of the stack.</typeparam>
		/// <param name="self">Stack itself.</param>
		/// <param name="defaultValue">Value for failure.</param>
		/// <returns>Return the bottom element of the stack. Return defaultValue if the stack is null or empty.</returns>
		public static T PeekBottomOrDefault<T>(this Stack<T> self, T defaultValue = default(T))
		{
			if (self.Count <= 0) {
				return defaultValue;
			}

			return self.Last();
		}
	}
}

using System.Collections.Generic;

namespace Carbon
{
	/// <summary>
	/// LinkedList Extensions
	/// </summary>
	public static class LinkedListExtensions
	{
		/*
		 * データ構造の拡張メソッドを軽く作るんではありません.
		 *
		 * 1. データ構造には其々の特性と設計コンセプトがあります.
		 *    他のデータ構造の特性を利用したい場合, 該当データ構造を使ってください.
		 *    強引な真似はバグと処理負荷の源になりますので, 厳守しましょう.
		 *
		 */

		#region Pop := Remove + Return

		/// <summary>
		/// Remove and return the first node. TimeComplexity = O(1).
		/// </summary>
		public static LinkedListNode<T> PopFirst<T>(this LinkedList<T> self)
		{
			if (self.Count <= 0) {
				return null;
			}

			var node = self.First;
			self.RemoveFirst();
			return node;
		}

		/// <summary>
		/// Remove and return the first node with given element. TimeComplexity = O(n).
		/// </summary>
		public static LinkedListNode<T> PopFirst<T>(this LinkedList<T> self, T element)
		{
			if (self.Count <= 0) {
				return null;
			}

			var node = self.Find(element);
			if (node != null) {
				self.Remove(node);
			}
			return node;
		}

		/// <summary>
		/// Remove and return the last node. TimeComplexity = O(1).
		/// </summary>
		public static LinkedListNode<T> PopLast<T>(this LinkedList<T> self)
		{
			if (self.Count <= 0) {
				return null;
			}

			var node = self.Last;
			self.RemoveLast();
			return node;
		}

		/// <summary>
		/// Remove and return the last node with given element. TimeComplexity = O(n).
		/// </summary>
		public static LinkedListNode<T> PopLast<T>(this LinkedList<T> self, T element)
		{
			if (self.Count <= 0) {
				return null;
			}

			var node = self.FindLast(element);
			if (node != null) {
				self.Remove(node);
			}
			return node;
		}
		#endregion
	}
}

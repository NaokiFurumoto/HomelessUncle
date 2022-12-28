using System.Collections.Generic;

namespace Carbon
{
	/// <summary>
	/// Generic Utils
	/// </summary>
	public static class GenericExtensions
	{
		/// <summary>
		/// Create an array that contains the element.
		/// </summary>
		/// <typeparam name="T">Type of element.</typeparam>
		/// <returns>An array that contains the element.</returns>
		public static T[] CreateArray<T>(this T self)
		{
			return new T[] { self };
		}

		/// <summary>
		/// Create a list that contains the element.
		/// </summary>
		/// <typeparam name="T">Type of element.</typeparam>
		/// <returns>A list that contains the element.</returns>
		public static List<T> CreateList<T>(this T self)
		{
			return new List<T>() { self };
		}

		/// <summary>
		/// Create a stack that contains the element.
		/// </summary>
		/// <typeparam name="T">Type of element.</typeparam>
		/// <returns>A stack that contains the element.</returns>
		public static Stack<T> CreateStack<T>(this T self)
		{
			return new Stack<T>(new T[] { self });
		}

		/// <summary>
		/// Create a queue that contains the element.
		/// </summary>
		/// <typeparam name="T">Type of element.</typeparam>
		/// <returns>A queue that contains the element.</returns>
		public static Queue<T> CreateQueue<T>(this T self)
		{
			return new Queue<T>(new T[] { self });
		}

		/// <summary>
		/// Create a linked-list that contains the element.
		/// </summary>
		/// <typeparam name="T">Type of element.</typeparam>
		/// <returns>A linked-list that contains the element.</returns>
		public static LinkedList<T> CreateLinkedList<T>(this T self)
		{
			LinkedList<T> list = new LinkedList<T>();
			list.AddFirst(new LinkedListNode<T>(self));
			return list;
		}
	}
}

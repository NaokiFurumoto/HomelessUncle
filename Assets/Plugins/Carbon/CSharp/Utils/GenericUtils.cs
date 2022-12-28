using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Carbon
{
	/// <summary>
	/// Generic Utils
	/// </summary>
	public static class GenericUtils
	{
		#region Create Collection
		/// <summary>
		/// Create an array that contains the element.
		/// </summary>
		/// <typeparam name="T">Type of element.</typeparam>
		/// <param name="element">Element.</param>
		/// <returns>An array that contains the element.</returns>
		public static T[] CreateArray<T>(params T[] elements)
		{
			return elements;
		}

		/// <summary>
		/// Create a list that contains the element.
		/// </summary>
		/// <typeparam name="T">Type of element.</typeparam>
		/// <param name="element">Element.</param>
		/// <returns>A list that contains the element.</returns>
		public static List<T> CreateList<T>(params T[] elements)
		{
			return new List<T>(elements);
		}

		/// <summary>
		/// Create a stack that contains the element.
		/// </summary>
		/// <typeparam name="T">Type of element.</typeparam>
		/// <param name="element">Element.</param>
		/// <returns>A stack that contains the element.</returns>
		public static Stack<T> CreateStack<T>(params T[] elements)
		{
			return new Stack<T>(elements);
		}

		/// <summary>
		/// Create a queue that contains the element.
		/// </summary>
		/// <typeparam name="T">Type of element.</typeparam>
		/// <param name="element">Element.</param>
		/// <returns>A queue that contains the element.</returns>
		public static Queue<T> CreateQueue<T>(params T[] elements)
		{
			return new Queue<T>(elements);
		}

		/// <summary>
		/// Create a linked-list that contains the element.
		/// </summary>
		/// <typeparam name="T">Type of element.</typeparam>
		/// <param name="element">Element.</param>
		/// <returns>A linked-list that contains the element.</returns>
		public static LinkedList<T> CreateLinkedList<T>(params T[] elements)
		{
			return new LinkedList<T>(elements);
		}
		#endregion

		/// <summary>
		/// Get a string of information for public fields.
		/// </summary>
		/// <typeparam name="T">Type of object.</typeparam>
		/// <param name="obj">Object.</param>
		/// <returns>A string of information for public fields.</returns>
		public static string ToStringFields<T>(T obj)
		{
			string[] strings = obj
				.GetType()
				.GetFields(BindingFlags.Instance | BindingFlags.Public)
				.Select(n => string.Format("{0}:{1}", n.Name, n.GetValue(obj)))
				.ToArray();

			return string.Join(",", strings);
		}

		/// <summary>
		/// Get a string of information for public properties.
		/// </summary>
		/// <typeparam name="T">Type of object.</typeparam>
		/// <param name="obj">Object.</param>
		/// <returns>A string of information for public properties.</returns>
		public static string ToStringProperties<T>(T obj)
		{
			string[] strings = obj
				.GetType()
				.GetProperties(BindingFlags.Instance | BindingFlags.Public)
				.Where(n => n.CanRead)
				.Select(n => string.Format("{0}:{1}", n.Name, n.GetValue(obj, null)))
				.ToArray();

			return string.Join(",", strings);
		}
	}
}

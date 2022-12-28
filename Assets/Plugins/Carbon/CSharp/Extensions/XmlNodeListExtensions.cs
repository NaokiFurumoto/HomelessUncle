using System;
using System.Collections.Generic;
using System.Xml;

namespace Carbon
{
	/// <summary>
	/// List Extensions
	/// </summary>
	public static class XmlNodeListExtensions
	{
		/// <summary>
		/// Get a value indicating whether the XmlNodeList itself is null or empty.
		/// </summary>
		/// <param name="self">XmlNodeList itself.</param>
		/// <returns>Return true if the XmlNodeList itself is null or empty.</returns>
		public static bool IsNullOrEmpty(this XmlNodeList self)
		{
			return (self == null || self.Count <= 0);
		}

		/// <summary>
		/// Peek nodes which fit predicate.
		/// </summary>
		/// <param name="predicate">Predicate function delegate.</param>
		/// <returns>IEnumerable<XmlNode>.</returns>
		public static IEnumerable<XmlNode> Where(this XmlNodeList self, Func<XmlNode, bool> predicate)
		{
			int i = 0;
			int iCount = self.Count;
			for (i = 0; i < iCount; ++i) {
				XmlNode node = self[i];
				if (predicate(node)) {
					yield return node;
				}
			}
		}

		/// <summary>
		/// Peek nodes which fit predicate.
		/// </summary>
		/// <param name="predicate">Predicate function delegate.</param>
		/// <returns>IEnumerable<XmlNode>.</returns>
		public static IEnumerable<XmlNode> Where(this XmlNodeList self, Func<XmlNode, int, bool> predicate)
		{
			int i = 0;
			int iCount = self.Count;
			for (i = 0; i < iCount; ++i) {
				XmlNode node = self[i];
				if (predicate(node, i)) {
					yield return node;
				}
			}
		}

		/// <summary>
		/// Find the first node which fits the predicate in XmlNodeList itself. Return defaultValue if such node is not found.
		/// </summary>
		/// <param name="predicate">Predicate function delegate.</param>
		/// <returns>XmlNode</returns>
		public static XmlNode FirstOrDefault(this XmlNodeList self, Func<XmlNode, bool> predicate, XmlNode defaultValue = null)
		{
			foreach (XmlNode n in self) {
				if (predicate(n)) {
					return n;
				}
			}
			return defaultValue;
		}
	}
}

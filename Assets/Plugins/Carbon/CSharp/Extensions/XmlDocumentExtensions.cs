using System.Xml;

namespace Carbon
{
	/// <summary>
	/// XmlDocumentExtensions Extensions
	/// </summary>
	public static class XmlDocumentExtensions
	{
		/// <summary>
		/// Get the first element with given tagName. Return null if not found.
		/// </summary>
		/// <param name="tagName">TagName.</param>
		/// <returns>XmlNode</returns>
		public static XmlNode FirstElementWithTagName(this XmlDocument self, string tagName)
		{
			XmlNodeList list = self.GetElementsByTagName(tagName);
			if (list.IsNullOrEmpty()) {
				return null;
			}
			return list[0];
		}
	}
}

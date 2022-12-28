using System.Xml;

namespace Carbon
{
	/// <summary>
	/// XmlNode Extensions
	/// </summary>
	public static class XmlNodeExtensions
	{
		/// <summary>
		/// Get the value with given attribute-name. Return string.Empty if not found.
		/// </summary>
		/// <param name="attributeName">Attribute's name.</param>
		/// <returns>Return the value with given attribute-name. Return string.Empty if not found.</returns>
		public static string GetAttributeValueOrDefault(this XmlNode self, string attributeName)
		{
			if (self == null) {
				return string.Empty;
			}

			XmlAttribute attribute = self.Attributes[attributeName];
			return attribute != null ? attribute.Value : string.Empty;
		}
	}
}

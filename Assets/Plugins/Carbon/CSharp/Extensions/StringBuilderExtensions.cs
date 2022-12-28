using System;
using System.Text;

namespace Carbon
{
	/// <summary>
	/// StringBuilder Extensions
	/// </summary>
	public static class StringBuilderExtensions
	{
		/// <summary>
		/// Clear the content of the StringBuilder.
		/// </summary>
		/// <param name="self">StringBuilder itself.</param>
		public static void Clear(this StringBuilder self)
		{
			if (self.Length > 0) {
				self.Remove(0, self.Length);
			}
		}

		/// <summary>
		/// AppendLine with format string and return self.
		/// </summary>
		/// <param name="self">StringBuilder itself.</param>
		/// <param name="format">Fromat string.</param>
		/// <param name="arg0">Argument 0.</param>
		/// <returns>StringBuilder itself.</returns>
		public static StringBuilder AppendLineFormat(this StringBuilder self, string format, object arg0)
		{
			return self.AppendFormat(format, arg0).AppendLine();
		}

		/// <summary>
		/// AppendLine with format string and return self.
		/// </summary>
		/// <param name="self">StringBuilder itself.</param>
		/// <param name="format">Fromat string.</param>
		/// <param name="arg0">Argument 0.</param>
		/// <param name="arg1">Argument 1.</param>
		/// <returns>StringBuilder itself.</returns>
		public static StringBuilder AppendLineFormat(this StringBuilder self, string format, object arg0, object arg1)
		{
			return self.AppendFormat(format, arg0, arg1).AppendLine();
		}

		/// <summary>
		/// AppendLine with format string and return self.
		/// </summary>
		/// <param name="self">StringBuilder itself.</param>
		/// <param name="format">Fromat string.</param>
		/// <param name="arg0">Argument 0.</param>
		/// <param name="arg1">Argument 1.</param>
		/// <param name="arg2">Argument 2.</param>
		/// <returns>StringBuilder itself.</returns>
		public static StringBuilder AppendLineFormat(this StringBuilder self, string format, object arg0, object arg1, object arg2)
		{
			return self.AppendFormat(format, arg0, arg1, arg2).AppendLine();
		}

		/// <summary>
		/// AppendLine with format string and return self. Note that array-allocation occurs if given arguments are not in one array.
		/// </summary>
		/// <param name="self">StringBuilder itself.</param>
		/// <param name="format">Fromat string.</param>
		/// <param name="args">Arguments array. Note that array-allocation occurs if given arguments are not in one array.</param>
		/// <returns>StringBuilder itself.</returns>
		public static StringBuilder AppendLineFormat(this StringBuilder self, string format, params object[] args)
		{
			return self.AppendFormat(format, args).AppendLine();
		}

		/// <summary>
		/// AppendLine with format string and return self. Note that array-allocation occurs if given arguments are not in one array.
		/// </summary>
		/// <param name="self">StringBuilder itself.</param>
		/// <param name="provider">Interface of the format's provider.</param>
		/// <param name="format">Fromat string.</param>
		/// <param name="args">Arguments array.</param>
		/// <returns>StringBuilder itself. Note that array-allocation occurs if given arguments are not in one array.</returns>
		public static StringBuilder AppendLineFormat(this StringBuilder self, IFormatProvider provider, string format, params object[] args)
		{
			return self.AppendFormat(provider, format, args).AppendLine();
		}

		/// <summary>
		/// Append the XML-Document "summary" and return self.
		/// </summary>
		/// <param name="self">StringBuilder itself.</param>
		/// <param name="texts">Document texts.</param>
		/// <returns>StringBuilder itself.</returns>
		public static StringBuilder AppendXmlDocumentSummary(this StringBuilder self, params string[] texts)
		{
			self.AppendLine("/// <summary>");

			foreach (string text in texts) {
				self.AppendLineFormat("/// {0}", text);
			}

			self.AppendLine("/// </summary>");
			return self;
		}
	}
}

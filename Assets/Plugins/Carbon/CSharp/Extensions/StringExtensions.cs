using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Carbon
{
	/// <summary>
	/// String Extensions
	/// </summary>
	public static class StringExtensions
	{
		#region Check
		/// <summary>
		/// Get a value indicating whether the string itself is null or empty.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>True if the string itself is null or empty.</returns>
		public static bool IsNullOrEmpty(this string self)
		{
			return string.IsNullOrEmpty(self);
		}

		/// <summary>
		/// Get a value indicating whether the string itself is null or contains whitespace only.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>True if the string itself is null or contains whitespace only.</returns>
		public static bool IsNullOrWhiteSpace(this string self)
		{
			return (self == null || string.IsNullOrEmpty(self.Trim()));
		}

		/// <summary>
		/// Get a value indicating whether the string itself contains the other string.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <param name="other">The other string.</param>
		/// <returns>True if the string itself contains the other string.</returns>
		public static bool NotContains(this string self, string other)
		{
			return !self.Contains(other);
		}
		#endregion

		#region AsFormat
		/// <summary>
		/// Use itself to create a format string with a given argument.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <param name="arg0">Argument 0.</param>
		/// <returns>A format string with the given argument.</returns>
		public static string AsFormat(this string self, object arg0)
		{
			return string.Format(self, arg0);
		}

		/// <summary>
		/// Use itself to create a format string with given arguments.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <param name="arg0">Argument 0.</param>
		/// <param name="arg1">Argument 1.</param>
		/// <returns>A format string with the given arguments.</returns>
		public static string AsFormat(this string self, object arg0, object arg1)
		{
			return string.Format(self, arg0, arg1);
		}

		/// <summary>
		/// Use itself to create a format string with given arguments.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <param name="arg0">Argument 0.</param>
		/// <param name="arg1">Argument 1.</param>
		/// <param name="arg2">Argument 2.</param>
		/// <returns>A format string with the given arguments.</returns>
		public static string AsFormat(this string self, object arg0, object arg1, object arg2)
		{
			return string.Format(self, arg0, arg1, arg2);
		}

		/// <summary>
		/// Use itself to create a format string with a given arguments array. Note that array-allocation occurs if given arguments are not in one array.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <param name="args">Arguments array.</param>
		/// <returns>A format string with the given arguments array. Note that array-allocation occurs if given arguments are not in one array.</returns>
		public static string AsFormat(this string self, params object[] args)
		{
			return string.Format(self, args);
		}

		/// <summary>
		/// Use itself to create a format string with a given arguments array. Note that array-allocation occurs if given arguments are not in one array.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <param name="provider">Interface of the format's provider.</param>
		/// <param name="args">Arguments array.</param>
		/// <returns>A format string with the given arguments array. Note that array-allocation occurs if given arguments are not in one array.</returns>
		public static string AsFormat(this string self, IFormatProvider provider, params object[] args)
		{
			return string.Format(self, provider, args);
		}
		#endregion

		#region Concat
		/// <summary>
		/// Concatenate collection using given separator to form a string.
		/// </summary>
		/// <typeparam name="T">Type of collection's element.</typeparam>
		/// <param name="self">Collection it self.</param>
		/// <param name="separator">Separator.</param>
		/// <returns>The concatenated string.</returns>
		public static string ConcatWith<T>(this IEnumerable<T> self, string separator)
		{
			return string.Join(separator, self.Select(n => n.ToString()).ToArray());
		}

		/// <summary>
		/// Concatenate collection using "\n" to form a string.
		/// </summary>
		/// <typeparam name="T">Type of collection's element.</typeparam>
		/// <param name="self">Collection it self.</param>
		/// <returns>The concatenated string.</returns>
		public static string ConcatWithNewLine<T>(this IEnumerable<T> self)
		{
			return string.Join("\n", self.Select(n => n.ToString()).ToArray());
		}
		#endregion

		#region Regular-Expression
		/// <summary>
		/// Get a value indicating whether there exists a match for given regexPattern.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <param name="regexPattern">Regular-expression pattern to match.</param>
		/// <returns>True if there exists a match for given regexPattern.</returns>
		public static bool ExistsMatch(this string self, string regexPattern)
		{
			return Regex.IsMatch(self, regexPattern);
		}

		/// <summary>
		/// Get a "Match" for given regexPattern found in the string.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <param name="regexPattern">Regular-expression pattern to match.</param>
		/// <returns>A "Match" for given regexPattern found in the string.</returns>
		public static Match FindMatch(this string self, string regexPattern)
		{
			return Regex.Match(self, regexPattern);
		}

		/// <summary>
		/// Get a collection of all Matches(MatchCollection), for given regexPattern found in the string.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <param name="regexPattern">Regular-expression pattern to match.</param>
		/// <returns>A collection of all Matches(MatchCollection), for given regexPattern found in the string.</returns>
		public static MatchCollection FindMatches(this string self, string regexPattern)
		{
			return Regex.Matches(self, regexPattern);
		}

		/// <summary>
		/// Get a string which adapts regular-expression replacement.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <param name="regexPattern">Regular-expression pattern.</param>
		/// <param name="replacement">String to replace with.</param>
		/// <returns>A string which adapts given regular-expression replacement.</returns>
		public static string RegexReplace(this string self, string regexPattern, string replacement)
		{
			return Regex.Replace(self, regexPattern, replacement);
		}

		/// <summary>
		/// Convert \, *, +, ?, |, {, [, (, ), ^, $, ., #, [space] to escape character.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>\\, \*, \+, \?, \|, \{, \[, \(, \), \^, \$, \., \#, \[space]</returns>
		public static string Escape(this string self)
		{
			return Regex.Escape(self);
		}

		/// <summary>
		/// Convert escape character \\, \*, \+, \?, \|, \{, \[, \(, \), \^, \$, \., \#, \[space] to standard character.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>\, *, +, ?, |, {, [, (, ), ^, $, ., #, [space]</returns>
		public static string Unescape(this string self)
		{
			return Regex.Unescape(self);
		}
		#endregion

		#region Substring
		/// <summary>
		/// Count the number of the given substring.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <param name="substring">Substring to count.</param>
		/// <returns>Number of the given substring.</returns>
		public static int CountSubstring(this string self, string substring)
		{
			return (self.Length - self.Replace(substring, "").Length) / substring.Length;
		}

		/// <summary>
		/// Count the number of new-line character.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Number of new-line character in the string.</returns>
		public static int CountNewLine(this string self)
		{
			return CountSubstring(self, "\n");
		}

		/// <summary>
		/// Get a substring which starts from the head of string with given length.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <param name="length">Length of substring.</param>
		/// <returns>Substring which starts from the head of string with given length.</returns>
		public static string GetHeadSubstring(this string self, int length)
		{
			length = Math.Min(length, self.Length);

			return self.Substring(0, length);
		}

		/// <summary>
		/// Get a substring which ends at the end of string with given length.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <param name="length">Length of substring.</param>
		/// <returns>Substring which ends at the end of string with given length.</returns>
		public static string GetTailSubstring(this string self, int length)
		{
			length = Math.Min(length, self.Length);

			return self.Substring(self.Length - length, length);
		}

		/// <summary>
		/// Get a string which removes given substring.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <param name="substring">Substring to remove.</param>
		/// <returns>A string which removes given substring from String itself.</returns>
		public static string RemoveSubstring(this string self, string substring)
		{
			return self.Replace(substring, "");
		}

		/// <summary>
		/// Get a substring which removes newline symbols("\n" and "\r").
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>A substring which removes newline symbols("\n" and "\r").</returns>
		public static string RemoveNewLine(this string self)
		{
			return Regex.Replace(self, @"[\n|\r]", "");
		}

		/// <summary>
		/// Get a substring which removes newline symbols("\n" and "\r") on tail.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>A substring which removes newline symbols("\n" and "\r") on tail.</returns>
		public static string RemoveTailNewLine(this string self)
		{
			return self.TrimEnd('\n', '\r');
		}

		/// <summary>
		/// Get a substring which removes space symbols("\s").
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>A substring which removes space symbols("\s").</returns>
		public static string RemoveSpace(this string self)
		{
			return Regex.Replace(self, @" ", "");
		}

		/// <summary>
		/// Get a substring which removes tab symbols("\t").
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>A substring which removes tab symbols("\t").</returns>
		public static string RemoveTab(this string self)
		{
			return Regex.Replace(self, @"\t", "");
		}

		/// <summary>
		/// Get a substring which removes symbols("\s" and "\t").
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>A substring which removes symbols("\s" and "\t").</returns>
		public static string RemoveSpaceAndTab(this string self)
		{
			return Regex.Replace(self, @"[ |\t]", "");
		}

		/// <summary>
		/// Get a substring witch removes parameters.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>A substring which removes parameters.</returns>
		public static string RemoveParameter(this string self)
		{
			return Regex.Replace(self, @"{[0-9]*}", "");
		}

		/// <summary>
		/// Get a substring which removes surrogate characters.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>A substring which removes surrogate characters.</returns>
		public static string RemoveSurrogate(this string self)
		{
			StringBuilder sb = new StringBuilder();

			for (int i = 0, iCount = self.Length; i < iCount; i++) {
				if (char.IsSurrogate(self[i])) {
					continue;
				}
				sb.Append(self[i]);
			}

			return sb.ToString();
		}

		/// <summary>
		/// Split the string into substrings by given separator.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <param name="separator">Separator char.</param>
		/// <param name="options">Split option.</param>
		/// <returns>An array of substrings.</returns>
		public static string[] Split(this string self, char separator, StringSplitOptions options = StringSplitOptions.None)
		{
			return self.Split(separator.CreateArray(), options);
		}

		/// <summary>
		/// Split the string into substrings by given separator.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <param name="separator">Separator string.</param>
		/// <param name="options">Split option.</param>
		/// <returns>An array of substrings.</returns>
		public static string[] Split(this string self, string separator, StringSplitOptions options = StringSplitOptions.None)
		{
			return self.Split(separator.CreateArray(), options);
		}

		/// <summary>
		/// Split the string into substrings by newline symbols("\n" and "\r").
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>An array of substrings.</returns>
		public static string[] SplitByNewLine(this string self, StringSplitOptions options = StringSplitOptions.None)
		{
			return self.Split(new char[] { '\n', '\r' }, options);
		}
		#endregion

		#region Convert : Cases
		/// <summary>
		/// Convert the string to title-case.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>The result string in title-case.</returns>
		public static string ToTitleCase(this string self)
		{
			return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(self);
		}

		/// <summary>
		/// Convert the snake-string to upper-camel-string.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>An upper-camel-string.</returns>
		public static string FromSnakeToUpperCamel(this string self)
		{
			StringBuilder sb = new StringBuilder();

			string[] substrings = self.Split('_', StringSplitOptions.RemoveEmptyEntries);
			foreach (string str in substrings) {
				sb.Append(char.ToUpperInvariant(str[0]));
				int strLength = str.Length;
				if (strLength > 1) {
					sb.Append(str, 1, strLength - 1);
				}
			}

			return sb.ToString();
		}

		/// <summary>
		/// Convert the snake-string to lower-camel-string.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>A lower-camel-string.</returns>
		public static string FromSnakeToLowerCamel(this string self)
		{
			StringBuilder sb = new StringBuilder();

			string[] substrings = self.Split('_', StringSplitOptions.RemoveEmptyEntries);
			foreach (string str in substrings) {
				sb.Append(char.ToUpperInvariant(str[0]));
				int strLength = str.Length;
				if (strLength > 1) {
					sb.Append(str, 1, strLength - 1);
				}
			}

			sb.Replace(sb[0], char.ToLowerInvariant(sb[0]), 0, 1);

			return sb.ToString();
		}
		#endregion

		#region Convert : Types
		/// <summary>
		/// Get a value indicating whether the string representation can be converted to Boolean.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return true if the string representation can be converted to Boolean.</returns>
		public static bool IsBoolean(this string self)
		{
			bool result;
			return bool.TryParse(self, out result);
		}
		/// <summary>
		/// Convert the string representation to Boolean. Throw exception for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in Boolean.</returns>
		public static bool ToBoolean(this string self)
		{
			return bool.Parse(self);
		}
		/// <summary>
		/// Convert the string representation to Boolean. Return null for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in Boolean?.</returns>
		public static bool? ToBooleanOrNull(this string self)
		{
			bool result;
			return bool.TryParse(self, out result) ? (bool?)result : null;
		}
		/// <summary>
		/// Convert the string representation to Boolean. Return the given defaultValue for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in Boolean.</returns>
		public static bool ToBooleanOrDefault(this string self, bool defaultValue = default(bool))
		{
			bool result;
			return bool.TryParse(self, out result) ? result : defaultValue;
		}

		/// <summary>
		/// Get a value indicating whether the string representation can be converted to SByte.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return true if the string representation can be converted to SByte.</returns>
		public static bool IsSByte(this string self)
		{
			sbyte result;
			return sbyte.TryParse(self, out result);
		}
		/// <summary>
		/// Convert the string representation to SByte. Throw exception for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in SByte.</returns>
		public static sbyte ToSByte(this string self)
		{
			return sbyte.Parse(self);
		}
		/// <summary>
		/// Convert the string representation to SByte. Return null for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in SByte?.</returns>
		public static sbyte? ToSByteOrNull(this string self)
		{
			sbyte result;
			return sbyte.TryParse(self, out result) ? (sbyte?)result : null;
		}
		/// <summary>
		/// Convert the string representation to SByte. Return the given defaultValue for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in SByte.</returns>
		public static sbyte ToSByteOrDefault(this string self, sbyte defaultValue = default(sbyte))
		{
			sbyte result;
			return sbyte.TryParse(self, out result) ? result : defaultValue;
		}

		/// <summary>
		/// Get a value indicating whether the string representation can be converted to Byte.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return true if the string representation can be converted to Byte.</returns>
		public static bool IsByte(this string self)
		{
			byte result;
			return byte.TryParse(self, out result);
		}
		/// <summary>
		/// Convert the string representation to Byte. Throw exception for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in Byte.</returns>
		public static byte ToByte(this string self)
		{
			return byte.Parse(self);
		}
		/// <summary>
		/// Convert the string representation to Byte. Return null for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in Byte?.</returns>
		public static byte? ToByteOrNull(this string self)
		{
			byte result;
			return byte.TryParse(self, out result) ? (byte?)result : null;
		}
		/// <summary>
		/// Convert the string representation to Byte. Return the given defaultValue for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in Byte.</returns>
		public static byte ToByteOrDefault(this string self, byte defaultValue = default(byte))
		{
			byte result;
			return byte.TryParse(self, out result) ? result : defaultValue;
		}

		/// <summary>
		/// Get a value indicating whether the string representation can be converted to Char.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return true if the string representation can be converted to Char.</returns>
		public static bool IsChar(this string self)
		{
			char result;
			return char.TryParse(self, out result);
		}
		/// <summary>
		/// Convert the string representation to Char. Throw exception for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in Char.</returns>
		public static char ToChar(this string self)
		{
			return char.Parse(self);
		}
		/// <summary>
		/// Convert the string representation to Char. Return null for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in Char?.</returns>
		public static char? ToCharOrNull(this string self)
		{
			char result;
			return char.TryParse(self, out result) ? (char?)result : null;
		}
		/// <summary>
		/// Convert the string representation to Char. Return the given defaultValue for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in Char.</returns>
		public static char ToCharOrDefault(this string self, char defaultValue = default(char))
		{
			char result;
			return char.TryParse(self, out result) ? result : defaultValue;
		}

		/// <summary>
		/// Get a value indicating whether the string representation can be converted to Short.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return true if the string representation can be converted to Short.</returns>
		public static bool IsShort(this string self)
		{
			short result;
			return short.TryParse(self, out result);
		}
		/// <summary>
		/// Convert the string representation to Short. Throw exception for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in Short.</returns>
		public static short ToShort(this string self)
		{
			return short.Parse(self);
		}
		/// <summary>
		/// Convert the string representation to Short. Return null for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in Short?.</returns>
		public static short? ToShortOrNull(this string self)
		{
			short result;
			return short.TryParse(self, out result) ? (short?)result : null;
		}
		/// <summary>
		/// Convert the string representation to Short. Return the given defaultValue for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in Short.</returns>
		public static short ToShortOrDefault(this string self, short defaultValue = default(short))
		{
			short result;
			return short.TryParse(self, out result) ? result : defaultValue;
		}

		/// <summary>
		/// Get a value indicating whether the string representation can be converted to UShort.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return true if the string representation can be converted to UShort.</returns>
		public static bool IsUShort(this string self)
		{
			ushort result;
			return ushort.TryParse(self, out result);
		}
		/// <summary>
		/// Convert the string representation to UShort. Throw exception for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in UShort.</returns>
		public static ushort ToUShort(this string self)
		{
			return ushort.Parse(self);
		}
		/// <summary>
		/// Convert the string representation to UShort. Return null for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in UShort?.</returns>
		public static ushort? ToUShortOrNull(this string self)
		{
			ushort result;
			return ushort.TryParse(self, out result) ? (ushort?)result : null;
		}
		/// <summary>
		/// Convert the string representation to UShort. Return the given defaultValue for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in UShort.</returns>
		public static ushort ToUShortOrDefault(this string self, ushort defaultValue = default(ushort))
		{
			ushort result;
			return ushort.TryParse(self, out result) ? result : defaultValue;
		}

		/// <summary>
		/// Get a value indicating whether the string representation can be converted to Int.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return true if the string representation can be converted to Int.</returns>
		public static bool IsInt(this string self)
		{
			int result;
			return int.TryParse(self, out result);
		}
		/// <summary>
		/// Convert the string representation to Int. Throw exception for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in Int.</returns>
		public static int ToInt(this string self)
		{
			return int.Parse(self);
		}
		/// <summary>
		/// Convert the string representation to Int. Return null for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in Int?.</returns>
		public static int? ToIntOrNull(this string self)
		{
			int result;
			return int.TryParse(self, out result) ? (int?)result : null;
		}
		/// <summary>
		/// Convert the string representation to Int. Return the given defaultValue for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in Int.</returns>
		public static int ToIntOrDefault(this string self, int defaultValue = default(int))
		{
			int result;
			return int.TryParse(self, out result) ? result : defaultValue;
		}

		/// <summary>
		/// Get a value indicating whether the string representation can be converted to UInt.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return true if the string representation can be converted to UInt.</returns>
		public static bool IsUInt(this string self)
		{
			uint result;
			return uint.TryParse(self, out result);
		}
		/// <summary>
		/// Convert the string representation to UInt. Throw exception for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in UInt.</returns>
		public static uint ToUInt(this string self)
		{
			return uint.Parse(self);
		}
		/// <summary>
		/// Convert the string representation to UInt. Return null for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in UInt?.</returns>
		public static uint? ToUIntOrNull(this string self)
		{
			uint result;
			return uint.TryParse(self, out result) ? (uint?)result : null;
		}
		/// <summary>
		/// Convert the string representation to UInt. Return the given defaultValue for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in UInt.</returns>
		public static uint ToUIntOrDefault(this string self, uint defaultValue = default(uint))
		{
			uint result;
			return uint.TryParse(self, out result) ? result : defaultValue;
		}

		/// <summary>
		/// Get a value indicating whether the string representation can be converted to Float.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return true if the string representation can be converted to Float.</returns>
		public static bool IsFloat(this string self)
		{
			float result;
			return float.TryParse(self, out result);
		}
		/// <summary>
		/// Convert the string representation to Float. Throw exception for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in Float.</returns>
		public static float ToFloat(this string self)
		{
			return float.Parse(self);
		}
		/// <summary>
		/// Convert the string representation to Float. Return null for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in Float?.</returns>
		public static float? ToFloatOrNull(this string self)
		{
			float result;
			return float.TryParse(self, out result) ? (float?)result : null;
		}
		/// <summary>
		/// Convert the string representation to Float. Return the given defaultValue for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in Float.</returns>
		public static float ToFloatOrDefault(this string self, float defaultValue = default(float))
		{
			float result;
			return float.TryParse(self, out result) ? result : defaultValue;
		}

		/// <summary>
		/// Get a value indicating whether the string representation can be converted to Long.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return true if the string representation can be converted to Long.</returns>
		public static bool IsLong(this string self)
		{
			long result;
			return long.TryParse(self, out result);
		}
		/// <summary>
		/// Convert the string representation to Long. Throw exception for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in Long.</returns>
		public static long ToLong(this string self)
		{
			return long.Parse(self);
		}
		/// <summary>
		/// Convert the string representation to Long. Return null for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in Long?.</returns>
		public static long? ToLongOrNull(this string self)
		{
			long result;
			return long.TryParse(self, out result) ? (long?)result : null;
		}
		/// <summary>
		/// Convert the string representation to Long. Return the given defaultValue for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in Long.</returns>
		public static long ToLongOrDefault(this string self, long defaultValue = default(long))
		{
			long result;
			return long.TryParse(self, out result) ? result : defaultValue;
		}

		/// <summary>
		/// Get a value indicating whether the string representation can be converted to ULong.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return true if the string representation can be converted to ULong.</returns>
		public static bool IsULong(this string self)
		{
			ulong result;
			return ulong.TryParse(self, out result);
		}
		/// <summary>
		/// Convert the string representation to ULong. Throw exception for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in ULong.</returns>
		public static ulong ToULong(this string self)
		{
			return ulong.Parse(self);
		}
		/// <summary>
		/// Convert the string representation to ULong. Return null for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in ULong?.</returns>
		public static ulong? ToULongOrNull(this string self)
		{
			ulong result;
			return ulong.TryParse(self, out result) ? (ulong?)result : null;
		}
		/// <summary>
		/// Convert the string representation to ULong. Return the given defaultValue for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in ULong.</returns>
		public static ulong ToULongOrDefault(this string self, ulong defaultValue = default(ulong))
		{
			ulong result;
			return ulong.TryParse(self, out result) ? result : defaultValue;
		}

		/// <summary>
		/// Get a value indicating whether the string representation can be converted to Double.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return true if the string representation can be converted to Double.</returns>
		public static bool IsDouble(this string self)
		{
			double result;
			return double.TryParse(self, out result);
		}
		/// <summary>
		/// Convert the string representation to Double. Throw exception for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in Double.</returns>
		public static double ToDouble(this string self)
		{
			return double.Parse(self);
		}
		/// <summary>
		/// Convert the string representation to Double. Return null for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in Double?.</returns>
		public static double? ToDoubleOrNull(this string self)
		{
			double result;
			return double.TryParse(self, out result) ? (double?)result : null;
		}
		/// <summary>
		/// Convert the string representation to Double. Return the given defaultValue for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in Double.</returns>
		public static double ToDoubleOrDefault(this string self, double defaultValue = default(double))
		{
			double result;
			return double.TryParse(self, out result) ? result : defaultValue;
		}

		/// <summary>
		/// Get a value indicating whether the string representation can be converted to Decimal.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return true if the string representation can be converted to Decimal.</returns>
		public static bool IsDecimal(this string self)
		{
			decimal result;
			return decimal.TryParse(self, out result);
		}
		/// <summary>
		/// Convert the string representation to Decimal. Throw exception for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in Decimal.</returns>
		public static decimal ToDecimal(this string self)
		{
			return decimal.Parse(self);
		}
		/// <summary>
		/// Convert the string representation to Decimal. Return null for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in Decimal?.</returns>
		public static decimal? ToDecimalOrNull(this string self)
		{
			decimal result;
			return decimal.TryParse(self, out result) ? (decimal?)result : null;
		}
		/// <summary>
		/// Convert the string representation to Decimal. Return the given defaultValue for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in Decimal.</returns>
		public static decimal ToDecimalOrDefault(this string self, decimal defaultValue = default(decimal))
		{
			decimal result;
			return decimal.TryParse(self, out result) ? result : defaultValue;
		}

		/// <summary>
		/// Get a value indicating whether the string representation can be converted to Decimal.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return true if the string representation can be converted to Decimal.</returns>
		public static bool IsDateTime(this string self)
		{
			DateTime result;
			return DateTime.TryParse(self, out result);
		}
		/// <summary>
		/// Convert the string representation to DateTime. Throw exception for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in DateTime.</returns>
		public static DateTime ToDateTime(this string self)
		{
			return DateTime.Parse(self);
		}
		/// <summary>
		/// Convert the string representation to DateTime. Return null for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in DateTime?.</returns>
		public static DateTime? ToDateTimeOrNull(this string self)
		{
			DateTime result;
			return DateTime.TryParse(self, out result) ? (DateTime?)result : null;
		}
		/// <summary>
		/// Convert the string representation to DateTime. Return the given defaultValue for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in DateTime.</returns>
		public static DateTime ToDateTimeOrDefault(this string self, DateTime defaultValue = default(DateTime))
		{
			DateTime result;
			return DateTime.TryParse(self, out result) ? result : defaultValue;
		}

		/// <summary>
		/// Convert the string representation to specific Enum. Throw exception for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in specific Enum.</returns>
		public static T ToEnum<T>(this string self, bool ignoreCase = true) where T : struct
		{
			return (T)Enum.Parse(typeof(T), self, ignoreCase);
		}
		/// <summary>
		/// Convert the string representation to specific Enum. Return the given defaultValue for failure.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Return the conversion result in specific Enum.</returns>
		public static T ToEnumOrDefault<T>(this string self, bool ignoreCase = true, T defaultValue = default(T)) where T : struct
		{
			return EnumUtils.ParseOrDefault(self, ignoreCase, defaultValue);
		}
		#endregion

		#region Encoding
		/// <summary>
		/// Get a value indicating whether all characters in the given string are ascii-alphanumeric.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>True if all characters in the given string are ascii-alphanumeric.</returns>
		public static bool IsAllAsciiAlphanumeric(this string self)
		{
			for (int i = 0, iCount = self.Length; i < iCount; ++i) {
				if (!self[i].IsAsciiAlphanumeric()) {
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Get a value indicating whether all characters in the string are half-width.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>True if all characters in the given string are half-width.</returns>
		public static bool IsAllHalfWidth(this string self)
		{
			for (int i = 0, iCount = self.Length; i < iCount; ++i) {
				if (self[i].IsFullWidth()) {
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Get a value indicating whether all characters in the string are full-width.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>True if all characters in the given string are full-width.</returns>
		public static bool IsAllFullWidth(this string self)
		{
			for (int i = 0, iCount = self.Length; i < iCount; ++i) {
				if (self[i].IsHalfWidth()) {
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Count the number of half-width characters in the string.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Number of half-width characters in the string.</returns>
		public static int CountHalfWidth(this string self)
		{
			int ret = 0;
			for (int i = 0, iCount = self.Length; i < iCount; ++i) {
				if (self[i].IsHalfWidth()) {
					ret++;
				}
			}
			return ret;
		}

		/// <summary>
		/// Count the number of full-width characters in the string.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>Number of full-width characters in the string.</returns>
		public static int CountFullWidth(this string self)
		{
			int ret = 0;
			for (int i = 0, iCount = self.Length; i < iCount; ++i) {
				if (self[i].IsFullWidth()) {
					ret++;
				}
			}
			return ret;
		}
		#endregion

		#region Path
		/// <summary>
		/// Get a string which replaces "\" with "/".
		/// Windows: Both "\" and "/" are legal path separators.
		/// Unix: Only "/" is the legal path separator.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <returns>A string which replaces "\" with "/".</returns>
		public static string ToUniversalPath(this string self)
		{
			return self.Replace("\\", "/");
		}

		/// <summary>
		/// Get a string which appends given extension to the origin.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <param name="extension">Extension string to append.</param>
		/// <param name="forceAppend">Force appending if the origin string endswith extension.</param>
		/// <returns>A string which appends given extension to the origin.</returns>
		public static string AppendExtension(this string self, string extension, bool forceAppend = false)
		{
			if (!forceAppend && self.EndsWith(extension)) {
				return self;
			}

			return self + extension;
		}

		/// <summary>
		/// Get a value indicating whether the string itself is under the given directory.
		/// </summary>
		/// <param name="self">String itself.</param>
		/// <param name="directory">Target directory.</param>
		/// <returns>True if the string itself is under the given directory.</returns>
		public static bool IsUnderDirectory(this string self, string directory)
		{
			if (self.Length < directory.Length)
			{
				return false;
			}

			if (self.StartsWith(directory))
			{
				// same-directory
				if (self.Length == directory.Length)
				{
					return true;
				}
				// sub-directory
				if (self[directory.Length] == Path.DirectorySeparatorChar)
				{
					return true;
				}
			}

			return false;
		}
		#endregion
	}
}

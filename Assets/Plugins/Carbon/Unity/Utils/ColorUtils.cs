using UnityEngine;

namespace Carbon
{
	public static class ColorUtils
	{
		private static readonly string ColorTagFormat = "<color=#{0}>{1}</color>";

		#region ValueToColor
		/// <summary>
		/// Get a Color from RGB.
		/// </summary>
		/// <param name="r">R.</param>
		/// <param name="g">G.</param>
		/// <param name="b">B.</param>
		/// <returns>Color.</returns>
		public static Color RGBToColor(int r, int g, int b)
		{
			return new Color(r / 255.0f, g / 255.0f, b / 255.0f);
		}

		/// <summary>
		/// Get a Color from RGB.
		/// </summary>
		/// <param name="value">RGB value in integer.</param>
		/// <returns>Color.</returns>
		public static Color RGBToColor(int value)
		{
			int r = (value >> 16) & 0xFF;
			int g = (value >> 8) & 0xFF;
			int b = (value) & 0xFF;
			return RGBToColor(r, g, b);
		}

		/// <summary>
		/// Get a Color from RGB hexadecimal-string.
		/// </summary>
		/// <param name="value">RGB value in hexadecimal-string.</param>
		/// <returns>Color.</returns>
		public static Color RGBToColor(string hex)
		{
			return RGBToColor(hex.ToIntOrDefault());
		}

		/// <summary>
		/// Get a Color from RGBA.
		/// </summary>
		/// <param name="r">R.</param>
		/// <param name="g">G.</param>
		/// <param name="b">B.</param>
		/// <param name="a">A.</param>
		/// <returns>Color.</returns>
		public static Color RGBAToColor(int r, int g, int b, int a)
		{
			return new Color(r / 255.0f, g / 255.0f, b / 255.0f, a / 255.0f);
		}

		/// <summary>
		/// Get a Color from RGBA.
		/// </summary>
		/// <param name="value">RGBA value in integer.</param>
		/// <returns>Color.</returns>
		public static Color RGBAToColor(int value)
		{
			int r = (value >> 24) & 0xFF;
			int g = (value >> 16) & 0xFF;
			int b = (value >> 8) & 0xFF;
			int a = (value) & 0xFF;
			return RGBAToColor(r, g, b, a);
		}

		/// <summary>
		/// Get a Color from RGBA hexadecimal-string.
		/// </summary>
		/// <param name="value">RGBA value in hexadecimal-string.</param>
		/// <returns>Color.</returns>
		public static Color RGBAToColor(string hex)
		{
			return RGBAToColor(hex.ToIntOrDefault());
		}

		/// <summary>
		/// Get a Color from ARGB.
		/// </summary>
		/// <param name="value">ARGB value in integer.</param>
		/// <returns>Color.</returns>
		public static Color ARGBToColor(int value)
		{
			int a = (value >> 24) & 0xFF;
			int r = (value >> 16) & 0xFF;
			int g = (value >> 8) & 0xFF;
			int b = (value) & 0xFF;
			return RGBAToColor(r, g, b, a);
		}

		/// <summary>
		/// Get a Color from ARGB hexadecimal-string.
		/// </summary>
		/// <param name="value">ARGB value in hexadecimal-string.</param>
		/// <returns>Color.</returns>
		public static Color ARGBToColor(string hex)
		{
			return ARGBToColor(hex.ToIntOrDefault());
		}
		#endregion

		#region ColorToValue
		/// <summary>
		/// Get a interger RGB value from given Color.
		/// </summary>
		/// <param name="color">Color.</param>
		/// <returns>A interger RGB value.</returns>
		public static int ColorToRGB(Color color)
		{
			int rgb = 0;
			rgb |= Mathf.RoundToInt(color.r * 255f) << 16;
			rgb |= Mathf.RoundToInt(color.g * 255f) << 8;
			rgb |= Mathf.RoundToInt(color.b * 255f);
			return rgb;
		}

		/// <summary>
		/// Get an RGB hexadecimal-string from given Color.
		/// </summary>
		/// <param name="color">Color.</param>
		/// <returns>An RGB hedadecimal-string.</returns>
		public static string ColorToRGBHex(Color color)
		{
			return ColorToRGB(color).ToString("X6");
		}

		/// <summary>
		/// Get a interger RGBA value from given Color.
		/// </summary>
		/// <param name="color">Color.</param>
		/// <returns>A interger RGBA value.</returns>
		public static int ColorToRGBA(Color color)
		{
			int rgba = 0;
			rgba |= Mathf.RoundToInt(color.r * 255f) << 24;
			rgba |= Mathf.RoundToInt(color.g * 255f) << 16;
			rgba |= Mathf.RoundToInt(color.b * 255f) << 8;
			rgba |= Mathf.RoundToInt(color.a * 255f);
			return rgba;
		}

		/// <summary>
		/// Get an RGBA hexadecimal-string from given Color.
		/// </summary>
		/// <param name="color">Color.</param>
		/// <returns>An RGBA hedadecimal-string.</returns>
		public static string ColorToRGBAHex(Color color)
		{
			return ColorToRGBA(color).ToString("X8");
		}

		/// <summary>
		/// Get a interger ARGB value from given Color.
		/// </summary>
		/// <param name="color">Color.</param>
		/// <returns>A interger ARGB value.</returns>
		public static int ColorToARGB(Color color)
		{
			int argb = 0;
			argb |= Mathf.RoundToInt(color.a * 255f) << 24;
			argb |= Mathf.RoundToInt(color.r * 255f) << 16;
			argb |= Mathf.RoundToInt(color.g * 255f) << 8;
			argb |= Mathf.RoundToInt(color.b * 255f);
			return argb;
		}

		/// <summary>
		/// Get an ARGB hexadecimal-string from given Color.
		/// </summary>
		/// <param name="color">Color.</param>
		/// <returns>An ARGB hedadecimal-string.</returns>
		public static string ColorToARGBHex(Color color)
		{
			return ColorToARGB(color).ToString("X8");
		}
		#endregion

		#region ColorTag
		public static string ColorToRGBTagFormat(Color color)
		{
			var hex = ColorToRGBHex(color);
			return ColorTagFormat.AsFormat(hex, "{0}");
		}

		public static string ColorToRGBATagFormat(Color color)
		{
			var hex = ColorToRGBAHex(color);
			return ColorTagFormat.AsFormat(hex, "{0}");
		}
		#endregion
	}
}
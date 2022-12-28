using System;

namespace Carbon
{
	public enum DateTimePattern
	{
		/// <summary>
		/// yyyy/MM/dd HH:mm:ss
		/// </summary>
		YMD_HMS,
		/// <summary>
		/// yyyy/MM/dd HH:mm
		/// </summary>
		YMD_HM,
		/// <summary>
		/// yyyy/MM/dd
		/// </summary>
		YMD__,
		/// <summary>
		/// yyyy年M月d日 HH:mm:ss
		/// </summary>
		YMDj_HMS,
		/// <summary>
		/// yyyy年M月d日 HH:mm
		/// </summary>
		YMDj_HM,
		/// <summary>
		/// yyyy年M月d日
		/// </summary>
		YMDj__,
		/// <summary>
		/// MM/dd HH:mm
		/// </summary>
		MD_HM,
		/// <summary>
		/// HH:mm:ss
		/// </summary>
		__HMS,
		/// <summary>
		/// HH:mm
		/// </summary>
		__HM,
	}

	/// <summary>
	/// DateTimePattern Extensions
	/// </summary>
	public static class DateTimePatternExtensions
	{
		public static string ToFormat(this DateTimePattern self)
		{
			switch (self) {
				case DateTimePattern.YMD_HMS:  return "yyyy/MM/dd HH:mm:ss";
				case DateTimePattern.YMD_HM:   return "yyyy/MM/dd HH:mm";
				case DateTimePattern.YMD__:    return "yyyy/MM/dd";

				case DateTimePattern.YMDj_HMS: return "yyyy年M月d日 HH:mm:ss";
				case DateTimePattern.YMDj_HM:  return "yyyy年M月d日 HH:mm";
				case DateTimePattern.YMDj__:   return "yyyy年M月d日 HH:mm";

				case DateTimePattern.MD_HM:    return "MM/dd HH:mm";
				case DateTimePattern.__HMS:    return "HH:mm:ss";
				case DateTimePattern.__HM:     return "HH:mm";
			}
			return "yyyy/MM/dd HH:mm:ss";
		}
	}

	/// <summary>
	/// DateTime Extensions
	/// </summary>
	public static class DateTimeExtensions
	{
		/// <summary>
		/// Get string with given pattern.
		/// </summary>
		/// <param name="self">DateTime itself.</param>
		/// <param name="pattern">Pattern of string.</param>
		/// <returns>Return string with given pattern.</returns>
		public static string ToPattern(this DateTime self, DateTimePattern pattern)
		{
			return self.ToString(pattern.ToFormat());
		}

		/// <summary>
		/// Convert DateTime to DateTimeOffset with specific hour-offset.
		/// </summary>
		/// <param name="self">DateTime itself.</param>
		/// <returns>Specific DateTimeOffset.</returns>
		public static DateTimeOffset ToSpecificDateTimeOffset(this DateTime self, TimeSpan utcTimeOffset)
		{
			// local -> utc -> jst
			// UTC + TimeOffset の「表示時間」だけを取る
			DateTime exp = self.ToUniversalTime().Add(utcTimeOffset);

			return new DateTimeOffset(
				exp.Year,
				exp.Month,
				exp.Day,
				exp.Hour,
				exp.Minute,
				exp.Second,
				exp.Millisecond,
				utcTimeOffset
			);
		}

		/// <summary>
		/// Convert to UnixTime.
		/// </summary>
		/// <param name="self">DateTime itself.</param>
		/// <returns>UnixTime.</returns>
		public static long ToUnixTime(this DateTime self)
		{
			return new DateTimeOffset(self).ToUnixTimeSeconds();
		}
	}
}

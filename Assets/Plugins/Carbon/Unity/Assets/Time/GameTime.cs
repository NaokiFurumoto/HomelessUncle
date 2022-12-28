using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Carbon
{
	/// <summary>
	/// ゲーム時間 (JST)
	/// </summary>
	[Serializable]
	public struct GameTime : IFormattable, IComparable, IComparable<GameTime>, IEquatable<GameTime>, IComparer<GameTime>, IEqualityComparer<GameTime>
	{
		//======================================================================================================
		// Definition
		//======================================================================================================
		/// <summary>
		/// JST - UTC 時差
		/// </summary>
		private const int JST_UTC_HOUR_OFFSET = +9;

		//======================================================================================================
		// Static Private Variable
		//======================================================================================================
		/// <summary>
		/// ゲーム時間起点
		/// </summary>
		private static long ms_GameBaseTime = 0;
		/// <summary>
		/// ローカル時間起点
		/// </summary>
		private static float ms_LocalBaseTime = 0;

		//======================================================================================================
		// Static Property
		//======================================================================================================
		/// <summary>
		/// 現在 UnixTime.
		/// <para>long 型 UnixTime そのままのため, サーバー UnixTime 時刻データと比較しやすいでしょう.</para>
		/// </summary>
		public static long NowUnixTime {
			get {
				return ms_GameBaseTime + (long)(Time.realtimeSinceStartup - ms_LocalBaseTime);
			}
		}

		/// <summary>
		/// 現在 Local-GameTime.
		/// </summary>
		public static GameTime Now {
			get {
				return new GameTime(NowUnixTime, DefaultLocalTimeOffset);
			}
		}

		/// <summary>
		/// 現在 UTC-GameTime.
		/// </summary>
		public static GameTime NowUtc {
			get {
				return new GameTime(NowUnixTime, TimeSpan.Zero);
			}
		}

		/// <summary>
		/// 現在 Local-GameTime.
		/// </summary>
		public static GameTime NowLocal => Now;

		/// <summary>
		/// Local-UTC 時差
		/// </summary>
		public static TimeSpan DefaultLocalTimeOffset { get; private set; } = TimeSpan.FromHours(JST_UTC_HOUR_OFFSET);

		//======================================================================================================
		// Static Constructor
		//======================================================================================================
		static GameTime()
		{
			SetBaseTime(DateTimeOffset.Now.ToUnixTimeSeconds());
		}

		//======================================================================================================
		// Static Public Method
		//======================================================================================================
		/// <summary>
		/// Local-UTC 時差設定
		/// </summary>
		public static void SetDefaultLocalTimeOffset(TimeSpan defaultLocalTimeOffset)
		{
			DefaultLocalTimeOffset = defaultLocalTimeOffset;
		}

		/// <summary>
		/// 基礎タイム設定
		/// </summary>
		public static void SetBaseTime(long baseTime)
		{
			ms_GameBaseTime = baseTime;
			ms_LocalBaseTime = Time.realtimeSinceStartup;
		}

		/// <summary>
		/// 指定時差でローカルタイムを作成.
		/// </summary>
		public static GameTime Create(long unixTime, TimeSpan localTimeOffset)
		{
			return new GameTime(unixTime, localTimeOffset);
		}

		/// <summary>
		/// UTCタイムを作成
		/// </summary>
		public static GameTime CreateUtc(long unixTime)
		{
			return Create(unixTime, TimeSpan.Zero);
		}

		/// <summary>
		/// 既定ローカルタイムを作成.
		/// </summary>
		public static GameTime CreateLocal(long unixTime)
		{
			return Create(unixTime, DefaultLocalTimeOffset);
		}

		/// <summary>
		/// 指定時差でローカルタイムを作成.
		/// </summary>
		public static GameTime Create(int year, int month, int day, int hour, int minute, int second, TimeSpan localTimeOffset)
		{
			return new GameTime(year, month, day, hour, minute, second, localTimeOffset);
		}

		/// <summary>
		/// UTCタイムを作成
		/// </summary>
		public static GameTime CreateUtc(int year, int month, int day, int hour, int minute, int second)
		{
			return Create(year, month, day, hour, minute, second, TimeSpan.Zero);
		}

		/// <summary>
		/// 既定ローカルタイムを作成.
		/// </summary>
		public static GameTime CreateLocal(int year, int month, int day, int hour, int minute, int second)
		{
			return Create(year, month, day, hour, minute, second, DefaultLocalTimeOffset);
		}

		/// <summary>
		/// 指定時差で時間文字列として解析.
		/// </summary>
		public static GameTime TryParse(string localTimeString, TimeSpan localTimeOffset)
		{
			return new GameTime(localTimeString, localTimeOffset);
		}

		/// <summary>
		/// 解析用 UTC 時差で時間文字列を解析して表記用時差で表記する.
		/// </summary>
		public static GameTime TryParse(string srcTimeString, TimeSpan srcTimeOffset, TimeSpan dstTimeOffset)
		{
			return new GameTime(srcTimeString, srcTimeOffset, dstTimeOffset);
		}

		/// <summary>
		/// UTC 時間文字列として解析.
		/// </summary>
		public static GameTime TryParseUtc(string timeString)
		{
			return TryParse(timeString, TimeSpan.Zero);
		}

		/// <summary>
		/// UTC 時間文字列として解析して既定ローカル時間で表記する.
		/// </summary>
		public static GameTime TryParseUtcToLocal(string timeString)
		{
			return TryParse(timeString, TimeSpan.Zero, DefaultLocalTimeOffset);
		}

		/// <summary>
		/// 既定ローカル時間文字列として解析
		/// </summary>
		public static GameTime TryParseLocal(string timeString)
		{
			return TryParse(timeString, DefaultLocalTimeOffset);
		}

		/// <summary>
		/// 既定ローカル時間文字列として解析して UTC 時間で表記する.
		/// </summary>
		public static GameTime TryParseLocalToUtc(string timeString)
		{
			return TryParse(timeString, DefaultLocalTimeOffset, TimeSpan.Zero);
		}

		//======================================================================================================
		// Private Variable
		//======================================================================================================
		/// <summary>
		/// UnixTime
		/// </summary>
		private long m_UnixTime;
		/// <summary>
		/// DateTimeOffset for expression
		/// </summary>
		private DateTimeOffset m_Expression;

		//======================================================================================================
		// Property
		//======================================================================================================
		/// <summary>
		/// UnixTime
		/// </summary>
		public long UnixTime => m_UnixTime;
		/// <summary>
		/// UTC 時差
		/// </summary>
		public TimeSpan LocalTimeOffset => m_Expression.Offset;

		#region expression
		public int			Year		=> m_Expression.Year;
		public int			Month		=> m_Expression.Month;
		public int			Day			=> m_Expression.Day;
		public int			DayOfYear	=> m_Expression.DayOfYear;
		public DayOfWeek	DayOfWeek	=> m_Expression.DayOfWeek;

		public int			Hour		=> m_Expression.Hour;
		public int			Minute		=> m_Expression.Minute;
		public int			Second		=> m_Expression.Second;
		public TimeSpan		TimeOfDay	=> m_Expression.TimeOfDay;
		#endregion

		//======================================================================================================
		// Public Constructor
		//======================================================================================================
		/// <summary>
		/// コピー
		/// </summary>
		/// <param name="other">コピー元</param>
		public GameTime(GameTime other)
		{
			m_UnixTime = other.m_UnixTime;
			m_Expression = other.m_Expression;
		}

		//======================================================================================================
		// Private Constructor
		//======================================================================================================
		/// <summary>
		/// UnixTime と UTC 時差で構築する.
		/// </summary>
		/// <param name="unixTime">UnixTime</param>
		private GameTime(long unixTime, TimeSpan localTimeOffset)
		{
			m_UnixTime = unixTime;
			m_Expression = DateTimeOffset.FromUnixTimeSeconds(m_UnixTime).ToOffset(localTimeOffset);
		}

		/// <summary>
		/// UTC 時差で時間を解析して構築する.
		/// </summary>
		/// <param name="localTimeOffset">時差</param>
		private GameTime(int year, int month, int day, int hour, int minute, int second, TimeSpan localTimeOffset)
		{
			DateTimeOffset parseResult = new DateTimeOffset(year, month, day, hour, minute, second, localTimeOffset);
			m_UnixTime = parseResult.ToUnixTimeSeconds();
			m_Expression = parseResult;
		}

		/// <summary>
		/// UTC 時差で時間文字列を解析して構築する.
		/// </summary>
		/// <param name="localTimeString">ローカル時間文字列</param>
		/// <param name="localTimeOffset">時差</param>
		private GameTime(string localTimeString, TimeSpan localTimeOffset)
		{
			DateTimeOffset parseResult;
			if (DateTimeOffset.TryParse(localTimeString, out parseResult)) {
				parseResult = new DateTimeOffset(parseResult.Ticks, localTimeOffset);
			}
			else {
				parseResult = DateTimeOffset.FromUnixTimeSeconds(0);
			}

			m_UnixTime = parseResult.ToUnixTimeSeconds();
			m_Expression = parseResult;
		}

		/// <summary>
		/// 解析用 UTC 時差で時間文字列を解析し, 表記用時差で構築する.
		/// </summary>
		/// <param name="srcTimeString">時間文字列</param>
		/// <param name="srcTimeOffset">解析用 UTC 時差</param>
		/// <param name="dstTimeOffset">表記用 UTC 時差</param>
		private GameTime(string srcTimeString, TimeSpan srcTimeOffset, TimeSpan dstTimeOffset)
		{
			DateTimeOffset parseResult;
			if (DateTimeOffset.TryParse(srcTimeString, out parseResult)) {
				parseResult = new DateTimeOffset(parseResult.Ticks, srcTimeOffset);
			}
			else {
				parseResult = DateTimeOffset.FromUnixTimeSeconds(0);
			}

			m_UnixTime = parseResult.ToUnixTimeSeconds();
			m_Expression = parseResult.ToOffset(dstTimeOffset);
		}

		//======================================================================================================
		// Public Method (Compare)
		//======================================================================================================
		/// <summary>
		/// 期間中か
		/// </summary>
		public bool IsWithIn(long beginUnixTime, long endUnixTime)
		{
			return (m_UnixTime >= beginUnixTime && m_UnixTime <= endUnixTime);
		}

		/// <summary>
		/// 期間中か
		/// </summary>
		public bool IsWithIn(GameTime beginTime, GameTime endTime)
		{
			return (m_UnixTime >= beginTime && m_UnixTime <= endTime);
		}

		/// <summary>
		/// 同じ日か
		/// </summary>
		public bool IsSameDay(GameTime other)
		{
			if (other.Year != this.Year)
			{
				return false;
			}

			return (other.DayOfYear == this.DayOfYear);
		}

		/// <summary>
		/// 同じ週か
		/// </summary>
		public bool IsSameWeek(GameTime other)
		{
			double deltaDays = (other - this).TotalDays;
			if (deltaDays <= -7 || deltaDays >= 7)
			{
				return false;
			}
			if (deltaDays == 0)
			{
				return true;
			}

			if (other.Day == this.Day)
			{
				return true;
			}

			return (deltaDays > 0) ? (other.DayOfWeek > this.DayOfWeek) : (other.DayOfWeek < this.DayOfWeek);

			// https://docs.microsoft.com/ja-jp/dotnet/api/system.globalization.calendar.getweekofyear?view=netframework-4.6
			//if (other.Year != this.Year)
			//{
			//	return false;
			//}
			//var thisWeek = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(this.m_Expression.DateTime, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
			//var otherWeek = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(other.m_Expression.DateTime, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
			//return otherWeek == thisWeek;
		}

		/// <summary>
		/// 同じ月か
		/// </summary>
		public bool IsSameMonth(GameTime other)
		{
			if (other.Year != this.Year)
			{
				return false;
			}

			return (other.Month == this.Month);
		}

		//======================================================================================================
		// Public Method (Set Time)
		//======================================================================================================
		public void SetTime(long unixTime)
		{
			m_UnixTime = unixTime;
			m_Expression = DateTimeOffset.FromUnixTimeSeconds(m_UnixTime).ToOffset(LocalTimeOffset);
		}

		public void SetTime(GameTime gameTime)
		{
			SetTime(gameTime.m_UnixTime);
		}

		//======================================================================================================
		// Public Method (Adjust Time)
		//======================================================================================================
		public GameTime AddYears(int years)
		{
			GameTime ret = new GameTime(this);
			ret.m_Expression = ret.m_Expression.AddYears(years);
			ret.UpdateUnixTimeByExpression();
			return ret;
		}
		public GameTime AddMonths(int months)
		{
			GameTime ret = new GameTime(this);
			ret.m_Expression = ret.m_Expression.AddMonths(months);
			ret.UpdateUnixTimeByExpression();
			return ret;
		}
		public GameTime AddTime(int days, int hours, int minutes, int seconds)
		{
			return this + new TimeSpan(days, hours, minutes, seconds);
		}
		public GameTime AddTime(int hours, int minutes, int seconds)
		{
			return this + new TimeSpan(hours, minutes, seconds);
		}
		public GameTime AddDays(int days)
		{
			return this + TimeSpan.FromDays(days);
		}
		public GameTime AddHours(int hours)
		{
			return this + TimeSpan.FromHours(hours);
		}
		public GameTime AddMinutes(int minutes)
		{
			return this + TimeSpan.FromMinutes(minutes);
		}
		public GameTime AddSeconds(int seconds)
		{
			return this + TimeSpan.FromSeconds(seconds);
		}

		//======================================================================================================
		// Public Method (Copy Convert)
		//======================================================================================================
		public GameTime To(TimeSpan localTimeOffset)
		{
			return Create(m_UnixTime, localTimeOffset);
		}

		public GameTime ToUtc()
		{
			return CreateUtc(m_UnixTime);
		}

		public GameTime ToLocal()
		{
			return CreateLocal(m_UnixTime);
		}

		//======================================================================================================
		// Public Method (DateTimePattern)
		//======================================================================================================
		public string ToPattern(DateTimePattern pattern)
		{
			return ToString(pattern.ToFormat());
		}

		//======================================================================================================
		// Public Method (Interface override)
		//======================================================================================================
		public long ToFileTime()
		{
			return m_Expression.ToFileTime();
		}
		public string ToString(string format)
		{
			return m_Expression.ToString(format);
		}
		public string ToString(IFormatProvider formatProvider)
		{
			return m_Expression.ToString(formatProvider);
		}
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return m_Expression.ToString(format, formatProvider);
		}

		// IComparable<GameTime>
		public int CompareTo(GameTime other)
		{
			return m_UnixTime.CompareTo(other.m_UnixTime);
		}

		// IEquatable<GameTime>
		public bool Equals(GameTime other)
		{
			return m_UnixTime == other.m_UnixTime;
		}

		// ValueType (for '==' and '!=')
		public override bool Equals(object obj)
		{
			if (obj.GetType() != GetType()) {
				return false;
			}
			return Equals((GameTime)obj);
		}
		public override int GetHashCode()
		{
			return m_UnixTime.GetHashCode();
		}
		public override string ToString()
		{
			return m_Expression.ToString();
		}

		// IComparable
		int IComparable.CompareTo(object obj)
		{
			if (obj.GetType() != GetType()) {
				throw new ArgumentException();
			}
			return CompareTo((GameTime)obj);
		}

		// IComparer<GameTime>
		int IComparer<GameTime>.Compare(GameTime x, GameTime y)
		{
			return x.CompareTo(y);
		}

		// IEqualityComparer<GameTime>
		bool IEqualityComparer<GameTime>.Equals(GameTime x, GameTime y)
		{
			return x.Equals(y);
		}

		int IEqualityComparer<GameTime>.GetHashCode(GameTime obj)
		{
			return m_UnixTime.GetHashCode();
		}

		//======================================================================================================
		// Private Method
		//======================================================================================================
		private void UpdateUnixTimeByExpression()
		{
			m_UnixTime = m_Expression.ToUnixTimeSeconds();
		}

		//======================================================================================================
		// Operator : Calculation
		//======================================================================================================
		public static TimeSpan operator +(GameTime t1, GameTime t2)
		{
			return TimeSpan.FromSeconds(t1.m_UnixTime + t2.m_UnixTime);
		}
		public static TimeSpan operator -(GameTime t1, GameTime t2)
		{
			return TimeSpan.FromSeconds(t1.m_UnixTime - t2.m_UnixTime);
		}

		public static GameTime operator +(GameTime x, TimeSpan t)
		{
			return new GameTime(x.m_UnixTime + (long)t.TotalSeconds, x.LocalTimeOffset);
		}
		public static GameTime operator -(GameTime x, TimeSpan t)
		{
			return new GameTime(x.m_UnixTime - (long)t.TotalSeconds, x.LocalTimeOffset);
		}

		//======================================================================================================
		// Operator : Comparison
		//======================================================================================================
		public static bool operator ==(GameTime t1, GameTime t2)
		{
			return t1.m_UnixTime == t2.m_UnixTime;
		}
		public static bool operator !=(GameTime t1, GameTime t2)
		{
			return t1.m_UnixTime != t2.m_UnixTime;
		}
		public static bool operator <(GameTime t1, GameTime t2)
		{
			return t1.m_UnixTime < t2.m_UnixTime;
		}
		public static bool operator >(GameTime t1, GameTime t2)
		{
			return t1.m_UnixTime > t2.m_UnixTime;
		}
		public static bool operator <=(GameTime t1, GameTime t2)
		{
			return t1.m_UnixTime <= t2.m_UnixTime;
		}
		public static bool operator >=(GameTime t1, GameTime t2)
		{
			return t1.m_UnixTime >= t2.m_UnixTime;
		}


		public static bool operator ==(GameTime t, long unixTime)
		{
			return t.m_UnixTime == unixTime;
		}
		public static bool operator !=(GameTime t, long unixTime)
		{
			return t.m_UnixTime != unixTime;
		}
		public static bool operator <(GameTime t, long unixTime)
		{
			return t.m_UnixTime < unixTime;
		}
		public static bool operator >(GameTime t, long unixTime)
		{
			return t.m_UnixTime > unixTime;
		}
		public static bool operator <=(GameTime t, long unixTime)
		{
			return t.m_UnixTime <= unixTime;
		}
		public static bool operator >=(GameTime t, long unixTime)
		{
			return t.m_UnixTime >= unixTime;
		}


		public static bool operator ==(long unixTime, GameTime t)
		{
			return unixTime == t.m_UnixTime;
		}
		public static bool operator !=(long unixTime, GameTime t)
		{
			return unixTime != t.m_UnixTime;
		}
		public static bool operator <(long unixTime, GameTime t)
		{
			return unixTime < t.m_UnixTime;
		}
		public static bool operator >(long unixTime, GameTime t)
		{
			return unixTime > t.m_UnixTime;
		}
		public static bool operator <=(long unixTime, GameTime t)
		{
			return unixTime <= t.m_UnixTime;
		}
		public static bool operator >=(long unixTime, GameTime t)
		{
			return unixTime >= t.m_UnixTime;
		}
	}
}

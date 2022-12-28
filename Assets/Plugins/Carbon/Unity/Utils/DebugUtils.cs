using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Carbon
{
	/// <summary>
	/// Debug Utilities
	/// </summary>
	public static class DebugUtils
	{
		public static bool PassAssertionFailed = false;
		//======================================================================================================================
		// LogHeader
		//======================================================================================================================
		/// <summary>
		/// ログヘッダー
		/// </summary>
		public enum LogHeader
		{
			/// <summary> なし </summary>
			None,
			/// <summary> 頭文字のみ. Error = [E] </summary>
			Initial,
			/// <summary> フル. [Error], [Warning], ... </summary>
			Full,
		}

		/// <summary>
		/// ログヘッダーハッシュ
		/// </summary>
		private static readonly Dictionary<LogType, string> ms_LogHeaderHash = new Dictionary<LogType, string>();

		/// <summary>
		/// ログヘッダーハッシュ作成
		/// </summary>
		private static void CreateLogHeaderHash(LogHeader logHeader)
		{
			ms_LogHeaderHash.Clear();
			switch (logHeader) {
				case LogHeader.None:
				ms_LogHeaderHash.Add(LogType.Log      , "");
				ms_LogHeaderHash.Add(LogType.Warning  , "");
				ms_LogHeaderHash.Add(LogType.Error    , "");
				ms_LogHeaderHash.Add(LogType.Assert   , "");
				ms_LogHeaderHash.Add(LogType.Exception, "");
				break;

				case LogHeader.Initial:
				ms_LogHeaderHash.Add(LogType.Log      , "[-] ");
				ms_LogHeaderHash.Add(LogType.Warning  , "[W] ");
				ms_LogHeaderHash.Add(LogType.Error    , "[E] ");
				ms_LogHeaderHash.Add(LogType.Assert   , "[A] ");
				ms_LogHeaderHash.Add(LogType.Exception, "[X] ");
				break;

				case LogHeader.Full:
				ms_LogHeaderHash.Add(LogType.Log      , "[-] ");
				ms_LogHeaderHash.Add(LogType.Warning  , "[Warning] ");
				ms_LogHeaderHash.Add(LogType.Error    , "[Error] ");
				ms_LogHeaderHash.Add(LogType.Assert   , "[Assert] ");
				ms_LogHeaderHash.Add(LogType.Exception, "[Exception] ");
				break;
			}
		}

		/// <summary>
		/// ログヘッダー設定
		/// </summary>
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void SetLogHeader(LogHeader logHeader)
		{
			CreateLogHeaderHash(logHeader);
		}

		//======================================================================================================================
		// Event
		//======================================================================================================================

		/// <summary>
		/// Application.logMessageReceivedThreaded
		/// </summary>
		public static Action<LogType, string, string> OnAllThreadLog { get; set; }

		/// <summary>
		/// Application.logMessageReceived
		/// </summary>
		public static Action<LogType, string, string> OnMainThreadLog { get; set; }


		//======================================================================================================================
		// Constructor
		//======================================================================================================================
		static DebugUtils()
		{
			Application.logMessageReceived         += OnLogReceivedMain;
			Application.logMessageReceivedThreaded += OnLogReceivedAll;
			CreateLogHeaderHash(LogHeader.None);
		}

		//======================================================================================================================
		// Log Handler
		//======================================================================================================================
		private static void OnLogReceivedAll( string condition, string stackTrace, LogType type )
		{
			OnAllThreadLog.Call( type, condition, stackTrace );
		}
		private static void OnLogReceivedMain( string condition, string stackTrace, LogType type )
		{
			OnMainThreadLog.Call( type, condition, stackTrace );
		}

		//======================================================================================================================
		// Log
		//======================================================================================================================
		#region Break-ENABLE_DEBUG_UTILS
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void Break()
		{
			UnityEngine.Debug.Break();
		}
		#endregion

		#region Log-ENABLE_DEBUG_UTILS
		// Log
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void Log(object message)
		{
			UnityEngine.Debug.Log(ms_LogHeaderHash[LogType.Log] + message);
		}
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void Log(object message, UnityEngine.Object context)
		{
			UnityEngine.Debug.Log(ms_LogHeaderHash[LogType.Log] + message, context);
		}
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void Log(string format, params object[] args)
		{
			UnityEngine.Debug.Log(ms_LogHeaderHash[LogType.Log] + string.Format(format, args));
		}

		// Log if true
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void LogTrue(bool condition, object message)
		{
			if (condition)
				Log(message);
		}
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void LogTrue(bool condition, object message, UnityEngine.Object context)
		{
			if (condition)
				Log(message, context);
		}
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void LogTrue(bool condition, string format, params object[] args)
		{
			if (condition)
				Log(format, args);
		}

		// Log if false
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void LogFalse(bool condition, object message)
		{
			LogTrue(!condition, message);
		}
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void LogFalse(bool condition, object message, UnityEngine.Object context)
		{
			LogTrue(!condition, message, context);
		}
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void LogFalse(bool condition, string format, params object[] args)
		{
			LogTrue(!condition, format, args);
		}

		// Log in color
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void LogColor(Color color, object message)
		{
			Log("<color=#{0}>{1}</color>", ColorUtility.ToHtmlStringRGB(color), message);
		}
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void LogColor(Color color, string format, params object[] args)
		{
			Log("<color=#{0}>{1}</color>", ColorUtility.ToHtmlStringRGB(color), string.Format(format, args));
		}
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void LogRed(object message)
		{
			LogColor(Color.red, message.ToString());
		}
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void LogRed(string format, params object[] args)
		{
			LogColor(Color.red, format, args);
		}
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void LogBlue(object message)
		{
			LogColor(Color.blue, message.ToString());
		}
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void LogBlue(string format, params object[] args)
		{
			LogColor(Color.blue, format, args);
		}
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void LogGreen(object message)
		{
			LogColor(Color.green, message.ToString());
		}
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void LogGreen(string format, params object[] args)
		{
			LogColor(Color.green, format, args);
		}
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void LogCyan(object message)
		{
			LogColor(Color.cyan, message.ToString());
		}
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void LogCyan(string format, params object[] args)
		{
			LogColor(Color.cyan, format, args);
		}
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void LogYellow(object message)
		{
			LogColor(Color.yellow, message.ToString());
		}
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void LogYellow(string format, params object[] args)
		{
			LogColor(Color.yellow, format, args);
		}
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void LogMagenta(object message)
		{
			LogColor(Color.magenta, message.ToString());
		}
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void LogMagenta(string format, params object[] args)
		{
			LogColor(Color.magenta, format, args);
		}
		#endregion

		#region Warning-ENABLE_DEBUG_UTILS
		// Log-Warning
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void Warning(object message)
		{
			UnityEngine.Debug.LogWarning(ms_LogHeaderHash[LogType.Warning] + message);
		}
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void Warning(object message, UnityEngine.Object context)
		{
			UnityEngine.Debug.LogWarning(ms_LogHeaderHash[LogType.Warning] + message, context);
		}
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void Warning(string format, params object[] args)
		{
			UnityEngine.Debug.LogWarning(ms_LogHeaderHash[LogType.Warning] + string.Format(format, args));
		}

		// Log-Warning if true
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void WarningTrue(bool condition, object message)
		{
			if (condition)
				Warning(message);
		}
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void WarningTrue(bool condition, object message, UnityEngine.Object context)
		{
			if (condition)
				Warning(message, context);
		}
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void WarningTrue(bool condition, string format, params object[] args)
		{
			if (condition)
				Warning(format, args);
		}

		// Log-Warning if false
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void WarningFalse(bool condition, object message)
		{
			WarningTrue(!condition, message);
		}
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void WarningFalse(bool condition, object message, UnityEngine.Object context)
		{
			WarningTrue(!condition, message, context);
		}
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void WarningFalse(bool condition, string format, params object[] args)
		{
			WarningTrue(!condition, format, args);
		}
		#endregion

		#region Assert-ENABLE_DEBUG_UTILS
		// Log and Assert 
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void Assert(object message)
		{
			if (PassAssertionFailed) {
				UnityEngine.Debug.LogAssertion(ms_LogHeaderHash[LogType.Assert] + message);
				return;
			}
			
			UnityEngine.Debug.LogError("!!!!!!!!!!!!!!!! ASSERTION FAILED !!!!!!!!!!!!!!!!\n" + ms_LogHeaderHash[LogType.Assert] + message);
		}
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void Assert(object message, UnityEngine.Object context)
		{
			if (PassAssertionFailed) {
				UnityEngine.Debug.LogAssertion(ms_LogHeaderHash[LogType.Assert] + message, context);
				return;
			}

			UnityEngine.Debug.LogError("!!!!!!!!!!!!!!!! ASSERTION FAILED !!!!!!!!!!!!!!!!\n" + ms_LogHeaderHash[LogType.Assert] + message, context);
		}
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void Assert(string format, params object[] args)
		{
			if (PassAssertionFailed) {
				UnityEngine.Debug.LogAssertion(ms_LogHeaderHash[LogType.Assert] + string.Format(format, args));
				return;
			}

			UnityEngine.Debug.LogError("!!!!!!!!!!!!!!!! ASSERTION FAILED !!!!!!!!!!!!!!!!\n" + ms_LogHeaderHash[LogType.Assert] + string.Format(format, args));
		}

		/// <summary>
		/// Asserts that the condition is true. (Logs while the condition is false.)
		/// </summary>
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void AssertTrue(bool condition, object message)
		{
			if (condition) {
				return;
			}
			Assert(message);
		}
		/// <summary>
		/// Asserts that the condition is true. (Logs while the condition is false.)
		/// </summary>
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void AssertTrue(bool condition, object message, UnityEngine.Object context)
		{
			if (condition) {
				return;
			}
			Assert(message, context);
		}
		/// <summary>
		/// Asserts that the condition is true. (Logs while the condition is false.)
		/// </summary>
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void AssertTrue(bool condition, string format, params object[] args)
		{
			if (condition) {
				return;
			}
			Assert(format, args);
		}

		/// <summary>
		/// Asserts that the condition is false. (Logs while the condition is true.)
		/// </summary>
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void AssertFalse(bool condition, object message)
		{
			if (!condition) {
				return;
			}
			Assert(message);
		}
		/// <summary>
		/// Asserts that the condition is false. (Logs while the condition is true.)
		/// </summary>
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void AssertFalse(bool condition, object message, UnityEngine.Object context)
		{
			if (!condition) {
				return;
			}
			Assert(message, context);
		}
		/// <summary>
		/// Asserts that the condition is false. (Logs while the condition is true.)
		/// </summary>
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void AssertFalse(bool condition, string format, params object[] args)
		{
			if (!condition) {
				return;
			}
			Assert(format, args);
		}

		/// <summary>
		/// Asserts that the obj is not null. (Logs while the obj is null.)
		/// </summary>
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void AssertNotNull<T>(T obj, object message) where T : class
		{
			if (obj != null) {
				return;
			}
			Assert(message);
		}
		/// <summary>
		/// Asserts that the obj is not null. (Logs while the obj is null.)
		/// </summary>
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void AssertNotNull<T>(T obj, object message, UnityEngine.Object context) where T : class
		{
			if (obj != null) {
				return;
			}
			Assert(message, context);
		}
		/// <summary>
		/// Asserts that the obj is not null. (Logs while the obj is null.)
		/// </summary>
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void AssertNotNull<T>(T obj, string format, params object[] args) where T : class
		{
			if (obj != null) {
				return;
			}
			Assert(format, args);
		}

		/// <summary>
		/// Asserts that the obj is null. (Logs while the obj is not null.)
		/// </summary>
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void AssertNull<T>(T obj, object message) where T : class
		{
			if (obj == null) {
				return;
			}
			Assert(message);
		}
		/// <summary>
		/// Asserts that the obj is null. (Logs while the obj is not null.)
		/// </summary>
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void AssertNull<T>(T obj, object message, UnityEngine.Object context) where T : class
		{
			if (obj == null) {
				return;
			}
			Assert(message, context);
		}
		/// <summary>
		/// Asserts that the obj is null. (Logs while the obj is not null.)
		/// </summary>
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void AssertNull<T>(T obj, string format, params object[] args) where T : class
		{
			if (obj == null) {
				return;
			}
			Assert(format, args);
		}

		/// <summary>
		/// Asserts that the UnityObject exists. (Logs while the UnityObject does not exist.)
		/// </summary>
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void AssertExists(UnityEngine.Object unityObject, object message)
		{
			if (unityObject) {
				return;
			}
			Assert(message);
		}
		/// <summary>
		/// Asserts that the UnityObject exists. (Logs while the UnityObject does not exist.)
		/// </summary>
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void AssertExists(UnityEngine.Object unityObject, object message, UnityEngine.Object context)
		{
			if (unityObject) {
				return;
			}
			Assert(message, context);
		}
		/// <summary>
		/// Asserts that the UnityObject exists. (Logs while the UnityObject does not exist.)
		/// </summary>
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void AssertExists(UnityEngine.Object unityObject, string format, params object[] args)
		{
			if (unityObject) {
				return;
			}
			Assert(format, args);
		}

		/// <summary>
		/// Asserts that the UnityObject does not exist. (Logs while the UnityObject exists.)
		/// </summary>
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void AssertNotExists(UnityEngine.Object unityObject, object message)
		{
			if (!unityObject) {
				return;
			}
			Assert(message);
		}
		/// <summary>
		/// Asserts that the UnityObject does not exist. (Logs while the UnityObject exists.)
		/// </summary>
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void AssertNotExists(UnityEngine.Object unityObject, object message, UnityEngine.Object context)
		{
			if (!unityObject) {
				return;
			}
			Assert(message, context);
		}
		/// <summary>
		/// Asserts that the UnityObject does not exist. (Logs while the UnityObject exists.)
		/// </summary>
		[Conditional("ENABLE_DEBUG_UTILS"), DebuggerHidden, DebuggerStepThrough]
		public static void AssertNotExists(UnityEngine.Object unityObject, string format, params object[] args)
		{
			if (!unityObject) {
				return;
			}
			Assert(format, args);
		}
		#endregion

		#region Error
		public static void Error(object message)
		{
			UnityEngine.Debug.LogError(ms_LogHeaderHash[LogType.Error] + message);
		}
		public static void Error(object message, UnityEngine.Object context)
		{
			UnityEngine.Debug.LogError(ms_LogHeaderHash[LogType.Error] + message, context);
		}
		public static void Error(string format, params object[] args)
		{
			UnityEngine.Debug.LogError(ms_LogHeaderHash[LogType.Error] + string.Format(format, args));
		}

		public static void ErrorTrue(bool condition, object message)
		{
			if (condition)
				Error(message);
		}
		public static void ErrorTrue(bool condition, object message, UnityEngine.Object context)
		{
			if (condition)
				Error(message, context);
		}
		public static void ErrorTrue(bool condition, string format, params object[] args)
		{
			if (condition)
				Error(format, args);
		}

		public static void ErrorFalse(bool condition, object message)
		{
			ErrorTrue(!condition, message);
		}
		public static void ErrorFalse(bool condition, object message, UnityEngine.Object context)
		{
			ErrorTrue(!condition, message, context);
		}
		public static void ErrorFalse(bool condition, string format, params object[] args)
		{
			ErrorTrue(!condition, format, args);
		}
		#endregion

		#region Exception
		public static void Exception(object message)
		{
			UnityEngine.Debug.LogException(new Exception(ms_LogHeaderHash[LogType.Exception] + message.ToString()));
		}
		public static void Exception(object message, UnityEngine.Object context)
		{
			UnityEngine.Debug.LogException(new Exception(ms_LogHeaderHash[LogType.Exception] + message.ToString()), context);
		}
		public static void Exception(string format, params object[] args)
		{
			UnityEngine.Debug.LogException(new Exception(ms_LogHeaderHash[LogType.Exception] + string.Format(format, args)));
		}

		public static void ExceptionTrue(bool condition, object message)
		{
			if (condition)
				Exception(message);
		}
		public static void ExceptionTrue(bool condition, object message, UnityEngine.Object context)
		{
			if (condition)
				Exception(message, context);
		}
		public static void ExceptionTrue(bool condition, string format, params object[] args)
		{
			if (condition)
				Exception(format, args);
		}

		public static void ExceptionFalse(bool condition, object message)
		{
			ExceptionTrue(!condition, message);
		}
		public static void ExceptionFalse(bool condition, object message, UnityEngine.Object context)
		{
			ExceptionTrue(!condition, message, context);
		}
		public static void ExceptionFalse(bool condition, string format, params object[] args)
		{
			ExceptionTrue(!condition, format, args);
		}
		#endregion
	}
}
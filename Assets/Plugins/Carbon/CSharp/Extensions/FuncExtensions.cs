using System;
using System.Diagnostics;

namespace Carbon
{
	/// <summary>
	/// Extensions for Func
	/// </summary>
	public static class FuncExtensions
	{
		[DebuggerHidden]
		[DebuggerStepThrough]
		public static TResult Call<TResult>(this Func<TResult> func, TResult result = default(TResult))
		{
			return func != null ? func() : result;
		}

		[DebuggerHidden]
		[DebuggerStepThrough]
		public static TResult Call<T, TResult>(this Func<T, TResult> func, T arg, TResult result = default(TResult))
		{
			return func != null ? func(arg) : result;
		}

		[DebuggerHidden]
		[DebuggerStepThrough]
		public static TResult Call<T1, T2, TResult>(this Func<T1, T2, TResult> func, T1 arg1, T2 arg2, TResult result = default(TResult))
		{
			return func != null ? func(arg1, arg2) : result;
		}

		[DebuggerHidden]
		[DebuggerStepThrough]
		public static TResult Call<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func, T1 arg1, T2 arg2, T3 arg3, TResult result = default(TResult))
		{
			return func != null ? func(arg1, arg2, arg3) : result;
		}

		[DebuggerHidden]
		[DebuggerStepThrough]
		public static TResult Call<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, TResult result = default(TResult))
		{
			return func != null ? func(arg1, arg2, arg3, arg4) : result;
		}
	}
}
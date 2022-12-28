using System;
using System.Diagnostics;

namespace Carbon
{
	/// <summary>
	/// Extensions for Action
	/// </summary>
	public static partial class ActionExtensions
	{
		[DebuggerHidden]
		[DebuggerStepThrough]
		public static void Call(this Action action)
		{
			action?.Invoke();
		}

		[DebuggerHidden]
		[DebuggerStepThrough]
		public static void Call<T>(this Action<T> action, T arg)
		{
			action?.Invoke(arg);
		}

		[DebuggerHidden]
		[DebuggerStepThrough]
		public static void Call<T1, T2>(this Action<T1, T2> action, T1 arg1, T2 arg2)
		{
			action?.Invoke(arg1, arg2);
		}

		[DebuggerHidden]
		[DebuggerStepThrough]
		public static void Call<T1, T2, T3>(this Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
		{
			action?.Invoke(arg1, arg2, arg3);
		}

		[DebuggerHidden]
		[DebuggerStepThrough]
		public static void Call<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			action?.Invoke(arg1, arg2, arg3, arg4);
		}

		[DebuggerHidden]
		[DebuggerStepThrough]
		public static void Call<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 args5)
		{
			action?.Invoke(arg1, arg2, arg3, arg4, args5);
		}

		[DebuggerHidden]
		[DebuggerStepThrough]
		public static void CallOrDefault(this Action action, Action defaultValue)
		{
			if (action != null) {
				action();
				return;
			}
			defaultValue.Call();
		}

		[DebuggerHidden]
		[DebuggerStepThrough]
		public static void CallOrDefault<T>(this Action<T> action, T arg, Action defaultValue)
		{
			if (action != null) {
				action(arg);
				return;
			}
			defaultValue.Call();
		}

		[DebuggerHidden]
		[DebuggerStepThrough]
		public static void CallOrDefault<T1, T2>(this Action<T1, T2> action, T1 arg1, T2 arg2, Action defaultValue)
		{
			if (action != null) {
				action(arg1, arg2);
				return;
			}
			defaultValue.Call();
		}

		[DebuggerHidden]
		[DebuggerStepThrough]
		public static void CallOrDefault<T1, T2, T3>(this Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3, Action defaultValue)
		{
			if (action != null) {
				action(arg1, arg2, arg3);
				return;
			}
			defaultValue.Call();
		}

		[DebuggerHidden]
		[DebuggerStepThrough]
		public static void CallOrDefault<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action defaultValue)
		{
			if (action != null) {
				action(arg1, arg2, arg3, arg4);
				return;
			}
			defaultValue.Call();
		}

		[DebuggerHidden]
		[DebuggerStepThrough]
		public static void CallOrDefault<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 args5, Action defaultValue)
		{
			if (action != null)
			{
				action(arg1, arg2, arg3, arg4, args5);
				return;
			}
			defaultValue.Call();
		}
	}
}
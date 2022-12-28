using System;
using System.Diagnostics;

namespace Carbon
{
	public static class ActionUtils
	{
		[DebuggerHidden]
		[DebuggerStepThrough]
		public static void CallOnce(ref Action rAction)
		{
			if (rAction == null) {
				return;
			}

			Action tAction = rAction;
			rAction = null;
			tAction();
		}

		[DebuggerHidden]
		[DebuggerStepThrough]
		public static void CallOnce<T>(ref Action<T> rAction, T arg)
		{
			if (rAction == null) {
				return;
			}

			Action<T> tAction = rAction;
			rAction = null;
			tAction(arg);
		}

		[DebuggerHidden]
		[DebuggerStepThrough]
		public static void CallOnce<T1, T2>(ref Action<T1, T2> rAction, T1 arg1, T2 arg2)
		{
			if (rAction == null) {
				return;
			}

			Action<T1, T2> tAction = rAction;
			rAction = null;
			tAction(arg1, arg2);
		}

		[DebuggerHidden]
		[DebuggerStepThrough]
		public static void CallOnce<T1, T2, T3>(ref Action<T1, T2, T3> rAction, T1 arg1, T2 arg2, T3 arg3)
		{
			if (rAction == null) {
				return;
			}

			Action<T1, T2, T3> tAction = rAction;
			rAction = null;
			tAction(arg1, arg2, arg3);
		}

		[DebuggerHidden]
		[DebuggerStepThrough]
		public static void CallOnce<T1, T2, T3, T4>(ref Action<T1, T2, T3, T4> rAction, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			if (rAction == null) {
				return;
			}

			Action<T1, T2, T3, T4> tAction = rAction;
			rAction = null;
			tAction(arg1, arg2, arg3, arg4);
		}
	}
}
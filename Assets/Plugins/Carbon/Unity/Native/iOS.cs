using UnityEngine;
using System.Runtime.InteropServices;

namespace Carbon.Native
{
	public static class IOS
	{
#if UNITY_IOS
		[DllImport("__Internal")] static extern void _SetText(string text);
#endif
		/// <summary>
		/// クリップボード
		/// </summary>
		/// <param name="text"></param>
		public static void Clipboard(string text)
		{
#if UNITY_IOS
			_SetText(text);
#endif
		}
	}
}
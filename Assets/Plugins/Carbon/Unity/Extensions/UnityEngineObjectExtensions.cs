using UnityEngine;

namespace Carbon
{
	public static class UnityEngineObjectExtensions
	{
		/// <summary>
		/// 破棄します.
		/// </summary>
		/// <param name="self">Object 自身.</param>
		public static void Destroy(this Object self)
		{
			Object.Destroy(self);
		}

		/// <summary>
		/// 直ちに破棄します.
		/// </summary>
		/// <param name="self">Object 自身.</param>
		public static void DestroyImmediate(this Object self)
		{
			Object.DestroyImmediate(self);
		}

		/// <summary>
		/// 存在したら破棄します.
		/// </summary>
		public static void TryDestroy(this Object self)
		{
			if (self) {
				Object.Destroy(self);
			}
		}

		/// <summary>
		/// 存在したら直ちに破棄します.
		/// </summary>
		public static void TryDestroyImmediate(this Object self)
		{
			if (self) {
				Object.DestroyImmediate(self);
			}
		}

		/// <summary>
		/// 新しいシーンを読み込む時に自動で破棄されないようにします.
		/// </summary>
		/// <param name="self">Object 自身.</param>
		public static void DontDestroyOnLoad(this Object self)
		{
			Object.DontDestroyOnLoad(self);
		}
	}
}

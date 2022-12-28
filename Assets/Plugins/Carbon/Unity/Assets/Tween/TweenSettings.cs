using System;
using UnityEngine;

namespace Carbon
{
	[Serializable]
	public class TweenSettings : ScriptableObject
	{
		/// <summary>
		/// Tween 方法
		/// </summary>
		public Tween.Method Method = Tween.Method.Linear;
		/// <summary>
		/// Tween スタイル
		/// </summary>
		public Tween.Style Style = Tween.Style.Once;
		/// <summary>
		/// RealTime (UnscaledTime) を使うフラグ. Time.timeScale 無視.
		/// </summary>
		public bool UsesRealTime = false;
		/// <summary>
		/// 開始遅延時間 (base) (秒)
		/// </summary>
		public float DelayBase = 0;
		/// <summary>
		/// 長さ (base) (秒)
		/// </summary>
		public float DurationBase = 1;
		/// <summary>
		/// Factor 変化曲線
		/// </summary>
		public AnimationCurve FactorCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));

		private void OnDestroy()
		{
			FactorCurve = null;
		}
	}
}

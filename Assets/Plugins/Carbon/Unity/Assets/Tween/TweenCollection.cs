using System;
using UnityEngine;

namespace Carbon
{
	[Serializable]
	public sealed class TweenCollection : IDisposable
	{
		[SerializeField]
		private TweenBase[] m_Tweens = new TweenBase[0];

		/// <summary>
		/// factor
		/// </summary>
		public float Factor {
			get {
				return m_Tweens[0].Factor;
			}
			set {
				foreach (var tw in m_Tweens) {
					tw.Factor = value;
				}
			}
		}

		public void Dispose()
		{
			m_Tweens = null;
		}

		/// <summary>
		/// リセット
		/// </summary>
		public void Reset(GameObject gameObject)
		{
			m_Tweens = gameObject.GetComponentsInChildren<TweenBase>();
		}

		/// <summary>
		/// リセット
		/// </summary>
		public void Reset(Component component)
		{
			m_Tweens = component.GetComponentsInChildren<TweenBase>();
		}

		/// <summary>
		/// 開始 (前進)
		/// </summary>
		public void BeginForward(Action onComplete = null)
		{
			Begin(true, onComplete);
		}

		/// <summary>
		/// 開始 (反転)
		/// </summary>
		public void BeginReverse(Action onComplete = null)
		{
			Begin(false, onComplete);
		}

		/// <summary>
		/// 開始
		/// </summary>
		/// <param name="isForward">前進フラグ</param>
		public void Begin(bool isForward, Action onComplete)
		{
			ParallelTask task = new ParallelTask();

			foreach (var tw in m_Tweens) {
				task.Push(next => tw.Begin(isForward, next));
			}

			task.Process(onComplete);
		}

		/// <summary>
		/// 開始状態までリセットします. 動作中断/開始しません. 'Begin()'後有効.
		/// </summary>
		public void ResetToBeginning()
		{
			foreach (var tw in m_Tweens) {
				tw.ResetToBeginning();
			}
		}

		/// <summary>
		/// スキップします. 'Begin()'後有効.
		/// </summary>
		public void Skip()
		{
			foreach (var tw in m_Tweens) {
				tw.Skip();
			}
		}

		/// <summary>
		/// 動作中断
		/// </summary>
		public void Pause()
		{
			foreach (var tw in m_Tweens) {
				tw.Pause();
			}
		}

		/// <summary>
		/// 動作再開
		/// </summary>
		public void Resume()
		{
			foreach (var tw in m_Tweens) {
				tw.Resume();
			}
		}

		/// <summary>
		/// 現在 Factor を反映します.
		/// </summary>
		public void Sample()
		{
			foreach (var tw in m_Tweens) {
				tw.Sample();
			}
		}
	}
}

using Carbon;
using System;
using UnityEngine;

namespace Shock.Scenes.LiveResult
{
	/// <summary>
	/// 数値カウンターUI
	/// </summary>
	[DisallowMultipleComponent]
	public sealed class NumCounterUI : RectTransformBehaviour, ILiveResultPlayer
	{
		//============================================
		// メンバー変数(SerializeField)
		//============================================
		/// <summary>
		/// 数値 text
		/// </summary>
		[SerializeField] private TextMeshUI m_NumText = null;
		/// <summary>
		/// SE再生するか？
		/// </summary>
		[SerializeField] private bool m_IsPlaySE = true;
		/// <summary>
		/// 0埋めるか？
		/// </summary>
		[SerializeField] private bool m_IsStaff = false;
		/// <summary>
		/// スコアの先頭の0のカラー値
		/// </summary>
		[SerializeField] private Color m_ZeroColor = Color.white;
		/// <summary>
		/// スコアのカラー値
		/// </summary>
		[SerializeField] private Color m_ScoreColor = Color.white;
		/// <summary>
		/// 桁数
		/// </summary>
		[SerializeField] private int m_TextLength = 4;
		/// <summary>
		/// Value tweener
		/// </summary>
		[SerializeField] private TweenValue m_TweenValue = null;

		//============================================
		// メンバー変数
		//============================================
		/// <summary>
		/// 初期値
		/// </summary>
		private int m_NumberFrom = 0;
		/// <summary>
		/// 目的値
		/// </summary>
		private int m_NumberTo = 0;
		/// <summary>
		/// 終了コールバック
		/// </summary>
		private Action m_OnCountComplete = null;
		/// <summary>
		/// 表示フォーマット
		/// </summary>
		private string m_TextFormat = "{0}";

		//--------------------------------------------
		// プロパティ
		//--------------------------------------------
		/// <summary>
		/// カウント数
		/// </summary>
		public int NumberTo { get { return m_NumberTo; } }
		/// <summary>
		/// 毎更新時の値
		/// </summary>
		public Action<int> OnUpdateValue = null;

		//--------------------------------------------
		// Monobehaviour
		//--------------------------------------------
		private void Start()
		{
			m_TweenValue = gameObject.GetComponent<TweenValue>();
			if (!m_TweenValue) {
				m_TweenValue = gameObject.AddComponent<TweenValue>();
				m_TweenValue.DurationBase = 0.5f;
			}
		}

		/// <summary>
		/// OnDestroy
		/// </summary>
		private void OnDestroy()
		{
			m_NumText = null;
			m_OnCountComplete = null;
			OnUpdateValue = null;
		}

		//--------------------------------------------
		// public
		//--------------------------------------------
		/// <summary>
		/// SE 再生 ON/OFF
		/// </summary>
		public void SetPlaySE(bool value)
		{
			m_IsPlaySE = value;
		}

		/// <summary>
		/// 表示フォーマット
		/// </summary>
		public void SetFormat(string format)
		{
			m_TextFormat = format;
		}

		/// <summary>
		/// 目標値セット
		/// </summary>
		public void SetTargetNum(int numberTo, int numberFrom = 0)
		{
			m_NumberTo = numberTo;
			m_NumberFrom = numberFrom;
			UpdateValue(m_NumberFrom);
		}

		public void SetScoreColor(Color color)
		{
			m_ScoreColor = color;
		}

		/// <summary>
		/// 再生遅延 (秒)
		/// </summary>
		public void SetPlayDelay(float delaySecond)
		{
			if (m_TweenValue) {
				m_TweenValue.DelayOffset = delaySecond;
			}
		}

		/// <summary>
		/// 再生
		/// </summary>
		public void Play(Action onEnded)
		{
			StartAutoCount(m_NumberTo, onEnded);
		}

		/// <summary>
		/// スキップ
		/// </summary>
		public void Skip()
		{
			if (m_TweenValue)
			{
				m_TweenValue.Skip();
			}
		}

		/// <summary>
		/// 最終段階を再生する
		/// </summary>
		public void PlayFinalState()
		{
			UpdateValue(m_NumberTo);
		}

		//--------------------------------------------
		// private
		//--------------------------------------------
		private void StartAutoCount(int score, Action onComplete)
		{
			if (score <= 0) {
				UpdateValue(m_NumberTo);
				onComplete.Call();
				return;
			}

			if (!m_TweenValue)
			{
				m_TweenValue = gameObject.DemandComponent<TweenValue>();
				m_TweenValue.DurationBase = 0.5f;
			}

			m_OnCountComplete = onComplete;

			//var tweenNumberPlaybackHDL = new SoundManager.PlaybackHandle();

			//m_TweenValue.From = 0;
			//m_TweenValue.To = 1;
			//m_TweenValue.OnTweenBegin = () => {
			//	if (m_IsPlaySE) {
			//		tweenNumberPlaybackHDL.SetupWithSoundKey(SoundKey.LiveResultScene.Se.TweenNumber);
			//		tweenNumberPlaybackHDL.Play(SoundManager.CreateSeOption(isLoop: true));
			//	}
			//};
			m_TweenValue.OnTweening = () =>
			{
				var value = (int)Mathf.Lerp(m_NumberFrom, m_NumberTo, m_TweenValue.Value);
				UpdateValue(value);
			};
			m_TweenValue.BeginForward(() => {
				if (m_IsPlaySE) {
					//tweenNumberPlaybackHDL.Stop();
				}
				UpdateValue(m_NumberTo); // avoid calc error
				ActionUtils.CallOnce(ref m_OnCountComplete);
			});
			m_TweenValue.ResetToBeginning();
		}

		/// <summary>
		/// 値更新
		/// </summary>
		private void UpdateValue(int score)
		{
			// 文字詰めしない場合
			if (!m_IsStaff) {
				if (score <= 0) {
					SetNumText("0");
					OnUpdateValue.Call(score);
					return;
				}
				SetNumText(score.ToString());
				OnUpdateValue.Call(score);
				return;
			}
			// 文字詰め時 ---------------------------
			if (score <= 0) {
				SetNumText(ColorUtils.ColorToRGBTagFormat(m_ZeroColor).AsFormat(string.Empty.PadRight(m_TextLength, '0')));
				OnUpdateValue.Call(score);
				return;
			}
			// スコアの先頭に付ける0の数
			var pointText = score.ToString();
			var zeroCount = Math.Min(m_TextLength, m_TextLength - pointText.Length);

			// スコアの先頭の0以外の文字列
			var scoreText = ColorUtils.ColorToRGBTagFormat(m_ScoreColor).AsFormat(score);
			var totalWidth = zeroCount + scoreText.Length;
			var text = ColorUtils.ColorToRGBTagFormat(m_ZeroColor).AsFormat(scoreText.PadLeft(totalWidth, '0'));
			SetNumText(text);
			OnUpdateValue.Call(score);
		}

		/// <summary>
		/// 数値文字列をフォーマットに適用
		/// </summary>
		private void SetNumText(string value)
		{
			m_NumText.SetText(ColorUtils.ColorToRGBTagFormat(m_ScoreColor).AsFormat(m_TextFormat.AsFormat(value)));
		}
	}
}
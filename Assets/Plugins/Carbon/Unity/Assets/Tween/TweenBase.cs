using System;
using UnityEngine;

namespace Carbon
{
	/// <summary>
	/// TweenBase. Base of tween-component.
	/// </summary>
	public abstract class TweenBase : CarbonBehaviour, IUpdateRoutine
	{
		//======================================================================================================================
		// Definition
		//======================================================================================================================
		/// <summary>
		/// Mathf.PI * 2f
		/// </summary>
		protected const float k2PI = Mathf.PI * 2f;

		//======================================================================================================================
		// Field
		//======================================================================================================================
		/// <summary>
		/// Settings
		/// </summary>
		[SerializeField, HideInInspector]
		private TweenSettings m_Settings = null;
		/// <summary>
		/// Tween 方法
		/// </summary>
		[SerializeField, HideInInspector]
		private Tween.Method m_Method = Tween.Method.Linear;
		/// <summary>
		/// Tween スタイル
		/// </summary>
		[SerializeField, HideInInspector]
		private Tween.Style m_Style = Tween.Style.Once;
		/// <summary>
		/// Factor 変化曲線
		/// </summary>
		[SerializeField, HideInInspector]
		private AnimationCurve m_FactorCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));
		/// <summary>
		/// 開始遅延時間 base (秒)
		/// </summary>
		[SerializeField, HideInInspector]
		private float m_DelayBase = 0;
		/// <summary>
		/// 開始遅延時間 offset (秒)
		/// </summary>
		[SerializeField, HideInInspector]
		private float m_DelayOffset = 0;
		/// <summary>
		/// 残り遅延時間
		/// </summary>
		private float m_DelayRemain = 0;
		/// <summary>
		/// 長さ base (秒)
		/// </summary>
		[SerializeField, HideInInspector]
		private float m_DurationBase = 1;
		/// <summary>
		/// 長さ offset (秒)
		/// </summary>
		[SerializeField, HideInInspector]
		private float m_DurationOffset = 0;
		/// <summary>
		/// RealTime (UnscaledTime) を使うフラグ. Time.timeScale 無視.
		/// </summary>
		[SerializeField, HideInInspector]
		private bool m_UsesRealTime = false;
		/// <summary>
		/// Time.SmoothDeltaTime を Time.deltaTime の代わりに使うフラグ. m_UsesRealTime が true の場合は無視される.
		/// <para>TweenBase.Start() 後変更不可.</para>
		/// </summary>
		[SerializeField, HideInInspector]
		private bool m_UpdatesSmoothly = true;
		/// <summary>
		/// OnEnable 時オート再生フラグ.
		/// </summary>
		[SerializeField, HideInInspector]
		private bool m_BeginsOnEnable = false;
		/// <summary>
		/// TimeScale. 特別な理由がなければ触らないでください.
		/// </summary>
		private float m_TimeScale = 1f;
		/// <summary>
		/// Tween 進行因子
		/// </summary>
		private float m_Factor = 0;
		/// <summary>
		/// 秒間因子変化量
		/// </summary>
		private float m_FactorDeltaPerSecond = 0;
		/// <summary>
		/// 初回更新フラグ
		/// </summary>
		private bool m_IsFirstUpdate = false;
		/// <summary>
		/// 開始コールバック
		/// </summary>
		private Action m_OnTweenBegin = null;
		/// <summary>
		/// 再生中コールバック
		/// </summary>
		private Action m_OnTweening = null;
		/// <summary>
		/// 終了コールバック
		/// </summary>
		private Action m_OnTweenEnd = null;
		/// <summary>
		/// 終了コールバック (一回きり)
		/// </summary>
		private Action m_OnTweenEndOnce = null;

		//======================================================================================================================
		// Property
		//======================================================================================================================
		/// <summary>
		/// Settings
		/// </summary>
		public TweenSettings Settings {
			get { return m_Settings; }
		}
		/// <summary>
		/// Tween 方法
		/// </summary>
		public Tween.Method Method {
			get { return m_Method; }
			set { m_Method = value; }
		}
		/// <summary>
		/// Tween スタイル
		/// </summary>
		public Tween.Style Style {
			get { return m_Style; }
			set { m_Style = value; }
		}
		/// <summary>
		/// OnEnable 時オート再生フラグ.
		/// </summary>
		public bool BeginsOnEnable {
			get { return m_BeginsOnEnable; }
			set { m_BeginsOnEnable = value; }
		}
		/// <summary>
		/// RealTime (UnscaledTime) を使うフラグ. Time.timeScale 無視.
		/// </summary>
		public bool UsesRealTime {
			get { return m_UsesRealTime; }
			set { m_UsesRealTime = value; }
		}
		/// <summary>
		/// RealTime (UnscaledTime) を使うフラグ. Time.timeScale 無視.
		/// </summary>
		public bool UpdatesSmoothly {
			get { return m_UpdatesSmoothly; }
			set { m_UpdatesSmoothly = value; }
		}
		/// <summary>
		/// 開始遅延時間 base (秒)
		/// </summary>
		public float DelayBase {
			get { return m_DelayBase; }
			set { m_DelayBase = value; }
		}
		/// <summary>
		/// 開始遅延時間 offset (秒)
		/// </summary>
		public float DelayOffset {
			get { return m_DelayOffset; }
			set { m_DelayOffset = value; }
		}
		/// <summary>
		/// 開始遅延時間 (秒)
		/// </summary>
		public float Delay {
			get { return m_DelayBase + m_DelayOffset; }
		}
		/// <summary>
		/// 長さ base (秒)
		/// </summary>
		public float DurationBase {
			get { return m_DurationBase; }
			set { m_DurationBase = value; }
		}
		/// <summary>
		/// 長さ offset (秒)
		/// </summary>
		public float DurationOffset {
			get { return m_DurationOffset; }
			set { m_DurationOffset = value; }
		}
		/// <summary>
		/// 長さ (秒)
		/// </summary>
		public float Duration {
			get { return m_DurationBase + m_DurationOffset; }
		}
		/// <summary>
		/// TimeScale. 特別な理由がなければ触らないでください.
		/// </summary>
		public float TimeScale {
			get { return m_TimeScale; }
			set { m_TimeScale = value; }
		}
		/// <summary>
		/// Tween 進行因子
		/// </summary>
		public float Factor {
			get { return m_Factor; }
			set { m_Factor = Mathf.Clamp01(value); }
		}
		/// <summary>
		/// 秒間 Factor 変化量.
		/// </summary>
		public float FactorDeltaPerSecond {
			get { return m_FactorDeltaPerSecond; }
			set { m_FactorDeltaPerSecond = value; }
		}
		/// <summary>
		/// Factor 変化曲線
		/// </summary>
		public AnimationCurve FactorCurve {
			get { return m_FactorCurve; }
			set { m_FactorCurve = value; }
		}
		/// <summary>
		/// Tween 開始コールバック
		/// </summary>
		public Action OnTweenBegin {
			set { m_OnTweenBegin = value; }
		}
		/// <summary>
		/// Tween 中コールバック
		/// </summary>
		public Action OnTweening {
			set { m_OnTweening = value; }
		}
		/// <summary>
		/// Tween 終了コールバック
		/// </summary>
		public Action OnTweenEnd {
			set { m_OnTweenEnd += value; }
		}
		/// <summary>
		/// Tween 再生中
		/// </summary>
		public bool IsTweening {
			get { return m_FactorDeltaPerSecond != 0; }
		}
		/// <summary>
		/// 前進中
		/// </summary>
		public bool IsTweeningForward {
			get { return m_FactorDeltaPerSecond > 0; }
		}
		/// <summary>
		/// 後退中
		/// </summary>
		public bool IsTweeningReverse {
			get { return m_FactorDeltaPerSecond < 0; }
		}
		/// <summary>
		/// 遅延中
		/// </summary>
		public bool IsDelaying {
			get { return IsTweening && m_DelayRemain > 0; }
		}
		/// <summary>
		/// 停止状態
		/// </summary>
		public bool IsIdle {
			get { return m_FactorDeltaPerSecond == 0; }
		}

		//======================================================================================================================
		// CarbonBehaviour
		//======================================================================================================================
		protected virtual void DoOnDestroy() { }
		private void OnDestroy()
		{
			DoOnDestroy();

			m_OnTweenBegin = null;
			m_OnTweening = null;
			m_OnTweenEnd = null;
			m_OnTweenEndOnce = null;

			m_FactorCurve = null;
			m_Settings = null;
		}

		protected virtual void DoOnEnable() { }
		private void OnEnable()
		{
			if (m_BeginsOnEnable) {
				if (m_FactorDeltaPerSecond == 0) {
					Begin(true, null);
				}
			}

			DoOnEnable();
		}

		protected virtual void DoStart() { }
		private void Start()
		{
			if (m_UpdatesSmoothly) {
				RoutineManager.UpdateSmooth(this);
			}
			else {
				RoutineManager.Update(this);
			}

			DoStart();
		}

		void IUpdateRoutine.OnUpdate(float deltaTime)
		{
			if (m_FactorDeltaPerSecond == 0) {
				return;
			}

			if (m_UsesRealTime) {
				deltaTime = RealTime.deltaTime;
			}
			deltaTime *= m_TimeScale;

			// 遅延時間減算
			m_DelayRemain -= deltaTime;
			if (m_DelayRemain > 0) {
				return;
			}

			if (m_IsFirstUpdate) {
				m_IsFirstUpdate = false;
				// callback
				m_OnTweenBegin.Call();
			}

			// factor 変化値
			float deltaFactor = (Duration > 0)
				? m_FactorDeltaPerSecond * deltaTime
				: m_FactorDeltaPerSecond;

			// factor 加算
			m_Factor += deltaFactor;

			// pre-process
			if (m_Style == Tween.Style.Loop) {
				if (m_Factor > 1f) {
					m_Factor -= Mathf.Floor(m_Factor);
					// Ex: 1.1 --> 0.1
				}
			}
			else
			if (m_Style == Tween.Style.PingPong) {
				if (m_Factor > 1f) {
					m_Factor = 1f - (m_Factor - Mathf.Floor(m_Factor));
					m_FactorDeltaPerSecond = -m_FactorDeltaPerSecond;
					// Ex: 
					//   Factor: 1.1 --> 0.9
					//   FactorDeltaPerSecond: 0.1 --> -0.1
				}
				else
				if (m_Factor < 0f) {
					m_Factor = -m_Factor;
					m_Factor -= Mathf.Floor(m_Factor);
					m_FactorDeltaPerSecond = -m_FactorDeltaPerSecond;
					// Ex: 
					//   Factor: -0.1 --> 0.1
					//   FactorDeltaPerSecond: -0.1 --> 0.1
				}
			}

			// 終了チェック
			bool isEnd = false;

			if (m_Style == Tween.Style.Once) {
				if (m_Factor <= 0) {
					m_Factor = 0;
					isEnd = true;
				}
				else
				if (m_Factor >= 1) {
					m_Factor = 1;
					isEnd = true;
				}
			}

			// Factor を反映
			Sample();

			// 終了
			if (isEnd) {
				// 停止
				enabled = false;
				// delta リセット
				m_FactorDeltaPerSecond = 0;
				// コールバック
				m_OnTweenEnd.Call();
				// コールバック (一回きり)
				ActionUtils.CallOnce(ref m_OnTweenEndOnce);
			}
			// 進行中
			else {
				// コールバック
				m_OnTweening.Call();
			}
		}

		//======================================================================================================================
		// Public Method
		//======================================================================================================================
		/// <summary>
		/// Settings を適用します.
		/// </summary>
		/// <param name="settings">Settings</param>
		public void ApplySettings(TweenSettings settings)
		{
			m_Settings = settings;

			if (!settings) {
				return;
			}

			m_Method       = m_Settings.Method;
			m_Style        = m_Settings.Style;
			m_UsesRealTime = m_Settings.UsesRealTime;
			m_DelayBase    = m_Settings.DelayBase;
			m_DurationBase = m_Settings.DurationBase;
			m_FactorCurve  = m_Settings.FactorCurve;
		}

		/// <summary>
		/// Settings 再適用します.
		/// </summary>
		public void ReapplySettings()
		{
			ApplySettings(m_Settings);
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
			// register callback
			m_OnTweenEndOnce = onComplete;

			// delta
			m_FactorDeltaPerSecond = ((Duration > 0) ? (1f / Duration) : 1f) * (isForward ? +1f : -1f);

			// record delay time
			m_DelayRemain = Delay;

			// flag
			m_IsFirstUpdate = true;

			// 起動
			enabled = true;
		}

		/// <summary>
		/// 開始状態までリセットします. 動作中断/開始しません.
		/// </summary>
		public void ResetToBeginning()
		{
			m_Factor = (m_FactorDeltaPerSecond < 0) ? 1f : 0f;

			Sample();
		}

		/// <summary>
		/// スキップします. 'Begin()'後有効.
		/// </summary>
		public void Skip()
		{
			m_Factor = (m_FactorDeltaPerSecond > 0) ? 1 : 0;
			m_DelayRemain = 0;
		}

		/// <summary>
		/// 動作中断
		/// </summary>
		public void Pause()
		{
			enabled = false;
		}

		/// <summary>
		/// 動作再開
		/// </summary>
		public void Resume()
		{
			enabled = true;
		}

		/// <summary>
		/// 停止
		/// </summary>
		public void Stop()
		{
			enabled = false;
			m_FactorDeltaPerSecond = 0;
		}

		/// <summary>
		/// Factor でサンプリング
		/// </summary>
		/// <param name="factor">Tween factor</param>
		public void Sample()
		{
			// Calculate the sampling value
			float progress = Mathf.Clamp01(m_Factor);

			switch (m_Method) {
				case Tween.Method.EaseIn: {
						progress = 1f - Mathf.Sin(0.5f * Mathf.PI * (1f - progress));
					} break;

				case Tween.Method.EaseOut: {
						progress = 1f - Mathf.Sin(0.5f * Mathf.PI * (1f - progress));
					} break;

				case Tween.Method.EaseInOut: {
						progress = progress - Mathf.Sin(progress * k2PI) / k2PI;
					} break;

				case Tween.Method.BounceIn: {
						progress = CalculateBounceValue(progress);
					} break;

				case Tween.Method.BounceOut: {
						progress = 1f - CalculateBounceValue(1f - progress);
					} break;
			}

			// Call the virtual update
			UpdateValue((m_FactorCurve != null) ? m_FactorCurve.Evaluate(progress) : progress);
		}

		/// <summary>
		/// 現在値を開始値 (From) として設定します.
		/// </summary>
		public virtual void SetBeginFromCurrentValue() { }

		/// <summary>
		/// 現在値を終了値 (To) として設定します.
		/// </summary>
		public virtual void SetEndToCurrentValue() { }

		//======================================================================================================================
		// Protected Method
		//======================================================================================================================
		/// <summary>
		/// Update value
		/// </summary>
		/// <param name="progress">Progress value</param>
		protected abstract void UpdateValue(float progress);

		//======================================================================================================================
		// Private Method
		//======================================================================================================================
		/// <summary>
		/// Bounce Value 算出 (from NGUI)
		/// </summary>
		private float CalculateBounceValue(float factor)
		{
			float result = 0;

			if (factor < 0.363636f) // 0.363636 = (1/ 2.75)
			{
				result = 7.5685f * factor * factor;
			}
			else if (factor < 0.727272f) // 0.727272 = (2 / 2.75)
			{
				result = 7.5625f * (factor -= 0.545454f) * factor + 0.75f; // 0.545454f = (1.5 / 2.75) 
			}
			else if (factor < 0.909090f) // 0.909090 = (2.5 / 2.75) 
			{
				result = 7.5625f * (factor -= 0.818181f) * factor + 0.9375f; // 0.818181 = (2.25 / 2.75) 
			}
			else {
				result = 7.5625f * (factor -= 0.9545454f) * factor + 0.984375f; // 0.9545454 = (2.625 / 2.75) 
			}

			return result;
		}
	}
}
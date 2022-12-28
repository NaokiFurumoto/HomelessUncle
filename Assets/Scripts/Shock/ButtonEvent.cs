using Carbon;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shock
{
	/// <summary>
	/// ボタンイベント
	/// </summary>
	[RequireComponent(typeof(RectTransform))]
	[DisallowMultipleComponent]
	public class ButtonEvent : RectTransformBehaviour
		, IPointerClickHandler
		, IPointerDownHandler
		, IPointerUpHandler
		, IPointerExitHandler
		, IBeginDragHandler
		, IDragHandler
		, IEndDragHandler
	{
		//======================================================================================================
		// Definition
		//======================================================================================================
		private const string MASTER_TRIGGER_NAME = "IsMaster";
		private const string APPEAL_TRIGGER_NAME = "Appeal";
		//======================================================================================================
		// Static
		//======================================================================================================
		private static int? sm_MasterPointerId = null;

		//======================================================================================================
		// Definition
		//======================================================================================================
		/// <summary>
		/// コールバック
		/// </summary>
		private class Callback
		{
			//==========================
			// Field
			//==========================
			public Action Every;
			public Action Once;

			//==========================
			// Method
			//==========================
			/// <summary>
			/// 破棄処理
			/// </summary>
			public void Dispose()
			{
				Every = null;
				Once = null;
			}
			/// <summary>
			/// 呼び出し
			/// </summary>
			public void Call()
			{
				Every.Call();
				ActionUtils.CallOnce(ref Once);
			}
			/// <summary>
			/// コールバックが登録されているか
			/// </summary>
			public bool Any()
			{
				return Every != null || Once != null;
			}
		}

		/// <summary>
		/// 長押し発生時間閾値(秒)
		/// </summary>
		private const float LONG_PRESS_TIME_THRESHOLD = 1f;

		//======================================================================================================
		// SerializeField
		//======================================================================================================
		[SerializeField] private ButtonSeKey m_PressDownSound = ButtonSeKey.None;
		[SerializeField] private ButtonSeKey m_ClickSound     = ButtonSeKey.Decide00;
		[SerializeField] private ButtonSeKey m_LongPressSound = ButtonSeKey.Decide00;

		//======================================================================================================
		// Field
		//======================================================================================================
		/// <summary>
		/// 押下時コールバック
		/// </summary>
		private Callback m_OnPressDown = new Callback();
		/// <summary>
		/// Clickコールバック
		/// </summary>
		private Callback m_OnClick = new Callback();
		/// <summary>
		/// 長押しコールバック
		/// </summary>
		private Callback m_OnLongPress = new Callback();
		/// <summary>
		/// 長押しタイマー
		/// </summary>
		private IDisposable m_LongPressTimer = null;
		/// <summary>
		/// Click許可フラグ
		/// </summary>
		private int? m_PointerId = null;
		/// <summary>
		/// 入力受付条件
		/// </summary>
		private Func<bool> m_Condition = () => true;
		/// <summary>
		/// ボタンアニメーター
		/// </summary>
		private Animator m_ButtonAnimator = null;
		/// <summary>
		/// ボタンアニメーター取得済みフラグ
		/// </summary>
		private bool m_HasFetchedButtonAnimator = false;

		//======================================================================================================
		// Property
		//======================================================================================================
		/// <summary>
		/// 押下コールバック
		/// </summary>
		public Action OnPressDown {
			get { return m_OnPressDown.Every; }
			set { m_OnPressDown.Every = value; }
		}
		/// <summary>
		/// 押下コールバック (一回きり)
		/// </summary>
		public Action OnPressDownOnce {
			get { return m_OnPressDown.Once; }
			set { m_OnPressDown.Once = value; }
		}
		/// <summary>
		/// Click コールバック
		/// </summary>
		public Action OnClick {
			get { return m_OnClick.Every; }
			set { m_OnClick.Every = value; }
		}
		/// <summary>
		/// Click コールバック (一回きり)
		/// </summary>
		public Action OnClickOnce {
			get { return m_OnClick.Once; }
			set { m_OnClick.Once = value; }
		}
		/// <summary>
		/// 長押しコールバック
		/// </summary>
		public Action OnLongPress {
			get { return m_OnLongPress.Every; }
			set { m_OnLongPress.Every = value; }
		}
		/// <summary>
		/// 長押しコールバック (一回きり)
		/// </summary>
		public Action OnLongPressOnce {
			get { return m_OnLongPress.Once; }
			set { m_OnLongPress.Once = value; }
		}

		//======================================================================================================
		// MonoBehaviour
		//======================================================================================================
		private void OnDestroy()
		{
			m_ButtonAnimator = null;

			if (m_OnPressDown != null) {
				m_OnPressDown.Dispose();
				m_OnPressDown = null;
			}

			if (m_OnClick != null) {
				m_OnClick.Dispose();
				m_OnClick = null;
			}

			if (m_OnLongPress != null) {
				m_OnLongPress.Dispose();
				m_OnLongPress = null;
			}

			m_Condition = null;
		}

		private void OnDisable()
		{
			CancelClick();
			CancelLongPress();
		}

		//======================================================================================================
		// Public Method for Interface
		//======================================================================================================
		/// <summary>
		/// クリックされた時のイベント
		/// </summary>
		public void OnPointerClick(PointerEventData eventData)
		{
			//DebugUtils.Log("OnPointerClick");
			if (eventData.pointerId != m_PointerId)
			{
				return;
			}

			m_PointerId = null;

			if (enabled && m_Condition.Call())
			{
				if (m_OnClick.Any())
				{
					m_OnClick.Call();
					PlaySe(m_ClickSound);
				}
			}
		}

		/// <summary>
		/// 押した時のイベント
		/// </summary>
		public void OnPointerDown(PointerEventData eventData)
		{
			//DebugUtils.Log("OnPointerDown");
			if (!m_Condition.Call()) {
				return;
			}

			if (sm_MasterPointerId.HasValue) {
				if (sm_MasterPointerId.Value == eventData.pointerId) {
					DebugUtils.Warning("ありえん {0}", eventData.pointerId);
				}
				return;
			}

			if (m_HasFetchedButtonAnimator == false) {
				m_HasFetchedButtonAnimator = true;
				var button = GetComponent<Button>();
				if (button) {
					m_ButtonAnimator = button.animator;
				}
			}
			if (m_ButtonAnimator) {
				m_ButtonAnimator.SetBool(MASTER_TRIGGER_NAME, true);
			}

			sm_MasterPointerId	= eventData.pointerId;
			m_PointerId			= eventData.pointerId;

			m_LongPressTimer = Observable
				.Timer(TimeSpan.FromSeconds(LONG_PRESS_TIME_THRESHOLD))
				.Subscribe(_ => {
					if (enabled
					&&	sm_MasterPointerId.HasValue
					&&	sm_MasterPointerId.Value == m_PointerId
					&&	m_Condition.Call()
					&&	m_OnLongPress.Any()
					) {
						//DebugUtils.Log("OnLongPress");
						m_OnLongPress.Call();
						PlaySe(m_LongPressSound);
					}

					CancelClick();
					CancelLongPress();
				})
				.AddTo(this);

			m_OnPressDown.Call();
			PlaySe(m_PressDownSound);
		}

		/// <summary>
		/// 放した時のイベント
		/// </summary>
		public void OnPointerUp(PointerEventData eventData)
		{
			//DebugUtils.Log("OnPointerUp");
			if (eventData.pointerId != m_PointerId)
			{
				return;
			}

			// 処理順 OnPointerDown -> OnPointerUp -> OnPointerClick のため,
			// 潜在クリックの場合は CancelClick() で消失した Pointer ID を戻して, OnPointerClick で照合可能にする.
			if (CancelClick())
			{
				m_PointerId = eventData.pointerId;
			}
			CancelLongPress();
		}

		/// <summary>
		/// タップ領域が離れたときに呼ばれるイベント
		/// </summary>
		public void OnPointerExit(PointerEventData eventData)
		{
			if (eventData.pointerId != m_PointerId)
			{
				return;
			}

			CancelClick();
			CancelLongPress();
		}

		/// <summary>
		/// ドラッグ発生したときに呼ばれるイベント
		/// ドラッグの閾値はEventSystemのDragThreshold（CoreSystemSceneにあるEventSystemで設定）
		/// </summary>
		public void OnBeginDrag(PointerEventData eventData)
		{
			//DebugUtils.Log("OnBeginDrag");
			if (eventData.pointerId != m_PointerId)
			{
				return;
			}

			CancelClick();
			CancelLongPress();
		}

		/// <summary>
		/// ドラッグ中
		/// </summary>
		/// <param name="eventData"></param>
		public void OnDrag(PointerEventData eventData)
		{
			//DebugUtils.Log("OnDrag");
		}

		/// <summary>
		/// ドラッグ終了
		/// </summary>
		/// <param name="eventData"></param>
		public void OnEndDrag(PointerEventData eventData)
		{
			//DebugUtils.Log("OnEndDrag");
		}

		//======================================================================================================
		// Public Method
		//======================================================================================================
		/// <summary>
		/// クリック挙動をシミュレートする
		/// </summary>
		public void SimulateClick()
		{
			if (!isActiveAndEnabled)
			{
				return;
			}

			if (m_PointerId != null)
			{
				return;
			}

			var dummyPointerEventData = new PointerEventData(EventSystem.current);
			dummyPointerEventData.pointerId = int.MinValue;
			OnPointerDown(dummyPointerEventData);
			OnPointerUp(dummyPointerEventData);
			OnPointerClick(dummyPointerEventData);
		}

		/// <summary>
		/// ボタン入力受付条件を設定
		/// </summary>
		public void SetConditions(params Func<bool>[] conditions)
		{
			m_Condition = () => conditions.All(c => c.Call());
		}

		/// <summary>
		/// ボタンの有効/無効を設定
		/// </summary>
		public void SetEnable(bool value)
		{
			enabled = value;

			var button = GetComponent<Button>();
			if (button) {
				button.enabled = value;
			}
		}

		/// <summary>
		/// グレーアウトを設定
		/// </summary>
		public void SetGrayout(bool value)
		{
			var grayoutUI = gameObject.DemandComponent<GrayoutUI>();
			if (grayoutUI) {
				grayoutUI.SetGrayout(value);
			}
		}

		/// <summary>
		/// アピールアニメーション
		/// </summary>
		public void SetEnableAppealAnimation(bool value)
		{
			var button = GetComponent<Button>();
			if (button) {
				var animator = button.GetComponent<Animator>();
				if (animator) {
					animator.SetBool(APPEAL_TRIGGER_NAME, value);
				}
			}
		}

		//======================================================================================================
		// Private Method
		//======================================================================================================
		/// <summary>
		/// クリックイベントキャンセル
		/// </summary>
		private bool CancelClick()
		{
			if (m_PointerId.HasValue == false)
			{
				return false;
			}

			int pointerId = m_PointerId.Value;
			m_PointerId = null;

			if (pointerId != sm_MasterPointerId)
			{
				return false;
			}

			sm_MasterPointerId = null;

			if (m_ButtonAnimator) {
				m_ButtonAnimator.SetBool(MASTER_TRIGGER_NAME, false);
			}

			return true;
		}

		/// <summary>
		/// 長押しイベントキャンセル
		/// </summary>
		private void CancelLongPress()
		{
			if (m_LongPressTimer != null) {
				m_LongPressTimer.Dispose();
				m_LongPressTimer = null;
			}
		}

		private void PlaySe(ButtonSeKey buttonSeKey)
		{
			if (buttonSeKey == ButtonSeKey.None) {
				return;
			}
			//SoundManager.PlaySe(ButtonSeSettings.GetSoundKey(buttonSeKey));
		}
	}
}

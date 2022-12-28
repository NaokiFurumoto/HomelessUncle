using Carbon;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ボタンイベントコンポーネント
/// </summary>
[RequireComponent(typeof(RectTransform))]
[DisallowMultipleComponent]
public class ButtonEventSystem : RectTransformBehaviour
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
	private static int? masterPointerId = null;

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
	[SerializeField] private ButtonSeKey pressDownSound = ButtonSeKey.None;
	[SerializeField] private ButtonSeKey clickSound = ButtonSeKey.Decide00;
	[SerializeField] private ButtonSeKey longPressSound = ButtonSeKey.Decide00;

	//======================================================================================================
	// Field
	//======================================================================================================
	/// <summary>
	/// 押下時コールバック
	/// </summary>
	private Callback onPressDown = new Callback();
	/// <summary>
	/// Clickコールバック
	/// </summary>
	private Callback onClick = new Callback();
	/// <summary>
	/// 長押しコールバック
	/// </summary>
	private Callback onLongPress = new Callback();
	/// <summary>
	/// 長押しタイマー
	/// </summary>
	private IDisposable longPressTimer = null;
	/// <summary>
	/// Click許可フラグ
	/// </summary>
	private int? pointerId = null;
	/// <summary>
	/// 入力受付条件
	/// </summary>
	private Func<bool> condition = () => true;
	/// <summary>
	/// ボタンアニメーター
	/// </summary>
	private Animator buttonAnimator = null;
	/// <summary>
	/// ボタンアニメーター取得済みフラグ
	/// </summary>
	private bool hasFetchedButtonAnimator = false;

	//======================================================================================================
	// Property
	//======================================================================================================
	/// <summary>
	/// 押下コールバック
	/// </summary>
	public Action OnPressDown
	{
		get { return onPressDown.Every; }
		set { onPressDown.Every = value; }
	}
	/// <summary>
	/// 押下コールバック (一回きり)
	/// </summary>
	public Action OnPressDownOnce
	{
		get { return onPressDown.Once; }
		set { onPressDown.Once = value; }
	}
	/// <summary>
	/// Click コールバック
	/// </summary>
	public Action OnClick
	{
		get { return onClick.Every; }
		set { onClick.Every = value; }
	}
	/// <summary>
	/// Click コールバック (一回きり)
	/// </summary>
	public Action OnClickOnce
	{
		get { return onClick.Once; }
		set { onClick.Once = value; }
	}
	/// <summary>
	/// 長押しコールバック
	/// </summary>
	public Action OnLongPress
	{
		get { return onLongPress.Every; }
		set { onLongPress.Every = value; }
	}
	/// <summary>
	/// 長押しコールバック (一回きり)
	/// </summary>
	public Action OnLongPressOnce
	{
		get { return onLongPress.Once; }
		set { onLongPress.Once = value; }
	}

	//======================================================================================================
	// MonoBehaviour
	//======================================================================================================
	private void OnDestroy()
	{
		buttonAnimator = null;

		if (OnPressDown != null)
		{
			onPressDown.Dispose();
			onPressDown = null;
		}

		if (OnClick != null)
		{
			onClick.Dispose();
			onClick = null;
		}

		if (OnLongPress != null)
		{
			onLongPress.Dispose();
			onLongPress = null;
		}

		condition = null;
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
		if (eventData.pointerId != pointerId)
		{
			return;
		}

		pointerId = null;

		if (enabled && condition.Call())
		{
			if (onClick.Any())
			{
				onClick.Call();
				PlaySe(clickSound);
			}
		}
	}

	/// <summary>
	/// 押した時のイベント
	/// </summary>
	public void OnPointerDown(PointerEventData eventData)
	{
		//DebugUtils.Log("OnPointerDown");
		if (!condition.Call())
		{
			return;
		}

		if (masterPointerId.HasValue)
		{
			if (masterPointerId.Value == eventData.pointerId)
			{
				DebugUtils.Warning("ありえん {0}", eventData.pointerId);
			}
			return;
		}

		if (hasFetchedButtonAnimator == false)
		{
			hasFetchedButtonAnimator = true;
			var button = GetComponent<Button>();
			if (button)
			{
				buttonAnimator = button.animator;
			}
		}
		if (buttonAnimator)
		{
			buttonAnimator.SetBool(MASTER_TRIGGER_NAME, true);
		}

		masterPointerId = eventData.pointerId;
		pointerId = eventData.pointerId;

		longPressTimer = Observable
			.Timer(TimeSpan.FromSeconds(LONG_PRESS_TIME_THRESHOLD))
			.Subscribe(_ => {
				if (enabled
				&& masterPointerId.HasValue
				&& masterPointerId.Value == pointerId
				&& condition.Call()
				&& onLongPress.Any()
				)
				{
						//DebugUtils.Log("OnLongPress");
						onLongPress.Call();
					PlaySe(longPressSound);
				}

				CancelClick();
				CancelLongPress();
			})
			.AddTo(this);

		onPressDown.Call();
		PlaySe(pressDownSound);
	}

	/// <summary>
	/// 放した時のイベント
	/// </summary>
	public void OnPointerUp(PointerEventData eventData)
	{
		//DebugUtils.Log("OnPointerUp");
		if (eventData.pointerId != pointerId)
		{
			return;
		}

		// 処理順 OnPointerDown -> OnPointerUp -> OnPointerClick のため,
		// 潜在クリックの場合は CancelClick() で消失した Pointer ID を戻して, OnPointerClick で照合可能にする.
		if (CancelClick())
		{
			pointerId = eventData.pointerId;
		}
		CancelLongPress();
	}

	/// <summary>
	/// タップ領域が離れたときに呼ばれるイベント
	/// </summary>
	public void OnPointerExit(PointerEventData eventData)
	{
		if (eventData.pointerId != pointerId)
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
		if (eventData.pointerId != pointerId)
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

		if (pointerId != null)
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
		condition = () => conditions.All(c => c.Call());
	}

	/// <summary>
	/// ボタンの有効/無効を設定
	/// </summary>
	public void SetEnable(bool value)
	{
		enabled = value;

		var button = GetComponent<Button>();
		if (button)
		{
			button.enabled = value;
		}
	}

	/// <summary>
	/// グレーアウトを設定
	/// </summary>
	public void SetGrayout(bool value)
	{
		var grayoutUI = gameObject.DemandComponent<UIGrayOut>();
		if (grayoutUI)
		{
			grayoutUI.SetGrayout(value);
		}
	}

	/// <summary>
	/// アピールアニメーション
	/// </summary>
	public void SetEnableAppealAnimation(bool value)
	{
		var button = GetComponent<Button>();
		if (button)
		{
			var animator = button.GetComponent<Animator>();
			if (animator)
			{
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
		if (pointerId.HasValue == false)
		{
			return false;
		}

		int _pointerId = pointerId.Value;
		pointerId = null;

		if (_pointerId != masterPointerId)
		{
			return false;
		}

		masterPointerId = null;

		if (buttonAnimator)
		{
			buttonAnimator.SetBool(MASTER_TRIGGER_NAME, false);
		}

		return true;
	}

	/// <summary>
	/// 長押しイベントキャンセル
	/// </summary>
	private void CancelLongPress()
	{
		if (longPressTimer != null)
		{
			longPressTimer.Dispose();
			longPressTimer = null;
		}
	}

	private void PlaySe(ButtonSeKey buttonSeKey)
	{
		if (buttonSeKey == ButtonSeKey.None)
		{
			return;
		}
		//SoundManager.PlaySe(ButtonSeSettings.GetSoundKey(buttonSeKey));
	}
}


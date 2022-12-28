using Carbon;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// �{�^���C�x���g�R���|�[�l���g
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
	/// �R�[���o�b�N
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
		/// �j������
		/// </summary>
		public void Dispose()
		{
			Every = null;
			Once = null;
		}
		/// <summary>
		/// �Ăяo��
		/// </summary>
		public void Call()
		{
			Every.Call();
			ActionUtils.CallOnce(ref Once);
		}
		/// <summary>
		/// �R�[���o�b�N���o�^����Ă��邩
		/// </summary>
		public bool Any()
		{
			return Every != null || Once != null;
		}
	}

	/// <summary>
	/// ��������������臒l(�b)
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
	/// �������R�[���o�b�N
	/// </summary>
	private Callback onPressDown = new Callback();
	/// <summary>
	/// Click�R�[���o�b�N
	/// </summary>
	private Callback onClick = new Callback();
	/// <summary>
	/// �������R�[���o�b�N
	/// </summary>
	private Callback onLongPress = new Callback();
	/// <summary>
	/// �������^�C�}�[
	/// </summary>
	private IDisposable longPressTimer = null;
	/// <summary>
	/// Click���t���O
	/// </summary>
	private int? pointerId = null;
	/// <summary>
	/// ���͎�t����
	/// </summary>
	private Func<bool> condition = () => true;
	/// <summary>
	/// �{�^���A�j���[�^�[
	/// </summary>
	private Animator buttonAnimator = null;
	/// <summary>
	/// �{�^���A�j���[�^�[�擾�ς݃t���O
	/// </summary>
	private bool hasFetchedButtonAnimator = false;

	//======================================================================================================
	// Property
	//======================================================================================================
	/// <summary>
	/// �����R�[���o�b�N
	/// </summary>
	public Action OnPressDown
	{
		get { return onPressDown.Every; }
		set { onPressDown.Every = value; }
	}
	/// <summary>
	/// �����R�[���o�b�N (��񂫂�)
	/// </summary>
	public Action OnPressDownOnce
	{
		get { return onPressDown.Once; }
		set { onPressDown.Once = value; }
	}
	/// <summary>
	/// Click �R�[���o�b�N
	/// </summary>
	public Action OnClick
	{
		get { return onClick.Every; }
		set { onClick.Every = value; }
	}
	/// <summary>
	/// Click �R�[���o�b�N (��񂫂�)
	/// </summary>
	public Action OnClickOnce
	{
		get { return onClick.Once; }
		set { onClick.Once = value; }
	}
	/// <summary>
	/// �������R�[���o�b�N
	/// </summary>
	public Action OnLongPress
	{
		get { return onLongPress.Every; }
		set { onLongPress.Every = value; }
	}
	/// <summary>
	/// �������R�[���o�b�N (��񂫂�)
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
	/// �N���b�N���ꂽ���̃C�x���g
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
	/// ���������̃C�x���g
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
				DebugUtils.Warning("���肦�� {0}", eventData.pointerId);
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
	/// ���������̃C�x���g
	/// </summary>
	public void OnPointerUp(PointerEventData eventData)
	{
		//DebugUtils.Log("OnPointerUp");
		if (eventData.pointerId != pointerId)
		{
			return;
		}

		// ������ OnPointerDown -> OnPointerUp -> OnPointerClick �̂���,
		// ���݃N���b�N�̏ꍇ�� CancelClick() �ŏ������� Pointer ID ��߂���, OnPointerClick �ŏƍ��\�ɂ���.
		if (CancelClick())
		{
			pointerId = eventData.pointerId;
		}
		CancelLongPress();
	}

	/// <summary>
	/// �^�b�v�̈悪���ꂽ�Ƃ��ɌĂ΂��C�x���g
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
	/// �h���b�O���������Ƃ��ɌĂ΂��C�x���g
	/// �h���b�O��臒l��EventSystem��DragThreshold�iCoreSystemScene�ɂ���EventSystem�Őݒ�j
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
	/// �h���b�O��
	/// </summary>
	/// <param name="eventData"></param>
	public void OnDrag(PointerEventData eventData)
	{
		//DebugUtils.Log("OnDrag");
	}

	/// <summary>
	/// �h���b�O�I��
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
	/// �N���b�N�������V�~�����[�g����
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
	/// �{�^�����͎�t������ݒ�
	/// </summary>
	public void SetConditions(params Func<bool>[] conditions)
	{
		condition = () => conditions.All(c => c.Call());
	}

	/// <summary>
	/// �{�^���̗L��/������ݒ�
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
	/// �O���[�A�E�g��ݒ�
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
	/// �A�s�[���A�j���[�V����
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
	/// �N���b�N�C�x���g�L�����Z��
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
	/// �������C�x���g�L�����Z��
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


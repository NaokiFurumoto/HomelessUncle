using UnityEngine;
using UnityEngine.EventSystems;
using Carbon;
using UniRx;
using System;

/// <summary>
/// ScrollRect�̃C�x���g�p���p�R���|�[�l���g
/// </summary>
[DisallowMultipleComponent]
public sealed class ScrollRectSystem : UnityEngine.UI.ScrollRect
{
	//============================================
	//! �萔
	//============================================
	private const int ElasticMoveTimeFrame = 30;

	//============================================
	//! �����o�[�ϐ�
	//============================================
	private IDisposable elasticTimer;
	private PointerEventData currentPointerEventData = null;

	//============================================
	//! �v���p�e�B
	//============================================
	public bool IsDrag { get; private set; }
	public bool IsElasticMove { get; private set; }
	public bool IsHorizontal { get { return horizontal; } } // �������ɃX�N���[������̂�
	public bool IsVertical { get { return vertical; } } // �c�����ɃX�N���[������̂�
	public float Velocity { get { return Mathf.Abs(IsHorizontal ? velocity.x : velocity.y); } } // �����x
	private bool IsElastic { get { return movementType == MovementType.Elastic; } } // Elastic�^�C�v�Ȃ̂�
	public float NormalizedPosition { get { return IsVertical ? verticalNormalizedPosition : horizontalNormalizedPosition; } }
	//============================================
	//! �R�[���o�b�N�@:�@�Z�b�g�͊O���B�擾�͓���
	//============================================
	public Action BeginDrag { private get; set; }

	//--------------------------------------------
	// Monobehaviour
	//--------------------------------------------
	/// <summary>
	/// Reset�����F�^�ɂ���ČĂяo����郁�\�b�h��ς���I�Inew
	/// </summary>
	new void Reset()
	{
		// �Ƃ肠�����ǂ������̃p�����[�^����Ă���
		movementType = MovementType.Unrestricted;
		decelerationRate = 0.2f;
		scrollSensitivity = 7f;

		// �R���|�[���l���g�ݒ�
		var scrollContent = GetComponentInChildren<ScrollContentUI>();
		if (scrollContent == null)
		{
			return;
		}
		content = scrollContent.GetComponent<RectTransform>();
	}

	/// <summary>
	/// �h���b�O�J�n���ɌĂ΂��C�x���g
	/// </summary>
	public override void OnBeginDrag(PointerEventData eventData)
	{
		currentPointerEventData = eventData;
		base.OnBeginDrag(eventData);
		BeginDrag.Call();
		IsDrag = true;
		IsElasticMove = false;

		elasticTimer.TryDispose();
	}

	/// <summary>
	/// �h���b�O���ɌĂ΂��C�x���g
	/// </summary>
	public override void OnDrag(PointerEventData eventData)
	{
		if (IsDrag)
		{
			currentPointerEventData = eventData;
			base.OnDrag(eventData);
		}
	}

	/// <summary>
	/// �h���b�O�I�����ɌĂ΂��C�x���g
	/// </summary>
	public override void OnEndDrag(PointerEventData eventData)
	{
		currentPointerEventData = eventData;
		base.OnEndDrag(eventData);
		IsDrag = false;

		if (!IsElastic)
		{
			return;
		}

		IsElasticMove = true;
		elasticTimer = Observable.TimerFrame(ElasticMoveTimeFrame, FrameCountType.Update)
								   .Subscribe(_ => IsElasticMove = false)
								   .AddTo(this);
	}

	public void CancelDrag()
	{
		IsDrag = false;
	}

	public void ForceDragEnd()
	{
		if (currentPointerEventData != null)
		{
			CancelDrag();
			//�����h���b�O�I��
			ExecuteEvents.Execute(currentPointerEventData.pointerDrag, currentPointerEventData, ExecuteEvents.endDragHandler);
		}
	}


	//--------------------------------------------
	// private
	//--------------------------------------------
	/// <summary>
	/// �X�N���[���o�[�̃A�N�e�B�u�ݒ�
	/// </summary>
	private void SetActiveScrollBar(bool isActive)
	{
		verticalScrollbar.TrySetActive(isActive);
		horizontalScrollbar.TrySetActive(isActive);
	}

	//--------------------------------------------
	// public
	//--------------------------------------------
	/// <summary>
	/// �R���|�[�l���g��L��/�����ݒ�
	/// </summary>
	public void SetEnabled(bool enabled)
	{
		// �R���|�[�l���g�L��/�����ݒ�
		this.enabled = enabled;

		// �X�N���[���o�[�\��/��\���ݒ�
		SetActiveScrollBar(enabled);
	}
}


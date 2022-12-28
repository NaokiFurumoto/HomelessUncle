using Carbon;
using System;
using UnityEngine;

/// <summary>
/// ScrollRect��Content�o�^����N���X
/// </summary>
[DisallowMultipleComponent]
public sealed class ScrollContentUI : RectTransformBehaviour
{
    //==================================================
    // �����o�[�ϐ�
    //==================================================
    private bool isInit = false;
    private float interval = 0;        // UI�z�u�̊Ԋu
    private float topInterval = 0;        // ��Ɖ��Ŋ�ʒu�Ⴄ�̂ō��킹��p
    private float offsetPos = 0;        // �����z�u���ꂽ������̃X�N���[���ړ���
    private long scrollIndex = 0;        // �X�N���[���̃C���f�b�N�X�l
    private float contentOffsetPos = 0;    // �R���e���c�̃I�t�Z�b�g

    //==================================================
    // �C�x���g
    //==================================================
    // �X�N���[���̃C���f�b�N�X�l���X�V���ꂽ���ɌĂ΂��C�x���g
    public Action<bool,long> OnUpdateScrollIndex { private get; set; }

    //==================================================
    // �v���p�e�B
    //==================================================
    public float AnchoredDirPosition => IsHorizontal ? anchoredPosition.x : -anchoredPosition.y;
    public float DefaultSize => IsHorizontal ? savedSizeDelta.x : savedSizeDelta.y;
    private bool IsHorizontal { get; set; } = false;    // �������ɃX�N���[������̂�
    private bool IsVertical => !IsHorizontal;       // �c�����ɃX�N���[������̂�

    //--------------------------------------------------
    // Monobehaviour
    //--------------------------------------------------
    private void OnDestroy()
    {
        OnUpdateScrollIndex = null;
    }

	//--------------------------------------------
	// public
	//--------------------------------------------
	/// <summary>
	/// ����������
	/// </summary>
	public void Init(bool isHorizontal, float interval, float contentOffset)
	{
		// RectTransform�̏����l��ێ����܂�(����̂�)
		SaveValue();

		// �e�평����
		isInit = true;
		this.interval = interval;

		IsHorizontal = isHorizontal;
		topInterval = interval;
		offsetPos = 0f;
		contentOffsetPos = contentOffset;
		scrollIndex = 0;

		SetAnchoredPosition(savedAnchoredPosition);
	}

	/// <summary>
	/// �N���A
	/// </summary>
	public void Clear()
	{
		isInit = false;
		interval = 0;
		topInterval = 0;
		offsetPos = 0;
		scrollIndex = 0;
		contentOffsetPos = 0;

		IsHorizontal = false;
	}

	/// <summary>
	/// ���t���[���X�V����
	/// </summary>
	public void DoUpdate()
	{
		if (!isInit)
		{
			return;
		}

		// �擪�̗v�f�𖖔��ֈړ�
		while (AnchoredDirPosition - offsetPos < -(topInterval + contentOffsetPos))
		{
			offsetPos -= interval;
			OnUpdateScrollIndex.Call(false, scrollIndex);
			scrollIndex++;
		}

		// �����̗v�f��擪�Ɉړ�
		while (AnchoredDirPosition - offsetPos > 0 - contentOffsetPos)
		{
			offsetPos += interval;
			scrollIndex--;
			OnUpdateScrollIndex.Call(true, scrollIndex);
		}
	}

	/// <summary>
	/// �X�N���[���������(XorY)��AnchoredPosition�ɒl�����Z���܂�
	/// </summary>
	public void AddAnchoredPosition(float pos)
	{
		var addPos = IsHorizontal ? new Vector2(pos, 0) : new Vector2(0, -pos);
		AddAnchoredPosition(addPos);
	}

	/// <summary>
	/// ��`�̃T�C�Y�ݒ�
	/// </summary>
	public void SetSize(float size)
	{
		var sizeDeita = IsHorizontal ? new Vector2(size, 0) : new Vector2(0, size);
		SetSizeDelta(sizeDeita);
	}
}

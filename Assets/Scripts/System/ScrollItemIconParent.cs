using Carbon;
using UnityEngine;
using UnityEngine.UI;

public sealed class ScrollItemIconParent : RectTransformBehaviour
{
	//============================================
	//! �����o�[�ϐ�
	//============================================
	[SerializeField] private HorizontalOrVerticalLayoutGroup layoutGroup;
	[SerializeField] private bool isHorizontal;

	//============================================
	//! �v���p�e�B
	//============================================
	public int Index { get; private set; }
	public int InstanceId { get { return gameObject.GetInstanceID(); } }


	//--------------------------------------------
	// private
	//--------------------------------------------
	/// <summary>
	/// ���������܂�
	/// </summary>
	private void Init(bool isHorizontal)
	{
		if (layoutGroup != null)
		{
			return;
		}

		this.isHorizontal = isHorizontal;

		if (!this.isHorizontal)
		{
			layoutGroup = gameObject.AddComponent<HorizontalLayoutGroup>();
			layoutGroup.childAlignment = TextAnchor.MiddleLeft;
		}
		else
		{
			layoutGroup = gameObject.AddComponent<VerticalLayoutGroup>();
			layoutGroup.childAlignment = TextAnchor.UpperCenter;
		}

		layoutGroup.childControlWidth = false;
		layoutGroup.childControlHeight = false;
		layoutGroup.childForceExpandWidth = false;
		layoutGroup.childForceExpandHeight = false;
	}

	//--------------------------------------------
	// public
	//--------------------------------------------
	/// <summary>
	/// LayoutGroup���f
	/// </summary>
	public void ApplyLayoutGroup(int gridCount, int totalPrefabSize)
	{
		// Spacing�ݒ�
		var rect = rectTransform.rect;
		var rectSize = isHorizontal ? rect.height : rect.width;
		var spacing = (rectSize - totalPrefabSize) / gridCount;
		layoutGroup.spacing = spacing;

		// �J�n�ʒu�𒲐�
		var halfSpacing = spacing * 0.5f;
		if (isHorizontal)
		{
			layoutGroup.padding.top = (int)halfSpacing;
		}
		else
		{
			layoutGroup.padding.left = (int)halfSpacing;
		}

		// �q��LayoutElement�̈ʒu����
		layoutGroup.CalculateLayoutInputHorizontal();
		layoutGroup.CalculateLayoutInputVertical();
		layoutGroup.SetLayoutHorizontal();
		layoutGroup.SetLayoutVertical();

		layoutGroup.enabled = false;
	}

	/// <summary>
	/// �C���f�b�N�X�l��ݒ肵�܂�
	/// </summary>
	public void SetIndex(int index)
	{
		Index = index;
	}

	//--------------------------------------------
	// public static
	//--------------------------------------------
	/// <summary>
	/// ScrollItemParent���쐬���ĕԂ��܂�
	/// </summary>
	/// <param name="parent">				�e�ƂȂ�RectTransform							</param>
	/// <param name="cachedRectTransform">	RectTransform�̐ݒ�l(Anchor�APivot�ASizeDelta)	</param>
	/// <param name="isHorizontal">			�X�N���[������									</param>
	public static ScrollItemIconParent Create(
		RectTransform parent,
		RectTransformBehaviour content,
		Vector2 pivot,
		bool isHorizontal
	)
	{
		// �Q�[���I�u�W�F�N�g�쐬
		var gameObject = new GameObject("ScrollItemIconParent");
		var scrollParent = gameObject.AddComponent<ScrollItemIconParent>();
		scrollParent.SetParent(parent, false);
		scrollParent.SetLayer(parent);
		scrollParent.ResetLocalTransform();

		// Anchor�ASize�APivot�ݒ�
		scrollParent.AdaptAnchor(content.rectTransform);
		scrollParent.SetSizeDelta(content.sizeDelta);
		scrollParent.SetPivot(pivot);

		if (!isHorizontal)
		{
			scrollParent.SetSizeDeltaY(0);
		}
		else
		{
			scrollParent.SetSizeDeltaX(0);
		}

		// ScrollItemParent�R���|�[�l���g������
		scrollParent.Init(isHorizontal);

		return scrollParent;
	}
}


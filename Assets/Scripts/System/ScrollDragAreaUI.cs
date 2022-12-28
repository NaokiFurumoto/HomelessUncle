using Carbon;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// ���g��ScrollRect�̃h���b�O�̈�Ɋ܂߂�N���X
/// </summary>
[DisallowMultipleComponent]
public sealed class ScrollDragAreaUI : CarbonBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	//============================================
	//! �����o�[�ϐ�(SerializeField)
	//============================================
	[SerializeField] private ScrollRectSystem scrollRect = null;


	//--------------------------------------------
	//	MonoBehaviour
	//--------------------------------------------
	/// <summary>
	/// Reset����
	/// </summary>
	private void Reset()
	{
		scrollRect = GetComponentInParent<ScrollRectSystem>();
	}

	private void Start()
	{
		if (!scrollRect)
		{
			scrollRect = GetComponentInParent<ScrollRectSystem>();
		}
	}

	//--------------------------------------------
	//	public
	//--------------------------------------------
	public void OnBeginDrag(PointerEventData eventData)
	{
		if (scrollRect && scrollRect.isActiveAndEnabled)
		{
			scrollRect.OnBeginDrag(eventData);
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (scrollRect && scrollRect.isActiveAndEnabled)
		{
			scrollRect.OnDrag(eventData);
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (scrollRect && scrollRect.isActiveAndEnabled)
		{
			scrollRect.OnEndDrag(eventData);
		}
	}
}


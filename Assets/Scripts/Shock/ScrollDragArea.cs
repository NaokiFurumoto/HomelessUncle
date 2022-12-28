using Carbon;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Shock
{
	/// <summary>
	/// 自身をScrollRectのドラッグ領域に含めるクラス
	/// </summary>
	[DisallowMultipleComponent]
	public sealed class ScrollDragArea : CarbonBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		//============================================
		//! メンバー変数(SerializeField)
		//============================================
		[SerializeField] private ScrollRect m_scrollRect = null;


		//--------------------------------------------
		//	MonoBehaviour
		//--------------------------------------------
		/// <summary>
		/// Reset処理
		/// </summary>
		private void Reset()
		{
			m_scrollRect = GetComponentInParent<ScrollRect>();
		}

		private void Start()
		{
			if (!m_scrollRect) {
				m_scrollRect = GetComponentInParent<ScrollRect>();
			}
		}

		//--------------------------------------------
		//	public
		//--------------------------------------------
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (m_scrollRect && m_scrollRect.isActiveAndEnabled) {
				m_scrollRect.OnBeginDrag(eventData);
			}
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (m_scrollRect && m_scrollRect.isActiveAndEnabled) {
				m_scrollRect.OnDrag(eventData);
			}
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			if (m_scrollRect && m_scrollRect.isActiveAndEnabled) {
				m_scrollRect.OnEndDrag(eventData);
			}
		}
	}
}
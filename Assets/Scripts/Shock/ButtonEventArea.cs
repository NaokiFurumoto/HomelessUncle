using Carbon;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Shock
{
	[DisallowMultipleComponent]
	public sealed class ButtonEventArea : RectTransformBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IBeginDragHandler
	{
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// MonoBehaviour
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		[SerializeField] private ButtonEvent m_ButtonEvent = null;

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// MonoBehaviour
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void Reset()
		{
			m_ButtonEvent = GetComponentInParent<ButtonEvent>();
		}

		private void Start()
		{
			if (!m_ButtonEvent) {
				m_ButtonEvent = GetComponentInParent<ButtonEvent>();
			}
		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Interface Implementation
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
		{
			if (m_ButtonEvent) {
				m_ButtonEvent.OnPointerClick(eventData);
			}
		}

		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			if (m_ButtonEvent) {
				m_ButtonEvent.OnPointerDown(eventData);
			}
		}

		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			if (m_ButtonEvent) {
				m_ButtonEvent.OnPointerUp(eventData);
			}
		}

		void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
		{
			if (m_ButtonEvent) {
				m_ButtonEvent.OnPointerExit(eventData);
			}
		}

		void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
		{
			if (m_ButtonEvent) {
				m_ButtonEvent.OnBeginDrag(eventData);
			}
		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Public Method
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 目標 ButtonEvent 指定
		/// </summary>
		public void SetTargetButtonEvent(ButtonEvent buttonEvent)
		{
			m_ButtonEvent = buttonEvent;
		}
	}
}

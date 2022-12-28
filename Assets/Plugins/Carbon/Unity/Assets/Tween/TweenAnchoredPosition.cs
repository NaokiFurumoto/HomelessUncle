using UnityEngine;

namespace Carbon
{
	[AddComponentMenu("Carbon/Tween/Tween Anchored-Position")]
	[RequireComponent(typeof(RectTransform))]
	public sealed class TweenAnchoredPosition : TweenBase
	{
		//======================================================================================================================
		// SerializeField
		//======================================================================================================================
		/// <summary>
		/// Value From
		/// </summary>
		[SerializeField]
		private Vector2 m_From = Vector2.zero;
		/// <summary>
		/// Value To
		/// </summary>
		[SerializeField]
		private Vector2 m_To = Vector2.zero;

		//======================================================================================================================
		// Field
		//======================================================================================================================
		/// <summary>
		/// RectTransform
		/// </summary>
		private RectTransform m_RectTransform = null;

		//======================================================================================================================
		// Property
		//======================================================================================================================
		/// <summary>
		/// Value From
		/// </summary>
		public Vector2 From {
			get { return m_From; }
			set { m_From = value; }
		}
		/// <summary>
		/// Value To
		/// </summary>
		public Vector2 To {
			get { return m_To; }
			set { m_To = value; }
		}
		/// <summary>
		/// Current Value
		/// </summary>
		public Vector2 Value {
			get { return rectTransform.anchoredPosition; }
			set { rectTransform.anchoredPosition = value; }
		}
		/// <summary>
		/// RectTransform
		/// </summary>
		private RectTransform rectTransform {
			get {
				if (!m_RectTransform) {
					m_RectTransform = this.DemandComponent<RectTransform>();
				}
				return m_RectTransform;
			}
		}

		//======================================================================================================================
		// TweenBase
		//======================================================================================================================
		protected override void DoOnDestroy()
		{
			m_RectTransform = null;
		}

		protected override void UpdateValue(float progress)
		{
			Value = m_From * (1f - progress) + m_To * progress;
		}

		[ContextMenu("Set 'From' to current value")]
		public override void SetBeginFromCurrentValue()
		{
			m_From = Value;
		}

		[ContextMenu("Set 'To' to current value")]
		public override void SetEndToCurrentValue()
		{
			m_To = Value;
		}

		[ContextMenu("Assume value of 'From'")]
		private void SetCurrentValueToBegin()
		{
			Value = m_From;
		}

		[ContextMenu("Assume value of 'To'")]
		private void SetCurrentValueToEnd()
		{
			Value = m_To;
		}
	}
}

using UnityEngine;

namespace Carbon
{
	[AddComponentMenu("Carbon/Tween/Tween CanvasRenderer Color")]
	[RequireComponent(typeof(CanvasRenderer))]
	public sealed class TweenCanvasRendererColor : TweenBase
	{
		//======================================================================================================================
		// SerializeField
		//======================================================================================================================
		/// <summary>
		/// Value From
		/// </summary>
		[SerializeField]
		private Color m_From = Color.white;
		/// <summary>
		/// Value To
		/// </summary>
		[SerializeField]
		private Color m_To = Color.white;

		//======================================================================================================================
		// Field
		//======================================================================================================================
		/// <summary>
		/// Target CanvasRenderer
		/// </summary>
		private CanvasRenderer m_CanvasRenderer = null;
		/// <summary>
		/// Target CanvasRenderer
		/// </summary>
		private CanvasRenderer CanvasRenderer {
			get {
				if (!m_CanvasRenderer) {
					m_CanvasRenderer = GetComponent<CanvasRenderer>();
				}
				return m_CanvasRenderer;
			}
		}

		//======================================================================================================================
		// Property
		//======================================================================================================================
		/// <summary>
		/// Value From
		/// </summary>
		public Color From {
			get { return m_From; }
			set { m_From = value; }
		}
		/// <summary>
		/// Value To
		/// </summary>
		public Color To {
			get { return m_To; }
			set { m_To = value; }
		}
		/// <summary>
		/// Current Value
		/// </summary>
		public Color Value {
			get { return CanvasRenderer.GetColor(); }
			set { CanvasRenderer.SetColor(value); }
		}

		//======================================================================================================================
		// TweenBase
		//======================================================================================================================
		protected override void DoOnDestroy()
		{
			m_CanvasRenderer = null;
		}

		protected override void UpdateValue(float progress)
		{
			Value = Color.Lerp(m_From, m_To, progress);
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

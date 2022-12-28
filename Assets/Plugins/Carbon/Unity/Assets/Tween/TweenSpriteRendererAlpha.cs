using UnityEngine;

namespace Carbon
{
	[AddComponentMenu("Carbon/Tween/Tween SpriteRenderer Alpha")]
	[RequireComponent(typeof(SpriteRenderer))]
	public sealed class TweenSpriteRendererAlpha : TweenBase
	{
		//======================================================================================================================
		// SerializeField
		//======================================================================================================================
		/// <summary>
		/// Value From
		/// </summary>
		[SerializeField, Range(0, 1)]
		private float m_From = 0f;
		/// <summary>
		/// Value To
		/// </summary>
		[SerializeField, Range(0, 1)]
		private float m_To = 1f;

		//======================================================================================================================
		// Field
		//======================================================================================================================
		/// <summary>
		/// Target SpriteRenderer
		/// </summary>
		private SpriteRenderer m_SpriteRenderer = null;
		/// <summary>
		/// Target SpriteRenderer
		/// </summary>
		private SpriteRenderer SpriteRenderer {
			get {
				if (!m_SpriteRenderer) {
					m_SpriteRenderer = GetComponent<SpriteRenderer>();
				}
				return m_SpriteRenderer;
			}
		}

		//======================================================================================================================
		// Property
		//======================================================================================================================
		/// <summary>
		/// Value From
		/// </summary>
		public float From {
			get { return m_From; }
			set { m_From = value; }
		}
		/// <summary>
		/// Value To
		/// </summary>
		public float To {
			get { return m_To; }
			set { m_To = value; }
		}
		/// <summary>
		/// Current Value
		/// </summary>
		public float Value {
			get { return SpriteRenderer.color.a; }
			set {
				var color = SpriteRenderer.color;
				color.a = value;
				SpriteRenderer.color = color;
			}
		}

		//======================================================================================================================
		// TweenBase
		//======================================================================================================================
		protected override void DoOnDestroy()
		{
			m_SpriteRenderer = null;
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

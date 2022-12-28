using UnityEngine;

namespace Carbon
{
	[AddComponentMenu("Carbon/Tween/Tween Integer")]
	public sealed class TweenInteger : TweenBase
	{
		//======================================================================================================================
		// SerializeField
		//======================================================================================================================
		/// <summary>
		/// Value From
		/// </summary>
		[SerializeField] private int m_From = 0;
		/// <summary>
		/// Value To
		/// </summary>
		[SerializeField] private int m_To = 0;

		//======================================================================================================================
		// Property
		//======================================================================================================================
		/// <summary>
		/// Value From
		/// </summary>
		public int From {
			get { return m_From; }
			set { m_From = value; }
		}
		/// <summary>
		/// Value To
		/// </summary>
		public int To {
			get { return m_To; }
			set { m_To = value; }
		}
		/// <summary>
		/// Current Value
		/// </summary>
		public int Value { get; set; }

		//======================================================================================================================
		// TweenBase
		//======================================================================================================================
		protected override void UpdateValue(float progress)
		{
			Value = Mathf.FloorToInt(m_From * (1f - progress) + m_To * progress);
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

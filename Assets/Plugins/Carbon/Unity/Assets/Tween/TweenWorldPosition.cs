﻿using UnityEngine;

namespace Carbon
{
	[AddComponentMenu("Carbon/Tween/Tween World Position")]
	public class TweenWorldPosition : TweenBase
	{
		//======================================================================================================================
		// SerializeField
		//======================================================================================================================
		/// <summary>
		/// Value From
		/// </summary>
		[SerializeField] protected Vector3 m_From = Vector3.zero;
		/// <summary>
		/// Value To
		/// </summary>
		[SerializeField] protected Vector3 m_To = Vector3.zero;

		//======================================================================================================================
		// Property
		//======================================================================================================================
		/// <summary>
		/// Value From
		/// </summary>
		public Vector3 From {
			get { return m_From; }
			set { m_From = value; }
		}
		/// <summary>
		/// Value To
		/// </summary>
		public Vector3 To {
			get { return m_To; }
			set { m_To = value; }
		}
		/// <summary>
		/// Current Value
		/// </summary>
		public virtual Vector3 Value {
			get { return worldPosition; }
			set { worldPosition = value; }
		}

		//======================================================================================================================
		// TweenBase
		//======================================================================================================================
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
		protected void SetCurrentValueToBegin()
		{
			Value = m_From;
		}

		[ContextMenu("Assume value of 'To'")]
		protected void SetCurrentValueToEnd()
		{
			Value = m_To;
		}
	}
}

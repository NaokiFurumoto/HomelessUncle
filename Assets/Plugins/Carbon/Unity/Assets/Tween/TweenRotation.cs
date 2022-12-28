using UnityEngine;

namespace Carbon
{
	[AddComponentMenu("Carbon/Tween/Tween Rotation")]
	public sealed class TweenRotation : TweenBase
	{
		//======================================================================================================================
		// SerializeField
		//======================================================================================================================
		/// <summary>
		/// Value From
		/// </summary>
		[SerializeField]
		private Vector3 m_From = Vector3.zero;
		/// <summary>
		/// Value To
		/// </summary>
		[SerializeField]
		private Vector3 m_To = Vector3.zero;
		/// <summary>
		/// Use SLerp
		/// </summary>
		[SerializeField]
		private bool m_UsesSlerp = false;

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
		public Quaternion Value {
			get { return localRotation; }
			set { localRotation = value; }
		}

		//======================================================================================================================
		// TweenBase
		//======================================================================================================================
		protected override void UpdateValue(float progress)
		{
			if (m_UsesSlerp) {
				Value = Quaternion.Slerp(Quaternion.Euler(m_From), Quaternion.Euler(m_To), progress);
			}
			else {
				Value = Quaternion.Euler(Vector3.Lerp(m_From, m_To, progress));
			}
		}

		[ContextMenu("Set 'From' to current value")]
		public override void SetBeginFromCurrentValue()
		{
			m_From = localEulerAngles;
		}

		[ContextMenu("Set 'To' to current value")]
		public override void SetEndToCurrentValue()
		{
			m_To = localEulerAngles;
		}

		[ContextMenu("Assume value of 'From'")]
		private void SetCurrentValueToBegin()
		{
			Value = Quaternion.Euler(m_From);
		}

		[ContextMenu("Assume value of 'To'")]
		private void SetCurrentValueToEnd()
		{
			Value = Quaternion.Euler(m_To);
		}
	}
}

﻿using System.Collections;
using UnityEngine;

namespace Carbon
{
	public sealed class WaitForSeconds : IEnumerator
	{
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// WaitForSeconds
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private float m_RemainingSeconds = 0f;

		public WaitForSeconds(float seconds)
		{
			m_RemainingSeconds = seconds;
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// IEnumerator Implementation
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		object IEnumerator.Current {
			get {
				return null;
			}
		}

		bool IEnumerator.MoveNext()
		{
			bool ret = m_RemainingSeconds > 0;
			m_RemainingSeconds -= Time.deltaTime;
			return ret;
		}

		void IEnumerator.Reset()
		{

		}
	}
}

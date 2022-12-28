﻿using System.Collections;

namespace Carbon
{
	public sealed class WaitForFrames : IEnumerator
	{
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// WaitForFrames
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private int m_RemainingFrames = 0;

		public WaitForFrames(int frames)
		{
			m_RemainingFrames = frames;
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
			return 0 < m_RemainingFrames--;
		}

		void IEnumerator.Reset()
		{

		}
	}
}
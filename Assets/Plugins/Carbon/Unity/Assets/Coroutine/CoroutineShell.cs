namespace Carbon
{
	/// <summary>
	/// Carbon コルーチンをハンドルするシェル
	/// </summary>
	public sealed class CoroutineShell : ICoroutine
	{
		private CoroutineManager.ICoroutineCore	m_Core = null;
		private long							m_CoreUniqueId = -1;

		public bool IsAlive {
			get {
				if (m_Core == null
				||	m_Core.UniqueId != m_CoreUniqueId) {
					return false;
				}
				return m_Core.IsAlive;
			}
		}

		public bool IsPaused {
			get {
				if (m_Core == null
				||	m_Core.UniqueId != m_CoreUniqueId) {
					return false;
				}
				return m_Core.IsPaused;
			}
		}

		public void Pause(bool pause)
		{
			if (m_Core == null) {
				return;
			}

			if (m_Core.UniqueId != m_CoreUniqueId) {
				return;
			}

			m_Core.Pause(pause);
		}

		public void Dispose()
		{
			if (m_Core == null) {
				return;
			}

			if (m_Core.UniqueId != m_CoreUniqueId) {
				return;
			}

			m_Core.Dispose();
		}

		//======================================================================================================================
		// Internal Method
		//======================================================================================================================
		internal void SetCore(CoroutineManager.ICoroutineCore core)
		{
			m_Core = core;
			m_CoreUniqueId = m_Core.UniqueId;
		}
	}
}

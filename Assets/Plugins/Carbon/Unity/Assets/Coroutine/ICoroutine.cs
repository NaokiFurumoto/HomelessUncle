using System;

namespace Carbon
{
	/// <summary>
	/// Carbon コルーチン interface
	/// </summary>
	public interface ICoroutine : IDisposable
	{
		bool IsAlive { get; }
		bool IsPaused { get; }
		void Pause(bool pause);
	}
}

namespace Carbon
{
	public interface IRoutine
	{
		bool IsAlive { get; }
		bool IsRunnable { get; }
	}

	public interface IUpdateRoutine : IRoutine
	{
		void OnUpdate(float deltaTime);
	}

	public interface ILateUpdateRoutine : IRoutine
	{
		void OnLateUpdate(float deltaTime);
	}
}

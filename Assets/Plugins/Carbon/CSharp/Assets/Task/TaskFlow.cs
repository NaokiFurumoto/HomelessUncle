using System;
using System.Collections.Generic;

namespace Carbon
{
	/// <summary>
	/// Single Thread Task Flow
	/// </summary>
	public sealed class TaskFlow
	{
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Field
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Main Queue
		/// </summary>
		private readonly StepTask m_MainTask = new StepTask();
		/// <summary>
		/// Sub-task List
		/// </summary>
		private readonly List<ParallelTask> m_SubTaskList = new List<ParallelTask>();
		/// <summary>
		/// Temporary TaskSequence
		/// </summary>
		private readonly TaskSequence m_TempTaskSequence = new TaskSequence();

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Public Method
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Clear
		/// </summary>
		public void Clear()
		{
			m_MainTask.Clear();
			m_TempTaskSequence.Clear();
		}

		/// <summary>
		/// Create a synchronization point.
		/// </summary>
		public void Synchronize()
		{
			if (m_TempTaskSequence.IsEmpty) {
				return;
			}

			var parallelTask = new ParallelTask(m_TempTaskSequence);

			m_TempTaskSequence.Clear();

			m_MainTask.Push(next => parallelTask.Process(next));
		}

		/// <summary>
		/// Add an action and create synchronization points in front and behind.
		/// </summary>
		/// <param name="action">Action</param>
		public void Step(Action action)
		{
			Synchronize();
			m_MainTask.Push(action);
		}

		/// <summary>
		/// Add a task and create synchronization points in front and behind.
		/// </summary>
		/// <param name="action">Action</param>
		public void Step(Action<Action> task)
		{
			Synchronize();
			m_MainTask.Push(task);
		}

		/// <summary>
		/// Add an action without creating synchronization point.
		/// </summary>
		/// <param name="action">Action</param>
		public void Parallel(Action action)
		{
			m_TempTaskSequence.Push(action);
		}

		/// <summary>
		/// Add an task without creating a synchronization point.
		/// </summary>
		/// <param name="action">Action</param>
		public void Parallel(Action<Action> task)
		{
			m_TempTaskSequence.Push(task);
		}

		/// <summary>
		/// Process the TaskFlow.
		/// </summary>
		/// <param name="onComplete">Callback on complete.</param>
		public void Process(Action onComplete = null)
		{
			Synchronize();
			m_MainTask.Process(onComplete);
		}
	}
}

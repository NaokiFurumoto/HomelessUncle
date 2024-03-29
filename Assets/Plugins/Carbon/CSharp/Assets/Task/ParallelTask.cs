﻿using System;

namespace Carbon
{
	/// <summary>
	/// TaskSequence which processes tasks simultaneously.
	/// </summary>
	public sealed class ParallelTask : TaskSequence
	{
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Private Field
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Callback of queue completion.
		/// </summary>
		private Action m_OnComplete = null;
		/// <summary>
		/// count of executable or executing task.
		/// </summary>
		private int m_RestTaskCount = 0;

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Constructor
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public ParallelTask() { }
		public ParallelTask(TaskSequence taskSequence) : base(taskSequence) { }

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Public Method
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Clear and kill tasks.
		/// </summary>
		public override void Clear()
		{
			m_RestTaskCount = 0;
			m_OnComplete = null;
			base.Clear();
		}

		/// <summary>
		/// Process the task sequence simultaneously.
		/// </summary>
		public void Process(Action onComplete)
		{
			// return if is processing or the queue is empty
			if (Count <= 0) {
				onComplete.Call();
				return;
			}

			m_OnComplete = onComplete;
			m_RestTaskCount = Count;

			while (Count > 0) {
				Dequeue().Call(OnTaskComplete);
			};
		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Private Method
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Event on task complete.
		/// </summary>
		private void OnTaskComplete()
		{
			// 残 task 数 -1
			m_RestTaskCount--;

			// 残 task なし
			if (m_RestTaskCount > 0) {
				return;
			}

			ActionUtils.CallOnce(ref m_OnComplete);
		}
	}
}
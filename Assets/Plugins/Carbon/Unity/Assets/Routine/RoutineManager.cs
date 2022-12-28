using System.Collections.Generic;
using UnityEngine;

namespace Carbon
{
	[DisallowMultipleComponent]
	public sealed class RoutineManager : MonoBehaviour
	{
		//======================================================================================================================
		// Private Field
		//======================================================================================================================
		private static RoutineManager ms_Instance = null;

		//======================================================================================================================
		// Constructor
		//======================================================================================================================
		static RoutineManager()
		{
			DebugUtils.Log("static RoutineManager()");
			GameObject go = new GameObject("RoutineManager", typeof(RoutineManager));
			GameObject.DontDestroyOnLoad(go);
		}

		public static void Construct()
		{
			// do nothing
		}

		//======================================================================================================================
		// Public Method
		//======================================================================================================================
		public static void Clear()
		{
			if (ms_Instance) {
				ms_Instance.m_UpdateRoutineList.Clear();
				ms_Instance.m_UpdateSmoothRoutineList.Clear();
				ms_Instance.m_LateUpdateRoutineList.Clear();
			}
		}

		public static void Update(IUpdateRoutine iUpdateRoutine)
		{
			if (ms_Instance) {
				ms_Instance.m_UpdateRoutineList.Add(iUpdateRoutine);
			}
		}

		public static void UpdateSmooth(IUpdateRoutine iUpdateRoutine)
		{
			if (ms_Instance) {
				ms_Instance.m_UpdateSmoothRoutineList.Add(iUpdateRoutine);
			}
		}

		public static void LateUpdate(ILateUpdateRoutine iLateUpdateRoutine)
		{
			if (ms_Instance) {
				ms_Instance.m_LateUpdateRoutineList.Add(iLateUpdateRoutine);
			}
		}

		//======================================================================================================================
		// Field
		//======================================================================================================================
		/// <summary>
		/// List to update. Called if component is active and enabled.
		/// </summary>
		private readonly List<IUpdateRoutine> m_UpdateRoutineList = new List<IUpdateRoutine>(128);
		/// <summary>
		/// List to update with smoothDeltaTime. Called if component is active and enabled.
		/// </summary>
		private readonly List<IUpdateRoutine> m_UpdateSmoothRoutineList = new List<IUpdateRoutine>(128);
		/// <summary>
		/// List to late-update. Called if component is active and enabled.
		/// </summary>
		private readonly List<ILateUpdateRoutine> m_LateUpdateRoutineList = new List<ILateUpdateRoutine>(128);

		//======================================================================================================================
		// MonoBehaviour Method
		//======================================================================================================================
		private void Awake()
		{
			if (ms_Instance && ms_Instance != this) {
				if (ms_Instance.gameObject == gameObject) {
					this.Destroy();
				}
				else {
					this.DestroyGameObject();
				}
				return;
			}
			ms_Instance = this;
		}

		private void Update()
		{
			float deltaTime = Time.deltaTime;

			bool shouldRemove = false;
			for (int i = 0, count = m_UpdateRoutineList.Count; i < count; i++) {
				var routine = m_UpdateRoutineList[i];
				if (routine != null && routine.IsAlive) {
					if (routine.IsRunnable) {
						routine.OnUpdate(deltaTime);
					}
					continue;
				}
				shouldRemove = true;
			}

			if (shouldRemove) {
				m_UpdateRoutineList.RemoveAll(IsRoutineTerminated);
			}

			float smoothDeltaTime = Time.smoothDeltaTime;

			bool shouldRemoveSmooth = false;
			for (int i = 0, count = m_UpdateSmoothRoutineList.Count; i < count; i++) {
				var routine = m_UpdateSmoothRoutineList[i];
				if (routine != null && routine.IsAlive) {
					if (routine.IsRunnable) {
						routine.OnUpdate(smoothDeltaTime);
					}
					continue;
				}
				shouldRemoveSmooth = true;
			}

			if (shouldRemoveSmooth) {
				m_UpdateSmoothRoutineList.RemoveAll(IsRoutineTerminated);
			}
		}

		private void LateUpdate()
		{
			float deltaTime = Time.deltaTime;

			bool shouldRemove = false;
			for (int i = 0; i < m_LateUpdateRoutineList.Count; i++) {
				var routine = m_LateUpdateRoutineList[i];
				if (routine != null && routine.IsAlive) {
					if (routine.IsRunnable) {
						routine.OnLateUpdate(deltaTime);
					}
					continue;
				}
				shouldRemove = true;
			}

			if (shouldRemove) {
				m_LateUpdateRoutineList.RemoveAll(IsRoutineTerminated);
			}
		}

		//======================================================================================================================
		// Private Method
		//======================================================================================================================
		private bool IsRoutineTerminated(IRoutine routine)
		{
			return (routine == null) || (routine.IsAlive == false);
		}
	}
}
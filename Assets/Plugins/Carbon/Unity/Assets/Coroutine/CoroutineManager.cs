using Carbon.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carbon
{
	/// <summary>
	/// Coroutine Manager
	/// </summary>
	[DisallowMultipleComponent]
	public sealed class CoroutineManager : MonoBehaviour, ILateUpdateRoutine
	{
		//======================================================================================================================
		// Definition
		//======================================================================================================================
		public interface ICoroutineCore : IDisposable
		{
			long UniqueId	{ get; }
			bool IsAlive	{ get; }
			bool IsPaused	{ get; }
			void Pause(bool pause);
		}

		private sealed class CoroutineCore : ICoroutineCore
		{
			private MonoBehaviour				m_MonoBehaviour		= null;
			private readonly Stack<IEnumerator>	m_CoroutineStack	= new Stack<IEnumerator>(4);

			internal bool					IsRunnable		{ get { return m_MonoBehaviour.isActiveAndEnabled && !IsPaused; } }
			internal CoroutineProcessTiming	ProcessTiming	{ get; private set; } = CoroutineProcessTiming.Update;
			internal long					ProcessVersion	{ get; set; } = -1;

			public long UniqueId	{ get; private set; } = -1;
			public bool IsAlive		=> m_MonoBehaviour;
			public bool IsPaused	{ get; private set; } = false;

			public void Setup(long uniqueId, MonoBehaviour monoBehaviour, IEnumerator coroutine)
			{
				UniqueId		= uniqueId;
				m_MonoBehaviour	= monoBehaviour;
				m_CoroutineStack.Clear();
				m_CoroutineStack.Push(coroutine);
				IsPaused		= false;
				ProcessTiming	= CoroutineProcessTiming.Update;
				ProcessVersion	= -1;
			}

			public void Run()
			{
				var interrupts = false;
				var coroutine = m_CoroutineStack.PopOrDefault();
				while (coroutine != null) {
					if (TryRun(coroutine, out interrupts)) {
						m_CoroutineStack.Push(coroutine);
						if (interrupts) {
							return;
						}
						coroutine = coroutine.Current as IEnumerator;
					}
					else {
						coroutine = m_CoroutineStack.PopOrDefault();
					}
				}
				Dispose();
			}

			public void Pause(bool pause)
			{
				IsPaused = pause;
			}

			public void Dispose()
			{
				ProcessVersion	= -1;
				ProcessTiming	= CoroutineProcessTiming.Update;
				IsPaused		= false;
				m_CoroutineStack.Clear();
				m_MonoBehaviour	= null;
				UniqueId		= -1;
			}

			// -----------------------------------------------------------------

			private bool TryRun(IEnumerator coroutine, out bool interrupts)
			{
				interrupts = false;

				if (coroutine.MoveNext()) {
					if (coroutine.Current == null) {
						ProcessTiming = CoroutineProcessTiming.Update;
						interrupts = true;
					}
					else if (coroutine.Current is IEnumerator) {
						// do nothing
					}
					else if (!(coroutine.Current as AsyncOperation)?.isDone ?? false) {
						ProcessTiming = CoroutineProcessTiming.Update;
						interrupts = true;
					}
					else if (coroutine.Current is WaitForLateUpdate) {
						ProcessTiming = CoroutineProcessTiming.LateUpdate;
						interrupts = true;
					}
					else if (coroutine.Current is WaitForEndOfFrame) {
						ProcessTiming = CoroutineProcessTiming.EndOfFrame;
						interrupts = true;
					}
					else if (coroutine.Current is WaitForFixedUpdate) {
						ProcessTiming = CoroutineProcessTiming.FixedUpdate;
						interrupts = true;
					}
					else {
						DebugUtils.WarningTrue(coroutine.Current is YieldInstruction, "Sorry. CoroutineCore cannot deal with regular UnityEngine.YieldInstruction.");
						ProcessTiming = CoroutineProcessTiming.Update;
					}
					return true;
				}

				return false;
			}
		}

		//======================================================================================================================
		// Field
		//======================================================================================================================
		private readonly ListBuffer<CoroutineCore> m_CoreBuffer = new ListBuffer<CoroutineCore>();

		private long					m_CoroutineSerialCount	= 0;
		private CoroutineProcessTiming	m_CurrentProcessTiming	= CoroutineProcessTiming.Update;
		private long					m_MainLoopVersion		= 0;
		private long					m_FixedLoopVersion		= 0;

		//======================================================================================================================
		// Interface Implementation
		//======================================================================================================================
		bool IRoutine.IsAlive { get { return this; } }

		bool IRoutine.IsRunnable { get { return isActiveAndEnabled; } }

		void ILateUpdateRoutine.OnLateUpdate(float deltaTime)
		{
			// late-update -> end-of-frame
			m_CurrentProcessTiming = CoroutineProcessTiming.LateUpdate;
			ProcessCoroutine(m_CurrentProcessTiming, m_MainLoopVersion);
			m_CurrentProcessTiming = CoroutineProcessTiming.EndOfFrame;
		}

		//======================================================================================================================
		// Private Method
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

			m_CoreBuffer.CreateBuffer(64, false, () => new CoroutineCore());

			RoutineManager.LateUpdate(this);

			StartCoroutine(_UpdateCoroutine());
			StartCoroutine(_EndOfFrameCoroutine());
			StartCoroutine(_FixedUpdateCoroutine());
		}

		private void ProcessCoroutine(CoroutineProcessTiming timing, long version)
		{
			for (int i = 0; i < m_CoreBuffer.UsedCount;) {
				var coroutine = m_CoreBuffer.UsedList[i];
				if (coroutine.ProcessTiming != timing) {
					i++;
					continue;
				}

				if (coroutine.IsAlive) {
					if (coroutine.IsRunnable
					&&	coroutine.ProcessVersion < version) {
						coroutine.ProcessVersion = version;
						coroutine.Run();
						// keep processable if timing is changed
						if (coroutine.ProcessTiming == timing) {
							coroutine.ProcessVersion = version;
						}
					}
					i++;
					continue;
				}
				m_CoreBuffer.FreeAt(i);
			}
		}

		private void PushCoroutine(MonoBehaviour monoBehaviour, IEnumerator coroutine, CoroutineShell coroutineShell)
		{
			if (!monoBehaviour) {
				return;
			}

			var core = m_CoreBuffer.Alloc();
			if (core == null) {
				return;
			}

			core.Setup(++m_CoroutineSerialCount, monoBehaviour, coroutine);

			if (core.IsRunnable) {
				core.Run();
				// skips to process on the next loop if its next processing timing is same as current.
				var timing = core.ProcessTiming;
				if (timing == m_CurrentProcessTiming) {
					if (timing == CoroutineProcessTiming.FixedUpdate) {
						core.ProcessVersion = m_FixedLoopVersion;
					}
					else {
						core.ProcessVersion = m_MainLoopVersion;
					}
				}
			}

			coroutineShell?.SetCore(core);
		}

		private IEnumerator _UpdateCoroutine()
		{
			while (true) {
				yield return null;
				// update -> late-update
				m_CurrentProcessTiming = CoroutineProcessTiming.Update;
				ProcessCoroutine(m_CurrentProcessTiming, m_MainLoopVersion);
				m_CurrentProcessTiming = CoroutineProcessTiming.LateUpdate;
			}
		}

		private IEnumerator _EndOfFrameCoroutine()
		{
			while (true) {
				yield return ms_WaitForEndOfFrame;
				// end-of-frame -> update
				m_CurrentProcessTiming = CoroutineProcessTiming.EndOfFrame;
				ProcessCoroutine(m_CurrentProcessTiming, m_MainLoopVersion);
				m_CurrentProcessTiming = CoroutineProcessTiming.Update;
				// raise loop-version
				++ m_MainLoopVersion;
			}
		}

		private IEnumerator _FixedUpdateCoroutine()
		{
			while (true) {
				yield return ms_WaitForFixedUpdate;
				// xxx -> fixed-update -> xxx
				var tempTiming = m_CurrentProcessTiming;
				m_CurrentProcessTiming = CoroutineProcessTiming.FixedUpdate;
				ProcessCoroutine(m_CurrentProcessTiming, m_FixedLoopVersion);
				m_CurrentProcessTiming = tempTiming;
			}
		}

		//======================================================================================================================
		// Static Field
		//======================================================================================================================
		private static CoroutineManager ms_Instance = null;

		private static readonly WaitForEndOfFrame	ms_WaitForEndOfFrame	= new WaitForEndOfFrame();
		private static readonly WaitForFixedUpdate	ms_WaitForFixedUpdate	= new WaitForFixedUpdate();

		//======================================================================================================================
		// Static Constructor
		//======================================================================================================================
		static CoroutineManager()
		{
			DebugUtils.Log("static CoroutineManager()");
			GameObject go = new GameObject("CoroutineManager", typeof(CoroutineManager));
			GameObject.DontDestroyOnLoad(go);
		}

		public static void Construct()
		{
			// do nothing
		}

		//======================================================================================================================
		// Static Public Method
		//======================================================================================================================
		/// <summary>
		/// コルーチンを実行する.
		/// </summary>
		/// <param name="coroutineShell">コルーチンをハンドルするシェル.</param>
		/// <param name="monoBehaviour">コルーチンの依存先. isActiveAndEnabled 状態のみ実行される.</param>
		/// <param name="coroutine">コルーチン.</param>
		public static void Process(CoroutineShell coroutineShell, MonoBehaviour monoBehaviour, IEnumerator coroutine)
		{
			if (!monoBehaviour) {
				return;
			}

			if (!ms_Instance) {
				return;
			}

			ms_Instance.PushCoroutine(monoBehaviour, coroutine, coroutineShell);
		}

		/// <summary>
		/// コルーチンを実行する.
		/// </summary>
		/// <param name="monoBehaviour">コルーチンの依存先. isActiveAndEnabled 状態のみ実行される.</param>
		/// <param name="routine">コルーチン.</param>
		/// <returns>コルーチンをハンドルするシェル</returns>
		public static CoroutineShell Process(MonoBehaviour monoBehaviour, IEnumerator coroutine)
		{
			var coroutineShell = new CoroutineShell();

			Process(coroutineShell, monoBehaviour, coroutine);

			return coroutineShell;
		}

		/// <summary>
		/// コルーチンを実行する. コルーチンの依存先を CoroutineManager に指定 (つまりコルーチン自身が終了まで止まることはない).
		/// </summary>
		/// <param name="coroutineShell">コルーチンをハンドルするシェル.</param>
		/// <param name="coroutine">コルーチン</param>
		public static void Process(CoroutineShell coroutineShell, IEnumerator coroutine)
		{
			Process(coroutineShell, ms_Instance, coroutine);
		}

		/// <summary>
		/// コルーチンを実行する. コルーチンの依存先を CoroutineManager に指定 (つまりコルーチン自身が終了まで止まることはない).
		/// </summary>
		/// <param name="coroutine">コルーチン</param>
		/// <returns>コルーチンをハンドルするシェル</returns>
		public static CoroutineShell Process(IEnumerator coroutine)
		{
			return Process(ms_Instance, coroutine);
		}

		#region coroutine
		/// <summary>
		/// 1 フレーム待機してから処理デリゲートを呼び出する.
		/// </summary>
		/// <param name="action">1 フレーム経過後に行う処理.</param>
		/// <returns>コルーチン.</returns>
		public static void CallWaitForOneFrame(CoroutineShell coroutineShell, MonoBehaviour monoBehaviour, Action action)
		{
			Process(coroutineShell, monoBehaviour, _CallWaitForFrames(1, action));
		}
		public static CoroutineShell CallWaitForOneFrame(MonoBehaviour monoBehaviour, Action action)
		{
			return Process(monoBehaviour, _CallWaitForFrames(1, action));
		}
		public static CoroutineShell CallWaitForOneFrame(Action action)
		{
			return Process(_CallWaitForFrames(1, action));
		}

		/// <summary>
		/// n フレーム待機してから処理デリゲートを呼び出する.
		/// </summary>
		/// <param name="seconds">待機フレーム数.</param>
		/// <param name="action">指定されたフレーム数経過後に行う処理.</param>
		/// <returns>コルーチン.</returns>
		public static void CallWaitForFrames(CoroutineShell coroutineShell, MonoBehaviour monoBehaviour, int frames, Action action)
		{
			Process(coroutineShell, monoBehaviour, _CallWaitForFrames(frames, action));
		}
		public static CoroutineShell CallWaitForFrames(MonoBehaviour monoBehaviour, int frames, Action action)
		{
			return Process(monoBehaviour, _CallWaitForFrames(frames, action));
		}
		public static CoroutineShell CallWaitForFrames(int frames, Action action)
		{
			return Process(_CallWaitForFrames(frames, action));
		}

		/// <summary>
		/// n 秒待機してから処理デリゲートを呼び出する.
		/// </summary>
		/// <param name="seconds">待機秒数.</param>
		/// <param name="action">指定された秒数経過後に行う処理.</param>
		/// <returns>コルーチン.</returns>
		public static void CallWaitForSeconds(CoroutineShell coroutineShell, MonoBehaviour monoBehaviour, float seconds, Action action)
		{
			Process(coroutineShell, monoBehaviour, _CallWaitForSeconds(seconds, action));
		}
		public static CoroutineShell CallWaitForSeconds(MonoBehaviour monoBehaviour, float seconds, Action action)
		{
			return Process(monoBehaviour, _CallWaitForSeconds(seconds, action));
		}
		public static CoroutineShell CallWaitForSeconds(float seconds, Action action)
		{
			return Process(_CallWaitForSeconds(seconds, action));
		}

		/// <summary>
		/// n 秒待機してから Action デリゲートを呼び出する. Realtime 版.
		/// </summary>
		/// <param name="seconds">待機秒数.</param>
		/// <param name="action">指定された秒数経過後に行う処理.</param>
		/// <returns>コルーチン.</returns>
		public static void CallWaitForSecondsRealtime(CoroutineShell coroutineShell, MonoBehaviour monoBehaviour, float seconds, Action action)
		{
			Process(coroutineShell, monoBehaviour, _CallWaitForSecondsRealtime(seconds, action));
		}
		public static CoroutineShell CallWaitForSecondsRealtime(MonoBehaviour monoBehaviour, float seconds, Action action)
		{
			return Process(monoBehaviour, _CallWaitForSecondsRealtime(seconds, action));
		}
		public static CoroutineShell CallWaitForSecondsRealtime(float seconds, Action action)
		{
			return Process(_CallWaitForSecondsRealtime(seconds, action));
		}

		/// <summary>
		/// 条件を満たしたら処理デリゲートを呼び出する.
		/// </summary>
		/// <param name="condition">条件関数.</param>
		/// <param name="action">条件を満たした際に行う処理.</param>
		/// <returns>コルーチン.</returns>
		public static void CallWaitForCondition(CoroutineShell coroutineShell, MonoBehaviour monoBehaviour, Func<bool> condition, Action action)
		{
			Process(coroutineShell, monoBehaviour, _CallWaitForCondition(condition, action));
		}
		public static CoroutineShell CallWaitForCondition(MonoBehaviour monoBehaviour, Func<bool> condition, Action action)
		{
			return Process(monoBehaviour, _CallWaitForCondition(condition, action));
		}
		public static CoroutineShell CallWaitForCondition(Func<bool> condition, Action action)
		{
			return Process(_CallWaitForCondition(condition, action));
		}

		/// <summary>
		/// タスク実行完了したら処理デリゲートを呼び出する.
		/// </summary>
		/// <param name="task">タスク.</param>
		/// <param name="action">タスク実行完了した際に行う処理.</param>
		/// <returns>コルーチン.</returns>
		public static void CallWaitForTask(CoroutineShell coroutineShell, MonoBehaviour monoBehaviour, Action<Action> task, Action action)
		{
			Process(coroutineShell, monoBehaviour, _CallWaitForTask(task, action));
		}
		public static CoroutineShell CallWaitForTask(MonoBehaviour monoBehaviour, Action<Action> task, Action action)
		{
			return Process(monoBehaviour, _CallWaitForTask(task, action));
		}
		public static CoroutineShell CallWaitForTask(Action<Action> task, Action action)
		{
			return Process(_CallWaitForTask(task, action));
		}

		/// <summary>
		/// AsyncOperation 完了したら処理デリゲートを呼び出する.
		/// </summary>
		/// <param name="asyncOperation">AsyncOperation</param>
		/// <param name="action">タスク実行完了した際に行う処理.</param>
		/// <returns>コルーチン.</returns>
		public static void CallWaitForAsyncOperation(CoroutineShell coroutineShell, MonoBehaviour monoBehaviour, AsyncOperation asyncOperation, Action action)
		{
			Process(coroutineShell, monoBehaviour, _CallWaitForAsyncOperation(asyncOperation, action));
		}
		public static CoroutineShell CallWaitForAsyncOperation(MonoBehaviour monoBehaviour, AsyncOperation asyncOperation, Action action)
		{
			return Process(monoBehaviour, _CallWaitForAsyncOperation(asyncOperation, action));
		}
		public static CoroutineShell CallWaitForAsyncOperation(AsyncOperation asyncOperation, Action action)
		{
			return Process(_CallWaitForAsyncOperation(asyncOperation, action));
		}

		/// <summary>
		/// FPS 安定したら処理デリゲートを呼び出する. 0.5 ~ 1.0 秒がおすすめ.
		/// </summary>
		/// <param name="timeOutSeconds">タイムアウト秒数.</param>
		/// <param name="action">処理デリゲート.</param>
		/// <returns>コルーチン.</returns>
		public static void CallWaitForStableFps(CoroutineShell coroutineShell, MonoBehaviour monoBehaviour, float timeOutSeconds, Action action)
		{
			Process(coroutineShell, monoBehaviour, _CallWaitForStableFps(timeOutSeconds, action));
		}
		public static CoroutineShell CallWaitForStableFps(MonoBehaviour monoBehaviour, float timeOutSeconds, Action action)
		{
			return Process(monoBehaviour, _CallWaitForStableFps(timeOutSeconds, action));
		}
		public static CoroutineShell CallWaitForStableFps(float timeOutSeconds, Action action)
		{
			return Process(_CallWaitForStableFps(timeOutSeconds, action));
		}

		/// <summary>
		/// n フレーム経過する度に処理デリゲートを呼び出する. MonoBehaviour.InvokeRepeating() の Action デリゲート版.
		/// </summary>
		/// <param name="delayFrames">初回呼び出しの遅延フレーム数.</param>
		/// <param name="intervalFrames">２回目からの呼び出し間隔フレーム数.</param>
		/// <param name="action">処理デリゲート.</param>
		/// <returns>コルーチン.</returns>
		public static void CallPerFrames(CoroutineShell coroutineShell, MonoBehaviour monoBehaviour, int delayFrames, int intervalFrames, Action action)
		{
			Process(coroutineShell, monoBehaviour, _CallPerFrames(delayFrames, intervalFrames, action));
		}
		public static CoroutineShell CallPerFrames(MonoBehaviour monoBehaviour, int delayFrames, int intervalFrames, Action action)
		{
			return Process(monoBehaviour, _CallPerFrames(delayFrames, intervalFrames, action));
		}
		public static CoroutineShell CallPerFrames(int delayFrames, int intervalFrames, Action action)
		{
			return Process(_CallPerFrames(delayFrames, intervalFrames, action));
		}

		/// <summary>
		/// n 秒経過する度に処理デリゲートを呼び出する. MonoBehaviour.InvokeRepeating() の Action デリゲート版.
		/// </summary>
		/// <param name="delaySeconds">初回呼び出しの遅延秒数.</param>
		/// <param name="intervalSeconds">２回目からの呼び出し間隔秒数.</param>
		/// <param name="action">処理デリゲート</param>
		/// <returns>コルーチン.</returns>
		public static void CallPerSeconds(CoroutineShell coroutineShell, MonoBehaviour monoBehaviour, float delaySeconds, float intervalSeconds, Action action)
		{
			Process(coroutineShell, monoBehaviour, _CallPerSeconds(delaySeconds, intervalSeconds, action));
		}
		public static CoroutineShell CallPerSeconds(MonoBehaviour monoBehaviour, float delaySeconds, float intervalSeconds, Action action)
		{
			return Process(monoBehaviour, _CallPerSeconds(delaySeconds, intervalSeconds, action));
		}
		public static CoroutineShell CallPerSeconds(float delaySeconds, float intervalSeconds, Action action)
		{
			return Process(_CallPerSeconds(delaySeconds, intervalSeconds, action));
		}

		/// <summary>
		/// LateUpdate タイミングで処理デリゲートを呼び出すコルーチン.
		/// </summary>
		/// <param name="action">LateUpdate タイミングで行う処理.</param>
		/// <returns>コルーチン.</returns>
		public static void CallWaitForLateUpdate(CoroutineShell coroutineShell, MonoBehaviour monoBehaviour, Action action)
		{
			Process(coroutineShell, monoBehaviour, _CallOnLateUpdate(action));
		}
		public static CoroutineShell CallWaitForLateUpdate(MonoBehaviour monoBehaviour, Action action)
		{
			return Process(monoBehaviour, _CallOnLateUpdate(action));
		}
		public static CoroutineShell CallWaitForLateUpdate(Action action)
		{
			return Process(_CallOnLateUpdate(action));
		}

		/// <summary>
		/// EndOfFrame タイミングで処理デリゲートを呼び出すコルーチン.
		/// </summary>
		/// <param name="action">EndOfFrame タイミングで行う処理.</param>
		/// <returns>コルーチン.</returns>
		public static void CallWaitForEndOfFrame(CoroutineShell coroutineShell, MonoBehaviour monoBehaviour, Action action)
		{
			Process(coroutineShell, monoBehaviour, _CallOnEndOfFrame(action));
		}
		public static CoroutineShell CallWaitForEndOfFrame(MonoBehaviour monoBehaviour, Action action)
		{
			return Process(monoBehaviour, _CallOnEndOfFrame(action));
		}
		public static CoroutineShell CallWaitForEndOfFrame(Action action)
		{
			return Process(_CallOnEndOfFrame(action));
		}
		#endregion

		//======================================================================================================================
		// Static Private Method
		//======================================================================================================================
		#region regular-coroutine-implementation
		/// <summary>
		/// n フレーム待機してから処理デリゲートを呼び出すコルーチン.
		/// </summary>
		/// <param name="seconds">待機フレーム数.</param>
		/// <param name="action">指定されたフレーム数経過後に行う処理.</param>
		/// <returns>コルーチン.</returns>
		private static IEnumerator _CallWaitForFrames(int frames, Action action)
		{
			for (int i = 0; i < frames; i++) {
				yield return null;
			}
			action.Call();
		}

		/// <summary>
		/// n 秒待機してから処理デリゲートを呼び出すコルーチン.
		/// </summary>
		/// <param name="seconds">待機秒数.</param>
		/// <param name="action">指定された秒数経過後に行う処理.</param>
		/// <returns>コルーチン.</returns>
		private static IEnumerator _CallWaitForSeconds(float seconds, Action action)
		{
			for (float elapsedTime = 0; elapsedTime < seconds; elapsedTime += Time.deltaTime) {
				yield return null;
			}
			action.Call();
		}

		/// <summary>
		/// n 秒待機してから処理デリゲートを呼び出すコルーチン. Realtime 版.
		/// </summary>
		/// <param name="seconds">待機秒数.</param>
		/// <param name="action">指定された秒数経過後に行う処理.</param>
		/// <returns>コルーチン.</returns>
		private static IEnumerator _CallWaitForSecondsRealtime(float seconds, Action action)
		{
			for (float elapsedTime = 0; elapsedTime < seconds; elapsedTime += Time.unscaledDeltaTime) {
				yield return null;
			}
			action.Call();
		}

		/// <summary>
		/// 条件を満たしたら処理デリゲートを呼び出すコルーチン.
		/// </summary>
		/// <param name="condition">条件判定式.</param>
		/// <param name="action">条件を満たした際に行う処理.</param>
		/// <returns>コルーチン.</returns>
		private static IEnumerator _CallWaitForCondition(Func<bool> condition, Action action)
		{
			while (!condition()) {
				yield return null;
			}
			action.Call();
		}

		/// <summary>
		/// タスク実行完了したら処理デリゲートを呼び出すコルーチン.
		/// </summary>
		/// <param name="task">タスク.</param>
		/// <param name="action">タスク実行完了した際に行う処理.</param>
		/// <returns>コルーチン.</returns>
		private static IEnumerator _CallWaitForTask(Action<Action> task, Action action)
		{
			bool isWaiting = true;
			task(() => isWaiting = false);
			while (isWaiting) {
				yield return null;
			}
			action.Call();
		}

		/// <summary>
		/// AsyncOperation 完了したら処理デリゲートを呼び出すコルーチン.
		/// </summary>
		/// <param name="asyncOperation">AsyncOperation</param>
		/// <param name="action">タスク実行完了した際に行う処理.</param>
		/// <returns>コルーチン.</returns>
		private static IEnumerator _CallWaitForAsyncOperation(AsyncOperation asyncOperation, Action action)
		{
			yield return asyncOperation;
			action.Call();
		}

		/// <summary>
		/// FPS 安定したら処理デリゲートを呼び出すコルーチン.
		/// </summary>
		/// <param name="timeOutSeconds">待機タイムアウト秒数.</param>
		/// <param name="action">処理デリゲート.</param>
		/// <returns>コルーチン.</returns>
		private static IEnumerator _CallWaitForStableFps(float timeoutSeconds, Action action)
		{
			// 変化率閾値
			const float kStableRatio = 0.01f;

			// 安定フレーム数閾値
			const int kStableCount = 3;

			// 安定したフレーム数
			int stableCount = 0;

			// 開始時間. タイムアウト用.
			float startTime = Time.time;

			while (true) {
				// 現 FPS
				float currentFps = (Time.deltaTime > 0) ? (1f / Time.deltaTime) : float.MaxValue;

				// 変化率が kStableRatio 以下
				if (Mathf.Abs((Application.targetFrameRate / currentFps) - 1f) < kStableRatio) {
					++stableCount;
				}
				else {
					stableCount = 0;
				}

				// 安定
				if (stableCount >= kStableCount) {
					break;
				}

				// タイムアウト
				if (Time.time - startTime >= timeoutSeconds) {
					break;
				}

				yield return null;
			}
			action.Call();
		}

		/// <summary>
		/// n フレーム経過する度に処理デリゲートを呼び出すコルーチン. MonoBehaviour.InvokeRepeating() の Action デリゲート版.
		/// </summary>
		/// <param name="delayFrames">初回呼び出しの遅延フレーム数.</param>
		/// <param name="intervalFrames">２回目からの呼び出し間隔フレーム数.</param>
		/// <param name="action">処理デリゲート.</param>
		/// <returns>コルーチン.</returns>
		private static IEnumerator _CallPerFrames(int delayFrames, int intervalFrames, Action action)
		{
			float fc = delayFrames;
			// delay for first calling
			while (fc > 0) {
				yield return null;
				fc--;
			}

			// repeat
			while (true) {
				action.Call();

				fc = intervalFrames;
				while (fc > 0) {
					yield return null;
					fc--;
				}
			}
		}

		/// <summary>
		/// n 秒経過する度に処理デリゲートを呼び出すコルーチン. MonoBehaviour.InvokeRepeating() の Action デリゲート版.
		/// </summary>
		/// <param name="delaySeconds">初回呼び出しの遅延秒数.</param>
		/// <param name="intervalSeconds">２回目からの呼び出し間隔秒数.</param>
		/// <param name="action">処理デリゲート</param>
		/// <returns>コルーチン.</returns>
		private static IEnumerator _CallPerSeconds(float delaySeconds, float intervalSeconds, Action action)
		{
			float tc = delaySeconds;

			// delay for first calling
			while (tc > 0) {
				yield return null;
				tc -= Time.deltaTime;
			}

			// repeat
			while (true) {
				action.Call();

				tc = intervalSeconds;
				while (tc > 0) {
					yield return null;
					tc -= Time.deltaTime;
				}
			}
		}

		/// <summary>
		/// 次の LateUpdate で処理デリゲートを呼び出すコルーチン.
		/// </summary>
		/// <param name="action">行う処理.</param>
		/// <returns>コルーチン.</returns>
		private static IEnumerator _CallOnLateUpdate(Action action)
		{
			yield return WaitForLateUpdate.Default;
			action.Call();
		}

		/// <summary>
		/// 次の EndOfFrame で処理デリゲートを呼び出すコルーチン.
		/// </summary>
		/// <param name="action">行う処理.</param>
		/// <returns>コルーチン.</returns>
		private static IEnumerator _CallOnEndOfFrame(Action action)
		{
			yield return ms_WaitForEndOfFrame;
			action.Call();
		}
		#endregion
	}
}
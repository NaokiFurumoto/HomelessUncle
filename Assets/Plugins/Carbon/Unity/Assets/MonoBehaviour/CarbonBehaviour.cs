/*
 * ワールド座標系の操作は非推奨. ゲーム全体調整する時痛い目に遭う.
 */
//#define ENABLE_WORLD_COORDINATE_EXTENSIONS

using System;
using System.Collections;
using UnityEngine;

namespace Carbon
{
	public class CarbonBehaviour : MonoBehaviour, IRoutine
	{
		//======================================================================================================================
		// Component.transform Cache
		//======================================================================================================================
		/// <summary>
		/// Cache of base.transform.
		/// </summary>
		protected Transform m_TransformCache = null;
		/// <summary>
		/// Cache of base.transform.
		/// </summary>
		public new Transform transform {
			get {
				return m_TransformCache
					? m_TransformCache
					: m_TransformCache = GetComponent<Transform>();
			}
		}

		/// <summary>
		/// World position.
		/// </summary>
		public Vector3 worldPosition {
			get { return transform.position; }
			set { transform.position = value; }
		}
		/// <summary>
		/// World rotation.
		/// </summary>
		public Quaternion worldRotation {
			get { return transform.rotation; }
			set { transform.rotation = value; }
		}
		/// <summary>
		/// World rotation in euler-angles.
		/// </summary>
		public Vector3 worldEulerAngles {
			get { return transform.eulerAngles; }
			set { transform.eulerAngles = value; }
		}
		/// <summary>
		/// World scale.
		/// </summary>
		public Vector3 worldScale {
			get { return transform.lossyScale; }
		}

		/// <summary>
		/// Local position.
		/// </summary>
		public Vector3 localPosition {
			get { return transform.localPosition; }
			set { transform.localPosition = value; }
		}
		/// <summary>
		/// Local rotation.
		/// </summary>
		public Quaternion localRotation {
			get { return transform.localRotation; }
			set { transform.localRotation = value; }
		}
		/// <summary>
		/// Local rotation in euler-angles.
		/// </summary>
		public Vector3 localEulerAngles {
			get { return transform.localEulerAngles; }
			set { transform.localEulerAngles = value; }
		}
		/// <summary>
		/// Local scale.
		/// </summary>
		public Vector3 localScale {
			get { return transform.localScale; }
			set { transform.localScale = value; }
		}
		/// <summary>
		/// Layer.
		/// </summary>
		public int layer {
			get { return gameObject.layer; }
			set { gameObject.layer = value; }
		}

		//======================================================================================================================
		// Routine
		//======================================================================================================================
		#region Routine
		/// <summary>
		/// Life
		/// </summary>
		bool IRoutine.IsAlive { get { return this; } }
		/// <summary>
		/// IUpdateRoutine.OnUpdate() and ILateUpdateRoutine.OnLateUpdate() are called while IsRunnable.
		/// </summary>
		bool IRoutine.IsRunnable { get { return isActiveAndEnabled; } }
		#endregion

		//======================================================================================================================
		// Coroutine Extensions
		//======================================================================================================================
		/// <summary>
		/// コルーチンを実行します.
		/// </summary>
		/// <param name="coroutineShell">コルーチンをハンドルするシェル.</param>
		/// <param name="routine">コルーチン.</param>
		public void ProcessCoroutine(CoroutineShell coroutineShell, IEnumerator coroutine)
		{
			CoroutineManager.Process(coroutineShell, this, coroutine);
		}
		/// <summary>
		/// コルーチンを実行します.
		/// </summary>
		/// <param name="routine">コルーチン.</param>
		/// <returns>コルーチン.</returns>
		public CoroutineShell ProcessCoroutine(IEnumerator coroutine)
		{
			return CoroutineManager.Process(this, coroutine);
		}

		#region Coroutine
		/// <summary>
		/// 1 フレーム待機してから処理デリゲートを呼び出します.
		/// </summary>
		/// <param name="coroutineShell">コルーチンをハンドルするシェル.</param>
		/// <param name="action">1 フレーム経過後に行う処理.</param>
		public void CallWaitForOneFrame(CoroutineShell coroutineShell, Action action)
		{
			CoroutineManager.CallWaitForOneFrame(coroutineShell, this, action);
		}
		/// <summary>
		/// 1 フレーム待機してから処理デリゲートを呼び出します.
		/// </summary>
		/// <param name="action">1 フレーム経過後に行う処理.</param>
		/// <returns>コルーチン.</returns>
		public CoroutineShell CallWaitForOneFrame(Action action)
		{
			return CoroutineManager.CallWaitForOneFrame(this, action);
		}

		/// <summary>
		/// n フレーム待機してから処理デリゲートを呼び出します.
		/// </summary>
		/// <param name="coroutineShell">コルーチンをハンドルするシェル.</param>
		/// <param name="seconds">待機フレーム数.</param>
		/// <param name="action">指定されたフレーム数経過後に行う処理.</param>
		public void CallWaitForFrames(CoroutineShell coroutineShell, int frames, Action action)
		{
			CoroutineManager.CallWaitForFrames(coroutineShell, this, frames, action);
		}
		/// <summary>
		/// n フレーム待機してから処理デリゲートを呼び出します.
		/// </summary>
		/// <param name="seconds">待機フレーム数.</param>
		/// <param name="action">指定されたフレーム数経過後に行う処理.</param>
		/// <returns>コルーチン.</returns>
		public CoroutineShell CallWaitForFrames(int frames, Action action)
		{
			return CoroutineManager.CallWaitForFrames(this, frames, action);
		}

		/// <summary>
		/// n 秒待機してから処理デリゲートを呼び出します.
		/// </summary>
		/// <param name="coroutineShell">コルーチンをハンドルするシェル.</param>
		/// <param name="seconds">待機秒数.</param>
		/// <param name="action">指定された秒数経過後に行う処理.</param>
		public void CallWaitForSeconds(CoroutineShell coroutineShell, float seconds, Action action)
		{
			CoroutineManager.CallWaitForSeconds(coroutineShell, this, seconds, action);
		}
		/// <summary>
		/// n 秒待機してから処理デリゲートを呼び出します.
		/// </summary>
		/// <param name="seconds">待機秒数.</param>
		/// <param name="action">指定された秒数経過後に行う処理.</param>
		/// <returns>コルーチン.</returns>
		public CoroutineShell CallWaitForSeconds(float seconds, Action action)
		{
			return CoroutineManager.CallWaitForSeconds(this, seconds, action);
		}

		/// <summary>
		/// n 秒待機してから処理デリゲートを呼び出します. Realtime 版.
		/// </summary>
		/// <param name="coroutineShell">コルーチンをハンドルするシェル.</param>
		/// <param name="seconds">待機秒数.</param>
		/// <param name="action">指定された秒数経過後に行う処理.</param>
		public void CallWaitForSecondsRealtime(CoroutineShell coroutineShell, float seconds, Action action)
		{
			CoroutineManager.CallWaitForSecondsRealtime(coroutineShell, this, seconds, action);
		}
		/// <summary>
		/// n 秒待機してから処理デリゲートを呼び出します. Realtime 版.
		/// </summary>
		/// <param name="seconds">待機秒数.</param>
		/// <param name="action">指定された秒数経過後に行う処理.</param>
		/// <returns>コルーチン.</returns>
		public CoroutineShell CallWaitForSecondsRealtime(float seconds, Action action)
		{
			return CoroutineManager.CallWaitForSecondsRealtime(this, seconds, action);
		}

		/// <summary>
		/// 条件を満たしたら処理デリゲートを呼び出します.
		/// </summary>
		/// <param name="coroutineShell">コルーチンをハンドルするシェル.</param>
		/// <param name="condition">条件関数.</param>
		/// <param name="action">条件を満たした際に行う処理.</param>
		public void CallWaitForCondition(CoroutineShell coroutineShell, Func<bool> condition, Action action)
		{
			CoroutineManager.CallWaitForCondition(coroutineShell, this, condition, action);
		}
		/// <summary>
		/// 条件を満たしたら処理デリゲートを呼び出します.
		/// </summary>
		/// <param name="condition">条件関数.</param>
		/// <param name="action">条件を満たした際に行う処理.</param>
		/// <returns>コルーチン.</returns>
		public CoroutineShell CallWaitForCondition(Func<bool> condition, Action action)
		{
			return CoroutineManager.CallWaitForCondition(this, condition, action);
		}

		/// <summary>
		/// タスク実行完了したら処理デリゲートを呼び出します.
		/// </summary>
		/// <param name="coroutineShell">コルーチンをハンドルするシェル.</param>
		/// <param name="task">タスク.</param>
		/// <param name="action">タスク実行完了した際に行う処理.</param>
		public void CallWaitForTask(CoroutineShell coroutineShell, Action<Action> task, Action action)
		{
			CoroutineManager.CallWaitForTask(coroutineShell, this, task, action);
		}
		/// <summary>
		/// タスク実行完了したら処理デリゲートを呼び出します.
		/// </summary>
		/// <param name="task">タスク.</param>
		/// <param name="action">タスク実行完了した際に行う処理.</param>
		/// <returns>コルーチン.</returns>
		public CoroutineShell CallWaitForTask(Action<Action> task, Action action)
		{
			return CoroutineManager.CallWaitForTask(this, task, action);
		}

		/// <summary>
		/// AsyncOperation 完了したら処理デリゲートを呼び出します.
		/// </summary>
		/// <param name="coroutineShell">コルーチンをハンドルするシェル.</param>
		/// <param name="asyncOperation">AsyncOperation</param>
		/// <param name="action">タスク実行完了した際に行う処理.</param>
		public void CallWaitForAsyncOperation(CoroutineShell coroutineShell, AsyncOperation asyncOperation, Action action)
		{
			CoroutineManager.CallWaitForAsyncOperation(coroutineShell, this, asyncOperation, action);
		}
		/// <summary>
		/// AsyncOperation 完了したら処理デリゲートを呼び出します.
		/// </summary>
		/// <param name="asyncOperation">AsyncOperation</param>
		/// <param name="action">タスク実行完了した際に行う処理.</param>
		/// <returns>コルーチン.</returns>
		public CoroutineShell CallWaitForAsyncOperation(AsyncOperation asyncOperation, Action action)
		{
			return CoroutineManager.CallWaitForAsyncOperation(this, asyncOperation, action);
		}

		/// <summary>
		/// FPS 安定したら処理デリゲートを呼び出します. 0.5 ~ 1.0 秒がおすすめ.
		/// </summary>
		/// <param name="coroutineShell">コルーチンをハンドルするシェル.</param>
		/// <param name="timeOutSeconds">待機タイムアウト秒数.</param>
		/// <param name="action">処理デリゲート.</param>
		public void CallWaitForStableFps(CoroutineShell coroutineShell, float timeoutSeconds, Action action)
		{
			CoroutineManager.CallWaitForStableFps(coroutineShell, this, timeoutSeconds, action);
		}
		/// <summary>
		/// FPS 安定したら処理デリゲートを呼び出します. 0.5 ~ 1.0 秒がおすすめ.
		/// </summary>
		/// <param name="timeOutSeconds">待機タイムアウト秒数.</param>
		/// <param name="action">処理デリゲート.</param>
		/// <returns>コルーチン.</returns>
		public CoroutineShell CallWaitForStableFps(float timeoutSeconds, Action action)
		{
			return CoroutineManager.CallWaitForStableFps(this, timeoutSeconds, action);
		}

		/// <summary>
		/// n フレーム経過する度に処理デリゲートを呼び出します. MonoBehaviour.InvokeRepeating() の Action デリゲート版.
		/// </summary>
		/// <param name="coroutineShell">コルーチンをハンドルするシェル.</param>
		/// <param name="delayFrames">初回呼び出しの遅延フレーム数.</param>
		/// <param name="intervalFrames">２回目からの呼び出し間隔フレーム数.</param>
		/// <param name="action">処理デリゲート.</param>
		public void CallPerFrames(CoroutineShell coroutineShell, int delayFrames, int intervalFrames, Action action)
		{
			CoroutineManager.CallPerFrames(coroutineShell, this, delayFrames, intervalFrames, action);
		}
		/// <summary>
		/// n フレーム経過する度に処理デリゲートを呼び出します. MonoBehaviour.InvokeRepeating() の Action デリゲート版.
		/// </summary>
		/// <param name="delayFrames">初回呼び出しの遅延フレーム数.</param>
		/// <param name="intervalFrames">２回目からの呼び出し間隔フレーム数.</param>
		/// <param name="action">処理デリゲート.</param>
		/// <returns>コルーチン.</returns>
		public CoroutineShell CallPerFrames(int delayFrames, int intervalFrames, Action action)
		{
			return CoroutineManager.CallPerFrames(this, delayFrames, intervalFrames, action);
		}

		/// <summary>
		/// n 秒経過する度に処理デリゲートを呼び出します. MonoBehaviour.InvokeRepeating() の Action デリゲート版.
		/// </summary>
		/// <param name="coroutineShell">コルーチンをハンドルするシェル.</param>
		/// <param name="delaySeconds">初回呼び出しの遅延秒数.</param>
		/// <param name="intervalSeconds">２回目からの呼び出し間隔秒数.</param>
		/// <param name="action">処理デリゲート</param>
		public void CallPerSeconds(CoroutineShell coroutineShell, float delaySeconds, float intervalSeconds, Action action)
		{
			CoroutineManager.CallPerSeconds(coroutineShell, this, delaySeconds, intervalSeconds, action);
		}
		/// <summary>
		/// n 秒経過する度に処理デリゲートを呼び出します. MonoBehaviour.InvokeRepeating() の Action デリゲート版.
		/// </summary>
		/// <param name="delaySeconds">初回呼び出しの遅延秒数.</param>
		/// <param name="intervalSeconds">２回目からの呼び出し間隔秒数.</param>
		/// <param name="action">処理デリゲート</param>
		/// <returns>コルーチン.</returns>
		public CoroutineShell CallPerSeconds(float delaySeconds, float intervalSeconds, Action action)
		{
			return CoroutineManager.CallPerSeconds(this, delaySeconds, intervalSeconds, action);
		}

		/// <summary>
		/// LateUpdate タイミングで処理デリゲートを呼び出すコルーチン.
		/// </summary>
		/// <param name="coroutineShell">コルーチンをハンドルするシェル.</param>
		/// <param name="action">LateUpdate タイミングで行う処理.</param>
		public void CallWaitForLateUpdate(CoroutineShell coroutineShell, Action action)
		{
			CoroutineManager.CallWaitForLateUpdate(coroutineShell, this, action);
		}
		/// <summary>
		/// LateUpdate タイミングで処理デリゲートを呼び出すコルーチン.
		/// </summary>
		/// <param name="action">LateUpdate タイミングで行う処理.</param>
		/// <returns>コルーチン.</returns>
		public CoroutineShell CallWaitForLateUpdate(Action action)
		{
			return CoroutineManager.CallWaitForLateUpdate(this, action);
		}

		/// <summary>
		/// EndOfFrame タイミングで処理デリゲートを呼び出すコルーチン.
		/// </summary>
		/// <param name="coroutineShell">コルーチンをハンドルするシェル.</param>
		/// <param name="action">EndOfFrame タイミングで行う処理.</param>
		public void CallWaitForEndOfFrame(CoroutineShell coroutineShell, Action action)
		{
			CoroutineManager.CallWaitForEndOfFrame(coroutineShell, this, action);
		}
		/// <summary>
		/// EndOfFrame タイミングで処理デリゲートを呼び出すコルーチン.
		/// </summary>
		/// <param name="action">EndOfFrame タイミングで行う処理.</param>
		/// <returns>コルーチン.</returns>
		public CoroutineShell CallWaitForEndOfFrame(Action action)
		{
			return CoroutineManager.CallWaitForEndOfFrame(this, action);
		}
		#endregion
	}
}
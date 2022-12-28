using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Carbon
{
	public abstract class DataNodeBase<T> : CarbonBehaviour, IDataNode<T>, IDataRelayNode, IDataReceiverNode<T>
	{
		//======================================================================================================
		// SerializeField
		//======================================================================================================
		/// <summary>
		/// 中継フラグ
		/// </summary>
		[SerializeField] private bool m_Relay = false;
		/// <summary>
		/// 無効時の中継フラグ
		/// </summary>
		[SerializeField] private bool m_RelayWhileDisable = false;
		/// <summary>
		/// ランタイム時レシーバーをフェッチする
		/// </summary>
		[SerializeField] private bool m_StaticReceiverComponentList = false;
		/// <summary>
		/// レシーバーリスト
		/// </summary>
		[FixInInspector]
		[SerializeField] private Component[] m_ReceiverComponentList = new Component[0];

		//======================================================================================================
		// Public Property
		//======================================================================================================
		/// <summary>
		/// データ
		/// </summary>
		public T Data { get; private set; }
		/// <summary>
		/// 中継フラグ
		/// </summary>
		public bool Relay { get { return m_Relay; } set { m_Relay = value; } }
		/// <summary>
		/// 無効時の中継フラグ
		/// </summary>
		public bool RelayWhileDisable { get { return m_RelayWhileDisable; } set { m_RelayWhileDisable = value; } }

		//======================================================================================================
		// MonoBehaviour
		//======================================================================================================
#if UNITY_EDITOR
		private void OnValidate()
		{
			if (m_StaticReceiverComponentList)
			{
				if (m_ReceiverComponentList.Length == 0)
				{
					m_ReceiverComponentList = FetchReceiverComponentsInChildren();
				}
			}
			else
			{
				if (m_ReceiverComponentList.Length != 0)
				{
					m_ReceiverComponentList = new Component[0];
				}
			}
		}
#endif

		/// <summary>
		/// OnDestroy 時処理
		/// </summary>
		protected virtual void DoOnDestroy() { }
		private void OnDestroy()
		{
			DoOnDestroy();
		}

		//======================================================================================================
		// Public Method
		//======================================================================================================
		/// <summary>
		/// 子孫レシーバーリスト更新
		/// </summary>
		public void RefreshReceiverList()
		{
			m_ReceiverComponentList = FetchReceiverComponentsInChildren();
		}

		/// <summary>
		/// 指定 Data を登録.
		/// </summary>
		public void SetData(T data)
		{
			Data = data;
		}

		/// <summary>
		/// 所持 Data を拡散する.
		/// </summary>
		public void Broadcast(Action onComplete = null)
		{
			var receiverList = m_StaticReceiverComponentList
				? m_ReceiverComponentList
				: m_ReceiverComponentList.Any()
					? m_ReceiverComponentList
					: m_ReceiverComponentList = FetchReceiverComponentsInChildren();

			var task = new ParallelTask();
			foreach (var receiver in receiverList)
			{
				task.Push(next => ((IDataReceiverNode<T>)receiver).OnReceive(Data, next));
			}
			task.Process(onComplete);
		}

		/// <summary>
		/// 指定 Data を登録して拡散する.
		/// </summary>
		public void Broadcast(T data, Action onComplete = null)
		{
			SetData(data);
			Broadcast(onComplete);
		}

		//======================================================================================================
		// Interface
		//======================================================================================================
		/// <summary>
		/// Data 受信時処理.
		/// </summary>
		void IDataReceiverNode<T>.OnReceive(T data, Action onComplete)
		{
			if (!m_Relay) {
				onComplete.Call();
				return;
			}

			if (!m_RelayWhileDisable && !isActiveAndEnabled) {
				onComplete.Call();
				return;
			}

			Broadcast(data, onComplete);
		}

		//======================================================================================================
		// Private Method
		//======================================================================================================
		/// <summary>
		/// 子孫レシーバーをフェッチ
		/// </summary>
		private Component[] FetchReceiverComponentsInChildren()
		{
			var receiverHash = new HashSet<Component>();

			Transform relayNodeTransform = null;

			foreach (var tf in GetComponentsInChildren<Transform>(true))
			{
				// relay-node の子供判定 (DFS 前提)
				if (relayNodeTransform)
				{
					// 子供であればスキップ
					if (tf.IsChildOf(relayNodeTransform))
					{
						continue;
					}
					relayNodeTransform = null;
				}

				// component 抽出
				var componentList = tf.GetComponents(typeof(IDataReceiverNode<T>));

				// 子供の場合
				if (tf != transform)
				{
					// relay-node 存在チェック
					if (componentList.Any(n => n is IDataRelayNode))
					{
						relayNodeTransform = tf;
					}

					receiverHash.AddRange(componentList);
				}
				// 自分自身の場合
				else
				{
					// 他の relay-node を除外 (relay-node 間 recursive call 防止)
					receiverHash.AddRange(componentList.Where(n => n is IDataRelayNode == false));
				}
			}

			return receiverHash.ToArray();
		}
	}
}

using System;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Carbon
{
	[Obsolete("未検証")]
	public abstract class DataObserverNodeBase<T> : MonoBehaviour, IDataNode<T>
	{
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// SerializeField
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 観察対象 GameObject
		/// </summary>
		[SerializeField] private GameObject m_NodeToObserve = null;
		/// <summary>
		/// 通知対象 GameObject. 対象 GameObject にアタッチされた全ての IDataReceiverNode<T> が通知を受ける.
		/// </summary>
		[SerializeField] private GameObject m_NodeToNotify = null;

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Field
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 観察対象 Node
		/// </summary>
		private IDataNode<T> m_SrcNode = null;
		/// <summary>
		/// 観察 stream
		/// </summary>
		private IDisposable m_ObserveStream = null;

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Property
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// データ
		/// </summary>
		public T Data { get; private set; } = default(T);

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// MonoBehaviour
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// OnDestroy 時処理
		/// </summary>
		protected virtual void DoOnDestroy() { }
		private void OnDestroy()
		{
			DoOnDestroy();

			if (m_ObserveStream != null) {
				m_ObserveStream.Dispose();
				m_ObserveStream = null;
			}

			m_SrcNode = null;
		}

		/// <summary>
		/// Start 時処理
		/// </summary>
		protected virtual void DoStart() { }
		private void Start()
		{
			if (m_ObserveStream == null) {
				Observe();
			}

			DoStart();
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Public Method
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 観察対象 GameObject を設定.
		/// </summary>
		public void SetNodeToObserve(GameObject go)
		{
			SetupSrcNode(go);
		}

		/// <summary>
		/// 通知対象 GameObject を設定. 対象 GameObject にアタッチされた全ての IDataReceiverNode<T> が通知を受ける.
		/// </summary>
		public void SetNodeToNotify(GameObject go)
		{
			m_NodeToNotify = go;
		}

		/// <summary>
		/// 観察
		/// </summary>
		public void Observe()
		{
			if (m_ObserveStream != null) {
				m_ObserveStream.Dispose();
				m_ObserveStream = null;
			}

			SetupSrcNode(m_NodeToObserve);
			if (m_SrcNode == null) {
				return;
			}

			m_ObserveStream = m_SrcNode
				.ObserveEveryValueChanged(x => x.Data)
				.Where(x => enabled)
				.Subscribe(_ => Notify())
				.AddTo(this);
		}

		/// <summary>
		/// 通知
		/// </summary>
		public void Notify()
		{
			SetupSrcNode(m_NodeToObserve);

			if (m_NodeToNotify) {
				m_NodeToNotify.GetComponents<Component>()
					.Select	(n => n as IDataReceiverNode<T>)
					.Where	(n => n != null)
					.ForEach(n => n.OnReceive(Data, null));
			}
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Private Method
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void SetupSrcNode(GameObject nextGo)
		{
			if (nextGo == null) {
				Data = default(T);
				return;
			}

			if (nextGo == m_NodeToObserve) {
				return;
			}

			m_NodeToObserve = nextGo;
			m_SrcNode = m_NodeToObserve.GetComponents<Component>()
				.Select(n => n as IDataNode<T>)
				.FirstOrDefault(n => n != null);

			Data = (m_SrcNode != null) ? m_SrcNode.Data : default(T);
		}
	}
}

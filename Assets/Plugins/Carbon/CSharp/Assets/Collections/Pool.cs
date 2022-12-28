using System;
using System.Collections.Generic;

namespace Carbon.Collections
{
	/// <summary>
	/// Pool
	/// </summary>
	[Serializable]
	public class Pool<T> where T : class
	{
		/// <summary>
		/// Function to create element.
		/// </summary>
		private Func<T> m_SpawnFunc = null;
		/// <summary>
		/// Buffer list.
		/// </summary>
		private readonly List<T> m_BufferList = new List<T>();
		/// <summary>
		/// Active hash.
		/// </summary>
		private readonly HashSet<T> m_ActiveHash = new HashSet<T>();

		/// <summary>
		/// Default Constructor.
		/// </summary>
		public Pool() { }

		/// <summary>
		/// Construct with spawn function.
		/// </summary>
		/// <param name="spawnFunc">Function to create element.</param>
		public Pool(Func<T> spawnFunc)
		{
			m_SpawnFunc = spawnFunc;
		}

		/// <summary>
		/// Construct with given elements.
		/// </summary>
		/// <param name="collection">Element collection.</param>
		public Pool(IEnumerable<T> collection)
		{
			m_BufferList.AddRange(collection);
		}

		/// <summary>
		/// Add element into pool's buffer.
		/// </summary>
		/// <param name="element">Element to add.</param>
		public void Add(T element)
		{
			m_BufferList.Add(element);
		}

		/// <summary>
		/// Spawn element.
		/// </summary>
		/// <returns>Element spawned.</returns>
		public T Spawn()
		{
			if (m_BufferList.IsEmpty()) {
				if (!TryAdd()) {
					return null;
				}
			}

			T ret = m_BufferList.PopFirst();
			m_ActiveHash.Add(ret);
			return ret;
		}

		/// <summary>
		/// Despawn element and put it back to buffer if it's a member of pool.
		/// </summary>
		/// <param name="element">Element to despawn.</param>
		public void Despawn(T element)
		{
			if (m_ActiveHash.Contains(element)) {
				m_ActiveHash.Remove(element);
				m_BufferList.Add(element);
			}
		}

		/// <summary>
		/// Despawn all member elements and put them back to buffer.
		/// </summary>
		public void DespawnAll()
		{
			m_BufferList.AddRange(m_ActiveHash);
			m_ActiveHash.Clear();
		}

		/// <summary>
		/// ForEach free element.
		/// </summary>
		/// <param name="action">ForEach action.</param>
		public void ForEachFree(Action<T> action)
		{
			m_BufferList.ForEach(action);
		}

		/// <summary>
		/// ForEach free element.
		/// </summary>
		/// <param name="action">ForEach action.</param>
		public void ForEachFree(Action<T, int> action)
		{
			m_BufferList.ForEach(action);
		}

		/// <summary>
		/// ForEach used element.
		/// </summary>
		/// <param name="action">ForEach action.</param>
		public void ForEachUsed(Action<T> action)
		{
			m_ActiveHash.ForEach(action);
		}

		/// <summary>
		/// ForEach used element.
		/// </summary>
		/// <param name="action">ForEach action.</param>
		public void ForEachUsed(Action<T, int> action)
		{
			m_ActiveHash.ForEach(action);
		}

		private bool TryAdd()
		{
			T obj = m_SpawnFunc.Call();
			if (obj == null) {
				return false;
			}
			m_BufferList.Add(obj);
			return true;
		}
	}
}

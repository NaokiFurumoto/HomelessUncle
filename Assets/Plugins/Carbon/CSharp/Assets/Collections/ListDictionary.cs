using System.Linq;
using System.Collections.Generic;

namespace Carbon
{
	/// <summary>
	/// Dictionary with values stored in a list.
	/// <para>Pros: Lighter value-accessing, especially on sequential processing.</para>
	/// <para>Cons: Heavier element-removement (except clear all). More memory cost.</para>
	/// </summary>
	public sealed class ListDictionary<TKey, TValue>
	{
		//======================================================================================================================
		// Field
		//======================================================================================================================
		private readonly List<TValue>			m_List;
		private readonly Dictionary<TKey, int>	m_ListIndexDictionary;

		//======================================================================================================================
		// Property
		//======================================================================================================================
		public TValue this[TKey key]
		{
			get
			{
				return m_List[m_ListIndexDictionary[key]];
			}
			set
			{
				m_List[m_ListIndexDictionary[key]] = value;
			}
		}
		public int							Count			=> m_List.Count;
		public IReadOnlyList<TValue>		ValueList		=> m_List;
		public IReadOnlyCollection<TKey>	KeyCollection	=> m_ListIndexDictionary.Keys;
		public IEqualityComparer<TKey>		KeyComparer		=> m_ListIndexDictionary.Comparer;

		#region Constructor
		//======================================================================================================================
		// Constructor
		//======================================================================================================================
		public ListDictionary()
		{
			m_List = new List<TValue>();
			m_ListIndexDictionary = new Dictionary<TKey, int>();
		}
		public ListDictionary(IDictionary<TKey, TValue> dictionary)
		{
			m_List = new List<TValue>();
			m_ListIndexDictionary = new Dictionary<TKey, int>();
			foreach (var kvp in dictionary) {
				m_ListIndexDictionary.Add(kvp.Key, m_List.Count);
				m_List.Add(kvp.Value);
			}
		}
		public ListDictionary(IEqualityComparer<TKey> keyComparer)
		{
			m_List = new List<TValue>();
			m_ListIndexDictionary = new Dictionary<TKey, int>(keyComparer);
		}
		public ListDictionary(int capacity)
		{
			m_List = new List<TValue>(capacity);
			m_ListIndexDictionary = new Dictionary<TKey, int>(capacity);
		}
		public ListDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> keyComparer)
		{
			m_List = new List<TValue>();
			m_ListIndexDictionary = new Dictionary<TKey, int>(keyComparer);
			foreach (var kvp in dictionary) {
				m_ListIndexDictionary.Add(kvp.Key, m_List.Count);
				m_List.Add(kvp.Value);
			}
		}
		public ListDictionary(int capacity, IEqualityComparer<TKey> keyComparer)
		{
			m_List = new List<TValue>(capacity);
			m_ListIndexDictionary = new Dictionary<TKey, int>(capacity, keyComparer);
		}
		#endregion

		//======================================================================================================================
		// Public Method (const)
		//======================================================================================================================
		public bool IsEmpty()
		{
			return Count == 0;
		}

		public bool Any()
		{
			return Count > 0;
		}

		/// <summary>
		/// Return a value that indicates whether the given key exists.
		/// </summary>
		public bool ContainsKey(TKey key)
		{
			return m_ListIndexDictionary.ContainsKey(key);
		}

		/// <summary>
		/// Inversion of 'ContainsKey()'.
		/// </summary>
		public bool NotContainsKey(TKey key)
		{
			return !m_ListIndexDictionary.ContainsKey(key);
		}

		/// <summary>
		/// Return a value that indicates whether the given value exists.
		/// </summary>
		public bool ContainsValue(TValue value)
		{
			return m_List.Contains(value);
		}

		/// <summary>
		/// Inversion of 'ContainsValue()'.
		/// </summary>
		public bool NotContainsValue(TValue value)
		{
			return !m_List.Contains(value);
		}

		//======================================================================================================================
		// Public Method
		//======================================================================================================================
		/// <summary>
		/// Add a (key, value) pair and return true if the given key does not exists.
		/// </summary>
		public bool Add(TKey key, TValue value)
		{
			if (m_ListIndexDictionary.ContainsKey(key)) {
				return false;
			}

			m_ListIndexDictionary.Add(key, m_List.Count);
			m_List.Add(value);
			return true;
		}

		/// <summary>
		/// Add a (key, value) pair and return true if the given key does not exists. Otherwise, update the value of (key, value) pair and return false.
		/// </summary>
		public bool SetValue(TKey key, TValue value)
		{
			int index;
			if (m_ListIndexDictionary.TryGetValue(key, out index)) {
				m_List[index] = value;
				return false;
			}

			m_ListIndexDictionary.Add(key, m_List.Count);
			m_List.Add(value);
			return true;
		}

		/// <summary>
		/// Remove (key, value) pair with the given key and return true if the given key exists.
		/// </summary>
		public bool Remove(TKey key)
		{
			int index;
			if (m_ListIndexDictionary.TryGetValue(key, out index) == false)
			{
				return false;
			}

			var kList = new TKey[m_ListIndexDictionary.Count];
			m_ListIndexDictionary.Keys.CopyTo(kList, 0);
			foreach (var k in kList)
			{
				if (m_ListIndexDictionary[k] > index)
				{
					m_ListIndexDictionary[k]--;
				}
			}

			m_ListIndexDictionary.Remove(key);
			m_List.RemoveAt(index);

			return true;
		}

		/// <summary>
		/// Remove all elements.
		/// </summary>
		public void Clear()
		{
			m_ListIndexDictionary.Clear();
			m_List.Clear();
		}

		/// <summary>
		/// Remove unused buffer for value-list.
		/// </summary>
		public void TrimExcess()
		{
			m_List.TrimExcess();
		}

		/// <summary>
		/// Try to get the value for the given key. 'value' is set and return true if the given key exists.
		/// </summary>
		public bool TryGetValue(TKey key, out TValue value)
		{
			int index;
			if (m_ListIndexDictionary.TryGetValue(key, out index)) {
				value = m_List[index];
				return true;
			}
			value = default;
			return false;
		}

		/// <summary>
		/// Return the value for the given key. Return 'defaultValue' if such key does not exist.
		/// </summary>
		public TValue GetValueOrDefault(TKey key, TValue defaultValue = default)
		{
			TValue value;
			return TryGetValue(key, out value) ? value : defaultValue;
		}

		/// <summary>
		/// Try to get the value for the given key. 'value' is set and return true if the given key exists.
		/// </summary>
		public bool TryGetIndex(TKey key, out int index)
		{
			return m_ListIndexDictionary.TryGetValue(key, out index);
		}

		/// <summary>
		/// Return the value for the given key. Return 'defaultValue' if such key does not exist.
		/// </summary>
		public int GetIndexOrDefault(TKey key, int defaultIndex = -1)
		{
			int index;
			return TryGetIndex(key, out index) ? index : defaultIndex;
		}
	}
}

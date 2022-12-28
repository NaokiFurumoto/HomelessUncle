using System;
using System.Collections.Generic;

namespace Carbon.Collections
{
	public class ListBuffer<T> where T : new()
	{
		//======================================================================================================================
		// Field
		//======================================================================================================================
		private readonly List<T> m_FreeList = new List<T>();
		private readonly List<T> m_UsedList = new List<T>();

		private bool m_FixesCapacity = false;

		private Func<T>			m_CreateFunc			= null;
		private Func<int, T>	m_CreateFuncWithIndex	= null;

		//======================================================================================================================
		// Property
		//======================================================================================================================
		public IReadOnlyList<T>	FreeList	=> m_FreeList;
		public int				FreeCount	=> m_FreeList.Count;

		public IReadOnlyList<T>	UsedList	=> m_UsedList;
		public int				UsedCount	=> m_UsedList.Count;

		public T	this[int index]	=> m_UsedList[index];
		public int	Count			=> m_UsedList.Count;

		//======================================================================================================================
		// Public Method [Create/Release]
		//======================================================================================================================
		public void CreateBuffer(int capacity, bool fixesCapacity)
		{
			GenerateBuffer(capacity, fixesCapacity);

			for (int i = 0; i < capacity; i++) {
				m_FreeList.Add(new T());
			}

			m_CreateFunc = null;
			m_CreateFuncWithIndex = null;
		}

		public void CreateBuffer(int capacity, bool fixesCapacity, Func<T> createFunc)
		{
			GenerateBuffer(capacity, fixesCapacity);

			for (int i = 0; i < capacity; i++) {
				m_FreeList.Add(createFunc());
			}

			m_CreateFunc = createFunc;
			m_CreateFuncWithIndex = null;
		}

		public void CreateBuffer(int capacity, bool fixesCapacity, Func<int, T> createFunc)
		{
			GenerateBuffer(capacity, fixesCapacity);

			for (int i = 0; i < capacity; i++) {
				m_FreeList.Add(createFunc(i));
			}

			m_CreateFunc = null;
			m_CreateFuncWithIndex = createFunc;
		}

		public void ReleaseBuffer()
		{
			m_UsedList.Clear();
			m_FreeList.Clear();
		}

		//======================================================================================================================
		// Public Method [Allocate]
		//======================================================================================================================
		/// <summary>
		/// Allocate a new element with the index for UsedList as output.
		/// </summary>
		public T Alloc(out int usedListIndex)
		{
			T element = default;

			int freeListIndex = m_FreeList.Count - 1;
			if (freeListIndex < 0) {
				if (m_FixesCapacity) {
					usedListIndex = -1;
					return default;
				}

				if (m_CreateFunc != null) {
					element = m_CreateFunc();
				}
				else if (m_CreateFuncWithIndex != null) {
					int newIndex = m_UsedList.Count;
					element = m_CreateFuncWithIndex(newIndex);
				}
				else {
					element = new T();
				}
			}
			else {
				element = m_FreeList[freeListIndex];
				m_FreeList.RemoveAt(freeListIndex);
			}

			m_UsedList.Add(element);
			usedListIndex = m_UsedList.Count - 1;
			return element;
		}

		/// <summary>
		/// Allocate a new element.
		/// </summary>
		public T Alloc()
		{
			int index;
			return Alloc(out index);
		}

		//======================================================================================================================
		// Public Method [Free]
		//======================================================================================================================
		/// <summary>
		/// Free the element at given index for the UsedList.
		/// </summary>
		public void FreeAt(int index)
		{
			if (index < 0 || index >= m_UsedList.Count) {
				return;
			}

			m_FreeList.Add(m_UsedList[index]);
			m_UsedList.RemoveAt(index);
		}

		/// <summary>
		/// Free the first element.
		/// </summary>
		public void FreeFirst()
		{
			FreeAt(0);
		}

		/// <summary>
		/// Free the first element equals to the given element. Note that boxing occurs if T is not IEquatable.
		/// </summary>
		public void FreeFirst(T element)
		{
			FreeAt(m_UsedList.IndexOf(element));
		}

		/// <summary>
		/// Free the first element matches the given predicate.
		/// </summary>
		public void FreeFirst(Predicate<T> match)
		{
			FreeAt(m_UsedList.FindIndex(match));
		}

		/// <summary>
		/// Free the last element.
		/// </summary>
		public void FreeLast()
		{
			FreeAt(m_UsedList.Count - 1);
		}

		/// <summary>
		/// Free the last element equals to the given element. Note that boxing occurs if T is not IEquatable.
		/// </summary>
		public void FreeLast(T element)
		{
			FreeAt(m_UsedList.LastIndexOf(element));
		}

		/// <summary>
		/// Free the last element matches the given predicate.
		/// </summary>
		public void FreeLast(Predicate<T> match)
		{
			FreeAt(m_UsedList.FindLastIndex(match));
		}

		/// <summary>
		/// Free all elements.
		/// </summary>
		public void FreeAll()
		{
			m_FreeList.AddRange(m_UsedList);
			m_UsedList.Clear();
		}

		/// <summary>
		/// Free all elements equal to the given element. Note that boxing occurs if T is not IEquatable.
		/// </summary>
		public void FreeAll(T element)
		{
			for (int i = m_UsedList.Count - 1; i >= 0; i--) {
				T t = m_UsedList[i];
				if (t.Equals(element)) {
					m_FreeList.Add(t);
					m_UsedList.RemoveAt(i);
				}
			}
		}

		/// <summary>
		/// Free all elements match the given predicate.
		/// </summary>
		public void FreeAll(Predicate<T> match)
		{
			for (int i = m_UsedList.Count - 1; i >= 0; i--) {
				T t = m_UsedList[i];
				if (match(t)) {
					m_FreeList.Add(t);
				}
			}
			m_UsedList.RemoveAll(match);
		}

		//======================================================================================================================
		// Private Method
		//======================================================================================================================
		private void GenerateBuffer(int capacity, bool fixesCapacity)
		{
			ReleaseBuffer();

			m_FreeList.Capacity = capacity;
			m_UsedList.Capacity = capacity;

			m_FixesCapacity = fixesCapacity;
		}
	}
}

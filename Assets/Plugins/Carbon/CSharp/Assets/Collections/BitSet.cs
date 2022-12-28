using System;
using System.Collections.Generic;
using System.Linq;

namespace Carbon.Collections
{
	/// <summary>
	/// メモリ節約版 HashSet<int>
	/// </summary>
	[Serializable]
	public class BitSet
	{
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Definition
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Bit per word(Int32).
		/// </summary>
		protected const int kBitsPerWord = 32;

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Field
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Buffer.
		/// </summary>
		protected readonly List<int> m_WordList;

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Constructor
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Default constructor.
		/// </summary>
		public BitSet()
		{
			m_WordList = new List<int>();
		}
		/// <summary>
		/// Construct with a bit collection.
		/// </summary>
		/// <param name="indexCollection">Bit collection to construct.</param>
		public BitSet(IEnumerable<int> indexCollection)
		{
			m_WordList = new List<int>();
			AddRange(indexCollection);
		}
		/// <summary>
		/// Construct with bits.
		/// </summary>
		/// <param name="indices">Bits to construct.</param>
		public BitSet(params int[] indices) : this(indices as IEnumerable<int>) {}
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Public Method
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Clear all bits.
		/// </summary>
		public void Clear()
		{
			m_WordList.Clear();
		}
		/// <summary>
		/// Add bit.
		/// </summary>
		/// <param name="index">Index of bit to add.</param>
		/// <returns>False if given index less than 0 or exists. Otherwise, true.</returns>
		public bool Add(int index)
		{
			if (index < 0) {
				return false;
			}

			int wordIndex = index / kBitsPerWord;
			int bitIndex = index % kBitsPerWord;

			// 拡張
			for (int i = m_WordList.Count; i <= wordIndex; i++) {
				m_WordList.Add(0);
			}

			int orWord = (0x1 << bitIndex);

			if ((m_WordList[wordIndex] & orWord) > 0) {
				return false;
			}

			m_WordList[wordIndex] |= orWord;
			return true;
		}
		/// <summary>
		/// Add bits.
		/// </summary>
		/// <param name="indexCollection">Index collection of bits to add.</param>
		public void AddRange(IEnumerable<int> indexCollection)
		{
			int maxIndex = indexCollection.Max();
			int maxWordIndex = maxIndex / kBitsPerWord;
			if (maxWordIndex < 0) {
				return;
			}

			// 拡張
			for (int i = m_WordList.Count; i <= maxWordIndex; i++) {
				m_WordList.Add(0);
			}

			foreach (int index in indexCollection) {
				int wordIndex = index / kBitsPerWord;
				int bitIndex = index % kBitsPerWord;
				m_WordList[wordIndex] |= (0x1 << bitIndex);
			}
		}
		/// <summary>
		/// Add bits
		/// </summary>
		/// <param name="indices">Indices of bits to add.</param>
		public void AddRange(params int[] indices)
		{
			AddRange(indices as IEnumerable<int>);
		}
		/// <summary>
		/// Clear and then Add.
		/// </summary>
		/// <param name="index">Index of bit to set.</param>
		public void Set(int index)
		{
			Clear();
			Add(index);
		}
		/// <summary>
		/// Clear and then AddRange.
		/// </summary>
		/// <param name="indexCollection">Index collection of bits to set.</param>
		public void Set(IEnumerable<int> indexCollection)
		{
			Clear();
			AddRange(indexCollection);
		}
		/// <summary>
		/// Clear and then AddRange.
		/// </summary>
		/// <param name="indices">Indices of bits to set.</param>
		public void Set(params int[] indices)
		{
			Set(indices as IEnumerable<int>);
		}
		/// <summary>
		/// Return a value that indicates whether this BitSet contains the given bit.
		/// </summary>
		/// <param name="index">Index of bit to check.</param>
		/// <returns>True if contains.</returns>
		public bool Contains(int index)
		{
			if (index < 0) {
				return false;
			}

			int wordIndex = index / kBitsPerWord;
			if (wordIndex >= m_WordList.Count) {
				return false;
			}

			int bitIndex = index % kBitsPerWord;

			return ((m_WordList[wordIndex] & (0x1 << bitIndex)) > 0);
		}
		/// <summary>
		/// Return a value that indicates whether this BitSet contains all bits in the given BitSet.
		/// </summary>
		/// <param name="other">BitSet to check.</param>
		/// <returns>True if contains all.</returns>
		public bool ContainsAll(BitSet other)
		{
			int thisCount = m_WordList.Count;

			List<int> otherList = other.m_WordList;

			for (int i = 0; i < otherList.Count; i++) {
				int x = otherList[i];
				int y = (i < thisCount) ? m_WordList[i] : 0;
				if ((x & y) != x) {
					return false;
				}
			}

			return true;
		}
		/// <summary>
		/// Return a value that indicates whether this BitSet contains all bits in the given collection.
		/// </summary>
		/// <param name="indexCollection">Index collection of bits to check.</param>
		/// <returns>True if contains all.</returns>
		public bool ContainsAll(IEnumerable<int> indexCollection)
		{
			return ContainsAll(new BitSet(indexCollection));
		}
		/// <summary>
		/// Return a value that indicates whether this BitSet contains all given bits.
		/// </summary>
		/// <param name="indices">Indices of bits to check.</param>
		/// <returns>True if contains all.</returns>
		public bool ContainsAll(params int[] indices)
		{
			return ContainsAll(new BitSet(indices));
		}
		/// <summary>
		/// Return a value that indicates whether this BitSet contains any bit in the given BitSet.
		/// </summary>
		/// <param name="other">BitSet to check.</param>
		/// <returns>True if contains any.</returns>
		public bool ContainsAny(BitSet other)
		{
			int thisCount = m_WordList.Count;

			List<int> otherList = other.m_WordList;

			for (int i = 0; i < otherList.Count; i++) {
				int x = otherList[i];
				int y = (i < thisCount) ? m_WordList[i] : 0;
				if ((x & y) > 0) {
					return true;
				}
			}

			return false;
		}
		/// <summary>
		/// Return a value that indicates whether this BitSet contains any bit in the given collection.
		/// </summary>
		/// <param name="indexCollection">Index collection of bits to check.</param>
		/// <returns>True if contains any.</returns>
		public bool ContainsAny(IEnumerable<int> indexCollection)
		{
			return ContainsAny(new BitSet(indexCollection));
		}
		/// <summary>
		/// Return a value that indicates whether this BitSet contains any given bits.
		/// </summary>
		/// <param name="indices">Indices of bits to check.</param>
		/// <returns>True if contains any.</returns>
		public bool ContainsAny(params int[] indices)
		{
			foreach (int index in indices) {
				if (Contains(index)) {
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// Return a value that indicates whether this BitSet contains bits in the given BitSet only.
		/// </summary>
		/// <param name="other">BitSet to check.</param>
		/// <returns>True if contains only.</returns>
		public bool ContainsOnly(BitSet other)
		{
			// boost
			// this is empty -> this should not contains what other contains.
			if (m_WordList.Count <= 0) {
				return true;
			}

			// boost
			// other is empty -> this should contains other bits.
			if (other.m_WordList.Count <= 0) {
				return false;
			}

			IList<int> otherList = other.m_WordList;
			int otherCount = otherList.Count;

			for (int i = 0; i <= m_WordList.Count; i++) {
				int x = (i < otherCount) ? otherList[i] : 0;
				int y = m_WordList[i];
				if ((x ^ y) != 0) {
					return false;
				}
			}

			return true;
		}
		/// <summary>
		/// Return a value that indicates whether this BitSet contains bits in the given collection only.
		/// </summary>
		/// <param name="indexCollection">Index collection of bits to check.</param>
		/// <returns>True if contains only.</returns>
		public bool ContainsOnly(IEnumerable<int> indexCollection)
		{
			return ContainsOnly(new BitSet(indexCollection));
		}
		/// <summary>
		/// Return a value that indicates whether this BitSet contains given bits only.
		/// </summary>
		/// <param name="indices">Indices of bits to check.</param>
		/// <returns>True if contains only.</returns>
		public bool ContainsOnly(params int[] indices)
		{
			return ContainsOnly(new BitSet(indices));
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �V���A���C�Y�\�� Dictionary
/// </summary>
/// <typeparam name="TKey">�L�[�̌^</typeparam>
/// <typeparam name="TValue">�l�̌^</typeparam>
/// <typeparam name="TPair">�V���A���C�Y�\�� KeyValuePair</typeparam>
[Serializable]
public abstract class SerializableDictionarySystem<TKey, TValue, TPair> : IEnumerable<TPair>
	where TPair : SerializableKeyValuePair<TKey, TValue>, new()
{
	/// <summary>
	/// �f�[�^���X�g
	/// </summary>
	[SerializeField] private List<TPair> m_List = new List<TPair>();

	/// <summary>
	/// �e�[�u��
	/// </summary>
	private Dictionary<TKey, TValue> m_Table;

	/// <summary>
	/// IEqualityComparer �쐬
	/// </summary>
	private IEqualityComparer<TKey> m_KeyComparer = null;

	/// <summary>
	/// Constructor
	/// </summary>
	public SerializableDictionarySystem() { }
	public SerializableDictionarySystem(IEqualityComparer<TKey> keyComparer) { m_KeyComparer = keyComparer; }

	/// <summary>
	/// �w�肳�ꂽ�L�[�ɕR�t���l��Ԃ��܂�
	/// </summary>
	public TValue this[TKey key]
	{
		get
		{
			var pair = m_List.Find(c => c.Key.Equals(key));
			if (pair == null)
			{
				return default(TValue);
			}
			//Debug.Assert( pair != null, "�w�肳�ꂽ�L�[�ɕR�t���l�͑��݂��܂���Bkey = " + key );
			return pair.Value;
		}
	}

	/// <summary>
	/// Dictionary ��Ԃ��܂�
	/// </summary>
	public Dictionary<TKey, TValue> Table { get { return m_Table ?? (m_Table = ListToDictionary(m_List, m_KeyComparer)); } }

	/// <summary>
	/// mList �� Dictionary �ɕϊ����ĕԂ��܂�
	/// </summary>
	private static Dictionary<TKey, TValue> ListToDictionary(IList<TPair> list, IEqualityComparer<TKey> keyComparer)
	{
		var dict = (keyComparer != null) ? new Dictionary<TKey, TValue>(keyComparer) : new Dictionary<TKey, TValue>();
		foreach (var n in list)
		{
			dict.Add(n.Key, n.Value);
		}
		return dict;
	}

	/// <summary>
	/// �R���N�V�����𔽕���������񋓎q��Ԃ��܂�
	/// </summary>
	IEnumerator<TPair> IEnumerable<TPair>.GetEnumerator()
	{
		foreach (var n in m_List)
		{
			yield return n;
		}
	}

	/// <summary>
	/// �R���N�V�����𔽕���������񋓎q��Ԃ��܂�
	/// </summary>
	IEnumerator IEnumerable.GetEnumerator()
	{
		foreach (var n in m_List)
		{
			yield return n;
		}
	}
}

/// <summary>
/// �V���A���C�Y�\�� KeyValuePair
/// </summary>
/// <typeparam name="TKey">�L�[�̌^</typeparam>
/// <typeparam name="TValue">�l�̌^</typeparam>
[Serializable]
public abstract class SerializableKeyValuePair<TKey, TValue>
{
	[SerializeField] private TKey m_Key;   // �L�[
	[SerializeField] private TValue m_Value;   // �l

	/// <summary>
	/// �L�[��Ԃ��܂�
	/// </summary>
	public TKey Key { get { return m_Key; } }

	/// <summary>
	/// �l��Ԃ��܂�
	/// </summary>
	public TValue Value { get { return m_Value; } }

	/// <summary>
	/// �R���X�g���N�^
	/// </summary>
	public SerializableKeyValuePair() { }

	/// <summary>
	/// �R���X�g���N�^
	/// </summary>
	/// <param name="key">�L�[</param>
	/// <param name="value">�l</param>
	public SerializableKeyValuePair(TKey key, TValue value)
	{
		m_Key = key;
		m_Value = value;
	}
}

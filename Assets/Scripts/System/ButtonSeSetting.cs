using Carbon;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonSeKey
{
	None,

	Decide00,
	Cancel00,
	Select00,
	Back00,

	// ���� (�g��)
	Decide01,
	Decide02,
	Decide03,

	// �L�����Z�� (�g��)
	Cancel01,
	Cancel02,
	Cancel03,

	// �I�� (�g��)
	Select01,
	Select02,
	Select03,

	// �߂� (�g��)
	Back01,
	Back02,
	Back03,
}

/// <summary>
/// EXCHANGE_TYPE Comparer
/// </summary>
public sealed class ButtonSeKeyComparer : IEqualityComparer<ButtonSeKey>
{
	public bool Equals(ButtonSeKey x, ButtonSeKey y)
	{
		return x == y;
	}

	public int GetHashCode(ButtonSeKey obj)
	{
		return (int)obj;
	}
}

public sealed class ButtonSeSetting : ScriptableObject
{
	[Serializable]
	public class ButtonSeKeyValuePair : SerializableKeyValuePair<ButtonSeKey, string> { }
	[Serializable]
	public class ButtonSeDictionary : SerializableDictionarySystem<ButtonSeKey, string, ButtonSeKeyValuePair>
	{
		public ButtonSeDictionary(IEqualityComparer<ButtonSeKey> keyComparer) : base(keyComparer) { }
	}

	/// <summary>
	/// �f�[�^
	/// </summary>
	[SerializeField] private ButtonSeDictionary m_ButtonSeTable = new ButtonSeDictionary(new ButtonSeKeyComparer());

	///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Static
	///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// �ݒ� instance
	/// </summary>
	private static ButtonSeSetting ms_Instance;

	/// <summary>
	/// Static�R���X�g���N�^�����s���邽�߂����̓ǂݍ��݊֐�
	/// </summary>
	public static void Load()
	{
		ms_Instance = Instantiate(Resources.Load<ButtonSeSetting>("ButtonSeSettings"));
	}
}

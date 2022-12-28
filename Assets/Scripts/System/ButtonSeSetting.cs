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

	// 決定 (拡張)
	Decide01,
	Decide02,
	Decide03,

	// キャンセル (拡張)
	Cancel01,
	Cancel02,
	Cancel03,

	// 選択 (拡張)
	Select01,
	Select02,
	Select03,

	// 戻る (拡張)
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
	/// データ
	/// </summary>
	[SerializeField] private ButtonSeDictionary m_ButtonSeTable = new ButtonSeDictionary(new ButtonSeKeyComparer());

	///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Static
	///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// 設定 instance
	/// </summary>
	private static ButtonSeSetting ms_Instance;

	/// <summary>
	/// Staticコンストラクタを実行するためだけの読み込み関数
	/// </summary>
	public static void Load()
	{
		ms_Instance = Instantiate(Resources.Load<ButtonSeSetting>("ButtonSeSettings"));
	}
}

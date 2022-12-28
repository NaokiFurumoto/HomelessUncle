using Carbon;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Shock
{
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

	/// <summary>
	/// ボタンサウンド設定
	/// </summary>
	public sealed class ButtonSeSettings : ScriptableObject
	{
		[Serializable]
		public class ButtonSeKeyValuePair : SerializableKeyValuePair<ButtonSeKey, string> { }
		[Serializable]
		public class ButtonSeDictionary : SerializableDictionary<ButtonSeKey, string, ButtonSeKeyValuePair>
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
		private static ButtonSeSettings ms_Instance;

		/// <summary>
		/// SE ID 取得
		/// </summary>
		//public static string GetSoundKey(ButtonSeKey key)
		//{
		//	//if (ms_Instance) {
		//	//	return ms_Instance.m_ButtonSeTable.Table.GetValueOrDefault(key, "");
		//	//}
		//	//return "";
		//}

		/// <summary>
		/// Staticコンストラクタを実行するためだけの読み込み関数
		/// </summary>
		public static void Load()
		{
			ms_Instance = Instantiate(Resources.Load<ButtonSeSettings>("ButtonSeSettings"));
		}
	}
}
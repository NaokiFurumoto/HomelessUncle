using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shock
{
	/// <summary>
	/// シリアライズ可能な Dictionary
	/// </summary>
	/// <typeparam name="TKey">キーの型</typeparam>
	/// <typeparam name="TValue">値の型</typeparam>
	/// <typeparam name="TPair">シリアライズ可能な KeyValuePair</typeparam>
	[Serializable]
	public abstract class SerializableDictionary<TKey, TValue, TPair> : IEnumerable<TPair>
		where TPair : SerializableKeyValuePair<TKey, TValue>, new()
	{
		/// <summary>
		/// データリスト
		/// </summary>
		[SerializeField] private List<TPair> m_List = new List<TPair>();

		/// <summary>
		/// テーブル
		/// </summary>
		private Dictionary<TKey, TValue> m_Table;

		/// <summary>
		/// IEqualityComparer 作成
		/// </summary>
		private IEqualityComparer<TKey> m_KeyComparer = null;

		/// <summary>
		/// Constructor
		/// </summary>
		public SerializableDictionary() {}
		public SerializableDictionary(IEqualityComparer<TKey> keyComparer) { m_KeyComparer = keyComparer; }

		/// <summary>
		/// 指定されたキーに紐付く値を返します
		/// </summary>
		public TValue this[TKey key] {
			get {
				var pair = m_List.Find(c => c.Key.Equals(key));
				if (pair == null) {
					return default(TValue);
				}
				//Debug.Assert( pair != null, "指定されたキーに紐付く値は存在しません。key = " + key );
				return pair.Value;
			}
		}

		/// <summary>
		/// Dictionary を返します
		/// </summary>
		public Dictionary<TKey, TValue> Table { get { return m_Table ?? (m_Table = ListToDictionary(m_List, m_KeyComparer)); } }

		/// <summary>
		/// mList を Dictionary に変換して返します
		/// </summary>
		private static Dictionary<TKey, TValue> ListToDictionary(IList<TPair> list, IEqualityComparer<TKey> keyComparer)
		{
			var dict = (keyComparer != null) ? new Dictionary<TKey, TValue>(keyComparer) : new Dictionary<TKey, TValue>();
			foreach (var n in list) {
				dict.Add(n.Key, n.Value);
			}
			return dict;
		}

		/// <summary>
		/// コレクションを反復処理する列挙子を返します
		/// </summary>
		IEnumerator<TPair> IEnumerable<TPair>.GetEnumerator()
		{
			foreach (var n in m_List) {
				yield return n;
			}
		}

		/// <summary>
		/// コレクションを反復処理する列挙子を返します
		/// </summary>
		IEnumerator IEnumerable.GetEnumerator()
		{
			foreach (var n in m_List) {
				yield return n;
			}
		}
	}

	/// <summary>
	/// シリアライズ可能な KeyValuePair
	/// </summary>
	/// <typeparam name="TKey">キーの型</typeparam>
	/// <typeparam name="TValue">値の型</typeparam>
	[Serializable]
    public abstract class SerializableKeyValuePair<TKey, TValue>
    {
        [SerializeField] private TKey   m_Key    ;   // キー
        [SerializeField] private TValue m_Value  ;   // 値

        /// <summary>
        /// キーを返します
        /// </summary>
        public TKey Key { get { return m_Key; } }

        /// <summary>
        /// 値を返します
        /// </summary>
        public TValue Value { get { return m_Value; } }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SerializableKeyValuePair(){}
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="value">値</param>
        public SerializableKeyValuePair( TKey key, TValue value )
        {
            m_Key    = key   ;
            m_Value  = value ;
        }
    }
}
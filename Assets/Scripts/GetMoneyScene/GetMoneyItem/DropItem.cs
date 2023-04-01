using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// 共通のドロップアイテムデータ
/// </summary>
namespace GetMoney
{
    [Serializable]
    [CreateAssetMenu(fileName = "DropItemData", menuName = "Create_DropItemData")]
    public class DropItem : ScriptableObject
    {
        //ドロップアイテムレア率
        public enum DROPITEM_RARITY
        {
            N = 0, R = 1, SR = 2, SSR = 3, UR = 4,
        }

        /// <summary> アイテム画像 </summary>
        [SerializeField]
        [Header("アイテム画像")]
        private Sprite itemSprite;

        /// <summary> 名前 </summary>
        [SerializeField]
        [Header("アイテム名")]
        private string itemName;

        /// <summary> 名前 </summary>
        [SerializeField]
        [Header("レア度")]
        private DROPITEM_RARITY rarity;

        /// <summary> 売値 </summary>
        [SerializeField]
        [Header("売値")]
        private int sellPrice;

        public Sprite ItemSprite { get { return itemSprite; } set { itemSprite = value; } }
        public string ItemName => itemName;
        public DROPITEM_RARITY Rarity => rarity;
        public int SellPrice { get { return sellPrice; } set { sellPrice = value; } }

    }
}

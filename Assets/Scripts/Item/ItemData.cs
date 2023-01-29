using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
[CreateAssetMenu(fileName = "ItemData", menuName = "Create_ItemData")]
public class ItemData : ScriptableObject
{
    /// <summary> アイテムの種類 </summary>
    public enum ITEM_TYPE
    {
        SHOPITEM,   //ショップ
        FISH,       //魚
        DIG         //掘る
    }

    /// <summary> レアリティ </summary>
    public enum ITEM_RARITY
    {
        N = 0, R = 1, SR = 2, SSR = 3, UR = 4
    }

    /// <summary> アイテムの種類 </summary>
    [SerializeField] [Header("アイテムの種類")]
    private ITEM_TYPE itemType;

    /// <summary> アイコン画像 </summary>
    [SerializeField]
    [Header("アイコン画像")]
    private Sprite iconImage;

    /// <summary> アイテム画像 </summary>
    [SerializeField][Header("アイテム画像")]
    private Sprite itemSprite;

    /// <summary> 名前 </summary>
    [SerializeField][Header("アイテム名")]
    private string itemName;

    /// <summary> 名前 </summary>
    [SerializeField]
    [Header("レア度")]
    private ITEM_RARITY rarity;

    /// <summary> 情報 </summary>
    [SerializeField][Header("アイテムの説明")]
    [TextArea(1, 6)]
    private string itemInfo;

    /// <summary> 最大所持数 </summary>
    [SerializeField][Header("最大所持数")]
    private int maxHaveIndex;

    /// <summary> 買うことが出来る </summary>
    [SerializeField][Header("買うことが出来る")]
    private bool isBuy;

    /// <summary> 売ることが出来る </summary>
    [SerializeField][Header("売ることが出来る")]
    private bool isSell;

    /// <summary> 使う事が出来る </summary>
    [SerializeField]
    [Header("使うことが出来る")]
    private bool isUse;

    /// <summary> 買値 </summary>
    [SerializeField][Header("買値")]
    private int price;

    /// <summary> 売値 </summary>
    [SerializeField][Header("売値")]
    private int sellPrice;

    /// <summary> コレクションアイテムかどうか </summary>
    [SerializeField][Header("コレクションアイテムかどうか")]
    private bool isCollection;

    /// <summary> 効果 </summary>
    [SerializeField][Header("効果")]
    private ItemAbility itemAbility;

    public ITEM_TYPE ItemType => itemType;
    public Sprite IconImage => iconImage;
    public Sprite ItemSprite => itemSprite;
    public string ItemName => itemName;
    public ITEM_RARITY Rarity => rarity;
    public string ItemInfo => itemInfo;
    public int MaxHaveIndex => maxHaveIndex;
    public bool IsSell => isSell;
    public bool IsBuy => isBuy;
    public bool IsUse => isUse;
    public int Price => price;
    public int SellPrice => sellPrice;
    public bool IsCollection => isCollection;
    public bool IsAbility => itemAbility.IsHaveAbility;
    public ItemAbility Ability => itemAbility;


    /// <summary> 初期化 </summary>
    private void Awake()
    {
        if (!isBuy) price = 0;
        if (!isSell || isCollection) sellPrice = 0;
        if (!IsAbility) itemAbility.StatusClear();
    }

    /// <summary>
    /// レアリティを文字列で取得する
    /// </summary>
    /// <returns></returns>
    public string GetRareText()
    {
        return rarity.ToString();
    }

    /// <summary>
    /// アイテムの効果
    /// </summary>
    [Serializable]
    public class ItemAbility
    {
        /// <summary> アビリティをもっているか </summary>
        [SerializeField]
        private bool isHaveAbility;

        /// <summary> 所持して効果を発揮 </summary>
        [SerializeField]
        private bool isHoldEffective;
        /*
         体力減算値
        臭み減算値
        効果時間
         
         */
        [SerializeField]
        [Header("発掘_レア率アップ")]
        private int digRareRateUp;

        /// <summary> 情報 </summary>
        [SerializeField]
        [Header("アビリティの説明")]
        [TextArea(1, 6)]
        private string abilityInfo;

        public bool IsHaveAbility => isHaveAbility;
        public bool IsHoldEffective => isHoldEffective;

        public int DigRareRateUp => digRareRateUp;
        public string AbilityInfo => abilityInfo;
        /// <summary>
        /// ステータスのクリア
        /// </summary>
        public void StatusClear()
        {

        }

    }
}



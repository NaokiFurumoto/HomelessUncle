using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// 共通のアイテムデータ。拡張元
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "ItemData", menuName = "Create_ItemData")]
public class ItemData : ScriptableObject
{
    /// <summary> アイテムの種類 </summary>
    public enum ITEM_TYPE
    {
        SAO,        //竿
        FISH,       //魚
        ESA,        //エサ
        UKI,        //ウキ
        SHOP,       //ショップ
    }

    /// <summary> レアリティ </summary>
    public enum ITEM_RARITY
    {
        N = 0, R = 1, SR = 2, SSR = 3, UR = 4, NUSHI = 5
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

    /// <summary> 使う事が出来る </summary>
    [SerializeField]
    [Header("装備する事が出来る")]
    private bool isEquip;

    /// <summary> 買値 </summary>
    [SerializeField][Header("買値")]
    private int price;

    /// <summary> 売値 </summary>
    [SerializeField][Header("売値")]
    private int sellPrice;

    /// <summary> スキルがあるかどうか </summary>
    [SerializeField]
    [Header("効果")]
    private bool isSkill;

    /// <summary> 情報 </summary>
    [SerializeField]
    [Header("スキル説明")]
    [TextArea(1, 6)]
    private string skillInfo;

    ///// <summary> 効果 </summary>
    //[SerializeField][Header("効果")]
    //private ItemAbility itemAbility;

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
    public bool IsEquip => isEquip;
    public int Price => price;
    public int SellPrice => sellPrice;
    public bool IsSkill => isSkill;
    public string SkillInfo => skillInfo;

    /// <summary> 初期化 </summary>
    protected virtual void Awake()
    {
        if (!isBuy) price = 0;
        if (!isSell) sellPrice = 0;
        if (!isSkill) skillInfo = "-";
    }

    /// <summary>
    /// レアリティを文字列で取得する
    /// </summary>
    /// <returns></returns>
    public string GetRareText()
    {
        return rarity.ToString();
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// アイテムアイコンのデータ
/// </summary>
public struct ItemIconData
{
    /// <summary> アイコンイメージ </summary>
    public Sprite ItemImage;

    /// <summary> レアリティ </summary>
    public ItemData.ITEM_RARITY Rarity;

    /// <summary> アイコンデータ </summary>
    public ItemData Data;

    /// <summary> 名前 </summary>
    public string ItemName;

    /// <summary> 所持数 </summary>
    public int HoldIndex;

    /// <summary>
    /// データの設定
    /// </summary>
    /// <param name="data"></param>
    /// <param name="index"></param>
    public void SetIconsData(ItemData data, int index)
    {
        ItemImage = data.IconImage;
        Rarity = data.Rarity;
        ItemName = data.ItemName;
        HoldIndex = index;
        Data = data;
    }
}

/// <summary>
/// ショップアイコンデータ
/// </summary>
public struct ShopIconData
{
    /// <summary> アイコンイメージ </summary>
    public Sprite ItemImage;

    /// <summary> レアリティ </summary>
    public ItemData.ITEM_RARITY Rarity;

    /// <summary> アイコンデータ </summary>
    public ItemData Data;

    /// <summary> 価格 </summary>
    public int Price;

    /// <summary> 売り切れか </summary>
    public bool IsSoldOut;

    public void SetShopIconData(ItemData data, int index)
    {

    }
}

/// <summary>
/// ショップ詳細アイコンデータ
/// </summary>
public struct ShopIconDetailData
{
    /// <summary> アイコンイメージ </summary>
    public Sprite ItemImage;

    /// <summary> レアリティ </summary>
    public ItemData.ITEM_RARITY Rarity;

    /// <summary> アイコンデータ </summary>
    public ItemData Data;

    /// <summary> 名前 </summary>
    public string ItemName;

    /// <summary> 販売数 </summary>
    public int BuyIndex;

    /// <summary> 商品説明 </summary>
    public string ItemDesc;

    /// <summary> プライスレスバッジ <summary>
    public Sprite BadgePriceLess;

    /// <summary> 割引率 </summary>
    public string Text_Parcent;
}


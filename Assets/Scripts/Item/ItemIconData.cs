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
    public void SetIconsData(ItemData　data,int index)
    {
        ItemImage = data.IconImage;
        Rarity = data.Rarity;
        ItemName = data.ItemName;
        HoldIndex = index;
        Data = data;
    }
}

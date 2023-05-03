using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

/// <summary>
/// ショップアイコンクラス
/// </summary>
public class ShopIcon : ItemIcon
{
    /// <summary> 価格 </summary>
    [SerializeField] private TextMeshProUGUI txt_Price;

    /// <summary> セール中 </summary>
    //[SerializeField] private GameObject badge_Sale;

    /// <summary> 売り切れ </summary>
    [SerializeField] private GameObject badge_SoldOut;

    public override void Setup(ItemIconData iconData, Action<ItemIcon> callback = null)
    {
        base.Setup(iconData, callback);

        var price_value = iconData.Data.Price;
        txt_Price.text = price_value == 0 ? "無料" : "￥" + iconData.Data.Price.ToString();

        //売り切れならば売り切れバッジ表示
        //プレイヤーの所持数がアイテムの最大所持数と同じ条件

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Carbon;
using TMPro;
using System;
/// <summary>
/// アイコンの情報をセット
/// </summary>
public class ItemIcon : ItemIconBase
{
    /// <summary> イメージ </summary>
    [SerializeField]
    private Image itemImage;

    /// <summary> レアイメージ </summary>
    [SerializeField]
    private Image rareImage;

    /// <summary> ボタン </summary>
    [SerializeField]
    private ButtonEventSystem button;

    /// <summary> TextMeshUI </summary>
    [SerializeField]
    private TextMeshProUGUI txt_itemName;

    /// <summary> TextMeshUI </summary>
    [SerializeField]
    private TextMeshProUGUI txt_holdIndex;

    /// <summary> TextMeshUI </summary>
    [SerializeField]
    private ItemBGData bgData;

    /// <summary> データ </summary>
    [SerializeField]
    private ItemData data;

    /// <summary> 名前 </summary>
    private string itemName;

    /// <summary> 所持数 </summary>
    private int holdIndex;

    // <summary> クリックした際のコールバック </summary>
    public Action<ItemIconData> ClickCallback;

    /// <summary> 背景イメージ </summary>
    [SerializeField]
    private Image bgImage;

    /// <summary> レアシート </summary>
    [SerializeField]
    private UISpriteSheet rareSheet;

    /// <summary> 背景レアシート </summary>
    [SerializeField]
    private UISpriteSheet bgSheet;


    //プロパティ
    public string ItemName => itemName;
    public int HoldIndex
    {
        get { return holdIndex; }
        set { holdIndex = value; }
    }
    public Image ItemImage => itemImage;
    public ItemData Data => data;
   
    /// <summary>
    /// アイコンにデータを表示する場合の設定
    /// </summary>
    /// <param name="iconData"></param>
    /// <param name="callback"></param>
    public void Setup(ItemIconData iconData, Action<ItemIcon> callback = null)
    {
        itemImage.sprite = iconData.ItemImage;
        itemName = iconData.ItemName;
        holdIndex = iconData.HoldIndex;
        this.data = iconData.Data;
        bgImage.sprite = bgSheet.GetSprite(this.data.Rarity.ToString());
        rareImage.sprite = rareSheet.GetSprite(this.data.Rarity.ToString());

        txt_itemName.text = itemName.ToString();
        txt_holdIndex.text = holdIndex.ToString();

        if(callback != null)
        {
            button.OnClick = () => callback(this);
        }
    }

    /// <summary>
    /// アイコンにデータを表示しない場合の設定
    /// </summary>
    /// <param name="iconData"></param>
    public void SetupView(ItemData itemData)
    {
        itemImage.sprite = itemData.IconImage;
        bgImage.sprite = bgSheet.GetSprite(itemData.Rarity.ToString());
    }

}

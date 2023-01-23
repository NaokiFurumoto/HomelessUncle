using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Carbon;
using TMPro;
/// <summary>
/// アイコンの情報をセット
/// </summary>
public class ItemIcon : ItemIconBase
{
    /// <summary> イメージ </summary>
    [SerializeField]
    private Image itemImage;

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

    //メンバ変数
    /// <summary> 名前 </summary>
    private string itemName;

    /// <summary> 所持数 </summary>
    private int holdIndex;

    /// <summary> 背景イメージ </summary>
    [SerializeField]
    private Image bgImage;


    //プロパティ
    public string ItemName => itemName;
    public int HoldIndex => holdIndex;
    public Image ItemImage => itemImage;
    public ItemData Data => data;
   

    public void Setup(ItemIconData iconData)
    {
        itemImage.sprite = iconData.ItemImage;
        itemName = iconData.ItemName;
        holdIndex = iconData.HoldIndex;
        this.data = iconData.Data;
        bgImage.sprite = bgData.BGImagesList[(int)this.data.Rarity];

        txt_itemName.text = itemName.ToString();
        txt_holdIndex.text = holdIndex.ToString();
    }

    //ボタンを押すとアイテム詳細画面が出る。そこで売却可能

}

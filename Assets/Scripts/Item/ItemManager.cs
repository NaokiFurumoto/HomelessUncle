using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    [SerializeField]
    private ItemDataBase itemDataBase;

    private List<ItemData> allItemDataLists = new List<ItemData>();
    private List<ItemData> shopItemDataLists = new List<ItemData>();
    private List<ItemData> fishItemDataLists = new List<ItemData>();
    private List<ItemData> digItemDataLists = new List<ItemData>();

    public List<ItemData> AllItemTableLists => allItemDataLists;
    public List<ItemData> ShopItemDataLists => shopItemDataLists;
    public List<ItemData> FishItemDataLists => fishItemDataLists;
    public List<ItemData> DigItemDataLists => digItemDataLists;

    private void Awake()
    {
        Instance ??= this;
        allItemDataLists = itemDataBase.AllItemTableLists;
        shopItemDataLists = allItemDataLists.FindAll(item => item.ItemType == ItemData.ITEM_TYPE.SHOPITEM);
        fishItemDataLists = allItemDataLists.FindAll(item => item.ItemType == ItemData.ITEM_TYPE.FISH);
        digItemDataLists = allItemDataLists.FindAll(item => item.ItemType == ItemData.ITEM_TYPE.DIG);
    }

    /// <summary>
    /// アイテムデータを受け取ってアイテムを作成
    /// アイテム用の空のプレファブを用意
    /// </summary>
    public void CreateItem()
    {

    }

}

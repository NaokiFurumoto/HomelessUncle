using Carbon;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// プレイヤーの所持アイテム画面
/// </summary>
public class PlayerItemView : ViewBase, IUpdateList
{
    /// <summary> 画面 <summary>
    private VIEWTYPE type;

    /// <summary> Player <summary>
    [SerializeField] private Player player;

    /// <summary> 表示数 <summary>
    [SerializeField] private int index;

    //<summary スクロール機能 <summary
    [SerializeField] private ScrollSystemBase<ItemIconBase> scroll;

    /// <summary> 閉じるボタン <summary>
    [SerializeField] private Button btn_Close;

    //表示するアイテムアイコン
    private static List<ItemIconData> itemIconsData = new List<ItemIconData>();


    protected override void Awake()
    {
        base.Awake();
        type = VIEWTYPE.ITEMVIEW;
    }

    protected override void OnEnable()
    {
        //リストの更新
        UpdateList();
    }

    /// <summary>
    /// 外部初期化
    /// </summary>
    protected override void Start()
    {
        player ??= GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        index = player.HaveItemsCount;

        btn_Close.onClick.AddListener(() => animator?.SetTrigger("close"));
    }

    /// <summary>
    /// アイテムの設定
    /// </summary>
    private void SetItemDataList(Action callback)
    {
        //プレイヤーのデータ所持データを取得
        var data = player.PlayerHoldItems;
        var itemManager = ItemManager.Instance;
        if (data == null || itemManager == null)
            return;

        itemIconsData.Clear();
        foreach (var (name, index) in data)
        {
            try
            {
                var itemData = itemManager.AllItemTableLists.Single(list => list.ItemName == name);
                var iconData = new ItemIconData();
                iconData.SetIconsData(itemData, index);
                itemIconsData.Add(iconData);
            }
            catch (IndexOutOfRangeException e)
            {
                DebugUtils.Log("要素が取得できません");
                return;
            }
        }

        callback();
    }

    /// <summary>
    /// アイコン設定
    /// </summary>
    private void SetScrollItemIcon()
    {
        scroll.OnInitializeItem = icon =>
        {
            var itemIcon = icon as ItemIcon;
            if (!itemIcon) return;
            itemIcon.Setup(itemIconsData[icon.Index]);
        };

        scroll.Create(player.HaveItemsCount,null);
    }

    /// <summary>
    /// リストの更新する必要がある画面
    /// </summary>
    public void UpdateList() 
    {
        SetItemDataList(SetScrollItemIcon);
    }
}

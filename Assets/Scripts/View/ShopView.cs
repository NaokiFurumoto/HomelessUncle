using Carbon;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ショップ画面
/// </summary>
public class ShopView : ViewBase, IUpdateList
{
    //■要素
    //スクロール
    //<summary スクロール機能 <summary
    [SerializeField] private ScrollSystemBase<ItemIconBase> scroll;

    /// <summary> shop 表示数 <summary>
    [SerializeField] private int totalItemNum;

    //表示するショップアイコン:ShopIconDataに変更
    private static List<ShopIconData> shopIconsData = new List<ShopIconData>();

    //割引率：タイムセール：時間による：割引パス持ってたら加算

    /// <summary> Player <summary>
    [SerializeField] private Player player;

    /// <summary>
    /// Awake初期化
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        type = VIEWTYPE.SHOPVIEW;
    }

    /// <summary>
    /// Start初期化
    /// </summary>
    protected override void Start()
    {
        base.Start();
        player ??= GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    /// <summary>
    /// 有効時の処理
    /// </summary>
    protected override void OnEnable()
    {
        //リストの更新
        UpdateList();
    }

    private void SetScrollShopIcon()
    {
        scroll.OnInitializeItem = icon =>
        {
            var itemIcon = icon as ItemIcon;
            if (!itemIcon) return;
            //itemIcon.Setup(shopIconsData[icon.Index], OnClickItemDetail);
        };
    }

    /// <summary>
    /// アイテムの設定
    /// </summary>
    private void SetItemDataList(Action callback)
    {
        //プレイヤーのデータ所持データを取得(アイテム名と所持数)
        var data = player.PlayerHoldItems;
        var itemManager = ItemManager.Instance;
        if (itemManager == null)
            return;

        //ショップのデータを取得
        var shopDataList = itemManager.ShopItemDataLists;
        //ショップのアイコンデータリスト
        shopIconsData.Clear();


        //ショップアイコン用データに変換する。
        //ショップのアイテムデータからアイコン用データに変換
        //プレイヤーのアイテム所持数がショップアイテムの最大所持数と同じなら売り切れ
        //








        //foreach (var (name, index) in data)
        //{
        //    try
        //    {
        //        var itemData = itemManager.AllItemTableLists.Single(list => list.ItemName == name);
        //        var iconData = new ShopIconData();
        //        iconData.SetIconsData(itemData, index);
        //        shopIconsData.Add(iconData);
        //    }
        //    catch (IndexOutOfRangeException e)
        //    {
        //        DebugUtils.Log("要素が取得できません");
        //        return;
        //    }
        //}

        //callback();
    }



    //■関数
    //買う処理
    //プレイヤーの所持物から、2つ以上持てないものはsoldOut
    //購入後のプレイヤー所持アイテム追加
    //購入後の保存

    //◆関連
    //購入ダイアログ



    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// リストの更新する必要がある画面
    /// </summary>
    public void UpdateList()
    {
        //SetItemDataList(SetScrollItemIcon);
    }
}

// 親クラス：Shop
public class Shop
{
    protected string name;  // 店名
    protected float discountRate;  // 割引率

    public Shop(string name, float discountRate)
    {
        this.name = name;
        this.discountRate = discountRate;
    }

    // 商品を買う
    public virtual float Buy(float price)
    {
        // 割引を適用する
        float discountedPrice = price * (1 - discountRate);
        Debug.Log(name + "で商品を" + price + "円で購入しました。割引後の価格は" + discountedPrice + "円です。");
        return discountedPrice;
    }
}

// 子クラス：ConvenienceStore
public class ConvenienceStore : Shop
{
    public ConvenienceStore() : base("コンビニエンスストア", 0.1f)
    {
    }

    // ポイントカードを持っている場合はさらに割引
    public override float Buy(float price)
    {
        float discountedPrice = base.Buy(price);

        bool hasPointCard = true;  // 本来はポイントカードを持っているかどうかを判定する処理が必要
        if (hasPointCard)
        {
            discountedPrice *= 0.9f;  // 10%割引
            Debug.Log("ポイントカードを利用してさらに10%割引しました。割引後の価格は" + discountedPrice + "円です。");
        }

        return discountedPrice;
    }
}

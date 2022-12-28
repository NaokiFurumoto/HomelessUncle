using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static GlobalValue;

/// <summary>
/// 掘るアクション：基本共通クラスが欲しい
/// </summary>
public class DigAction : MonoBehaviour
{
    // N=0/R=1/SR=2/SSR=3/UR=4/はずれ=5とする
    [SerializeField]
    private List<int> digRatioList = new List<int>();

    //掘り出しアイテムリスト
    private List<ItemData> digItemDataList = new List<ItemData>();

    /// <summary> 掘り出されたアイテム </summary>
    //private ItemData diggingItem;

    //アクション可能判定
    //private bool isAction;

    #region プロパティ
    //public bool IsAction { get { return isAction; } set { isAction = value; } }
    //public ItemData DiggingItem => diggingItem;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        digItemDataList = ItemManager.Instance.DigItemDataLists;
    }

    #region アイテム設定処理
    /// <summary> 掘り出しアイテムの取得 </summary>
    public ItemData GetDigItem()
    {
        var rareNo = GetRearityNo();
        if (rareNo == DIG_MISSING_NO)
        {
            //はずれ
            return null;
        }
        else
        {
            var items = digItemDataList.Where(item => (int)item.Rarity == rareNo).ToList();
            return SelectDigItem(items);
        }
    }

    /// <summary> 掘り出すレア度取得 </summary>
    private int GetRearityNo()
    {
        var totalRatio = TotalRareRatio();
        var createNum = Random.Range(1, totalRatio + 1);

        var total = 0;
        for (int i = 0; i < digRatioList.Count; i++)
        {
            total += digRatioList[i];
            if (createNum <= total)
            {
                return i;
            }
        }

        return DIG_MISSING_NO;
    }

    /// <summary> レア度確率合計 </summary> バフ：センサー/PlayerItem/環境/購入アイテム
    private int TotalRareRatio()
    {
        //その前にバフを追加設定
        return digRatioList.Sum();
    }

    /// <summary> 掘り出しアイテム取得 </summary>
    private ItemData SelectDigItem(List<ItemData> items = null)
    {
        if (!(items?.Count > 0))
            return null;

        var count = items.Count;
        if (count == 1)
        {
            return items.FirstOrDefault();
        }

        var num = Random.Range(0, count);
        return items[num];
    }
    #endregion

}

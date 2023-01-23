using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static GlobalValue;

public abstract class GameAction : MonoBehaviour
{
    // N=0/R=1/SR=2/SSR=3/UR=4/はずれ=5とする
    [SerializeField]
    protected List<int> ratioList = new List<int>();

    //アイテムリスト
    protected List<ItemData> DataList = new List<ItemData>();

    protected ItemManager itemManager;

    protected Player player;

    IEnumerator Start()
    {
        while (ItemManager.Instance == null)
        {
            yield return null;
        }

        itemManager = ItemManager.Instance;
        Initialize();
    }

    protected virtual void Initialize()
    {
        player = GameObject.FindGameObjectWithTag("Player")
                           .GetComponent<Player>();
    }

    public virtual ItemData GetItem()
    {
        var rareNo = GetRearityNo();
        if (rareNo == MISSING_NO)
        {
            //はずれ
            return null;
        }
        else
        {
            var items = DataList.Where(item => (int)item.Rarity == rareNo).ToList();
            return SelectItem(items);
        }
    }

    /// <summary> 
    /// レア度確率合計 
    /// ■バフ：センサー/PlayerItem/環境/購入アイテム
    /// </summary> 
    protected int TotalRareRatio()
    {
        //その前にバフを追加設定
        return ratioList.Sum();
    }

    /// <summary> 掘り出すレア度取得 </summary>
    protected int GetRearityNo()
    {
        var totalRatio = TotalRareRatio();
        var createNum = Random.Range(1, totalRatio + 1);

        var total = 0;
        for (int i = 0; i < ratioList.Count; i++)
        {
            total += ratioList[i];
            if (createNum <= total)
            {
                return i;
            }
        }

        return MISSING_NO;
    }

    /// <summary> アイテム取得 </summary>
    protected ItemData SelectItem(List<ItemData> items = null)
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
}


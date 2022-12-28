using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 所持アイテムクラス
/// </summary>
public partial class Player : MonoBehaviour
{
    //アイテムと所持数
    private static Dictionary<string,int> playerHoldItems = new Dictionary<string,int>();
    //取得
    public Dictionary<string, int> PlayerHoldItems => playerHoldItems;
    public int HaveItemsCount => playerHoldItems.Count();

    /// <summary>
    /// ダミーデータ登録
    /// </summary>
    void AwakeHoldItemes()
    {
        //playerHoldItems.Add("鉄くず", 2);
        //playerHoldItems.Add("銅鉱石", 10);
        //playerHoldItems.Add("銀鉱石", 999);
        //playerHoldItems.Add("金鉱石", 8000);
        //playerHoldItems.Add("隕石", 10);
    }
    /// <summary>
    /// アイテムの追加所持
    /// 所持数超えたら売却
    /// </summary>
    /// <param name="data"></param>
    /// <param name="index"></param>
    public void SetHoldPlayerItem(ItemData data, int index)
    {
        var name = data.ItemName;        //名前
        var maxCount = data.MaxHaveIndex;//最大所持数
        var price = data.SellPrice;      //売値

        //キーが存在している   
        if (playerHoldItems.ContainsKey(name))
        {
            int value = 0;
            playerHoldItems.TryGetValue(name, out value);

            var total = value + index;
            var diff = total - maxCount;
            if(diff > 0)
            {
                total = maxCount;
                price = (price * diff);
                SellOverItem(price);
            }
            else if(diff == 0)
            {
                total = maxCount;
            }

            playerHoldItems[name] = total;
            return;
        }

        playerHoldItems.Add(name, index);
        //ここでセーブしたい
    }

    /// <summary>
    /// アイテム売却
    /// </summary>
    /// <param name="sellPrice"></param>
    private void SellOverItem(int sellPrice) { }
}

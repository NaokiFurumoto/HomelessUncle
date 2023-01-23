using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static GlobalValue;

/// <summary>
/// 掘るアクション：基本共通クラスが欲しい
/// </summary>
public class DigAction : GameAction
{
    protected override void Initialize()
    {
        DataList = ItemManager.Instance.DigItemDataLists;
    }

    /// <summary>
    /// 掘り出しアイテムを表示
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public void DigItemGenerate(ItemData data)
    {
        GameObject itemObj = (GameObject)Resources.Load("Prefabs/DigItem");
        var parent = PlayerEffectRoot.Instance.GetEffectRoot(PlayerEffectRoot.ROOT_TYPE.RIGHT);
        GameObject instace = Instantiate(itemObj, Vector2.zero, Quaternion.identity, parent);

        var status = instace?.GetComponent<DigItem>();
        status?.SetImage(data);
    }
}

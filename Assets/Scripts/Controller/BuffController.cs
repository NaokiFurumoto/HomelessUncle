using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// プレイヤーや環境に関するバフ処理
/// バフの実行：使い切りと時間経過バフがある
/// </summary>
public partial class BuffController : SingletonMonoBehaviour<BuffController>
{
    //全バフの登録
    private readonly Dictionary<BUFF_TYPE, Delegate> buffActionDic 
               = new Dictionary<BUFF_TYPE, Delegate>();

    //現在実行中のバフ：時間経過のやつ
    private Dictionary<BUFF_TYPE, Delegate> currentTimeBufDic
               = new Dictionary<BUFF_TYPE, Delegate>();

    private Player player;
    private DigAction digAction;
    private FishAction fishAction;

    public Dictionary<BUFF_TYPE, Delegate> BuffActionDic => buffActionDic;
    //DigAction
    //FishAction
    //Player

    //呼び出し
    //((Action<int>) BuffActionDic[BUFF_TYPE.DIG_RAREUP_AMOUNT]).Invoke(1);

    
    IEnumerator Start() 
    {
        AddBuffDelegate();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        while (!player.IsInitialized) yield return null;
    }

    /// <summary>
    /// バフメソッドの追加
    /// </summary>
    private void AddBuffDelegate()
    {
        buffActionDic.Add(BUFF_TYPE.DIG_RAREUP_AMOUNT, new Action<int>(DigRareRandomUp));
        buffActionDic.Add(BUFF_TYPE.DIG_RAREUP_RARE_AMOUNT, new Action<ItemData.ITEM_RARITY,int>(DigRarePointUp));
    }

    /// <summary>
    /// 時間経過中ののバフを格納
    /// </summary>
    /// <param name="type"></param>
    public void AddCurrentTimeBuffDic(BUFF_TYPE type)
    {
        if (buffActionDic == null) return;
        var buff = buffActionDic.FirstOrDefault(bu => bu.Key.Equals(type));
        if (!buff.Equals(default(KeyValuePair<BUFF_TYPE, Delegate>)))
        {
            currentTimeBufDic[buff.Key] = buff.Value;
        }
    }
}

/// <summary>
/// バフの呼び出しキータイプ
/// </summary>
public enum BUFF_TYPE
{
    DIG_RAREUP_AMOUNT,//ランダム掘る率アップ
    DIG_RAREUP_RARE_AMOUNT,//特定の掘るレア率アップ
   
}

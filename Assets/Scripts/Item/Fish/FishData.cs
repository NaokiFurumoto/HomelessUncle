using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
[CreateAssetMenu(fileName = "FishData", menuName = "Create_FishData")]
public class FishData : ItemData
{
    //魚データ
    //生息域
    //釣れるエサ
    public enum FISH_GETPOINT
    {
        A,B,C,D,E,F
    }

    /// <summary> アイテムの種類 </summary>
    [SerializeField]
    [Header("釣れる場所")]
    private FISH_GETPOINT[] fishGetPointList;

    public FISH_GETPOINT[] FishGetPointList => fishGetPointList;
}

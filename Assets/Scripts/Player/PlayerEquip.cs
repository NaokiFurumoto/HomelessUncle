using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//プレイヤーの装備に関するクラス
public class PlayerEquip : MonoBehaviour
{
    //竿
    [SerializeField] private SpriteRenderer sao;

    private const string FRONT = "EquipFront";
    private const string BACK = "EquipBack";

    // Start is called before the first frame update
    void Start()
    {
        sao ??= transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// インゲーム表示順の変更
    /// </summary>
    /// <param name="index"></param>
    public void ChangeSortingLayer(int index)
    {
        sao.sortingLayerName = index == 1 ? FRONT : BACK;
    }

    ///装備する
    //釣り竿データを受け取って装備する
}

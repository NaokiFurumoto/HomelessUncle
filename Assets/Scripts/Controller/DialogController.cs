using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

/// <summary>
/// Viewの上に表示されるものはダイアログとして扱う
/// リソースの読み込み表示
/// Layerをつくってそこの子として表示
/// </summary>

///ダイアログタイプ
public enum DIALOGTYPE
{
    ITEMDETAIL,//アイテム詳細
}

//レイヤータイプ
public enum LayerType
{
    LAYER01 = 0,
    LAYER02 = 1,
    LAYER03 = 2,
}
public class DialogController : SingletonMonoBehaviour<DialogController>
{
    //[SerializeField]
    //private List<DialogBase> dialogList = new List<DialogBase>();

    //表示中のダイアログリスト
    //private List<DialogBase> currentDialogList;

    //レイヤーリスト
    [SerializeField]
    private List<Transform> layerList = new List<Transform>(); 

    void Start()
    {
        if (!(layerList?.Count > 0))
        {
            layerList = gameObject.transform.OfType<Transform>().ToList();
        }
    }



    //表示中のダイアログを削除
    //指定したレイヤー以下に表示されてるダイアログを削除
    //最上面のダイアログの削除

    //指定したダイアログを読み込んで空いてるレイヤーの子として配置

}

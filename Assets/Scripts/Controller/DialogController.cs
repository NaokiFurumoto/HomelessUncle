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
    ITEMDETAIL,//アイテム売却詳細
    ITEMSELL//アイテム売却
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
    private const string ItemDetailPath = "Prefabs/Dialog/ItemDetailSellDialog";
    private const string ItemSellPath = "Prefabs/Dialog/ItemSellDialog";

    //表示中のダイアログリスト
    private List<GameObject> currentDialogList = new List<GameObject>();

    //レイヤーリスト
    [SerializeField]
    private List<Transform> layerList = new List<Transform>();

    void Start()
    {
        if (!(layerList?.Count > 0))
        {
            layerList = gameObject.transform.OfType<Transform>().ToList();
        }

        currentDialogList.Clear();
    }

    /// <summary>
    /// ダイアログを取得
    /// </summary>
    /// <param name="dialog">ダイアログタイプ</param>
    /// <param name="layer">親レイヤー</param>
    /// <param name="avtive">アクティブ状態</param>
    /// <returns></returns>
    public GameObject ShowDialog(DIALOGTYPE type)
    {
        try
        {
            GameObject dialog = null;
            switch (type)
            {
                case DIALOGTYPE.ITEMDETAIL:
                    dialog = Resources.Load(ItemDetailPath) as GameObject;
                    break;

                case DIALOGTYPE.ITEMSELL:
                    dialog = Resources.Load(ItemSellPath) as GameObject;
                    break;
            }

            if (dialog == null) throw new System.Exception("Loadできませんでした");

            var obj = Instantiate(dialog, GetDialogParent());
            currentDialogList.Add(obj);
            return obj;
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            return null;
        }
    }

    /// <summary>
    /// 表示中のダイアログを削除
    /// </summary>
    public void DeleteAllDialog()
    {
        if (currentDialogList == null) return;
        //条件を満たす要素を除外：nullでないオブジェクトを除外
        currentDialogList = currentDialogList.Where(dialog => dialog != null).ToList();
        currentDialogList.ForEach(dialog => Destroy(dialog.gameObject));
        currentDialogList.Clear();
    }


    /// <summary>
    /// 指定したレイヤーに表示されてるダイアログを削除
    /// </summary>
    /// <param name="layer"></param>
    public void DeleteIndexDialog(LayerType layer)
    {
        //指定レイヤーのenum番号の取得
        //レイヤーのtransformの取得
        //childCountが1の場合にその子供のオブジェクトを取得してDestroy
        //currentDialogListからおbj削除

        int index = (int)layer;
        Transform layerTrfm = layerList[index];
        if (layerTrfm.childCount == 1)
        {
            GameObject dialog = layerTrfm.GetChild(0).gameObject;
            Destroy(dialog);
            currentDialogList = currentDialogList.Where(dialog => dialog.gameObject != dialog).ToList();
            if (currentDialogList == null) currentDialogList.Clear();
        }
    }

    /// <summary>
    /// 最上面のダイアログの削除
    /// </summary>
    public void DeleteFrontDialog()
    {
        Transform lastLayer = layerList.FindLast(layer => layer.childCount == 1);
        GameObject dialog = lastLayer.GetChild(0).gameObject;

        Destroy(dialog);
        currentDialogList = currentDialogList.Where(dialog => dialog.gameObject != dialog).ToList();
        if (currentDialogList == null) currentDialogList.Clear();
    }

    /// <summary>
    /// 空いてるレイヤーを取得
    /// </summary>
    /// <returns></returns>
    private Transform GetDialogParent()
    {
        return layerList.Where(list => list.childCount == 0).FirstOrDefault();
    }
}

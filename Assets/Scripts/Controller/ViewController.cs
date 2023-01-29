using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Viewに関する操作
/// </summary>
//随時追加
public enum VIEWTYPE
{
    ITEMVIEW,
    SHOPVIEW,
    MONEYCHANGEVIEW,
    USEITEMTEXT,
}

public class ViewController : SingletonMonoBehaviour<ViewController>
{
    [SerializeField]
    private List<ViewBase> viewList = new List<ViewBase>();
    
    //選択中の画面
    private ViewBase currentView;

    public List<ViewBase> ViewList => viewList;

    // Start is called before the first frame update
    private void Start() 
    {
        if(!(viewList?.Count > 0))
        {
            viewList = FindObjectsOfType<ViewBase>().ToList();
        }
    }

    /// <summary>
    /// 指定された画面の取得
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public ViewBase GetView(VIEWTYPE type)
    {
        return viewList.FirstOrDefault(view => view.ViewType == type) as ViewBase;
    }

    /// <summary>
    /// 選択されたViewの開閉
    /// </summary>
    /// <param name="isOpen"></param>
    public void ActiveView(VIEWTYPE type, bool isOpen)
    {
        currentView = viewList.FirstOrDefault(view => view.ViewType == type) as ViewBase;
        if (currentView == null) return;

        currentView.gameObject.SetActive(isOpen);
    }


    /// <summary>
    /// 現在開いてる画面を全て閉じる
    /// </summary>
    public void CloseView()
    {


    }

}

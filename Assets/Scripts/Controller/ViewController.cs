using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// View�Ɋւ��鑀��
/// </summary>

//�����ǉ�
public enum VIEWTYPE
{
    ITEMVIEW,
    SHOPVIEW,
}
public class ViewController : SingletonMonoBehaviour<ViewController>
{
    [SerializeField]
    private List<ViewBase> viewList = new List<ViewBase>();
    
    //�I�𒆂̉��
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
    /// �w�肳�ꂽ��ʂ̎擾
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public ViewBase GetView(VIEWTYPE type)
    {
        return viewList.Select(view => view.ViewType == type) as ViewBase;
    }

    /// <summary>
    /// �I�����ꂽView�̊J��
    /// </summary>
    /// <param name="isOpen"></param>
    public void ActiveView(VIEWTYPE type, bool isOpen)
    {
        currentView = viewList.FirstOrDefault(view => view.ViewType == type) as ViewBase;
        if (currentView == null) return;

        currentView.gameObject.SetActive(isOpen);
    }

}

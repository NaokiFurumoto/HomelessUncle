using Carbon;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public　abstract class ViewBase : RectTransformBehaviour
{
    [SerializeField]
    private  VIEWTYPE type;

    //アニメーターによる開閉
    protected Animator animator;

    public VIEWTYPE ViewType => type;

    //1回
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Start() { }

    //複数回
    protected virtual void OnEnable()
    {
        //animator ??= GetComponent<Animator>();
        //animator.SetTrigger("open");
    }

    protected virtual void OnDisable()
    {
        //animator.SetTrigger("close");
    }

}

/// <summary>
/// リストを更新する機能
/// </summary>
interface IUpdateList
{
    void UpdateList();
}

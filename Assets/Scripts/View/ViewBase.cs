﻿using Carbon;
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
    protected VIEWTYPE type;

    [SerializeField]
    protected Button btn_Close;

    //アニメーターによる開閉
    protected Animator animator;

    public VIEWTYPE ViewType => type;

    //1回
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        btn_Close.onClick.AddListener(() => animator?.SetTrigger("close"));
    }

    //複数回
    protected virtual void OnEnable() { }
   
    protected virtual void OnDisable() { }

}

/// <summary>
/// リストを更新する機能
/// </summary>
interface IUpdateList
{
    void UpdateList();
}

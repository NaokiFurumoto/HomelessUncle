using Carbon;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ダイアログ基底クラス
/// </summary>
public abstract class DialogBase : RectTransformBehaviour
{
    [SerializeField]
    protected DIALOGTYPE type;

    [SerializeField]
    private Button btn_Close;

    //アニメーターによる開閉
    protected Animator animator;

    public DIALOGTYPE DialogType => type;

    //1回
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        btn_Close.onClick.AddListener(() => animator?.SetTrigger("close"));
    }

    /// <summary>
    /// ダイアログを開く
    /// </summary>
    protected virtual void OpenDialog()
    {
        animator.SetTrigger("open");
    }

    //複数回
    protected virtual void OnEnable() { }

    protected virtual void OnDisable() { }

}
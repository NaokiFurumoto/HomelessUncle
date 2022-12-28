using Carbon;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public�@abstract class ViewBase : RectTransformBehaviour
{
    [SerializeField]
    private  VIEWTYPE type;

    //�A�j���[�^�[�ɂ��J��
    protected Animator animator;

    public VIEWTYPE ViewType => type;

    //1��
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Start() { }

    //������
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
/// ���X�g���X�V����@�\
/// </summary>
interface IUpdateList
{
    void UpdateList();
}

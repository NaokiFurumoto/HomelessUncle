using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonController : UIParts
{
    [SerializeField]
    private GameObject disableImage;

    private Button button;

    private void Awake()
    {
        button = this.gameObject.GetComponent<Button>();
    }
    //private void Start()
    //{
    //    button = this.gameObject.GetComponent<Button>();
    //}

    /// <summary>
    /// イベントの登録
    /// </summary>
    /// <param name="action"></param>
    public void SetBtnEvent(UnityAction action)
    {
        button.onClick.AddListener(action);
    }

    /// <summary>
    /// ボタンを押せない対応
    /// </summary>
    /// <param name="judge"></param>
    public void SetDisable (bool judge)
    {
        if (disableImage == null) return;
        disableImage.SetActive(judge);
    }
}



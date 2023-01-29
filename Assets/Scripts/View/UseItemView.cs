using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections;

/// <summary>
/// アイテム使用画面
/// </summary>
public class UseItemView : ViewBase
{
    [SerializeField] private TextMeshProUGUI txt_info;
    private string text;
    private Player player;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        base.Start();
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        while (!player.IsInitialized) yield return null;
    }

    protected override void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (btn_Close == null)
        {
            btn_Close = transform.GetChild(0).gameObject.GetComponent<Button>();
        }
        btn_Close.interactable = false;
        txt_info.text = "";
    }

    protected override void OnDisable()
    {
        StopCoroutine(DisplayText());
        player.ChangeState(player.Idle);
       //状態変更Idleに戻す
       // ボタンの有効とか？
    }

    /// <summary>
    /// テキストの設定
    /// </summary>
    /// <param name="txt"></param>
    public void SetText(string txt)
    {
        text = txt;
        StartCoroutine(DisplayText());
    }

    /// <summary>
    /// 一文字づつ追加
    /// </summary>
    /// <returns></returns>
    private IEnumerator DisplayText()
    {
        if (text == "") yield break;
        foreach (char x in text.ToCharArray()) //～.ToCharArray()はテキスト1文字ずつの配列
        {
            txt_info.text += x; //1文字追加
            //効果音再生
            yield return new WaitForSeconds(0.1f); //0.1秒間隔
        }
        btn_Close.interactable = true;
    }

}

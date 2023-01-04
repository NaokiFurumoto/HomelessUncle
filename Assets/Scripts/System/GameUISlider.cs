﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUISlider : MonoBehaviour
{
    // === 外部パラメータ（インスペクタ表示） =====================
    public GameObject scriptObject;

    public GameObject slideObject;
    public GameObject anchorStart;
    public GameObject anchorEnd;

    public bool scorllMode = false;
    public bool slideMoveX = true;
    public bool slideMoveY = false;

    public float SlideMoveAcceleX = 1.0f;
    public float SlideMoveAcceleY = 1.0f;
    public float SlideBreakeX = 0.9f;
    public float SlideBreakeY = 0.9f;

    public Button MaxButton;
    public Button MinButton;

    // === 内部パラメータ ======================================
    Vector3 movSt;
    Vector3 movNow;
    Vector2 slideSize;
    [SerializeField] Vector2 curosorPosition = Vector2.zero;

    // === 外部パラメータ ======================================

    public bool Initialized { get; set; } = false;
    public Vector2 CurosorPosition
    {
        get { return curosorPosition; }
        set { curosorPosition = value; }
    }
    

    // === コード =============================================


    public void SetValue()
    {
        slideObject.transform.position = new Vector3(anchorStart.transform.position.x,
                                                 anchorStart.transform.position.y,
                                                 slideObject.transform.position.z);
        slideSize.x = anchorEnd.transform.position.x - anchorStart.transform.position.x;
        slideSize.y = anchorEnd.transform.position.y - anchorStart.transform.position.y;
        if (scorllMode)
        {
            anchorStart.transform.position -= new Vector3(slideSize.x, slideSize.y, 0.0f);
            anchorEnd.transform.position -= new Vector3(slideSize.x, slideSize.y, 0.0f);
        }

        ///ロード後の初期化を行う
        Init();
        Initialized = true;
    }

    void Update()
    {
        //初期化完了後に実行
        if (!Initialized)
            return;

        // モードチェック
        if (scorllMode)
        {
            // --- スクロール	 ------------------------------
            // タッチチェック
            if (Input.touchCount > 0)
            {
                if (Physics2D.OverlapPoint(GetScreenPosition(Input.GetTouch(0).position)) != null)
                {
                    switch (Input.GetTouch(0).phase)
                    {
                        case TouchPhase.Began:
                            movSt = GetScreenPosition(Input.GetTouch(0).position);
                            break;
                        case TouchPhase.Moved:
                            MoveSlide(GetScreenPosition(Input.GetTouch(0).position) - movSt);
                            movSt = GetScreenPosition(Input.GetTouch(0).position);
                            break;
                        case TouchPhase.Ended:
                            break;
                    }
                }
            }
            else
            // マウスチェック
            if (Input.GetMouseButton(0))
            {
                if (Physics2D.OverlapPoint(GetScreenPosition(Input.mousePosition)) != null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        movSt = GetScreenPosition(Input.mousePosition);
                    }
                    if (Input.GetMouseButton(0))
                    {
                        MoveSlide(GetScreenPosition(Input.mousePosition) - movSt);
                        movSt = GetScreenPosition(Input.mousePosition);
                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                    }
                }
            }
            else
            {
                MoveSlide(new Vector2(movNow.x * SlideBreakeX, movNow.y * SlideBreakeY));
            }
        }
        else
        {
            // --- スライダー -------------------------------
            // タッチチェック
            if (Input.touchCount > 0)
            {
                switch (Input.GetTouch(0).phase)
                {
                    case TouchPhase.Began:
                    case TouchPhase.Moved:
                        SetSlide(GetScreenPosition(Input.GetTouch(0).position));
                        break;
                }
            }
            else
            // マウスチェック
            if (Input.GetMouseButton(0))
            {
                SetSlide(GetScreenPosition(Input.mousePosition));
            }
        }
        CheckSlide();
    }

    Vector3 GetScreenPosition(Vector3 touchPos)
    {
        touchPos.z = transform.position.z - Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(touchPos);
    }

    void MoveSlide(Vector2 mov)
    {
        movNow = mov;
        mov.x *= slideMoveX ? SlideMoveAcceleX : 0.0f;
        mov.y *= slideMoveY ? SlideMoveAcceleY : 0.0f;
        slideObject.transform.position += (Vector3)mov;
        if (scriptObject != null)
        {
            scriptObject.SendMessage("SlidebarDrag", this);
        }
    }

    void SetSlide(Vector2 pos)
    {
        Collider2D col2d = Physics2D.OverlapPoint(pos);
        if (col2d != null)
        {
            if (col2d.transform.parent == transform)
            {
                float x = 0.0f;
                float y = 0.0f;
                if (slideSize.x != 0.0f)
                {
                    x = (pos.x - anchorStart.transform.position.x) / slideSize.x;
                }
                if (slideSize.y != 0.0f)
                {
                    y = (pos.y - anchorStart.transform.position.y) / slideSize.y;
                }
                SetPosition(new Vector2(x, y));
            }
        }
        if (scriptObject != null)
        {
            scriptObject.SendMessage("SlidebarDrag", this);
        }
    }

    void CheckSlide()
    {
        // 移動範囲チェック
        if (slideObject.transform.position.x < anchorStart.transform.position.x)
        {
            slideObject.transform.position = new Vector3(anchorStart.transform.position.x, slideObject.transform.position.y, slideObject.transform.position.z);
        }
        if (slideObject.transform.position.x > anchorEnd.transform.position.x)
        {
            slideObject.transform.position = new Vector3(anchorEnd.transform.position.x, slideObject.transform.position.y, slideObject.transform.position.z);
        }
        if (slideObject.transform.position.y > anchorStart.transform.position.y)
        {
            slideObject.transform.position = new Vector3(slideObject.transform.position.x, anchorStart.transform.position.y, slideObject.transform.position.z);
        }
        if (slideObject.transform.position.y < anchorEnd.transform.position.y)
        {
            slideObject.transform.position = new Vector3(slideObject.transform.position.x, anchorEnd.transform.position.y, slideObject.transform.position.z);
        }
        // 現在位置を0.0f～1.0fに変換する
        Vector3 ofsPos = slideObject.transform.position - anchorStart.transform.position;
        curosorPosition = Vector2.zero;
        if (slideSize.x != 0.0f)
        {
            curosorPosition.x = ofsPos.x / slideSize.x;
        }
        if (slideSize.y != 0.0f)
        {
            curosorPosition.y = ofsPos.y / slideSize.y;
        }
        if (scorllMode)
        {
            curosorPosition = Vector2.one - curosorPosition;
        }
        curosorPosition.x = Mathf.Clamp01(curosorPosition.x);
        curosorPosition.y = Mathf.Clamp01(curosorPosition.y);
    }

    public void Init()
    {
        //ボタンイベントの設定
        MaxButton.onClick.AddListener(() => { //AppSound.Instance.SE_MENU_OK.Play();
                                              SetPosition(new Vector2(1.0f, 0.0f)); });
        MinButton.onClick.AddListener(() => { //AppSound.Instance.SE_MENU_OK.Play();
                                              SetPosition(new Vector2(0.0f, 0.0f)); });
    }

    public void SetPosition(Vector2 pos)
    {
        curosorPosition = pos;
        float x = anchorStart.transform.position.x + slideSize.x * curosorPosition.x;
        float y = anchorStart.transform.position.y + slideSize.y * curosorPosition.y;
        slideObject.transform.position = new Vector3(x, y, 0.0f);
        CheckSlide();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// タッチ入力管理
/// </summary>
public class InputManager : MonoBehaviour
{
    /// <summary> インスタンス <summary>
    public static InputManager Instance;

    /// <summary> メインカメラ <summary>
    private Camera mainCamera;

    /// <summary> 画面タッチ可能ならtrue <summary>
    private bool touchFlag;

    /// <summary> タッチ位置 <summary>
    private Vector2 touchBeginPos, touchingPos, touchLastPos;

    /// <summary> タッチ状態 <summary>
    private TouchPhase touchPhase;

    //ゲーム管理クラス
    private Player player;


    /// <summary> ポイントデータ </summary>
    private PointerEventData pointerEventData;

    #region プロパティ
    public bool TouchFlag => touchFlag;
    public Vector2 TouchBeginPos => touchBeginPos;
    public Vector2 TouchingPos => touchingPos;
    public Vector2 TouchingLastPos => touchLastPos;
    public TouchPhase TouchPhase
    {
        get { return touchPhase; }
        set { touchPhase = value; }
    }
    #endregion

    /// <summary> 初期化 <summary>
    private void Awake()
    {
        Instance ??= this;
        mainCamera = Camera.main;
        touchFlag = true;
        touchBeginPos = touchingPos
                      = touchLastPos
                      = Vector2.zero;
        touchPhase = TouchPhase.Canceled;
        //touchPhase = TouchPhase.Began;

        pointerEventData = new PointerEventData(EventSystem.current);

        player = GameObject.FindGameObjectWithTag("Player")
                                   .GetComponent<Player>();
    }

    /// <summary> 入力更新 </summary>
    private void Update()
    {
        //Editor
        //if (Application.isEditor)
        //{ }
        //else//端末
        //{
        //    //TODO:追加で必要そう
        //    if (Input.touchCount > 0)
        //    {
        //        Touch touch = Input.GetTouch(0);
        //        touchingPos = touch.position;
        //        touchPhase = touch.phase;
        //        touchFlag = true;
        //    }
        //}



        //押した瞬間のタッチは受け付ける
        if (Input.GetMouseButtonDown(0))
        {
            var hitobjects = GetObjectAll();
            if (hitobjects?.Count > 0)
            {
                foreach (RaycastResult obj in hitobjects)
                {
                    if (obj.gameObject.layer == LayerMask.NameToLayer("UI"))
                    {
                        //タッチさせない
                        touchFlag = false;
                        return;
                    }
                }

            }

            touchFlag = true;
            touchPhase = TouchPhase.Began;
            touchBeginPos = touchingPos =
            mainCamera.ScreenToWorldPoint(Input.mousePosition);
            //player.IsMove = true;
            //player.ChangeState(player.Walking);
            //Debug.Log("Begin");
        }

        if (touchFlag)
        {
            //離した瞬間
            if (Input.GetMouseButtonUp(0))
            {
                touchFlag = false;
                touchPhase = TouchPhase.Ended;
                touchLastPos = touchingPos;
                touchBeginPos = touchingPos
                                = Vector2.zero;
                //player.ChangeState(player.Idle);
                //Debug.Log("End");
            }

            //押しっぱなし
            if (Input.GetMouseButton(0))
            {
                var distance = Vector2.Distance(touchBeginPos, touchingPos);
                if (distance >= 0.5f)
                {
                    touchPhase = TouchPhase.Moved;

                }
                touchingPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            }
        }


    }

    /// <summary> ヒットしたオブジェクトを全て取得 <summary>
    private List<RaycastResult> GetObjectAll()
    {
        if (EventSystem.current != null)
        {
            List<RaycastResult> result = new List<RaycastResult>();
            pointerEventData.position = Input.mousePosition;
            EventSystem.current.RaycastAll(pointerEventData, result);
            return result;
        }

        return default;
    }
}

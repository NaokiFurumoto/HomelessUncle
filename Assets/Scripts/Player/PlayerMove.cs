using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playerの移動に関するクラス
/// </summary>
public partial class Player
{
    /// <summary> アニメーション </summary>
    [SerializeField]
    private Animator playerAnim;

    //キャッシュ用
    private InputManager inputManager;

    /// <summary> 移動スピード </summary>
    [SerializeField]
    private float moveSpeed;

    /// <summary> タップした位置 </summary>
    [SerializeField]
    private Vector2 tapPos;

    /// <summary> センサー位置 </summary>
    [SerializeField]
    private Transform sensorTrfm;

    /// <summary> 向くべき方向 </summary>
    private Vector2 direction;

    /// <summary> プレーヤーの向きの一時的な退避 </summary>
    private Vector3 tempScale;

    /// <summary> プレイヤーが移動可能かどうかの判定 </summary>
    private bool isMove;

    /// <summary> Rigidbody2D </summary>
    private Rigidbody2D rigid2D;
    #region プロパティ
    public Vector2 Direction => direction;
    public Animator PlayerAnim => playerAnim;
    public bool IsMove { get { return isMove; } set { isMove = value; } }
    #endregion

    private void MoveAwake()
    {
        direction = -Vector2.up;
        //isMove = true;//とりあえず動ける
    }

    /// <summary> 初期化：外部クラス </summary>
    private void MoveStart()
    {
        inputManager = InputManager.Instance;
        playerAnim ??= GameObject.FindGameObjectWithTag("PlayerAnimator")
                                 .GetComponent<Animator>();
        rigid2D = this.gameObject.GetComponent<Rigidbody2D>();
        sensorTrfm ??= GameObject.FindGameObjectWithTag("PlayerSensor")
                                 .GetComponent<Transform>();
    }

    /// <summary>
    /// 移動更新
    /// 待機中か歩いてるときに出来る？
    /// </summary>
    private void MoveUpdate()
    {
        if (!isMove)
        {
            inputManager.TouchPhase = TouchPhase.Canceled;
            return;
        }

        //タッチ状態で移動を制御
        switch (inputManager.TouchPhase)
        {
            case TouchPhase.Began://画面に指が触れた
                CancelInvoke("SetStateIdle");
                ChangeState(stateWalking);
                tapPos = inputManager.TouchBeginPos;
                PlayerChangeAnim();
                CharacterMovement();
                break;

            case TouchPhase.Moved://画面上で指が動いてるとき
                PlayerChangeAnim();
                CharacterMovement();
                break;

            case TouchPhase.Ended://指が離れる
                rigid2D.velocity = Vector2.zero;
                inputManager.TouchPhase = TouchPhase.Canceled;
                Invoke("SetStateIdle", 3.0f);
                break;
        }
    }

    /// <summary>
    /// アニメーションの変更
    /// </summary>
    private void PlayerChangeAnim()
    {
        tapPos = inputManager.TouchingPos;
        direction = new Vector2(tapPos.x - transform.position.x,
                                  tapPos.y - transform.position.y).normalized;

        PlayerAnimation(direction.x, direction.y);
    }

    /// <summary>
    /// 向きに合わせたアニメーションの切り替え
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void PlayerAnimation(float x, float y)
    {
        //0.5の数を偶数に合わせる
        x = Mathf.RoundToInt(x);
        y = Mathf.RoundToInt(y);

        //向きの変更
        ChangeDirection(x);
        
        //アニメーションのために初期化させる
        x = Mathf.Abs(x);
        playerAnim.SetFloat("FaceX", x);
        playerAnim.SetFloat("FaceY", y);
    }

    /// <summary>
    /// プレイヤーの向きの変更
    /// </summary>
    private void ChangeDirection(float x)
    {
        AnimatorClipInfo[] currentClipInfo = playerAnim.GetCurrentAnimatorClipInfo(0);
        string currentAnimName = currentClipInfo[0].clip.name;

        int changex;
        //横移動はxの値を変更
        if (currentAnimName == "PlayerSide")
        {
            tempScale = transform.localScale;
            tempScale.x = x > 0 ? Mathf.Abs(tempScale.x) : -Mathf.Abs(tempScale.x);
            transform.localScale = tempScale;
            changex = (int)tempScale.x;
            SetSensorPos(1.5f, 0.5f,true);
        }
        else if (currentAnimName == "PlayerBack")
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 0.0f);
            changex = -1;
            SetSensorPos(0f, 2.5f,true);
        }
        else//正面
        {
            transform.localScale = Vector3.one;
            changex = 1;
            SetSensorPos(0.0f,0.5f,false);
        }
        equip.ChangeSortingLayer(changex);
    }

    /// <summary>
    /// プレイヤーの移動
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void CharacterMovement()
    {
        if (Vector2.Distance(transform.position, inputManager.TouchingPos) < 0.1f)
        {
            // 目標位置に到達したら移動を停止する
            rigid2D.velocity = Vector2.zero;
        }
        else
        {
            Vector2 direction = inputManager.TouchingPos - (Vector2)transform.position;
            rigid2D.velocity = direction.normalized * moveSpeed;
        }
    }

    /// <summary>
    /// センサーの位置設定
    /// </summary>
    /// <param name="_x"></param>
    /// <param name="_y"></param>
    private void SetSensorPos(float _x, float _y, bool idActive)
    {
        //if(sensorTrfm.gameObject.active)
        var pos = sensorTrfm.position;
        pos.x = _x;
        pos.y = _y;
        sensorTrfm.transform.localPosition = pos;
    }

    /// <summary>
    /// 待機アニメーションにする
    /// </summary>
    private void SetStateIdle()
    {
        ChangeState(stateIdle);
    }
}

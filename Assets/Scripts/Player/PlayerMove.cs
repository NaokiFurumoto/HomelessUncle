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

    /// <summary> 向くべき方向 </summary>
    private Vector2 direction;

    /// <summary> プレーヤーの向きの一時的な退避 </summary>
    private Vector3 tempScale;

    /// <summary> プレイヤーが移動可能かどうかの判定 </summary>
    private bool isMove;

    #region プロパティ
    public Vector2 Direction => direction;
    public Animator PlayerAnim => playerAnim;
   public bool IsMove { get { return isMove; } set { isMove = value; } }
    #endregion

    private void MoveAwake()
    {
        direction = -Vector2.up;
        moveSpeed = 4.0f;
        //isMove = true;//とりあえず動ける
    }

    /// <summary> 初期化：外部クラス </summary>
    private void MoveStart()
    {
        inputManager = InputManager.Instance;
        playerAnim ??= GameObject.FindGameObjectWithTag("PlayerAnimator")
                                  .GetComponent<Animator>();

        //gameController ??= GameObject.FindGameObjectWithTag("GameController")
        //                               .GetComponent<GameController>();
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
                ChangeState(stateWalking);
                tapPos = inputManager.TouchBeginPos;
                PlayerTurning();
                CharacterMovement();
                break;

            case TouchPhase.Moved://画面上で指が動いてるとき
                PlayerTurning();
                CharacterMovement();
                break;

            case TouchPhase.Ended://指が離れる
                tapPos = inputManager.TouchingLastPos;
                var x = Mathf.Clamp(tapPos.x, -4.5f, 4.5f);
                var y = Mathf.Clamp(tapPos.y, -3.0f, 4.0f);
                transform.position = new Vector2(x, y);
                ChangeState(stateIdle);
                inputManager.TouchPhase = TouchPhase.Canceled;
                break;
        }
    }

    /// <summary>
    /// タップした場所に方向転換
    /// </summary>
    private void PlayerTurning()
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

        tempScale = transform.localScale;
        tempScale.x = x > 0 ? Mathf.Abs(tempScale.x) : -Mathf.Abs(tempScale.x);
        transform.localScale = tempScale;

        //アニメーションのために初期化させる
        x = Mathf.Abs(x);
        playerAnim.SetFloat("FaceX", x);
        playerAnim.SetFloat("FaceY", y);
    }

    /// <summary>
    /// プレイヤーの移動
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void CharacterMovement()
    {
        var pos = Vector2.Lerp(transform.position,
                               inputManager.TouchingPos,
                               moveSpeed * Time.deltaTime);

        var x = Mathf.Clamp(pos.x, -4.5f, 4.5f);
        var y = Mathf.Clamp(pos.y, -3.0f, 4.0f);

        transform.position = new Vector2(x, y);
    }
}

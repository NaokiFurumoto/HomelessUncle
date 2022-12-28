using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤー本体
/// </summary>
public partial class Player : MonoBehaviour
{
    /// <summary> プレイヤーの初期化完了フラグ <summary>
    private bool isInitialized;

    public bool IsInitialized => isInitialized;

    void Awake()
    {
        isInitialized = false;
        StateAwake();
        MoveAwake();

        //後で削除
        AwakeHoldItemes();
    }

    /// <summary>
    /// コルーチン化する
    /// </summary>
    private IEnumerator Start()
    {
        //ゲームコントローラーがシステムロードまち
        //そのあとプレイヤー

        //各種ロード完了待ち？？

        //whileでload完了待ち？？
        //yield return null;

        //ステートに関する初期化
        StateStart();
        //移動に関する初期化
        MoveStart();
        //ステータス初期化
        playerStatus.SetInitializeStatus();
        //ステータス反映
        yield return playerStatus.SetLoadedStatus();
        isInitialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        //ステート更新
        StateUpdate();
    }

    void LateUpdate()
    {
        
        //移動更新
        MoveUpdate();
    }

    //衝突
    private void OnCollisionEnter(Collision colliion)
    {
        // ChangeState(stateIdle);
    }
}

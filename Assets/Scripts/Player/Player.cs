using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤー本体
/// </summary>
public partial class Player : MonoBehaviour
{
    

    void Awake()
    {
        StateAwake();
        MoveAwake();

        //後で削除
        AwakeHoldItemes();
    }

    void Start()
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
        //UI反映

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

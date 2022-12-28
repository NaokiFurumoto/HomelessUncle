using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///プレイヤーの状態管理するクラス
/// </summary>
public partial class Player
{
    // <summary> 状態ステート</summary>
    private static readonly StateIdle stateIdle = new StateIdle();
    private static readonly StateWalking stateWalking = new StateWalking();
    private static readonly StateEat stateEat = new StateEat();
    private static readonly StateSleep stateSleep = new StateSleep();
    private static readonly StateToilet stateToilet = new StateToilet();
    private static readonly StateDig stateDig = new StateDig();
    private static readonly StateFishing stateFishing = new StateFishing();
    private static readonly StateDead stateDead = new StateDead();
    //気絶：気絶を過ぎたら死亡

    /// <summary> 現在のステート </summary>
    private PlayerStateBase currentState;

    #region プロパティ
    public PlayerStateBase CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }

    public StateIdle Idle => stateIdle;
    public StateWalking Walking => stateWalking;
    public StateEat Eat => stateEat;
    public StateSleep Sleep => stateSleep;
    public StateToilet Toilet => stateToilet;
    public StateDig Dig => stateDig;
    public StateFishing Fishing => stateFishing;
    public StateDead Dead => stateDead;
    public bool IsDead => currentState is StateDead;
    #endregion


    private void StateAwake()
    {
        //とりあえず待機
        currentState = stateIdle;
    }


    //Start()からよばれる
    private void StateStart()
    {
        //開始時のステートは存在しないのでnull
        currentState.OnEnter(this, null);
    }

    //Update()からよばれる
    private void StateUpdate()
    {
        currentState.OnUpdate(this);
    }

    //ステートの変更:newするとメモリが確保されるのであかん
    //privateだが、子クラスからは参照わたせばアクセス可能
    public void ChangeState(PlayerStateBase nextState)
    {
        currentState.OnExit(this, nextState);
        nextState.OnEnter(this, currentState);
        currentState = nextState;
    }

    //死亡
    private void Death()
    {
        ChangeState(stateDead);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///�v���C���[�̏�ԊǗ�����N���X
/// </summary>
public partial class Player
{
    // <summary> ��ԃX�e�[�g</summary>
    private static readonly StateIdle stateIdle = new StateIdle();
    private static readonly StateWalking stateWalking = new StateWalking();
    private static readonly StateEat stateEat = new StateEat();
    private static readonly StateSleep stateSleep = new StateSleep();
    private static readonly StateToilet stateToilet = new StateToilet();
    private static readonly StateDig stateDig = new StateDig();
    private static readonly StateFishing stateFishing = new StateFishing();
    private static readonly StateDead stateDead = new StateDead();
    //�C��F�C����߂����玀�S

    /// <summary> ���݂̃X�e�[�g </summary>
    private PlayerStateBase currentState;

    #region �v���p�e�B
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
        //�Ƃ肠�����ҋ@
        currentState = stateIdle;
    }


    //Start()�����΂��
    private void StateStart()
    {
        //�J�n���̃X�e�[�g�͑��݂��Ȃ��̂�null
        currentState.OnEnter(this, null);
    }

    //Update()�����΂��
    private void StateUpdate()
    {
        currentState.OnUpdate(this);
    }

    //�X�e�[�g�̕ύX:new����ƃ��������m�ۂ����̂ł�����
    //private�����A�q�N���X����͎Q�Ƃ킽���΃A�N�Z�X�\
    public void ChangeState(PlayerStateBase nextState)
    {
        currentState.OnExit(this, nextState);
        nextState.OnEnter(this, currentState);
        currentState = nextState;
    }

    //���S
    private void Death()
    {
        ChangeState(stateDead);
    }
}

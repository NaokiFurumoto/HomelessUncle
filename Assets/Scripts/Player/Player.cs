using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�{��
/// </summary>
public partial class Player : MonoBehaviour
{
    

    void Awake()
    {
        StateAwake();
        MoveAwake();

        //��ō폜
        AwakeHoldItemes();
    }

    void Start()
    {
        //�Q�[���R���g���[���[���V�X�e�����[�h�܂�
        //���̂��ƃv���C���[

        //�e�탍�[�h�����҂��H�H

        //while��load�����҂��H�H
        //yield return null;

        //�X�e�[�g�Ɋւ��鏉����
        StateStart();
        //�ړ��Ɋւ��鏉����
        MoveStart();
        //�X�e�[�^�X������
        playerStatus.SetInitializeStatus();
        //UI���f

    }

    // Update is called once per frame
    void Update()
    {
        //�X�e�[�g�X�V
        StateUpdate();
    }

    void LateUpdate()
    {
        
        //�ړ��X�V
        MoveUpdate();
    }

    //�Փ�
    private void OnCollisionEnter(Collision colliion)
    {
        // ChangeState(stateIdle);
    }
}

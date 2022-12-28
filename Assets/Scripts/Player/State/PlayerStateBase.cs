using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�e�[�g���N���X
/// </summary>
public partial class Player 
{
    public abstract class PlayerStateBase 
    {
        /// <summary> �X�e�[�g���J�n�������ɌĂ΂�� </summary>
        public virtual void OnEnter(Player player, PlayerStateBase state) { }

        /// <summary> ���t���[���Ă΂�� </summary>
        public virtual void OnUpdate(Player player) { }

        /// <summary> �X�e�[�g���I���������ɌĂ΂�� </summary>
        public virtual void OnExit(Player player, PlayerStateBase nextState) { }

        //�A�j���[�V�����I�����̏�������`����
    }
}

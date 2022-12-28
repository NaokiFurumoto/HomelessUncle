using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �C���Q�[��UI�Ɋւ��郁�\�b�h�̓o�^
/// </summary>
public partial class UIController
{
    private ViewController viewCtrl;

    /// <summary>
    /// �@��A�N�V����
    /// </summary>
    private void DigBtnCallback()
    {
        //�v���C���[�̃X�e�[�g��Dig��ԂȂ�ΘA�łł��Ȃ�
        if (player.CurrentState == player.Idle)
        {
            player.transform.localScale = Vector3.one;
            player.ChangeState(player.Dig);
            //�v���C���[�̗̑͂����Z
        }
    }

    /// <summary>
    /// �A�C�e���{�^��
    /// </summary>
    private void ItemBtnCallback()
    {
        if (viewCtrl == null) return;
        viewCtrl.ActiveView(VIEWTYPE.ITEMVIEW,true);
    }


}

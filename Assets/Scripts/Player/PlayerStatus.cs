using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// �v���C���[�̃X�e�[�^�X�Ɋւ���N���X�@�ݒ�/�擾
/// </summary>
public partial class Player
{
    [SerializeField]
    private Status playerStatus;

    public Status PlayerStatus => playerStatus;

    [Serializable]
    public class Status
    {
        /// <summary> �̗� </summary>
        [SerializeField]
        private float hp;

        /// <summary> �ő�̗� </summary>
        [SerializeField]
        private float maxHp;

        /// <summary> �a�C���� </summary>
        [SerializeField]
        private bool isSickness;

        /// <summary> �L�� </summary>
        [SerializeField]
        private int smell;

        /// <summary> �ֈ� </summary>
        [SerializeField]
        private int bowel;

        /// <summary> �ċN�����Ƃ��̓����ֈ� </summary>
        [SerializeField]
        private int loadingBowel;

        /// <summary> ������ </summary>
        [SerializeField]
        private int haveMoney;

        /// <summary> �؋� </summary>
        [SerializeField]
        private long loan;


        #region �v���p�e�B
        public float Hp { get { return hp; } set { hp = value; } }
        public float MaxHp { get { return maxHp; } private set { maxHp = value; } }
        public bool IsSickness { get { return isSickness; } private set { isSickness = value; } }
        public int Smell { get { return smell; } private set { smell = value; } }
        public int Bowel { get { return bowel; } private set { bowel = value; } }
        public int LoadingBowel { get { return loadingBowel; } private set { loadingBowel = value; } }
        public int HaveMoney { get { return haveMoney; } private set { haveMoney = value; } }
        public long Loan { get { return loan; } private set { loan = value; } }
        #endregion

        /// <summary>
        /// �J�n���̏����X�e�[�^�X�ݒ�:�܂Ƃ߂�B
        /// ���[�h�f�[�^�ŏ�����
        /// </summary>
        public void SetInitializeStatus()
        {
            hp = 100;
            maxHp = 100;
            isSickness = false;
            smell = 0;
            bowel = 0;
            loadingBowel = 0;
            haveMoney = 0;
            loan = 50000000;
        }

        /// <summary>
        ///�@���[�h��̃X�e�[�^�X�ݒ�F�܂Ƃ߂�
        ///�@���[�h�f�[�^���󂯎��
        /// </summary>
        public void SetLoadedStatus()
        {
        }
    }
}

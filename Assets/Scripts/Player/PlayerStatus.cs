using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// プレイヤーのステータスに関するクラス　設定/取得
/// </summary>
public partial class Player
{
    [SerializeField]
    private Status playerStatus;

    public Status PlayerStatus => playerStatus;

    [Serializable]
    public class Status
    {
        /// <summary> 体力 </summary>
        [SerializeField]
        private float hp;

        /// <summary> 最大体力 </summary>
        [SerializeField]
        private float maxHp;

        /// <summary> 病気判定 </summary>
        [SerializeField]
        private bool isSickness;

        /// <summary> 臭み </summary>
        [SerializeField]
        private int smell;

        /// <summary> 便意 </summary>
        [SerializeField]
        private int bowel;

        /// <summary> 再起したときの内部便意 </summary>
        [SerializeField]
        private int loadingBowel;

        /// <summary> 所持金 </summary>
        [SerializeField]
        private int haveMoney;

        /// <summary> 借金 </summary>
        [SerializeField]
        private long loan;


        #region プロパティ
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
        /// 開始時の初期ステータス設定:まとめる。
        /// ロードデータで初期化
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
        ///　ロード後のステータス設定：まとめる
        ///　ロードデータを受け取る
        /// </summary>
        public void SetLoadedStatus()
        {
        }
    }
}

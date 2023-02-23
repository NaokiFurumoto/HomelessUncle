using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

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

        /// <summary> 所持金 </summary>
        [SerializeField]
        private long haveMoney;

        #region プロパティ
        public long HaveMoney { get { return haveMoney; } set { haveMoney = value; } }
        #endregion

        /// <summary>
        /// 開始時の初期ステータス設定:まとめる。
        /// ロードデータで初期化
        /// </summary>
        public void SetInitializeStatus()
        {
            haveMoney = 5000000;
        }

        /// <summary>
        ///　ロード後のステータス設定：まとめる
        ///　ロードデータを受け取る
        /// </summary>
        public async Task SetLoadedStatus()
        {
        }
    }
}

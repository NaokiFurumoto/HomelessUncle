using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GetMoney
{
    /// <summary>
    /// ドロップゲーム管理クラス
    /// </summary>
    public class GetMoneyGameController : MonoBehaviour
    {
        //レアレベル:Nの確率を下げる
        [SerializeField] private int rareLevel;

        //最大生成個数
        [SerializeField] private int maxIndex;

        //現在の生成個数
        [SerializeField] private int nowIndex;

        //生成器
        [SerializeField] private DropItemGenerator itemGenerator;


        //ここから生成器の制御
        //生成割合を受け取る

        // データを受け取るのでココじゃダメかも
        IEnumerator Start()
        {
            while (!DropItemManager.Instance)
            {
                yield return null;
            }

            //パラメータを受け取る
            var generateParam = new GenerateParam();

            //生成開始
            itemGenerator?.GenerateStart(generateParam);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

    //構造体：生成パラメーター
    public struct GenerateParam
    {
        public float Interval;
        public int RareLevel;
        public float MaxIndex;
    }
}

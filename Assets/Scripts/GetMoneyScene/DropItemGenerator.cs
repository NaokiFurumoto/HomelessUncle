using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static GlobalValue;
using System;
using UnityEngine.UI;

namespace GetMoney
{
    /// <summary>
    /// 生成状態
    /// 初期値を持ってるので、バフ値を受け取って計算
    /// </summary>
    public enum DropItemState
    {
        NONE,//未設定
        STOP,//停止
        START,//開始
        SUPER,//スーパー
    }

    /// <summary>
    /// ドロップアイテムを生成するクラス
    /// </summary>
    public class DropItemGenerator : MonoBehaviour
    {
        //ドロップアイテム配置親
        [SerializeField] private Transform dropRoot;

        //ドロップアイテム
        [SerializeField] private GameObject dropItemObj;

        //生成状態
        [SerializeField] private DropItemState currentState = DropItemState.STOP;

        //生成割合：レアリティによって変化する値
        [SerializeField]
        private List<int> rareRatioList = new List<int>();

        //生成間隔
        [SerializeField] private float spawnInterval;

        //生成間隔
        [SerializeField] private GeneratorBar gaugeController;

        //時間計測用
        private float timer;

        //生成親
        [SerializeField] private Transform itemsParent;

        /// <summary>  表示アイテム <summary> 
        private List<DropItem> AllDropItems = new List<DropItem>();

        /// </summary> スクリーンサイズ <summary>
        private Vector3 rightTopScreen;
        private Vector3 leftBottomScreen;

        private int rareLevel;
        private float maxIndex;

        //保存用：全ドロップアイテム:位置も保存
        //allTempDropItems[createItem.transform.position] = createItem;//設定
        private Dictionary<Vector3, GameObject> allTempDropItems = new Dictionary<Vector3, GameObject>();

        //全配置アイテム
        private List<GameObject> allDropItems = new List<GameObject>();

        //取得用配置アイテム数
        public int DropedItemCount { get { return allDropItems.Count; } }
        public DropItemState CurrentState { get { return currentState; } set { currentState = value; } }


        public void GenerateStart(GenerateParam param)
        {
            rightTopScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            leftBottomScreen = Camera.main.ScreenToWorldPoint(Vector3.zero);
            timer = 0.0f;

            SetParam(param);
            gaugeController.SetMinMaxIntValue(0, (int)this.maxIndex);
            currentState = DropItemState.START;

            //環境設定：レア度/ボーナス生成
            //インターバル計算

            //生成開始
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        private void SetParam(GenerateParam param)
        {
            //あとで変更
            spawnInterval = param.Interval = 5.0f;
            rareLevel = param.RareLevel = 1;
            maxIndex = param.MaxIndex = 20.0f;
        }

        // Update is called once per frame
        void Update()
        {
            if (currentState == DropItemState.STOP) return;
            if (IsCheckOver())//生成数最大
            {
                timer = 0.0f;
                this.gaugeController.SetValie(timer);
                return;
            }

            timer += Time.deltaTime;
            float value = Mathf.Clamp(timer, 0.0f, this.maxIndex);
            this.gaugeController.SetValie(value);

            if (timer >= spawnInterval)
            {
                this.dropItemObj = GetItem();
                this.dropItemObj.transform.SetParent(itemsParent);

                allDropItems.Add(this.dropItemObj);
                this.dropItemObj = null;
                timer = 0.0f;
            }
        }

        /// <summary> 合計排出率 </summary>
        private int TotalRatio()
        {
            return rareRatioList.Sum();
        }

        /// <summary>
        /// レア度を設定
        /// 後で修正
        /// </summary>
        private int GetRarityIndex()
        {
            var totalRatio = TotalRatio();
            var createNum = UnityEngine.Random.Range(1, totalRatio);

            var total = 0;
            //レア種類リストからどのリストをを取得するか？
            //0～4　レア率
            for (int i = 0; i < 4; i++)
            {
                total += rareRatioList[i];
                if (createNum <= total)
                {
                    //レア値を返す
                    return i;
                }
            }
            return 0;
        }


        /// <summary>
        /// アイテム取得
        /// </summary>
        /// <returns></returns>
        private GameObject GetItem()
        {
            try
            {
                var index = GetRarityIndex();
                var selectedRereList = DropItemManager.Instance.GetRareList(index);
                if (selectedRereList == null)
                {
                    throw new Exception("レアリティリストを取得できません");
                }

                //情報を取得
                DropItem dropItem = selectedRereList.OrderBy(x => UnityEngine.Random.value).First();

                var pos = this.GetDropItemPos();
                var item = Instantiate(this.dropItemObj, pos, Quaternion.identity);

                var dropItemParam = item.GetComponent<DropItem>();
                dropItemParam.ItemSprite = dropItem.ItemSprite;
                dropItemParam.SellPrice = dropItem.SellPrice;

                return item;
            }
            catch (Exception ex)
            {
                Debug.LogError($"アイテムの取得に失敗しました: {ex.Message}");
                return null;
            }
        }


        /// <summary>
        /// 生成位置の設定
        /// </summary>
        private Vector3 GetDropItemPos()
        {
            float x, y;
            bool isCheck;

            do
            {
                x = UnityEngine.Random.Range(leftBottomScreen.x + 1.0f, rightTopScreen.x - 1.0f);
                y = UnityEngine.Random.Range(leftBottomScreen.y + 1.0f, rightTopScreen.y - 2.0f);

                isCheck = (leftBottomScreen.x - 1.0f <= x)
                        && (rightTopScreen.x + 1.0f >= x)
                        && (leftBottomScreen.y - 2.0f <= y)
                        && (rightTopScreen.y + 2.0f >= y);

            } while (isCheck);

            return new Vector3(x, y, 0);
        }

        /// <summary>
        /// 画面に表示させる最大生成数チェック
        /// </summary>
        public bool IsCheckOver()
        {
            return itemsParent?.childCount >= maxIndex ? true : false;
        }

        /// <summary>
        /// 表示されてるアイテムの削除
        /// </summary>
        public void DeleteAllItems()
        {
            allDropItems.Clear();
            if (dropRoot.childCount <= 0) return;
            foreach (Transform item in dropRoot) GameObject.Destroy(item.gameObject);
        }

        //指定オブジェクトの削除

    }//end class
}


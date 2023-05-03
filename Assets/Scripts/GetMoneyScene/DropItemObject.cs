using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

namespace GetMoney
{
    /// <summary>
    /// ドロップアイテムオブジェクトクラス
    /// </summary>
    public class DropItemObject : MonoBehaviour
    {
        [SerializeField] private TextMeshPro txt_price;
        private Animator animator;
        private Animator animator_text;
        private SpriteRenderer sprite;
        private bool isInit;
        private DropItemGenerator itemGenerator;
        private GameObject thisObj;
        public int Price;

        private void Awake()
        {
            this.isInit = false;
            this.thisObj = this.gameObject;
            this.sprite = GetComponent<SpriteRenderer>();
            this.animator = GetComponent<Animator>();
        }

        public void SetData(DropItem _data,float _rotate)
        {
            if (_data == null) return;

            this.sprite.sprite = _data.ItemSprite;
            this.Price = _data.SellPrice;
            this.txt_price.text = ("￥"+this.Price).ToString();
            var rotate = Quaternion.Euler(0.0f, 0.0f, (_rotate*=-1.0f));
            this.txt_price.transform.localRotation = rotate;
            this.itemGenerator = GameObject.FindGameObjectWithTag("DropItemGenerator").GetComponent<DropItemGenerator>();
            //金額用UIコンポーネントの取得
            this.isInit = true;
        }

        /// <summary>
        /// アイテムをクリックしたときの処理
        /// </summary>
        public void OnClickEnterDropItem()
        {
            StartCoroutine(GetItem());
        }

        /// <summary>
        /// アイテムの取得
        /// </summary>
        /// <returns></returns>
        private IEnumerator GetItem()
        {
            if (!isInit) yield return null;

            this.animator.SetTrigger("get");
            this.txt_price.gameObject.SetActive(true);
            this.itemGenerator.DeleteSelectList(this.gameObject);

            yield return new WaitForSeconds(1.0f);
            //秒後に所持金アップ
        }
    }
}

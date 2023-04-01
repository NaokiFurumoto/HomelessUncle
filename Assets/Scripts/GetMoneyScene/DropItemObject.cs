using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GetMoney
{
    public class DropItemObject : MonoBehaviour
    {
        private Animator _animator;
        private SpriteRenderer _sprite;
        private bool _isInit;
        public int Price;

        private void Awake()
        {
            this._isInit = false;
            this._sprite = GetComponent<SpriteRenderer>();
            this._animator = GetComponent<Animator>();
        }

        public void SetData(DropItem data)
        {
            if (data == null) return;

            this._sprite.sprite = data.ItemSprite;
            this.Price = data.SellPrice;
            this._isInit = true;
        }

        /// <summary>
        /// アイテムをクリックしたときの処理
        /// </summary>
        public void OnClickDropItem()
        {
            if (!_isInit) return;

            //取得アニメーションの実行
            //秒後に消す:アニメーション終了後
            //所持金アップ
        }
    }
}

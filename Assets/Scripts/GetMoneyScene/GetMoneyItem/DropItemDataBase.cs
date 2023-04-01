using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//ドロップアイテムデータベース
namespace GetMoney {

    [CreateAssetMenu(fileName = "DropItemDataBase", menuName = "CreateDropItemDataaBase")]
    public class DropItemDataBase : ScriptableObject
    {
        [SerializeField]
        private List<DropItem> allDropDataLists = new List<DropItem>();

        public List<DropItem> AllDropDataList => allDropDataLists;
    }
}

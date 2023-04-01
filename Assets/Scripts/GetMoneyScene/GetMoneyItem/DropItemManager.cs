using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GetMoney
{
    public class DropItemManager : SingletonMonoBehaviour<DropItemManager>
    {
        [SerializeField]
        private DropItemDataBase dropItemDataBase;

        private List<DropItem> allDropItemLists = new List<DropItem>();

        private List<DropItem> normalDropItemLists = new List<DropItem>();
        private List<DropItem> rareDropItemLists = new List<DropItem>();
        private List<DropItem> sRareDropItemLists = new List<DropItem>();
        private List<DropItem> ssRareDropItemLists = new List<DropItem>();
        private List<DropItem> uRareDropItemLists = new List<DropItem>();

        public List<DropItem> AllDropItemLists => allDropItemLists;
        public List<DropItem> NormalDropItemLists => normalDropItemLists;
        public List<DropItem> RareDropItemLists => rareDropItemLists;
        public List<DropItem> SRareDropItemLists => sRareDropItemLists;
        public List<DropItem> SSRareDropItemLists => ssRareDropItemLists;
        public List<DropItem> URareDropItemLists => uRareDropItemLists;

        private void Awake()
        {
            normalDropItemLists = allDropItemLists.Where(item => item.Rarity == DropItem.DROPITEM_RARITY.N).ToList();
            rareDropItemLists = allDropItemLists.Where(item => item.Rarity == DropItem.DROPITEM_RARITY.R).ToList();
            sRareDropItemLists = allDropItemLists.Where(item => item.Rarity == DropItem.DROPITEM_RARITY.SR).ToList();
            ssRareDropItemLists = allDropItemLists.Where(item => item.Rarity == DropItem.DROPITEM_RARITY.SSR).ToList();
            uRareDropItemLists = allDropItemLists.Where(item => item.Rarity == DropItem.DROPITEM_RARITY.UR).ToList();
        }

        //インデックスからリストを取得する
        public List<DropItem> GetRareList(int _index)
        {
            List<DropItem> result = null;

            switch (_index)
            {
                case (int)DropItem.DROPITEM_RARITY.N:
                    result = normalDropItemLists;
                    break;
                case (int)DropItem.DROPITEM_RARITY.R:
                    result = rareDropItemLists;
                    break;
                case (int)DropItem.DROPITEM_RARITY.SR:
                    result = sRareDropItemLists;
                    break;
                case (int)DropItem.DROPITEM_RARITY.SSR:
                    result = ssRareDropItemLists;
                    break;
                case (int)DropItem.DROPITEM_RARITY.UR:
                    result = uRareDropItemLists;
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}

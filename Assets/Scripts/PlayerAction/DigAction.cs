using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static GlobalValue;

/// <summary>
/// �@��A�N�V�����F��{���ʃN���X���~����
/// </summary>
public class DigAction : MonoBehaviour
{
    // N=0/R=1/SR=2/SSR=3/UR=4/�͂���=5�Ƃ���
    [SerializeField]
    private List<int> digRatioList = new List<int>();

    //�@��o���A�C�e�����X�g
    private List<ItemData> digItemDataList = new List<ItemData>();

    /// <summary> �@��o���ꂽ�A�C�e�� </summary>
    //private ItemData diggingItem;

    //�A�N�V�����\����
    //private bool isAction;

    #region �v���p�e�B
    //public bool IsAction { get { return isAction; } set { isAction = value; } }
    //public ItemData DiggingItem => diggingItem;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        digItemDataList = ItemManager.Instance.DigItemDataLists;
    }

    #region �A�C�e���ݒ菈��
    /// <summary> �@��o���A�C�e���̎擾 </summary>
    public ItemData GetDigItem()
    {
        var rareNo = GetRearityNo();
        if (rareNo == DIG_MISSING_NO)
        {
            //�͂���
            return null;
        }
        else
        {
            var items = digItemDataList.Where(item => (int)item.Rarity == rareNo).ToList();
            return SelectDigItem(items);
        }
    }

    /// <summary> �@��o�����A�x�擾 </summary>
    private int GetRearityNo()
    {
        var totalRatio = TotalRareRatio();
        var createNum = Random.Range(1, totalRatio + 1);

        var total = 0;
        for (int i = 0; i < digRatioList.Count; i++)
        {
            total += digRatioList[i];
            if (createNum <= total)
            {
                return i;
            }
        }

        return DIG_MISSING_NO;
    }

    /// <summary> ���A�x�m�����v </summary> �o�t�F�Z���T�[/PlayerItem/��/�w���A�C�e��
    private int TotalRareRatio()
    {
        //���̑O�Ƀo�t��ǉ��ݒ�
        return digRatioList.Sum();
    }

    /// <summary> �@��o���A�C�e���擾 </summary>
    private ItemData SelectDigItem(List<ItemData> items = null)
    {
        if (!(items?.Count > 0))
            return null;

        var count = items.Count;
        if (count == 1)
        {
            return items.FirstOrDefault();
        }

        var num = Random.Range(0, count);
        return items[num];
    }
    #endregion

}

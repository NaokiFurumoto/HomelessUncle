using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// �����A�C�e���N���X
/// </summary>
public partial class Player : MonoBehaviour
{
    //�A�C�e���Ə�����
    private static Dictionary<string,int> playerHoldItems = new Dictionary<string,int>();
    //�擾
    public Dictionary<string, int> PlayerHoldItems => playerHoldItems;
    public int HaveItemsCount => playerHoldItems.Count();

    /// <summary>
    /// �_�~�[�f�[�^�o�^
    /// </summary>
    void AwakeHoldItemes()
    {
        //playerHoldItems.Add("�S����", 2);
        //playerHoldItems.Add("���z��", 10);
        //playerHoldItems.Add("��z��", 999);
        //playerHoldItems.Add("���z��", 8000);
        //playerHoldItems.Add("覐�", 10);
    }
    /// <summary>
    /// �A�C�e���̒ǉ�����
    /// �������������甄�p
    /// </summary>
    /// <param name="data"></param>
    /// <param name="index"></param>
    public void SetHoldPlayerItem(ItemData data, int index)
    {
        var name = data.ItemName;        //���O
        var maxCount = data.MaxHaveIndex;//�ő及����
        var price = data.SellPrice;      //���l

        //�L�[�����݂��Ă���   
        if (playerHoldItems.ContainsKey(name))
        {
            int value = 0;
            playerHoldItems.TryGetValue(name, out value);

            var total = value + index;
            var diff = total - maxCount;
            if(diff > 0)
            {
                total = maxCount;
                price = (price * diff);
                SellOverItem(price);
            }
            else if(diff == 0)
            {
                total = maxCount;
            }

            playerHoldItems[name] = total;
            return;
        }

        playerHoldItems.Add(name, index);
        //�����ŃZ�[�u������
    }

    /// <summary>
    /// �A�C�e�����p
    /// </summary>
    /// <param name="sellPrice"></param>
    private void SellOverItem(int sellPrice) { }
}

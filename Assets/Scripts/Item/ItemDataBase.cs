using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ItemDataBase", menuName = "CreateItemDataBase")]
public class ItemDataBase : ScriptableObject
{
    /// <summary> �A�C�e���e�[�u�����X�g <summary>
    [SerializeField]
    private List<ItemData> allItemDataLists = new List<ItemData>();
    public List<ItemData> AllItemTableLists => allItemDataLists;
}

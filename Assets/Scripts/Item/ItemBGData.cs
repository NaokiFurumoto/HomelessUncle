using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemBGData", menuName = "CreateItemBGData")]
public class ItemBGData : ScriptableObject
{
    /// <summary> �A�C�R���摜 </summary>
    [SerializeField]
    [Header("�w�i�摜")]
    private List<Sprite> bgImages;

    public List<Sprite> BGImagesList => bgImages;

}

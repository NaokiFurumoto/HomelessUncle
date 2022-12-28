using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemBGData", menuName = "CreateItemBGData")]
public class ItemBGData : ScriptableObject
{
    /// <summary> ƒAƒCƒRƒ“‰æ‘œ </summary>
    [SerializeField]
    [Header("”wŒi‰æ‘œ")]
    private List<Sprite> bgImages;

    public List<Sprite> BGImagesList => bgImages;

}

/**************************************************************************/
/*! @file   UIImageArray.cs
    @brief  UIImageArray
***************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[AddComponentMenu("Scripts/System/UI/ImageArray")]
public class UIImageArray : Image
{
    [CustomFieldAttribute("画像配列", CustomFieldAttribute.Type.UISprites)]
    public Sprite[] Images = new Sprite[0];

    private int mImageIndex = 0;        // get追加(ticket/6518)

    public int ImageIndex
    {
        get { return mImageIndex; }     // get追加(ticket/6518)
        set
        {
            if (0 <= value && value < Images.Length)
            {
                sprite = Images[value];
                mImageIndex = value;    // get追加(ticket/6518)
            }
            else
            {
                Debug.Log("範囲外のインデックスが指定されました。");
            }
        }
    }

    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        // SpriteがNULLの時は何も描画しない
        if (sprite == null)
        {
            toFill.Clear();
            return;
        }

        base.OnPopulateMesh(toFill);
    }
}


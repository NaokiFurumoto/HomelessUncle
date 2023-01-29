/**************************************************************************/
/*! @file   UISpriteSheet.cs
    @brief  スプライトシート
***************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif


[System.Serializable]
public class UISpriteSheet : ScriptableObject
#if UNITY_EDITOR
    , CustomFieldInterface
#endif
{
    // スプライト情報
    [System.Serializable]
    public class SpriteInfo
    {
        [CustomFieldAttribute("キー", CustomFieldAttribute.Type.String)]
        public string Key;

        [CustomFieldAttribute("スプライト", CustomFieldAttribute.Type.UISprite)]
        public Sprite Sprite;
    }

    #region フィールド

    [CustomFieldAttribute("スプライト配列", CustomFieldAttribute.Type.Custom)]
    public SpriteInfo[] SpriteInfos = null;

    // --------------------------------------------

    #endregion

    #region プロパティ

    public SpriteInfo this[string key] { get { return GetInfo(key); } }

    #endregion

    //=========================================================================
    //. 初期化/破棄
    //=========================================================================
    #region 初期化/破棄

    #endregion

    //=========================================================================
    //. 取得
    //=========================================================================
    #region 取得

    /// ***********************************************************************
    /// <summary>
    /// スプライト情報の取得
    /// </summary>
    /// ***********************************************************************
    public SpriteInfo GetInfo(string key)
    {
        if (SpriteInfos != null)
        {
            for (int i = 0; i < SpriteInfos.Length; ++i)
            {
                SpriteInfo info = SpriteInfos[i];
                if (info != null)
                {
                    if (info.Key == key)
                    {
                        return info;
                    }
                }
            }
        }
        return null;
    }

    /// ***********************************************************************
    /// <summary>
    /// スプライトの取得
    /// </summary>
    /// ***********************************************************************
    public Sprite GetSprite(string key)
    {
        SpriteInfo info = GetInfo(key);
        if (info != null)
        {
            return info.Sprite;
        }
        return null;
    }

    #endregion

    //=========================================================================
    //. エディタ
    //=========================================================================
    #region エディタ
#if UNITY_EDITOR

    private EditorHelp.ReorderableList<SpriteInfo> m_EditorSpriteInfos = null;

    /// ***********************************************************************
    /// <summary>
    /// カスタムプロパティ
    /// </summary>
    /// ***********************************************************************
    public void OnCustomProperty(CustomFieldAttribute attr, UnityEditor.SerializedProperty prop, float width)
    {
        if (prop.name == "SpriteInfos")
        {
            // 初期化
            if (m_EditorSpriteInfos == null)
            {
                m_EditorSpriteInfos = new EditorHelp.ReorderableList<SpriteInfo>();
                m_EditorSpriteInfos.Initialize(this, SpriteInfos, OnGUI_SpriteSheet, UnityEditor.EditorGUIUtility.singleLineHeight + 55);
            }
            else
            {
                m_EditorSpriteInfos.SetArray(SpriteInfos);
            }

            // 更新
            m_EditorSpriteInfos.Update();

            // 変更があったら戻す
            if (m_EditorSpriteInfos.isChanged)
            {
                SpriteInfos = m_EditorSpriteInfos.ToArray();
            }
        }
    }

    /// ***********************************************************************
    /// <summary>
    /// SpriteSheetGUI
    /// </summary>
    /// ***********************************************************************
    private void OnGUI_SpriteSheet(Rect rect, EditorHelp.ReorderableList<SpriteInfo> reorderableList, int index)
    {
        SpriteInfo value = reorderableList.GetValue(index);
        if (value != null)
        {
            Rect elementRect = rect;
            int buttonWidth = 30;

            elementRect.x += 1;
            elementRect.y += 1;
            elementRect.width -= 2;
            elementRect.height = reorderableList.list.elementHeight - 2; //EditorGUIUtility.singleLineHeight;
            GUI.Box(elementRect, "");

            elementRect = rect;
            elementRect.x += elementRect.width - buttonWidth;
            elementRect.y += 2;
            elementRect.width = buttonWidth;
            elementRect.height = EditorGUIUtility.singleLineHeight;
            if (GUI.Button(elementRect, "×"))
            {
                reorderableList.OnChangeBegin();
                reorderableList.RemoveValue(value);
                reorderableList.OnChangeEnd();
            }

            elementRect = rect;
            elementRect.x += 2;
            elementRect.y += 2;
            elementRect.width -= buttonWidth + 4;
            elementRect.height = EditorGUIUtility.singleLineHeight;

            string nextKey = EditorGUI.TextField(elementRect, "Key", value.Key);
            if (nextKey != value.Key)
            {
                reorderableList.OnChangeBegin();
                value.Key = nextKey;
                reorderableList.OnChangeEnd();
            }
            elementRect.y += elementRect.height;

            elementRect.height = 50;
            Sprite nextSprite = EditorGUI.ObjectField(elementRect, "Sprite", value.Sprite, typeof(Sprite), true) as Sprite;
            if (nextSprite != value.Sprite)
            {
                reorderableList.OnChangeBegin();
                value.Sprite = nextSprite;
                reorderableList.OnChangeEnd();
            }
            elementRect.y += elementRect.height;
        }
    }

#endif //UNITY_EDITOR
    #endregion エディタ
}

//=========================================================================
//. スプライトシート(EDITOR)
//=========================================================================
#if UNITY_EDITOR

[UnityEditor.CustomEditor(typeof(UISpriteSheet), true)]
public class EditorInspactor_UISpriteSheet : UnityEditor.Editor
{
    // 新しいアセットを作成する
    [UnityEditor.MenuItem("Assets/Create/ScriptableObject/スプライトシート", false, 1)]
    public static void CreateAsset()
    {
        EditorHelp.CreateScriptableObject<UISpriteSheet>(DoCreateAction);
    }

    // 生成コールバック
    private static void DoCreateAction(ScriptableObject sobj, string pathName, string resourceFile)
    {
        // ラベル設定
        AssetDatabase.SetLabels(sobj, new string[] { "Data", "ScriptableObject", "SpriteSheet" });
    }

    /// ***********************************************************************
    /// <summary>
    /// インスペクタ
    /// </summary>
    /// ***********************************************************************
    public override void OnInspectorGUI()
    {
        CustomFieldAttribute.OnInspectorGUI(typeof(UISpriteSheet), serializedObject);
    }
}

#endif

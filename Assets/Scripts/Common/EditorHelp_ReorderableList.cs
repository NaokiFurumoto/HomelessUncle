/**************************************************************************/
/*! @file   EditorHelp_ReorderableList.cs
    @brief  エディタヘルプ
***************************************************************************/
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;


#if UNITY_EDITOR
    //=========================================================================
    //. エディタヘルプ
    //=========================================================================
    public static partial class EditorHelp
    {
        //=========================================================================
        //. EditorHelp_ReorderableList
        //=========================================================================
        public class ReorderableList<T>
        {
            //=========================================================================
            //. メンバ
            //=========================================================================

            public delegate void GUIInspector(Rect rect, ReorderableList<T> reorderableList, int index);

            #region フィールド

            private UnityEditorInternal.ReorderableList m_ReorderableList;
            private Rect m_HeaderRect;

            private UnityEngine.Object m_Owner;
            private List<T> m_List;
            private GUIInspector m_GuiInspector;

            private bool m_Changed;

            #endregion フィールド

            #region プロパティ

            public UnityEditorInternal.ReorderableList list { get { return m_ReorderableList; } }

            public bool isChanged { get { return m_Changed; } }

            #endregion プロパティ

            //=========================================================================
            //. 初期化/破棄
            //=========================================================================
            #region 初期化/破棄

            /// ***********************************************************************
            /// <summary>
            /// 初期化
            /// </summary>
            /// ***********************************************************************
            public void Initialize(UnityEngine.Object owner, List<T> list, GUIInspector gui, float guiHeight)
            {
                m_Owner = owner;
                m_List = list;
                m_GuiInspector = gui;

                m_ReorderableList = new UnityEditorInternal.ReorderableList(m_List, typeof(T), true, true, false, false);

                m_ReorderableList.showDefaultBackground = false;
                m_ReorderableList.elementHeight = guiHeight; // UnityEditor.EditorGUIUtility.singleLineHeight * 1;

                // ヘッダーの描画
                m_ReorderableList.drawHeaderCallback = GUI_DrawHeader;

                // フッターの描画
                m_ReorderableList.drawFooterCallback = GUI_DrawFooter;

                // 要素の描画コールバック
                m_ReorderableList.drawElementCallback = GUI_DrawElement;

                // イベントコールバック
                m_ReorderableList.onReorderCallback = GUI_OnReorder;
            }

            /// ***********************************************************************
            /// <summary>
            /// 初期化
            /// </summary>
            /// ***********************************************************************
            public void Initialize(UnityEngine.Object owner, T[] array, GUIInspector gui, float guiHeight)
            {
                SetArray(array);
                Initialize(owner, m_List, gui, guiHeight);
            }

            /// ***********************************************************************
            /// <summary>
            /// 破棄
            /// </summary>
            /// ***********************************************************************
            public void Release()
            {
            }

            #endregion 初期化/破棄

            //=========================================================================
            //. 更新
            //=========================================================================
            #region 更新

            /// ***********************************************************************
            /// <summary>
            /// 更新
            /// </summary>
            /// ***********************************************************************
            public void Update()
            {
                // 変更フラグ
                m_Changed = false;

                // 毎回リストを渡す
                m_ReorderableList.list = m_List;

                // リスト表示
                m_ReorderableList.DoLayoutList();
            }

            #endregion 更新

            //=========================================================================
            //. 設定/取得
            //=========================================================================
            #region 設定/取得

            /// ***********************************************************************
            /// <summary>
            /// リストを設定
            /// </summary>
            /// ***********************************************************************
            public void SetList(List<T> list)
            {
                m_List = list;
            }

            /// ***********************************************************************
            /// <summary>
            /// 配列を設定
            /// </summary>
            /// ***********************************************************************
            public void SetArray(T[] array)
            {
                m_List = new List<T>();
                m_List.AddRange(array);
            }

            /// ***********************************************************************
            /// <summary>
            /// リストを取得
            /// </summary>
            /// ***********************************************************************
            public List<T> ToList()
            {
                return m_List;
            }

            /// ***********************************************************************
            /// <summary>
            /// リストを配列へ変換
            /// </summary>
            /// ***********************************************************************
            public T[] ToArray()
            {
                return m_List.ToArray();
            }

            /// ***********************************************************************
            /// <summary>
            /// 要素を取得
            /// </summary>
            /// ***********************************************************************
            public T GetValue(int index)
            {
                if (index >= 0 && index < m_ReorderableList.list.Count)
                {
                    return (T)m_ReorderableList.list[index];
                }
                return default(T);
            }

            /// ***********************************************************************
            /// <summary>
            /// 要素を設定
            /// </summary>
            /// ***********************************************************************
            public void SetValue(int index, T value)
            {
                if (index >= 0 && index < m_ReorderableList.list.Count)
                {
                    m_ReorderableList.list[index] = value;
                }
            }

            /// ***********************************************************************
            /// <summary>
            /// 要素を削除
            /// </summary>
            /// ***********************************************************************
            public void RemoveValue(T value)
            {
                m_List.Remove(value);
            }

            #endregion 設定/取得

            //=========================================================================
            //. GUI
            //=========================================================================
            #region GUI

            /// ***********************************************************************
            /// <summary>
            /// ヘッダーの描画
            /// </summary>
            /// ***********************************************************************
            private void GUI_DrawHeader(Rect rect)
            {
                m_HeaderRect = rect;

                GUI.contentColor = new Color(0.9f, 0.9f, 0.9f);
                UnityEditor.EditorGUI.LabelField(rect, "オブジェクトリスト", EditorHelp.GUIStyles.GroupSubTitleLeft);

                GUI.contentColor = Color.white;
                GUI.backgroundColor = Color.white;
                GUI.color = Color.white;

                rect.x = rect.x + rect.width - 30 - 3;
                rect.width = 30;

                if (GUI.Button(rect, "＋"))
                {
                    OnChangeBegin();
                    m_List.Add(default(T));
                    OnChangeEnd();
                }
            }

            /// ***********************************************************************
            /// <summary>
            /// フッターの描画
            /// </summary>
            /// ***********************************************************************
            private void GUI_DrawFooter(Rect rect)
            {
            }

            /// ***********************************************************************
            /// <summary>
            /// 要素の描画
            /// </summary>
            /// ***********************************************************************
            private void GUI_DrawElement(Rect rect, int index, bool isActive, bool isFocused)
            {
                if (index >= 0 && index < m_ReorderableList.list.Count)
                {
                    rect.width -= 5;
                    if (m_GuiInspector != null)
                    {
                        m_GuiInspector(rect, this, index);
                    }
                }
            }

            /// ***********************************************************************
            /// <summary>
            /// 要素入れ替え時のイベント
            /// </summary>
            /// ***********************************************************************
            private void GUI_OnReorder(UnityEditorInternal.ReorderableList reorderableList)
            {
                IList tmp = reorderableList.list;
                if (m_List.Count == tmp.Count)
                {
                    OnChangeBegin();
                    for (int i = 0; i < tmp.Count; ++i)
                    {
                        m_List[i] = (T)tmp[i];
                    }
                    OnChangeEnd();
                }
            }

            /// ***********************************************************************
            /// <summary>
            /// 要素変更前イベント
            /// </summary>
            /// ***********************************************************************
            public void OnChangeBegin()
            {
                if (m_Owner != null)
                {
                    UnityEditor.Undo.RecordObjects(new UnityEngine.Object[] { m_Owner }, typeof(T).Name);
                }
            }

            /// ***********************************************************************
            /// <summary>
            /// 要素変更後イベント
            /// </summary>
            /// ***********************************************************************
            public void OnChangeEnd()
            {
                if (m_Owner != null)
                {
                    UnityEditor.EditorUtility.SetDirty(m_Owner);
                }

                m_Changed = true;
            }

            #endregion GUI
        }
    }

#endif

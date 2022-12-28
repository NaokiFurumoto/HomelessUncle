using UnityEngine;
using TMPro;
using System.Linq;
using Carbon;
using System;
using System.Text;

#if UNITY_EDITOR
using UnityEditor;
using FontSettings = Shock.TextMeshUISettings.FontSettings;
#endif

namespace Shock
{
    /// <summary>
    /// TextMeshProUGUIのMaterial設定を行うクラス
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class TextMeshUI : RectTransformBehaviour
    {
        /// <summary>
        /// IntCharArray デフォルト長さ
        /// </summary>
        private const int IntegerCharArrayDefaultLength = 8;

        /// <summary>
        /// フォントサイズの列挙型
        /// </summary>
        public enum FontSizeType
        {
            Custom    = 0,    // フォントのサイズを自身で設定する場合
            Small    = 1,    // フォントのサイズ：小
            Medium    = 2,    // フォントのサイズ：中
            Large    = 3,    // フォントのサイズ：大
        }

        //---------------------------------------------------------------------
        //    SerializeField
        //---------------------------------------------------------------------
        [SerializeField] private TMP_Text        m_textMesh        = null;
        [SerializeField] private string            m_settingsName    = string.Empty;
        [SerializeField] private FontSizeType    m_fontSizeType    = FontSizeType.Medium;

        //---------------------------------------------------------------------
        //    Field
        //---------------------------------------------------------------------
        private char[] m_IntegerCharArray = new char[IntegerCharArrayDefaultLength];

        //---------------------------------------------------------------------
        //    Unity
        //---------------------------------------------------------------------
        private void Awake()
        {
            SafeAttach();
        }

        //---------------------------------------------------------------------
        //    メンバ関数(public)
        //---------------------------------------------------------------------

        /// <summary>
        /// テキストを取得します
        /// </summary>
        public string GetText()
        {
            SafeAttach();
            return m_textMesh.text;
        }

        /// <summary>
        /// 空文字のテキストを設定します
        /// </summary>
        public void SetEmptyText()
        {
            SafeAttach();
            SetText(string.Empty);
        }

        /// <summary>
        /// テキストを設定します
        /// </summary>
        public void SetText(string text)
        {
            SafeAttach();
            m_textMesh.text = text;
        }

        /// <summary>
        /// テキストを設定します
        /// </summary>
        public void SetText(long longText)
        {
            SafeAttach();
            // GC 避け --> UpdateText
            m_textMesh.text = longText.ToString();
        }

        /// <summary>
        /// テキストを設定します
        /// </summary>
        public void SetText(float floatText)
        {
            SafeAttach();
            // GC 避け --> UpdateText
            m_textMesh.text = floatText.ToString();
        }

        /// <summary>
        /// テキストを設定します
        /// </summary>
        public void UpdateText(long longText)
        {
            SafeAttach();
            m_textMesh.SetText("{0}", longText);
        }

        /// <summary>
        /// テキストを設定します
        /// </summary>
        public void UpdateText(float floatText)
        {
            SafeAttach();
            m_textMesh.SetText("{0}", floatText);
        }

        /// <summary>
        /// テキストを設定します
        /// </summary>
        public void UpdateText(StringBuilder stringBuilder)
        {
            SafeAttach();
            m_textMesh.SetText(stringBuilder);
        }

        /// <summary>
        /// テキストを設定します
        /// </summary>
        public void UpdateText(char[] charArray)
        {
            SafeAttach();
            m_textMesh.SetCharArray(charArray);
        }

        /// <summary>
        /// テキストを設定します
        /// </summary>
        public void SetText(string text, object arg0)
        {
            SafeAttach();
            m_textMesh.text = text.AsFormat(arg0);
        }

        /// <summary>
        /// テキストを設定します
        /// </summary>
        public void SetText(string text, object arg0, object arg1)
        {
            SafeAttach();
            m_textMesh.text = text.AsFormat(arg0, arg1);
        }

        /// <summary>
        /// テキストを設定します
        /// </summary>
        public void SetText(string text, object arg0, object arg1, object arg2)
        {
            SafeAttach();
            m_textMesh.text = text.AsFormat(arg0, arg1, arg2);
        }

        /// <summary>
        /// テキストを設定します
        /// </summary>
        public void SetText(string text, object arg0, object arg1, object arg2, object arg3)
        {
            SafeAttach();
            m_textMesh.text = text.AsFormat(arg0, arg1, arg2, arg3);
        }

        /// <summary>
        /// テキストを設定します
        /// </summary>
        public void SetText(string text, params object[] args)
        {
            SafeAttach();
            m_textMesh.text = text.AsFormat(args);
        }

        /// <summary>
        /// アルファ値の設定
        /// </summary>
        /// <param name="alpha"></param>
        public void SetAlpha(float alpha)
        {
            SafeAttach();
            m_textMesh.alpha = alpha;
        }

        /// <summary>
        /// テキストの横幅
        /// スペース分の幅は無視される
        /// </summary>
        /// <returns></returns>
        public float GetTextWidth(string text)
        {
            SafeAttach();
            return m_textMesh.GetPreferredValues(text).x;
        }
        public float GetTextWidth()
        {
            SafeAttach();
            return m_textMesh.GetPreferredValues().x;
        }

        /// <summary>
        /// テキストの縦幅
        /// </summary>
        /// <returns></returns>
        public float GetTextHeight(string text)
        {
            SafeAttach();
            return m_textMesh.GetPreferredValues(text).y;
        }
        public float GetTextHeight()
        {
            SafeAttach();
            return m_textMesh.GetPreferredValues().y;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="textAlignmentOptions"></param>
        public void SetAlignment(TextAlignmentOptions textAlignmentOptions)
        {
            m_textMesh.alignment = textAlignmentOptions;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="margin"></param>
        public void SetMargin(Vector4 margin)
        {
            m_textMesh.margin = margin;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="isEnable"></param>
        public void SetTextEnable(bool isEnable)
        {
            m_textMesh.enabled = isEnable;
        }

        /// <summary>
        /// 描画順設定 (TextMeshPro ONLY)
        /// </summary>
        public void SetSortingOrder(int sortingOrder)
        {
            var textMeshPro = m_textMesh as TextMeshPro;
            if (textMeshPro)
            {
                textMeshPro.sortingOrder = sortingOrder;
            }
        }

        /// <summary>
        /// テキストカラー設定
        /// </summary>
        public void SetTextColor(Color color)
        {
            m_textMesh.color = color;
        }

        //---------------------------------------------------------------------
        //    メンバ関数(private)
        //---------------------------------------------------------------------
        /// <summary>
        /// signed 整数の Char 配列を m_IntegerCharArray に作成し, 長さを返す.
        /// </summary>
        private int CreateIntergerCharArray(long arg)
        {
            if (arg == 0) {
                m_IntegerCharArray[0] = '0';
                return 1;
            }

            const int zero = '0';

            // value
            ulong value = 0;

            // 長さ初期化
            int length = 0;

            // 負数
            if (arg < 0) {
                if (arg == long.MinValue) {
                    value = (ulong)long.MaxValue + 1;
                }
                else {
                    value = (ulong)(-arg);
                }
                m_IntegerCharArray[0] = '-';
                length += 1;
            }
            else {
                value = (ulong)arg;
            }

            // 数字数
            int digitCount = ((int)Math.Floor(Math.Log10(value)) + 1);

            // 長さ += 数字数
            length += digitCount;

            // 配列長さ足りないなら拡張する
            if (length > m_IntegerCharArray.Length) {
                Array.Resize(ref m_IntegerCharArray, length);
            }

            for (int i = length - 1; i >= length - digitCount; i--) {
                m_IntegerCharArray[i] = (char)(zero + (value % 10));
                value /= 10;
            }

            return length;
        }

        /// <summary>
        /// TextMeshPro移行による保険
        /// </summary>
        private void SafeAttach()
        {
            if( !this )
            {
                return;
            }
            if( m_textMesh )
            {
                return;
            }
            m_textMesh = GetComponent<TMP_Text>();
        }
        //---------------------------------------------------------------------
        //    UNITY_EDITOR
        //---------------------------------------------------------------------
#if UNITY_EDITOR
        //---------------------------------------------------------------------
        //    MonoBehaviour
        //---------------------------------------------------------------------
        private void Reset()
        {
            m_textMesh = GetComponent<TMP_Text>();
            if (m_textMesh == null) {
                return;
            }
            m_textMesh.raycastTarget = false;
            ApplySettings(TextMeshUISettings.Get().SettingsList[0]);
        }


        //---------------------------------------------------------------------
        //    メンバ変数
        //---------------------------------------------------------------------
        private int m_index = -1;    // 設定項目のindex

        //---------------------------------------------------------------------
        //    メンバ関数
        //---------------------------------------------------------------------
        /// <summary>
        /// indexを設定リストの範囲に丸める
        /// </summary>
        private void ClampIndex()
        {
            var settings = TextMeshUISettings.Get();
            m_index = settings.SettingsList.FindIndex(s => s.m_settingName == m_settingsName);
            m_index = Mathf.Clamp(m_index, 0, settings.SettingsList.Count - 1);
        }

        /// <summary>
        /// 設定を適用
        /// </summary>
        public void ApplySettings()
        {
            var settingsList = TextMeshUISettings.Get().SettingsList;
            ClampIndex();
            ApplySettings(settingsList[m_index]);
        }


        /// <summary>
        /// 設定を適用
        /// </summary>
        /// <param name="settings"></param>
        private void ApplySettings(FontSettings settings)
        {
            //    設定名を保存
            m_settingsName = settings.m_settingName;

            m_textMesh.font                 = settings.m_fontAsset;
            m_textMesh.fontSharedMaterial   = settings.m_material;
            m_textMesh.fontStyle            = settings.m_fontStyles;
            m_textMesh.color                = settings.m_color;
            m_textMesh.enableVertexGradient = settings.m_enableGradient;
            m_textMesh.overrideColorTags    = settings.m_overrideTags;
            m_textMesh.colorGradient        = settings.m_gradient;

            var fontSize = GetFontSize( settings, m_fontSizeType );
            m_textMesh.fontSize = fontSize;

            // AutoSize使用時は最大サイズにも設定する
            if( !m_textMesh.enableAutoSizing )
            {
                return;
            }
            m_textMesh.fontSizeMax = fontSize;
        }

        /// <summary>
        /// フォントのサイズを取得します
        /// </summary>
        public int GetFontSize( FontSettings settings, FontSizeType fontSizeType )
        {
            switch( fontSizeType )
            {
                case FontSizeType.Large  :  return settings.m_largeFontSize    ;
                case FontSizeType.Medium :  return settings.m_mediumFontSize;
                case FontSizeType.Small     :  return settings.m_smallFontSize    ;
                default : return (int)m_textMesh.fontSize;
            }
        }

        /// <summary>
        /// カスタムインスペクタ
        /// </summary>
        [CustomEditor(typeof(TextMeshUI))]
        public class Inspector : Editor
        {
            //========================================
            // 定数
            //========================================
            private const string  PopupFontSizeTypeFormat = "{0} ( {1}px )";

            public override void OnInspectorGUI()
            {
                var ui = target as TextMeshUI;
                var textMeshUISettings = TextMeshUISettings.Get();

                //    Script
                EditorGUI.BeginDisabledGroup(true);
                var guids  = AssetDatabase.FindAssets("t:textasset TextMeshUI");
                var path   = AssetDatabase.GUIDToAssetPath(guids.FirstOrDefault());
                var script = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
                EditorGUILayout.ObjectField("Script", script, typeof(TextAsset), false);
                EditorGUILayout.ObjectField("SettingsList", textMeshUISettings, typeof(TextMeshUISettings), false);
                EditorGUI.EndDisabledGroup();

                //    TestMeshProUGUI
                ui.m_textMesh = EditorGUILayout.ObjectField("TextMeshProUGI", ui.m_textMesh, typeof(TMP_Text), false) as TMP_Text;

                if (textMeshUISettings == null)
                {
                    EditorGUILayout.HelpBox("TextMeshUISettingsを作成してください", MessageType.Warning);
                    return;
                }

                //    FontSettings
                var settingsList = textMeshUISettings.SettingsList;

                // 設定ファイルが無ければ表示しない
                if( settingsList == null )
                {
                    return;
                }

                // index値調整
                ui.ClampIndex();

                // 設定ファイル情報取得
                var names            = settingsList.Select( s => s.ToNameWithMemo() ).ToArray();
                var selectIndex        = EditorGUILayout.Popup("FontSettings", ui.m_index, names );
                var fontSettings    = settingsList[selectIndex];

                var fontSizeTypes    = Enum.GetValues( typeof( FontSizeType ) ) as FontSizeType[];
                var index            = fontSizeTypes.FindIndex( c => c == ui.m_fontSizeType );

                var fontSizeTypeNames    = fontSizeTypes.Select( c => c == FontSizeType.Custom ? "Custom" : string.Format( PopupFontSizeTypeFormat, c, ui.GetFontSize(fontSettings,c) ) ).ToArray();
                var fontSizeType        = (FontSizeType)EditorGUILayout.Popup("フォントサイズ", index, fontSizeTypeNames );

                var isApply = selectIndex != ui.m_index || fontSizeType != ui.m_fontSizeType;
                if( !isApply )
                {
                    return;
                }

                ui.m_index            = selectIndex;
                ui.m_fontSizeType    = fontSizeType;

                ui.ApplySettings(settingsList[ui.m_index]);

                EditorUtility.SetDirty(ui);
            }
        }
#endif
    }
}

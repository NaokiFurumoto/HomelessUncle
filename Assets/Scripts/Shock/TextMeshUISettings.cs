#if UNITY_EDITOR
using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using Carbon;
using UniRx;
using UnityEditor;

namespace Shock
{
	/// <summary>
	/// TextMeshProの設定
	/// </summary>
	[CreateAssetMenu(menuName = "Shock/TextMeshPro/TextMeshUI Settings" )]
	public class TextMeshUISettings : ScriptableObject
	{
		//============================================
		// 定数
		//============================================
		private const string DetailFormat = "{0}：{1}";

		//---------------------------------------------------------------------
		//	メンバ変数(public)
		//---------------------------------------------------------------------
		[SerializeField] private List<FontSettings> m_settings = new List<FontSettings>();        //	フォント設定リスト


		//---------------------------------------------------------------------
		//	プロパティ
		//---------------------------------------------------------------------
		public IReadOnlyList<FontSettings> SettingsList
		{
			get { return m_settings; }
		}

		public FontSettings this[int index]
		{
			get { return m_settings[index];  }
			set { m_settings[index] = value; }
		}


		//---------------------------------------------------------------------
		//	インナークラス
		//---------------------------------------------------------------------
		[Serializable]
		public class FontSettings
		{
			public	string			m_settingName;      // 設定名
			public	string			m_memo;			    // メモ用フィールド
			public	TMP_FontAsset	m_fontAsset;        // フォントアセット
			public	Material		m_material;         // マテリアル
			public	FontStyles		m_fontStyles;       // フォントスタイル
			public	Color			m_color;            // フォントの頂点カラー
			public	bool			m_enableGradient;   // グラデーションを有効にするか
			public	bool			m_overrideTags;     // オーバーライドタグ
			public	VertexGradient	m_gradient;         // グラデーション
			public	int				m_largeFontSize;	// フォントサイズ：L
			public	int				m_mediumFontSize;	// フォントサイズ：M
			public	int				m_smallFontSize;	// フォントサイズ：S


			/// <summary>
			/// コンストラクタ
			/// </summary>
			public FontSettings()
			{
				m_settingName		= "New Settings";
				m_memo				= "";
				m_fontStyles		= FontStyles.Normal;
				m_color				= Color.white;
				m_enableGradient	= false;
				m_overrideTags		= false;
				m_gradient			= new VertexGradient(Color.white);
				m_largeFontSize		= 48;
				m_mediumFontSize	= 36;
				m_smallFontSize		= 28;
			}

			/// <summary>
			/// 設定名とメモを結合した文字列を返します
			/// </summary>
			public string ToNameWithMemo()
			{
				if( m_memo.IsNullOrWhiteSpace() )
				{
					return m_settingName;
				}
				return DetailFormat.AsFormat( m_settingName, m_memo );
			}
		}


		//---------------------------------------------------------------------
		//	静的メンバ関数(UNITY_EDITOR)
		//---------------------------------------------------------------------
		/// <summary>
		/// インスタンスを取得
		/// </summary>
		/// <returns></returns>
		public static TextMeshUISettings Get()
		{
			var guids = AssetDatabase.FindAssets("t:TextMeshUISettings");
			var path = AssetDatabase.GUIDToAssetPath(guids.FirstOrDefault());
			return AssetDatabase.LoadAssetAtPath<TextMeshUISettings>(path);
		}

		/// <summary>
		/// 全てのフォントアセットを取得
		/// </summary>
		/// <returns></returns>
		private static TMP_FontAsset[] FindFontAssetList()
		{
			return
			AssetDatabase
						.FindAssets("t:TMP_FontAsset")
						.Select(g => AssetDatabase.GUIDToAssetPath(g))
						.Select(p => AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(p))
						.ToArray();
		}

		/// <summary>
		/// マテリアルプリセットを取得
		/// </summary>
		/// <param name="assetPath"></param>
		/// <param name="fontAsset"></param>
		/// <returns></returns>
		private static Material[] FindMaterialPresets(TMP_FontAsset fontAsset)
		{
			var mats =
			AssetDatabase
					.FindAssets("t:material")
					.Select(g => AssetDatabase.GUIDToAssetPath(g))
					.Select(p => AssetDatabase.LoadAssetAtPath<Material>(p))
					.Where(m => m != null)
					.Where(m => m.name.StartsWith(fontAsset.name));

			return new Material[] { fontAsset.material }.Concat(mats).ToArray();
		}

		/// <summary>
		/// フォントアセットのパスを取得
		/// </summary>
		/// <param name="fontAsset"></param>
		/// <returns></returns>
		private static string GetFontAssetPath(TMP_FontAsset fontAsset)
		{
			return
			AssetDatabase
						.FindAssets("t:TMP_FontAsset")
						.Select(g => AssetDatabase.GUIDToAssetPath(g))
						.Select(p => UniRx.Tuple.Create(AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(p), p))
						.Where(t => t.Item1 == fontAsset)
						.Select(t => t.Item2)
						.FirstOrDefault();
		}

		//---------------------------------------------------------------------
		//	メンバ変数(UNITY_EDITOR)
		//---------------------------------------------------------------------
		 
		/// <summary>
		/// カスタムインスペクタ
		/// </summary>
		[CustomEditor(typeof(TextMeshUISettings))]
		public class Inspector : Editor
		{
			//---------------------------------------------------------------------
			//	メンバ変数(private)
			//---------------------------------------------------------------------
			private readonly List<bool> m_foldOutList = new List<bool>();
			private int	 m_length;

			//---------------------------------------------------------------------
			//	プロパティ
			//---------------------------------------------------------------------
			TextMeshUISettings settingsList { get { return target as TextMeshUISettings; } }


			/// <summary>
			/// Awake処理
			/// </summary>
			private void Awake()
			{
				m_length = settingsList.m_settings.Count;
				m_foldOutList.Set( new bool[m_length] );
			}

			/// <summary>
			/// Inspector表示
			/// </summary>
			public override void OnInspectorGUI()
			{
				if (AssetDatabase.FindAssets("t:TextMeshUISettings").Count() > 1)
				{
					EditorGUILayout.HelpBox("複数のScriptableObjectが存在します。TextMeshUISettingsは1つだけ生成してください。", MessageType.Warning);
				}

				//	Script
				DrawBaseScript();

				//	Size
				DrawSize();

				var removeList = new List<int>();

				//	各要素
				for (int i = 0; i < settingsList.m_settings.Count; ++i)
				{
					if( m_foldOutList[i] = EditorGUILayout.Foldout( m_foldOutList[i], settingsList[i].ToNameWithMemo(), true ) )
					{
						DrawSettingsName(i);
						DrawMemo(i);
						DrawFontAsset(i);
						DrawMaterialPresets(i);
						DrawColor(i);
						DrawFontSize(i);

						//	削除ボタン
						using (new GUILayout.HorizontalScope())
						{
							EditorGUILayout.LabelField("");
							if (GUILayout.Button("削除"))
							{
								removeList.Add(i);
							}
						}
					}
				}

				//	削除登録した要素を削除
				settingsList.m_settings = settingsList.m_settings.Where((s, i) => !removeList.Contains(i)).ToList();

				if( !removeList.IsEmpty() )
				{
					m_length = settingsList.m_settings.Count;
					m_foldOutList.Set( new bool[m_length] );
				}

				if (GUILayout.Button("現在のシーンの全てのオブジェクトに反映"))
				{
					EditorUtility.SetDirty(target);
					AssetDatabase.SaveAssets();
					ApplySceneObjects();
				}
			}

			
			/// <summary>
			/// シーンに配置されているTextMeshUIに設定を適用
			/// </summary>
			[MenuItem("Tools/Shock/UI/TextMeshPro/ApplySceneObjects")]
			private static void ApplySceneObjects()
			{
				GameObject
					.FindObjectsOfType<TextMeshUI>()
					.ForEach(t => t.ApplySettings());
			}

			/// <summary>
			/// このスクリプトへの参照フィールド
			/// </summary>
			private void DrawBaseScript()
			{
				//	Script
				EditorGUI.BeginDisabledGroup(true);
				var guids = AssetDatabase.FindAssets("t:textasset TextMeshUISettings");
				var path = AssetDatabase.GUIDToAssetPath(guids.FirstOrDefault());
				var script = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
				EditorGUILayout.ObjectField("Script", script, typeof(TextAsset), false);
				EditorGUI.EndDisabledGroup();
			}

			/// <summary>
			/// 設定リストのサイズ
			/// </summary>
			private void DrawSize()
			{
				//	リストサイズを変更する+ボタン
				using (new EditorGUILayout.HorizontalScope())
				{
					EditorGUILayout.LabelField("Size");
					if (GUILayout.Button("+"))
					{
						m_length++;
					}
				}
				EditorGUILayout.Separator();


				// リストサイズが0より小さくならないように
				if (m_length < 0)
				{
					m_length = 0;
				}

				// 増加してなければ処理しない
				if( m_length <= settingsList.m_settings.Count )
				{
					return;
				}

				//	リストサイズ増
				var font = FindFontAssetList().FirstOrDefault();
				var mat  = FindMaterialPresets(font).FirstOrDefault();

				while( settingsList.m_settings.Count < m_length )
				{
					settingsList.m_settings.Add( new FontSettings()
					{
						m_fontAsset = font,
						m_material  = mat
					});
					m_foldOutList.Add( true );
				}

				m_length = settingsList.m_settings.Count;
			}

			/// <summary>
			/// 設定項目名を描画します
			/// </summary>
			private void DrawSettingsName(int index)
			{
				settingsList[index].m_settingName = EditorGUILayout.TextField( "SettingsName", settingsList[index].m_settingName );
			}

			/// <summary>
			/// メモ項目を描画します
			/// </summary>
			private void DrawMemo(int index)
			{
				settingsList[index].m_memo = EditorGUILayout.TextField( "メモ", settingsList[index].m_memo );
			}

			/// <summary>
			/// フォントアセットを描画します
			/// </summary>
			private void DrawFontAsset(int index)
			{
				var fonts		= FindFontAssetList();
				var fontAsset	= settingsList[index].m_fontAsset;
				var fontIndex	= fontAsset == null ? 0 : fonts.FindIndex( font => font.GetInstanceID() == fontAsset.GetInstanceID() );

				//	FontAssest選択
				fontIndex = EditorGUILayout.Popup("FontAsset", fontIndex, fonts.Select(f=>f.name).ToArray());
				settingsList[index].m_fontAsset = fonts[fontIndex];
			}

			/// <summary>
			/// マテリアルプリセット
			/// </summary>
			/// <param name="index"></param>
			private void DrawMaterialPresets(int index)
			{
				var mats = FindMaterialPresets( settingsList[index].m_fontAsset );

				var materials		= FindMaterialPresets(settingsList[index].m_fontAsset);
				var material		= settingsList[index].m_material;
				var materialIndex	= material == null ? 0 : materials.FindIndex( mat => mat.GetInstanceID() == material.GetInstanceID() );
				materialIndex		= Mathf.Max( 0, materialIndex );

				//	MaterialPreset選択
				materialIndex = EditorGUILayout.Popup("MaterialPreset", materialIndex, mats.Select(m => m.name).ToArray());
				settingsList[index].m_material = mats[materialIndex];
			}

			/// <summary>
			/// フォントの頂点カラー
			/// </summary>
			/// <param name="index"></param>
			private void DrawColor(int index)
			{
				settingsList[index].m_color = EditorGUILayout.ColorField("Color(Vertex)", settingsList[index].m_color);

				using (new EditorGUILayout.HorizontalScope())
				{
					//	ColorGradient(toggle)
					settingsList[index].m_enableGradient = EditorGUILayout.Toggle("ColorGradient", settingsList[index].m_enableGradient);

					//	Override Tags
					settingsList[index].m_overrideTags = EditorGUILayout.Toggle("Override Tags", settingsList[index].m_overrideTags);
				}

				//	ColorGradient
				if (settingsList[index].m_enableGradient)
				{
					settingsList[index].m_gradient.topLeft     = EditorGUILayout.ColorField("TopLeft"    , settingsList[index].m_gradient.topLeft);
					settingsList[index].m_gradient.topRight    = EditorGUILayout.ColorField("TopRight"   , settingsList[index].m_gradient.topRight);
					settingsList[index].m_gradient.bottomLeft  = EditorGUILayout.ColorField("BottomLeft" , settingsList[index].m_gradient.bottomLeft);
					settingsList[index].m_gradient.bottomRight = EditorGUILayout.ColorField("BottomRight", settingsList[index].m_gradient.bottomRight);
				}
			}

			/// <summary>
			/// フォントのサイズ
			/// </summary>
			private void DrawFontSize(int index)
			{
				using (new EditorGUILayout.HorizontalScope())
				{
					GUILayout.Label("FontSize(PixelSize)");

					GUILayout.Label("Large");
					settingsList[index].m_largeFontSize	 = EditorGUILayout.IntField(settingsList[index].m_largeFontSize, GUILayout.Width(100) );

					GUILayout.Label("Medium");
					settingsList[index].m_mediumFontSize = EditorGUILayout.IntField( settingsList[index].m_mediumFontSize	, GUILayout.Width(100));

					GUILayout.Label("Small");
					settingsList[index].m_smallFontSize  = EditorGUILayout.IntField( settingsList[index].m_smallFontSize, GUILayout.Width(100)	);
				}
			}
		}
		
	}
}
#endif

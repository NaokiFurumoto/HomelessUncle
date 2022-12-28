using UnityEngine;
using TMPro;
using System.Linq;
using Carbon;
using System;
using UnityEngine.EventSystems;

namespace Shock
{
	/// <summary>
	/// TextMeshProUGUIのテキストにリンクがあった場合につけるコンポーネント
	/// </summary>
	[DisallowMultipleComponent]
	public class TextMeshUIOpenHyperlinks : CarbonBehaviour, IPointerClickHandler
	{
		private TMP_Text m_textMesh = null;

		private void Awake()
		{
			m_textMesh = GetComponent<TMP_Text>();
		}

		//---------------------------------------------------------------------
		//	メンバ関数(public)
		//---------------------------------------------------------------------
		/// <summary>
		/// テキストをクリックされた時
		/// </summary>
		/// <param name="eventData"></param>
		public void OnPointerClick(PointerEventData eventData)
		{
			//リンクが設定されていたらリンク先をブラウザで開く
			OnClickTextLink();
		}

		//---------------------------------------------------------------------
		//	メンバ関数(private)
		//---------------------------------------------------------------------
		/// <summary>
		/// テキストにリンクが設定されていたらブラウザを開く
		/// </summary>
		private void OnClickTextLink()
		{
			if (m_textMesh == null)
			{
				return;
			}
			var pos = Input.mousePosition;
			var cam = m_textMesh.canvas.worldCamera;
			if (cam == null)
			{
				return;
			}
			var index = TMP_TextUtilities.FindIntersectingLink(m_textMesh, pos, cam);
			if (index == -1)
			{
				return;
			}
			var linkInfo = m_textMesh.textInfo.linkInfo[index];
			var url = linkInfo.GetLinkID();
			Application.OpenURL(url.IsNullOrWhiteSpace() ? "" : new Uri(url).AbsoluteUri);
		}
	}
}
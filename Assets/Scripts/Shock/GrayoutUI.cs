using System.Linq;
using Carbon;
using UnityEngine;

namespace Shock
{
    [DisallowMultipleComponent]
    public sealed class GrayoutUI : MonoBehaviour
    {
        //============================================
        // メンバー変数(SerializeField)
        //============================================
        [SerializeField] private CanvasRenderer[] m_canvasRenderers = new CanvasRenderer[0];

        [SerializeField] private SpriteRenderer[] m_SpriteRendererList = new SpriteRenderer[0];

        [SerializeField] private TMPro.TextMeshPro[] m_TextMeshProList = new TMPro.TextMeshPro[0];

        //============================================
        //! メンバー変数
        //============================================
        private bool m_IsInitialized = false;

        private TMPro.TMP_SubMeshUI[] m_subMeshUIList = new TMPro.TMP_SubMeshUI[0];
        //============================================
        //! プロパティ
        //============================================
        public bool IsGrayout { private set; get; }

        //--------------------------------------------
        // MonoBehaviour
        //--------------------------------------------
        private void Reset()
        {
            m_canvasRenderers = GetComponentsInChildren<CanvasRenderer>();
            m_SpriteRendererList = GetComponentsInChildren<SpriteRenderer>();
            m_TextMeshProList = GetComponentsInChildren<TMPro.TextMeshPro>();
            m_subMeshUIList = GetComponentsInChildren<TMPro.TMP_SubMeshUI>();
            if (m_subMeshUIList != null && m_subMeshUIList.Any())
            {
                var canvasRenderers = m_subMeshUIList.Select(subMeshUI => subMeshUI.canvasRenderer).ToArray();
                m_canvasRenderers = m_canvasRenderers.Concat(canvasRenderers).Distinct(c => c.gameObject).ToArray();
            }
        }

        private void OnDestroy()
        {
            m_canvasRenderers = null;
            m_SpriteRendererList = null;
            m_TextMeshProList = null;
            m_subMeshUIList = null;
        }

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            SetGrayout(IsGrayout);
        }

        //--------------------------------------------
        // public
        //--------------------------------------------
        /// <summary>
        /// グレーアウト設定
        /// </summary>
        public void SetGrayout(bool isGrayout)
        {
            Initialize();

            IsGrayout = isGrayout;

            var color = isGrayout ? Color.gray : Color.white;

            if (m_canvasRenderers.Any())
            {
                foreach (var r in m_canvasRenderers)
                {
                    color.a = r.GetAlpha();
                    r.SetColor(color);
                }
            }

            if (m_SpriteRendererList.Any())
            {
                foreach (var r in m_SpriteRendererList)
                {
                    color.a = r.color.a;
                    r.color = color;
                }
            }

            if (m_TextMeshProList.Any())
            {
                foreach (var r in m_TextMeshProList)
                {
                    color.a = r.color.a;
                    r.color = color;
                }
            }
        }

        //--------------------------------------------
        // private
        //--------------------------------------------
        /// <summary>
        /// 初期化
        /// </summary>
        private void Initialize()
        {
            if (m_IsInitialized)
            {
                return;
            }

            if (m_canvasRenderers.IsNullOrEmpty())
            {
                m_canvasRenderers = GetComponentsInChildren<CanvasRenderer>(true);
            }

            if (m_SpriteRendererList.IsNullOrEmpty())
            {
                m_SpriteRendererList = GetComponentsInChildren<SpriteRenderer>(true);
            }

            if (m_TextMeshProList.IsNullOrEmpty())
            {
                m_TextMeshProList = GetComponentsInChildren<TMPro.TextMeshPro>(true);
            }

            if (m_subMeshUIList.IsNullOrEmpty())
            {
                m_subMeshUIList = GetComponentsInChildren<TMPro.TMP_SubMeshUI>(true);
                if (m_subMeshUIList != null && m_subMeshUIList.Any())
                {
                    var canvasRenderers = m_subMeshUIList.Select(subMeshUI => subMeshUI.canvasRenderer).ToArray();
                    m_canvasRenderers = m_canvasRenderers.Concat(canvasRenderers).Distinct(c => c.gameObject).ToArray();
                }
            }
        }
    }
}

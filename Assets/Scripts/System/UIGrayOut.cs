using System.Linq;
using Carbon;
using UnityEngine;

public sealed class UIGrayOut : MonoBehaviour
{
    //============================================
    // メンバー変数(SerializeField)
    //============================================
    [SerializeField] private CanvasRenderer[] canvasRenderers = new CanvasRenderer[0];

    [SerializeField] private SpriteRenderer[] spriteRendererList = new SpriteRenderer[0];

    [SerializeField] private TMPro.TextMeshPro[] textMeshProList = new TMPro.TextMeshPro[0];

    //============================================
    //! メンバー変数
    //============================================
    private bool isInitialized = false;

    private TMPro.TMP_SubMeshUI[] subMeshUIList = new TMPro.TMP_SubMeshUI[0];
    //============================================
    //! プロパティ
    //============================================
    public bool IsGrayout { private set; get; }

    //--------------------------------------------
    // MonoBehaviour
    //--------------------------------------------
    private void Reset()
    {
        canvasRenderers = GetComponentsInChildren<CanvasRenderer>();
        spriteRendererList = GetComponentsInChildren<SpriteRenderer>();
        textMeshProList = GetComponentsInChildren<TMPro.TextMeshPro>();
        subMeshUIList = GetComponentsInChildren<TMPro.TMP_SubMeshUI>();
        if (subMeshUIList != null && subMeshUIList.Any())
        {
            var canvasRenderers = subMeshUIList.Select(subMeshUI => subMeshUI.canvasRenderer).ToArray();
            canvasRenderers = canvasRenderers.Concat(canvasRenderers).Distinct(c => c.gameObject).ToArray();
        }
    }

    private void OnDestroy()
    {
        canvasRenderers = null;
        spriteRendererList = null;
        textMeshProList = null;
        subMeshUIList = null;
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

        if (canvasRenderers.Any())
        {
            foreach (var r in canvasRenderers)
            {
                color.a = r.GetAlpha();
                r.SetColor(color);
            }
        }

        if (spriteRendererList.Any())
        {
            foreach (var r in spriteRendererList)
            {
                color.a = r.color.a;
                r.color = color;
            }
        }

        if (textMeshProList.Any())
        {
            foreach (var r in textMeshProList)
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
        if (isInitialized)
        {
            return;
        }

        if (canvasRenderers.IsNullOrEmpty())
        {
            canvasRenderers = GetComponentsInChildren<CanvasRenderer>(true);
        }

        if (spriteRendererList.IsNullOrEmpty())
        {
            spriteRendererList = GetComponentsInChildren<SpriteRenderer>(true);
        }

        if (textMeshProList.IsNullOrEmpty())
        {
           textMeshProList = GetComponentsInChildren<TMPro.TextMeshPro>(true);
        }

        if (subMeshUIList.IsNullOrEmpty())
        {
            subMeshUIList = GetComponentsInChildren<TMPro.TMP_SubMeshUI>(true);
            if (subMeshUIList != null && subMeshUIList.Any())
            {
                var canvasRenderers = subMeshUIList.Select(subMeshUI => subMeshUI.canvasRenderer).ToArray();
                canvasRenderers = canvasRenderers.Concat(canvasRenderers).Distinct(c => c.gameObject).ToArray();
            }
        }
    }
}

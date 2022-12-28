using UnityEngine;

namespace Carbon
{
	[RequireComponent(typeof(RectTransform))]
	public class RectTransformBehaviour : CarbonBehaviour
	{
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Component.transform Cache
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Cache of RectTransform
		/// </summary>
		private RectTransform m_RectTransformCache = null;
		/// <summary>
		/// Cache of RectTransform
		/// </summary>
		public RectTransform rectTransform {
			get {
				return m_RectTransformCache
					? m_RectTransformCache
					: m_RectTransformCache = GetComponent<RectTransform>();
			}
		}
		/// <summary>
		/// rectTransform.anchoredPosition
		/// </summary>
		public Vector2 anchoredPosition {
			get { return rectTransform.anchoredPosition; }
			set { rectTransform.anchoredPosition = value; }
		}
		/// <summary>
		/// rectTransform.sizeDelta
		/// </summary>
		public Vector2 sizeDelta {
			get { return rectTransform.sizeDelta; }
			set { rectTransform.sizeDelta = value; }
		}
		/// <summary>
		/// rectTransform.pivot
		/// </summary>
		public Vector2 pivot {
			get { return rectTransform.pivot; }
			set { rectTransform.pivot = value; }
		}
		/// <summary>
		/// Saved rectTransform.anchoredPosition
		/// </summary>
		public Vector2 savedAnchoredPosition { get { SaveValue(); return m_SavedAnchoredPosition; } }
		/// <summary>
		/// Saved rectTransform.sizeDelta
		/// </summary>
		public Vector2 savedSizeDelta { get { SaveValue(); return m_SavedSizeDelta; } }
		/// <summary>
		/// Saved rectTransform.pivot
		/// </summary>
		public Vector2 savedPivot { get { SaveValue(); return m_SavedPivot; } }
		/// <summary>
		/// Saved rectTransform.scale
		/// </summary>
		public Vector3 savedScale { get { SaveValue(); return m_SavedScale; } }

		/// <summary>
		/// Flag indicates to whether 'SaveValue()' has processed.
		/// </summary>
		private bool m_SaveValueFlag = false;
		/// <summary>
		/// Saved rectTransform.anchoredPosition
		/// </summary>
		private Vector2 m_SavedAnchoredPosition = Vector2.zero;
		/// <summary>
		/// Saved rectTransform.sizeDelta
		/// </summary>
		private Vector2 m_SavedSizeDelta = Vector2.zero;
		/// <summary>
		/// Saved rectTransform.pivot
		/// </summary>
		private Vector2 m_SavedPivot = Vector2.zero;
		/// <summary>
		/// Saved rectTransform.localScale
		/// </summary>
		private Vector3 m_SavedScale = Vector3.zero;


		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// RectTransform Extensions
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 現在値をセーブする.
		/// </summary>
		public void SaveValue(bool forceOverwrite = false)
		{
			// force cache
			m_SaveValueFlag &= (!forceOverwrite);

			// process only once
			if (m_SaveValueFlag) {
				return;
			}
			m_SaveValueFlag = true;

			// cache
			m_SavedAnchoredPosition = anchoredPosition;
			m_SavedSizeDelta = sizeDelta;
			m_SavedPivot = pivot;
			m_SavedScale = localScale;
		}

		#region RectTransform

		#region RectTransform.anchoredPosition
		public void ResetAnchoredPosition()
		{
			rectTransform.anchoredPosition = Vector2.zero;
		}

		public float GetAnchoredPositionX()
		{
			return rectTransform.anchoredPosition.x;
		}

		public float GetAnchoredPositionY()
		{
			return rectTransform.anchoredPosition.y;
		}

		public void SetAnchoredPositionX(float x)
		{
			rectTransform.anchoredPosition = new Vector2(x, rectTransform.anchoredPosition.y);
		}

		public void SetAnchoredPositionY(float y)
		{
			rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, y);
		}

		public void SetAnchoredPosition(float x, float y)
		{
			rectTransform.anchoredPosition = new Vector2(x, y);
		}

		public void SetAnchoredPosition(Vector2 position)
		{
			rectTransform.anchoredPosition = position;
		}

		public void AddAnchoredPositionX(float x)
		{
			rectTransform.anchoredPosition += new Vector2(x, 0);
		}

		public void AddAnchoredPositionY(float y)
		{
			rectTransform.anchoredPosition += new Vector2(0, y);
		}

		public void AddAnchoredPosition(float x, float y)
		{
			rectTransform.anchoredPosition += new Vector2(x, y);
		}

		public void AddAnchoredPosition(Vector2 v)
		{
			rectTransform.anchoredPosition += v;
		}
		#endregion

		#region RectTransform.sizeDelta
		public Vector2 GetSizeDelta()
		{
			return rectTransform.sizeDelta;
		}

		public float GetSizeDeltaX()
		{
			return rectTransform.sizeDelta.x;
		}

		public float GetSizeDeltaY()
		{
			return rectTransform.sizeDelta.y;
		}

		public void SetSizeDeltaX(float x)
		{
			rectTransform.sizeDelta = new Vector2(x, rectTransform.sizeDelta.y);
		}

		public void SetSizeDeltaY(float y)
		{
			rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, y);
		}

		public void SetSizeDelta(float x, float y)
		{
			rectTransform.sizeDelta = new Vector2(x, y);
		}

		public void SetSizeDelta(Vector2 sizeDelta)
		{
			rectTransform.sizeDelta = sizeDelta;
		}
		#endregion

		#region RectTransform.pivot
		public Vector2 GetPivot()
		{
			return rectTransform.pivot;
		}

		public float GetPivotX()
		{
			return rectTransform.pivot.x;
		}

		public float GetPivotY()
		{
			return rectTransform.pivot.y;
		}

		public void SetPivotX(float x)
		{
			rectTransform.pivot = new Vector2(x, rectTransform.pivot.y);
		}

		public void SetPivotY(float y)
		{
			rectTransform.pivot = new Vector2(rectTransform.pivot.x, y);
		}

		public void SetPivot(float x, float y)
		{
			rectTransform.pivot = new Vector2(x, y);
		}

		public void SetPivot(Vector2 pivot)
		{
			rectTransform.pivot = pivot;
		}
		#endregion

		#region Anchor
		public void AdaptAnchor(RectTransform rt)
		{
			rectTransform.anchorMin = rt.anchorMin;
			rectTransform.anchorMax = rt.anchorMax;
		}

		public void SetAnchorMin(Vector2 anchorMin)
		{
			rectTransform.anchorMin = anchorMin;
		}

		public void SetAnchorMax(Vector2 anchorMax)
		{
			rectTransform.anchorMax = anchorMax;
		}

		public void SetAnchor(Vector2 anchorMin, Vector2 anchorMax)
		{
			rectTransform.anchorMin = anchorMin;
			rectTransform.anchorMax = anchorMax;
		}

		public void SetAnchorCenter()
		{
			rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
			rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
		}
		#endregion

		#endregion
	}
}
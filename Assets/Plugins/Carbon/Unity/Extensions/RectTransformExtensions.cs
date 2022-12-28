using System;
using System.Collections.Generic;
using UnityEngine;

namespace Carbon
{
	/// <summary>
	/// RectTransform Extensions
	/// </summary>
	public static class RectTransformExtensions
	{
		#region AnchoredPosition
		public static void ResetAnchoredPosition(this RectTransform self)
		{
			self.anchoredPosition = Vector2.zero;
		}

		public static void SetAnchoredPositionX(this RectTransform self, float x)
		{
			self.anchoredPosition = new Vector2(x, self.anchoredPosition.y);
		}

		public static void SetAnchoredPositionY(this RectTransform self, float y)
		{
			self.anchoredPosition = new Vector2(self.anchoredPosition.x, y);
		}

		public static void SetAnchoredPosition(this RectTransform self, float x, float y)
		{
			self.anchoredPosition = new Vector2(x, y);
		}

		public static void AddAnchoredPositionX(this RectTransform self, float x)
		{
			self.anchoredPosition += new Vector2(x, 0);
		}

		public static void AddAnchoredPositionY(this RectTransform self, float y)
		{
			self.anchoredPosition += new Vector2(0, y);
		}

		public static void AddAnchoredPosition(this RectTransform self, float x, float y)
		{
			self.anchoredPosition += new Vector2(x, y);
		}
		#endregion

		#region SizeDelta
		public static void SetSizeDeltaX(this RectTransform self, float x)
		{
			self.sizeDelta = new Vector2(x, self.sizeDelta.y);
		}

		public static void SetSizeDeltaY(this RectTransform self, float y)
		{
			self.sizeDelta = new Vector2(self.sizeDelta.x, y);
		}

		public static void SetSizeDelta(this RectTransform self, float x, float y)
		{
			self.sizeDelta = new Vector2(x, y);
		}
		#endregion

		#region Rect
		public static Rect GetRect(this RectTransform self)
		{
			return self.rect;
		}

		public static float GetRectWidth(this RectTransform self)
		{
			return self.rect.width;
		}

		public static float GetRectHeight(this RectTransform self)
		{
			return self.rect.height;
		}
		#endregion

		#region Pivot
		/// <summary>
		/// Pivotを中央に設定
		/// </summary>
		public static void SetPivotCenter(this RectTransform self)
		{
			self.pivot = new Vector2 (0.5f, 0.5f);
		}

		/// <summary>
		/// Pivotを上部に設定
		/// </summary>
		public static void SetPivotUpper(this RectTransform self)
		{
			self.pivot = new Vector2 (0.5f, 1.0f);
		}
		#endregion

		#region Anchor
		/// <summary>
		/// Anchorを中央に設定
		/// </summary>
		public static void SetAnchorCenter(this RectTransform self)
		{
			self.anchorMin = new Vector2(0.5f, 0.5f);
			self.anchorMax = new Vector2(0.5f, 0.5f);
		}

		/// <summary>
		/// Anchorを上部に設定
		/// </summary>
		public static void SetAnchorUpper(this RectTransform self)
		{
			self.anchorMin = new Vector2(0.5f, 1.0f);
			self.anchorMax = new Vector2(0.5f, 1.0f);
		}

		/// <summary>
		/// Anchor を XY Stretch に設定
		/// </summary>
		public static void SetAnchorStretch(this RectTransform self)
		{
			self.anchorMin = Vector2.zero;
			self.anchorMax = Vector2.one;
		}

		/// <summary>
		/// Anchorの設定
		/// </summary>
		public static void SetAnchor(this RectTransform self, Vector2 min, Vector2 max)
		{
			self.anchorMin = min;
			self.anchorMax = max;
		}
		#endregion

		#region Offset
		/// <summary>
		/// Offset を リセット (Anchor に合わせる)
		/// </summary>
		public static void ResetOffset(this RectTransform self)
		{
			self.offsetMin= Vector2.zero;
			self.offsetMax = Vector2.zero;
		}
		#endregion
	}
}

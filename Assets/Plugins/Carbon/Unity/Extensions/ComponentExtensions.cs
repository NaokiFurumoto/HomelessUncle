/*
 *
 * transform の操作は非推奨. 重いだから.
 * CarbonBehaviour と合わせて使ってください.
 *
 */

/*
 * ワールド座標系の操作は非推奨. ゲーム全体調整する時痛い目に遭う.
 */
//#define ENABLE_WORLD_COORDINATE_EXTENSIONS

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Carbon
{
	/// <summary>
	/// Component Extensions
	/// </summary>
	public static class ComponentExtensions
	{
		/*
		 *
		 *
		 * 無印 : null チェックなし
		 *
		 * Try : null チェックあり
		 *
		 *
		 */

		#region GameObject.Destroy
		/// <summary>
		/// 所属 GameObject を破棄します.
		/// </summary>
		public static void DestroyGameObject(this Component self)
		{
			GameObject.Destroy(self.gameObject);
		}
		/// <summary>
		/// 所属 GameObject 破棄を試します.
		/// </summary>
		public static void TryDestroyGameObject(this Component self)
		{
			if (self) {
				GameObject.Destroy(self.gameObject);
			}
		}

		/// <summary>
		/// 所属 GameObject を直ちに破棄します.
		/// </summary>
		public static void DestroyGameObjectImmediate(this Component self)
		{
			GameObject.DestroyImmediate(self.gameObject);
		}
		/// <summary>
		/// 所属 GameObject 破棄を直ちに試します.
		/// </summary>
		public static void TryDestroyGameObjectImmediate(this Component self)
		{
			if (self) {
				GameObject.DestroyImmediate(self.gameObject);
			}
		}
		#endregion

		#region GameObject.Active
		/// <summary>
		/// アクティブを設定します.
		/// </summary>
		/// <param name="value">アクティブかどうか.</param>
		public static void SetActive(this Component self, bool isActive)
		{
			self.gameObject.SetActive(isActive);
		}
		/// <summary>
		/// アクティブ設定を試します.
		/// </summary>
		/// <param name="value">アクティブかどうか.</param>
		public static void TrySetActive(this Component self, bool isActive)
		{
			if (self) {
				self.gameObject.SetActive(isActive);
			}
		}

		/// <summary>
		/// アクティブ状態を切り替える. アクティブ状態の変更したかどうかを返す.
		/// </summary>
		/// <param name="isActive">アクティブ状態.</param>
		public static bool ChangeActive(this Component self, bool isActive)
		{
			return self.gameObject.ChangeActive(isActive);
		}
		/// <summary>
		/// アクティブ状態を切り替える. Unity 的に破棄済み, またはアクティブ状態の変更がない場合は false を返す; それ以外は true を返す.
		/// </summary>
		/// <param name="isActive">アクティブ状態.</param>
		public static bool TryChangeActive(this Component self, bool isActive)
		{
			if (self) {
				return self.gameObject.ChangeActive(isActive);
			}
			return false;
		}

		/// <summary>
		/// 自身アクティブ状態を取得します.
		/// </summary>
		/// <returns>Component 存在, 且つ Active 状態なら true.</returns>
		public static bool GetActiveSelf(this Component self)
		{
			return self.gameObject.activeSelf;
		}
		/// <summary>
		/// 自身アクティブ状態取得を試します. Unity 的に破棄済みの場合は false を返す.
		/// </summary>
		/// <returns>Component 存在, 且つ Active 状態なら true.</returns>
		public static bool TryGetActiveSelf(this Component self)
		{
			return self ? self.gameObject.activeSelf : false;
		}

		/// <summary>
		/// 継承アクティブ状態を取得します.
		/// </summary>
		/// <returns>Component 存在, 且つ Active 状態なら true.</returns>
		public static bool GetActiveInHierarchy(this Component self)
		{
			return self.gameObject.activeInHierarchy;
		}
		/// <summary>
		/// 継承アクティブ状態取得を試します. Unity 的に破棄済みの場合は false を返す.
		/// </summary>
		/// <returns>Component 存在, 且つ Active 状態なら true.</returns>
		public static bool TryGetActiveInHierarchy(this Component self)
		{
			return self ? self.gameObject.activeInHierarchy : false;
		}
		#endregion

		#region GameObject.Layer
		/// <summary>
		/// 所属 GameObject のレイヤーを取得します.
		/// </summary>
		/// <returns>レイヤー.</returns>
		public static int GetLayer(this Component self)
		{
			return self.gameObject.layer;
		}
		/// <summary>
		/// 所属 GameObject のレイヤーを取得します. Unity 的に破棄済みの場合は defaultValue を返します.
		/// </summary>
		/// <returns>レイヤー.</returns>
		public static int TryGetLayer(this Component self, int defaultValue = default(int))
		{
			return self ? self.gameObject.layer : defaultValue;
		}

		/// <summary>
		/// レイヤーを設定します.
		/// </summary>
		/// <param name="layer">レイヤー.</param>
		public static void SetLayer(this Component self, int layer)
		{
			self.gameObject.layer = layer;
		}
		/// <summary>
		/// レイヤー設定を試します.
		/// </summary>
		/// <param name="layer">レイヤー.</param>
		public static void TrySetLayer(this Component self, int layer)
		{
			if (self) {
				self.gameObject.layer = layer;
			}
		}

		/// <summary>
		/// レイヤーを設定します.
		/// </summary>
		/// <param name="layerName">レイヤー名.</param>
		public static void SetLayer(this Component self, string layerName)
		{
			self.gameObject.layer = LayerMask.NameToLayer(layerName);
		}
		/// <summary>
		/// レイヤー設定を試します.
		/// </summary>
		/// <param name="layerName">レイヤー名.</param>
		public static void TrySetLayer(this Component self, string layerName)
		{
			if (self) {
				self.gameObject.layer = LayerMask.NameToLayer(layerName);
			}
		}

		/// <summary>
		/// レイヤーを指定 GameObject のレイヤーに設定します.
		/// </summary>
		/// <param name="targetGo">目標 GameObject.</param>
		public static void SetLayer(this Component self, GameObject targetGo)
		{
			self.gameObject.layer = targetGo.layer;
		}
		/// <summary>
		/// レイヤーを指定 GameObject のレイヤーに設定します.
		/// </summary>
		/// <param name="targetGo">目標 GameObject.</param>
		public static void TrySetLayer(this Component self, GameObject targetGo)
		{
			if (self && targetGo) {
				self.gameObject.layer = targetGo.layer;
			}
		}

		/// <summary>
		/// レイヤーを指定 Component のレイヤーに設定します.
		/// </summary>
		/// <param name="target">目標 Component.</param>
		public static void SetLayer(this Component self, Component target)
		{
			self.gameObject.layer = target.gameObject.layer;
		}
		/// <summary>
		/// レイヤーを指定 Component のレイヤーに設定します.
		/// </summary>
		/// <param name="target">目標 Component.</param>
		public static void TrySetLayer(this Component self, Component target)
		{
			if (self && target) {
				self.gameObject.layer = target.gameObject.layer;
			}
		}

		/// <summary>
		/// 再帰的にレイヤーを設定します.
		/// </summary>
		/// <param name="layer">レイヤー.</param>
		public static void SetLayerRecursively(this Component self, int layer)
		{
			/*
				 * 再帰関数コール禁止 !! スタックオーバーフローしてしまいますので.
				 */
			foreach (Transform tf in self.GetComponentsInChildren<Transform>()) {
				tf.gameObject.layer = layer;
			}
		}
		/// <summary>
		/// 再帰的にレイヤー設定を試します.
		/// </summary>
		/// <param name="layer">レイヤー.</param>
		public static void TrySetLayerRecursively(this Component self, int layer)
		{
			if (self) {
				/*
				 * 再帰関数コール禁止 !! スタックオーバーフローしてしまいますので.
				 */
				foreach (Transform tf in self.GetComponentsInChildren<Transform>()) {
					tf.gameObject.layer = layer;
				}
			}
		}

		/// <summary>
		/// 再帰的にレイヤーを設定します.
		/// </summary>
		/// <param name="layerName">レイヤー名.</param>
		public static void SetLayerRecursively(this Component self, string layerName)
		{
			int layer = LayerMask.NameToLayer(layerName);
			/*
				* 再帰関数コール禁止 !! スタックオーバーフローしてしまいますので.
				*/
			foreach (Transform tf in self.GetComponentsInChildren<Transform>()) {
				tf.gameObject.layer = layer;
			}
		}
		/// <summary>
		/// 再帰的にレイヤー設定を試します.
		/// </summary>
		/// <param name="layerName">レイヤー名.</param>
		public static void TrySetLayerRecursively(this Component self, string layerName)
		{
			if (self) {
				int layer = LayerMask.NameToLayer(layerName);
				/*
				 * 再帰関数コール禁止 !! スタックオーバーフローしてしまいますので.
				 */
				foreach (Transform tf in self.GetComponentsInChildren<Transform>()) {
					tf.gameObject.layer = layer;
				}
			}
		}

		/// <summary>
		/// 再帰的にレイヤーを指定 GameObject のレイヤーに設定します.
		/// </summary>
		/// <param name="targetGo">目標 GameObject.</param>
		public static void SetLayerRecursively(this Component self, GameObject targetGo)
		{
			int layer = targetGo.layer;
			/*
				* 再帰関数コール禁止 !! スタックオーバーフローしてしまいますので.
				*/
			foreach (Transform tf in self.GetComponentsInChildren<Transform>()) {
				tf.gameObject.layer = layer;
			}
		}
		/// <summary>
		/// 再帰的にレイヤーを指定 GameObject のレイヤーに設定します.
		/// </summary>
		/// <param name="targetGo">目標 GameObject.</param>
		public static void TrySetLayerRecursively(this Component self, GameObject targetGo)
		{
			if (self && targetGo) {
				int layer = targetGo.layer;
				/*
				 * 再帰関数コール禁止 !! スタックオーバーフローしてしまいますので.
				 */
				foreach (Transform tf in self.GetComponentsInChildren<Transform>()) {
					tf.gameObject.layer = layer;
				}
			}
		}

		/// <summary>
		/// 再帰的にレイヤーを指定 Component のレイヤーに設定します.
		/// </summary>
		/// <param name="target">目標 Component.</param>
		public static void SetLayerRecursively(this Component self, Component target)
		{
			int layer = target.gameObject.layer;
			/*
				* 再帰関数コール禁止 !! スタックオーバーフローしてしまいますので.
				*/
			foreach (Transform tf in self.GetComponentsInChildren<Transform>()) {
				tf.gameObject.layer = layer;
			}
		}
		/// <summary>
		/// 再帰的にレイヤーを指定 Component のレイヤーに設定します.
		/// </summary>
		/// <param name="target">目標 Component.</param>
		public static void TrySetLayerRecursively(this Component self, Component target)
		{
			if (self && target) {
				int layer = target.gameObject.layer;
				/*
				 * 再帰関数コール禁止 !! スタックオーバーフローしてしまいますので.
				 */
				foreach (Transform tf in self.GetComponentsInChildren<Transform>()) {
					tf.gameObject.layer = layer;
				}
			}
		}
		#endregion

		#region Find
		/// <summary>
		/// 名前で子孫から Transform を探す.
		/// </summary>
		/// <param name="self">Component 自身.</param>
		/// <param name="name">名前.</param>
		/// <returns>Transform.</returns>
		public static Transform FindTransform(this Component self, string name)
		{
			foreach (Transform tf in self.GetChildren(true)) {
				if (tf.name == name) {
					return tf;
				}
			}
			return null;
		}
		/// <summary>
		/// 名前で子孫から Transform を探す. Unity 的に破棄済みの場合は null を返します.
		/// </summary>
		/// <param name="self">Component 自身.</param>
		/// <param name="name">名前.</param>
		/// <returns>Transform.</returns>
		public static Transform TryFindTransform(this Component self, string name)
		{
			return self ? self.FindTransform(name) : null;
		}

		/// <summary>
		/// FindTransform の GameObject 版.
		/// </summary>
		/// <param name="self">Component 自身.</param>
		/// <param name="name">名前.</param>
		/// <returns>GameObject.</returns>
		public static GameObject FindGameObject(this Component self, string name)
		{
			Transform tf = self.FindTransform(name);
			return tf ? tf.gameObject : null;
		}
		/// <summary>
		/// FindTransform の GameObject 版を試します. Unity 的に破棄済みの場合は null を返します.
		/// </summary>
		/// <param name="self">Component 自身.</param>
		/// <param name="name">名前.</param>
		/// <returns>GameObject.</returns>
		public static GameObject TryFindGameObject(this Component self, string name)
		{
			return self ? self.FindGameObject(name) : null;
		}

		/// <summary>
		/// FindTransform の Component 版.
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <param name="self">Component 自身.</param>
		/// <param name="name">名前.</param>
		/// <returns>Component.</returns>
		public static T FindComponent<T>(this Component self, string name) where T : Component
		{
			Transform tf = self.FindTransform(name);
			return tf ? tf.GetComponent<T>() : null;
		}
		/// <summary>
		/// FindTransform の Component 版を試します. Unity 的に破棄済みの場合は null を返します.
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <param name="self">Component 自身.</param>
		/// <param name="name">名前.</param>
		/// <returns>Component.</returns>
		public static T TryFindComponent<T>(this Component self, string name) where T : Component
		{
			return self ? self.FindComponent<T>(name) : null;
		}
		#endregion

		#region Component

		#region GetComponent
		/// <summary>
		/// Component の取得を試します. Unity 的に破棄済みの場合は null を返す.
		/// </summary>
		/// <param name="type">Component の型.</param>
		/// <returns>取得した Component.</returns>
		public static Component TryGetComponent(this Component self, Type type)
		{
			return self ? self.GetComponent(type) : null;
		}

		/// <summary>
		/// Component の取得を試します. Unity 的に破棄済みの場合は null を返す.
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <returns>取得した Component.</returns>
		public static T TryGetComponent<T>(this Component self) where T : Component
		{
			return self ? self.GetComponent<T>() : null;
		}

		/// <summary>
		/// Component を追加します.
		/// </summary>
		/// <param name="type">Component の型.</param>
		/// <returns>追加した Component.</returns>
		public static Component AddComponent(this Component self, Type type)
		{
			return self.gameObject.AddComponent(type);
		}
		/// <summary>
		/// Component の追加を試します. Unity 的に破棄済みの場合は null を返す.
		/// </summary>
		/// <param name="type">Component の型.</param>
		/// <returns>追加した Component.</returns>
		public static Component TryAddComponent(this Component self, Type type)
		{
			return self ? self.gameObject.AddComponent(type) : null;
		}

		/// <summary>
		/// Component を追加します.
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <returns>追加した Component.</returns>
		public static T AddComponent<T>(this Component self) where T : Component
		{
			return self.gameObject.AddComponent<T>();
		}
		/// <summary>
		/// Component の追加を試します. Unity 的に破棄済みの場合は null を返す.
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <returns>追加した Component.</returns>
		public static T TryAddComponent<T>(this Component self) where T : Component
		{
			return self ? self.gameObject.AddComponent<T>() : null;
		}

		/// <summary>
		/// Component のを取得します. アタッチされていない場合は追加してから取得します.
		/// </summary>
		/// <param name="type">Component の型.</param>
		/// <returns>取得した Component.</returns>
		public static Component DemandComponent(this Component self, Type type)
		{
			return self.gameObject.DemandComponent(type);
		}
		/// <summary>
		/// Component の取得を試します. アタッチされていない場合は追加してから取得します. Unity 的に破棄済みの場合は null を返す.
		/// </summary>
		/// <param name="type">Component の型.</param>
		/// <returns>取得した Component.</returns>
		public static Component TryDemandComponent(this Component self, Type type)
		{
			return self ? self.gameObject.DemandComponent(type) : null;
		}

		/// <summary>
		/// Component のを取得します. アタッチされていない場合は追加してから取得します.
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <returns>取得した Component.</returns>
		public static T DemandComponent<T>(this Component self) where T : Component
		{
			return self.gameObject.DemandComponent<T>();
		}
		/// <summary>
		/// Component の取得を試します. アタッチされていない場合は追加してから取得します. Unity 的に破棄済みの場合は null を返す.
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <returns>取得した Component.</returns>
		public static T TryDemandComponent<T>(this Component self) where T : Component
		{
			return self ? self.gameObject.DemandComponent<T>() : null;
		}
		#endregion

		#region GetRectTransform
		/// <summary>
		/// RectTransform を取得します.
		/// </summary>
		/// <returns>取得した RectTransform.</returns>
		public static RectTransform GetRectTransform(this Component self)
		{
			return self.GetComponent<RectTransform>();
		}
		/// <summary>
		/// RectTransform の取得を試します. Unity 的に破棄済みの場合は null を返す.
		/// </summary>
		/// <returns>取得した RectTransform.</returns>
		public static RectTransform TryGetRectTransform(this Component self)
		{
			return self ? self.GetComponent<RectTransform>() : null;
		}

		/// <summary>
		/// RectTransform を取得します. アタッチされていない場合は追加してから取得します.
		/// </summary>
		/// <returns>取得した RectTransform.</returns>
		public static RectTransform DemandRectTransform(this Component self)
		{
			return self.gameObject.DemandRectTransform();
		}
		/// <summary>
		/// RectTransform の取得を試します. アタッチされていない場合は追加してから取得します. Unity 的に破棄済みの場合は null を返す.
		/// </summary>
		/// <returns>取得した RectTransform.</returns>
		public static RectTransform TryDemandRectTransform(this Component self)
		{
			return self ? self.gameObject.DemandRectTransform() : null;
		}
		#endregion

		#region GetComponentsInChildren
		/// <summary>
		/// GetComponentsInChildren() を試す. Unity 的に破棄済みの場合は空配列を返す.
		/// </summary>
		/// <param name="type">Component の型.</param>
		/// <param name="includeInactive">非アクティブな Child も取得するかどうか.</param>
		/// <returns>取得した Component 配列.</returns>
		public static Component[] TryGetComponentsInChildren(this Component self, Type type, bool includeInactive = false)
		{
			return self ? self.GetComponentsInChildren(type, includeInactive) : new Component[0];
		}

		/// <summary>
		/// GetComponentsInChildren() を試す. Unity 的に破棄済みの場合は空配列を返す.
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <param name="includeInactive">非アクティブな Child も取得するかどうか.</param>
		/// <returns>取得した Component 配列.</returns>
		public static T[] TryGetComponentsInChildren<T>(this Component self, bool includeInactive = false) where T : Component
		{
			return self ? self.GetComponentsInChildren<T>(includeInactive) : new T[0];
		}

		/// <summary>
		/// GetComponentsInChildren() を試す.
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <param name="includeInactive">非アクティブな Child も取得するかどうか.</param>
		public static void TryGetComponentsInChildren<T>(this Component self, List<T> resultList, bool includeInactive = false) where T : Component
		{
			if (self) {
				self.GetComponentsInChildren(includeInactive, resultList);
			}
		}

		/// <summary>
		/// 自身を含まない GetComponentsInChildren().
		/// </summary>
		/// <param name="type">Component の型.</param>
		/// <param name="includeInactive">非アクティブな Child も取得するかどうか.</param>
		/// <returns>取得した Component 配列.</returns>
		public static Component[] GetComponentsInChildrenWithoutSelf(this Component self, Type type, bool includeInactive = false)
		{
			List<Component> list = new List<Component>(self.GetComponentsInChildren(type, includeInactive));
			list.RemoveAll(n => n == self);
			return list.ToArray();
		}
		/// <summary>
		/// 自身を含まない GetComponentsInChildren() を試す. Unity 的に破棄済みの場合は空配列を返す.
		/// </summary>
		/// <param name="type">Component の型.</param>
		/// <param name="includeInactive">非アクティブな Child も取得するかどうか.</param>
		/// <returns>取得した Component 配列.</returns>
		public static Component[] TryGetComponentsInChildrenWithoutSelf(this Component self, Type type, bool includeInactive = false)
		{
			if (self) {
				List<Component> list = new List<Component>(self.GetComponentsInChildren(type, includeInactive));
				list.RemoveAll(n => n == self);
				return list.ToArray();
			}
			return new Component[0];
		}

		/// <summary>
		/// 自身を含まない GetComponentsInChildren().
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <param name="includeInactive">非アクティブな Child も取得するかどうか.</param>
		/// <returns>取得した Component 配列.</returns>
		public static T[] GetComponentsInChildrenWithoutSelf<T>(this Component self, bool includeInactive = false) where T : Component
		{
			List<T> list = new List<T>();
			self.GetComponentsInChildren(includeInactive, list);
			list.RemoveAll(n => n == self);
			return list.ToArray();
		}
		/// <summary>
		/// 自身を含まない GetComponentsInChildren() を試す. Unity 的に破棄済みの場合は空配列を返す.
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <param name="includeInactive">非アクティブな Child も取得するかどうか.</param>
		/// <returns>取得した Component 配列.</returns>
		public static T[] TryGetComponentsInChildrenWithoutSelf<T>(this Component self, bool includeInactive = false) where T : Component
		{
			if (self) {
				List<T> list = new List<T>();
				self.GetComponentsInChildren(includeInactive, list);
				list.RemoveAll(n => n == self);
				return list.ToArray();
			}
			return new T[0];
		}

		/// <summary>
		/// 自身を含まない GetComponentsInChildren().
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <param name="includeInactive">非アクティブな Child も取得するかどうか.</param>
		public static void GetComponentsInChildrenWithoutSelf<T>(this Component self, List<T> resultList, bool includeInactive = false) where T : Component
		{
			self.GetComponentsInChildren(includeInactive, resultList);
			resultList.RemoveAll(n => n == self);
		}
		/// <summary>
		/// 自身を含まない GetComponentsInChildren() を試す.
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <param name="includeInactive">非アクティブな Child も取得するかどうか.</param>
		public static void TryGetComponentsInChildrenWithoutSelf<T>(this Component self, List<T> resultList, bool includeInactive = false) where T : Component
		{
			if (self) {
				self.GetComponentsInChildren(includeInactive, resultList);
				resultList.RemoveAll(n => n == self);
			}
		}
		#endregion

		#region GetComponentsInParent
		/// <summary>
		/// GetComponentsInParent() を試す. Unity 的に破棄済みの場合は空配列を返す.
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <param name="includeInactive">非アクティブな Parent も取得するかどうか.</param>
		/// <returns>取得した Component 配列.</returns>
		public static Component[] TryGetComponentsInParent(this Component self, Type type, bool includeInactive = false)
		{
			return self ? self.GetComponentsInParent(type, includeInactive) : new Component[0];
		}

		/// <summary>
		/// GetComponentsInParent() を試す. Unity 的に破棄済みの場合は空配列を返す.
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <param name="includeInactive">非アクティブな Parent も取得するかどうか.</param>
		/// <returns>取得した Component 配列.</returns>
		public static T[] TryGetComponentsInParent<T>(this Component self, bool includeInactive = false) where T : Component
		{
			return self ? self.GetComponentsInParent<T>(includeInactive) : new T[0];
		}

		/// <summary>
		/// GetComponentsInParent() を試す.
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <param name="includeInactive">非アクティブな Parent も取得するかどうか.</param>
		public static void TryGetComponentsInParent<T>(this Component self, List<T> resultList, bool includeInactive = false) where T : Component
		{
			if (self) {
				self.GetComponentsInParent(includeInactive, resultList);
			}
		}

		/// <summary>
		/// 自身を含まない GetComponentsInParent().
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <param name="includeInactive">非アクティブな Parent も取得するかどうか.</param>
		/// <returns>取得した Component 配列.</returns>
		public static Component[] GetComponentsInParentWithoutSelf(this Component self, Type type, bool includeInactive = false)
		{
			List<Component> list = new List<Component>(self.GetComponentsInParent(type, includeInactive));
			list.RemoveAll(n => n == self);
			return list.ToArray();
		}
		/// <summary>
		/// 自身を含まない GetComponentsInParent() を試す. Unity 的に破棄済みの場合は空配列を返す.
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <param name="includeInactive">非アクティブな Parent も取得するかどうか.</param>
		/// <returns>取得した Component 配列.</returns>
		public static Component[] TryGetComponentsInParentWithoutSelf(this Component self, Type type, bool includeInactive = false)
		{
			if (self) {
				List<Component> list = new List<Component>(self.GetComponentsInParent(type, includeInactive));
				list.RemoveAll(n => n == self);
				return list.ToArray();
			}
			return new Component[0];
		}

		/// <summary>
		/// 自身を含まない GetComponentsInParent().
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <param name="includeInactive">非アクティブな Parent も取得するかどうか.</param>
		/// <returns>取得した Component 配列.</returns>
		public static T[] GetComponentsInParentWithoutSelf<T>(this Component self, bool includeInactive = false) where T : Component
		{
			List<T> list = new List<T>();
			self.GetComponentsInParent(includeInactive, list);
			list.RemoveAll(n => n == self);
			return list.ToArray();
		}
		/// <summary>
		/// 自身を含まない GetComponentsInParent() を試す. Unity 的に破棄済みの場合は空配列を返す.
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <param name="includeInactive">非アクティブな Parent も取得するかどうか.</param>
		/// <returns>取得した Component 配列.</returns>
		public static T[] TryGetComponentsInParentWithoutSelf<T>(this Component self, bool includeInactive = false) where T : Component
		{
			if (self) {
				List<T> list = new List<T>();
				self.GetComponentsInParent(includeInactive, list);
				list.RemoveAll(n => n == self);
				return list.ToArray();
			}
			return new T[0];
		}

		/// <summary>
		/// 自身を含まない GetComponentsInParent().
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <param name="includeInactive">非アクティブな Parent も取得するかどうか.</param>
		public static void GetComponentsInParentWithoutSelf<T>(this Component self, List<T> resultList, bool includeInactive = false) where T : Component
		{
			self.GetComponentsInParent(includeInactive, resultList);
			resultList.RemoveAll(n => n == self);
		}
		/// <summary>
		/// 自身を含まない GetComponentsInParent() を試す.
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <param name="includeInactive">非アクティブな Parent も取得するかどうか.</param>
		public static void TryGetComponentsInParentWithoutSelf<T>(this Component self, List<T> resultList, bool includeInactive = false) where T : Component
		{
			if (self) {
				self.GetComponentsInParent(includeInactive, resultList);
				resultList.RemoveAll(n => n == self);
			}
		}
		#endregion

		#region HasComponent
		/// <summary>
		/// 指定されたコンポーネントを持っているかどうかを返します.
		/// </summary>
		/// <param name="type">Component の型.</param>
		/// <returns>Component を持っている場合は true.</returns>
		public static bool HasComponent<T>(this Component self, Type type)
		{
			return self.GetComponent(type);
		}
		/// <summary>
		/// 指定されたコンポーネントを持っているかどうかを返します. Unity 的に破棄済みの場合は false を返します.
		/// </summary>
		/// <param name="type">Component の型.</param>
		/// <returns>Component を持っている場合は true.</returns>
		public static bool TryHasComponent<T>(this Component self, Type type)
		{
			return self ? self.GetComponent(type) : false;
		}

		/// <summary>
		/// 指定されたコンポーネントを持っているかどうかを返します.
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <returns>Component を持っている場合は true.</returns>
		public static bool HasComponent<T>(this Component self) where T : Component
		{
			return self.GetComponent<T>();
		}
		/// <summary>
		/// 指定されたコンポーネントを持っているかどうかを返します. Unity 的に破棄済みの場合は false を返します.
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <returns>Component を持っている場合は true.</returns>
		public static bool TryHasComponent<T>(this Component self) where T : Component
		{
			return self ? self.GetComponent<T>() : false;
		}
		#endregion

		#region GetComponentInterface
		/// <summary>
		/// 指定 Interface を Component から取得します ( 最初に見付かったヤツ ).
		/// </summary>
		/// <typeparam name="T">Interface の型.</typeparam>
		/// <param name="self">Component 自身.</param>
		/// <returns>Interface.</returns>
		public static T GetComponentInterface<T>(this Component self) where T : class
		{
			foreach (Component n in self.GetComponents<Component>()) {
				T component = n as T;
				if (component != null) {
					return component;
				}
			}
			return null;
		}

		/// <summary>
		/// 指定 Interface を Component から取得します ( 全部 ).
		/// </summary>
		/// <typeparam name="T">Interface の型.</typeparam>
		/// <param name="self">Component 自身.</param>
		/// <returns>Interface 配列.</returns>
		public static T[] GetComponentInterfaces<T>(this Component self) where T : class
		{
			Component[] componentList = self.GetComponents<Component>();
			List<T> result = new List<T>(componentList.Length);
			foreach (Component n in componentList) {
				T component = n as T;
				if (component != null) {
					result.Add(component);
				}
			}
			return result.ToArray();
		}

		/// <summary>
		/// 指定 Interface を子孫 ( 自身を含む ) Components から取得します ( 最初に見付かったヤツ ).
		/// </summary>
		/// <typeparam name="T">Interface の型.</typeparam>
		/// <param name="self">Component 自身.</param>
		/// <param name="includeInactive">非アクティブ子孫も走査対象に含めるかどうか.</param>
		/// <returns>Interface.</returns>
		public static T GetComponentInterfaceInChildren<T>(this Component self, bool includeInactive) where T : class
		{
			foreach (Component n in self.GetComponentsInChildren<Component>(includeInactive)) {
				T component = n as T;
				if (component != null) {
					return component;
				}
			}
			return null;
		}

		/// <summary>
		/// 指定 Interface を子孫 ( 自身を含む ) Components から取得します ( 全部 ).
		/// </summary>
		/// <typeparam name="T">Interface の型.</typeparam>
		/// <param name="self">Component 自身.</param>
		/// <param name="includeInactive">非アクティブ子孫も走査対象に含めるかどうか.</param>
		/// <returns>Interface 配列.</returns>
		public static T[] GetComponentInterfacesInChildren<T>(this Component self, bool includeInactive) where T : class
		{
			Component[] componentList = self.GetComponentsInChildren<Component>(includeInactive);
			List<T> result = new List<T>(componentList.Length);
			foreach (Component n in componentList) {
				T component = n as T;
				if (component != null) {
					result.Add(component);
				}
			}
			return result.ToArray();
		}
		#endregion

		#endregion

		#region Transform

#if ENABLE_WORLD_COORDINATE_EXTENSIONS
		#region transform.world
		/// <summary>
		/// ワールド Transform のリセットを試します.
		/// </summary>
		public static void ResetWorldTransform(this Component self)
		{
			self.ResetWorldPosition();
			self.ResetWorldEulerAngles();
			self.ResetWorldScale();
		}
		/// <summary>
		/// ワールド Transform のリセットを試します.
		/// </summary>
		public static void TryResetWorldTransform(this Component self)
		{
			if (self) {
				self.ResetWorldTransform();
			}
		}

		#region transform.world.position
		/// <summary>
		/// ワールド座標を(0, 0, 0)にリセットします.
		/// </summary>
		public static void ResetWorldPosition(this Component self)
		{
			self.transform.position = Vector3.zero;
		}
		/// <summary>
		/// ワールド座標を(0, 0, 0)にリセットします.
		/// </summary>
		public static void TryResetWorldPosition(this Component self)
		{
			if (self) {
				self.ResetWorldPosition();
			}
		}

		/// <summary>
		/// ワールド座標を返します.
		/// </summary>
		/// <returns>ワールド座標.</returns>
		public static Vector3 GetWorldPosition(this Component self)
		{
			return self.transform.position;
		}
		/// <summary>
		/// ワールド座標を返します. Unity 的に破棄済みの場合は defaultValue を返す.
		/// </summary>
		/// <returns>ワールド座標.</returns>
		public static Vector3 TryGetWorldPosition(this Component self, Vector3 defaultValue = default(Vector3))
		{
			return self ? self.GetWorldPosition() : defaultValue;
		}

		/// <summary>
		/// ワールド X 座標を返します.
		/// </summary>
		/// <returns>ワールド X 座標.</returns>
		public static float GetWorldPositionX(this Component self)
		{
			return self.transform.position.x;
		}
		/// <summary>
		/// ワールド X 座標を返します. Unity 的に破棄済みの場合は defaultValue を返す.
		/// </summary>
		/// <returns>ワールド X 座標.</returns>
		public static float TryGetWorldPositionX(this Component self, float defaultValue = default(float))
		{
			return self ? self.GetWorldPositionX() : defaultValue;
		}

		/// <summary>
		/// ワールド Y 座標を返します.
		/// </summary>
		/// <returns>ワールド Y 座標.</returns>
		public static float GetWorldPositionY(this Component self)
		{
			return self.transform.position.y;
		}
		/// <summary>
		/// ワールド Y 座標を返します. Unity 的に破棄済みの場合は defaultValue を返す.
		/// </summary>
		/// <returns>ワールド Y 座標.</returns>
		public static float TryGetWorldPositionY(this Component self, float defaultValue = default(float))
		{
			return self ? self.GetWorldPositionY() : defaultValue;
		}

		/// <summary>
		/// ワールド Z 座標を返します.
		/// </summary>
		/// <returns>ワールド Z 座標.</returns>
		public static float GetWorldPositionZ(this Component self)
		{
			return self.transform.position.z;
		}
		/// <summary>
		/// ワールド Z 座標を返します. Unity 的に破棄済みの場合は defaultValue を返す.
		/// </summary>
		/// <returns>ワールド Z 座標.</returns>
		public static float TryGetWorldPositionZ(this Component self, float defaultValue = default(float))
		{
			return self ? self.GetWorldPositionZ() : defaultValue;
		}

		/// <summary>
		/// ワールド X 座標を設定します.
		/// </summary>
		/// <param name="x">ワールド X 座標.</param>
		public static void SetWorldPositionX(this Component self, float x)
		{
			Vector3 pos = self.transform.position;
			pos.x = x;
			self.transform.position = pos;
		}
		/// <summary>
		/// ワールド X 座標を設定します.
		/// </summary>
		/// <param name="x">ワールド X 座標.</param>
		public static void TrySetWorldPositionX(this Component self, float x)
		{
			if (self) {
				self.SetWorldPositionX(x);
			}
		}

		/// <summary>
		/// ワールド Y 座標を設定します.
		/// </summary>
		/// <param name="y">ワールド Y 座標.</param>
		public static void SetWorldPositionY(this Component self, float y)
		{
			Vector3 pos = self.transform.position;
			pos.y = y;
			self.transform.position = pos;
		}
		/// <summary>
		/// ワールド Y 座標を設定します.
		/// </summary>
		/// <param name="y">ワールド Y 座標.</param>
		public static void TrySetWorldPositionY(this Component self, float y)
		{
			if (self) {
				self.SetWorldPositionY(y);
			}
		}

		/// <summary>
		/// ワールド Z 座標を設定します.
		/// </summary>
		/// <param name="z">ワールド Z 座標.</param>
		public static void SetWorldPositionZ(this Component self, float z)
		{
			Vector3 pos = self.transform.position;
			pos.z = z;
			self.transform.position = pos;
		}
		/// <summary>
		/// ワールド Z 座標を設定します.
		/// </summary>
		/// <param name="z">ワールド Z 座標.</param>
		public static void TrySetWorldPositionZ(this Component self, float z)
		{
			if (self) {
				self.SetWorldPositionZ(z);
			}
		}

		/// <summary>
		/// ワールド座標を設定します.
		/// </summary>
		/// <param name="x">ワールド X 座標.</param>
		/// <param name="y">ワールド Y 座標.</param>
		public static void SetWorldPosition(this Component self, float x, float y)
		{
			Vector3 pos = self.transform.position;
			pos.x = x;
			pos.y = y;
			self.transform.position = pos;
		}
		/// <summary>
		/// ワールド座標を設定します.
		/// </summary>
		/// <param name="x">ワールド X 座標.</param>
		/// <param name="y">ワールド Y 座標.</param>
		public static void TrySetWorldPosition(this Component self, float x, float y)
		{
			if (self) {
				self.SetWorldPosition(x, y);
			}
		}

		/// <summary>
		/// ワールド座標を設定します.
		/// </summary>
		/// <param name="x">ワールド X 座標.</param>
		/// <param name="y">ワールド Y 座標.</param>
		/// <param name="z">ワールド Z 座標.</param>
		public static void SetWorldPosition(this Component self, float x, float y, float z)
		{
			self.transform.position = new Vector3(x, y, z);
		}
		/// <summary>
		/// ワールド座標を設定します.
		/// </summary>
		/// <param name="x">ワールド X 座標.</param>
		/// <param name="y">ワールド Y 座標.</param>
		/// <param name="z">ワールド Z 座標.</param>
		public static void TrySetWorldPosition(this Component self, float x, float y, float z)
		{
			if (self) {
				self.SetWorldPosition(x, y, z);
			}
		}

		/// <summary>
		/// ワールド座標を設定します.
		/// </summary>
		/// <param name="position">ワールド座標.</param>
		public static void SetWorldPosition(this Component self, Vector2 position)
		{
			self.transform.position = new Vector3(position.x, position.y, self.transform.position.z);
		}
		/// <summary>
		/// ワールド座標を設定します.
		/// </summary>
		/// <param name="position">ワールド座標.</param>
		public static void TrySetWorldPosition(this Component self, Vector2 position)
		{
			if (self) {
				self.SetWorldPosition(position);
			}
		}

		/// <summary>
		/// ワールド座標を設定します.
		/// </summary>
		/// <param name="position">ワールド座標.</param>
		public static void SetWorldPosition(this Component self, Vector3 position)
		{
			self.transform.position = position;
		}
		/// <summary>
		/// ワールド座標を設定します.
		/// </summary>
		/// <param name="position">ワールド座標.</param>
		public static void TrySetWorldPosition(this Component self, Vector3 position)
		{
			if (self) {
				self.SetWorldPosition(position);
			}
		}

		/// <summary>
		/// ワールド X 座標に加算します.
		/// </summary>
		/// <param name="x">加算値.</param>
		public static void AddWorldPositionX(this Component self, float x)
		{
			self.transform.position += new Vector3(x, 0, 0);
		}
		/// <summary>
		/// ワールド X 座標に加算します.
		/// </summary>
		/// <param name="x">加算値.</param>
		public static void TryAddWorldPositionX(this Component self, float x)
		{
			if (self) {
				self.AddWorldPositionX(x);
			}
		}

		/// <summary>
		/// ワールド Y 座標に加算します.
		/// </summary>
		/// <param name="y">加算値.</param>
		public static void AddWorldPositionY(this Component self, float y)
		{
			self.transform.position += new Vector3(0, y, 0);
		}
		/// <summary>
		/// ワールド Y 座標に加算します.
		/// </summary>
		/// <param name="y">加算値.</param>
		public static void TryAddWorldPositionY(this Component self, float y)
		{
			if (self) {
				self.AddWorldPositionY(y);
			}
		}

		/// <summary>
		/// ワールド Z 座標に加算します.
		/// </summary>
		/// <param name="z">加算値.</param>
		public static void AddWorldPositionZ(this Component self, float z)
		{
			self.transform.position += new Vector3(0, 0, z);
		}
		/// <summary>
		/// ワールド Z 座標に加算します.
		/// </summary>
		/// <param name="z">加算値.</param>
		public static void TryAddWorldPositionZ(this Component self, float z)
		{
			if (self) {
				self.AddWorldPositionZ(z);
			}
		}

		/// <summary>
		/// ワールド座標を加算します.
		/// </summary>
		/// <param name="x">ワールド X 座標に加算する値.</param>
		/// <param name="y">ワールド Y 座標に加算する値.</param>
		public static void AddWorldPosition(this Component self, float x, float y)
		{
			self.transform.position += new Vector3(x, y, 0);
		}
		/// <summary>
		/// ワールド座標を加算します.
		/// </summary>
		/// <param name="x">ワールド X 座標に加算する値.</param>
		/// <param name="y">ワールド Y 座標に加算する値.</param>
		public static void TryAddWorldPosition(this Component self, float x, float y)
		{
			if (self) {
				self.AddWorldPosition(x, y);
			}
		}

		/// <summary>
		/// ワールド座標を加算します.
		/// </summary>
		/// <param name="x">ワールド X 座標に加算する値.</param>
		/// <param name="y">ワールド Y 座標に加算する値.</param>
		/// <param name="z">ワールド Z 座標に加算する値.</param>
		public static void AddWorldPosition(this Component self, float x, float y, float z)
		{
			self.transform.position += new Vector3(x, y, z);
		}
		/// <summary>
		/// ワールド座標を加算します.
		/// </summary>
		/// <param name="x">ワールド X 座標に加算する値.</param>
		/// <param name="y">ワールド Y 座標に加算する値.</param>
		/// <param name="z">ワールド Z 座標に加算する値.</param>
		public static void TryAddWorldPosition(this Component self, float x, float y, float z)
		{
			if (self) {
				self.AddWorldPosition(x, y, z);
			}
		}

		/// <summary>
		/// ワールド座標を加算します.
		/// </summary>
		/// <param name="v">加算値.</param>
		public static void AddWorldPosition(this Component self, Vector2 v)
		{
			self.transform.position += new Vector3(v.x, v.y);
		}
		/// <summary>
		/// ワールド座標を加算します.
		/// </summary>
		/// <param name="v">加算値.</param>
		public static void TryAddWorldPosition(this Component self, Vector2 v)
		{
			if (self) {
				self.AddWorldPosition(v);
			}
		}

		/// <summary>
		/// ワールド座標を加算します.
		/// </summary>
		/// <param name="v">加算値.</param>
		public static void AddWorldPosition(this Component self, Vector3 v)
		{
			self.transform.position += v;
		}
		/// <summary>
		/// ワールド座標を加算します.
		/// </summary>
		/// <param name="v">加算値.</param>
		public static void TryAddWorldPosition(this Component self, Vector3 v)
		{
			if (self) {
				self.AddWorldPosition(v);
			}
		}
		#endregion

		#region transform.world.scale
		/// <summary>
		/// ワールドスケールをリセットします.
		/// </summary>
		public static void ResetWorldScale(this Component self)
		{
			SetWorldScale(self, Vector3.one);
		}
		/// <summary>
		/// ワールドスケールのリセットを試します.
		/// </summary>
		public static void TryResetWorldScale(this Component self)
		{
			TrySetWorldScale(self, Vector3.one);
		}

		/// <summary>
		/// ワールドスケールを返します.
		/// </summary>
		/// <returns>ワールドスケール.</returns>
		public static Vector3 GetWorldScale(this Component self)
		{
			return self.transform.lossyScale;
		}
		/// <summary>
		/// ワールドスケールを返します. Unity 的に破棄済みの場合は defaultValue を返す.
		/// </summary>
		/// <returns>ワールドスケール.</returns>
		public static Vector3 TryGetWorldScale(this Component self, Vector3 defaultValue = default(Vector3))
		{
			return self ? self.GetWorldScale() : defaultValue;
		}

		/// <summary>
		/// ワールド X スケールを返します.
		/// </summary>
		/// <returns>ワールド X スケール.</returns>
		public static float GetWorldScaleX(this Component self)
		{
			return self.transform.lossyScale.x;
		}
		/// <summary>
		/// ワールド X スケールを返します. Unity 的に破棄済みの場合は defaultValue を返す.
		/// </summary>
		/// <returns>ワールド X スケール.</returns>
		public static float TryGetWorldScaleX(this Component self, float defaultValue = default(float))
		{
			return self ? self.GetWorldScaleX() : defaultValue;
		}

		/// <summary>
		/// ワールド Y スケールを返します.
		/// </summary>
		/// <returns>ワールド Y スケール.</returns>
		public static float GetWorldScaleY(this Component self)
		{
			return self.transform.lossyScale.y;
		}
		/// <summary>
		/// ワールド Y スケールを返します. Unity 的に破棄済みの場合は defaultValue を返す.
		/// </summary>
		/// <returns>ワールド Y スケール.</returns>
		public static float TryGetWorldScaleY(this Component self, float defaultValue = default(float))
		{
			return self ? self.GetWorldScaleY() : defaultValue;
		}

		/// <summary>
		/// ワールド Z スケールを返します.
		/// </summary>
		/// <returns>ワールド Z スケール</returns>
		public static float GetWorldScaleZ(this Component self)
		{
			return self.transform.lossyScale.z;
		}
		/// <summary>
		/// ワールド Z スケールを返します. Unity 的に破棄済みの場合は defaultValue を返す.
		/// </summary>
		/// <returns>ワールド Z スケール</returns>
		public static float TryGetWorldScaleZ(this Component self, float defaultValue = default(float))
		{
			return self ? self.GetWorldScaleZ() : defaultValue;
		}

		/// <summary>
		/// ワールドスケールを設定します.
		/// </summary>
		/// <param name="scale">ワールドスケール.</param>
		public static void SetWorldScale(this Component self, Vector3 scale)
		{
			Vector3 localScale = self.transform.localScale;

			Transform t = self.transform.parent;
			if (t) {
				localScale.x = (t.lossyScale.x != 0) ? (scale.x / t.lossyScale.x) : 1;
				localScale.y = (t.lossyScale.y != 0) ? (scale.y / t.lossyScale.y) : 1;
				localScale.z = (t.lossyScale.z != 0) ? (scale.z / t.lossyScale.z) : 1;
				self.transform.localScale = localScale;
			}
			else {
				self.transform.localScale = scale;
			}
		}
		/// <summary>
		/// ワールドスケールを設定します.
		/// </summary>
		/// <param name="scale">ワールドスケール.</param>
		public static void TrySetWorldScale(this Component self, Vector3 scale)
		{
			if (self) {
				self.SetWorldScale(scale);
			}
		}
		#endregion

		#region transform.world.eulerAngles
		/// <summary>
		/// ワールド回転角を(0, 0, 0)にリセットします.
		/// </summary>
		public static void ResetWorldEulerAngles(this Component self)
		{
			self.transform.eulerAngles = Vector3.zero;
		}
		/// <summary>
		/// ワールド回転角を(0, 0, 0)にリセットします.
		/// </summary>
		public static void TryResetWorldEulerAngles(this Component self)
		{
			if (self) {
				self.ResetWorldEulerAngles();
			}
		}

		/// <summary>
		/// ワールド回転角を返します.
		/// </summary>
		/// <returns>ワールド回転角.</returns>
		public static Vector3 GetWorldEulerAngles(this Component self)
		{
			return self.transform.eulerAngles;
		}
		/// <summary>
		/// ワールド回転角を返します. Unity 的に破棄済みの場合は defaultValue を返す.
		/// </summary>
		/// <returns>ワールド回転角.</returns>
		public static Vector3 TryGetWorldEulerAngles(this Component self, Vector3 defaultValue = default(Vector3))
		{
			return self ? self.GetWorldEulerAngles() : defaultValue;
		}

		/// <summary>
		/// ワールド X 軸方向の回転角を返します.
		/// </summary>
		/// <returns>ワールド X 回転角.</returns>
		public static float GetWorldEulerAngleX(this Component self)
		{
			return self.transform.eulerAngles.x;
		}
		/// <summary>
		/// ワールド X 軸方向の回転角を返します. Unity 的に破棄済みの場合は defaultValue を返す.
		/// </summary>
		/// <returns>ワールド X 回転角.</returns>
		public static float TryGetWorldEulerAngleX(this Component self, float defaultValue = default(float))
		{
			return self ? self.GetWorldEulerAngleX() : defaultValue;
		}

		/// <summary>
		/// ワールド Y 軸方向の回転角を返します.
		/// </summary>
		/// <returns>ワールド Y 回転角.</returns>
		public static float GetWorldEulerAngleY(this Component self)
		{
			return self.transform.eulerAngles.y;
		}
		/// <summary>
		/// ワールド Y 軸方向の回転角を返します. Unity 的に破棄済みの場合は defaultValue を返す.
		/// </summary>
		/// <returns>ワールド Y 回転角.</returns>
		public static float TryGetWorldEulerAngleY(this Component self, float defaultValue = default(float))
		{
			return self ? self.GetWorldEulerAngleY() : defaultValue;
		}

		/// <summary>
		/// ワールド Z 軸方向の回転角を返します.
		/// </summary>
		/// <returns>ワールド Z 回転角.</returns>
		public static float GetWorldEulerAngleZ(this Component self)
		{
			return self.transform.eulerAngles.z;
		}
		/// <summary>
		/// ワールド Z 軸方向の回転角を返します. Unity 的に破棄済みの場合は defaultValue を返す.
		/// </summary>
		/// <returns>ワールド Z 回転角.</returns>
		public static float TryGetWorldEulerAngleZ(this Component self, float defaultValue = default(float))
		{
			return self ? self.GetWorldEulerAngleZ() : defaultValue;
		}

		/// <summary>
		/// ワールド X 軸方向の回転角を設定します.
		/// </summary>
		/// <param name="x">回転角.</param>
		public static void SetWorldEulerAngleX(this Component self, float x)
		{
			Vector3 angles = self.transform.eulerAngles;
			angles.x = x;
			self.transform.eulerAngles = angles;
		}
		/// <summary>
		/// ワールド X 軸方向の回転角を設定します.
		/// </summary>
		/// <param name="x">回転角.</param>
		public static void TrySetWorldEulerAngleX(this Component self, float x)
		{
			if (self) {
				self.SetWorldEulerAngleX(x);
			}
		}

		/// <summary>
		/// ワールド Y 軸方向の回転角を設定します.
		/// </summary>
		/// <param name="y">回転角.</param>
		public static void SetWorldEulerAngleY(this Component self, float y)
		{
			Vector3 angles = self.transform.eulerAngles;
			angles.y = y;
			self.transform.eulerAngles = angles;
		}
		/// <summary>
		/// ワールド Y 軸方向の回転角を設定します.
		/// </summary>
		/// <param name="y">回転角.</param>
		public static void TrySetWorldEulerAngleY(this Component self, float y)
		{
			if (self) {
				self.SetWorldEulerAngleY(y);
			}
		}

		/// <summary>
		/// ワールド Z 軸方向の回転角を設定します.
		/// </summary>
		/// <param name="z">回転角.</param>
		public static void SetWorldEulerAngleZ(this Component self, float z)
		{
			Vector3 angles = self.transform.eulerAngles;
			angles.z = z;
			self.transform.eulerAngles = angles;
		}
		/// <summary>
		/// ワールド Z 軸方向の回転角を設定します.
		/// </summary>
		/// <param name="z">回転角.</param>
		public static void TrySetWorldEulerAngleZ(this Component self, float z)
		{
			if (self) {
				self.SetWorldEulerAngleZ(z);
			}
		}

		/// <summary>
		/// ワールド回転角を設定します.
		/// </summary>
		/// <param name="angles">回転角.</param>
		public static void SetWorldEulerAngles(this Component self, Vector3 angles)
		{
			self.transform.eulerAngles = angles;
		}
		/// <summary>
		/// ワールド回転角を設定します.
		/// </summary>
		/// <param name="angles">回転角.</param>
		public static void TrySetWorldEulerAngles(this Component self, Vector3 angles)
		{
			if (self) {
				self.SetWorldEulerAngles(angles);
			}
		}

		/// <summary>
		/// ワールド X 軸方向の回転角を加算します.
		/// </summary>
		/// <param name="x">加算値.</param>
		public static void AddWorldEulerAngleX(this Component self, float x)
		{
			self.transform.Rotate(x, 0, 0, Space.World);
		}
		/// <summary>
		/// ワールド X 軸方向の回転角を加算します.
		/// </summary>
		/// <param name="x">加算値.</param>
		public static void TryAddWorldEulerAngleX(this Component self, float x)
		{
			if (self) {
				self.AddWorldEulerAngleX(x);
			}
		}

		/// <summary>
		/// ワールド Y 軸方向の回転角を加算します.
		/// </summary>
		/// <param name="y">加算値.</param>
		public static void AddWorldEulerAngleY(this Component self, float y)
		{
			self.transform.Rotate(0, y, 0, Space.World);
		}
		/// <summary>
		/// ワールド Y 軸方向の回転角を加算します.
		/// </summary>
		/// <param name="y">加算値.</param>
		public static void TryAddWorldEulerAngleY(this Component self, float y)
		{
			if (self) {
				self.AddWorldEulerAngleY(y);
			}
		}

		/// <summary>
		/// ワールド Z 軸方向の回転角を加算します.
		/// </summary>
		/// <param name="z">加算値.</param>
		public static void AddWorldEulerAngleZ(this Component self, float z)
		{
			self.transform.Rotate(0, 0, z, Space.World);
		}
		/// <summary>
		/// ワールド Z 軸方向の回転角を加算します.
		/// </summary>
		/// <param name="z">加算値.</param>
		public static void TryAddWorldEulerAngleZ(this Component self, float z)
		{
			if (self) {
				self.AddWorldEulerAngleZ(z);
			}
		}

		/// <summary>
		/// ワールド回転角を加算します.
		/// </summary>
		/// <param name="v">加算値.</param>
		public static void AddWorldEulerAngle(this Component self, Vector3 v)
		{
			self.transform.Rotate(v, Space.World);
		}
		/// <summary>
		/// ワールド回転角を加算します.
		/// </summary>
		/// <param name="v">加算値.</param>
		public static void TryAddWorldEulerAngle(this Component self, Vector3 v)
		{
			if (self) {
				self.AddWorldEulerAngle(v);
			}
		}
		#endregion

		#endregion
#endif

		#region transform.local
		/// <summary>
		/// ローカル座標を(0, 0, 0)にリセットします.
		/// </summary>
		public static void ResetLocalTransform(this Component self)
		{
			self.transform.localPosition = Vector3.zero;
			self.transform.localEulerAngles = Vector3.zero;
			self.transform.localScale = Vector3.one;
		}
		/// <summary>
		/// ローカル座標を(0, 0, 0)にリセットします.
		/// </summary>
		public static void TryResetLocalTransform(this Component self)
		{
			if (self) {
				self.ResetLocalTransform();
			}
		}

		#region transform.local.position
		/// <summary>
		/// ローカル座標を(0, 0, 0)にリセットします.
		/// </summary>
		public static void ResetLocalPosition(this Component self)
		{
			self.transform.localPosition = Vector3.zero;
		}
		/// <summary>
		/// ローカル座標を(0, 0, 0)にリセットします.
		/// </summary>
		public static void TryResetLocalPosition(this Component self)
		{
			if (self) {
				self.ResetLocalPosition();
			}
		}

		/// <summary>
		/// ローカル座標を返します.
		/// </summary>
		/// <returns>ローカル座標.</returns>
		public static Vector3 GetLocalPosition(this Component self)
		{
			return self.transform.localPosition;
		}
		/// <summary>
		/// ローカル座標を返します. Unity 的に破棄済みの場合は defaultValue を返す.
		/// </summary>
		/// <returns>ローカル座標.</returns>
		public static Vector3 TryGetLocalPosition(this Component self, Vector3 defaultValue = default(Vector3))
		{
			return self ? self.GetLocalPosition() : defaultValue;
		}

		/// <summary>
		/// ローカル座標系の X 座標を返します.
		/// </summary>
		/// <returns>ローカル X 座標.</returns>
		public static float GetLocalPositionX(this Component self)
		{
			return self.transform.localPosition.x;
		}
		/// <summary>
		/// ローカル座標系の X 座標を返します. Unity 的に破棄済みの場合は defaultValue を返す.
		/// </summary>
		/// <returns>ローカル X 座標.</returns>
		public static float TryGetLocalPositionX(this Component self, float defaultValue = default(float))
		{
			return self ? self.GetLocalPositionX() : defaultValue;
		}

		/// <summary>
		/// ローカル座標系の Y 座標を返します.
		/// </summary>
		/// <returns>ローカル Y 座標.</returns>
		public static float GetLocalPositionY(this Component self)
		{
			return self.transform.localPosition.y;
		}
		/// <summary>
		/// ローカル座標系の Y 座標を返します. Unity 的に破棄済みの場合は defaultValue を返す.
		/// </summary>
		/// <returns>ローカル Y 座標.</returns>
		public static float TryGetLocalPositionY(this Component self, float defaultValue = default(float))
		{
			return self ? self.GetLocalPositionY() : defaultValue;
		}

		/// <summary>
		/// ローカル座標系の Z 座標を返します.
		/// </summary>
		/// <returns>ローカル Z 座標.</returns>
		public static float GetLocalPositionZ(this Component self)
		{
			return self.transform.localPosition.z;
		}
		/// <summary>
		/// ローカル座標系の Z 座標を返します. Unity 的に破棄済みの場合は defaultValue を返す.
		/// </summary>
		/// <returns>ローカル Z 座標.</returns>
		public static float TryGetLocalPositionZ(this Component self, float defaultValue = default(float))
		{
			return self ? self.GetLocalPositionZ() : defaultValue;
		}

		/// <summary>
		/// ローカル座標系の X 座標を設定します.
		/// </summary>
		/// <param name="x">ローカル X 座標.</param>
		public static void SetLocalPositionX(this Component self, float x)
		{
			Vector3 pos = self.transform.localPosition;
			pos.x = x;
			self.transform.localPosition = pos;
		}
		/// <summary>
		/// ローカル座標系の X 座標を設定します.
		/// </summary>
		/// <param name="x">ローカル X 座標.</param>
		public static void TrySetLocalPositionX(this Component self, float x)
		{
			if (self) {
				self.SetLocalPositionX(x);
			}
		}

		/// <summary>
		/// ローカル座標系の Y 座標を設定します.
		/// </summary>
		/// <param name="y">ローカル Y 座標.</param>
		public static void SetLocalPositionY(this Component self, float y)
		{
			Vector3 pos = self.transform.localPosition;
			pos.y = y;
			self.transform.localPosition = pos;
		}
		/// <summary>
		/// ローカル座標系の Y 座標を設定します.
		/// </summary>
		/// <param name="y">ローカル Y 座標.</param>
		public static void TrySetLocalPositionY(this Component self, float y)
		{
			if (self) {
				self.SetLocalPositionY(y);
			}
		}

		/// <summary>
		/// ローカル座標系の Z 座標を設定します.
		/// </summary>
		/// <param name="z">ローカル Z 座標.</param>
		public static void SetLocalPositionZ(this Component self, float z)
		{
			Vector3 pos = self.transform.localPosition;
			pos.z = z;
			self.transform.localPosition = pos;
		}
		/// <summary>
		/// ローカル座標系の Z 座標を設定します.
		/// </summary>
		/// <param name="z">ローカル Z 座標.</param>
		public static void TrySetLocalPositionZ(this Component self, float z)
		{
			if (self) {
				self.SetLocalPositionZ(z);
			}
		}

		/// <summary>
		/// ローカル座標を設定します.
		/// </summary>
		/// <param name="x">ローカル X 座標.</param>
		/// <param name="y">ローカル Y 座標.</param>
		public static void SetLocalPosition(this Component self, float x, float y)
		{
			Vector3 pos = self.transform.localPosition;
			pos.x = x;
			pos.y = y;
			self.transform.localPosition = pos;
		}
		/// <summary>
		/// ローカル座標を設定します.
		/// </summary>
		/// <param name="x">ローカル X 座標.</param>
		/// <param name="y">ローカル Y 座標.</param>
		public static void TrySetLocalPosition(this Component self, float x, float y)
		{
			if (self) {
				self.SetLocalPosition(x, y);
			}
		}

		/// <summary>
		/// ローカル座標を設定します.
		/// </summary>
		/// <param name="x">ローカル X 座標.</param>
		/// <param name="y">ローカル Y 座標.</param>
		/// <param name="z">ローカル Z 座標.</param>
		public static void SetLocalPosition(this Component self, float x, float y, float z)
		{
			self.transform.localPosition = new Vector3(x, y, z);
		}
		/// <summary>
		/// ローカル座標を設定します.
		/// </summary>
		/// <param name="x">ローカル X 座標.</param>
		/// <param name="y">ローカル Y 座標.</param>
		/// <param name="z">ローカル Z 座標.</param>
		public static void TrySetLocalPosition(this Component self, float x, float y, float z)
		{
			if (self) {
				self.SetLocalPosition(x, y, z);
			}
		}

		/// <summary>
		/// ローカル座標を設定します.
		/// </summary>
		/// <param name="position">ローカル座標.</param>
		public static void SetLocalPosition(this Component self, Vector2 position)
		{
			self.transform.localPosition = new Vector3(position.x, position.y, self.transform.localPosition.z);
		}
		/// <summary>
		/// ローカル座標を設定します.
		/// </summary>
		/// <param name="position">ローカル座標.</param>
		public static void TrySetLocalPosition(this Component self, Vector2 position)
		{
			if (self) {
				self.SetLocalPosition(position);
			}
		}

		/// <summary>
		/// ローカル座標を設定します.
		/// </summary>
		/// <param name="position">ローカル座標.</param>
		public static void SetLocalPosition(this Component self, Vector3 position)
		{
			self.transform.localPosition = position;
		}
		/// <summary>
		/// ローカル座標を設定します.
		/// </summary>
		/// <param name="position">ローカル座標.</param>
		public static void TrySetLocalPosition(this Component self, Vector3 position)
		{
			if (self) {
				self.SetLocalPosition(position);
			}
		}

		/// <summary>
		/// ローカルの X 座標に加算します.
		/// </summary>
		/// <param name="x">加算値.</param>
		public static void AddLocalPositionX(this Component self, float x)
		{
			self.transform.localPosition += new Vector3(x, 0, 0);
		}
		/// <summary>
		/// ローカルの X 座標に加算します.
		/// </summary>
		/// <param name="x">加算値.</param>
		public static void TryAddLocalPositionX(this Component self, float x)
		{
			if (self) {
				self.AddLocalPositionX(x);
			}
		}

		/// <summary>
		/// ローカルの Y 座標に加算します.
		/// </summary>
		/// <param name="y">加算値.</param>
		public static void AddLocalPositionY(this Component self, float y)
		{
			self.transform.localPosition += new Vector3(0, y, 0);
		}
		/// <summary>
		/// ローカルの Y 座標に加算します.
		/// </summary>
		/// <param name="y">加算値.</param>
		public static void TryAddLocalPositionY(this Component self, float y)
		{
			if (self) {
				self.AddLocalPositionY(y);
			}
		}

		/// <summary>
		/// ローカルの Z 座標に加算します.
		/// </summary>
		/// <param name="z">加算値.</param>
		public static void AddLocalPositionZ(this Component self, float z)
		{
			self.transform.localPosition += new Vector3(0, 0, z);
		}
		/// <summary>
		/// ローカルの Z 座標に加算します.
		/// </summary>
		/// <param name="z">加算値.</param>
		public static void TryAddLocalPositionZ(this Component self, float z)
		{
			if (self) {
				self.AddLocalPositionZ(z);
			}
		}

		/// <summary>
		/// ローカル座標を加算します.
		/// </summary>
		/// <param name="x">ローカル X 座標に加算する値.</param>
		/// <param name="y">ローカル Y 座標に加算する値.</param>
		public static void AddLocalPosition(this Component self, float x, float y)
		{
			self.transform.localPosition += new Vector3(x, y, 0);
		}
		/// <summary>
		/// ローカル座標を加算します.
		/// </summary>
		/// <param name="x">ローカル X 座標に加算する値.</param>
		/// <param name="y">ローカル Y 座標に加算する値.</param>
		public static void TryAddLocalPosition(this Component self, float x, float y)
		{
			if (self) {
				self.AddLocalPosition(x, y);
			}
		}

		/// <summary>
		/// ローカル座標を加算します.
		/// </summary>
		/// <param name="x">ローカル X 座標に加算する値.</param>
		/// <param name="y">ローカル Y 座標に加算する値.</param>
		/// <param name="z">ローカル Z 座標に加算する値.</param>
		public static void AddLocalPosition(this Component self, float x, float y, float z)
		{
			self.transform.localPosition += new Vector3(x, y, z);
		}
		/// <summary>
		/// ローカル座標を加算します.
		/// </summary>
		/// <param name="x">ローカル X 座標に加算する値.</param>
		/// <param name="y">ローカル Y 座標に加算する値.</param>
		/// <param name="z">ローカル Z 座標に加算する値.</param>
		public static void TryAddLocalPosition(this Component self, float x, float y, float z)
		{
			if (self) {
				self.AddLocalPosition(x, y, z);
			}
		}

		/// <summary>
		/// ローカル座標を加算します.
		/// </summary>
		/// <param name="v">加算値</param>
		public static void AddLocalPosition(this Component self, Vector2 v)
		{
			self.transform.localPosition += new Vector3(v.x, v.y);
		}
		/// <summary>
		/// ローカル座標を加算します.
		/// </summary>
		/// <param name="v">加算値</param>
		public static void TryAddLocalPosition(this Component self, Vector2 v)
		{
			if (self) {
				self.AddLocalPosition(v);
			}
		}

		/// <summary>
		/// ローカル座標を加算します.
		/// </summary>
		/// <param name="v">加算値</param>
		public static void AddLocalPosition(this Component self, Vector3 v)
		{
			self.transform.localPosition += v;
		}
		/// <summary>
		/// ローカル座標を加算します.
		/// </summary>
		/// <param name="v">加算値</param>
		public static void TryAddLocalPosition(this Component self, Vector3 v)
		{
			if (self) {
				self.AddLocalPosition(v);
			}
		}
		#endregion

		#region transform.local.scale
		/// <summary>
		/// ローカル座標系のスケールを(1, 1, 1)にリセットします.
		/// </summary>
		public static void ResetLocalScale(this Component self)
		{
			self.transform.localScale = Vector3.one;
		}
		/// <summary>
		/// ローカル座標系のスケールを(1, 1, 1)にリセットします.
		/// </summary>
		public static void TryResetLocalScale(this Component self)
		{
			if (self) {
				self.ResetLocalScale();
			}
		}

		/// <summary>
		/// ローカル座標系のスケールを返します.
		/// </summary>
		/// <returns>スケール.</returns>
		public static Vector3 GetLocalScale(this Component self)
		{
			return self.transform.localScale;
		}
		/// <summary>
		/// ローカル座標系のスケールを返します. Unity 的に破棄済みの場合は defaultValue を返す.
		/// </summary>
		/// <returns>スケール.</returns>
		public static Vector3 TryGetLocalScale(this Component self, Vector3 defaultValue = default(Vector3))
		{
			return self ? self.GetLocalScale() : defaultValue;
		}

		/// <summary>
		/// X 軸方向のローカル座標系のスケールを返します.
		/// </summary>
		/// <returns>スケール.</returns>
		public static float GetLocalScaleX(this Component self)
		{
			return self.transform.localScale.x;
		}
		/// <summary>
		/// X 軸方向のローカル座標系のスケールを返します. Unity 的に破棄済みの場合は defaultValue を返す.
		/// </summary>
		/// <returns>スケール.</returns>
		public static float TryGetLocalScaleX(this Component self, float defaultValue = default(float))
		{
			return self ? self.GetLocalScaleX() : defaultValue;
		}

		/// <summary>
		/// Y 軸方向のローカル座標系のスケールを返します.
		/// </summary>
		/// <returns>スケール.</returns>
		public static float GetLocalScaleY(this Component self)
		{
			return self.transform.localScale.y;
		}
		/// <summary>
		/// Y 軸方向のローカル座標系のスケールを返します. Unity 的に破棄済みの場合は defaultValue を返す.
		/// </summary>
		/// <returns>スケール.</returns>
		public static float TryGetLocalScaleY(this Component self, float defaultValue = default(float))
		{
			return self ? self.GetLocalScaleY() : defaultValue;
		}

		/// <summary>
		/// Z 軸方向のローカル座標系のスケールを返します.
		/// </summary>
		/// <returns>スケール.</returns>
		public static float GetLocalScaleZ(this Component self)
		{
			return self.transform.localScale.z;
		}
		/// <summary>
		/// Z 軸方向のローカル座標系のスケールを返します. Unity 的に破棄済みの場合は defaultValue を返す.
		/// </summary>
		/// <returns>スケール.</returns>
		public static float TryGetLocalScaleZ(this Component self, float defaultValue = default(float))
		{
			return self ? self.GetLocalScaleZ() : defaultValue;
		}

		/// <summary>
		/// X 軸方向のローカル座標系のスケールを設定します.
		/// </summary>
		/// <param name="x">スケール.</param>
		public static void SetLocalScaleX(this Component self, float x)
		{
			Vector3 v = self.transform.localScale;
			v.x = x;
			self.transform.localScale = v;
		}
		/// <summary>
		/// X 軸方向のローカル座標系のスケールを設定します.
		/// </summary>
		/// <param name="x">スケール.</param>
		public static void TrySetLocalScaleX(this Component self, float x)
		{
			if (self) {
				self.SetLocalScaleX(x);
			}
		}

		/// <summary>
		/// Y 軸方向のローカル座標系のスケールを設定します.
		/// </summary>
		/// <param name="y">スケール.</param>
		public static void SetLocalScaleY(this Component self, float y)
		{
			Vector3 v = self.transform.localScale;
			v.y = y;
			self.transform.localScale = v;
		}
		/// <summary>
		/// Y 軸方向のローカル座標系のスケールを設定します.
		/// </summary>
		/// <param name="y">スケール.</param>
		public static void TrySetLocalScaleY(this Component self, float y)
		{
			if (self) {
				self.SetLocalScaleY(y);
			}
		}

		/// <summary>
		/// Z 軸方向のローカル座標系のスケールを設定します.
		/// </summary>
		/// <param name="z">スケール.</param>
		public static void SetLocalScaleZ(this Component self, float z)
		{
			Vector3 v = self.transform.localScale;
			v.z = z;
			self.transform.localScale = v;
		}
		/// <summary>
		/// Z 軸方向のローカル座標系のスケールを設定します.
		/// </summary>
		/// <param name="z">スケール.</param>
		public static void TrySetLocalScaleZ(this Component self, float z)
		{
			if (self) {
				self.SetLocalScaleZ(z);
			}
		}

		/// <summary>
		/// ローカル座標系のスケールを設定します.
		/// </summary>
		/// <param name="x">ローカル X スケール.</param>
		/// <param name="y">ローカル Y スケール.</param>
		public static void SetLocalScale(this Component self, float x, float y)
		{
			Vector3 scale = self.transform.localScale;
			scale.x = x;
			scale.y = y;
			self.transform.localScale = scale;
		}
		/// <summary>
		/// ローカル座標系のスケールを設定します.
		/// </summary>
		/// <param name="x">ローカル X スケール.</param>
		/// <param name="y">ローカル Y スケール.</param>
		public static void TrySetLocalScale(this Component self, float x, float y)
		{
			if (self) {
				self.SetLocalScale(x, y);
			}
		}

		/// <summary>
		/// ローカル座標系のスケールを設定します.
		/// </summary>
		/// <param name="x">ローカル X スケール.</param>
		/// <param name="y">ローカル Y スケール.</param>
		/// <param name="z">ローカル Z スケール.</param>
		public static void SetLocalScale(this Component self, float x, float y, float z)
		{
			self.transform.localScale = new Vector3(x, y, z);
		}
		/// <summary>
		/// ローカル座標系のスケールを設定します.
		/// </summary>
		/// <param name="x">ローカル X スケール.</param>
		/// <param name="y">ローカル Y スケール.</param>
		/// <param name="z">ローカル Z スケール.</param>
		public static void TrySetLocalScale(this Component self, float x, float y, float z)
		{
			if (self) {
				self.SetLocalScale(x, y, z);
			}
		}

		/// <summary>
		/// ローカル座標系のスケールを設定します.
		/// </summary>
		/// <param name="scale">スケール.</param>
		public static void SetLocalScale(this Component self, Vector2 scale)
		{
			self.transform.localScale = new Vector3(scale.x, scale.y, self.transform.localScale.z);
		}
		/// <summary>
		/// ローカル座標系のスケールを設定します.
		/// </summary>
		/// <param name="scale">スケール.</param>
		public static void TrySetLocalScale(this Component self, Vector2 scale)
		{
			if (self) {
				self.SetLocalScale(scale);
			}
		}

		/// <summary>
		/// ローカル座標系のスケールを設定します.
		/// </summary>
		/// <param name="scale">スケール.</param>
		public static void SetLocalScale(this Component self, Vector3 scale)
		{
			self.transform.localScale = scale;
		}
		/// <summary>
		/// ローカル座標系のスケールを設定します.
		/// </summary>
		/// <param name="scale">スケール.</param>
		public static void TrySetLocalScale(this Component self, Vector3 scale)
		{
			if (self) {
				self.SetLocalScale(scale);
			}
		}

		/// <summary>
		/// X 軸方向のローカル座標系のスケールを加算します.
		/// </summary>
		/// <param name="x">加算値.</param>
		public static void AddLocalScaleX(this Component self, float x)
		{
			self.transform.localScale += new Vector3(x, 0, 0);
		}
		/// <summary>
		/// X 軸方向のローカル座標系のスケールを加算します.
		/// </summary>
		/// <param name="x">加算値.</param>
		public static void TryAddLocalScaleX(this Component self, float x)
		{
			if (self) {
				self.AddLocalScaleX(x);
			}
		}

		/// <summary>
		/// Y 軸方向のローカル座標系のスケールを加算します.
		/// </summary>
		/// <param name="y">加算値.</param>
		public static void AddLocalScaleY(this Component self, float y)
		{
			self.transform.localScale += new Vector3(0, y, 0);
		}
		/// <summary>
		/// Y 軸方向のローカル座標系のスケールを加算します.
		/// </summary>
		/// <param name="y">加算値.</param>
		public static void TryAddLocalScaleY(this Component self, float y)
		{
			if (self) {
				self.AddLocalScaleY(y);
			}
		}

		/// <summary>
		/// Z 軸方向のローカル座標系のスケールを加算します.
		/// </summary>
		/// <param name="z">加算値.</param>
		public static void AddLocalScaleZ(this Component self, float z)
		{
			self.transform.localScale += new Vector3(0, 0, z);
		}
		/// <summary>
		/// Z 軸方向のローカル座標系のスケールを加算します.
		/// </summary>
		/// <param name="z">加算値.</param>
		public static void TryAddLocalScaleZ(this Component self, float z)
		{
			if (self) {
				self.AddLocalScaleZ(z);
			}
		}

		/// <summary>
		/// ローカル座標系のスケールを加算します.
		/// </summary>
		/// <param name="x">ローカル X スケールに加算する値.</param>
		/// <param name="y">ローカル Y スケールに加算する値.</param>
		public static void AddLocalScale(this Component self, float x, float y)
		{
			self.transform.localScale = new Vector3(x, y, 0);
		}
		/// <summary>
		/// ローカル座標系のスケールを加算します.
		/// </summary>
		/// <param name="x">ローカル X スケールに加算する値.</param>
		/// <param name="y">ローカル Y スケールに加算する値.</param>
		public static void TryAddLocalScale(this Component self, float x, float y)
		{
			if (self) {
				self.AddLocalScale(x, y);
			}
		}

		/// <summary>
		/// ローカル座標系のスケールを加算します.
		/// </summary>
		/// <param name="x">ローカル X スケールに加算する値.</param>
		/// <param name="y">ローカル Y スケールに加算する値.</param>
		/// <param name="z">ローカル Z スケールに加算する値.</param>
		public static void AddLocalScale(this Component self, float x, float y, float z)
		{
			self.transform.localScale = new Vector3(x, y, z);
		}
		/// <summary>
		/// ローカル座標系のスケールを加算します.
		/// </summary>
		/// <param name="x">ローカル X スケールに加算する値.</param>
		/// <param name="y">ローカル Y スケールに加算する値.</param>
		/// <param name="z">ローカル Z スケールに加算する値.</param>
		public static void TryAddLocalScale(this Component self, float x, float y, float z)
		{
			if (self) {
				self.AddLocalScale(x, y, z);
			}
		}

		/// <summary>
		/// ローカル座標系のスケールを加算します.
		/// </summary>
		/// <param name="v">加算値.</param>
		public static void AddLocalScale(this Component self, Vector2 v)
		{
			self.transform.localScale += new Vector3(v.x, v.y);
		}
		/// <summary>
		/// ローカル座標系のスケールを加算します.
		/// </summary>
		/// <param name="v">加算値.</param>
		public static void TryAddLocalScale(this Component self, Vector2 v)
		{
			if (self) {
				self.AddLocalScale(v);
			}
		}

		/// <summary>
		/// ローカル座標系のスケールを加算します.
		/// </summary>
		/// <param name="v">加算値.</param>
		public static void AddLocalScale(this Component self, Vector3 v)
		{
			self.transform.localScale += v;
		}
		/// <summary>
		/// ローカル座標系のスケールを加算します.
		/// </summary>
		/// <param name="v">加算値.</param>
		public static void TryAddLocalScale(this Component self, Vector3 v)
		{
			if (self) {
				self.AddLocalScale(v);
			}
		}
		#endregion

		#region transform.local.eulerAngles
		/// <summary>
		/// ローカルの回転角を(0, 0, 0)にリセットします.
		/// </summary>
		public static void ResetLocalEulerAngles(this Component self)
		{
			self.transform.localEulerAngles = Vector3.zero;
		}
		/// <summary>
		/// ローカルの回転角を(0, 0, 0)にリセットします.
		/// </summary>
		public static void TryResetLocalEulerAngles(this Component self)
		{
			if (self) {
				self.ResetLocalEulerAngles();
			}
		}

		/// <summary>
		/// ローカルの回転角を返します.
		/// </summary>
		/// <returns>回転角.</returns>
		public static Vector3 GetLocalEulerAngles(this Component self)
		{
			return self.transform.localEulerAngles;
		}
		/// <summary>
		/// ローカルの回転角を返します. Unity 的に破棄済みの場合は defaultValue を返す.
		/// </summary>
		/// <returns>回転角.</returns>
		public static Vector3 TryGetLocalEulerAngles(this Component self, Vector3 defaultValue = default(Vector3))
		{
			return self ? self.GetLocalEulerAngles() : defaultValue;
		}

		/// <summary>
		/// ローカルの X 軸方向の回転角を返します.
		/// </summary>
		/// <returns>回転角.</returns>
		public static float GetLocalEulerAngleX(this Component self)
		{
			return self.transform.localEulerAngles.x;
		}
		/// <summary>
		/// ローカルの X 軸方向の回転角を返します. Unity 的に破棄済みの場合は defaultValue を返す.
		/// </summary>
		/// <returns>回転角.</returns>
		public static float TryGetLocalEulerAngleX(this Component self, float defaultValue = default(float))
		{
			return self ? self.GetLocalEulerAngleX() : defaultValue;
		}

		/// <summary>
		/// ローカルの Y 軸方向の回転角を返します.
		/// </summary>
		/// <returns>回転角.</returns>
		public static float GetLocalEulerAngleY(this Component self)
		{
			return self.transform.localEulerAngles.y;
		}
		/// <summary>
		/// ローカルの Y 軸方向の回転角を返します. Unity 的に破棄済みの場合は defaultValue を返す.
		/// </summary>
		/// <returns>回転角.</returns>
		public static float TryGetLocalEulerAngleY(this Component self, float defaultValue = default(float))
		{
			return self ? self.GetLocalEulerAngleY() : defaultValue;
		}

		/// <summary>
		/// ローカルの Z 軸方向の回転角を返します.
		/// </summary>
		/// <returns>回転角.</returns>
		public static float GetLocalEulerAngleZ(this Component self)
		{
			return self.transform.localEulerAngles.z;
		}
		/// <summary>
		/// ローカルの Z 軸方向の回転角を返します. Unity 的に破棄済みの場合は defaultValue を返す.
		/// </summary>
		/// <returns>回転角.</returns>
		public static float TryGetLocalEulerAngleZ(this Component self, float defaultValue = default(float))
		{
			return self ? self.GetLocalEulerAngleZ() : defaultValue;
		}

		/// <summary>
		/// ローカルの X 軸方向の回転角を設定します.
		/// </summary>
		/// <param name="x">回転角.</param>
		public static void SetLocalEulerAngleX(this Component self, float x)
		{
			Vector3 angles = self.transform.localEulerAngles;
			angles.x = x;
			self.transform.localEulerAngles = angles;
		}
		/// <summary>
		/// ローカルの X 軸方向の回転角を設定します.
		/// </summary>
		/// <param name="x">回転角.</param>
		public static void TrySetLocalEulerAngleX(this Component self, float x)
		{
			if (self) {
				self.SetLocalEulerAngleX(x);
			}
		}

		/// <summary>
		/// ローカルの Y 軸方向の回転角を設定します.
		/// </summary>
		/// <param name="y">回転角.</param>
		public static void SetLocalEulerAngleY(this Component self, float y)
		{
			Vector3 angles = self.transform.localEulerAngles;
			angles.y = y;
			self.transform.localEulerAngles = angles;
		}
		/// <summary>
		/// ローカルの Y 軸方向の回転角を設定します.
		/// </summary>
		/// <param name="y">回転角.</param>
		public static void TrySetLocalEulerAngleY(this Component self, float y)
		{
			if (self) {
				self.SetLocalEulerAngleY(y);
			}
		}

		/// <summary>
		/// ローカルの Z 軸方向の回転角を設定します.
		/// </summary>
		/// <param name="z">回転角.</param>
		public static void SetLocalEulerAngleZ(this Component self, float z)
		{
			Vector3 angles = self.transform.localEulerAngles;
			angles.z = z;
			self.transform.localEulerAngles = angles;
		}
		/// <summary>
		/// ローカルの Z 軸方向の回転角を設定します.
		/// </summary>
		/// <param name="z">回転角.</param>
		public static void TrySetLocalEulerAngleZ(this Component self, float z)
		{
			if (self) {
				self.SetLocalEulerAngleZ(z);
			}
		}

		/// <summary>
		/// ローカルの回転角を設定します.
		/// </summary>
		/// <param name="angles">回転角.</param>
		public static void SetLocalEulerAngle(this Component self, Vector3 angles)
		{
			self.transform.localEulerAngles = angles;
		}
		/// <summary>
		/// ローカルの回転角を設定します.
		/// </summary>
		/// <param name="angles">回転角.</param>
		public static void TrySetLocalEulerAngle(this Component self, Vector3 angles)
		{
			if (self) {
				self.SetLocalEulerAngle(angles);
			}
		}

		/// <summary>
		/// ローカルの X 軸方向の回転角を加算します.
		/// </summary>
		/// <param name="x">加算値.</param>
		public static void AddLocalEulerAngleX(this Component self, float x)
		{
			self.transform.Rotate(x, 0, 0, Space.Self);
		}
		/// <summary>
		/// ローカルの X 軸方向の回転角を加算します.
		/// </summary>
		/// <param name="x">加算値.</param>
		public static void TryAddLocalEulerAngleX(this Component self, float x)
		{
			if (self) {
				self.AddLocalEulerAngleX(x);
			}
		}

		/// <summary>
		/// ローカルの Y 軸方向の回転角を加算します.
		/// </summary>
		/// <param name="y">加算値.</param>
		public static void AddLocalEulerAngleY(this Component self, float y)
		{
			self.transform.Rotate(0, y, 0, Space.Self);
		}
		/// <summary>
		/// ローカルの Y 軸方向の回転角を加算します.
		/// </summary>
		/// <param name="y">加算値.</param>
		public static void TryAddLocalEulerAngleY(this Component self, float y)
		{
			if (self) {
				self.AddLocalEulerAngleY(y);
			}
		}

		/// <summary>
		/// ローカルの Z 軸方向の回転角を加算します.
		/// </summary>
		/// <param name="z">加算値.</param>
		public static void AddLocalEulerAngleZ(this Component self, float z)
		{
			self.transform.Rotate(0, 0, z, Space.Self);
		}
		/// <summary>
		/// ローカルの Z 軸方向の回転角を加算します.
		/// </summary>
		/// <param name="z">加算値.</param>
		public static void TryAddLocalEulerAngleZ(this Component self, float z)
		{
			if (self) {
				self.AddLocalEulerAngleZ(z);
			}
		}

		/// <summary>
		/// ローカル回転角を加算します.
		/// </summary>
		/// <param name="v">加算値.</param>
		public static void AddLocalEulerAngle(this Component self, Vector3 v)
		{
			self.transform.Rotate(v, Space.Self);
		}
		/// <summary>
		/// ローカル回転角を加算します.
		/// </summary>
		/// <param name="v">加算値.</param>
		public static void TryAddLocalEulerAngle(this Component self, Vector3 v)
		{
			if (self) {
				self.AddLocalEulerAngle(v);
			}
		}
		#endregion

		#endregion

		#region transform.LookAt
		/// <summary>
		/// 向きを変更します.
		/// </summary>
		/// <param name="target">目標 GameObject.</param>
		public static void LookAt(this Component self, GameObject target)
		{
			self.transform.LookAt(target.transform);
		}
		/// <summary>
		/// 向きを変更します.
		/// </summary>
		/// <param name="target">目標 GameObject.</param>
		public static void TryLookAt(this Component self, GameObject target)
		{
			if (self && target) {
				self.LookAt(target);
			}
		}

		/// <summary>
		/// 向きを変更します.
		/// </summary>
		/// <param name="target">目標 GameObject.</param>
		/// <param name="worldUp">上方ベクトル.</param>
		public static void LookAt(this Component self, GameObject target, Vector3 worldUp)
		{
			self.transform.LookAt(target.transform, worldUp);
		}
		/// <summary>
		/// 向きを変更します.
		/// </summary>
		/// <param name="target">目標 GameObject.</param>
		/// <param name="worldUp">上方ベクトル.</param>
		public static void TryLookAt(this Component self, GameObject target, Vector3 worldUp)
		{
			if (self && target) {
				self.LookAt(target, worldUp);
			}
		}

		/// <summary>
		/// 向きを変更します.
		/// </summary>
		/// <param name="target">目標 Transform.</param>
		public static void LookAt(this Component self, Transform target)
		{
			self.transform.LookAt(target);
		}
		/// <summary>
		/// 向きを変更します.
		/// </summary>
		/// <param name="target">目標 Transform.</param>
		public static void TryLookAt(this Component self, Transform target)
		{
			if (self && target) {
				self.LookAt(target);
			}
		}

		/// <summary>
		/// 向きを変更します.
		/// </summary>
		/// <param name="target">目標 Transform.</param>
		/// <param name="worldUp">上方ベクトル.</param>
		public static void LookAt(this Component self, Transform target, Vector3 worldUp)
		{
			self.transform.LookAt(target, worldUp);
		}
		/// <summary>
		/// 向きを変更します.
		/// </summary>
		/// <param name="target">目標 Transform.</param>
		/// <param name="worldUp">上方ベクトル.</param>
		public static void TryLookAt(this Component self, Transform target, Vector3 worldUp)
		{
			if (self && target) {
				self.LookAt(target, worldUp);
			}
		}

		/// <summary>
		/// 向きを変更します.
		/// </summary>
		/// <param name="worldPosition">目標座標.</param>
		public static void LookAt(this Component self, Vector3 worldPosition)
		{
			self.transform.LookAt(worldPosition);
		}
		/// <summary>
		/// 向きを変更します.
		/// </summary>
		/// <param name="worldPosition">目標座標.</param>
		public static void TryLookAt(this Component self, Vector3 worldPosition)
		{
			if (self) {
				self.LookAt(worldPosition);
			}
		}

		/// <summary>
		/// 向きを変更します.
		/// </summary>
		/// <param name="worldPosition">目標座標.</param>
		/// <param name="worldUp">上方ベクトル.</param>
		public static void LookAt(this Component self, Vector3 worldPosition, Vector3 worldUp)
		{
			self.transform.LookAt(worldPosition, worldUp);
		}
		/// <summary>
		/// 向きを変更します.
		/// </summary>
		/// <param name="worldPosition">目標座標.</param>
		/// <param name="worldUp">上方ベクトル.</param>
		public static void TryLookAt(this Component self, Vector3 worldPosition, Vector3 worldUp)
		{
			if (self) {
				self.LookAt(worldPosition, worldUp);
			}
		}
		#endregion

		#endregion

		#region Hierarchy

		#region Parent
		/// <summary>
		/// 親オブジェクトが存在するかどうか.
		/// </summary>
		/// <returns>存在する場合は true.</returns>
		public static bool HasParent(this Component self)
		{
			return self.transform.parent;
		}
		/// <summary>
		/// 親オブジェクトが存在するかどうか. Unity 的に破棄済みの場合は false を返す.
		/// </summary>
		/// <returns>存在する場合は true.</returns>
		public static bool TryHasParent(this Component self)
		{
			return self ? self.HasParent() : false;
		}

		/// <summary>
		/// 親 GameObject の transform を返します. Unity 的に破棄済みの場合は null を返す.
		/// </summary>
		/// <returns>親 GameObject の transform.</returns>
		public static Transform GetParent(this Component self)
		{
			return self.transform.parent;
		}
		/// <summary>
		/// 親 GameObject の transform を返します. Unity 的に破棄済みの場合は null を返す.
		/// </summary>
		/// <returns>親 GameObject の transform.</returns>
		public static Transform TryGetParent(this Component self)
		{
			return self ? self.GetParent() : null;
		}

		/// <summary>
		/// 自身を含まない全ての親 GameObject の transform を返します.
		/// </summary>
		/// <param name="includeInactive">非アクティブ GameObject を含むかどうか.</param>
		/// <param name="includeSelf">自身を含むかどうか.</param>
		/// <returns>親 GameObject の transform 配列.</returns>
		public static Transform[] GetParents(this Component self, bool includeInactive = false, bool includeSelf = false)
		{
			// container
			List<Transform> list = new List<Transform>();
			// get
			self.GetComponentsInParent(includeInactive, list);
			// remove self
			if (!includeSelf) {
				list.RemoveAt(0);
			}
			// to array
			return list.ToArray();
		}
		/// <summary>
		/// 自身を含まない全ての親 GameObject の transform を返します. Unity 的に破棄済みの場合は空配列を返す.
		/// </summary>
		/// <param name="includeInactive">非アクティブ GameObject を含むかどうか.</param>
		/// <param name="includeSelf">自身を含むかどうか.</param>
		/// <returns>親 GameObject の transform 配列.</returns>
		public static Transform[] TryGetParents(this Component self, bool includeInactive = false, bool includeSelf = false)
		{
			return self ? self.GetParents(includeInactive, includeSelf) : new Transform[0];
		}

		/// <summary>
		/// 親 GameObject を返します.
		/// </summary>
		/// <returns>親 GameObject.</returns>
		public static GameObject GetParentGameObject(this Component self)
		{
			Transform tf = self.transform.parent;
			return tf ? tf.gameObject : null;
		}
		/// <summary>
		/// 親 GameObject を返します. Unity 的に破棄済みの場合は null を返す.
		/// </summary>
		/// <returns>親 GameObject.</returns>
		public static GameObject TryGetParentGameObject(this Component self)
		{
			return self ? self.GetParentGameObject() : null;
		}

		/// <summary>
		/// 自身を含まない全ての親 GameObject を返します.
		/// </summary>
		/// <param name="includeInactive">非アクティブ GameObject を含むかどうか.</param>
		/// <param name="includeSelf">自身を含むかどうか.</param>
		/// <returns>親 GameObject 配列.</returns>
		public static GameObject[] GetParentGameObjects(this Component self, bool includeInactive = false, bool includeSelf = false)
		{
			return Array.ConvertAll(GetParents(self, includeInactive, includeSelf), tf => tf.gameObject);
		}
		/// <summary>
		/// 自身を含まない全ての親 GameObject を返します. Unity 的に破棄済みの場合は空配列を返す.
		/// </summary>
		/// <param name="includeInactive">非アクティブ GameObject を含むかどうか.</param>
		/// <param name="includeSelf">自身を含むかどうか.</param>
		/// <returns>親 GameObject 配列.</returns>
		public static GameObject[] TryGetParentGameObjects(this Component self, bool includeInactive = false, bool includeSelf = false)
		{
			return self ? self.GetParentGameObjects() : new GameObject[0];
		}

		/// <summary>
		/// 親を設定します.
		/// </summary>
		/// <param name="parent">親 GameObject.</param>
		/// <param name="worldTransformStays">true: ワールド transform 固定, ローカル transform 変更. false: ローカル transform 固定, ワールド transform 変更.</param>
		public static void SetParent(this Component self, GameObject parent, bool worldPositionStays = true)
		{
			self.transform.SetParent(parent ? parent.transform : null, worldPositionStays);
		}
		/// <summary>
		/// 親を設定します.
		/// </summary>
		/// <param name="parent">親 GameObject.</param>
		/// <param name="worldTransformStays">true: ワールド transform 固定, ローカル transform 変更. false: ローカル transform 固定, ワールド transform 変更.</param>
		public static void TrySetParent(this Component self, GameObject parent, bool worldPositionStays = true)
		{
			if (self) {
				self.SetParent(parent, worldPositionStays);
			}
		}

		/// <summary>
		/// 親を設定します.
		/// </summary>
		/// <param name="parent">親 Component.</param>
		/// <param name="worldTransformStays">true: ワールド transform 固定, ローカル transform 変更. false: ローカル transform 固定, ワールド transform 変更.</param>
		public static void SetParent(this Component self, Component parent, bool worldPositionStays = true)
		{
			if (self) {
				self.transform.SetParent(parent ? parent.transform : null, worldPositionStays);
			}
		}
		/// <summary>
		/// 親を設定します.
		/// </summary>
		/// <param name="parent">親 Component.</param>
		/// <param name="worldTransformStays">true: ワールド transform 固定, ローカル transform 変更. false: ローカル transform 固定, ワールド transform 変更.</param>
		public static void TrySetParent(this Component self, Component parent, bool worldPositionStays = true)
		{
			if (self) {
				self.SetParent(parent, worldPositionStays);
			}
		}
		#endregion

		#region Root
		/// <summary>
		/// ルートとなる GameObject の transform を返します.
		/// </summary>
		/// <returns>ルートとなる GameObject の transform.</returns>
		public static Transform GetRoot(this Component self)
		{
			return self.transform.root;
		}
		/// <summary>
		/// ルートとなる GameObject の transform を返します. Unity 的に破棄済みの場合は null を返す.
		/// </summary>
		/// <returns>ルートとなる GameObject の transform.</returns>
		public static Transform TryGetRoot(this Component self)
		{
			return self ? self.GetRoot() : null;
		}

		/// <summary>
		/// ルートとなる GameObject を返します.
		/// </summary>
		/// <returns>ルートとなる GameObject.</returns>
		public static GameObject GetRootGameObject(this Component self)
		{
			Transform tf = self.transform.root;
			return tf ? tf.gameObject : null;
		}
		/// <summary>
		/// ルートとなる GameObject を返します. Unity 的に破棄済みの場合は null を返す.
		/// </summary>
		/// <returns>ルートとなる GameObject.</returns>
		public static GameObject TryGetRootGameObject(this Component self)
		{
			return self ? self.GetRootGameObject() : null;
		}

		/// <summary>
		/// ルートとなるオブジェクトからのパスを返します.
		/// </summary>
		/// <returns>パス</returns>
		public static string GetRootPath(this Component self)
		{
			Transform[] parents = GetParents(self, true, true);
			if (parents.Length <= 0) {
				return string.Empty;
			}

			string[] parentNames = Array.ConvertAll(parents, n => n.name);

			Array.Reverse(parentNames);

			return string.Join("/", parentNames);
		}
		/// <summary>
		/// ルートとなるオブジェクトからのパスを返します. Unity 的に破棄済みの場合は空文字列を返す.
		/// </summary>
		/// <returns>パス</returns>
		public static string TryGetRootPath(this Component self)
		{
			return self ? self.GetRootPath() : string.Empty;
		}
		#endregion

		#region Child
		/// <summary>
		/// 子オブジェクトが存在するかどうか.
		/// </summary>
		/// <returns>存在する場合は true.</returns>
		public static bool HasChild(this Component self)
		{
			return (self.transform.childCount > 0);
		}
		/// <summary>
		/// 子オブジェクトが存在するかどうか.
		/// </summary>
		/// <returns>存在する場合は true.</returns>
		public static bool TryHasChild(this Component self)
		{
			return self ? self.HasChild() : false;
		}

		/// <summary>
		/// 指定されたインデックスの子 Transform を返します.
		/// </summary>
		/// <param name="index">インデックス.</param>
		/// <returns>子 Transform.</returns>
		public static Transform GetChild(this Component self, int index)
		{
			return self.transform.GetChild(index);
		}
		/// <summary>
		/// 指定されたインデックスの子 Transform を返します. Unity 的に破棄済みの場合は null を返す.
		/// </summary>
		/// <param name="index">インデックス.</param>
		/// <returns>子 Transform.</returns>
		public static Transform TryGetChild(this Component self, int index)
		{
			return self ? self.GetChild(index) : null;
		}

		/// <summary>
		/// 指定されたインデックスの子 GameObject を返します.
		/// </summary>
		/// <param name="index">インデックス.</param>
		/// <returns>子 GameObject.</returns>
		public static GameObject GetChildGameObject(this Component self, int index)
		{
			Transform tf = self.transform.GetChild(index);
			return tf ? tf.gameObject : null;
		}
		/// <summary>
		/// 指定されたインデックスの子 GameObject を返します. Unity 的に破棄済みの場合は null を返す.
		/// </summary>
		/// <param name="index">インデックス.</param>
		/// <returns>子 GameObject.</returns>
		public static GameObject TryGetChildGameObject(this Component self, int index)
		{
			return self ? self.GetChildGameObject(index) : null;
		}

		/// <summary>
		/// 第一階層の全ての子供 Transform を取得します.
		/// </summary>
		/// <returns>子供 Transform 配列.</returns>
		public static Transform[] GetFirstLayerChildren(this Component self)
		{
			List<Transform> childrenList = new List<Transform>();

			foreach (Transform child in self.transform) {
				childrenList.Add(child);
			}

			return childrenList.ToArray();
		}
		/// <summary>
		/// 第一階層の全ての子供 Transform を取得します. Unity 的に破棄済みの場合は空配列を返す.
		/// </summary>
		/// <returns>子供 Transform 配列.</returns>
		public static Transform[] TryGetFirstLayerChildren(this Component self)
		{
			return self ? self.GetFirstLayerChildren() : new Transform[0];
		}

		/// <summary>
		/// 第一階層の全ての子供 GameObject を取得します.
		/// </summary>
		/// <returns>子供 GameObject 配列.</returns>
		public static GameObject[] GetFirstLayerChildrenGameObjects(this Component self)
		{
			List<GameObject> childrenList = new List<GameObject>();

			foreach (Transform child in self.transform) {
				childrenList.Add(child.gameObject);
			}

			return childrenList.ToArray();
		}
		/// <summary>
		/// 第一階層の全ての子供 GameObject を取得します. Unity 的に破棄済みの場合は空配列を返す.
		/// </summary>
		/// <returns>子供 GameObject 配列.</returns>
		public static GameObject[] TryGetFirstLayerChildrenGameObjects(this Component self)
		{
			return self ? self.GetFirstLayerChildrenGameObjects() : new GameObject[0];
		}

		/// <summary>
		/// 自身を含まない全ての子孫 Transform を取得します.
		/// </summary>
		/// <param name="includeInactive">非アクティブな Child も取得するかどうか.</param>
		/// <param name="includeSelf">自身を含むかどうか.</param>
		/// <returns>子孫 Transform 配列.</returns>
		public static Transform[] GetChildren(this Component self, bool includeInactive = false, bool includeSelf = false)
		{
			// container
			List<Transform> list = new List<Transform>();
			// get
			self.GetComponentsInChildren(includeInactive, list);
			// remove self
			if (!includeSelf) {
				list.RemoveAt(0);
			}
			// to array
			return list.ToArray();
		}
		/// <summary>
		/// 自身を含まない全ての子孫 Transform を取得します. Unity 的に破棄済みの場合は空配列を返す.
		/// </summary>
		/// <param name="includeInactive">非アクティブな Child も取得するかどうか.</param>
		/// <param name="includeSelf">自身を含むかどうか.</param>
		/// <returns>子孫 Transform 配列.</returns>
		public static Transform[] TryGetChildren(this Component self, bool includeInactive = false, bool includeSelf = false)
		{
			return self ? self.GetChildren(includeInactive, includeSelf) : new Transform[0];
		}

		/// <summary>
		/// 全ての子孫 GameObject を取得します.
		/// </summary>
		/// <param name="includeInactive">非アクティブな Child も取得するかどうか.</param>
		/// <param name="includeSelf">自身を含むかどうか.</param>
		/// <returns>子孫 GameObject 配列.</returns>
		public static GameObject[] GetChildrenGameObjects(this Component self, bool includeInactive = false, bool includeSelf = false)
		{
			return Array.ConvertAll(GetChildren(self, includeInactive, includeSelf), tf => tf.gameObject);
		}
		/// <summary>
		/// 全ての子孫 GameObject を取得します. Unity 的に破棄済みの場合は空配列を返す.
		/// </summary>
		/// <param name="includeInactive">非アクティブな Child も取得するかどうか.</param>
		/// <param name="includeSelf">自身を含むかどうか.</param>
		/// <returns>子孫 GameObject 配列.</returns>
		public static GameObject[] TryGetChildrenGameObjects(this Component self, bool includeInactive = false, bool includeSelf = false)
		{
			return self ? self.GetChildrenGameObjects(includeInactive, includeSelf) : new GameObject[0];
		}

		/// <summary>
		/// 自身を含まない全ての子孫を破棄します.
		/// </summary>
		public static void DestroyChildren(this Component self)
		{
			foreach (GameObject child in GetChildrenGameObjects(self, true, false)) {
				GameObject.Destroy(child);
			}
		}
		/// <summary>
		/// 自身を含まない全ての子孫を破棄します.
		/// </summary>
		public static void TryDestroyChildren(this Component self)
		{
			if (self) {
				self.DestroyChildren();
			}
		}

		/// <summary>
		/// 直ちに自身を含まない全ての子孫を破棄します.
		/// </summary>
		public static void DestroyChildrenImmediate(this Component self)
		{
			foreach (GameObject child in GetChildrenGameObjects(self, true, false)) {
				GameObject.DestroyImmediate(child);
			}
		}
		/// <summary>
		/// 直ちに自身を含まない全ての子孫を破棄します.
		/// </summary>
		public static void TryDestroyChildrenImmediate(this Component self)
		{
			if (self) {
				self.DestroyChildrenImmediate();
			}
		}
		#endregion

		#region Hierarchy Tree Traversal
		/// <summary>
		/// DFS ( 深さ優先探索 ) 方式で自身を含む全ての子孫を呼び出します.
		/// </summary>
		/// <param name="action">呼び出しデリゲート.</param>
		/// <param name="traversePreOrder">Pre-Order ( 前順 ) を使うかどうか.</param>
		public static void TraverseDepthFirst(this Component self, Action<GameObject> action, bool traversePreOrder = true)
		{
			HierarchyUtils.DepthFirstTraversal(self.transform, tf => action.Call(tf.gameObject), traversePreOrder);
		}
		/// <summary>
		/// DFS ( 深さ優先探索 ) 方式で自身を含む全ての子孫を呼び出します.
		/// </summary>
		/// <param name="action">呼び出しデリゲート.</param>
		/// <param name="traversePreOrder">Pre-Order ( 前順 ) を使うかどうか.</param>
		public static void TryTraverseDepthFirst(this Component self, Action<GameObject> action, bool traversePreOrder = true)
		{
			if (self) {
				self.TraverseDepthFirst(action, traversePreOrder);
			}
		}

		/// <summary>
		/// BFS ( 幅優先探索 ) 方式で自身を含む全ての子孫を呼び出します.
		/// </summary>
		/// <param name="action">呼び出しデリゲート.</param>
		public static void TraverseBreadthFirst(this Component self, Action<GameObject> action)
		{
			HierarchyUtils.BreadthFirstTraversal(self.transform, tf => action.Call(tf.gameObject));
		}
		/// <summary>
		/// BFS ( 幅優先探索 ) 方式で自身を含む全ての子孫を呼び出します.
		/// </summary>
		/// <param name="action">呼び出しデリゲート.</param>
		public static void TryTraverseBreadthFirst(this Component self, Action<GameObject> action)
		{
			if (self) {
				self.TraverseBreadthFirst(action);
			}
		}
		#endregion

		#endregion
	}
}
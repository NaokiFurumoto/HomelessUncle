/*
 * transform の操作は非推奨. 重いだから.
 */
//#define ENABLE_TRANSFORM_EXTENSIONS
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
	/// GameObject Extensions
	/// </summary>
	public static class GameObjectExtensions
	{
		#region Active
		/// <summary>
		/// オブジェクトのアクティブ状態を設定する.
		/// </summary>
		/// <param name="isActive">アクティブ状態.</param>
		public static void TrySetActive(this GameObject self, bool isActive)
		{
			if (self) {
				self.SetActive(isActive);
			}
		}

		/// <summary>
		/// 自身アクティブ状態取得を試します. Unity 的に破棄済みの場合は false を返す.
		/// </summary>
		/// <returns>GameObject 存在, 且つ Active 状態なら true.</returns>
		public static bool TryGetActiveSelf(this GameObject self)
		{
			return self ? self.activeSelf : false;
		}

		/// <summary>
		/// 継承アクティブ状態取得を試します. Unity 的に破棄済みの場合は false を返す.
		/// </summary>
		/// <returns>GameObject 存在, 且つ Active 状態なら true.</returns>
		public static bool TryGetActiveInHierarchy(this GameObject self)
		{
			return self ? self.activeInHierarchy : false;
		}

		/// <summary>
		/// オブジェクトのアクティブ状態を切り替える. アクティブ状態の変更したかどうかを返す.
		/// </summary>
		/// <param name="isActive">アクティブ状態.</param>
		public static bool ChangeActive(this GameObject self, bool isActive)
		{
			if (self.activeSelf != isActive) {
				self.SetActive(isActive);
				return true;
			}
			return false;
		}

		/// <summary>
		/// オブジェクトのアクティブ状態を切り替える. Unity 的に破棄済み, またはアクティブ状態の変更がない場合は false を返す; それ以外は true を返す.
		/// </summary>
		/// <param name="isActive">アクティブ状態.</param>
		public static bool TryChangeActive(this GameObject self, bool isActive)
		{
			if (self && self.activeSelf != isActive) {
				self.SetActive(isActive);
				return true;
			}
			return false;
		}
		#endregion

		/*
		 *
		 *
		 * Try 系メソッドをこれ以上作りません.
		 *
		 * 作るなら ComponentExtensions を推奨します.
		 *
		 *
		 */

		#region Find
		/// <summary>
		/// 名前で子孫から Transform を探す.
		/// </summary>
		/// <param name="self">Component 自身.</param>
		/// <param name="name">名前.</param>
		/// <returns>Transform.</returns>
		public static Transform FindTransform(this GameObject self, string name)
		{
			foreach (Transform tf in self.GetChildren(true, true)) {
				if (tf.name == name) {
					return tf;
				}
			}
			return null;
		}

		/// <summary>
		/// FindTransform の GameObject 版.
		/// </summary>
		/// <param name="self">Component 自身.</param>
		/// <param name="name">名前.</param>
		/// <returns>GameObject.</returns>
		public static GameObject FindGameObject(this GameObject self, string name)
		{
			Transform tf = self.FindTransform(name);
			return tf ? tf.gameObject : null;
		}

		/// <summary>
		/// FindTransform の Component 版.
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <param name="self">Component 自身.</param>
		/// <param name="name">名前.</param>
		/// <returns>Component.</returns>
		public static T FindComponent<T>(this GameObject self, string name) where T : Component
		{
			Transform tf = self.FindTransform(name);
			return tf ? tf.GetComponent<T>() : null;
		}
		#endregion

		#region DemandComponent
		/// <summary>
		/// Component を取得します. アタッチされていない場合は追加してから取得します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="type">Component の型.</param>
		/// <returns>取得した Component.</returns>
		public static Component DemandComponent(this GameObject self, Type type)
		{
			Component component = self.GetComponent(type);
			if (!component) {
				component = self.AddComponent(type);
			}
			return component;
		}

		/// <summary>
		/// Component を取得します. アタッチされていない場合は追加してから取得します.
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>取得した Component.</returns>
		public static T DemandComponent<T>(this GameObject self) where T : Component
		{
			T component = self.GetComponent<T>();
			if (!component) {
				component = self.AddComponent<T>();
			}
			return component;
		}
		#endregion

		#region GetRectTransform
		/// <summary>
		/// RectTransform を取得します. アタッチされていない場合は null を返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>取得した RectTransform.</returns>
		public static RectTransform GetRectTransform(this GameObject self)
		{
			return self.GetComponent<RectTransform>();
		}

		/// <summary>
		/// RectTransform を取得します. アタッチされていない場合は追加してから取得します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>取得した RectTransform.</returns>
		public static RectTransform DemandRectTransform(this GameObject self)
		{
			RectTransform component = self.GetComponent<RectTransform>();
			if (!component) {
				component = self.AddComponent<RectTransform>();
			}
			return component;
		}
		#endregion

		#region GetComponentsInChildrenWithoutSelf
		/// <summary>
		/// 自身を含まない GetComponentsInChildren().
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <param name="includeInactive">非アクティブな Child も取得するかどうか.</param>
		/// <returns>取得した Component 配列.</returns>
		public static Component[] GetComponentsInChildrenWithoutSelf(this GameObject self, Type type, bool includeInactive = false)
		{
			List<Component> list = new List<Component>(self.GetComponentsInChildren(type, includeInactive));
			list.RemoveAll(n => n.gameObject == self);
			return list.ToArray();
		}

		/// <summary>
		/// 自身を含まない GetComponentsInChildren().
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <param name="includeInactive">非アクティブな Child も取得するかどうか.</param>
		/// <returns>取得した Component 配列.</returns>
		public static T[] GetComponentsInChildrenWithoutSelf<T>(this GameObject self, bool includeInactive = false) where T : Component
		{
			List<T> list = new List<T>();
			self.GetComponentsInChildren(includeInactive, list);
			list.RemoveAll(n => n.gameObject == self);
			return list.ToArray();
		}

		/// <summary>
		/// 自身を含まない GetComponentsInChildren().
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <param name="includeInactive">非アクティブな Child も取得するかどうか.</param>
		/// <returns>取得した Component 配列.</returns>
		public static void GetComponentsInChildrenWithoutSelf<T>(this GameObject self, List<T> resultList, bool includeInactive = false) where T : Component
		{
			self.GetComponentsInChildren(includeInactive, resultList);
			resultList.RemoveAll(n => n.gameObject == self);
		}
		#endregion

		#region GetComponentsInParentWithoutSelf
		/// <summary>
		/// 自身を含まない GetComponentsInParent().
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <param name="includeInactive">非アクティブな Child も取得するかどうか.</param>
		/// <returns>取得した Component 配列.</returns>
		public static Component[] GetComponentsInParentWithoutSelf(this GameObject self, Type type, bool includeInactive = false)
		{
			List<Component> list = new List<Component>(self.GetComponentsInParent(type, includeInactive));
			list.RemoveAll(n => n.gameObject == self);
			return list.ToArray();
		}

		/// <summary>
		/// 自身を含まない GetComponentsInParent().
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <param name="includeInactive">非アクティブな Child も取得するかどうか.</param>
		/// <returns>取得した Component 配列.</returns>
		public static T[] GetComponentsInParentWithoutSelf<T>(this GameObject self, bool includeInactive = false) where T : Component
		{
			List<T> list = new List<T>();
			self.GetComponentsInParent(includeInactive, list);
			list.RemoveAll(n => n.gameObject == self);
			return list.ToArray();
		}

		/// <summary>
		/// 自身を含まない GetComponentsInParent().
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <param name="includeInactive">非アクティブな Child も取得するかどうか.</param>
		/// <returns>取得した Component 配列.</returns>
		public static void GetComponentsInParentWithoutSelf<T>(this GameObject self, List<T> resultList, bool includeInactive = false) where T : Component
		{
			self.GetComponentsInParent(includeInactive, resultList);
			resultList.RemoveAll(n => n.gameObject == self);
		}
		#endregion

		#region RemoveComponent
		/// <summary>
		/// Component を削除します.
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		public static void RemoveComponent<T>(this GameObject self) where T : Component
		{
			GameObject.Destroy(self.GetComponent<T>());
		}

		/// <summary>
		/// 直ちに Component を削除します.
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		public static void RemoveComponentImmediate<T>(this GameObject self) where T : Component
		{
			GameObject.DestroyImmediate(self.GetComponent<T>());
		}

		/// <summary>
		/// Component をすべて削除します.
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		public static void RemoveComponents<T>(this GameObject self) where T : Component
		{
			foreach (T component in self.GetComponents<T>()) {
				GameObject.Destroy(component);
			}
		}

		/// <summary>
		/// 直ちに Component をすべて削除します.
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		public static void RemoveComponentsImmediate<T>(this GameObject self) where T : Component
		{
			foreach (T component in self.GetComponents<T>()) {
				GameObject.DestroyImmediate(component);
			}
		}
		#endregion

		#region HasComponent

		/// <summary>
		/// 指定された Component を持っているかどうか.
		/// </summary>
		/// <typeparam name="T">Component の型.</typeparam>
		/// <param name="self">Component.</param>
		/// <returns>持っていたら true.</returns>
		public static bool HasComponent<T>(this GameObject self) where T : Component
		{
			return self.GetComponent<T>();
		}

		#endregion

		#region GetComponentInterface
		/// <summary>
		/// 指定 Interface を Component から取得します ( 最初に見付かったヤツ ).
		/// </summary>
		/// <typeparam name="T">Interface の型.</typeparam>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>Interface.</returns>
		public static T GetComponentInterface<T>(this GameObject self) where T : class
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
		/// <param name="self">GameObject 自身.</param>
		/// <returns>Interface 配列.</returns>
		public static T[] GetComponentInterfaces<T>(this GameObject self) where T : class
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
		/// <param name="self">GameObject 自身.</param>
		/// <param name="includeInactive">非アクティブ子孫も走査対象に含めるかどうか.</param>
		/// <returns>Interface.</returns>
		public static T GetComponentInterfaceInChildren<T>(this GameObject self, bool includeInactive) where T : class
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
		/// <param name="self">GameObject 自身.</param>
		/// <param name="includeInactive">非アクティブ子孫も走査対象に含めるかどうか.</param>
		/// <returns>Interface 配列.</returns>
		public static T[] GetComponentInterfacesInChildren<T>(this GameObject self, bool includeInactive) where T : class
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

#if ENABLE_TRANSFORM_EXTENSIONS
		#region Transform

#if ENABLE_WORLD_COORDINATE_EXTENSIONS
		#region transform.world
		/// <summary>
		/// ワールド Transform をリセットします.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		public static void ResetWorldTransform(this GameObject self)
		{
			self.transform.position = Vector3.zero;
			self.transform.eulerAngles = Vector3.zero;
			ResetWorldScale(self);
		}

		#region transform.world.position
		/// <summary>
		/// ワールド座標を(0, 0, 0)にリセットします.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		public static void ResetWorldPosition(this GameObject self)
		{
			self.transform.position = Vector3.zero;
		}

		/// <summary>
		/// ワールド座標を返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>ワールド座標.</returns>
		public static Vector3 GetWorldPosition(this GameObject self)
		{
			return self.transform.position;
		}

		/// <summary>
		/// ワールド X 座標を返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>ワールド X 座標.</returns>
		public static float GetWorldPositionX(this GameObject self)
		{
			return self.transform.position.x;
		}

		/// <summary>
		/// ワールド Y 座標を返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>ワールド Y 座標.</returns>
		public static float GetWorldPositionY(this GameObject self)
		{
			return self.transform.position.y;
		}

		/// <summary>
		/// ワールド Z 座標を返します。
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>ワールド Z 座標.</returns>
		public static float GetWorldPositionZ(this GameObject self)
		{
			return self.transform.position.z;
		}

		/// <summary>
		/// ワールド X 座標を設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="x">ワールド X 座標.</param>
		public static void SetWorldPositionX(this GameObject self, float x)
		{
			Vector3 pos = self.transform.position;
			pos.x = x;
			self.transform.position = pos;
		}

		/// <summary>
		/// ワールド Y 座標を設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="y">ワールド Y 座標.</param>
		public static void SetWorldPositionY(this GameObject self, float y)
		{
			Vector3 pos = self.transform.position;
			pos.y = y;
			self.transform.position = pos;
		}

		/// <summary>
		/// ワールド Z 座標を設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="z">ワールド Z 座標.</param>
		public static void SetWorldPositionZ(this GameObject self, float z)
		{
			Vector3 pos = self.transform.position;
			pos.z = z;
			self.transform.position = pos;
		}

		/// <summary>
		/// ワールド座標を設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="x">ワールド X 座標.</param>
		/// <param name="y">ワールド Y 座標.</param>
		public static void SetWorldPosition(this GameObject self, float x, float y)
		{
			Vector3 pos = self.transform.position;
			pos.x = x;
			pos.y = y;
			self.transform.position = pos;
		}

		/// <summary>
		/// ワールド座標を設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="x">ワールド X 座標.</param>
		/// <param name="y">ワールド Y 座標.</param>
		/// <param name="z">ワールド Z 座標.</param>
		public static void SetWorldPosition(this GameObject self, float x, float y, float z)
		{
			self.transform.position = new Vector3(x, y, z);
		}

		/// <summary>
		/// ワールド座標を設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="position">ワールド座標.</param>
		public static void SetWorldPosition(this GameObject self, Vector2 position)
		{
			self.transform.position = new Vector3(position.x, position.y, self.transform.position.z);
		}

		/// <summary>
		/// ワールド座標を設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="position">ワールド座標.</param>
		public static void SetWorldPosition(this GameObject self, Vector3 position)
		{
			self.transform.position = position;
		}

		/// <summary>
		/// ワールド X 座標に加算します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="x">加算値.</param>
		public static void AddWorldPositionX(this GameObject self, float x)
		{
			self.transform.position += new Vector3(x, 0, 0);
		}

		/// <summary>
		/// ワールド Y 座標に加算します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="y">加算値.</param>
		public static void AddWorldPositionY(this GameObject self, float y)
		{
			self.transform.position += new Vector3(0, y, 0);
		}

		/// <summary>
		/// ワールド Z 座標に加算します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="z">加算値.</param>
		public static void AddWorldPositionZ(this GameObject self, float z)
		{
			self.transform.position += new Vector3(0, 0, z);
		}

		/// <summary>
		/// ワールド座標を加算します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="x">ワールド X 座標に加算する値.</param>
		/// <param name="y">ワールド Y 座標に加算する値.</param>
		public static void AddWorldPosition(this GameObject self, float x, float y)
		{
			self.transform.position += new Vector3(x, y, 0);
		}

		/// <summary>
		/// ワールド座標を加算します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="x">ワールド X 座標に加算する値.</param>
		/// <param name="y">ワールド Y 座標に加算する値.</param>
		/// <param name="z">ワールド Z 座標に加算する値.</param>
		public static void AddWorldPosition(this GameObject self, float x, float y, float z)
		{
			self.transform.position += new Vector3(x, y, z);
		}

		/// <summary>
		/// ワールド座標を加算します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="v">加算値.</param>
		public static void AddWorldPosition(this GameObject self, Vector2 v)
		{
			self.transform.position += new Vector3(v.x, v.y);
		}

		/// <summary>
		/// ワールド座標を加算します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="v">加算値.</param>
		public static void AddWorldPosition(this GameObject self, Vector3 v)
		{
			self.transform.position += v;
		}
		#endregion

		#region transform.world.scale
		/// <summary>
		/// ワールドスケールを (1, 1, 1) にリセットします.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		public static void ResetWorldScale(this GameObject self)
		{
			SetWorldScale(self, Vector3.one);
		}

		/// <summary>
		/// ワールドスケールを返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>ワールドスケール.</returns>
		public static Vector3 GetWorldScale(this GameObject self)
		{
			return self.transform.lossyScale;
		}

		/// <summary>
		/// ワールド X スケールを返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>ワールド X スケール.</returns>
		public static float GetWorldScaleX(this GameObject self)
		{
			return self.transform.lossyScale.x;
		}

		/// <summary>
		/// ワールド Y スケールを返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>ワールド Y スケール.</returns>
		public static float GetWorldScaleY(this GameObject self)
		{
			return self.transform.lossyScale.y;
		}

		/// <summary>
		/// ワールド Z スケールを返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>ワールド Z スケール</returns>
		public static float GetWorldScaleZ(this GameObject self)
		{
			return self.transform.lossyScale.z;
		}

		/// <summary>
		/// ワールドスケールを設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="scale">ワールドスケール.</param>
		public static void SetWorldScale(this GameObject self, Vector3 scale)
		{
			Vector3 localScale = self.transform.localScale;

			Transform t = self.transform.parent;
			if (t) {
				localScale.x = (t.lossyScale.x != 0) ? (scale.x / t.lossyScale.x) : 1;
				localScale.y = (t.lossyScale.y != 0) ? (scale.y / t.lossyScale.y) : 1;
				localScale.z = (t.lossyScale.z != 0) ? (scale.z / t.lossyScale.z) : 1;
				self.transform.localScale = localScale;
				return;
			}

			self.transform.localScale = scale;
		}
		#endregion

		#region transform.world.eulerAngles
		/// <summary>
		/// ワールド回転角を(0, 0, 0)にリセットします.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		public static void ResetWorldEulerAngles(this GameObject self)
		{
			self.transform.eulerAngles = Vector3.zero;
		}

		/// <summary>
		/// ワールド回転角を返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>ワールド回転角.</returns>
		public static Vector3 GetWorldEulerAngles(this GameObject self)
		{
			return self.transform.eulerAngles;
		}

		/// <summary>
		/// ワールド X 軸方向の回転角を返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>ワールド X 回転角.</returns>
		public static float GetWorldEulerAngleX(this GameObject self)
		{
			return self.transform.eulerAngles.x;
		}

		/// <summary>
		/// ワールド Y 軸方向の回転角を返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>ワールド Y 回転角.</returns>
		public static float GetWorldEulerAngleY(this GameObject self)
		{
			return self.transform.eulerAngles.y;
		}

		/// <summary>
		/// ワールド Z 軸方向の回転角を返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>ワールド Z 回転角.</returns>
		public static float GetWorldEulerAngleZ(this GameObject self)
		{
			return self.transform.eulerAngles.z;
		}

		/// <summary>
		/// ワールド X 軸方向の回転角を設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="x">回転角.</param>
		public static void SetWorldEulerAngleX(this GameObject self, float x)
		{
			Vector3 angles = self.transform.eulerAngles;
			angles.x = x;
			self.transform.eulerAngles = angles;
		}

		/// <summary>
		/// ワールド Y 軸方向の回転角を設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="y">回転角.</param>
		public static void SetWorldEulerAngleY(this GameObject self, float y)
		{
			Vector3 angles = self.transform.eulerAngles;
			angles.y = y;
			self.transform.eulerAngles = angles;
		}

		/// <summary>
		/// ワールド Z 軸方向の回転角を設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="z">回転角.</param>
		public static void SetWorldEulerAngleZ(this GameObject self, float z)
		{
			Vector3 angles = self.transform.eulerAngles;
			angles.z = z;
			self.transform.eulerAngles = angles;
		}

		/// <summary>
		/// ワールド回転角を設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="angles">回転角.</param>
		public static void SetWorldEulerAngles(this GameObject self, Vector3 angles)
		{
			self.transform.eulerAngles = angles;
		}

		/// <summary>
		/// ワールド X 軸方向の回転角を加算します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="x">加算値.</param>
		public static void AddWorldEulerAngleX(this GameObject self, float x)
		{
			self.transform.Rotate(x, 0, 0, Space.World);
		}

		/// <summary>
		/// ワールド Y 軸方向の回転角を加算します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="y">加算値.</param>
		public static void AddWorldEulerAngleY(this GameObject self, float y)
		{
			self.transform.Rotate(0, y, 0, Space.World);
		}

		/// <summary>
		/// ワールド Z 軸方向の回転角を加算します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="z">加算値.</param>
		public static void AddWorldEulerAngleZ(this GameObject self, float z)
		{
			self.transform.Rotate(0, 0, z, Space.World);
		}

		/// <summary>
		/// ワールド回転角を加算します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="v">加算値.</param>
		public static void AddWorldEulerAngle(this GameObject self, Vector3 v)
		{
			self.transform.Rotate(v, Space.World);
		}
		#endregion

		#endregion
#endif

		#region transform.local
		/// <summary>
		/// ローカル Transform をリセットします.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		public static void ResetLocalTransform(this GameObject self)
		{
			Transform tf = self.transform;
			tf.localPosition = Vector3.zero;
			tf.localEulerAngles = Vector3.zero;
			tf.localScale = Vector3.one;
		}

		#region transform.local.position
		/// <summary>
		/// ローカル座標を(0, 0, 0)にリセットします.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		public static void ResetLocalPosition(this GameObject self)
		{
			self.transform.localPosition = Vector3.zero;
		}

		/// <summary>
		/// ローカル座標を返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>ローカル座標.</returns>
		public static Vector3 GetLocalPosition(this GameObject self)
		{
			return self.transform.localPosition;
		}

		/// <summary>
		/// ローカル座標系の X 座標を返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>ローカル X 座標.</returns>
		public static float GetLocalPositionX(this GameObject self)
		{
			return self.transform.localPosition.x;
		}

		/// <summary>
		/// ローカル座標系の Y 座標を返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>ローカル Y 座標.</returns>
		public static float GetLocalPositionY(this GameObject self)
		{
			return self.transform.localPosition.y;
		}

		/// <summary>
		/// ローカル座標系の Z 座標を返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>ローカル Z 座標.</returns>
		public static float GetLocalPositionZ(this GameObject self)
		{
			return self.transform.localPosition.z;
		}

		/// <summary>
		/// ローカル座標系の X 座標を設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="x">ローカル X 座標.</param>
		public static void SetLocalPositionX(this GameObject self, float x)
		{
			Vector3 pos = self.transform.localPosition;
			pos.x = x;
			self.transform.localPosition = pos;
		}

		/// <summary>
		/// ローカル座標系の Y 座標を設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="y">ローカル Y 座標.</param>
		public static void SetLocalPositionY(this GameObject self, float y)
		{
			Vector3 pos = self.transform.localPosition;
			pos.y = y;
			self.transform.localPosition = pos;
		}

		/// <summary>
		/// ローカル座標系の Z 座標を設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="z">ローカル Z 座標.</param>
		public static void SetLocalPositionZ(this GameObject self, float z)
		{
			Vector3 pos = self.transform.localPosition;
			pos.z = z;
			self.transform.localPosition = pos;
		}

		/// <summary>
		/// ローカル座標を設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="x">ローカル X 座標.</param>
		/// <param name="y">ローカル Y 座標.</param>
		public static void SetLocalPosition(this GameObject self, float x, float y)
		{
			Vector3 pos = self.transform.localPosition;
			pos.x = x;
			pos.y = y;
			self.transform.localPosition = pos;
		}

		/// <summary>
		/// ローカル座標を設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="x">ローカル X 座標.</param>
		/// <param name="y">ローカル Y 座標.</param>
		/// <param name="z">ローカル Z 座標.</param>
		public static void SetLocalPosition(this GameObject self, float x, float y, float z)
		{
			self.transform.localPosition = new Vector3(x, y, z);
		}

		/// <summary>
		/// ローカル座標を設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="position">ローカル座標.</param>
		public static void SetLocalPosition(this GameObject self, Vector2 position)
		{
			self.transform.localPosition = new Vector3(position.x, position.y, self.transform.localPosition.z);
		}

		/// <summary>
		/// ローカル座標を設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="position">ローカル座標.</param>
		public static void SetLocalPosition(this GameObject self, Vector3 position)
		{
			self.transform.localPosition = position;
		}

		/// <summary>
		/// ローカルの X 座標に加算します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="x">加算値.</param>
		public static void AddLocalPositionX(this GameObject self, float x)
		{
			self.transform.localPosition += new Vector3(x, 0, 0);
		}

		/// <summary>
		/// ローカルの Y 座標に加算します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="y">加算値.</param>
		public static void AddLocalPositionY(this GameObject self, float y)
		{
			self.transform.localPosition += new Vector3(0, y, 0);
		}

		/// <summary>
		/// ローカルの Z 座標に加算します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="z">加算値.</param>
		public static void AddLocalPositionZ(this GameObject self, float z)
		{
			self.transform.localPosition += new Vector3(0, 0, z);
		}

		/// <summary>
		/// ローカル座標を加算します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="x">ローカル X 座標に加算する値.</param>
		/// <param name="y">ローカル Y 座標に加算する値.</param>
		public static void AddLocalPosition(this GameObject self, float x, float y)
		{
			self.transform.localPosition += new Vector3(x, y, 0);
		}

		/// <summary>
		/// ローカル座標を加算します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="x">ローカル X 座標に加算する値.</param>
		/// <param name="y">ローカル Y 座標に加算する値.</param>
		/// <param name="z">ローカル Z 座標に加算する値.</param>
		public static void AddLocalPosition(this GameObject self, float x, float y, float z)
		{
			self.transform.localPosition += new Vector3(x, y, z);
		}

		/// <summary>
		/// ローカル座標を加算します.
		/// </summary>
		/// <param name="v">加算値</param>
		public static void AddLocalPosition(this GameObject self, Vector2 v)
		{
			self.transform.localPosition += new Vector3(v.x, v.y);
		}

		/// <summary>
		/// ローカル座標を加算します.
		/// </summary>
		/// <param name="v">加算値</param>
		public static void AddLocalPosition(this GameObject self, Vector3 v)
		{
			self.transform.localPosition += v;
		}
		#endregion

		#region transform.local.scale
		/// <summary>
		/// ローカル座標系のスケールを(1, 1, 1)にリセットします.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		public static void ResetLocalScale(this GameObject self)
		{
			self.transform.localScale = Vector3.one;
		}

		/// <summary>
		/// ローカル座標系のスケールを返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>スケール</returns>
		public static Vector3 GetLocalScale(this GameObject self)
		{
			return self.transform.localScale;
		}

		/// <summary>
		/// X 軸方向のローカル座標系のスケールを返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>スケール.</returns>
		public static float GetLocalScaleX(this GameObject self)
		{
			return self.transform.localScale.x;
		}

		/// <summary>
		/// Y 軸方向のローカル座標系のスケールを返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>スケール.</returns>
		public static float GetLocalScaleY(this GameObject self)
		{
			return self.transform.localScale.y;
		}

		/// <summary>
		/// Z 軸方向のローカル座標系のスケールを返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>スケール.</returns>
		public static float GetLocalScaleZ(this GameObject self)
		{
			return self.transform.localScale.z;
		}

		/// <summary>
		/// X 軸方向のローカル座標系のスケールを設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="x">スケール.</param>
		public static void SetLocalScaleX(this GameObject self, float x)
		{
			Vector3 v = self.transform.localScale;
			v.x = x;
			self.transform.localScale = v;
		}

		/// <summary>
		/// Y 軸方向のローカル座標系のスケールを設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="y">スケール.</param>
		public static void SetLocalScaleY(this GameObject self, float y)
		{
			Vector3 v = self.transform.localScale;
			v.y = y;
			self.transform.localScale = v;
		}

		/// <summary>
		/// Z 軸方向のローカル座標系のスケールを設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="z">スケール.</param>
		public static void SetLocalScaleZ(this GameObject self, float z)
		{
			Vector3 v = self.transform.localScale;
			v.z = z;
			self.transform.localScale = v;
		}

		/// <summary>
		/// ローカル座標系のスケールを設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="x">ローカル X スケール.</param>
		/// <param name="y">ローカル Y スケール.</param>
		public static void SetLocalScale(this GameObject self, float x, float y)
		{
			Vector3 scale = self.transform.localScale;
			scale.x = x;
			scale.y = y;
			self.transform.localScale = scale;
		}

		/// <summary>
		/// ローカル座標系のスケールを設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="x">ローカル X スケール.</param>
		/// <param name="y">ローカル Y スケール.</param>
		/// <param name="z">ローカル Z スケール.</param>
		public static void SetLocalScale(this GameObject self, float x, float y, float z)
		{
			self.transform.localScale = new Vector3(x, y, z);
		}

		/// <summary>
		/// ローカル座標系のスケールを設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="scale">スケール.</param>
		public static void SetLocalScale(this GameObject self, Vector2 scale)
		{
			self.transform.localScale = new Vector3(scale.x, scale.y, self.transform.localScale.z);
		}

		/// <summary>
		/// ローカル座標系のスケールを設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="scale">スケール.</param>
		public static void SetLocalScale(this GameObject self, Vector3 scale)
		{
			self.transform.localScale = scale;
		}

		/// <summary>
		/// X 軸方向のローカル座標系のスケールを加算します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="x">加算値.</param>
		public static void AddLocalScaleX(this GameObject self, float x)
		{
			self.transform.localScale += new Vector3(x, 0, 0);
		}

		/// <summary>
		/// Y 軸方向のローカル座標系のスケールを加算します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="y">加算値.</param>
		public static void AddLocalScaleY(this GameObject self, float y)
		{
			self.transform.localScale += new Vector3(0, y, 0);
		}

		/// <summary>
		/// Z 軸方向のローカル座標系のスケールを加算します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="z">加算値.</param>
		public static void AddLocalScaleZ(this GameObject self, float z)
		{
			self.transform.localScale += new Vector3(0, 0, z);
		}

		/// <summary>
		/// ローカル座標系のスケールを加算します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="x">ローカル X スケールに加算する値.</param>
		/// <param name="y">ローカル Y スケールに加算する値.</param>
		public static void AddLocalScale(this GameObject self, float x, float y)
		{
			self.transform.localScale = new Vector3(x, y, 0);
		}

		/// <summary>
		/// ローカル座標系のスケールを加算します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="x">ローカル X スケールに加算する値.</param>
		/// <param name="y">ローカル Y スケールに加算する値.</param>
		/// <param name="z">ローカル Z スケールに加算する値.</param>
		public static void AddLocalScale(this GameObject self, float x, float y, float z)
		{
			self.transform.localScale = new Vector3(x, y, z);
		}

		/// <summary>
		/// ローカル座標系のスケールを加算します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="v">加算値.</param>
		public static void AddLocalScale(this GameObject self, Vector2 v)
		{
			self.transform.localScale += new Vector3(v.x, v.y);
		}

		/// <summary>
		/// ローカル座標系のスケールを加算します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="v">加算値.</param>
		public static void AddLocalScale(this GameObject self, Vector3 v)
		{
			self.transform.localScale += v;
		}
		#endregion

		#region transform.local.eulerAngles
		/// <summary>
		/// ローカルの回転角を(0, 0, 0)にリセットします.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		public static void ResetLocalEulerAngles(this GameObject self)
		{
			self.transform.localEulerAngles = Vector3.zero;
		}

		/// <summary>
		/// ローカルの回転角を返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>回転角.</returns>
		public static Vector3 GetLocalEulerAngles(this GameObject self)
		{
			return self.transform.localEulerAngles;
		}

		/// <summary>
		/// ローカルの X 軸方向の回転角を返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>回転角.</returns>
		public static float GetLocalEulerAngleX(this GameObject self)
		{
			return self.transform.localEulerAngles.x;
		}

		/// <summary>
		/// ローカルの Y 軸方向の回転角を返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>回転角.</returns>
		public static float GetLocalEulerAngleY(this GameObject self)
		{
			return self.transform.localEulerAngles.y;
		}

		/// <summary>
		/// ローカルの Z 軸方向の回転角を返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>回転角.</returns>
		public static float GetLocalEulerAngleZ(this GameObject self)
		{
			return self.transform.localEulerAngles.z;
		}

		/// <summary>
		/// ローカルの X 軸方向の回転角を設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="x">回転角.</param>
		public static void SetLocalEulerAngleX(this GameObject self, float x)
		{
			Vector3 angles = self.transform.localEulerAngles;
			angles.x = x;
			self.transform.localEulerAngles = angles;
		}

		/// <summary>
		/// ローカルの Y 軸方向の回転角を設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="y">回転角.</param>
		public static void SetLocalEulerAngleY(this GameObject self, float y)
		{
			Vector3 angles = self.transform.localEulerAngles;
			angles.y = y;
			self.transform.localEulerAngles = angles;
		}

		/// <summary>
		/// ローカルの Z 軸方向の回転角を設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="z">回転角.</param>
		public static void SetLocalEulerAngleZ(this GameObject self, float z)
		{
			Vector3 angles = self.transform.localEulerAngles;
			angles.z = z;
			self.transform.localEulerAngles = angles;
		}

		/// <summary>
		/// ローカルの回転角を設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="angles">回転角.</param>
		public static void SetLocalEulerAngle(this GameObject self, Vector3 angles)
		{
			self.transform.localEulerAngles = angles;
		}

		/// <summary>
		/// ローカルの X 軸方向の回転角を加算します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="x">加算値.</param>
		public static void AddLocalEulerAngleX(this GameObject self, float x)
		{
			self.transform.Rotate(x, 0, 0, Space.Self);
		}

		/// <summary>
		/// ローカルの Y 軸方向の回転角を加算します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="y">加算値.</param>
		public static void AddLocalEulerAngleY(this GameObject self, float y)
		{
			self.transform.Rotate(0, y, 0, Space.Self);
		}

		/// <summary>
		/// ローカルの Z 軸方向の回転角を加算します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="z">加算値.</param>
		public static void AddLocalEulerAngleZ(this GameObject self, float z)
		{
			self.transform.Rotate(0, 0, z, Space.Self);
		}

		/// <summary>
		/// ローカル回転角を加算します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="v">加算値.</param>
		public static void AddLocalEulerAngleZ(this GameObject self, Vector3 v)
		{
			self.transform.Rotate(v, Space.Self);
		}
		#endregion

		#endregion

		#region transform.LookAt
		/// <summary>
		/// 向きを変更します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="target">目標 GameObject.</param>
		public static void LookAt(this GameObject self, GameObject target)
		{
			self.transform.LookAt(target.transform);
		}

		/// <summary>
		/// 向きを変更します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="target">目標 GameObject.</param>
		/// <param name="worldUp">上方ベクトル.</param>
		public static void LookAt(this GameObject self, GameObject target, Vector3 worldUp)
		{
			self.transform.LookAt(target.transform, worldUp);
		}

		/// <summary>
		/// 向きを変更します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="target">目標 Transform.</param>
		public static void LookAt(this GameObject self, Transform target)
		{
			self.transform.LookAt(target);
		}

		/// <summary>
		/// 向きを変更します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="target">目標 Transform.</param>
		/// <param name="worldUp">上方ベクトル.</param>
		public static void LookAt(this GameObject self, Transform target, Vector3 worldUp)
		{
			self.transform.LookAt(target, worldUp);
		}

		/// <summary>
		/// 向きを変更します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="worldPosition">目標座標.</param>
		public static void LookAt(this GameObject self, Vector3 worldPosition)
		{
			self.transform.LookAt(worldPosition);
		}

		/// <summary>
		/// 向きを変更します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="worldPosition">目標座標.</param>
		/// <param name="worldUp">上方ベクトル.</param>
		public static void LookAt(this GameObject self, Vector3 worldPosition, Vector3 worldUp)
		{
			self.transform.LookAt(worldPosition, worldUp);
		}
		#endregion

		#endregion
#endif

		#region Hierarchy

		#region Parent
		/// <summary>
		/// 親オブジェクトが存在するかどうか.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>存在する場合は true.</returns>
		public static bool HasParent(this GameObject self)
		{
			return self.transform.parent;
		}

		/// <summary>
		/// 親 GameObject の transform を返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>親 GameObject の transform.</returns>
		public static Transform GetParent(this GameObject self)
		{
			return self.transform.parent;
		}

		/// <summary>
		/// 自身を含まない全ての親 GameObject の transform を返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="includeInactive">非アクティブ GameObject を含むかどうか.</param>
		/// <param name="includeSelf">自身を含むかどうか.</param>
		/// <returns>親 GameObject の transform 配列.</returns>
		public static Transform[] GetParents(this GameObject self, bool includeInactive = false, bool includeSelf = false)
		{
			List<Transform> list = new List<Transform>();

			self.GetComponentsInParent(includeInactive, list);

			if (!includeSelf) {
				list.RemoveAt(0);
			}

			return list.ToArray();
		}

		/// <summary>
		/// 親 GameObject を返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>親 GameObject.</returns>
		public static GameObject GetParentGameObject(this GameObject self)
		{
			Transform tf = self.transform.parent;
			return tf ? tf.gameObject : null;
		}

		/// <summary>
		/// 自身を含まない全ての親 GameObject を返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="includeInactive">非アクティブ GameObject を含むかどうか.</param>
		/// <param name="includeSelf">自身を含むかどうか.</param>
		/// <returns>親 GameObject 配列.</returns>
		public static GameObject[] GetParentGameObjects(this GameObject self, bool includeInactive = false, bool includeSelf = false)
		{
			return Array.ConvertAll(GetParents(self, includeInactive, includeSelf), tf => tf.gameObject);
		}

		/// <summary>
		/// 親を設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="parent">親 GameObject.</param>
		/// <param name="worldTransformStays">true: ワールド transform 固定, ローカル transform 変更. false: ローカル transform 固定, ワールド transform 変更.</param>
		public static void SetParent(this GameObject self, GameObject parent, bool worldPositionStays = true)
		{
			self.transform.SetParent(parent ? parent.transform : null, worldPositionStays);
		}

		/// <summary>
		/// 親を設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="parent">親 Component.</param>
		/// <param name="worldTransformStays">true: ワールド transform 固定, ローカル transform 変更. false: ローカル transform 固定, ワールド transform 変更.</param>
		public static void SetParent(this GameObject self, Component parent, bool worldPositionStays = true)
		{
			self.transform.SetParent(parent ? parent.transform : null, worldPositionStays);
		}
		#endregion

		#region Root
		/// <summary>
		/// ルートとなる GameObject の transform を返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>ルートとなる GameObject の transform.</returns>
		public static Transform GetRoot(this GameObject self)
		{
			return self.transform.root;
		}

		/// <summary>
		/// ルートとなる GameObject を返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>ルートとなる GameObject.</returns>
		public static GameObject GetRootGameObject(this GameObject self)
		{
			Transform tf = self.transform.root;
			return tf ? tf.gameObject : null;
		}

		/// <summary>
		/// ルートとなるオブジェクトからのパスを返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>パス</returns>
		public static string GetRootPath(this GameObject self)
		{
			string[] parentNames = Array.ConvertAll(self.GetParents(true, true), n => n.name);

			Array.Reverse(parentNames);

			return string.Join("/", parentNames);
		}
		#endregion

		#region Child
		/// <summary>
		/// 子オブジェクトが存在するかどうか.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <returns>存在する場合は true.</returns>
		public static bool HasChild(this GameObject self)
		{
			return self.transform.childCount > 0;
		}

		/// <summary>
		/// 指定されたインデックスの子 Transform を返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="index">インデックス.</param>
		/// <returns>子 Transform.</returns>
		public static Transform GetChild(this GameObject self, int index)
		{
			return self.transform.GetChild(index);
		}

		/// <summary>
		/// 指定されたインデックスの子 GameObject を返します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="index">インデックス.</param>
		/// <returns>子 GameObject.</returns>
		public static GameObject GetChildGameObject(this GameObject self, int index)
		{
			Transform tf = self.transform.GetChild(index);
			return tf ? tf.gameObject : null;
		}

		/// <summary>
		/// 第一階層の全ての子供 Transform を取得します.
		/// </summary>
		/// <returns>子供 Transform 配列.</returns>
		public static Transform[] GetFirstLayerChildren(this GameObject self)
		{
			List<Transform> childrenList = new List<Transform>();

			foreach (Transform child in self.transform) {
				childrenList.Add(child);
			}

			return childrenList.ToArray();
		}

		/// <summary>
		/// 第一階層の全ての子供 GameObject を取得します.
		/// </summary>
		/// <returns>子供 GameObject 配列.</returns>
		public static GameObject[] GetFirstLayerChildrenGameObjects(this GameObject self)
		{
			List<GameObject> childrenList = new List<GameObject>();

			foreach (Transform child in self.transform) {
				childrenList.Add(child.gameObject);
			}

			return childrenList.ToArray();
		}

		/// <summary>
		/// 自身を含まない全ての子孫 Transform を取得します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="includeInactive">非アクティブな Child も取得するかどうか.</param>
		/// <param name="includeSelf">自身を含むかどうか.</param>
		/// <returns>子孫 Transform 配列.</returns>
		public static Transform[] GetChildren(this GameObject self, bool includeInactive = false, bool includeSelf = false)
		{
			if (includeSelf)
			{
				return self.GetComponentsInChildren<Transform>(includeInactive);
			}

			List<Transform> list = new List<Transform>();

			self.GetComponentsInChildren(includeInactive, list);

			if (!includeSelf) {
				list.RemoveAt(0);
			}

			return list.ToArray();
		}

		/// <summary>
		/// 全ての子孫 GameObject を取得します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="includeInactive">非アクティブな Child も取得するかどうか.</param>
		/// <param name="includeSelf">自身を含むかどうか.</param>
		/// <returns>子孫 GameObject 配列.</returns>
		public static GameObject[] GetChildrenGameObjects(this GameObject self, bool includeInactive = false, bool includeSelf = false)
		{
			return Array.ConvertAll(GetChildren(self, includeInactive, includeSelf), tf => tf.gameObject);
		}

		/// <summary>
		/// 自身を含まない全ての子孫を破棄します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		public static void DestroyChildren(this GameObject self)
		{
			foreach (GameObject child in GetChildrenGameObjects(self, true, false)) {
				GameObject.Destroy(child);
			}
		}

		/// <summary>
		/// 直ちに自身を含まない全ての子孫を破棄します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		public static void DestroyChildrenImmediate(this GameObject self)
		{
			foreach (GameObject child in GetChildrenGameObjects(self, true, false)) {
				GameObject.DestroyImmediate(child);
			}
		}
		#endregion

		#region Hierarchy Tree Traversal
		/// <summary>
		/// DFS ( 深さ優先探索 ) 方式で自身を含む全ての子孫を呼び出します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="action">呼び出しデリゲート.</param>
		/// <param name="traversePreOrder">Pre-Order ( 前順 ) を使うかどうか.</param>
		public static void TraverseDepthFirst(this GameObject self, Action<GameObject> action, bool traversePreOrder = true)
		{
			HierarchyUtils.DepthFirstTraversal(self.transform, tf => action.Call(tf.gameObject), traversePreOrder);
		}

		/// <summary>
		/// BFS ( 幅優先探索 ) 方式で自身を含む全ての子孫を呼び出します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="action">呼び出しデリゲート.</param>
		public static void TraverseBreadthFirst(this GameObject self, Action<GameObject> action)
		{
			HierarchyUtils.BreadthFirstTraversal(self.transform, tf => action.Call(tf.gameObject));
		}

		#endregion

		#endregion

		#region Layer
		/// <summary>
		/// レイヤーを設定します. 関数コールですので, 'self.layer = layer' を直下書く方が若干速い.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="layer">レイヤー.</param>
		public static void SetLayer(this GameObject self, int layer)
		{
			self.layer = layer;
		}

		/// <summary>
		/// レイヤー名を使用してレイヤーを設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="layerName">レイヤー名.</param>
		public static void SetLayer(this GameObject self, string layerName)
		{
			self.layer = LayerMask.NameToLayer(layerName);
		}

		/// <summary>
		/// レイヤーを指定 GameObject のレイヤーに設定します.
		/// </summary>
		/// <param name="targetGo">目標 GameObject.</param>
		public static void SetLayer(this GameObject self, GameObject targetGo)
		{
			if (targetGo) {
				self.layer = targetGo.layer;
			}
		}

		/// <summary>
		/// レイヤーを指定 Component のレイヤーに設定します.
		/// </summary>
		/// <param name="target">目標 Component.</param>
		public static void SetLayer(this GameObject self, Component target)
		{
			if (target) {
				self.layer = target.gameObject.layer;
			}
		}

		/// <summary>
		/// 自身を含めたすべての子孫オブジェクトのレイヤーを設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="layer">レイヤー.</param>
		public static void SetLayerRecursively(this GameObject self, int layer)
		{
			/*
			 * 再帰関数コール禁止 !! スタックオーバーフローしてしまいますので.
			 */

			foreach (Transform tf in self.GetComponentsInChildren<Transform>()) {
				tf.gameObject.layer = layer;
			}
		}

		/// <summary>
		/// 自身を含めたすべての子孫オブジェクトのレイヤーを設定します.
		/// </summary>
		/// <param name="self">GameObject 自身.</param>
		/// <param name="layerName">レイヤー名.</param>
		public static void SetLayerRecursively(this GameObject self, string layerName)
		{
			self.SetLayerRecursively(LayerMask.NameToLayer(layerName));
		}

		/// <summary>
		/// 再帰的にレイヤーを指定 GameObject のレイヤーに設定します.
		/// </summary>
		/// <param name="targetGo">目標 GameObject.</param>
		public static void SetLayerRecursively(this GameObject self, GameObject targetGo)
		{
			if (targetGo) {
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
		public static void SetLayerRecursively(this GameObject self, Component target)
		{
			if (target) {
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
	}
}
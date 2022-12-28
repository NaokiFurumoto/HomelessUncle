using System;
using System.Collections.Generic;
using UnityEngine;

namespace Carbon
{
	public static class HierarchyUtils
	{
		/// <summary>
		/// Depth-First Tree Traversal.
		/// </summary>
		/// <param name="root">Root transform.</param>
		/// <param name="action">Action to invoke.</param>
		/// <param name="traversePreOrder">True for pre-order traversal; False for post-order traversal.</param>
		public static void DepthFirstTraversal(Transform root, Action<Transform> action, bool traversePreOrder = true)
		{
			if (!root) {
				return;
			}

			// pre-order なら Unity built-in を使う方がはるかに速い.
			if (traversePreOrder) {
				foreach (Transform tf in root.GetComponentsInChildren<Transform>()) {
					action.Call(tf);
				}
				return;
			}

			// post-order
			Stack<Transform> tStack = root.CreateStack();

			while (tStack.Count > 0) {
				// pop node as parent
				Transform pt = tStack.Peek();
				if (!pt) {
					continue;
				}

				// leaf-node ( == no child )
				if (pt.childCount <= 0) {
					// pop
					tStack.Pop();
					// invoke
					action.Call(pt);
					continue;
				}

				// internal-node
				// push leaves
				foreach (Transform ct in pt) {
					tStack.Push(ct);
				}
			}
		}

		/// <summary>
		/// Breadth-First Tree Traversal.
		/// </summary>
		/// <param name="root">Root transform.</param>
		/// <param name="action">Action to invoke.</param>
		public static void BreadthFirstTraversal(Transform root, Action<Transform> action)
		{
			if (!root) {
				return;
			}

			Queue<Transform> tQueue = root.CreateQueue();

			while (tQueue.Count > 0) {
				// peek node as parent
				Transform pt = tQueue.Dequeue();
				if (!pt) {
					continue;
				}

				// invoke
				action.Call(pt);

				// push leaves
				foreach (Transform ct in pt) {
					tQueue.Enqueue(ct);
				}
			}
		}

		/// <summary>
		/// Get the check queue in depth-first traversal.
		/// </summary>
		/// <param name="root">Root transform.</param>
		/// <param name="traversePreOrder">True for pre-order traversal; False for post-order traversal.</param>
		/// <returns>A queue of transforms.</returns>
		public static Queue<Transform> GetDepthFirstTraversalQueue(Transform root, bool traversePreOrder = true)
		{
			if (!root) {
				return null;
			}

			// pre-order なら Unity built-in を使う方がはるかに速い.
			if (traversePreOrder) {
				return new Queue<Transform>(root.GetComponentsInChildren<Transform>());
			}

			Queue<Transform> tQueue = new Queue<Transform>();

			DepthFirstTraversal(root, tf => tQueue.Enqueue(tf), traversePreOrder);

			return tQueue;
		}

		/// <summary>
		/// Get the check queue in breadth-first traversal.
		/// </summary>
		/// <param name="root">Root transform.</param>
		/// <returns>A queue of transforms.</returns>
		public static Queue<Transform> GetBreadthFirstTraversalQueue(Transform root)
		{
			if (!root) {
				return null;
			}

			Queue<Transform> tQueue = new Queue<Transform>();

			BreadthFirstTraversal(root, tf => tQueue.Enqueue(tf));

			return tQueue;
		}
	}
}
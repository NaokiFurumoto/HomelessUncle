using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Carbon
{
	/// <summary>
	/// real-time == Time.unscaled*
	/// </summary>
	public static class RealTime
	{
		/// <summary>
		/// Time.unscaledDeltaTime
		/// </summary>
		public static float deltaTime {
			get {
				return Time.unscaledDeltaTime;
			}
		}

		/// <summary>
		/// Time.fixedUnscaledDeltaTime
		/// </summary>
		public static float fixedDeltaTime {
			get {
				return Time.fixedUnscaledDeltaTime;
			}
		}

		/// <summary>
		/// Time.fixedUnscaledTime
		/// </summary>
		public static float fixedTime {
			get {
				return Time.fixedUnscaledTime;
			}
		}

		/// <summary>
		/// Time.unscaledTime
		/// </summary>
		public static float time {
			get {
				return Time.unscaledTime;
			}
		}

		/// <summary>
		/// Time.realtimeSinceStartup
		/// </summary>
		public static float timeSinceStartup {
			get {
				return Time.realtimeSinceStartup;
			}
		}
	}
}

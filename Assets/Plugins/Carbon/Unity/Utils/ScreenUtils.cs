using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Carbon
{
	public static class ScreenUtils
	{
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Definition
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Interface for screen
		/// </summary>
		public interface IScreen
		{
			Vector2Int	Resolution	{ get; }
			int			Width		{ get; }
			int			Height		{ get; }
			float		Aspect		{ get; }
			Rect		SafeArea	{ get; }
		}

#if UNITY_EDITOR
		public class EditorScreen : IScreen
		{
			public Vector2Int Resolution {
				get {
					return Application.isPlaying ? new Vector2Int(Screen.width, Screen.height) : GetGameViewResolution();
				}
			}

			public int Width {
				get {
					return Application.isPlaying ? Screen.width : GetGameViewResolution().x;
				}
			}

			public int Height {
				get {
					return Application.isPlaying ? Screen.height : GetGameViewResolution().y;
				}
			}

			public float Aspect {
				get {
					var res = Resolution;
					return (float)res.x / (float)res.y;
				}
			}

			public Rect SafeArea {
				get {
					var res = Resolution;
					return new Rect(0, 0, res.x, res.y);
				}
			}

			/// <summary>
			/// Get the resolution of GameView.
			/// </summary>
			private Vector2Int GetGameViewResolution()
			{
				string[] gameViewSize = UnityStats.screenRes.Split('x');
				Vector2Int resolution = new Vector2Int();
				resolution[0] = int.Parse(gameViewSize[0]);
				resolution[1] = int.Parse(gameViewSize[1]);
				return resolution;
			}
		}
#endif
		public class ApplicationScreen : IScreen
		{
			public Vector2Int Resolution {
				get {
					return new Vector2Int(Screen.width, Screen.height);
				}
			}

			public int Width {
				get {
					return Screen.width;
				}
			}

			public int Height {
				get {
					return Screen.height;
				}
			}

			public float Aspect {
				get {
					return (float)Screen.width / (float)Screen.height;
				}
			}

			public Rect SafeArea {
				get {
					return Screen.safeArea;
				}
			}
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Field
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private static IScreen ms_Screen;

		static ScreenUtils()
		{
#if UNITY_EDITOR
			ms_Screen = new EditorScreen();
#else
			ms_Screen = new ApplicationScreen();
#endif
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Publie Method
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public static void SetScreen(IScreen iScreen)
		{
			ms_Screen = iScreen;
		}

		public static Vector2Int Resolution {
			get {
				return ms_Screen.Resolution;
			}
		}

		public static int Width {
			get {
				return ms_Screen.Width;
			}
		}

		public static int Height {
			get {
				return ms_Screen.Height;
			}
		}

		public static float Aspect {
			get {
				return ms_Screen.Aspect;
			}
		}
	}
}

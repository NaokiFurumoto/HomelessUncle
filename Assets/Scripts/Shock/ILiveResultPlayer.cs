using System;

namespace Shock.Scenes.LiveResult
{
	/// <summary>
	/// ライブ結果演出プレイヤー interface
	/// </summary>
	interface ILiveResultPlayer
	{
		/// <summary>
		/// 演出を再生
		/// </summary>
		void Play(Action onPlayComplete);
		/// <summary>
		/// 演出をスキップ
		/// </summary>
		void Skip();
		/// <summary>
		/// 演出最終状態を直接再生する
		/// </summary>
		void PlayFinalState();
	}
}

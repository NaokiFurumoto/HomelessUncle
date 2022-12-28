// このファイルはconstgenによって自動生成されています。
using System;
using System.Collections.Generic;

namespace Shock
{
	/// <summary>
	/// <para>SUCCESS:0</para>
	/// <para>ERROR_GENERIC:1</para>
	/// <para>ERROR_INTERNAL_SERVER:2</para>
	/// <para>ERROR_BAD_REQUEST:3</para>
	/// <para>ERROR_NOT_FOUND:4</para>
	/// <para>ERROR_UNAUTHORIZED:5</para>
	/// <para>ERROR_ADMIN_UNAUTHORIZED:6</para>
	/// <para>ERROR_FORBIDDEN:7</para>
	/// <para>ERROR_NOT_IMPLEMENTED:8</para>
	/// <para>ERROR_SERVICE_UNAVAILABLE:9</para>
	/// <para>ERROR_MAINTENANCE:10</para>
	/// <para>ERROR_DATA_MISMATCH:11</para>
	/// <para>ERROR_GAME_VERSION_UPDATED:12</para>
	/// <para>ERROR_RESOURCE_UPDATED:13</para>
	/// <para>ERROR_SESSION:14</para>
	/// <para>ERROR_FUNCTION_LOCK:15</para>
	/// <para>ERROR_USER_NOT_FOUND:16</para>
	/// <para>ERROR_USER_STAMINA_MAX:17</para>
	/// <para>ERROR_USER_STAMINA_SHORTAGE:18</para>
	/// <para>ERROR_ITEM_NOT_FOUND:19</para>
	/// <para>ERROR_ITEM_SHORTAGE:20</para>
	/// <para>ERROR_ITEM_UNKNOWN_EFFECT:21</para>
	/// <para>ERROR_POINT_SHORTAGE:22</para>
	/// <para>ERROR_FRIEND_MAX_SELF:23</para>
	/// <para>ERROR_FRIEND_MAX_OTHER:24</para>
	/// <para>ERROR_FRIEND_REQUEST_MAX_SELF:25</para>
	/// <para>ERROR_FRIEND_REQUEST_MAX_OTHER:26</para>
	/// <para>ERROR_FRIEND_PENDING_MAX_SELF:27</para>
	/// <para>ERROR_FRIEND_PENDING_MAX_OTHER:28</para>
	/// <para>ERROR_FRIEND_ALREADY_REQUEST:29</para>
	/// <para>ERROR_FRIEND_ALREADY_PENDING:30</para>
	/// <para>ERROR_FRIEND_ALREADY_FRIEND:31</para>
	/// <para>ERROR_FRIEND_REQUEST_REJECT:32</para>
	/// <para>ERROR_FRIEND_NOT_FOUND:33</para>
	/// <para>ERROR_EVENT_CLOSED:34</para>
	/// <para>ERROR_MISSION_EXPIRED:35</para>
	/// <para>ERROR_GIFT_EXPIRED:36</para>
	/// <para>ERROR_POLICY_UPDATED:37</para>
	/// <para>WARNING_EXCEPTION:10000</para>
	/// </summary>
	// 矩形選択用のコメント
	// RESULT_CODE.SUCCESS
	// RESULT_CODE.ERROR_GENERIC
	// RESULT_CODE.ERROR_INTERNAL_SERVER
	// RESULT_CODE.ERROR_BAD_REQUEST
	// RESULT_CODE.ERROR_NOT_FOUND
	// RESULT_CODE.ERROR_UNAUTHORIZED
	// RESULT_CODE.ERROR_ADMIN_UNAUTHORIZED
	// RESULT_CODE.ERROR_FORBIDDEN
	// RESULT_CODE.ERROR_NOT_IMPLEMENTED
	// RESULT_CODE.ERROR_SERVICE_UNAVAILABLE
	// RESULT_CODE.ERROR_MAINTENANCE
	// RESULT_CODE.ERROR_DATA_MISMATCH
	// RESULT_CODE.ERROR_GAME_VERSION_UPDATED
	// RESULT_CODE.ERROR_RESOURCE_UPDATED
	// RESULT_CODE.ERROR_SESSION
	// RESULT_CODE.ERROR_FUNCTION_LOCK
	// RESULT_CODE.ERROR_USER_NOT_FOUND
	// RESULT_CODE.ERROR_USER_STAMINA_MAX
	// RESULT_CODE.ERROR_USER_STAMINA_SHORTAGE
	// RESULT_CODE.ERROR_ITEM_NOT_FOUND
	// RESULT_CODE.ERROR_ITEM_SHORTAGE
	// RESULT_CODE.ERROR_ITEM_UNKNOWN_EFFECT
	// RESULT_CODE.ERROR_POINT_SHORTAGE
	// RESULT_CODE.ERROR_FRIEND_MAX_SELF
	// RESULT_CODE.ERROR_FRIEND_MAX_OTHER
	// RESULT_CODE.ERROR_FRIEND_REQUEST_MAX_SELF
	// RESULT_CODE.ERROR_FRIEND_REQUEST_MAX_OTHER
	// RESULT_CODE.ERROR_FRIEND_PENDING_MAX_SELF
	// RESULT_CODE.ERROR_FRIEND_PENDING_MAX_OTHER
	// RESULT_CODE.ERROR_FRIEND_ALREADY_REQUEST
	// RESULT_CODE.ERROR_FRIEND_ALREADY_PENDING
	// RESULT_CODE.ERROR_FRIEND_ALREADY_FRIEND
	// RESULT_CODE.ERROR_FRIEND_REQUEST_REJECT
	// RESULT_CODE.ERROR_FRIEND_NOT_FOUND
	// RESULT_CODE.ERROR_EVENT_CLOSED
	// RESULT_CODE.ERROR_MISSION_EXPIRED
	// RESULT_CODE.ERROR_GIFT_EXPIRED
	// RESULT_CODE.ERROR_POLICY_UPDATED
	// RESULT_CODE.WARNING_EXCEPTION
	// 正常終了
	// 一般エラー
	// 内部エラー
	// 不正なリクエスト
	// 存在しないAPI
	// 認証エラー
	// 管理ツール認証エラー
	// アクセス拒否
	// 実行不可
	// サービスが利用できない
	// メンテナンス中
	// データ不整合
	// ゲームバージョンが更新された
	// リソースが更新された
	// セッション情報が異常
	// 機能ロックエラー
	// ユーザーが見つかりません
	// 体力が最大です
	// 体力が不足しています
	// アイテムが見つかりません
	// アイテムが不足しています
	// 不明なアイテム効果です
	// ポイントが不足しています
	// 自分のフレンドが最大です
	// 相手のフレンドが最大です
	// 自分のフレンド申請数が最大です
	// 相手のフレンド申請数が最大です
	// 自分のフレンド承認待ち数が最大です
	// 相手のフレンド承認待ち数が最大です
	// 既に申請中です
	// 既に相手から申請されています
	// 既にフレンドです
	// フレンド申請が拒否されています
	// フレンドが見つかりませんでした
	// イベントが開催されていません
	// ミッションの期限が切れています
	// ギフトの期限が切れています
	// 利用規約が更新された
	// 警告エラー
	public enum RESULT_CODE : int
	{
		/// <summary>正常終了</summary>
		SUCCESS = 0,

		/// <summary>一般エラー</summary>
		ERROR_GENERIC = 1,

		/// <summary>内部エラー</summary>
		ERROR_INTERNAL_SERVER = 2,

		/// <summary>不正なリクエスト</summary>
		ERROR_BAD_REQUEST = 3,

		/// <summary>存在しないAPI</summary>
		ERROR_NOT_FOUND = 4,

		/// <summary>認証エラー</summary>
		ERROR_UNAUTHORIZED = 5,

		/// <summary>管理ツール認証エラー</summary>
		ERROR_ADMIN_UNAUTHORIZED = 6,

		/// <summary>アクセス拒否</summary>
		ERROR_FORBIDDEN = 7,

		/// <summary>実行不可</summary>
		ERROR_NOT_IMPLEMENTED = 8,

		/// <summary>サービスが利用できない</summary>
		ERROR_SERVICE_UNAVAILABLE = 9,

		/// <summary>メンテナンス中</summary>
		ERROR_MAINTENANCE = 10,

		/// <summary>データ不整合</summary>
		ERROR_DATA_MISMATCH = 11,

		/// <summary>ゲームバージョンが更新された</summary>
		ERROR_GAME_VERSION_UPDATED = 12,

		/// <summary>リソースが更新された</summary>
		ERROR_RESOURCE_UPDATED = 13,

		/// <summary>セッション情報が異常</summary>
		ERROR_SESSION = 14,

		/// <summary>機能ロックエラー</summary>
		ERROR_FUNCTION_LOCK = 15,

		/// <summary>ユーザーが見つかりません</summary>
		ERROR_USER_NOT_FOUND = 16,

		/// <summary>体力が最大です</summary>
		ERROR_USER_STAMINA_MAX = 17,

		/// <summary>体力が不足しています</summary>
		ERROR_USER_STAMINA_SHORTAGE = 18,

		/// <summary>アイテムが見つかりません</summary>
		ERROR_ITEM_NOT_FOUND = 19,

		/// <summary>アイテムが不足しています</summary>
		ERROR_ITEM_SHORTAGE = 20,

		/// <summary>不明なアイテム効果です</summary>
		ERROR_ITEM_UNKNOWN_EFFECT = 21,

		/// <summary>ポイントが不足しています</summary>
		ERROR_POINT_SHORTAGE = 22,

		/// <summary>自分のフレンドが最大です</summary>
		ERROR_FRIEND_MAX_SELF = 23,

		/// <summary>相手のフレンドが最大です</summary>
		ERROR_FRIEND_MAX_OTHER = 24,

		/// <summary>自分のフレンド申請数が最大です</summary>
		ERROR_FRIEND_REQUEST_MAX_SELF = 25,

		/// <summary>相手のフレンド申請数が最大です</summary>
		ERROR_FRIEND_REQUEST_MAX_OTHER = 26,

		/// <summary>自分のフレンド承認待ち数が最大です</summary>
		ERROR_FRIEND_PENDING_MAX_SELF = 27,

		/// <summary>相手のフレンド承認待ち数が最大です</summary>
		ERROR_FRIEND_PENDING_MAX_OTHER = 28,

		/// <summary>既に申請中です</summary>
		ERROR_FRIEND_ALREADY_REQUEST = 29,

		/// <summary>既に相手から申請されています</summary>
		ERROR_FRIEND_ALREADY_PENDING = 30,

		/// <summary>既にフレンドです</summary>
		ERROR_FRIEND_ALREADY_FRIEND = 31,

		/// <summary>フレンド申請が拒否されています</summary>
		ERROR_FRIEND_REQUEST_REJECT = 32,

		/// <summary>フレンドが見つかりませんでした</summary>
		ERROR_FRIEND_NOT_FOUND = 33,

		/// <summary>イベントが開催されていません</summary>
		ERROR_EVENT_CLOSED = 34,

		/// <summary>ミッションの期限が切れています</summary>
		ERROR_MISSION_EXPIRED = 35,

		/// <summary>ギフトの期限が切れています</summary>
		ERROR_GIFT_EXPIRED = 36,

		/// <summary>利用規約が更新された</summary>
		ERROR_POLICY_UPDATED = 37,

		/// <summary>警告エラー</summary>
		WARNING_EXCEPTION = 10000,
	}

	/// <summary>
	/// <para>DISABLED:0</para>
	/// <para>ENABLED:1</para>
	/// </summary>
	// 矩形選択用のコメント
	// STATUS.DISABLED
	// STATUS.ENABLED
	// 無効
	// 有効
	public enum STATUS : int
	{
		/// <summary>無効</summary>
		DISABLED = 0,

		/// <summary>有効</summary>
		ENABLED = 1,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>VOCAL:1</para>
	/// <para>BASS:2</para>
	/// <para>DRUM:3</para>
	/// <para>KEYBOARD:4</para>
	/// <para>GUITAR:5</para>
	/// <para>SUPPORT:6</para>
	/// </summary>
	// 矩形選択用のコメント
	// DECK_ROLE.NONE
	// DECK_ROLE.VOCAL
	// DECK_ROLE.BASS
	// DECK_ROLE.DRUM
	// DECK_ROLE.KEYBOARD
	// DECK_ROLE.GUITAR
	// DECK_ROLE.SUPPORT
	// 無し
	// ボーカル
	// ベース
	// ドラム
	// キーボード
	// ギター
	// サポート
	public enum DECK_ROLE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>ボーカル</summary>
		VOCAL = 1,

		/// <summary>ベース</summary>
		BASS = 2,

		/// <summary>ドラム</summary>
		DRUM = 3,

		/// <summary>キーボード</summary>
		KEYBOARD = 4,

		/// <summary>ギター</summary>
		GUITAR = 5,

		/// <summary>サポート</summary>
		SUPPORT = 6,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>LIGHT:1</para>
	/// <para>FIRE:2</para>
	/// <para>CRYSTAL:3</para>
	/// <para>NIGHT:4</para>
	/// </summary>
	// 矩形選択用のコメント
	// CARD_TYPE.NONE
	// CARD_TYPE.LIGHT
	// CARD_TYPE.FIRE
	// CARD_TYPE.CRYSTAL
	// CARD_TYPE.NIGHT
	// 無し
	// ライト
	// ファイア
	// クリスタル
	// ナイト
	public enum CARD_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>ライト</summary>
		LIGHT = 1,

		/// <summary>ファイア</summary>
		FIRE = 2,

		/// <summary>クリスタル</summary>
		CRYSTAL = 3,

		/// <summary>ナイト</summary>
		NIGHT = 4,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>STILL:1</para>
	/// <para>COLLECTION:2</para>
	/// <para>FOCUS:3</para>
	/// </summary>
	// 矩形選択用のコメント
	// CARD_LIMIT_BREAK_REWARD_TYPE.NONE
	// CARD_LIMIT_BREAK_REWARD_TYPE.STILL
	// CARD_LIMIT_BREAK_REWARD_TYPE.COLLECTION
	// CARD_LIMIT_BREAK_REWARD_TYPE.FOCUS
	// 無し
	// コレクションスチル
	// コレクションムービー
	// フォーカスムービー
	public enum CARD_LIMIT_BREAK_REWARD_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>コレクションスチル</summary>
		STILL = 1,

		/// <summary>コレクションムービー</summary>
		COLLECTION = 2,

		/// <summary>フォーカスムービー</summary>
		FOCUS = 3,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>HEART:1</para>
	/// <para>DIAMOND:2</para>
	/// <para>SPADE:3</para>
	/// <para>CLOVER:4</para>
	/// <para>ALL:5</para>
	/// </summary>
	// 矩形選択用のコメント
	// SKILL_CARD_TYPE.NONE
	// SKILL_CARD_TYPE.HEART
	// SKILL_CARD_TYPE.DIAMOND
	// SKILL_CARD_TYPE.SPADE
	// SKILL_CARD_TYPE.CLOVER
	// SKILL_CARD_TYPE.ALL
	// なし
	// ハート
	// ダイヤモンド
	// スペード
	// クローバー
	// 全て
	public enum SKILL_CARD_TYPE : int
	{
		/// <summary>なし</summary>
		NONE = 0,

		/// <summary>ハート</summary>
		HEART = 1,

		/// <summary>ダイヤモンド</summary>
		DIAMOND = 2,

		/// <summary>スペード</summary>
		SPADE = 3,

		/// <summary>クローバー</summary>
		CLOVER = 4,

		/// <summary>全て</summary>
		ALL = 5,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>VOCAL:1</para>
	/// <para>BASS:2</para>
	/// <para>DRUM:3</para>
	/// <para>KEYBOARD:4</para>
	/// <para>GUITAR:5</para>
	/// <para>ALL:6</para>
	/// </summary>
	// 矩形選択用のコメント
	// SKILL_DECK_ROLE.NONE
	// SKILL_DECK_ROLE.VOCAL
	// SKILL_DECK_ROLE.BASS
	// SKILL_DECK_ROLE.DRUM
	// SKILL_DECK_ROLE.KEYBOARD
	// SKILL_DECK_ROLE.GUITAR
	// SKILL_DECK_ROLE.ALL
	// なし
	// ボーカル
	// ベース
	// ドラム
	// キーボード
	// ギター
	// 全て
	public enum SKILL_DECK_ROLE : int
	{
		/// <summary>なし</summary>
		NONE = 0,

		/// <summary>ボーカル</summary>
		VOCAL = 1,

		/// <summary>ベース</summary>
		BASS = 2,

		/// <summary>ドラム</summary>
		DRUM = 3,

		/// <summary>キーボード</summary>
		KEYBOARD = 4,

		/// <summary>ギター</summary>
		GUITAR = 5,

		/// <summary>全て</summary>
		ALL = 6,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>SCORE_UP:1</para>
	/// <para>LIFE_HEAL:2</para>
	/// <para>RESULT_UP:3</para>
	/// <para>STATUS_UP:4</para>
	/// <para>LIFE_UP:5</para>
	/// </summary>
	// 矩形選択用のコメント
	// SKILL_EFFECT_TYPE.NONE
	// SKILL_EFFECT_TYPE.SCORE_UP
	// SKILL_EFFECT_TYPE.LIFE_HEAL
	// SKILL_EFFECT_TYPE.RESULT_UP
	// SKILL_EFFECT_TYPE.STATUS_UP
	// SKILL_EFFECT_TYPE.LIFE_UP
	// 無し
	// スコアUP
	// ライフ回復
	// 入力評価UP
	// ステータスUP(パッシブ)
	// ライフUP
	public enum SKILL_EFFECT_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>スコアUP</summary>
		SCORE_UP = 1,

		/// <summary>ライフ回復</summary>
		LIFE_HEAL = 2,

		/// <summary>入力評価UP</summary>
		RESULT_UP = 3,

		/// <summary>ステータスUP(パッシブ)</summary>
		STATUS_UP = 4,

		/// <summary>ライフUP</summary>
		LIFE_UP = 5,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>LIFE_OR_OVER:1</para>
	/// <para>LIFE_UNDER:2</para>
	/// <para>ADAPT_INPUT_RESULT_OR_OVER:3</para>
	/// <para>UNTIL_INPUT_RESULT_OR_UNDER:4</para>
	/// <para>SINCE_INPUT_RESULT_OR_UNDER:5</para>
	/// </summary>
	// 矩形選択用のコメント
	// SKILL_TRIGGER_TYPE.NONE
	// SKILL_TRIGGER_TYPE.LIFE_OR_OVER
	// SKILL_TRIGGER_TYPE.LIFE_UNDER
	// SKILL_TRIGGER_TYPE.ADAPT_INPUT_RESULT_OR_OVER
	// SKILL_TRIGGER_TYPE.UNTIL_INPUT_RESULT_OR_UNDER
	// SKILL_TRIGGER_TYPE.SINCE_INPUT_RESULT_OR_UNDER
	// [汎用]無条件
	// [単発]powerが一定以上なら発動
	// [単発]powerが一定未満なら発動
	// [継続]入力判定が一定以上なら適用
	// [継続]入力判定が一定以下を出すまで
	// [継続]入力判定が一定以下を出すから
	public enum SKILL_TRIGGER_TYPE : int
	{
		/// <summary>[汎用]無条件</summary>
		NONE = 0,

		/// <summary>[単発]powerが一定以上なら発動</summary>
		LIFE_OR_OVER = 1,

		/// <summary>[単発]powerが一定未満なら発動</summary>
		LIFE_UNDER = 2,

		/// <summary>[継続]入力判定が一定以上なら適用</summary>
		ADAPT_INPUT_RESULT_OR_OVER = 3,

		/// <summary>[継続]入力判定が一定以下を出すまで</summary>
		UNTIL_INPUT_RESULT_OR_UNDER = 4,

		/// <summary>[継続]入力判定が一定以下を出すから</summary>
		SINCE_INPUT_RESULT_OR_UNDER = 5,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>PARAM1:1</para>
	/// <para>PARAM2:2</para>
	/// <para>PARAM3:3</para>
	/// <para>ALL:4</para>
	/// </summary>
	// 矩形選択用のコメント
	// STATUS_UP_TYPE.NONE
	// STATUS_UP_TYPE.PARAM1
	// STATUS_UP_TYPE.PARAM2
	// STATUS_UP_TYPE.PARAM3
	// STATUS_UP_TYPE.ALL
	// 無し
	// SPiRiT
	// PASSiON
	// LOVE
	// 全て
	public enum STATUS_UP_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>SPiRiT</summary>
		PARAM1 = 1,

		/// <summary>PASSiON</summary>
		PARAM2 = 2,

		/// <summary>LOVE</summary>
		PARAM3 = 3,

		/// <summary>全て</summary>
		ALL = 4,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>TAP:1</para>
	/// <para>FLICK:2</para>
	/// <para>LONG:3</para>
	/// <para>SKILL:4</para>
	/// </summary>
	// 矩形選択用のコメント
	// MARKER_TYPE.NONE
	// MARKER_TYPE.TAP
	// MARKER_TYPE.FLICK
	// MARKER_TYPE.LONG
	// MARKER_TYPE.SKILL
	// 無し
	// タップマーカー
	// フリックマーカー
	// 長押しマーカー
	// スキルマーカー
	public enum MARKER_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>タップマーカー</summary>
		TAP = 1,

		/// <summary>フリックマーカー</summary>
		FLICK = 2,

		/// <summary>長押しマーカー</summary>
		LONG = 3,

		/// <summary>スキルマーカー</summary>
		SKILL = 4,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>PERFECT:1</para>
	/// <para>GREAT:2</para>
	/// <para>GOOD:3</para>
	/// <para>BAD:4</para>
	/// <para>MISS:5</para>
	/// </summary>
	// 矩形選択用のコメント
	// INPUT_RESULT_TYPE.NONE
	// INPUT_RESULT_TYPE.PERFECT
	// INPUT_RESULT_TYPE.GREAT
	// INPUT_RESULT_TYPE.GOOD
	// INPUT_RESULT_TYPE.BAD
	// INPUT_RESULT_TYPE.MISS
	// 無し
	// ずれなくタップ出来た場合
	// 若干ずれた場合
	// ずれた場合
	// 大きくずれた場合
	// タップしなかったor正しくタップ出来なかった場合
	public enum INPUT_RESULT_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>ずれなくタップ出来た場合</summary>
		PERFECT = 1,

		/// <summary>若干ずれた場合</summary>
		GREAT = 2,

		/// <summary>ずれた場合</summary>
		GOOD = 3,

		/// <summary>大きくずれた場合</summary>
		BAD = 4,

		/// <summary>タップしなかったor正しくタップ出来なかった場合</summary>
		MISS = 5,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>REQUEST:1</para>
	/// <para>PENDING:2</para>
	/// <para>FRIEND:3</para>
	/// <para>FRIEND_SEARCH:4</para>
	/// </summary>
	// 矩形選択用のコメント
	// FRIEND_STATUS.NONE
	// FRIEND_STATUS.REQUEST
	// FRIEND_STATUS.PENDING
	// FRIEND_STATUS.FRIEND
	// FRIEND_STATUS.FRIEND_SEARCH
	// 無し
	// 申請中
	// 承認待ち
	// フレンド
	// フレンド検索
	public enum FRIEND_STATUS : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>申請中</summary>
		REQUEST = 1,

		/// <summary>承認待ち</summary>
		PENDING = 2,

		/// <summary>フレンド</summary>
		FRIEND = 3,

		/// <summary>フレンド検索</summary>
		FRIEND_SEARCH = 4,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>STAMINA_RECOVER:1</para>
	/// <para>STAMINA_RECOVER_MAX:2</para>
	/// <para>OBTAIN_CARD_EXP:3</para>
	/// <para>OBTAIN_CARD_SKILL_EXP:4</para>
	/// <para>OBTAIN_GROUP_EXP:5</para>
	/// <para>LOTTERY_TICKET:6</para>
	/// <para>MUSIC_PRICE:7</para>
	/// <para>CARD_EVOLVE:8</para>
	/// <para>LIVE_TICKET:9</para>
	/// </summary>
	// 矩形選択用のコメント
	// ITEM_EFFECT_TYPE.NONE
	// ITEM_EFFECT_TYPE.STAMINA_RECOVER
	// ITEM_EFFECT_TYPE.STAMINA_RECOVER_MAX
	// ITEM_EFFECT_TYPE.OBTAIN_CARD_EXP
	// ITEM_EFFECT_TYPE.OBTAIN_CARD_SKILL_EXP
	// ITEM_EFFECT_TYPE.OBTAIN_GROUP_EXP
	// ITEM_EFFECT_TYPE.LOTTERY_TICKET
	// ITEM_EFFECT_TYPE.MUSIC_PRICE
	// ITEM_EFFECT_TYPE.CARD_EVOLVE
	// ITEM_EFFECT_TYPE.LIVE_TICKET
	// 無し
	// 体力回復
	// 体力全回復
	// 獲得カード経験値
	// 獲得カードスキル経験値
	// 獲得グループ経験値
	// ガチャチケット
	// 楽曲の対価
	// カード特訓
	// ライブチケット
	public enum ITEM_EFFECT_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>体力回復</summary>
		STAMINA_RECOVER = 1,

		/// <summary>体力全回復</summary>
		STAMINA_RECOVER_MAX = 2,

		/// <summary>獲得カード経験値</summary>
		OBTAIN_CARD_EXP = 3,

		/// <summary>獲得カードスキル経験値</summary>
		OBTAIN_CARD_SKILL_EXP = 4,

		/// <summary>獲得グループ経験値</summary>
		OBTAIN_GROUP_EXP = 5,

		/// <summary>ガチャチケット</summary>
		LOTTERY_TICKET = 6,

		/// <summary>楽曲の対価</summary>
		MUSIC_PRICE = 7,

		/// <summary>カード特訓</summary>
		CARD_EVOLVE = 8,

		/// <summary>ライブチケット</summary>
		LIVE_TICKET = 9,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>TICKET:1</para>
	/// <para>MUSIC_EXCHANGE:2</para>
	/// <para>CARD_EVOLVE:3</para>
	/// <para>CARD_REINFORCE:4</para>
	/// <para>CARD_REINFORCE_SKILL:5</para>
	/// <para>EPISODE_UNLOCK:6</para>
	/// <para>GROUP_REINFORCE:7</para>
	/// <para>STAMINA_RECOVER:8</para>
	/// <para>LOTTERY_TICKET:9</para>
	/// <para>ORNAMENT_REINFORCE:10</para>
	/// <para>COMMON_EXCHANGE:11</para>
	/// <para>STAMP_EXCHANGE:12</para>
	/// <para>PHOTO_EXCHANGE:13</para>
	/// <para>REAL_INCENTIVE_TICKET:14</para>
	/// </summary>
	// 矩形選択用のコメント
	// ITEM_CATEGORY.NONE
	// ITEM_CATEGORY.TICKET
	// ITEM_CATEGORY.MUSIC_EXCHANGE
	// ITEM_CATEGORY.CARD_EVOLVE
	// ITEM_CATEGORY.CARD_REINFORCE
	// ITEM_CATEGORY.CARD_REINFORCE_SKILL
	// ITEM_CATEGORY.EPISODE_UNLOCK
	// ITEM_CATEGORY.GROUP_REINFORCE
	// ITEM_CATEGORY.STAMINA_RECOVER
	// ITEM_CATEGORY.LOTTERY_TICKET
	// ITEM_CATEGORY.ORNAMENT_REINFORCE
	// ITEM_CATEGORY.COMMON_EXCHANGE
	// ITEM_CATEGORY.STAMP_EXCHANGE
	// ITEM_CATEGORY.PHOTO_EXCHANGE
	// ITEM_CATEGORY.REAL_INCENTIVE_TICKET
	// 無し
	// チケット
	// 楽曲交換アイテム
	// 限界突破アイテム
	// カード強化アイテム
	// カードスキル強化アイテム
	// エピソード解放アイテ
	// バンド強化アイテム
	// スタミナ回復アイテム
	// ガチャチケ
	// エリアアイテム
	// 交換所アイテム
	// スタンプ交換券
	// フォトブック
	// 抽選チケット
	public enum ITEM_CATEGORY : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>チケット</summary>
		TICKET = 1,

		/// <summary>楽曲交換アイテム</summary>
		MUSIC_EXCHANGE = 2,

		/// <summary>限界突破アイテム</summary>
		CARD_EVOLVE = 3,

		/// <summary>カード強化アイテム</summary>
		CARD_REINFORCE = 4,

		/// <summary>カードスキル強化アイテム</summary>
		CARD_REINFORCE_SKILL = 5,

		/// <summary>エピソード解放アイテ</summary>
		EPISODE_UNLOCK = 6,

		/// <summary>バンド強化アイテム</summary>
		GROUP_REINFORCE = 7,

		/// <summary>スタミナ回復アイテム</summary>
		STAMINA_RECOVER = 8,

		/// <summary>ガチャチケ</summary>
		LOTTERY_TICKET = 9,

		/// <summary>エリアアイテム</summary>
		ORNAMENT_REINFORCE = 10,

		/// <summary>交換所アイテム</summary>
		COMMON_EXCHANGE = 11,

		/// <summary>スタンプ交換券</summary>
		STAMP_EXCHANGE = 12,

		/// <summary>フォトブック</summary>
		PHOTO_EXCHANGE = 13,

		/// <summary>抽選チケット</summary>
		REAL_INCENTIVE_TICKET = 14,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>GEM:1</para>
	/// <para>CARD:2</para>
	/// <para>ITEM:3</para>
	/// <para>POINT:4</para>
	/// <para>ORNAMENT:5</para>
	/// <para>COSTUME:6</para>
	/// <para>ORNAMENT_RELEASE:7</para>
	/// <para>ORNAMENT_LEVEL_UP:8</para>
	/// <para>TITLE:9</para>
	/// <para>MUSIC:10</para>
	/// <para>STAMP:11</para>
	/// <para>STAMINA:12</para>
	/// <para>EVENT_STAMINA:13</para>
	/// <para>EVENT_POINT_PICK:14</para>
	/// <para>EVENT_POINT_COIN:15</para>
	/// <para>MOVIE:16</para>
	/// <para>EVENT_POINT_CANDY:17</para>
	/// <para>COLLECTION_PHOTO:18</para>
	/// <para>COLLECTION_CARD_MOVIE:19</para>
	/// <para>COLLECTION_FOCUS_MOVIE:20</para>
	/// <para>EVENT_POINT_SPIRIT:21</para>
	/// </summary>
	// 矩形選択用のコメント
	// REWARD_TYPE.NONE
	// REWARD_TYPE.GEM
	// REWARD_TYPE.CARD
	// REWARD_TYPE.ITEM
	// REWARD_TYPE.POINT
	// REWARD_TYPE.ORNAMENT
	// REWARD_TYPE.COSTUME
	// REWARD_TYPE.ORNAMENT_RELEASE
	// REWARD_TYPE.ORNAMENT_LEVEL_UP
	// REWARD_TYPE.TITLE
	// REWARD_TYPE.MUSIC
	// REWARD_TYPE.STAMP
	// REWARD_TYPE.STAMINA
	// REWARD_TYPE.EVENT_STAMINA
	// REWARD_TYPE.EVENT_POINT_PICK
	// REWARD_TYPE.EVENT_POINT_COIN
	// REWARD_TYPE.MOVIE
	// REWARD_TYPE.EVENT_POINT_CANDY
	// REWARD_TYPE.COLLECTION_PHOTO
	// REWARD_TYPE.COLLECTION_CARD_MOVIE
	// REWARD_TYPE.COLLECTION_FOCUS_MOVIE
	// REWARD_TYPE.EVENT_POINT_SPIRIT
	// 無し
	// 課金通貨
	// カード
	// アイテム
	// ポイント
	// 備品
	// 衣装
	// 備品解放
	// 備品レベルアップ
	// 称号
	// 楽曲
	// スタンプ
	// スタミナ
	// イベントスタミナ
	// イベントポイントピック(不要)
	// イベントポイントコイン(不要)
	// 動画
	// イベントポイントキャンディ(不要)
	// コレクションスチル
	// コレクションカードムービー
	// コレクションフォーカスムービー
	// イベントポイントスピリット(不要)
	public enum REWARD_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>課金通貨</summary>
		GEM = 1,

		/// <summary>カード</summary>
		CARD = 2,

		/// <summary>アイテム</summary>
		ITEM = 3,

		/// <summary>ポイント</summary>
		POINT = 4,

		/// <summary>備品</summary>
		ORNAMENT = 5,

		/// <summary>衣装</summary>
		COSTUME = 6,

		/// <summary>備品解放</summary>
		ORNAMENT_RELEASE = 7,

		/// <summary>備品レベルアップ</summary>
		ORNAMENT_LEVEL_UP = 8,

		/// <summary>称号</summary>
		TITLE = 9,

		/// <summary>楽曲</summary>
		MUSIC = 10,

		/// <summary>スタンプ</summary>
		STAMP = 11,

		/// <summary>スタミナ</summary>
		STAMINA = 12,

		/// <summary>イベントスタミナ</summary>
		EVENT_STAMINA = 13,

		/// <summary>イベントポイントピック(不要)</summary>
		EVENT_POINT_PICK = 14,

		/// <summary>イベントポイントコイン(不要)</summary>
		EVENT_POINT_COIN = 15,

		/// <summary>動画</summary>
		MOVIE = 16,

		/// <summary>イベントポイントキャンディ(不要)</summary>
		EVENT_POINT_CANDY = 17,

		/// <summary>コレクションスチル</summary>
		COLLECTION_PHOTO = 18,

		/// <summary>コレクションカードムービー</summary>
		COLLECTION_CARD_MOVIE = 19,

		/// <summary>コレクションフォーカスムービー</summary>
		COLLECTION_FOCUS_MOVIE = 20,

		/// <summary>イベントポイントスピリット(不要)</summary>
		EVENT_POINT_SPIRIT = 21,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>COIN:1</para>
	/// <para>SEAL:2</para>
	/// <para>LIVE_STAMP:3</para>
	/// </summary>
	// 矩形選択用のコメント
	// POINT_TYPE.NONE
	// POINT_TYPE.COIN
	// POINT_TYPE.SEAL
	// POINT_TYPE.LIVE_STAMP
	// 無し
	// ゴールド
	// 記憶の欠片
	// ライブスタンプ
	public enum POINT_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>ゴールド</summary>
		COIN = 1,

		/// <summary>記憶の欠片</summary>
		SEAL = 2,

		/// <summary>ライブスタンプ</summary>
		LIVE_STAMP = 3,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>RARE1:1</para>
	/// <para>RARE2:2</para>
	/// <para>RARE3:3</para>
	/// <para>RARE4:4</para>
	/// </summary>
	// 矩形選択用のコメント
	// RARITY.NONE
	// RARITY.RARE1
	// RARITY.RARE2
	// RARITY.RARE3
	// RARITY.RARE4
	// 無し
	// レア1
	// レア2
	// レア3
	// レア4
	public enum RARITY : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>レア1</summary>
		RARE1 = 1,

		/// <summary>レア2</summary>
		RARE2 = 2,

		/// <summary>レア3</summary>
		RARE3 = 3,

		/// <summary>レア4</summary>
		RARE4 = 4,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>GEM:1</para>
	/// <para>CHARGE_GEM:2</para>
	/// <para>CARD:3</para>
	/// <para>ITEM:4</para>
	/// <para>POINT:5</para>
	/// <para>EVENT_POINT:6</para>
	/// </summary>
	// 矩形選択用のコメント
	// CONSUME_TYPE.NONE
	// CONSUME_TYPE.GEM
	// CONSUME_TYPE.CHARGE_GEM
	// CONSUME_TYPE.CARD
	// CONSUME_TYPE.ITEM
	// CONSUME_TYPE.POINT
	// CONSUME_TYPE.EVENT_POINT
	// 無し
	// 課金通貨
	// 課金通貨(有償)
	// カード
	// アイテム
	// ポイント
	// イベントポイント
	public enum CONSUME_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>課金通貨</summary>
		GEM = 1,

		/// <summary>課金通貨(有償)</summary>
		CHARGE_GEM = 2,

		/// <summary>カード</summary>
		CARD = 3,

		/// <summary>アイテム</summary>
		ITEM = 4,

		/// <summary>ポイント</summary>
		POINT = 5,

		/// <summary>イベントポイント</summary>
		EVENT_POINT = 6,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>GEM:1</para>
	/// <para>CHARGE_GEM:2</para>
	/// <para>POINT:3</para>
	/// <para>ITEM:4</para>
	/// <para>USER_RANK:5</para>
	/// </summary>
	// 矩形選択用のコメント
	// ORNAMENT_SHOP_CONSUME_TYPE.NONE
	// ORNAMENT_SHOP_CONSUME_TYPE.GEM
	// ORNAMENT_SHOP_CONSUME_TYPE.CHARGE_GEM
	// ORNAMENT_SHOP_CONSUME_TYPE.POINT
	// ORNAMENT_SHOP_CONSUME_TYPE.ITEM
	// ORNAMENT_SHOP_CONSUME_TYPE.USER_RANK
	// 無し
	// 課金通貨
	// 課金通貨(有償)
	// ポイント
	// アイテム
	// ユーザランク
	public enum ORNAMENT_SHOP_CONSUME_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>課金通貨</summary>
		GEM = 1,

		/// <summary>課金通貨(有償)</summary>
		CHARGE_GEM = 2,

		/// <summary>ポイント</summary>
		POINT = 3,

		/// <summary>アイテム</summary>
		ITEM = 4,

		/// <summary>ユーザランク</summary>
		USER_RANK = 5,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>FREE:1</para>
	/// <para>MULTI_FREE:2</para>
	/// <para>MULTI_REGULAR:3</para>
	/// <para>MULTI_MASTER:4</para>
	/// <para>MULTI_LEGEND:5</para>
	/// <para>MULTI_XJAPAN:6</para>
	/// <para>MULTI_LUNASEA:7</para>
	/// <para>MULTI_GLAY:8</para>
	/// </summary>
	// 矩形選択用のコメント
	// LIVE_MODE.NONE
	// LIVE_MODE.FREE
	// LIVE_MODE.MULTI_FREE
	// LIVE_MODE.MULTI_REGULAR
	// LIVE_MODE.MULTI_MASTER
	// LIVE_MODE.MULTI_LEGEND
	// LIVE_MODE.MULTI_XJAPAN
	// LIVE_MODE.MULTI_LUNASEA
	// LIVE_MODE.MULTI_GLAY
	// 無し
	// フリーライブ
	// マルチライブ(フリーセッション)
	// マルチライブ(レギュラーセッション)
	// マルチライブ(マスターセッション)
	// マルチライブ(レジェンドセッション)
	// マルチライブ(XJAPANセッション)
	// マルチライブ(LUNASEAセッション)
	// マルチライブ(GLAYセッション)
	public enum LIVE_MODE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>フリーライブ</summary>
		FREE = 1,

		/// <summary>マルチライブ(フリーセッション)</summary>
		MULTI_FREE = 2,

		/// <summary>マルチライブ(レギュラーセッション)</summary>
		MULTI_REGULAR = 3,

		/// <summary>マルチライブ(マスターセッション)</summary>
		MULTI_MASTER = 4,

		/// <summary>マルチライブ(レジェンドセッション)</summary>
		MULTI_LEGEND = 5,

		/// <summary>マルチライブ(XJAPANセッション)</summary>
		MULTI_XJAPAN = 6,

		/// <summary>マルチライブ(LUNASEAセッション)</summary>
		MULTI_LUNASEA = 7,

		/// <summary>マルチライブ(GLAYセッション)</summary>
		MULTI_GLAY = 8,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>EASY:1</para>
	/// <para>NORMAL:2</para>
	/// <para>HARD:3</para>
	/// <para>EXPERT:4</para>
	/// </summary>
	// 矩形選択用のコメント
	// LIVE_LEVEL.NONE
	// LIVE_LEVEL.EASY
	// LIVE_LEVEL.NORMAL
	// LIVE_LEVEL.HARD
	// LIVE_LEVEL.EXPERT
	// 無し
	// 低難易度
	// 中難易度
	// 高難易度
	// 超高難易度
	public enum LIVE_LEVEL : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>低難易度</summary>
		EASY = 1,

		/// <summary>中難易度</summary>
		NORMAL = 2,

		/// <summary>高難易度</summary>
		HARD = 3,

		/// <summary>超高難易度</summary>
		EXPERT = 4,
	}

	/// <summary>
	/// <para>D:0</para>
	/// <para>C:1</para>
	/// <para>B:2</para>
	/// <para>A:3</para>
	/// <para>S:4</para>
	/// <para>SS:5</para>
	/// </summary>
	// 矩形選択用のコメント
	// SCORE_RANK.D
	// SCORE_RANK.C
	// SCORE_RANK.B
	// SCORE_RANK.A
	// SCORE_RANK.S
	// SCORE_RANK.SS
	// D
	// C
	// B
	// A
	// S
	// SS
	public enum SCORE_RANK : int
	{
		/// <summary>D</summary>
		D = 0,

		/// <summary>C</summary>
		C = 1,

		/// <summary>B</summary>
		B = 2,

		/// <summary>A</summary>
		A = 3,

		/// <summary>S</summary>
		S = 4,

		/// <summary>SS</summary>
		SS = 5,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>MAIN:1</para>
	/// <para>SUB:2</para>
	/// <para>EVENT:3</para>
	/// </summary>
	// 矩形選択用のコメント
	// STORY_TYPE.NONE
	// STORY_TYPE.MAIN
	// STORY_TYPE.SUB
	// STORY_TYPE.EVENT
	// 無し
	// メイン
	// サブ
	// イベント
	public enum STORY_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>メイン</summary>
		MAIN = 1,

		/// <summary>サブ</summary>
		SUB = 2,

		/// <summary>イベント</summary>
		EVENT = 3,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>PROGRESS:1</para>
	/// <para>CLEAR:2</para>
	/// <para>RECEIVED:3</para>
	/// </summary>
	// 矩形選択用のコメント
	// MISSION_STATUS.NONE
	// MISSION_STATUS.PROGRESS
	// MISSION_STATUS.CLEAR
	// MISSION_STATUS.RECEIVED
	// 無し
	// 進行中
	// クリア
	// 報酬受取済
	public enum MISSION_STATUS : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>進行中</summary>
		PROGRESS = 1,

		/// <summary>クリア</summary>
		CLEAR = 2,

		/// <summary>報酬受取済</summary>
		RECEIVED = 3,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>MAIN:1</para>
	/// <para>DAILY:2</para>
	/// <para>WEEKLY:3</para>
	/// <para>EVENT:4</para>
	/// <para>EVENT_DAILY:5</para>
	/// <para>BINGO:6</para>
	/// <para>LIMIT:7</para>
	/// <para>EVENT_BINGO:8</para>
	/// <para>EVENT_TEACHER:9</para>
	/// </summary>
	// 矩形選択用のコメント
	// MISSION_TYPE.NONE
	// MISSION_TYPE.MAIN
	// MISSION_TYPE.DAILY
	// MISSION_TYPE.WEEKLY
	// MISSION_TYPE.EVENT
	// MISSION_TYPE.EVENT_DAILY
	// MISSION_TYPE.BINGO
	// MISSION_TYPE.LIMIT
	// MISSION_TYPE.EVENT_BINGO
	// MISSION_TYPE.EVENT_TEACHER
	// 無し
	// メイン
	// デイリー
	// ウィークリー
	// イベント
	// イベントデイリー
	// ビンゴ
	// 期間限定
	// イベントビンゴ
	// 先生イベント
	public enum MISSION_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>メイン</summary>
		MAIN = 1,

		/// <summary>デイリー</summary>
		DAILY = 2,

		/// <summary>ウィークリー</summary>
		WEEKLY = 3,

		/// <summary>イベント</summary>
		EVENT = 4,

		/// <summary>イベントデイリー</summary>
		EVENT_DAILY = 5,

		/// <summary>ビンゴ</summary>
		BINGO = 6,

		/// <summary>期間限定</summary>
		LIMIT = 7,

		/// <summary>イベントビンゴ</summary>
		EVENT_BINGO = 8,

		/// <summary>先生イベント</summary>
		EVENT_TEACHER = 9,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>USER_RANK:1</para>
	/// <para>USER_UPDATE_COMMENT_COUNT:2</para>
	/// <para>USER_UPDATE_TITLE_COUNT:3</para>
	/// <para>USER_UPDATE_FAVORITE_CARD_COUNT:4</para>
	/// <para>USER_UPDATE_FAVORITE_CHARACTER_COUNT:5</para>
	/// <para>USER_UPDATE_FAVORITE_GROUP_COUNT:6</para>
	/// <para>LIVE_CLEAR_COUNT:7</para>
	/// <para>LIVE_CLEAR_BOOST_COUNT:8</para>
	/// <para>LIVE_CLEAR_MUSIC_COUNT:9</para>
	/// <para>LIVE_CLEAR_MUSIC_CATEGORY_COUNT:10</para>
	/// <para>LIVE_SCORE_RANK_COUNT:11</para>
	/// <para>LIVE_FULL_COMBO_LEVEL_COUNT:12</para>
	/// <para>LIVE_TOTAL_PARAM:13</para>
	/// <para>MULTI_LIVE_CLEAR_COUNT:14</para>
	/// <para>CARD_COUNT:15</para>
	/// <para>CARD_RARE_COUNT:16</para>
	/// <para>CARD_LEVEL_COUNT:17</para>
	/// <para>CARD_REINFORCE_COUNT:18</para>
	/// <para>CARD_REINFORCE_SKILL_COUNT:19</para>
	/// <para>CARD_EVOLVE_COUNT:20</para>
	/// <para>CARD_EVOLVE_TARGET_COUNT:21</para>
	/// <para>CARD_UPDATE_COSTUME_COUNT:22</para>
	/// <para>ORNAMENT_BUY_COUNT:23</para>
	/// <para>ORNAMENT_SETTING_COUNT:24</para>
	/// <para>MUSIC_BUY_COUNT:25</para>
	/// <para>POINT_COUNT:26</para>
	/// <para>FRIEND_COUNT:27</para>
	/// <para>STAMP_COUNT:28</para>
	/// <para>DAILY_MISSION_COMPLETE:29</para>
	/// <para>SET_MIGRATION:30</para>
	/// <para>OFFICIAL_TWITTER_FOLLOW:31</para>
	/// <para>OFFICIAL_LINE_FOLLOW:32</para>
	/// <para>LIVE_SCORE:33</para>
	/// <para>STORY_CLEAR:34</para>
	/// <para>DECK_CARD_TYPE:35</para>
	/// <para>CHARACTER_MAX_LEVEL_COUNT:36</para>
	/// <para>LIVE_CLEAR_ALL_LEVEL:37</para>
	/// <para>LIVE_CLEAR_FULL_COMBO_ALL_LEVEL:38</para>
	/// <para>LIVE_CLEAR_TARGET_COUNT:39</para>
	/// <para>LIVE_CLEAR_TARGET_AND_LEVEL_COUNT:40</para>
	/// <para>LIVE_CLEAR_TARGET_CHARACTER_AND_LIVE_COUNT:41</para>
	/// <para>LIVE_CLEAR_TARGET_CARD_TYPE_AND_LIVE_COUNT:42</para>
	/// <para>EVENT_POINT_COUNT:43</para>
	/// <para>EVENT_LIVE_CLEAR_COUNT:44</para>
	/// <para>EVENT_LIVE_FULL_COMBO_COUNT:45</para>
	/// <para>TARGET_CARD_LEVEL:46</para>
	/// <para>EXTRA_EVENT_LIVE_CLEAR_COUNT:47</para>
	/// <para>EXTRA_EVENT_LIVE_FULL_COMBO_COUNT:48</para>
	/// <para>EXTRA_LIVE_CLEAR_TARGET_AND_LEVEL_COUNT:49</para>
	/// <para>LIVE_SCORE_COUNT:50</para>
	/// <para>LIVE_LIFE_COUNT:51</para>
	/// <para>LIVE_NOTES_JUDGEMENT_COUNT:52</para>
	/// <para>LIVE_COMBO_COUNT:53</para>
	/// <para>LIVE_FULL_COMBO_COUNT:54</para>
	/// <para>LIVE_CHARACTER_COUNT:55</para>
	/// <para>LIVE_CARD_TYPE_COUNT:56</para>
	/// <para>MULTI_LIVE_SCORE_RANK_COUNT:57</para>
	/// <para>LOGIN_COUNT:58</para>
	/// <para>TARGET_CARD_OBTAIN:59</para>
	/// <para>TARGET_MUSIC_OBTAIN:60</para>
	/// <para>EPISODE_UNLOCK_COUNT:61</para>
	/// <para>EPISODE_READ_COUNT:62</para>
	/// <para>HIGH_SCORE_RATE:63</para>
	/// <para>TIME_LOGIN:64</para>
	/// <para>GROUP_RANK:65</para>
	/// <para>TALK_COUNT:66</para>
	/// </summary>
	// 矩形選択用のコメント
	// MISSION_CONDITION_TYPE.NONE
	// MISSION_CONDITION_TYPE.USER_RANK
	// MISSION_CONDITION_TYPE.USER_UPDATE_COMMENT_COUNT
	// MISSION_CONDITION_TYPE.USER_UPDATE_TITLE_COUNT
	// MISSION_CONDITION_TYPE.USER_UPDATE_FAVORITE_CARD_COUNT
	// MISSION_CONDITION_TYPE.USER_UPDATE_FAVORITE_CHARACTER_COUNT
	// MISSION_CONDITION_TYPE.USER_UPDATE_FAVORITE_GROUP_COUNT
	// MISSION_CONDITION_TYPE.LIVE_CLEAR_COUNT
	// MISSION_CONDITION_TYPE.LIVE_CLEAR_BOOST_COUNT
	// MISSION_CONDITION_TYPE.LIVE_CLEAR_MUSIC_COUNT
	// MISSION_CONDITION_TYPE.LIVE_CLEAR_MUSIC_CATEGORY_COUNT
	// MISSION_CONDITION_TYPE.LIVE_SCORE_RANK_COUNT
	// MISSION_CONDITION_TYPE.LIVE_FULL_COMBO_LEVEL_COUNT
	// MISSION_CONDITION_TYPE.LIVE_TOTAL_PARAM
	// MISSION_CONDITION_TYPE.MULTI_LIVE_CLEAR_COUNT
	// MISSION_CONDITION_TYPE.CARD_COUNT
	// MISSION_CONDITION_TYPE.CARD_RARE_COUNT
	// MISSION_CONDITION_TYPE.CARD_LEVEL_COUNT
	// MISSION_CONDITION_TYPE.CARD_REINFORCE_COUNT
	// MISSION_CONDITION_TYPE.CARD_REINFORCE_SKILL_COUNT
	// MISSION_CONDITION_TYPE.CARD_EVOLVE_COUNT
	// MISSION_CONDITION_TYPE.CARD_EVOLVE_TARGET_COUNT
	// MISSION_CONDITION_TYPE.CARD_UPDATE_COSTUME_COUNT
	// MISSION_CONDITION_TYPE.ORNAMENT_BUY_COUNT
	// MISSION_CONDITION_TYPE.ORNAMENT_SETTING_COUNT
	// MISSION_CONDITION_TYPE.MUSIC_BUY_COUNT
	// MISSION_CONDITION_TYPE.POINT_COUNT
	// MISSION_CONDITION_TYPE.FRIEND_COUNT
	// MISSION_CONDITION_TYPE.STAMP_COUNT
	// MISSION_CONDITION_TYPE.DAILY_MISSION_COMPLETE
	// MISSION_CONDITION_TYPE.SET_MIGRATION
	// MISSION_CONDITION_TYPE.OFFICIAL_TWITTER_FOLLOW
	// MISSION_CONDITION_TYPE.OFFICIAL_LINE_FOLLOW
	// MISSION_CONDITION_TYPE.LIVE_SCORE
	// MISSION_CONDITION_TYPE.STORY_CLEAR
	// MISSION_CONDITION_TYPE.DECK_CARD_TYPE
	// MISSION_CONDITION_TYPE.CHARACTER_MAX_LEVEL_COUNT
	// MISSION_CONDITION_TYPE.LIVE_CLEAR_ALL_LEVEL
	// MISSION_CONDITION_TYPE.LIVE_CLEAR_FULL_COMBO_ALL_LEVEL
	// MISSION_CONDITION_TYPE.LIVE_CLEAR_TARGET_COUNT
	// MISSION_CONDITION_TYPE.LIVE_CLEAR_TARGET_AND_LEVEL_COUNT
	// MISSION_CONDITION_TYPE.LIVE_CLEAR_TARGET_CHARACTER_AND_LIVE_COUNT
	// MISSION_CONDITION_TYPE.LIVE_CLEAR_TARGET_CARD_TYPE_AND_LIVE_COUNT
	// MISSION_CONDITION_TYPE.EVENT_POINT_COUNT
	// MISSION_CONDITION_TYPE.EVENT_LIVE_CLEAR_COUNT
	// MISSION_CONDITION_TYPE.EVENT_LIVE_FULL_COMBO_COUNT
	// MISSION_CONDITION_TYPE.TARGET_CARD_LEVEL
	// MISSION_CONDITION_TYPE.EXTRA_EVENT_LIVE_CLEAR_COUNT
	// MISSION_CONDITION_TYPE.EXTRA_EVENT_LIVE_FULL_COMBO_COUNT
	// MISSION_CONDITION_TYPE.EXTRA_LIVE_CLEAR_TARGET_AND_LEVEL_COUNT
	// MISSION_CONDITION_TYPE.LIVE_SCORE_COUNT
	// MISSION_CONDITION_TYPE.LIVE_LIFE_COUNT
	// MISSION_CONDITION_TYPE.LIVE_NOTES_JUDGEMENT_COUNT
	// MISSION_CONDITION_TYPE.LIVE_COMBO_COUNT
	// MISSION_CONDITION_TYPE.LIVE_FULL_COMBO_COUNT
	// MISSION_CONDITION_TYPE.LIVE_CHARACTER_COUNT
	// MISSION_CONDITION_TYPE.LIVE_CARD_TYPE_COUNT
	// MISSION_CONDITION_TYPE.MULTI_LIVE_SCORE_RANK_COUNT
	// MISSION_CONDITION_TYPE.LOGIN_COUNT
	// MISSION_CONDITION_TYPE.TARGET_CARD_OBTAIN
	// MISSION_CONDITION_TYPE.TARGET_MUSIC_OBTAIN
	// MISSION_CONDITION_TYPE.EPISODE_UNLOCK_COUNT
	// MISSION_CONDITION_TYPE.EPISODE_READ_COUNT
	// MISSION_CONDITION_TYPE.HIGH_SCORE_RATE
	// MISSION_CONDITION_TYPE.TIME_LOGIN
	// MISSION_CONDITION_TYPE.GROUP_RANK
	// MISSION_CONDITION_TYPE.TALK_COUNT
	// 無し
	// ユーザーランク
	// ユーザーコメント変更
	// ユーザー称号変更
	// ユーザー推しカード変更
	// ユーザー推しキャラクター変更
	// ユーザー推しバンド変更
	// ライブクリア
	// ライブブーストを設定してクリア
	// 指定楽曲ライブクリア
	// 指定楽曲カテゴリライブクリア
	// ライブスコアランク
	// 難易度レベルフルコンボ数
	// 総合力
	// 協力ライブクリア
	// カード数
	// 指定レアリティカード数
	// 指定レベルカード数
	// カード練習
	// カードスキル練習
	// カード特訓
	// 指定カード特訓
	// カード衣装変更
	// 備品購入種類
	// 備品設置回数
	// 楽曲購入種類
	// ポイント収集量
	// フレンド数
	// スタンプ回数
	// デイリーミッション全てクリア
	// データ引き継ぎ設定
	// 公式Twitterフォロー
	// 公式LINEフォロー
	// ライブスコア
	// ストーリークリア
	// xタイプn人のデッキ作成
	// キャラxのフォトをn枚最大レベルにする
	// 曲xを全難易度でクリア
	// 曲xを全難易度でフルコンボ
	// 指定ライブクリア回数
	// 指定ライブの指定難易度クリア回数
	// 指定キャラクターを編成して指定ライブクリア回数
	// 指定タイプを編成して指定ライブクリア回数
	// イベントポイント収集量
	// イベントライブクリア回数
	// イベントライブフルコンボ回数
	// 指定カードレベル
	// イベントライブクリア(2回目用)
	// イベントライブフルコンボ回数(2回目用)
	// 指定ライブの指定難易度クリア回数(2回目用)
	// スコアx以上でy回クリア
	// ライフx以上でy回クリア
	// ノーツx判定y以内でz回クリア
	// コンボx以上でy回クリア
	// フルコンボx回でクリア
	// キャラクター(複数可)を入れてx回クリア
	// カードタイプ(複数可)を入れてx回クリア
	// マルチライブスコアランクx以上でy回クリア
	// ログイン回数
	// 指定カード入手
	// 指定楽曲入手
	// エピソード解放数
	// エピソード閲覧数
	// ハイスコアレート
	// 時間ログイン
	// バンドランク
	// ミニキャラ会話回数
	public enum MISSION_CONDITION_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>ユーザーランク</summary>
		USER_RANK = 1,

		/// <summary>ユーザーコメント変更</summary>
		USER_UPDATE_COMMENT_COUNT = 2,

		/// <summary>ユーザー称号変更</summary>
		USER_UPDATE_TITLE_COUNT = 3,

		/// <summary>ユーザー推しカード変更</summary>
		USER_UPDATE_FAVORITE_CARD_COUNT = 4,

		/// <summary>ユーザー推しキャラクター変更</summary>
		USER_UPDATE_FAVORITE_CHARACTER_COUNT = 5,

		/// <summary>ユーザー推しバンド変更</summary>
		USER_UPDATE_FAVORITE_GROUP_COUNT = 6,

		/// <summary>ライブクリア</summary>
		LIVE_CLEAR_COUNT = 7,

		/// <summary>ライブブーストを設定してクリア</summary>
		LIVE_CLEAR_BOOST_COUNT = 8,

		/// <summary>指定楽曲ライブクリア</summary>
		LIVE_CLEAR_MUSIC_COUNT = 9,

		/// <summary>指定楽曲カテゴリライブクリア</summary>
		LIVE_CLEAR_MUSIC_CATEGORY_COUNT = 10,

		/// <summary>ライブスコアランク</summary>
		LIVE_SCORE_RANK_COUNT = 11,

		/// <summary>難易度レベルフルコンボ数</summary>
		LIVE_FULL_COMBO_LEVEL_COUNT = 12,

		/// <summary>総合力</summary>
		LIVE_TOTAL_PARAM = 13,

		/// <summary>協力ライブクリア</summary>
		MULTI_LIVE_CLEAR_COUNT = 14,

		/// <summary>カード数</summary>
		CARD_COUNT = 15,

		/// <summary>指定レアリティカード数</summary>
		CARD_RARE_COUNT = 16,

		/// <summary>指定レベルカード数</summary>
		CARD_LEVEL_COUNT = 17,

		/// <summary>カード練習</summary>
		CARD_REINFORCE_COUNT = 18,

		/// <summary>カードスキル練習</summary>
		CARD_REINFORCE_SKILL_COUNT = 19,

		/// <summary>カード特訓</summary>
		CARD_EVOLVE_COUNT = 20,

		/// <summary>指定カード特訓</summary>
		CARD_EVOLVE_TARGET_COUNT = 21,

		/// <summary>カード衣装変更</summary>
		CARD_UPDATE_COSTUME_COUNT = 22,

		/// <summary>備品購入種類</summary>
		ORNAMENT_BUY_COUNT = 23,

		/// <summary>備品設置回数</summary>
		ORNAMENT_SETTING_COUNT = 24,

		/// <summary>楽曲購入種類</summary>
		MUSIC_BUY_COUNT = 25,

		/// <summary>ポイント収集量</summary>
		POINT_COUNT = 26,

		/// <summary>フレンド数</summary>
		FRIEND_COUNT = 27,

		/// <summary>スタンプ回数</summary>
		STAMP_COUNT = 28,

		/// <summary>デイリーミッション全てクリア</summary>
		DAILY_MISSION_COMPLETE = 29,

		/// <summary>データ引き継ぎ設定</summary>
		SET_MIGRATION = 30,

		/// <summary>公式Twitterフォロー</summary>
		OFFICIAL_TWITTER_FOLLOW = 31,

		/// <summary>公式LINEフォロー</summary>
		OFFICIAL_LINE_FOLLOW = 32,

		/// <summary>ライブスコア</summary>
		LIVE_SCORE = 33,

		/// <summary>ストーリークリア</summary>
		STORY_CLEAR = 34,

		/// <summary>xタイプn人のデッキ作成</summary>
		DECK_CARD_TYPE = 35,

		/// <summary>キャラxのフォトをn枚最大レベルにする</summary>
		CHARACTER_MAX_LEVEL_COUNT = 36,

		/// <summary>曲xを全難易度でクリア</summary>
		LIVE_CLEAR_ALL_LEVEL = 37,

		/// <summary>曲xを全難易度でフルコンボ</summary>
		LIVE_CLEAR_FULL_COMBO_ALL_LEVEL = 38,

		/// <summary>指定ライブクリア回数</summary>
		LIVE_CLEAR_TARGET_COUNT = 39,

		/// <summary>指定ライブの指定難易度クリア回数</summary>
		LIVE_CLEAR_TARGET_AND_LEVEL_COUNT = 40,

		/// <summary>指定キャラクターを編成して指定ライブクリア回数</summary>
		LIVE_CLEAR_TARGET_CHARACTER_AND_LIVE_COUNT = 41,

		/// <summary>指定タイプを編成して指定ライブクリア回数</summary>
		LIVE_CLEAR_TARGET_CARD_TYPE_AND_LIVE_COUNT = 42,

		/// <summary>イベントポイント収集量</summary>
		EVENT_POINT_COUNT = 43,

		/// <summary>イベントライブクリア回数</summary>
		EVENT_LIVE_CLEAR_COUNT = 44,

		/// <summary>イベントライブフルコンボ回数</summary>
		EVENT_LIVE_FULL_COMBO_COUNT = 45,

		/// <summary>指定カードレベル</summary>
		TARGET_CARD_LEVEL = 46,

		/// <summary>イベントライブクリア(2回目用)</summary>
		EXTRA_EVENT_LIVE_CLEAR_COUNT = 47,

		/// <summary>イベントライブフルコンボ回数(2回目用)</summary>
		EXTRA_EVENT_LIVE_FULL_COMBO_COUNT = 48,

		/// <summary>指定ライブの指定難易度クリア回数(2回目用)</summary>
		EXTRA_LIVE_CLEAR_TARGET_AND_LEVEL_COUNT = 49,

		/// <summary>スコアx以上でy回クリア</summary>
		LIVE_SCORE_COUNT = 50,

		/// <summary>ライフx以上でy回クリア</summary>
		LIVE_LIFE_COUNT = 51,

		/// <summary>ノーツx判定y以内でz回クリア</summary>
		LIVE_NOTES_JUDGEMENT_COUNT = 52,

		/// <summary>コンボx以上でy回クリア</summary>
		LIVE_COMBO_COUNT = 53,

		/// <summary>フルコンボx回でクリア</summary>
		LIVE_FULL_COMBO_COUNT = 54,

		/// <summary>キャラクター(複数可)を入れてx回クリア</summary>
		LIVE_CHARACTER_COUNT = 55,

		/// <summary>カードタイプ(複数可)を入れてx回クリア</summary>
		LIVE_CARD_TYPE_COUNT = 56,

		/// <summary>マルチライブスコアランクx以上でy回クリア</summary>
		MULTI_LIVE_SCORE_RANK_COUNT = 57,

		/// <summary>ログイン回数</summary>
		LOGIN_COUNT = 58,

		/// <summary>指定カード入手</summary>
		TARGET_CARD_OBTAIN = 59,

		/// <summary>指定楽曲入手</summary>
		TARGET_MUSIC_OBTAIN = 60,

		/// <summary>エピソード解放数</summary>
		EPISODE_UNLOCK_COUNT = 61,

		/// <summary>エピソード閲覧数</summary>
		EPISODE_READ_COUNT = 62,

		/// <summary>ハイスコアレート</summary>
		HIGH_SCORE_RATE = 63,

		/// <summary>時間ログイン</summary>
		TIME_LOGIN = 64,

		/// <summary>バンドランク</summary>
		GROUP_RANK = 65,

		/// <summary>ミニキャラ会話回数</summary>
		TALK_COUNT = 66,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>SCORE:1</para>
	/// <para>COMBO:2</para>
	/// <para>FULL_COMBO:3</para>
	/// <para>CLEAR_COUNT:4</para>
	/// <para>PERFECT_FULL_COMBO:5</para>
	/// </summary>
	// 矩形選択用のコメント
	// LIVE_MISSION_TYPE.NONE
	// LIVE_MISSION_TYPE.SCORE
	// LIVE_MISSION_TYPE.COMBO
	// LIVE_MISSION_TYPE.FULL_COMBO
	// LIVE_MISSION_TYPE.CLEAR_COUNT
	// LIVE_MISSION_TYPE.PERFECT_FULL_COMBO
	// 無し
	// スコア
	// コンボ数
	// フルコンボ
	// クリア回数
	// PERFECTフルコンボ
	public enum LIVE_MISSION_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>スコア</summary>
		SCORE = 1,

		/// <summary>コンボ数</summary>
		COMBO = 2,

		/// <summary>フルコンボ</summary>
		FULL_COMBO = 3,

		/// <summary>クリア回数</summary>
		CLEAR_COUNT = 4,

		/// <summary>PERFECTフルコンボ</summary>
		PERFECT_FULL_COMBO = 5,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>GIFT:1</para>
	/// <para>DIRECT:2</para>
	/// </summary>
	// 矩形選択用のコメント
	// GIVE_TYPE.NONE
	// GIVE_TYPE.GIFT
	// GIVE_TYPE.DIRECT
	// 無し
	// ギフト
	// 直接
	public enum GIVE_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>ギフト</summary>
		GIFT = 1,

		/// <summary>直接</summary>
		DIRECT = 2,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>LOCK:1</para>
	/// <para>UNLOCK:2</para>
	/// <para>EXECUTED:3</para>
	/// </summary>
	// 矩形選択用のコメント
	// LOCK_STATUS.NONE
	// LOCK_STATUS.LOCK
	// LOCK_STATUS.UNLOCK
	// LOCK_STATUS.EXECUTED
	// 無し
	// 未解放
	// 解放
	// 実行済
	public enum LOCK_STATUS : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>未解放</summary>
		LOCK = 1,

		/// <summary>解放</summary>
		UNLOCK = 2,

		/// <summary>実行済</summary>
		EXECUTED = 3,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>NORMAL:1</para>
	/// <para>SPECIAL:2</para>
	/// </summary>
	// 矩形選択用のコメント
	// CARD_EPISODE_TYPE.NONE
	// CARD_EPISODE_TYPE.NORMAL
	// CARD_EPISODE_TYPE.SPECIAL
	// 無し
	// 通常
	// スペシャル
	public enum CARD_EPISODE_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>通常</summary>
		NORMAL = 1,

		/// <summary>スペシャル</summary>
		SPECIAL = 2,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>USER_ID:1</para>
	/// <para>GROUP_RANK:2</para>
	/// <para>LAST_LOGIN_DATE:3</para>
	/// <para>MAIN_DECK_PARAM:4</para>
	/// <para>CLEAR_LIVE_NUMBER:5</para>
	/// <para>FULL_COMBO_LIVE_NUMBER:6</para>
	/// <para>ALL_PERFECT_LIVE_NUMBER:7</para>
	/// <para>HIGH_SCORE_RATE:8</para>
	/// <para>FRIEND_REQUEST:9</para>
	/// </summary>
	// 矩形選択用のコメント
	// PROFILE_TYPE.NONE
	// PROFILE_TYPE.USER_ID
	// PROFILE_TYPE.GROUP_RANK
	// PROFILE_TYPE.LAST_LOGIN_DATE
	// PROFILE_TYPE.MAIN_DECK_PARAM
	// PROFILE_TYPE.CLEAR_LIVE_NUMBER
	// PROFILE_TYPE.FULL_COMBO_LIVE_NUMBER
	// PROFILE_TYPE.ALL_PERFECT_LIVE_NUMBER
	// PROFILE_TYPE.HIGH_SCORE_RATE
	// PROFILE_TYPE.FRIEND_REQUEST
	// 無し
	// プレイヤーID
	// バンドランク
	// 最終ログイン日時
	// バンド総合力
	// クリア楽曲数
	// フルコンボ楽曲数
	// オールパーフェクト楽曲数
	// ハイスコアレート
	// フレンド申請可否
	public enum PROFILE_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>プレイヤーID</summary>
		USER_ID = 1,

		/// <summary>バンドランク</summary>
		GROUP_RANK = 2,

		/// <summary>最終ログイン日時</summary>
		LAST_LOGIN_DATE = 3,

		/// <summary>バンド総合力</summary>
		MAIN_DECK_PARAM = 4,

		/// <summary>クリア楽曲数</summary>
		CLEAR_LIVE_NUMBER = 5,

		/// <summary>フルコンボ楽曲数</summary>
		FULL_COMBO_LIVE_NUMBER = 6,

		/// <summary>オールパーフェクト楽曲数</summary>
		ALL_PERFECT_LIVE_NUMBER = 7,

		/// <summary>ハイスコアレート</summary>
		HIGH_SCORE_RATE = 8,

		/// <summary>フレンド申請可否</summary>
		FRIEND_REQUEST = 9,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>NORMAL:1</para>
	/// <para>STARTUP:2</para>
	/// <para>COMEBACK:3</para>
	/// <para>CALENDAR:4</para>
	/// <para>EVENT:5</para>
	/// </summary>
	// 矩形選択用のコメント
	// LOGIN_BONUS_TYPE.NONE
	// LOGIN_BONUS_TYPE.NORMAL
	// LOGIN_BONUS_TYPE.STARTUP
	// LOGIN_BONUS_TYPE.COMEBACK
	// LOGIN_BONUS_TYPE.CALENDAR
	// LOGIN_BONUS_TYPE.EVENT
	// 無し
	// 通常
	// スタートアップ
	// カムバック
	// カレンダー
	// イベント
	public enum LOGIN_BONUS_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>通常</summary>
		NORMAL = 1,

		/// <summary>スタートアップ</summary>
		STARTUP = 2,

		/// <summary>カムバック</summary>
		COMEBACK = 3,

		/// <summary>カレンダー</summary>
		CALENDAR = 4,

		/// <summary>イベント</summary>
		EVENT = 5,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>USER_RANK:1</para>
	/// <para>GROUP_LEVEL:2</para>
	/// <para>MISSION:3</para>
	/// <para>EVENT_POINT_RANKING:4</para>
	/// <para>SCORE_RANKING:5</para>
	/// <para>BINGO_EVENT:6</para>
	/// <para>BINGO_EVENT_EX:7</para>
	/// </summary>
	// 矩形選択用のコメント
	// TITLE_CONDITION_TYPE.NONE
	// TITLE_CONDITION_TYPE.USER_RANK
	// TITLE_CONDITION_TYPE.GROUP_LEVEL
	// TITLE_CONDITION_TYPE.MISSION
	// TITLE_CONDITION_TYPE.EVENT_POINT_RANKING
	// TITLE_CONDITION_TYPE.SCORE_RANKING
	// TITLE_CONDITION_TYPE.BINGO_EVENT
	// TITLE_CONDITION_TYPE.BINGO_EVENT_EX
	// 無し
	// ユーザランク
	// グループレベル
	// ミッション達成
	// イベントポイントランキング
	// スコアランキング
	// ビンゴイベント
	// ビンゴイベントEX
	public enum TITLE_CONDITION_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>ユーザランク</summary>
		USER_RANK = 1,

		/// <summary>グループレベル</summary>
		GROUP_LEVEL = 2,

		/// <summary>ミッション達成</summary>
		MISSION = 3,

		/// <summary>イベントポイントランキング</summary>
		EVENT_POINT_RANKING = 4,

		/// <summary>スコアランキング</summary>
		SCORE_RANKING = 5,

		/// <summary>ビンゴイベント</summary>
		BINGO_EVENT = 6,

		/// <summary>ビンゴイベントEX</summary>
		BINGO_EVENT_EX = 7,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>MUSIC_STORE:1</para>
	/// <para>VARIETY_STORE:2</para>
	/// <para>OTHER:3</para>
	/// </summary>
	// 矩形選択用のコメント
	// SHOP_TYPE.NONE
	// SHOP_TYPE.MUSIC_STORE
	// SHOP_TYPE.VARIETY_STORE
	// SHOP_TYPE.OTHER
	// 無し
	// 楽器屋
	// 雑貨屋
	// その他
	public enum SHOP_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>楽器屋</summary>
		MUSIC_STORE = 1,

		/// <summary>雑貨屋</summary>
		VARIETY_STORE = 2,

		/// <summary>その他</summary>
		OTHER = 3,
	}

	/// <summary>
	/// <para>SUNDAY:0</para>
	/// <para>MONDAY:1</para>
	/// <para>TUESDAY:2</para>
	/// <para>WEDNESDAY:3</para>
	/// <para>THURSDAY:4</para>
	/// <para>FRIDAY:5</para>
	/// <para>SATURDAY:6</para>
	/// <para>NONE:7</para>
	/// </summary>
	// 矩形選択用のコメント
	// DAY_OF_WEEK.SUNDAY
	// DAY_OF_WEEK.MONDAY
	// DAY_OF_WEEK.TUESDAY
	// DAY_OF_WEEK.WEDNESDAY
	// DAY_OF_WEEK.THURSDAY
	// DAY_OF_WEEK.FRIDAY
	// DAY_OF_WEEK.SATURDAY
	// DAY_OF_WEEK.NONE
	// 日曜日
	// 月曜日
	// 火曜日
	// 水曜日
	// 木曜日
	// 金曜日
	// 土曜日
	// 無し
	public enum DAY_OF_WEEK : int
	{
		/// <summary>日曜日</summary>
		SUNDAY = 0,

		/// <summary>月曜日</summary>
		MONDAY = 1,

		/// <summary>火曜日</summary>
		TUESDAY = 2,

		/// <summary>水曜日</summary>
		WEDNESDAY = 3,

		/// <summary>木曜日</summary>
		THURSDAY = 4,

		/// <summary>金曜日</summary>
		FRIDAY = 5,

		/// <summary>土曜日</summary>
		SATURDAY = 6,

		/// <summary>無し</summary>
		NONE = 7,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>SHOP:1</para>
	/// <para>MISSION:2</para>
	/// <para>EVENT:3</para>
	/// <para>STORY:4</para>
	/// <para>PLAYER_RANK:5</para>
	/// <para>ARTISTS_RANK:6</para>
	/// </summary>
	// 矩形選択用のコメント
	// OBTAIN_TYPE.NONE
	// OBTAIN_TYPE.SHOP
	// OBTAIN_TYPE.MISSION
	// OBTAIN_TYPE.EVENT
	// OBTAIN_TYPE.STORY
	// OBTAIN_TYPE.PLAYER_RANK
	// OBTAIN_TYPE.ARTISTS_RANK
	// 無し
	// ショップ
	// ミッション
	// イベント
	// ストーリー
	// プレイヤーランク
	// アーティストランク
	public enum OBTAIN_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>ショップ</summary>
		SHOP = 1,

		/// <summary>ミッション</summary>
		MISSION = 2,

		/// <summary>イベント</summary>
		EVENT = 3,

		/// <summary>ストーリー</summary>
		STORY = 4,

		/// <summary>プレイヤーランク</summary>
		PLAYER_RANK = 5,

		/// <summary>アーティストランク</summary>
		ARTISTS_RANK = 6,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>DECK_POWER:1</para>
	/// </summary>
	// 矩形選択用のコメント
	// COSTUME_EFFECT_TYPE.NONE
	// COSTUME_EFFECT_TYPE.DECK_POWER
	// 無し
	// デッキの総合力n%UP
	public enum COSTUME_EFFECT_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>デッキの総合力n%UP</summary>
		DECK_POWER = 1,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>POWER_UP:1</para>
	/// <para>REWARD_UP:2</para>
	/// </summary>
	// 矩形選択用のコメント
	// ORNAMENT_EFFECT_TYPE.NONE
	// ORNAMENT_EFFECT_TYPE.POWER_UP
	// ORNAMENT_EFFECT_TYPE.REWARD_UP
	// 無し
	// キャラクターの総合力n%UP
	// ドロップn%UP
	public enum ORNAMENT_EFFECT_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>キャラクターの総合力n%UP</summary>
		POWER_UP = 1,

		/// <summary>ドロップn%UP</summary>
		REWARD_UP = 2,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>DECK_ROLE:1</para>
	/// <para>GROUP:2</para>
	/// <para>USER_EXP:3</para>
	/// <para>COIN:4</para>
	/// <para>ITEM_CATEGORY:5</para>
	/// <para>CHARACTER:6</para>
	/// </summary>
	// 矩形選択用のコメント
	// ORNAMENT_EFFECT_TARGET_CATEGORY.NONE
	// ORNAMENT_EFFECT_TARGET_CATEGORY.DECK_ROLE
	// ORNAMENT_EFFECT_TARGET_CATEGORY.GROUP
	// ORNAMENT_EFFECT_TARGET_CATEGORY.USER_EXP
	// ORNAMENT_EFFECT_TARGET_CATEGORY.COIN
	// ORNAMENT_EFFECT_TARGET_CATEGORY.ITEM_CATEGORY
	// ORNAMENT_EFFECT_TARGET_CATEGORY.CHARACTER
	// 全体
	// デッキ内の役割
	// グループ
	// ユーザ経験値
	// 部費
	// アイテムカテゴリ
	// キャラクター
	public enum ORNAMENT_EFFECT_TARGET_CATEGORY : int
	{
		/// <summary>全体</summary>
		NONE = 0,

		/// <summary>デッキ内の役割</summary>
		DECK_ROLE = 1,

		/// <summary>グループ</summary>
		GROUP = 2,

		/// <summary>ユーザ経験値</summary>
		USER_EXP = 3,

		/// <summary>部費</summary>
		COIN = 4,

		/// <summary>アイテムカテゴリ</summary>
		ITEM_CATEGORY = 5,

		/// <summary>キャラクター</summary>
		CHARACTER = 6,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>PLAYER_NAME:10</para>
	/// <para>FAVORITE_MEMBER:20</para>
	/// <para>ADV_1:30</para>
	/// <para>LIVE:40</para>
	/// <para>ADV_2:50</para>
	/// <para>MOVIE_PROLOGUE:60</para>
	/// <para>SUB_LIVE:70</para>
	/// <para>SUB_BEGINNER_MISSION:80</para>
	/// <para>SUB_STORY:90</para>
	/// <para>SUB_EXCHANGE_SHOP:100</para>
	/// <para>SUB_BOXGARDEN:110</para>
	/// <para>SUB_GACHA:120</para>
	/// <para>END:130</para>
	/// </summary>
	// 矩形選択用のコメント
	// TUTORIAL_STEP.NONE
	// TUTORIAL_STEP.PLAYER_NAME
	// TUTORIAL_STEP.FAVORITE_MEMBER
	// TUTORIAL_STEP.ADV_1
	// TUTORIAL_STEP.LIVE
	// TUTORIAL_STEP.ADV_2
	// TUTORIAL_STEP.MOVIE_PROLOGUE
	// TUTORIAL_STEP.SUB_LIVE
	// TUTORIAL_STEP.SUB_BEGINNER_MISSION
	// TUTORIAL_STEP.SUB_STORY
	// TUTORIAL_STEP.SUB_EXCHANGE_SHOP
	// TUTORIAL_STEP.SUB_BOXGARDEN
	// TUTORIAL_STEP.SUB_GACHA
	// TUTORIAL_STEP.END
	// 無し
	// プレイヤー名入力
	// 押しメンバー選択
	// ADV(ライブ前)
	// ライブ
	// ADV(ライブ後)
	// 動画
	// ライブ(サブチュートリアル)
	// 初心者ミッション(サブチュートリアル)
	// ストーリー(サブチュートリアル)
	// 交換所(サブチュートリアル)
	// ハコニワ(サブチュートリアル)
	// ガチャ(サブチュートリアル)
	// 終了
	public enum TUTORIAL_STEP : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>プレイヤー名入力</summary>
		PLAYER_NAME = 10,

		/// <summary>押しメンバー選択</summary>
		FAVORITE_MEMBER = 20,

		/// <summary>ADV(ライブ前)</summary>
		ADV_1 = 30,

		/// <summary>ライブ</summary>
		LIVE = 40,

		/// <summary>ADV(ライブ後)</summary>
		ADV_2 = 50,

		/// <summary>動画</summary>
		MOVIE_PROLOGUE = 60,

		/// <summary>ライブ(サブチュートリアル)</summary>
		SUB_LIVE = 70,

		/// <summary>初心者ミッション(サブチュートリアル)</summary>
		SUB_BEGINNER_MISSION = 80,

		/// <summary>ストーリー(サブチュートリアル)</summary>
		SUB_STORY = 90,

		/// <summary>交換所(サブチュートリアル)</summary>
		SUB_EXCHANGE_SHOP = 100,

		/// <summary>ハコニワ(サブチュートリアル)</summary>
		SUB_BOXGARDEN = 110,

		/// <summary>ガチャ(サブチュートリアル)</summary>
		SUB_GACHA = 120,

		/// <summary>終了</summary>
		END = 130,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>MEDAL:1</para>
	/// <para>PACK:2</para>
	/// </summary>
	// 矩形選択用のコメント
	// BILLING_PRODUCT_TYPE.NONE
	// BILLING_PRODUCT_TYPE.MEDAL
	// BILLING_PRODUCT_TYPE.PACK
	// 無し
	// ジェム
	// パック
	public enum BILLING_PRODUCT_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>ジェム</summary>
		MEDAL = 1,

		/// <summary>パック</summary>
		PACK = 2,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>PREMIUM:1</para>
	/// <para>STEP_UP:2</para>
	/// <para>BOX:3</para>
	/// </summary>
	// 矩形選択用のコメント
	// LOTTERY_TYPE.NONE
	// LOTTERY_TYPE.PREMIUM
	// LOTTERY_TYPE.STEP_UP
	// LOTTERY_TYPE.BOX
	// 無し
	// 通常ガチャ
	// ステップアップ
	// ボックスガチャ
	public enum LOTTERY_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>通常ガチャ</summary>
		PREMIUM = 1,

		/// <summary>ステップアップ</summary>
		STEP_UP = 2,

		/// <summary>ボックスガチャ</summary>
		BOX = 3,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>COIN:1</para>
	/// <para>ITEM:2</para>
	/// </summary>
	// 矩形選択用のコメント
	// MULTI_DAILY_DROP_EFFECT_TARGET_CATEGORY.NONE
	// MULTI_DAILY_DROP_EFFECT_TARGET_CATEGORY.COIN
	// MULTI_DAILY_DROP_EFFECT_TARGET_CATEGORY.ITEM
	// 無し
	// ゴールド
	// アイテム
	public enum MULTI_DAILY_DROP_EFFECT_TARGET_CATEGORY : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>ゴールド</summary>
		COIN = 1,

		/// <summary>アイテム</summary>
		ITEM = 2,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>NORMAL:1</para>
	/// <para>SPIRIT:2</para>
	/// <para>BINGO:3</para>
	/// <para>HOMEWORK:4</para>
	/// <para>TEACHER:5</para>
	/// <para>CANDY:6</para>
	/// </summary>
	// 矩形選択用のコメント
	// EVENT_TYPE.NONE
	// EVENT_TYPE.NORMAL
	// EVENT_TYPE.SPIRIT
	// EVENT_TYPE.BINGO
	// EVENT_TYPE.HOMEWORK
	// EVENT_TYPE.TEACHER
	// EVENT_TYPE.CANDY
	// 指定なし
	// 通常イベント
	// スピリットイベント(不要)
	// ビンゴイベント(不要)
	// 宿題イベント(不要)
	// 先生イベント(不要)
	// キャンディイベント(不要)
	public enum EVENT_TYPE : int
	{
		/// <summary>指定なし</summary>
		NONE = 0,

		/// <summary>通常イベント</summary>
		NORMAL = 1,

		/// <summary>スピリットイベント(不要)</summary>
		SPIRIT = 2,

		/// <summary>ビンゴイベント(不要)</summary>
		BINGO = 3,

		/// <summary>宿題イベント(不要)</summary>
		HOMEWORK = 4,

		/// <summary>先生イベント(不要)</summary>
		TEACHER = 5,

		/// <summary>キャンディイベント(不要)</summary>
		CANDY = 6,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>EVENT_POINT:1</para>
	/// <para>EVENT_STAMINA:2</para>
	/// <para>EVENT:3</para>
	/// <para>SPIRIT:4</para>
	/// <para>PICK:5</para>
	/// <para>COIN:6</para>
	/// <para>CANDY:7</para>
	/// </summary>
	// 矩形選択用のコメント
	// EVENT_POINT_TYPE.NONE
	// EVENT_POINT_TYPE.EVENT_POINT
	// EVENT_POINT_TYPE.EVENT_STAMINA
	// EVENT_POINT_TYPE.EVENT
	// EVENT_POINT_TYPE.SPIRIT
	// EVENT_POINT_TYPE.PICK
	// EVENT_POINT_TYPE.COIN
	// EVENT_POINT_TYPE.CANDY
	// 無し
	// イベントポイント
	// イベントスタミナ
	// イベント(不要)
	// スピリット(不要)
	// ピック(不要)
	// コイン(不要)
	// キャンディ(不要)
	public enum EVENT_POINT_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>イベントポイント</summary>
		EVENT_POINT = 1,

		/// <summary>イベントスタミナ</summary>
		EVENT_STAMINA = 2,

		/// <summary>イベント(不要)</summary>
		EVENT = 3,

		/// <summary>スピリット(不要)</summary>
		SPIRIT = 4,

		/// <summary>ピック(不要)</summary>
		PICK = 5,

		/// <summary>コイン(不要)</summary>
		COIN = 6,

		/// <summary>キャンディ(不要)</summary>
		CANDY = 7,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>CARD_ID:1</para>
	/// <para>CARD_TYPE:2</para>
	/// <para>CHARACTER:3</para>
	/// </summary>
	// 矩形選択用のコメント
	// EVENT_BONUS_TARGET_TYPE.NONE
	// EVENT_BONUS_TARGET_TYPE.CARD_ID
	// EVENT_BONUS_TARGET_TYPE.CARD_TYPE
	// EVENT_BONUS_TARGET_TYPE.CHARACTER
	// 無し
	// カードID
	// カードタイプ
	// キャラクター
	public enum EVENT_BONUS_TARGET_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>カードID</summary>
		CARD_ID = 1,

		/// <summary>カードタイプ</summary>
		CARD_TYPE = 2,

		/// <summary>キャラクター</summary>
		CHARACTER = 3,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>DROP:1</para>
	/// </summary>
	// 矩形選択用のコメント
	// EVENT_BOOST_TYPE.NONE
	// EVENT_BOOST_TYPE.DROP
	// 無し
	// ドロップ報酬
	public enum EVENT_BOOST_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>ドロップ報酬</summary>
		DROP = 1,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>POINT:1</para>
	/// <para>SCORE:2</para>
	/// <para>EMOTIONAL:3</para>
	/// </summary>
	// 矩形選択用のコメント
	// RANKING_TYPE.NONE
	// RANKING_TYPE.POINT
	// RANKING_TYPE.SCORE
	// RANKING_TYPE.EMOTIONAL
	// 無し
	// ポイントランキング
	// スコアランキング
	// エモーショナルランキング
	public enum RANKING_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>ポイントランキング</summary>
		POINT = 1,

		/// <summary>スコアランキング</summary>
		SCORE = 2,

		/// <summary>エモーショナルランキング</summary>
		EMOTIONAL = 3,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>ALL:1</para>
	/// <para>GROUP:2</para>
	/// </summary>
	// 矩形選択用のコメント
	// RANKING_GROUP_TYPE.NONE
	// RANKING_GROUP_TYPE.ALL
	// RANKING_GROUP_TYPE.GROUP
	// 無し
	// 全体
	// グループ
	public enum RANKING_GROUP_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>全体</summary>
		ALL = 1,

		/// <summary>グループ</summary>
		GROUP = 2,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>CHARACTER:1</para>
	/// <para>GROUP:2</para>
	/// </summary>
	// 矩形選択用のコメント
	// EMOTIONAL_RANKING_TYPE.NONE
	// EMOTIONAL_RANKING_TYPE.CHARACTER
	// EMOTIONAL_RANKING_TYPE.GROUP
	// 無し
	// メンバー別
	// バンド別
	public enum EMOTIONAL_RANKING_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>メンバー別</summary>
		CHARACTER = 1,

		/// <summary>バンド別</summary>
		GROUP = 2,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>CARD:1</para>
	/// <para>ITEM:2</para>
	/// </summary>
	// 矩形選択用のコメント
	// EVOLVE_TYPE.NONE
	// EVOLVE_TYPE.CARD
	// EVOLVE_TYPE.ITEM
	// 無し
	// カード
	// アイテム
	public enum EVOLVE_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>カード</summary>
		CARD = 1,

		/// <summary>アイテム</summary>
		ITEM = 2,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>CARD_ID:1</para>
	/// <para>CARD_TYPE:2</para>
	/// <para>CHARACTER_ID:3</para>
	/// <para>BAND_CATEGORY:4</para>
	/// <para>RARITY:5</para>
	/// </summary>
	// 矩形選択用のコメント
	// EVOLVE_ITEM_CONDITION_TYPE.NONE
	// EVOLVE_ITEM_CONDITION_TYPE.CARD_ID
	// EVOLVE_ITEM_CONDITION_TYPE.CARD_TYPE
	// EVOLVE_ITEM_CONDITION_TYPE.CHARACTER_ID
	// EVOLVE_ITEM_CONDITION_TYPE.BAND_CATEGORY
	// EVOLVE_ITEM_CONDITION_TYPE.RARITY
	// 無し
	// カードID指定
	// カードタイプ指定
	// キャラクターID指定
	// バンド指定
	// レアリティ指定
	public enum EVOLVE_ITEM_CONDITION_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>カードID指定</summary>
		CARD_ID = 1,

		/// <summary>カードタイプ指定</summary>
		CARD_TYPE = 2,

		/// <summary>キャラクターID指定</summary>
		CHARACTER_ID = 3,

		/// <summary>バンド指定</summary>
		BAND_CATEGORY = 4,

		/// <summary>レアリティ指定</summary>
		RARITY = 5,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>LOTTERY:1</para>
	/// <para>SHOP:2</para>
	/// <para>EXCHANGE:3</para>
	/// <para>INCENTIVE_LOTTERY:4</para>
	/// <para>FREE_LIVE:5</para>
	/// <para>EVENT_LIVE:6</para>
	/// <para>MULTI_LIVE:7</para>
	/// </summary>
	// 矩形選択用のコメント
	// FUNCTION_ID.NONE
	// FUNCTION_ID.LOTTERY
	// FUNCTION_ID.SHOP
	// FUNCTION_ID.EXCHANGE
	// FUNCTION_ID.INCENTIVE_LOTTERY
	// FUNCTION_ID.FREE_LIVE
	// FUNCTION_ID.EVENT_LIVE
	// FUNCTION_ID.MULTI_LIVE
	// 無し
	// ガチャ
	// ショップ
	// 交換所
	// リアルインセンティブ抽選
	// フリーライブ
	// イベントライブ
	// マルチライブ
	public enum FUNCTION_ID : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>ガチャ</summary>
		LOTTERY = 1,

		/// <summary>ショップ</summary>
		SHOP = 2,

		/// <summary>交換所</summary>
		EXCHANGE = 3,

		/// <summary>リアルインセンティブ抽選</summary>
		INCENTIVE_LOTTERY = 4,

		/// <summary>フリーライブ</summary>
		FREE_LIVE = 5,

		/// <summary>イベントライブ</summary>
		EVENT_LIVE = 6,

		/// <summary>マルチライブ</summary>
		MULTI_LIVE = 7,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>COMBO:1</para>
	/// <para>FULL_COMBO:2</para>
	/// <para>NOTES_JUDGEMENT:3</para>
	/// <para>MISS:4</para>
	/// <para>LIFE:5</para>
	/// <para>SKILL:6</para>
	/// <para>SCORE:7</para>
	/// <para>TOTAL_SCORE:8</para>
	/// </summary>
	// 矩形選択用のコメント
	// HOMEWORK_TYPE.NONE
	// HOMEWORK_TYPE.COMBO
	// HOMEWORK_TYPE.FULL_COMBO
	// HOMEWORK_TYPE.NOTES_JUDGEMENT
	// HOMEWORK_TYPE.MISS
	// HOMEWORK_TYPE.LIFE
	// HOMEWORK_TYPE.SKILL
	// HOMEWORK_TYPE.SCORE
	// HOMEWORK_TYPE.TOTAL_SCORE
	// 無し
	// コンボ
	// フルコンボ
	// ノーツ判定
	// ミス回数
	// ライフ
	// スキル
	// スコア
	// 合計スコア
	public enum HOMEWORK_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>コンボ</summary>
		COMBO = 1,

		/// <summary>フルコンボ</summary>
		FULL_COMBO = 2,

		/// <summary>ノーツ判定</summary>
		NOTES_JUDGEMENT = 3,

		/// <summary>ミス回数</summary>
		MISS = 4,

		/// <summary>ライフ</summary>
		LIFE = 5,

		/// <summary>スキル</summary>
		SKILL = 6,

		/// <summary>スコア</summary>
		SCORE = 7,

		/// <summary>合計スコア</summary>
		TOTAL_SCORE = 8,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>CHARACTER:1</para>
	/// <para>GROUP:2</para>
	/// <para>CARD_TYPE:3</para>
	/// <para>DECK_ROLE:4</para>
	/// <para>CARD:5</para>
	/// <para>USER_RANK:6</para>
	/// <para>TITLE:7</para>
	/// <para>ITEM:8</para>
	/// </summary>
	// 矩形選択用のコメント
	// LIVE_CONDITION_TYPE.NONE
	// LIVE_CONDITION_TYPE.CHARACTER
	// LIVE_CONDITION_TYPE.GROUP
	// LIVE_CONDITION_TYPE.CARD_TYPE
	// LIVE_CONDITION_TYPE.DECK_ROLE
	// LIVE_CONDITION_TYPE.CARD
	// LIVE_CONDITION_TYPE.USER_RANK
	// LIVE_CONDITION_TYPE.TITLE
	// LIVE_CONDITION_TYPE.ITEM
	// 無し
	// キャラクター限定
	// キャラクターグループ限定
	// フォトタイプ限定
	// パート属性限定
	// フォト限定
	// ユーザーランク限定
	// 称号限定
	// アイテム使用
	public enum LIVE_CONDITION_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>キャラクター限定</summary>
		CHARACTER = 1,

		/// <summary>キャラクターグループ限定</summary>
		GROUP = 2,

		/// <summary>フォトタイプ限定</summary>
		CARD_TYPE = 3,

		/// <summary>パート属性限定</summary>
		DECK_ROLE = 4,

		/// <summary>フォト限定</summary>
		CARD = 5,

		/// <summary>ユーザーランク限定</summary>
		USER_RANK = 6,

		/// <summary>称号限定</summary>
		TITLE = 7,

		/// <summary>アイテム使用</summary>
		ITEM = 8,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>POWER_UP:1</para>
	/// </summary>
	// 矩形選択用のコメント
	// CHARACTER_EPISODE_EFFECT_TYPE.NONE
	// CHARACTER_EPISODE_EFFECT_TYPE.POWER_UP
	// 無し
	// キャラクターの総合力n%UP
	public enum CHARACTER_EPISODE_EFFECT_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>キャラクターの総合力n%UP</summary>
		POWER_UP = 1,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>RARE1:1</para>
	/// <para>RARE2:2</para>
	/// <para>RARE3:3</para>
	/// <para>RARE4:4</para>
	/// </summary>
	// 矩形選択用のコメント
	// CHARACTER_EPISODE_EXP.NONE
	// CHARACTER_EPISODE_EXP.RARE1
	// CHARACTER_EPISODE_EXP.RARE2
	// CHARACTER_EPISODE_EXP.RARE3
	// CHARACTER_EPISODE_EXP.RARE4
	// 無し
	// レア1
	// レア2
	// レア3
	// レア4
	public enum CHARACTER_EPISODE_EXP : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>レア1</summary>
		RARE1 = 1,

		/// <summary>レア2</summary>
		RARE2 = 2,

		/// <summary>レア3</summary>
		RARE3 = 3,

		/// <summary>レア4</summary>
		RARE4 = 4,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>DAILY:1</para>
	/// <para>WEEKLY:2</para>
	/// <para>MONTHLY:3</para>
	/// </summary>
	// 矩形選択用のコメント
	// TIME_RESET_TYPE.NONE
	// TIME_RESET_TYPE.DAILY
	// TIME_RESET_TYPE.WEEKLY
	// TIME_RESET_TYPE.MONTHLY
	// 無し
	// 日
	// 週
	// 月
	public enum TIME_RESET_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>日</summary>
		DAILY = 1,

		/// <summary>週</summary>
		WEEKLY = 2,

		/// <summary>月</summary>
		MONTHLY = 3,
	}

	/// <summary>
	/// <para>MAIN:0</para>
	/// <para>SUB:1</para>
	/// <para>CARD:2</para>
	/// <para>MINI_CHARA:3</para>
	/// <para>SYSTEM:4</para>
	/// <para>EVENT:5</para>
	/// <para>NOTICE:6</para>
	/// <para>EPISODE:7</para>
	/// <para>DEBUG:8</para>
	/// </summary>
	// 矩形選択用のコメント
	// ADV_SCENARIO_TYPE.MAIN
	// ADV_SCENARIO_TYPE.SUB
	// ADV_SCENARIO_TYPE.CARD
	// ADV_SCENARIO_TYPE.MINI_CHARA
	// ADV_SCENARIO_TYPE.SYSTEM
	// ADV_SCENARIO_TYPE.EVENT
	// ADV_SCENARIO_TYPE.NOTICE
	// ADV_SCENARIO_TYPE.EPISODE
	// ADV_SCENARIO_TYPE.DEBUG
	// メイン
	// サブ
	// カード
	// ミニキャラ
	// システム
	// イベント
	// 告知
	// エピソード
	// デバッグ
	public enum ADV_SCENARIO_TYPE : int
	{
		/// <summary>メイン</summary>
		MAIN = 0,

		/// <summary>サブ</summary>
		SUB = 1,

		/// <summary>カード</summary>
		CARD = 2,

		/// <summary>ミニキャラ</summary>
		MINI_CHARA = 3,

		/// <summary>システム</summary>
		SYSTEM = 4,

		/// <summary>イベント</summary>
		EVENT = 5,

		/// <summary>告知</summary>
		NOTICE = 6,

		/// <summary>エピソード</summary>
		EPISODE = 7,

		/// <summary>デバッグ</summary>
		DEBUG = 8,
	}

	/// <summary>
	/// <para>MOB:0</para>
	/// <para>NORMAL:1</para>
	/// <para>TUTORIAL:2</para>
	/// <para>NOTICE:3</para>
	/// <para>BIRTHDAY:4</para>
	/// </summary>
	// 矩形選択用のコメント
	// AREA_EVENT_TYPE.MOB
	// AREA_EVENT_TYPE.NORMAL
	// AREA_EVENT_TYPE.TUTORIAL
	// AREA_EVENT_TYPE.NOTICE
	// AREA_EVENT_TYPE.BIRTHDAY
	// モブ
	// 通常
	// チュートリアル
	// 告知
	// 誕生日
	public enum AREA_EVENT_TYPE : int
	{
		/// <summary>モブ</summary>
		MOB = 0,

		/// <summary>通常</summary>
		NORMAL = 1,

		/// <summary>チュートリアル</summary>
		TUTORIAL = 2,

		/// <summary>告知</summary>
		NOTICE = 3,

		/// <summary>誕生日</summary>
		BIRTHDAY = 4,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>STORY_READ:1</para>
	/// <para>USER_RANK:2</para>
	/// <para>TOTAL_GROUP_RANK:3</para>
	/// </summary>
	// 矩形選択用のコメント
	// AREA_UNLOCK_CONDITION_TYPE.NONE
	// AREA_UNLOCK_CONDITION_TYPE.STORY_READ
	// AREA_UNLOCK_CONDITION_TYPE.USER_RANK
	// AREA_UNLOCK_CONDITION_TYPE.TOTAL_GROUP_RANK
	// なし
	// ストーリーを読む
	// ユーザーランク
	// 総合ARTiSTSランク
	public enum AREA_UNLOCK_CONDITION_TYPE : int
	{
		/// <summary>なし</summary>
		NONE = 0,

		/// <summary>ストーリーを読む</summary>
		STORY_READ = 1,

		/// <summary>ユーザーランク</summary>
		USER_RANK = 2,

		/// <summary>総合ARTiSTSランク</summary>
		TOTAL_GROUP_RANK = 3,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>MUSIC:1</para>
	/// <para>ORNAMENT:2</para>
	/// <para>POINT_EXCHANGE:3</para>
	/// </summary>
	// 矩形選択用のコメント
	// AREA_SHOP_TYPE.NONE
	// AREA_SHOP_TYPE.MUSIC
	// AREA_SHOP_TYPE.ORNAMENT
	// AREA_SHOP_TYPE.POINT_EXCHANGE
	// 指定なし
	// 楽曲ショップ
	// 備品ショップ
	// ポイント交換所
	public enum AREA_SHOP_TYPE : int
	{
		/// <summary>指定なし</summary>
		NONE = 0,

		/// <summary>楽曲ショップ</summary>
		MUSIC = 1,

		/// <summary>備品ショップ</summary>
		ORNAMENT = 2,

		/// <summary>ポイント交換所</summary>
		POINT_EXCHANGE = 3,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>STORY_READ:1</para>
	/// <para>USER_RANK:2</para>
	/// <para>GROUP_RANK:3</para>
	/// </summary>
	// 矩形選択用のコメント
	// TALK_UNLOCK_CONDITION_TYPE.NONE
	// TALK_UNLOCK_CONDITION_TYPE.STORY_READ
	// TALK_UNLOCK_CONDITION_TYPE.USER_RANK
	// TALK_UNLOCK_CONDITION_TYPE.GROUP_RANK
	// なし
	// ストーリーを読む
	// ユーザーランク
	// グループランク
	public enum TALK_UNLOCK_CONDITION_TYPE : int
	{
		/// <summary>なし</summary>
		NONE = 0,

		/// <summary>ストーリーを読む</summary>
		STORY_READ = 1,

		/// <summary>ユーザーランク</summary>
		USER_RANK = 2,

		/// <summary>グループランク</summary>
		GROUP_RANK = 3,
	}

	/// <summary>
	/// <para>ZERO:0</para>
	/// <para>LOGIN:1</para>
	/// </summary>
	// 矩形選択用のコメント
	// DATE_LINE_TYPE.ZERO
	// DATE_LINE_TYPE.LOGIN
	// 零時
	// ログイン
	public enum DATE_LINE_TYPE : int
	{
		/// <summary>零時</summary>
		ZERO = 0,

		/// <summary>ログイン</summary>
		LOGIN = 1,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>MAIN:1</para>
	/// <para>TEA:2</para>
	/// </summary>
	// 矩形選択用のコメント
	// EXCHANGE_TAB.NONE
	// EXCHANGE_TAB.MAIN
	// EXCHANGE_TAB.TEA
	// 指定なし
	// メイン
	// 紅茶
	public enum EXCHANGE_TAB : int
	{
		/// <summary>指定なし</summary>
		NONE = 0,

		/// <summary>メイン</summary>
		MAIN = 1,

		/// <summary>紅茶</summary>
		TEA = 2,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>POINT:1</para>
	/// <para>EVENT:2</para>
	/// <para>TICKET:3</para>
	/// <para>ITEM:4</para>
	/// </summary>
	// 矩形選択用のコメント
	// EXCHANGE_TYPE.NONE
	// EXCHANGE_TYPE.POINT
	// EXCHANGE_TYPE.EVENT
	// EXCHANGE_TYPE.TICKET
	// EXCHANGE_TYPE.ITEM
	// 指定なし
	// ポイント
	// イベント(ピック)
	// チケット
	// アイテム
	public enum EXCHANGE_TYPE : int
	{
		/// <summary>指定なし</summary>
		NONE = 0,

		/// <summary>ポイント</summary>
		POINT = 1,

		/// <summary>イベント(ピック)</summary>
		EVENT = 2,

		/// <summary>チケット</summary>
		TICKET = 3,

		/// <summary>アイテム</summary>
		ITEM = 4,
	}

	/// <summary>
	/// <para>LOTTERY:0</para>
	/// <para>EVENT:1</para>
	/// <para>SHOP:2</para>
	/// <para>CUSTOMIZE_SHOP:3</para>
	/// <para>NOTICE:4</para>
	/// <para>CAMPAIGN:5</para>
	/// </summary>
	// 矩形選択用のコメント
	// HOME_BANNER_TYPE.LOTTERY
	// HOME_BANNER_TYPE.EVENT
	// HOME_BANNER_TYPE.SHOP
	// HOME_BANNER_TYPE.CUSTOMIZE_SHOP
	// HOME_BANNER_TYPE.NOTICE
	// HOME_BANNER_TYPE.CAMPAIGN
	// ガチャ
	// イベント
	// ショップ
	// カスタマイズショップ
	// お知らせ
	// キャンペーン
	public enum HOME_BANNER_TYPE : int
	{
		/// <summary>ガチャ</summary>
		LOTTERY = 0,

		/// <summary>イベント</summary>
		EVENT = 1,

		/// <summary>ショップ</summary>
		SHOP = 2,

		/// <summary>カスタマイズショップ</summary>
		CUSTOMIZE_SHOP = 3,

		/// <summary>お知らせ</summary>
		NOTICE = 4,

		/// <summary>キャンペーン</summary>
		CAMPAIGN = 5,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>ENHANCEMENT:1</para>
	/// <para>BOOST:2</para>
	/// <para>TICKET:3</para>
	/// <para>OTHER:4</para>
	/// </summary>
	// 矩形選択用のコメント
	// ITEM_TAB.NONE
	// ITEM_TAB.ENHANCEMENT
	// ITEM_TAB.BOOST
	// ITEM_TAB.TICKET
	// ITEM_TAB.OTHER
	// 指定なし
	// 強化
	// 回復アイテム
	// チケット
	// その他
	public enum ITEM_TAB : int
	{
		/// <summary>指定なし</summary>
		NONE = 0,

		/// <summary>強化</summary>
		ENHANCEMENT = 1,

		/// <summary>回復アイテム</summary>
		BOOST = 2,

		/// <summary>チケット</summary>
		TICKET = 3,

		/// <summary>その他</summary>
		OTHER = 4,
	}

	/// <summary>
	/// <para>CENTER:0</para>
	/// <para>LEFT:1</para>
	/// <para>RIGHT:2</para>
	/// <para>LEFT_BACK:3</para>
	/// <para>RIGHT_BACK:4</para>
	/// </summary>
	// 矩形選択用のコメント
	// LIVE_POSITION.CENTER
	// LIVE_POSITION.LEFT
	// LIVE_POSITION.RIGHT
	// LIVE_POSITION.LEFT_BACK
	// LIVE_POSITION.RIGHT_BACK
	// 通常ボーカル位置
	// 通常ベース位置
	// 通常ギター位置
	// 通常キーボード位置
	// 通常ドラム位置
	public enum LIVE_POSITION : int
	{
		/// <summary>通常ボーカル位置</summary>
		CENTER = 0,

		/// <summary>通常ベース位置</summary>
		LEFT = 1,

		/// <summary>通常ギター位置</summary>
		RIGHT = 2,

		/// <summary>通常キーボード位置</summary>
		LEFT_BACK = 3,

		/// <summary>通常ドラム位置</summary>
		RIGHT_BACK = 4,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>NOTES_JUDGEMENT:1</para>
	/// </summary>
	// 矩形選択用のコメント
	// LIVE_RULE_TYPE.NONE
	// LIVE_RULE_TYPE.NOTES_JUDGEMENT
	// 無し
	// ノーツ判定
	public enum LIVE_RULE_TYPE : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>ノーツ判定</summary>
		NOTES_JUDGEMENT = 1,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>PLAY_WEAK:1</para>
	/// <para>PLAY_STRONG:2</para>
	/// <para>JUMP:3</para>
	/// <para>WAIT:4</para>
	/// <para>FAILURE_START:5</para>
	/// <para>FAILURE_LOOP:6</para>
	/// <para>INPUT_SYNC:7</para>
	/// <para>INPUT_FLICK:8</para>
	/// <para>INPUT_LONG_PRESS:9</para>
	/// <para>INPUT_SKILL:10</para>
	/// <para>RESULT_CHANGE_COSTUME:11</para>
	/// <para>RESULT_WAIT:12</para>
	/// <para>RESULT_JUMP:13</para>
	/// <para>COOP_DISCONNECT:14</para>
	/// </summary>
	// 矩形選択用のコメント
	// LIVE_SPINE_MOTION.NONE
	// LIVE_SPINE_MOTION.PLAY_WEAK
	// LIVE_SPINE_MOTION.PLAY_STRONG
	// LIVE_SPINE_MOTION.JUMP
	// LIVE_SPINE_MOTION.WAIT
	// LIVE_SPINE_MOTION.FAILURE_START
	// LIVE_SPINE_MOTION.FAILURE_LOOP
	// LIVE_SPINE_MOTION.INPUT_SYNC
	// LIVE_SPINE_MOTION.INPUT_FLICK
	// LIVE_SPINE_MOTION.INPUT_LONG_PRESS
	// LIVE_SPINE_MOTION.INPUT_SKILL
	// LIVE_SPINE_MOTION.RESULT_CHANGE_COSTUME
	// LIVE_SPINE_MOTION.RESULT_WAIT
	// LIVE_SPINE_MOTION.RESULT_JUMP
	// LIVE_SPINE_MOTION.COOP_DISCONNECT
	// なし
	// 演奏(弱)
	// 演奏(強)
	// ジャンプ
	// 待機
	// 残念がる(起始)
	// 残念がる(ループ)
	// 同時押し
	// フリック
	// 長押し中
	// スキル
	// ライブ終了時の衣装切り替え
	// ライブ終了時の待機
	// ライブ終了時のジャンプ
	// 協力ライブ切断
	public enum LIVE_SPINE_MOTION : int
	{
		/// <summary>なし</summary>
		NONE = 0,

		/// <summary>演奏(弱)</summary>
		PLAY_WEAK = 1,

		/// <summary>演奏(強)</summary>
		PLAY_STRONG = 2,

		/// <summary>ジャンプ</summary>
		JUMP = 3,

		/// <summary>待機</summary>
		WAIT = 4,

		/// <summary>残念がる(起始)</summary>
		FAILURE_START = 5,

		/// <summary>残念がる(ループ)</summary>
		FAILURE_LOOP = 6,

		/// <summary>同時押し</summary>
		INPUT_SYNC = 7,

		/// <summary>フリック</summary>
		INPUT_FLICK = 8,

		/// <summary>長押し中</summary>
		INPUT_LONG_PRESS = 9,

		/// <summary>スキル</summary>
		INPUT_SKILL = 10,

		/// <summary>ライブ終了時の衣装切り替え</summary>
		RESULT_CHANGE_COSTUME = 11,

		/// <summary>ライブ終了時の待機</summary>
		RESULT_WAIT = 12,

		/// <summary>ライブ終了時のジャンプ</summary>
		RESULT_JUMP = 13,

		/// <summary>協力ライブ切断</summary>
		COOP_DISCONNECT = 14,
	}

	/// <summary>
	/// <para>FREE:0</para>
	/// <para>COOP:1</para>
	/// <para>VS:2</para>
	/// <para>EVENT:3</para>
	/// <para>EVENT_HOMEWORK:4</para>
	/// <para>LIMITED:5</para>
	/// </summary>
	// 矩形選択用のコメント
	// LIVE_TYPE.FREE
	// LIVE_TYPE.COOP
	// LIVE_TYPE.VS
	// LIVE_TYPE.EVENT
	// LIVE_TYPE.EVENT_HOMEWORK
	// LIVE_TYPE.LIMITED
	// デフォルト
	// 協力
	// 対戦
	// イベント
	// 宿題イベント
	// 限定(キッチン)
	public enum LIVE_TYPE : int
	{
		/// <summary>デフォルト</summary>
		FREE = 0,

		/// <summary>協力</summary>
		COOP = 1,

		/// <summary>対戦</summary>
		VS = 2,

		/// <summary>イベント</summary>
		EVENT = 3,

		/// <summary>宿題イベント</summary>
		EVENT_HOMEWORK = 4,

		/// <summary>限定(キッチン)</summary>
		LIMITED = 5,
	}

	/// <summary>
	/// <para>FIRST:0</para>
	/// <para>LOGIN_BONUS:1</para>
	/// <para>FIXED_NOTICE:2</para>
	/// <para>MIDDLE:3</para>
	/// <para>REWARD:4</para>
	/// <para>NAVIGATION:5</para>
	/// <para>LAST:6</para>
	/// </summary>
	// 矩形選択用のコメント
	// LOGIN_NOTICE_PHASE.FIRST
	// LOGIN_NOTICE_PHASE.LOGIN_BONUS
	// LOGIN_NOTICE_PHASE.FIXED_NOTICE
	// LOGIN_NOTICE_PHASE.MIDDLE
	// LOGIN_NOTICE_PHASE.REWARD
	// LOGIN_NOTICE_PHASE.NAVIGATION
	// LOGIN_NOTICE_PHASE.LAST
	// 先頭汎用告知
	// ログインボーナス
	// 固定告知
	// 中間汎用告知
	// 報酬
	// ナビゲーション
	// 終盤汎用告知
	public enum LOGIN_NOTICE_PHASE : int
	{
		/// <summary>先頭汎用告知</summary>
		FIRST = 0,

		/// <summary>ログインボーナス</summary>
		LOGIN_BONUS = 1,

		/// <summary>固定告知</summary>
		FIXED_NOTICE = 2,

		/// <summary>中間汎用告知</summary>
		MIDDLE = 3,

		/// <summary>報酬</summary>
		REWARD = 4,

		/// <summary>ナビゲーション</summary>
		NAVIGATION = 5,

		/// <summary>終盤汎用告知</summary>
		LAST = 6,
	}

	/// <summary>
	/// <para>MOVIE:0</para>
	/// <para>ADV:1</para>
	/// <para>SPINE:2</para>
	/// <para>ANIMATION:3</para>
	/// </summary>
	// 矩形選択用のコメント
	// LOGIN_NOTICE_TYPE.MOVIE
	// LOGIN_NOTICE_TYPE.ADV
	// LOGIN_NOTICE_TYPE.SPINE
	// LOGIN_NOTICE_TYPE.ANIMATION
	// 動画
	// ADV
	// SPINE
	// ANIMATION
	public enum LOGIN_NOTICE_TYPE : int
	{
		/// <summary>動画</summary>
		MOVIE = 0,

		/// <summary>ADV</summary>
		ADV = 1,

		/// <summary>SPINE</summary>
		SPINE = 2,

		/// <summary>ANIMATION</summary>
		ANIMATION = 3,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>WHITE_OUT:1</para>
	/// <para>BLACK_OUT:2</para>
	/// </summary>
	// 矩形選択用のコメント
	// LOGIN_NOTICE_TRANSITION.NONE
	// LOGIN_NOTICE_TRANSITION.WHITE_OUT
	// LOGIN_NOTICE_TRANSITION.BLACK_OUT
	// なし
	// ホワイトアウト
	// ブラックアウト
	public enum LOGIN_NOTICE_TRANSITION : int
	{
		/// <summary>なし</summary>
		NONE = 0,

		/// <summary>ホワイトアウト</summary>
		WHITE_OUT = 1,

		/// <summary>ブラックアウト</summary>
		BLACK_OUT = 2,
	}

	/// <summary>
	/// <para>HAS_POINT:0</para>
	/// <para>HAS_ITEM:1</para>
	/// <para>LIVE_CLEAR_COUNT:2</para>
	/// <para>USER_RANK:3</para>
	/// </summary>
	// 矩形選択用のコメント
	// NAVIGATION_CONDITION_TYPE.HAS_POINT
	// NAVIGATION_CONDITION_TYPE.HAS_ITEM
	// NAVIGATION_CONDITION_TYPE.LIVE_CLEAR_COUNT
	// NAVIGATION_CONDITION_TYPE.USER_RANK
	// ポイント所持
	// アイテム所持
	// ライブクリア回数
	// ユーザーランク
	public enum NAVIGATION_CONDITION_TYPE : int
	{
		/// <summary>ポイント所持</summary>
		HAS_POINT = 0,

		/// <summary>アイテム所持</summary>
		HAS_ITEM = 1,

		/// <summary>ライブクリア回数</summary>
		LIVE_CLEAR_COUNT = 2,

		/// <summary>ユーザーランク</summary>
		USER_RANK = 3,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>BLUE:1</para>
	/// <para>GREEN:2</para>
	/// <para>PURPLE:3</para>
	/// <para>RED:4</para>
	/// <para>YELLOW:5</para>
	/// </summary>
	// 矩形選択用のコメント
	// PLATE_COLOR.NONE
	// PLATE_COLOR.BLUE
	// PLATE_COLOR.GREEN
	// PLATE_COLOR.PURPLE
	// PLATE_COLOR.RED
	// PLATE_COLOR.YELLOW
	// なし
	// 青
	// 緑
	// 紫
	// 赤
	// 黄
	public enum PLATE_COLOR : int
	{
		/// <summary>なし</summary>
		NONE = 0,

		/// <summary>青</summary>
		BLUE = 1,

		/// <summary>緑</summary>
		GREEN = 2,

		/// <summary>紫</summary>
		PURPLE = 3,

		/// <summary>赤</summary>
		RED = 4,

		/// <summary>黄</summary>
		YELLOW = 5,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>COMMON:1</para>
	/// <para>BILLING_PRODUCT:2</para>
	/// <para>EVENT_BINGO:3</para>
	/// <para>EVENT_HOMEWORK:4</para>
	/// <para>EVENT_POINT:5</para>
	/// <para>EVENT_SCORE_RANKING:6</para>
	/// <para>EXCHANGE_ITEM:7</para>
	/// <para>GROUP_LEVEL:8</para>
	/// <para>LIVE_MISSION:9</para>
	/// <para>LIVE_RANKING:10</para>
	/// <para>LOGIN_BONUS:11</para>
	/// <para>LOTTERY:12</para>
	/// <para>MISSION:13</para>
	/// <para>SHOP:14</para>
	/// <para>STORY:15</para>
	/// <para>USER_RANK:16</para>
	/// <para>NOTICE:17</para>
	/// </summary>
	// 矩形選択用のコメント
	// REWARD_SCENE.NONE
	// REWARD_SCENE.COMMON
	// REWARD_SCENE.BILLING_PRODUCT
	// REWARD_SCENE.EVENT_BINGO
	// REWARD_SCENE.EVENT_HOMEWORK
	// REWARD_SCENE.EVENT_POINT
	// REWARD_SCENE.EVENT_SCORE_RANKING
	// REWARD_SCENE.EXCHANGE_ITEM
	// REWARD_SCENE.GROUP_LEVEL
	// REWARD_SCENE.LIVE_MISSION
	// REWARD_SCENE.LIVE_RANKING
	// REWARD_SCENE.LOGIN_BONUS
	// REWARD_SCENE.LOTTERY
	// REWARD_SCENE.MISSION
	// REWARD_SCENE.SHOP
	// REWARD_SCENE.STORY
	// REWARD_SCENE.USER_RANK
	// REWARD_SCENE.NOTICE
	// なし
	// 共通
	// 課金報酬
	// イベントビンゴ報酬
	// イベント宿題報酬
	// イベントポイント報酬
	// イベントスコアランキング報酬
	// 交換アイテム報酬
	// グループレベル報酬
	// ライブミッション報酬
	// ライブランキング報酬
	// ログインボーナス報酬
	// ガチャ報酬
	// ミッション報酬
	// ショップ報酬
	// ストーリー報酬
	// ユーザーランク報酬
	// 通知報酬
	public enum REWARD_SCENE : int
	{
		/// <summary>なし</summary>
		NONE = 0,

		/// <summary>共通</summary>
		COMMON = 1,

		/// <summary>課金報酬</summary>
		BILLING_PRODUCT = 2,

		/// <summary>イベントビンゴ報酬</summary>
		EVENT_BINGO = 3,

		/// <summary>イベント宿題報酬</summary>
		EVENT_HOMEWORK = 4,

		/// <summary>イベントポイント報酬</summary>
		EVENT_POINT = 5,

		/// <summary>イベントスコアランキング報酬</summary>
		EVENT_SCORE_RANKING = 6,

		/// <summary>交換アイテム報酬</summary>
		EXCHANGE_ITEM = 7,

		/// <summary>グループレベル報酬</summary>
		GROUP_LEVEL = 8,

		/// <summary>ライブミッション報酬</summary>
		LIVE_MISSION = 9,

		/// <summary>ライブランキング報酬</summary>
		LIVE_RANKING = 10,

		/// <summary>ログインボーナス報酬</summary>
		LOGIN_BONUS = 11,

		/// <summary>ガチャ報酬</summary>
		LOTTERY = 12,

		/// <summary>ミッション報酬</summary>
		MISSION = 13,

		/// <summary>ショップ報酬</summary>
		SHOP = 14,

		/// <summary>ストーリー報酬</summary>
		STORY = 15,

		/// <summary>ユーザーランク報酬</summary>
		USER_RANK = 16,

		/// <summary>通知報酬</summary>
		NOTICE = 17,
	}

	/// <summary>
	/// <para>BGM:0</para>
	/// <para>SE:1</para>
	/// <para>VOICE:2</para>
	/// </summary>
	// 矩形選択用のコメント
	// SOUND_TYPE.BGM
	// SOUND_TYPE.SE
	// SOUND_TYPE.VOICE
	// BGM
	// SE
	// VOICE
	public enum SOUND_TYPE : int
	{
		/// <summary>BGM</summary>
		BGM = 0,

		/// <summary>SE</summary>
		SE = 1,

		/// <summary>VOICE</summary>
		VOICE = 2,
	}

	/// <summary>
	/// <para>NORMAL:0</para>
	/// <para>EVENT:1</para>
	/// </summary>
	// 矩形選択用のコメント
	// TITLE_CATEGORY.NORMAL
	// TITLE_CATEGORY.EVENT
	// 通常
	// イベント
	public enum TITLE_CATEGORY : int
	{
		/// <summary>通常</summary>
		NORMAL = 0,

		/// <summary>イベント</summary>
		EVENT = 1,
	}

	/// <summary>
	/// <para>GROUP_LEVEL_REWARD:0</para>
	/// <para>USER_RANK_REWARD:1</para>
	/// <para>STORY_PART_REWARD:2</para>
	/// <para>MISSION_REWARD:3</para>
	/// </summary>
	// 矩形選択用のコメント
	// UNLOCK_ROUTE.GROUP_LEVEL_REWARD
	// UNLOCK_ROUTE.USER_RANK_REWARD
	// UNLOCK_ROUTE.STORY_PART_REWARD
	// UNLOCK_ROUTE.MISSION_REWARD
	// グループレベル報酬
	// プレイヤーランク報酬
	// ストーリー再生報酬
	// ミッション報酬
	public enum UNLOCK_ROUTE : int
	{
		/// <summary>グループレベル報酬</summary>
		GROUP_LEVEL_REWARD = 0,

		/// <summary>プレイヤーランク報酬</summary>
		USER_RANK_REWARD = 1,

		/// <summary>ストーリー再生報酬</summary>
		STORY_PART_REWARD = 2,

		/// <summary>ミッション報酬</summary>
		MISSION_REWARD = 3,
	}

	/// <summary>
	/// <para>SYSTEM:0</para>
	/// <para>PART:1</para>
	/// </summary>
	// 矩形選択用のコメント
	// VOICE_CATEGORY.SYSTEM
	// VOICE_CATEGORY.PART
	// システムボイス
	// パートボイス
	public enum VOICE_CATEGORY : int
	{
		/// <summary>システムボイス</summary>
		SYSTEM = 0,

		/// <summary>パートボイス</summary>
		PART = 1,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>GACHA:1</para>
	/// <para>EVENT:2</para>
	/// <para>LOGIN_BONUS:3</para>
	/// <para>EXCHANGE:4</para>
	/// <para>STORY:5</para>
	/// <para>MISSION:6</para>
	/// <para>EVOLVED:7</para>
	/// </summary>
	// 矩形選択用のコメント
	// GET_CATEGORY.NONE
	// GET_CATEGORY.GACHA
	// GET_CATEGORY.EVENT
	// GET_CATEGORY.LOGIN_BONUS
	// GET_CATEGORY.EXCHANGE
	// GET_CATEGORY.STORY
	// GET_CATEGORY.MISSION
	// GET_CATEGORY.EVOLVED
	// 初期入手
	// ガチャ入手
	// イベント入手
	// ログインボーナス入手
	// 交換所入手
	// ストーリー入手
	// ミッション入手
	// カード限界突破
	public enum GET_CATEGORY : int
	{
		/// <summary>初期入手</summary>
		NONE = 0,

		/// <summary>ガチャ入手</summary>
		GACHA = 1,

		/// <summary>イベント入手</summary>
		EVENT = 2,

		/// <summary>ログインボーナス入手</summary>
		LOGIN_BONUS = 3,

		/// <summary>交換所入手</summary>
		EXCHANGE = 4,

		/// <summary>ストーリー入手</summary>
		STORY = 5,

		/// <summary>ミッション入手</summary>
		MISSION = 6,

		/// <summary>カード限界突破</summary>
		EVOLVED = 7,
	}

	/// <summary>
	/// <para>NORMAL:0</para>
	/// <para>SYSTEM:1</para>
	/// </summary>
	// 矩形選択用のコメント
	// HOME_BG_TYPE.NORMAL
	// HOME_BG_TYPE.SYSTEM
	// 通常背景
	// システム背景
	public enum HOME_BG_TYPE : int
	{
		/// <summary>通常背景</summary>
		NORMAL = 0,

		/// <summary>システム背景</summary>
		SYSTEM = 1,
	}

	/// <summary>
	/// <para>ALL:0</para>
	/// <para>HOME:1</para>
	/// </summary>
	// 矩形選択用のコメント
	// CHARACTER_STAND_DISP_TYPE.ALL
	// CHARACTER_STAND_DISP_TYPE.HOME
	// 全て
	// ホーム
	public enum CHARACTER_STAND_DISP_TYPE : int
	{
		/// <summary>全て</summary>
		ALL = 0,

		/// <summary>ホーム</summary>
		HOME = 1,
	}

	/// <summary>
	/// <para>HOME_BG:0</para>
	/// </summary>
	// 矩形選択用のコメント
	// CHARACTER_IMAGE_CATEGOLY.HOME_BG
	// ホーム背景
	public enum CHARACTER_IMAGE_CATEGOLY : int
	{
		/// <summary>ホーム背景</summary>
		HOME_BG = 0,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>BISH:1</para>
	/// <para>EMPIRE:2</para>
	/// <para>GANGPARADE:3</para>
	/// <para>ASP:4</para>
	/// <para>BIS:5</para>
	/// <para>MAMESHIBANOTAIGUN:6</para>
	/// <para>WAGG:7</para>
	/// <para>OTHER:8</para>
	/// <para>XJAPAN:9</para>
	/// <para>LUNASEA:10</para>
	/// <para>GLAY:11</para>
	/// </summary>
	// 矩形選択用のコメント
	// BAND_CATEGORY.NONE
	// BAND_CATEGORY.BISH
	// BAND_CATEGORY.EMPIRE
	// BAND_CATEGORY.GANGPARADE
	// BAND_CATEGORY.ASP
	// BAND_CATEGORY.BIS
	// BAND_CATEGORY.MAMESHIBANOTAIGUN
	// BAND_CATEGORY.WAGG
	// BAND_CATEGORY.OTHER
	// BAND_CATEGORY.XJAPAN
	// BAND_CATEGORY.LUNASEA
	// BAND_CATEGORY.GLAY
	// 無し
	// BiSH
	// EMPiRE
	// GANGPARADE
	// ASP
	// BiS
	// 豆柴の大群
	// Wagg
	// その他
	// XJAPAN
	// LUNASEA
	// GLAY
	public enum BAND_CATEGORY : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>BiSH</summary>
		BISH = 1,

		/// <summary>EMPiRE</summary>
		EMPIRE = 2,

		/// <summary>GANGPARADE</summary>
		GANGPARADE = 3,

		/// <summary>ASP</summary>
		ASP = 4,

		/// <summary>BiS</summary>
		BIS = 5,

		/// <summary>豆柴の大群</summary>
		MAMESHIBANOTAIGUN = 6,

		/// <summary>Wagg</summary>
		WAGG = 7,

		/// <summary>その他</summary>
		OTHER = 8,

		/// <summary>XJAPAN</summary>
		XJAPAN = 9,

		/// <summary>LUNASEA</summary>
		LUNASEA = 10,

		/// <summary>GLAY</summary>
		GLAY = 11,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>SHORT:1</para>
	/// <para>LONG:2</para>
	/// </summary>
	// 矩形選択用のコメント
	// MOVIE_LENGTH_TYPE.NONE
	// MOVIE_LENGTH_TYPE.SHORT
	// MOVIE_LENGTH_TYPE.LONG
	// なし
	// ショート
	// ロング
	public enum MOVIE_LENGTH_TYPE : int
	{
		/// <summary>なし</summary>
		NONE = 0,

		/// <summary>ショート</summary>
		SHORT = 1,

		/// <summary>ロング</summary>
		LONG = 2,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>TOTAL_POWER:1</para>
	/// </summary>
	// 矩形選択用のコメント
	// MULTI_CONDITION_TYPE.NONE
	// MULTI_CONDITION_TYPE.TOTAL_POWER
	// なし
	// 合計総合力
	public enum MULTI_CONDITION_TYPE : int
	{
		/// <summary>なし</summary>
		NONE = 0,

		/// <summary>合計総合力</summary>
		TOTAL_POWER = 1,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>INSTRUMENT:1</para>
	/// <para>GOODS:2</para>
	/// <para>INTERIOR_EXTERIOR:3</para>
	/// </summary>
	// 矩形選択用のコメント
	// ORNAMENT_CATEGORY.NONE
	// ORNAMENT_CATEGORY.INSTRUMENT
	// ORNAMENT_CATEGORY.GOODS
	// ORNAMENT_CATEGORY.INTERIOR_EXTERIOR
	// なし
	// 楽器
	// 雑貨
	// 内装外装
	public enum ORNAMENT_CATEGORY : int
	{
		/// <summary>なし</summary>
		NONE = 0,

		/// <summary>楽器</summary>
		INSTRUMENT = 1,

		/// <summary>雑貨</summary>
		GOODS = 2,

		/// <summary>内装外装</summary>
		INTERIOR_EXTERIOR = 3,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>RIGHT_FRONT:1</para>
	/// <para>LEFT_FRONT:2</para>
	/// <para>RIGHT_CENTER:3</para>
	/// <para>LEFT_CENTER:4</para>
	/// <para>RIGHT_BACK:5</para>
	/// <para>LEFT_BACK:6</para>
	/// <para>WALL_MATERIAL:7</para>
	/// <para>FLOOR_MATERIAL:8</para>
	/// <para>BACKGROUND:9</para>
	/// <para>MICROPHONE:10</para>
	/// <para>GUITAR:11</para>
	/// <para>BASS:12</para>
	/// <para>DRUM:13</para>
	/// <para>PIANO:14</para>
	/// <para>OBJECT:15</para>
	/// <para>SOFA:16</para>
	/// <para>TABLE:17</para>
	/// <para>ON_THE_TABLE:18</para>
	/// <para>POSTER:19</para>
	/// <para>WALL_SURFACE_1:20</para>
	/// <para>WALL_SURFACE_2:21</para>
	/// </summary>
	// 矩形選択用のコメント
	// ORNAMENT_SUB_CATEGORY.NONE
	// ORNAMENT_SUB_CATEGORY.RIGHT_FRONT
	// ORNAMENT_SUB_CATEGORY.LEFT_FRONT
	// ORNAMENT_SUB_CATEGORY.RIGHT_CENTER
	// ORNAMENT_SUB_CATEGORY.LEFT_CENTER
	// ORNAMENT_SUB_CATEGORY.RIGHT_BACK
	// ORNAMENT_SUB_CATEGORY.LEFT_BACK
	// ORNAMENT_SUB_CATEGORY.WALL_MATERIAL
	// ORNAMENT_SUB_CATEGORY.FLOOR_MATERIAL
	// ORNAMENT_SUB_CATEGORY.BACKGROUND
	// ORNAMENT_SUB_CATEGORY.MICROPHONE
	// ORNAMENT_SUB_CATEGORY.GUITAR
	// ORNAMENT_SUB_CATEGORY.BASS
	// ORNAMENT_SUB_CATEGORY.DRUM
	// ORNAMENT_SUB_CATEGORY.PIANO
	// ORNAMENT_SUB_CATEGORY.OBJECT
	// ORNAMENT_SUB_CATEGORY.SOFA
	// ORNAMENT_SUB_CATEGORY.TABLE
	// ORNAMENT_SUB_CATEGORY.ON_THE_TABLE
	// ORNAMENT_SUB_CATEGORY.POSTER
	// ORNAMENT_SUB_CATEGORY.WALL_SURFACE_1
	// ORNAMENT_SUB_CATEGORY.WALL_SURFACE_2
	// 無し
	// 右手前
	// 左手前
	// 右中央
	// 左中央
	// 右奥
	// 左奥
	// 壁材
	// 床材
	// 背景
	// マイク
	// ギター
	// ベース
	// ドラム
	// ピアノ
	// オブジェ
	// ソファー
	// テーブル
	// テーブル上
	// ポスター
	// 壁面１
	// 壁面２
	public enum ORNAMENT_SUB_CATEGORY : int
	{
		/// <summary>無し</summary>
		NONE = 0,

		/// <summary>右手前</summary>
		RIGHT_FRONT = 1,

		/// <summary>左手前</summary>
		LEFT_FRONT = 2,

		/// <summary>右中央</summary>
		RIGHT_CENTER = 3,

		/// <summary>左中央</summary>
		LEFT_CENTER = 4,

		/// <summary>右奥</summary>
		RIGHT_BACK = 5,

		/// <summary>左奥</summary>
		LEFT_BACK = 6,

		/// <summary>壁材</summary>
		WALL_MATERIAL = 7,

		/// <summary>床材</summary>
		FLOOR_MATERIAL = 8,

		/// <summary>背景</summary>
		BACKGROUND = 9,

		/// <summary>マイク</summary>
		MICROPHONE = 10,

		/// <summary>ギター</summary>
		GUITAR = 11,

		/// <summary>ベース</summary>
		BASS = 12,

		/// <summary>ドラム</summary>
		DRUM = 13,

		/// <summary>ピアノ</summary>
		PIANO = 14,

		/// <summary>オブジェ</summary>
		OBJECT = 15,

		/// <summary>ソファー</summary>
		SOFA = 16,

		/// <summary>テーブル</summary>
		TABLE = 17,

		/// <summary>テーブル上</summary>
		ON_THE_TABLE = 18,

		/// <summary>ポスター</summary>
		POSTER = 19,

		/// <summary>壁面１</summary>
		WALL_SURFACE_1 = 20,

		/// <summary>壁面２</summary>
		WALL_SURFACE_2 = 21,
	}

	/// <summary>
	/// <para>MAIN:0</para>
	/// <para>SUB:1</para>
	/// </summary>
	// 矩形選択用のコメント
	// TITLE_TYPE.MAIN
	// TITLE_TYPE.SUB
	// メイン
	// サブ
	public enum TITLE_TYPE : int
	{
		/// <summary>メイン</summary>
		MAIN = 0,

		/// <summary>サブ</summary>
		SUB = 1,
	}

	/// <summary>
	/// <para>NONE:0</para>
	/// <para>STANDARD:1</para>
	/// <para>WIDE:2</para>
	/// </summary>
	// 矩形選択用のコメント
	// MOVIE_SCREEN_TYPE.NONE
	// MOVIE_SCREEN_TYPE.STANDARD
	// MOVIE_SCREEN_TYPE.WIDE
	// なし
	// 標準(4:3)
	// ワイド(16:9)
	public enum MOVIE_SCREEN_TYPE : int
	{
		/// <summary>なし</summary>
		NONE = 0,

		/// <summary>標準(4:3)</summary>
		STANDARD = 1,

		/// <summary>ワイド(16:9)</summary>
		WIDE = 2,
	}


	/// <summary>
	/// DictionaryのキーにRESULT_CODE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class ResultCodeComparer : IEqualityComparer<RESULT_CODE>
	{
		public bool Equals( RESULT_CODE x, RESULT_CODE y )
		{
			return x == y;
		}

		public int GetHashCode( RESULT_CODE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにSTATUS列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class StatusComparer : IEqualityComparer<STATUS>
	{
		public bool Equals( STATUS x, STATUS y )
		{
			return x == y;
		}

		public int GetHashCode( STATUS obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにDECK_ROLE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class DeckRoleComparer : IEqualityComparer<DECK_ROLE>
	{
		public bool Equals( DECK_ROLE x, DECK_ROLE y )
		{
			return x == y;
		}

		public int GetHashCode( DECK_ROLE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにCARD_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class CardTypeComparer : IEqualityComparer<CARD_TYPE>
	{
		public bool Equals( CARD_TYPE x, CARD_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( CARD_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにCARD_LIMIT_BREAK_REWARD_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class CardLimitBreakRewardTypeComparer : IEqualityComparer<CARD_LIMIT_BREAK_REWARD_TYPE>
	{
		public bool Equals( CARD_LIMIT_BREAK_REWARD_TYPE x, CARD_LIMIT_BREAK_REWARD_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( CARD_LIMIT_BREAK_REWARD_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにSKILL_CARD_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class SkillCardTypeComparer : IEqualityComparer<SKILL_CARD_TYPE>
	{
		public bool Equals( SKILL_CARD_TYPE x, SKILL_CARD_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( SKILL_CARD_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにSKILL_DECK_ROLE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class SkillDeckRoleComparer : IEqualityComparer<SKILL_DECK_ROLE>
	{
		public bool Equals( SKILL_DECK_ROLE x, SKILL_DECK_ROLE y )
		{
			return x == y;
		}

		public int GetHashCode( SKILL_DECK_ROLE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにSKILL_EFFECT_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class SkillEffectTypeComparer : IEqualityComparer<SKILL_EFFECT_TYPE>
	{
		public bool Equals( SKILL_EFFECT_TYPE x, SKILL_EFFECT_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( SKILL_EFFECT_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにSKILL_TRIGGER_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class SkillTriggerTypeComparer : IEqualityComparer<SKILL_TRIGGER_TYPE>
	{
		public bool Equals( SKILL_TRIGGER_TYPE x, SKILL_TRIGGER_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( SKILL_TRIGGER_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにSTATUS_UP_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class StatusUpTypeComparer : IEqualityComparer<STATUS_UP_TYPE>
	{
		public bool Equals( STATUS_UP_TYPE x, STATUS_UP_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( STATUS_UP_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにMARKER_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class MarkerTypeComparer : IEqualityComparer<MARKER_TYPE>
	{
		public bool Equals( MARKER_TYPE x, MARKER_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( MARKER_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにINPUT_RESULT_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class InputResultTypeComparer : IEqualityComparer<INPUT_RESULT_TYPE>
	{
		public bool Equals( INPUT_RESULT_TYPE x, INPUT_RESULT_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( INPUT_RESULT_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにFRIEND_STATUS列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class FriendStatusComparer : IEqualityComparer<FRIEND_STATUS>
	{
		public bool Equals( FRIEND_STATUS x, FRIEND_STATUS y )
		{
			return x == y;
		}

		public int GetHashCode( FRIEND_STATUS obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにITEM_EFFECT_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class ItemEffectTypeComparer : IEqualityComparer<ITEM_EFFECT_TYPE>
	{
		public bool Equals( ITEM_EFFECT_TYPE x, ITEM_EFFECT_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( ITEM_EFFECT_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにITEM_CATEGORY列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class ItemCategoryComparer : IEqualityComparer<ITEM_CATEGORY>
	{
		public bool Equals( ITEM_CATEGORY x, ITEM_CATEGORY y )
		{
			return x == y;
		}

		public int GetHashCode( ITEM_CATEGORY obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにREWARD_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class RewardTypeComparer : IEqualityComparer<REWARD_TYPE>
	{
		public bool Equals( REWARD_TYPE x, REWARD_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( REWARD_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにPOINT_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class PointTypeComparer : IEqualityComparer<POINT_TYPE>
	{
		public bool Equals( POINT_TYPE x, POINT_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( POINT_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにRARITY列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class RarityComparer : IEqualityComparer<RARITY>
	{
		public bool Equals( RARITY x, RARITY y )
		{
			return x == y;
		}

		public int GetHashCode( RARITY obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにCONSUME_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class ConsumeTypeComparer : IEqualityComparer<CONSUME_TYPE>
	{
		public bool Equals( CONSUME_TYPE x, CONSUME_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( CONSUME_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにORNAMENT_SHOP_CONSUME_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class OrnamentShopConsumeTypeComparer : IEqualityComparer<ORNAMENT_SHOP_CONSUME_TYPE>
	{
		public bool Equals( ORNAMENT_SHOP_CONSUME_TYPE x, ORNAMENT_SHOP_CONSUME_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( ORNAMENT_SHOP_CONSUME_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにLIVE_MODE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class LiveModeComparer : IEqualityComparer<LIVE_MODE>
	{
		public bool Equals( LIVE_MODE x, LIVE_MODE y )
		{
			return x == y;
		}

		public int GetHashCode( LIVE_MODE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにLIVE_LEVEL列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class LiveLevelComparer : IEqualityComparer<LIVE_LEVEL>
	{
		public bool Equals( LIVE_LEVEL x, LIVE_LEVEL y )
		{
			return x == y;
		}

		public int GetHashCode( LIVE_LEVEL obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにSCORE_RANK列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class ScoreRankComparer : IEqualityComparer<SCORE_RANK>
	{
		public bool Equals( SCORE_RANK x, SCORE_RANK y )
		{
			return x == y;
		}

		public int GetHashCode( SCORE_RANK obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにSTORY_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class StoryTypeComparer : IEqualityComparer<STORY_TYPE>
	{
		public bool Equals( STORY_TYPE x, STORY_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( STORY_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにMISSION_STATUS列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class MissionStatusComparer : IEqualityComparer<MISSION_STATUS>
	{
		public bool Equals( MISSION_STATUS x, MISSION_STATUS y )
		{
			return x == y;
		}

		public int GetHashCode( MISSION_STATUS obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにMISSION_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class MissionTypeComparer : IEqualityComparer<MISSION_TYPE>
	{
		public bool Equals( MISSION_TYPE x, MISSION_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( MISSION_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにMISSION_CONDITION_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class MissionConditionTypeComparer : IEqualityComparer<MISSION_CONDITION_TYPE>
	{
		public bool Equals( MISSION_CONDITION_TYPE x, MISSION_CONDITION_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( MISSION_CONDITION_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにLIVE_MISSION_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class LiveMissionTypeComparer : IEqualityComparer<LIVE_MISSION_TYPE>
	{
		public bool Equals( LIVE_MISSION_TYPE x, LIVE_MISSION_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( LIVE_MISSION_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにGIVE_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class GiveTypeComparer : IEqualityComparer<GIVE_TYPE>
	{
		public bool Equals( GIVE_TYPE x, GIVE_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( GIVE_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにLOCK_STATUS列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class LockStatusComparer : IEqualityComparer<LOCK_STATUS>
	{
		public bool Equals( LOCK_STATUS x, LOCK_STATUS y )
		{
			return x == y;
		}

		public int GetHashCode( LOCK_STATUS obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにCARD_EPISODE_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class CardEpisodeTypeComparer : IEqualityComparer<CARD_EPISODE_TYPE>
	{
		public bool Equals( CARD_EPISODE_TYPE x, CARD_EPISODE_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( CARD_EPISODE_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにPROFILE_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class ProfileTypeComparer : IEqualityComparer<PROFILE_TYPE>
	{
		public bool Equals( PROFILE_TYPE x, PROFILE_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( PROFILE_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにLOGIN_BONUS_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class LoginBonusTypeComparer : IEqualityComparer<LOGIN_BONUS_TYPE>
	{
		public bool Equals( LOGIN_BONUS_TYPE x, LOGIN_BONUS_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( LOGIN_BONUS_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにTITLE_CONDITION_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class TitleConditionTypeComparer : IEqualityComparer<TITLE_CONDITION_TYPE>
	{
		public bool Equals( TITLE_CONDITION_TYPE x, TITLE_CONDITION_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( TITLE_CONDITION_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにSHOP_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class ShopTypeComparer : IEqualityComparer<SHOP_TYPE>
	{
		public bool Equals( SHOP_TYPE x, SHOP_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( SHOP_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにDAY_OF_WEEK列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class DayOfWeekComparer : IEqualityComparer<DAY_OF_WEEK>
	{
		public bool Equals( DAY_OF_WEEK x, DAY_OF_WEEK y )
		{
			return x == y;
		}

		public int GetHashCode( DAY_OF_WEEK obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにOBTAIN_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class ObtainTypeComparer : IEqualityComparer<OBTAIN_TYPE>
	{
		public bool Equals( OBTAIN_TYPE x, OBTAIN_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( OBTAIN_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにCOSTUME_EFFECT_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class CostumeEffectTypeComparer : IEqualityComparer<COSTUME_EFFECT_TYPE>
	{
		public bool Equals( COSTUME_EFFECT_TYPE x, COSTUME_EFFECT_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( COSTUME_EFFECT_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにORNAMENT_EFFECT_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class OrnamentEffectTypeComparer : IEqualityComparer<ORNAMENT_EFFECT_TYPE>
	{
		public bool Equals( ORNAMENT_EFFECT_TYPE x, ORNAMENT_EFFECT_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( ORNAMENT_EFFECT_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにORNAMENT_EFFECT_TARGET_CATEGORY列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class OrnamentEffectTargetCategoryComparer : IEqualityComparer<ORNAMENT_EFFECT_TARGET_CATEGORY>
	{
		public bool Equals( ORNAMENT_EFFECT_TARGET_CATEGORY x, ORNAMENT_EFFECT_TARGET_CATEGORY y )
		{
			return x == y;
		}

		public int GetHashCode( ORNAMENT_EFFECT_TARGET_CATEGORY obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにTUTORIAL_STEP列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class TutorialStepComparer : IEqualityComparer<TUTORIAL_STEP>
	{
		public bool Equals( TUTORIAL_STEP x, TUTORIAL_STEP y )
		{
			return x == y;
		}

		public int GetHashCode( TUTORIAL_STEP obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにBILLING_PRODUCT_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class BillingProductTypeComparer : IEqualityComparer<BILLING_PRODUCT_TYPE>
	{
		public bool Equals( BILLING_PRODUCT_TYPE x, BILLING_PRODUCT_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( BILLING_PRODUCT_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにLOTTERY_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class LotteryTypeComparer : IEqualityComparer<LOTTERY_TYPE>
	{
		public bool Equals( LOTTERY_TYPE x, LOTTERY_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( LOTTERY_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにMULTI_DAILY_DROP_EFFECT_TARGET_CATEGORY列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class MultiDailyDropEffectTargetCategoryComparer : IEqualityComparer<MULTI_DAILY_DROP_EFFECT_TARGET_CATEGORY>
	{
		public bool Equals( MULTI_DAILY_DROP_EFFECT_TARGET_CATEGORY x, MULTI_DAILY_DROP_EFFECT_TARGET_CATEGORY y )
		{
			return x == y;
		}

		public int GetHashCode( MULTI_DAILY_DROP_EFFECT_TARGET_CATEGORY obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにEVENT_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class EventTypeComparer : IEqualityComparer<EVENT_TYPE>
	{
		public bool Equals( EVENT_TYPE x, EVENT_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( EVENT_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにEVENT_POINT_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class EventPointTypeComparer : IEqualityComparer<EVENT_POINT_TYPE>
	{
		public bool Equals( EVENT_POINT_TYPE x, EVENT_POINT_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( EVENT_POINT_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにEVENT_BONUS_TARGET_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class EventBonusTargetTypeComparer : IEqualityComparer<EVENT_BONUS_TARGET_TYPE>
	{
		public bool Equals( EVENT_BONUS_TARGET_TYPE x, EVENT_BONUS_TARGET_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( EVENT_BONUS_TARGET_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにEVENT_BOOST_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class EventBoostTypeComparer : IEqualityComparer<EVENT_BOOST_TYPE>
	{
		public bool Equals( EVENT_BOOST_TYPE x, EVENT_BOOST_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( EVENT_BOOST_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにRANKING_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class RankingTypeComparer : IEqualityComparer<RANKING_TYPE>
	{
		public bool Equals( RANKING_TYPE x, RANKING_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( RANKING_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにRANKING_GROUP_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class RankingGroupTypeComparer : IEqualityComparer<RANKING_GROUP_TYPE>
	{
		public bool Equals( RANKING_GROUP_TYPE x, RANKING_GROUP_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( RANKING_GROUP_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにEMOTIONAL_RANKING_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class EmotionalRankingTypeComparer : IEqualityComparer<EMOTIONAL_RANKING_TYPE>
	{
		public bool Equals( EMOTIONAL_RANKING_TYPE x, EMOTIONAL_RANKING_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( EMOTIONAL_RANKING_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにEVOLVE_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class EvolveTypeComparer : IEqualityComparer<EVOLVE_TYPE>
	{
		public bool Equals( EVOLVE_TYPE x, EVOLVE_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( EVOLVE_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにEVOLVE_ITEM_CONDITION_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class EvolveItemConditionTypeComparer : IEqualityComparer<EVOLVE_ITEM_CONDITION_TYPE>
	{
		public bool Equals( EVOLVE_ITEM_CONDITION_TYPE x, EVOLVE_ITEM_CONDITION_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( EVOLVE_ITEM_CONDITION_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにFUNCTION_ID列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class FunctionIdComparer : IEqualityComparer<FUNCTION_ID>
	{
		public bool Equals( FUNCTION_ID x, FUNCTION_ID y )
		{
			return x == y;
		}

		public int GetHashCode( FUNCTION_ID obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにHOMEWORK_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class HomeworkTypeComparer : IEqualityComparer<HOMEWORK_TYPE>
	{
		public bool Equals( HOMEWORK_TYPE x, HOMEWORK_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( HOMEWORK_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにLIVE_CONDITION_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class LiveConditionTypeComparer : IEqualityComparer<LIVE_CONDITION_TYPE>
	{
		public bool Equals( LIVE_CONDITION_TYPE x, LIVE_CONDITION_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( LIVE_CONDITION_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにCHARACTER_EPISODE_EFFECT_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class CharacterEpisodeEffectTypeComparer : IEqualityComparer<CHARACTER_EPISODE_EFFECT_TYPE>
	{
		public bool Equals( CHARACTER_EPISODE_EFFECT_TYPE x, CHARACTER_EPISODE_EFFECT_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( CHARACTER_EPISODE_EFFECT_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにCHARACTER_EPISODE_EXP列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class CharacterEpisodeExpComparer : IEqualityComparer<CHARACTER_EPISODE_EXP>
	{
		public bool Equals( CHARACTER_EPISODE_EXP x, CHARACTER_EPISODE_EXP y )
		{
			return x == y;
		}

		public int GetHashCode( CHARACTER_EPISODE_EXP obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにTIME_RESET_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class TimeResetTypeComparer : IEqualityComparer<TIME_RESET_TYPE>
	{
		public bool Equals( TIME_RESET_TYPE x, TIME_RESET_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( TIME_RESET_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにADV_SCENARIO_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class AdvScenarioTypeComparer : IEqualityComparer<ADV_SCENARIO_TYPE>
	{
		public bool Equals( ADV_SCENARIO_TYPE x, ADV_SCENARIO_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( ADV_SCENARIO_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにAREA_EVENT_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class AreaEventTypeComparer : IEqualityComparer<AREA_EVENT_TYPE>
	{
		public bool Equals( AREA_EVENT_TYPE x, AREA_EVENT_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( AREA_EVENT_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにAREA_UNLOCK_CONDITION_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class AreaUnlockConditionTypeComparer : IEqualityComparer<AREA_UNLOCK_CONDITION_TYPE>
	{
		public bool Equals( AREA_UNLOCK_CONDITION_TYPE x, AREA_UNLOCK_CONDITION_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( AREA_UNLOCK_CONDITION_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにAREA_SHOP_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class AreaShopTypeComparer : IEqualityComparer<AREA_SHOP_TYPE>
	{
		public bool Equals( AREA_SHOP_TYPE x, AREA_SHOP_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( AREA_SHOP_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにTALK_UNLOCK_CONDITION_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class TalkUnlockConditionTypeComparer : IEqualityComparer<TALK_UNLOCK_CONDITION_TYPE>
	{
		public bool Equals( TALK_UNLOCK_CONDITION_TYPE x, TALK_UNLOCK_CONDITION_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( TALK_UNLOCK_CONDITION_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにDATE_LINE_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class DateLineTypeComparer : IEqualityComparer<DATE_LINE_TYPE>
	{
		public bool Equals( DATE_LINE_TYPE x, DATE_LINE_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( DATE_LINE_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにEXCHANGE_TAB列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class ExchangeTabComparer : IEqualityComparer<EXCHANGE_TAB>
	{
		public bool Equals( EXCHANGE_TAB x, EXCHANGE_TAB y )
		{
			return x == y;
		}

		public int GetHashCode( EXCHANGE_TAB obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにEXCHANGE_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class ExchangeTypeComparer : IEqualityComparer<EXCHANGE_TYPE>
	{
		public bool Equals( EXCHANGE_TYPE x, EXCHANGE_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( EXCHANGE_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにHOME_BANNER_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class HomeBannerTypeComparer : IEqualityComparer<HOME_BANNER_TYPE>
	{
		public bool Equals( HOME_BANNER_TYPE x, HOME_BANNER_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( HOME_BANNER_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにITEM_TAB列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class ItemTabComparer : IEqualityComparer<ITEM_TAB>
	{
		public bool Equals( ITEM_TAB x, ITEM_TAB y )
		{
			return x == y;
		}

		public int GetHashCode( ITEM_TAB obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにLIVE_POSITION列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class LivePositionComparer : IEqualityComparer<LIVE_POSITION>
	{
		public bool Equals( LIVE_POSITION x, LIVE_POSITION y )
		{
			return x == y;
		}

		public int GetHashCode( LIVE_POSITION obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにLIVE_RULE_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class LiveRuleTypeComparer : IEqualityComparer<LIVE_RULE_TYPE>
	{
		public bool Equals( LIVE_RULE_TYPE x, LIVE_RULE_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( LIVE_RULE_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにLIVE_SPINE_MOTION列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class LiveSpineMotionComparer : IEqualityComparer<LIVE_SPINE_MOTION>
	{
		public bool Equals( LIVE_SPINE_MOTION x, LIVE_SPINE_MOTION y )
		{
			return x == y;
		}

		public int GetHashCode( LIVE_SPINE_MOTION obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにLIVE_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class LiveTypeComparer : IEqualityComparer<LIVE_TYPE>
	{
		public bool Equals( LIVE_TYPE x, LIVE_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( LIVE_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにLOGIN_NOTICE_PHASE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class LoginNoticePhaseComparer : IEqualityComparer<LOGIN_NOTICE_PHASE>
	{
		public bool Equals( LOGIN_NOTICE_PHASE x, LOGIN_NOTICE_PHASE y )
		{
			return x == y;
		}

		public int GetHashCode( LOGIN_NOTICE_PHASE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにLOGIN_NOTICE_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class LoginNoticeTypeComparer : IEqualityComparer<LOGIN_NOTICE_TYPE>
	{
		public bool Equals( LOGIN_NOTICE_TYPE x, LOGIN_NOTICE_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( LOGIN_NOTICE_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにLOGIN_NOTICE_TRANSITION列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class LoginNoticeTransitionComparer : IEqualityComparer<LOGIN_NOTICE_TRANSITION>
	{
		public bool Equals( LOGIN_NOTICE_TRANSITION x, LOGIN_NOTICE_TRANSITION y )
		{
			return x == y;
		}

		public int GetHashCode( LOGIN_NOTICE_TRANSITION obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにNAVIGATION_CONDITION_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class NavigationConditionTypeComparer : IEqualityComparer<NAVIGATION_CONDITION_TYPE>
	{
		public bool Equals( NAVIGATION_CONDITION_TYPE x, NAVIGATION_CONDITION_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( NAVIGATION_CONDITION_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにPLATE_COLOR列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class PlateColorComparer : IEqualityComparer<PLATE_COLOR>
	{
		public bool Equals( PLATE_COLOR x, PLATE_COLOR y )
		{
			return x == y;
		}

		public int GetHashCode( PLATE_COLOR obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにREWARD_SCENE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class RewardSceneComparer : IEqualityComparer<REWARD_SCENE>
	{
		public bool Equals( REWARD_SCENE x, REWARD_SCENE y )
		{
			return x == y;
		}

		public int GetHashCode( REWARD_SCENE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにSOUND_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class SoundTypeComparer : IEqualityComparer<SOUND_TYPE>
	{
		public bool Equals( SOUND_TYPE x, SOUND_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( SOUND_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにTITLE_CATEGORY列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class TitleCategoryComparer : IEqualityComparer<TITLE_CATEGORY>
	{
		public bool Equals( TITLE_CATEGORY x, TITLE_CATEGORY y )
		{
			return x == y;
		}

		public int GetHashCode( TITLE_CATEGORY obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにUNLOCK_ROUTE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class UnlockRouteComparer : IEqualityComparer<UNLOCK_ROUTE>
	{
		public bool Equals( UNLOCK_ROUTE x, UNLOCK_ROUTE y )
		{
			return x == y;
		}

		public int GetHashCode( UNLOCK_ROUTE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにVOICE_CATEGORY列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class VoiceCategoryComparer : IEqualityComparer<VOICE_CATEGORY>
	{
		public bool Equals( VOICE_CATEGORY x, VOICE_CATEGORY y )
		{
			return x == y;
		}

		public int GetHashCode( VOICE_CATEGORY obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにGET_CATEGORY列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class GetCategoryComparer : IEqualityComparer<GET_CATEGORY>
	{
		public bool Equals( GET_CATEGORY x, GET_CATEGORY y )
		{
			return x == y;
		}

		public int GetHashCode( GET_CATEGORY obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにHOME_BG_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class HomeBgTypeComparer : IEqualityComparer<HOME_BG_TYPE>
	{
		public bool Equals( HOME_BG_TYPE x, HOME_BG_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( HOME_BG_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにCHARACTER_STAND_DISP_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class CharacterStandDispTypeComparer : IEqualityComparer<CHARACTER_STAND_DISP_TYPE>
	{
		public bool Equals( CHARACTER_STAND_DISP_TYPE x, CHARACTER_STAND_DISP_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( CHARACTER_STAND_DISP_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにCHARACTER_IMAGE_CATEGOLY列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class CharacterImageCategolyComparer : IEqualityComparer<CHARACTER_IMAGE_CATEGOLY>
	{
		public bool Equals( CHARACTER_IMAGE_CATEGOLY x, CHARACTER_IMAGE_CATEGOLY y )
		{
			return x == y;
		}

		public int GetHashCode( CHARACTER_IMAGE_CATEGOLY obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにBAND_CATEGORY列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class BandCategoryComparer : IEqualityComparer<BAND_CATEGORY>
	{
		public bool Equals( BAND_CATEGORY x, BAND_CATEGORY y )
		{
			return x == y;
		}

		public int GetHashCode( BAND_CATEGORY obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにMOVIE_LENGTH_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class MovieLengthTypeComparer : IEqualityComparer<MOVIE_LENGTH_TYPE>
	{
		public bool Equals( MOVIE_LENGTH_TYPE x, MOVIE_LENGTH_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( MOVIE_LENGTH_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにMULTI_CONDITION_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class MultiConditionTypeComparer : IEqualityComparer<MULTI_CONDITION_TYPE>
	{
		public bool Equals( MULTI_CONDITION_TYPE x, MULTI_CONDITION_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( MULTI_CONDITION_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにORNAMENT_CATEGORY列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class OrnamentCategoryComparer : IEqualityComparer<ORNAMENT_CATEGORY>
	{
		public bool Equals( ORNAMENT_CATEGORY x, ORNAMENT_CATEGORY y )
		{
			return x == y;
		}

		public int GetHashCode( ORNAMENT_CATEGORY obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにORNAMENT_SUB_CATEGORY列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class OrnamentSubCategoryComparer : IEqualityComparer<ORNAMENT_SUB_CATEGORY>
	{
		public bool Equals( ORNAMENT_SUB_CATEGORY x, ORNAMENT_SUB_CATEGORY y )
		{
			return x == y;
		}

		public int GetHashCode( ORNAMENT_SUB_CATEGORY obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにTITLE_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class TitleTypeComparer : IEqualityComparer<TITLE_TYPE>
	{
		public bool Equals( TITLE_TYPE x, TITLE_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( TITLE_TYPE obj )
		{
			return ( int )obj;
		}
	}

	/// <summary>
	/// DictionaryのキーにMOVIE_SCREEN_TYPE列挙型を使用した際のボックス化を避けるための構造体
	/// </summary>
	public sealed class MovieScreenTypeComparer : IEqualityComparer<MOVIE_SCREEN_TYPE>
	{
		public bool Equals( MOVIE_SCREEN_TYPE x, MOVIE_SCREEN_TYPE y )
		{
			return x == y;
		}

		public int GetHashCode( MOVIE_SCREEN_TYPE obj )
		{
			return ( int )obj;
		}
	}
}

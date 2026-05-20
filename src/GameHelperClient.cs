using System;
using System.Collections.Generic;
using Mirror;
using Mirror.Discovery;
using RVO;
using Steamworks;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000105 RID: 261
public class GameHelperClient
{
	// Token: 0x0600055A RID: 1370 RVA: 0x0001F241 File Offset: 0x0001D441
	public static float GetKingBattleTime()
	{
		if (GameHelperClient.gameConfig != null && GameHelperClient.gameConfig.KingBattleTime > 0f)
		{
			return GameHelperClient.gameConfig.KingBattleTime;
		}
		return 90f;
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x0001F271 File Offset: 0x0001D471
	public static float GetKingBattleFinalTipTime()
	{
		if (GameHelperClient.gameConfig != null)
		{
			return GameHelperClient.gameConfig.KingBattleFinalTipTime;
		}
		return 45f;
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x0001F290 File Offset: 0x0001D490
	public static bool IsFinalKingBattle()
	{
		return GameHelperClient.isKingBattle && GameHelperClient.CountDownTime <= GameHelperClient.GetKingBattleFinalTipTime();
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x0001F2AA File Offset: 0x0001D4AA
	public static bool IsShowPlayerName()
	{
		return GameHelperClient.playerNameDisplayMode != GameHelperClient.PlayerNameDisplayMode.HideName;
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x0001F2B7 File Offset: 0x0001D4B7
	public static string GetPlayerDisplayName(PlayerBase playerBase)
	{
		if (playerBase == null || GameHelperClient.playerNameDisplayMode == GameHelperClient.PlayerNameDisplayMode.HideName)
		{
			return string.Empty;
		}
		if (GameHelperClient.playerNameDisplayMode == GameHelperClient.PlayerNameDisplayMode.SteamName && !string.IsNullOrWhiteSpace(playerBase.steamName))
		{
			return playerBase.steamName;
		}
		return playerBase.roleName;
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x0001F2F4 File Offset: 0x0001D4F4
	public static float GetKingBattleDamageLevel()
	{
		if (GameHelperClient.gameConfig == null)
		{
			return GameHelperClient.GetKingBattleRateValue(0.05f, 45f, 0f, 0.2f);
		}
		return GameHelperClient.gameConfig.KingDamageLevel.GetValue(GameHelperClient.GetKingBattleElapsedTime(), GameHelperClient.GetKingBattleTime());
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x0001F344 File Offset: 0x0001D544
	public static float GetKingBattleReduceLevel()
	{
		if (GameHelperClient.gameConfig == null)
		{
			return GameHelperClient.GetKingBattleRateValue(4f, 45f, 0f, 4f);
		}
		return GameHelperClient.gameConfig.KingReduceLevel.GetValue(GameHelperClient.GetKingBattleElapsedTime(), GameHelperClient.GetKingBattleTime());
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x0001F394 File Offset: 0x0001D594
	public static float GetKingBattleMagicAddHpLevel()
	{
		if (GameHelperClient.gameConfig == null)
		{
			return GameHelperClient.GetKingBattleRateValue(8f, 45f, 0f, 8f);
		}
		return GameHelperClient.gameConfig.KingBattleMagicAddHpLevel.GetValue(GameHelperClient.GetKingBattleElapsedTime(), GameHelperClient.GetKingBattleTime());
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x0001F3E4 File Offset: 0x0001D5E4
	public static float GetKingBattleAttackAddHpLevel()
	{
		if (GameHelperClient.gameConfig == null)
		{
			return GameHelperClient.GetKingBattleRateValue(0.5f, 45f, 0f, 0.5f);
		}
		return GameHelperClient.gameConfig.KingAttackAddHpLevel.GetValue(GameHelperClient.GetKingBattleElapsedTime(), GameHelperClient.GetKingBattleTime());
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x0001F434 File Offset: 0x0001D634
	public static float GetKingBattleAttackPercentAddHpLevel()
	{
		if (GameHelperClient.gameConfig == null)
		{
			return GameHelperClient.GetKingBattleRateValue(1f, 45f, 0f, 1f);
		}
		return GameHelperClient.gameConfig.KingAttackPercentAddHpLevel.GetValue(GameHelperClient.GetKingBattleElapsedTime(), GameHelperClient.GetKingBattleTime());
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x0001F484 File Offset: 0x0001D684
	public static float GetKingBattleAddHpLevel()
	{
		if (GameHelperClient.gameConfig == null)
		{
			return GameHelperClient.GetKingBattleRateValue(1f, 45f, 0f, 1f);
		}
		return GameHelperClient.gameConfig.KingBattleAddHpLevel.GetValue(GameHelperClient.GetKingBattleElapsedTime(), GameHelperClient.GetKingBattleTime());
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x0001F4D1 File Offset: 0x0001D6D1
	private static float GetKingBattleElapsedTime()
	{
		return Mathf.Max(0f, GameHelperClient.GetKingBattleTime() - GameHelperClient.CountDownTime);
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x0001F4E8 File Offset: 0x0001D6E8
	private static float GetKingBattleRateValue(float baseValue, float changeStartTime, float changeEndTime, float finalValue)
	{
		if (GameHelperClient.GetKingBattleElapsedTime() <= changeStartTime)
		{
			return baseValue;
		}
		float kingBattleTime = GameHelperClient.GetKingBattleTime();
		float num = Mathf.Max(changeStartTime, kingBattleTime - changeEndTime);
		if (num <= changeStartTime)
		{
			return finalValue;
		}
		float t = Mathf.InverseLerp(changeStartTime, num, GameHelperClient.GetKingBattleElapsedTime());
		return Mathf.Lerp(baseValue, finalValue, t);
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x0001F52C File Offset: 0x0001D72C
	public static void AOEDamage(RoleBase attackRole, float damage, Vector3 pos, float distance, string effectName, float scale)
	{
		List<RoleBase> list = (attackRole.roleType == RoleType.King) ? Game.PlayerManagerClient.GetRangeEnemy(distance, pos) : Game.EnemyManagerClient.GetRangeEnemy(distance, pos);
		bool isAttackWeek = attackRole.GetIsAttackWeek(AttackType.Skill);
		foreach (RoleBase roleBase in list)
		{
			Util.OnLocalPlayerHit(attackRole, roleBase, (double)((int)damage), Util.GetV2Angle(roleBase.MyTransform.position, pos), AttackType.Skill, isAttackWeek);
		}
		GameHelperClient.localPlayer.CmdPlayEffect(effectName, 1f, pos, scale);
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x0001F5D0 File Offset: 0x0001D7D0
	public static void BeginCoronationGuard(PlayerBase playerBase)
	{
		GameHelperClient.coronationGuardActive = (playerBase != null);
		GameHelperClient.coronationGuardKey = Random.Range(1, int.MaxValue);
		GameHelperClient.guardedRefreshNum = GameHelperClient.EncodeCoronationValue(GameHelperClient.RefreshNum);
		GameHelperClient.guardedGold = GameHelperClient.EncodeCoronationValue((playerBase != null) ? playerBase.gold : 0);
		GameHelperClient.guardedGem = GameHelperClient.EncodeCoronationValue((playerBase != null) ? playerBase.gem : 0);
		GameHelperClient.guardedGetGoldNum = GameHelperClient.EncodeCoronationValue((playerBase != null) ? playerBase.getGoldNum : 0);
		GameHelperClient.guardedGetGemNum = GameHelperClient.EncodeCoronationValue((playerBase != null) ? playerBase.getGemNum : 0);
		GameHelperClient.guardedSTR = GameHelperClient.EncodeCoronationValue((playerBase != null) ? playerBase.mSTR : 0);
		GameHelperClient.guardedSTA = GameHelperClient.EncodeCoronationValue((playerBase != null) ? playerBase.sta : 0);
		GameHelperClient.guardedAGI = GameHelperClient.EncodeCoronationValue((playerBase != null) ? playerBase.agi : 0);
		GameHelperClient.coronationGuardReported = false;
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x0001F6D2 File Offset: 0x0001D8D2
	public static void AddRefreshNum(int value)
	{
		GameHelperClient.CheckCoronationGuard();
		GameHelperClient.RefreshNum += value;
		GameHelperClient.guardedRefreshNum = GameHelperClient.EncodeCoronationValue(GameHelperClient.RefreshNum);
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x0001F6F5 File Offset: 0x0001D8F5
	public static void SetRefreshNum(int value)
	{
		GameHelperClient.CheckCoronationGuard();
		GameHelperClient.RefreshNum = value;
		GameHelperClient.guardedRefreshNum = GameHelperClient.EncodeCoronationValue(GameHelperClient.RefreshNum);
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x0001F712 File Offset: 0x0001D912
	public static void TrackGold(PlayerBase playerBase)
	{
		if (!GameHelperClient.coronationGuardActive || playerBase != GameHelperClient.localPlayer)
		{
			return;
		}
		GameHelperClient.guardedGold = GameHelperClient.EncodeCoronationValue(playerBase.gold);
		GameHelperClient.guardedGetGoldNum = GameHelperClient.EncodeCoronationValue(playerBase.getGoldNum);
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x0001F749 File Offset: 0x0001D949
	public static void TrackGem(PlayerBase playerBase)
	{
		if (!GameHelperClient.coronationGuardActive || playerBase != GameHelperClient.localPlayer)
		{
			return;
		}
		GameHelperClient.guardedGem = GameHelperClient.EncodeCoronationValue(playerBase.gem);
		GameHelperClient.guardedGetGemNum = GameHelperClient.EncodeCoronationValue(playerBase.getGemNum);
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x0001F780 File Offset: 0x0001D980
	public static void TrackAttributes(PlayerBase playerBase)
	{
		if (!GameHelperClient.coronationGuardActive || playerBase != GameHelperClient.localPlayer)
		{
			return;
		}
		GameHelperClient.guardedSTR = GameHelperClient.EncodeCoronationValue(playerBase.mSTR);
		GameHelperClient.guardedSTA = GameHelperClient.EncodeCoronationValue(playerBase.sta);
		GameHelperClient.guardedAGI = GameHelperClient.EncodeCoronationValue(playerBase.agi);
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x0001F7D4 File Offset: 0x0001D9D4
	public static bool CheckCoronationGuard()
	{
		if (!GameHelperClient.coronationGuardActive)
		{
			return true;
		}
		if (GameHelperClient.DecodeCoronationValue(GameHelperClient.guardedRefreshNum) != GameHelperClient.RefreshNum)
		{
			GameHelperClient.DisableCoronation("refresh");
		}
		if (GameHelperClient.localPlayer != null)
		{
			if (GameHelperClient.DecodeCoronationValue(GameHelperClient.guardedGold) != GameHelperClient.localPlayer.gold)
			{
				GameHelperClient.DisableCoronation("gold");
			}
			if (GameHelperClient.DecodeCoronationValue(GameHelperClient.guardedGem) != GameHelperClient.localPlayer.gem)
			{
				GameHelperClient.DisableCoronation("gem");
			}
			if (GameHelperClient.DecodeCoronationValue(GameHelperClient.guardedGetGoldNum) != GameHelperClient.localPlayer.getGoldNum)
			{
				GameHelperClient.DisableCoronation("getGoldNum");
			}
			if (GameHelperClient.DecodeCoronationValue(GameHelperClient.guardedGetGemNum) != GameHelperClient.localPlayer.getGemNum)
			{
				GameHelperClient.DisableCoronation("getGemNum");
			}
			if (GameHelperClient.DecodeCoronationValue(GameHelperClient.guardedSTR) != GameHelperClient.localPlayer.mSTR)
			{
				GameHelperClient.DisableCoronation("str");
			}
			if (GameHelperClient.DecodeCoronationValue(GameHelperClient.guardedSTA) != GameHelperClient.localPlayer.sta)
			{
				GameHelperClient.DisableCoronation("sta");
			}
			if (GameHelperClient.DecodeCoronationValue(GameHelperClient.guardedAGI) != GameHelperClient.localPlayer.agi)
			{
				GameHelperClient.DisableCoronation("agi");
			}
		}
		return GameHelperClient.CanCoronation;
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x0001F8FA File Offset: 0x0001DAFA
	public static void CheckCardCheat()
	{
		if (GameHelperClient.IsEquipCardCapacityCheat())
		{
			GameHelperClient.DisableCoronation("equipCardCapacity");
		}
	}

	// Token: 0x06000570 RID: 1392 RVA: 0x0001F90D File Offset: 0x0001DB0D
	public static bool IsEquipCardCapacityCheat()
	{
		return GameHelperClient.GetEquipCardCapacityTotal() > SaveLoadManager.gameSaveData.maxCapacity;
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x0001F920 File Offset: 0x0001DB20
	public static int GetEquipCardCapacityTotal()
	{
		List<int> equipCards = SaveLoadManager.gameSaveData.equipCards;
		if (equipCards == null)
		{
			return 0;
		}
		int num = 0;
		foreach (int key in equipCards)
		{
			CardData cardData;
			if (Game.GameData.CardDataDic.TryGetValue(key, out cardData))
			{
				num += cardData.capacity;
			}
		}
		return num;
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x0001F998 File Offset: 0x0001DB98
	private static int EncodeCoronationValue(int value)
	{
		return value ^ GameHelperClient.coronationGuardKey;
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x0001F998 File Offset: 0x0001DB98
	private static int DecodeCoronationValue(int value)
	{
		return value ^ GameHelperClient.coronationGuardKey;
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x0001F9A4 File Offset: 0x0001DBA4
	private static void ResetCoronationGuardValues()
	{
		GameHelperClient.coronationGuardActive = false;
		GameHelperClient.coronationGuardKey = 1;
		GameHelperClient.guardedRefreshNum = GameHelperClient.EncodeCoronationValue(GameHelperClient.RefreshNum);
		GameHelperClient.guardedGold = GameHelperClient.EncodeCoronationValue(0);
		GameHelperClient.guardedGem = GameHelperClient.EncodeCoronationValue(0);
		GameHelperClient.guardedGetGoldNum = GameHelperClient.EncodeCoronationValue(0);
		GameHelperClient.guardedGetGemNum = GameHelperClient.EncodeCoronationValue(0);
		GameHelperClient.guardedSTR = GameHelperClient.EncodeCoronationValue(0);
		GameHelperClient.guardedSTA = GameHelperClient.EncodeCoronationValue(0);
		GameHelperClient.guardedAGI = GameHelperClient.EncodeCoronationValue(0);
		GameHelperClient.coronationGuardReported = false;
	}

	// Token: 0x06000575 RID: 1397 RVA: 0x0001FA1F File Offset: 0x0001DC1F
	private static void DisableCoronation(string reason)
	{
		GameHelperClient.CanCoronation = false;
		GameHelperClient.ReportCoronationGuardError(reason);
		Debug.LogWarning("Coronation disabled by guard: " + reason);
	}

	// Token: 0x06000576 RID: 1398 RVA: 0x0001FA40 File Offset: 0x0001DC40
	private static void ReportCoronationGuardError(string reason)
	{
		if (GameHelperClient.coronationGuardReported)
		{
			return;
		}
		GameHelperClient.coronationGuardReported = true;
		string text = (GameHelperClient.localPlayer != null) ? GameHelperClient.localPlayer.roleName : string.Empty;
		if (!NetworkServer.active || !GameHelperClient.isHost)
		{
			if (NetworkClient.isConnected)
			{
				NetworkClient.connection.Send<ServerNetMessage>(new ServerNetMessage
				{
					serverNetOperation = ServerNetOperation.ReportCoronationCheat,
					strData = text
				}, 0);
			}
			return;
		}
		MyServerNetworkManager myServerNetworkManager = NetworkManager.singleton as MyServerNetworkManager;
		if (myServerNetworkManager == null)
		{
			return;
		}
		myServerNetworkManager.RecordCoronationCheatPlayer(text);
	}

	// Token: 0x06000577 RID: 1399 RVA: 0x0001FACC File Offset: 0x0001DCCC
	public static void OnGameReset()
	{
		SaveLoadManager.SaveGameData();
		GameHelperClient.isHost = false;
		GameHelperClient.localPlayer = null;
		GameHelperClient.PlayerNum = 0;
		GameHelperClient.ChallengePlayerNum = 0;
		GameHelperClient.isReady = false;
		GameHelperClient.isGameOver = true;
		GameHelperClient.attackList.Clear();
		GameHelperClient.IsQiJiXingZhe = 0;
		GameHelperClient.RemainsNum = 0;
		GameHelperClient.WaveNum = 0;
		GameHelperClient.LevelIndex = 0;
		GameHelperClient.EliteProbabilityAdd = 0f;
		GameHelperClient.BuyMonsterIndex = 0;
		GameHelperClient.CantLearnActiveSkill = 0;
		GameHelperClient.CantLearnPasssiveSkill = 0;
		GameHelperClient.CountDownTime = 0f;
		GameHelperClient.RefreshNum = 5;
		GameHelperClient.ResetCoronationGuardValues();
		GameHelperClient.SkillBookId = 0;
		GameHelperClient.AddEnemyLimit = 0;
		GameHelperClient.MaxSkillNum = 4;
		GameHelperClient.MaxEquipNum = 6;
		GameHelperClient.isGameReset = true;
		GameHelperClient.InDungeon = false;
		GameHelperClient.GamePlayItemDatas.Clear();
		GameHelperClient.GamePlayItemIndex = 0;
		Simulator.Instance.Clear();
		GameHelperClient.IsMoveToAttack = false;
		GameHelperClient.IsExitGameOver = false;
		AssetManagerMirror.OnClear();
		GameHelperClient.ClickTrackRole = null;
		GameHelperClient.isFreeCamera = false;
		GameHelperClient.IsInputChat = false;
		GameHelperClient.AutoSellBookMask = 0;
		GameHelperClient.isKingBattle = false;
		GameHelperClient.isWin = false;
		GameHelperClient.CanCoronation = true;
		GameHelperClient.StopNet();
		SteamManager steamManager = EntityStatic.Get<SteamManager>();
		if (steamManager != null)
		{
			steamManager.OnApplicationQuit();
		}
		SteamManager.Reset();
		InputManager inputManager = EntityStatic.Get<InputManager>();
		if (inputManager != null)
		{
			inputManager.Clear();
		}
		AssetManager.Clear();
		EntityStatic.Clear();
		Main.Clear();
		SceneManager.LoadScene(0);
		Resources.UnloadUnusedAssets();
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x0001FC14 File Offset: 0x0001DE14
	public static void StopNet()
	{
		GameHelperClient.CloseLobbyOnHostExit();
		NetworkManager singleton = NetworkManager.singleton;
		if (singleton == null)
		{
			return;
		}
		MyServerNetworkManager myServerNetworkManager = singleton as MyServerNetworkManager;
		if (NetworkServer.active && NetworkClient.isConnected)
		{
			singleton.StopHost();
			if (myServerNetworkManager != null)
			{
				NetworkDiscovery networkDiscovery = myServerNetworkManager.NetworkDiscovery;
				if (networkDiscovery == null)
				{
					return;
				}
				networkDiscovery.StopDiscovery();
				return;
			}
		}
		else if (NetworkClient.isConnected)
		{
			singleton.StopClient();
			if (myServerNetworkManager != null)
			{
				NetworkDiscovery networkDiscovery2 = myServerNetworkManager.NetworkDiscovery;
				if (networkDiscovery2 == null)
				{
					return;
				}
				networkDiscovery2.StopDiscovery();
				return;
			}
		}
		else if (NetworkServer.active)
		{
			singleton.StopServer();
			if (myServerNetworkManager != null)
			{
				NetworkDiscovery networkDiscovery3 = myServerNetworkManager.NetworkDiscovery;
				if (networkDiscovery3 == null)
				{
					return;
				}
				networkDiscovery3.StopDiscovery();
			}
		}
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x0001FCA8 File Offset: 0x0001DEA8
	public static void CloseLobbyOnHostExit()
	{
		if (GameHelperClient.LobbyID != CSteamID.Nil)
		{
			SteamMatchmaking.SetLobbyJoinable(GameHelperClient.LobbyID, false);
			SteamMatchmaking.SetLobbyType(GameHelperClient.LobbyID, ELobbyType.k_ELobbyTypeInvisible);
			SteamMatchmaking.SetLobbyData(GameHelperClient.LobbyID, "playState", "closed");
			SteamMatchmaking.LeaveLobby(GameHelperClient.LobbyID);
			GameHelperClient.LobbyID = CSteamID.Nil;
		}
		if (GameHelperClient.JoinLobbyID != CSteamID.Nil)
		{
			SteamMatchmaking.LeaveLobby(GameHelperClient.JoinLobbyID);
			GameHelperClient.JoinLobbyID = CSteamID.Nil;
		}
	}

	// Token: 0x0600057A RID: 1402 RVA: 0x0001FD2C File Offset: 0x0001DF2C
	public static void LockLobbyOnGameStart()
	{
		if (GameHelperClient.LobbyID != CSteamID.Nil)
		{
			SteamMatchmaking.SetLobbyJoinable(GameHelperClient.LobbyID, false);
			SteamMatchmaking.SetLobbyData(GameHelperClient.LobbyID, "playState", "playing");
		}
	}

	// Token: 0x0600057B RID: 1403 RVA: 0x0001FD60 File Offset: 0x0001DF60
	public static RoleBuff AddShowBuff(string name, string info, string iconPath, float time)
	{
		RoleBuff roleBuff = GameHelperClient.localPlayer.roleBuffManager.AddOneBuff<Buff通用显示>(name, time);
		roleBuff.info = info;
		roleBuff.icon = iconPath;
		roleBuff.isShow = true;
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui == null)
		{
			return roleBuff;
		}
		ui.RefreshRelic();
		return roleBuff;
	}

	// Token: 0x0600057C RID: 1404 RVA: 0x0001FD9C File Offset: 0x0001DF9C
	public static int GetLocalSkillBookId()
	{
		GameHelperClient.SkillBookId++;
		return GameHelperClient.SkillBookId;
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x0001FDB0 File Offset: 0x0001DFB0
	public static int AddGamePlayItem(GamePlayItemData gamePlayItemData)
	{
		GameHelperClient.GamePlayItemIndex++;
		gamePlayItemData.id = GameHelperClient.GamePlayItemIndex;
		GameHelperClient.GamePlayItemDatas.Add(GameHelperClient.GamePlayItemIndex, gamePlayItemData);
		if (gamePlayItemData.gamePlayItemType == GamePlayItemType.Talk)
		{
			Game.UI.GetUI<UI_PlayerState>().ShowTalkUI(gamePlayItemData.targetRole, new string[]
			{
				"......"
			}, 9999f);
		}
		return GameHelperClient.GamePlayItemIndex;
	}

	// Token: 0x04000495 RID: 1173
	public static bool CanCoronation = true;

	// Token: 0x04000496 RID: 1174
	public static int WaveNum;

	// Token: 0x04000497 RID: 1175
	public static bool isHost;

	// Token: 0x04000498 RID: 1176
	public static SOSpawnConfig spawnConfig;

	// Token: 0x04000499 RID: 1177
	public static PlayerBase localPlayer;

	// Token: 0x0400049A RID: 1178
	public static int PlayerNum;

	// Token: 0x0400049B RID: 1179
	public static int ChallengePlayerNum;

	// Token: 0x0400049C RID: 1180
	public static SOGameConfig gameConfig;

	// Token: 0x0400049D RID: 1181
	public static float CountDownTime;

	// Token: 0x0400049E RID: 1182
	public static bool isReady;

	// Token: 0x0400049F RID: 1183
	public static bool InDungeon;

	// Token: 0x040004A0 RID: 1184
	public static bool isGameOver;

	// Token: 0x040004A1 RID: 1185
	public static bool isKingBattle;

	// Token: 0x040004A2 RID: 1186
	public static bool isGameReset;

	// Token: 0x040004A3 RID: 1187
	public static bool isWin;

	// Token: 0x040004A4 RID: 1188
	public static bool isFreeCamera;

	// Token: 0x040004A5 RID: 1189
	public static List<RoleBase> attackList = new List<RoleBase>(8);

	// Token: 0x040004A6 RID: 1190
	public static bool CanMoveCancel = true;

	// Token: 0x040004A7 RID: 1191
	public static bool IsAutoBattle = false;

	// Token: 0x040004A8 RID: 1192
	public static bool IsShowDamage = true;

	// Token: 0x040004A9 RID: 1193
	public static bool IsKeyPickTalisman = true;

	// Token: 0x040004AA RID: 1194
	public static bool IsAutoUseCard = false;

	// Token: 0x040004AB RID: 1195
	public static bool IsSmartCasting = false;

	// Token: 0x040004AC RID: 1196
	public static bool IsPickShare = true;

	// Token: 0x040004AD RID: 1197
	public static bool IsMoveToAttack = false;

	// Token: 0x040004AE RID: 1198
	public static bool IsInputChat = false;

	// Token: 0x040004AF RID: 1199
	public static int AutoSellBookMask = 0;

	// Token: 0x040004B0 RID: 1200
	public static GameHelperClient.PlayerNameDisplayMode playerNameDisplayMode = GameHelperClient.PlayerNameDisplayMode.HeroName;

	// Token: 0x040004B1 RID: 1201
	public static bool IsExitGameOver;

	// Token: 0x040004B2 RID: 1202
	public static float EliteProbabilityAdd = 0f;

	// Token: 0x040004B3 RID: 1203
	public static float YaLiValue;

	// Token: 0x040004B4 RID: 1204
	public static int LevelIndex = 0;

	// Token: 0x040004B5 RID: 1205
	public static int RemainsNum;

	// Token: 0x040004B6 RID: 1206
	public static int TalismanNum;

	// Token: 0x040004B7 RID: 1207
	public static Dictionary<int, GamePlayItemData> GamePlayItemDatas = new Dictionary<int, GamePlayItemData>();

	// Token: 0x040004B8 RID: 1208
	private static int GamePlayItemIndex;

	// Token: 0x040004B9 RID: 1209
	public static int MapLevel = 1;

	// Token: 0x040004BA RID: 1210
	public static RaycastHit[] RaycastHitPool = new RaycastHit[8];

	// Token: 0x040004BB RID: 1211
	public static RoleBase ClickTrackRole;

	// Token: 0x040004BC RID: 1212
	public static bool IsPublicMode = false;

	// Token: 0x040004BD RID: 1213
	public static bool IsJoyStick = false;

	// Token: 0x040004BE RID: 1214
	public static int IsQiJiXingZhe;

	// Token: 0x040004BF RID: 1215
	public static Vector3 PlayerCameraEuler;

	// Token: 0x040004C0 RID: 1216
	public static int BuyMonsterIndex = 0;

	// Token: 0x040004C1 RID: 1217
	public static bool isSaveHero = true;

	// Token: 0x040004C2 RID: 1218
	public static bool isSaveShop = false;

	// Token: 0x040004C3 RID: 1219
	public static int CantLearnActiveSkill = 0;

	// Token: 0x040004C4 RID: 1220
	public static int CantLearnPasssiveSkill = 0;

	// Token: 0x040004C5 RID: 1221
	public static UnityEngine.Vector2 CanSpellArea;

	// Token: 0x040004C6 RID: 1222
	public static UnityEngine.Vector2 NoSpellArea;

	// Token: 0x040004C7 RID: 1223
	public static int RefreshNum = 5;

	// Token: 0x040004C8 RID: 1224
	private static bool coronationGuardActive;

	// Token: 0x040004C9 RID: 1225
	private static int coronationGuardKey = 1;

	// Token: 0x040004CA RID: 1226
	private static int guardedRefreshNum;

	// Token: 0x040004CB RID: 1227
	private static int guardedGold;

	// Token: 0x040004CC RID: 1228
	private static int guardedGem;

	// Token: 0x040004CD RID: 1229
	private static int guardedGetGoldNum;

	// Token: 0x040004CE RID: 1230
	private static int guardedGetGemNum;

	// Token: 0x040004CF RID: 1231
	private static int guardedSTR;

	// Token: 0x040004D0 RID: 1232
	private static int guardedSTA;

	// Token: 0x040004D1 RID: 1233
	private static int guardedAGI;

	// Token: 0x040004D2 RID: 1234
	private static bool coronationGuardReported;

	// Token: 0x040004D3 RID: 1235
	public static int SkillBookId;

	// Token: 0x040004D4 RID: 1236
	public static CSteamID LobbyID;

	// Token: 0x040004D5 RID: 1237
	public static CSteamID JoinLobbyID;

	// Token: 0x040004D6 RID: 1238
	public static int AddEnemyLimit;

	// Token: 0x040004D7 RID: 1239
	public static int MaxSkillNum = 4;

	// Token: 0x040004D8 RID: 1240
	public static int MaxEquipNum = 6;

	// Token: 0x040004D9 RID: 1241
	public static string ScenePath;

	// Token: 0x040004DA RID: 1242
	public static bool CanSkillRefresh = true;

	// Token: 0x02000106 RID: 262
	public enum PlayerNameDisplayMode
	{
		// Token: 0x040004DC RID: 1244
		HeroName,
		// Token: 0x040004DD RID: 1245
		SteamName,
		// Token: 0x040004DE RID: 1246
		HideName
	}
}

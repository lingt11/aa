using System;
using System.Collections.Generic;
using System.Linq;
using kcp2k;
using Mirror;
using Mirror.Discovery;
using Mirror.FizzySteam;
using Steamworks;
using UnityEngine;

// Token: 0x020003DE RID: 990
public class MyServerNetworkManager : NetworkManager
{
	// Token: 0x060016C0 RID: 5824 RVA: 0x0008CCE0 File Offset: 0x0008AEE0
	public override void OnStartClient()
	{
		base.OnStartClient();
		Debug.Log("OnStartClient");
		NetworkClient.RegisterHandler<ClientNetMessage>(new Action<ClientNetMessage>(this.OnClientMatchMessage), true);
		Debug.Log("自己连接服务器成功");
		UI_StartGame ui = Game.UI.GetUI<UI_StartGame>();
		if (ui == null)
		{
			return;
		}
		ui.CreateRoomSuccess();
	}

	// Token: 0x060016C1 RID: 5825 RVA: 0x0008CD30 File Offset: 0x0008AF30
	public void OnSelectHeroOver()
	{
		this.isSelectHero = false;
		for (int i = 0; i < this.heroGameObjects.Count; i++)
		{
			Object.Destroy(this.heroGameObjects[i]);
		}
		Object.Destroy(GameObject.Find("Scenes/SelectHero"));
	}

	// Token: 0x060016C2 RID: 5826 RVA: 0x0008CD7C File Offset: 0x0008AF7C
	private void OnClientMatchMessage(ClientNetMessage msg)
	{
		if (!NetworkClient.active)
		{
			return;
		}
		switch (msg.clientNetOperation)
		{
		case ClientNetOperation.StartSelectHero:
		{
			this.myConnectionId = msg.data;
			this.selectHeros = msg.datas;
			Game.CameraManager.MyTransform.position = new Vector3(100f, 6.3f, 92.1f);
			Game.CameraManager.MyTransform.eulerAngles = new Vector3(40.8f, 0f, 0f);
			this.heroGameObjects = new List<GameObject>();
			for (int i = 0; i < this.selectHeros.Length; i++)
			{
				HeroType heroType = (HeroType)this.selectHeros[i];
				GameObject gameObject = AssetManager.LoadPrefab(Util.GetHeroModePath(heroType), null, true);
				if (gameObject != null)
				{
					RoleModeBase component = gameObject.GetComponent<RoleModeBase>();
					if (component != null)
					{
						LocalHeroModelService.TryApplyEnabledHeroPreview(component, heroType);
					}
				}
				float f = (float)i * 200f / (float)this.selectHeros.Length * 0.017453292f;
				float x = GameHelperClient.gameConfig.selectHeroPoint.x + 4f * Mathf.Cos(f);
				float z = GameHelperClient.gameConfig.selectHeroPoint.z + 4f * Mathf.Sin(f);
				Vector3 position = new Vector3(x, GameHelperClient.gameConfig.selectHeroPoint.y, z);
				gameObject.transform.position = position;
				gameObject.transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
				GameObject gameObject2 = new GameObject();
				gameObject2.name = gameObject.name;
				gameObject2.transform.position = gameObject.transform.position;
				gameObject2.transform.rotation = gameObject.transform.rotation;
				gameObject.transform.SetParent(gameObject2.transform);
				this.heroGameObjects.Add(gameObject2);
				CapsuleCollider capsuleCollider = gameObject2.AddComponent<CapsuleCollider>();
				capsuleCollider.radius = 0.7f;
				capsuleCollider.height = 1.8f;
				capsuleCollider.center = new Vector3(0f, 1f, 0f);
			}
			this.isSelectHero = true;
			UI_SelectHero ui = Game.UI.GetUI<UI_SelectHero>();
			if (ui == null)
			{
				return;
			}
			ui.ShowAllHero(this.heroGameObjects);
			return;
		}
		case ClientNetOperation.SelectHero:
			if (msg.strs != null && msg.strs.Length != 0)
			{
				string[] strs = msg.strs;
				for (int j = 0; j < strs.Length; j++)
				{
					string[] array = strs[j].Split('|', StringSplitOptions.None);
					int key = int.Parse(array[0]);
					HeroType value = (HeroType)int.Parse(array[1]);
					this.playerHeroes[key] = value;
				}
			}
			for (int k = 0; k < this.selectHeros.Length; k++)
			{
				GameObject gameObject3 = this.heroGameObjects[k];
				Transform child = gameObject3.transform.GetChild(gameObject3.transform.childCount - 1);
				GameObject gameObject4 = child.name.Equals(EffectDefine.SpellGroundTip) ? child.gameObject : null;
				gameObject3.GetComponent<CapsuleCollider>().enabled = true;
				int connectIdByHeroType = this.GetConnectIdByHeroType((HeroType)this.selectHeros[k]);
				if (connectIdByHeroType != -1)
				{
					if (gameObject4 == null)
					{
						GameObject gameObject5 = AssetManager.LoadPrefab((connectIdByHeroType == this.myConnectionId) ? EffectDefine.SpellGroundTip : EffectDefine.SpellGroundTipRed, null, true);
						gameObject5.name = EffectDefine.SpellGroundTip;
						gameObject5.transform.SetParent(gameObject3.transform);
						gameObject5.transform.localPosition = Vector3.zero;
						gameObject5.transform.localScale = Vector3.one;
					}
				}
				else if (gameObject4 != null)
				{
					Object.Destroy(gameObject4.gameObject);
				}
			}
			return;
		case ClientNetOperation.OnPlayerDisconnect:
			this.ClearDisconnectPlayer((uint)msg.data);
			GameHelperClient.PlayerNum--;
			Game.UI.GetUI<UI_Battle>().UpdatePlayerNum();
			return;
		case ClientNetOperation.OnStartGame:
			GameHelperClient.localPlayer.CmdUpdateHp(GameHelperClient.localPlayer.maxHp, GameHelperClient.localPlayer.netId, -1);
			GameHelperClient.localPlayer.AddMp(GameHelperClient.localPlayer.maxMp);
			GameHelperClient.isReady = false;
			GameHelperClient.WaveNum = msg.data;
			if (GameHelperClient.spawnConfig.levelStageType == LevelStageType.Wave)
			{
				GameHelperClient.CountDownTime = (float)GameHelperClient.spawnConfig.enemySpawnData[GameHelperClient.WaveNum].spawnTime;
			}
			else if (GameHelperClient.spawnConfig.levelStageType == LevelStageType.Stage)
			{
				int num = 0;
				foreach (SOSpawnConfig.EnemySpawnTime enemySpawnTime in GameHelperClient.spawnConfig.enemySpawnData)
				{
					num += enemySpawnTime.spawnTime;
				}
				GameHelperClient.CountDownTime = (float)num;
			}
			if (msg.datas != null && msg.datas.Length != 0)
			{
				int num2 = msg.datas[0];
				if (num2 > 0)
				{
					GameHelperClient.CountDownTime *= (float)num2;
				}
			}
			Game.UI.GetUI<UI_Battle>().UpdatePlayerNum();
			Game.UI.GetUI<UI_Battle>().OnStartGame();
			Game.UI.GetUI<UI_Shop>().OnStartGame();
			if (Game.UI.GetUI<UI_Shop>().isOpenShop)
			{
				Game.UI.GetUI<UI_Shop>().CloseAnim(false, false);
				return;
			}
			break;
		case ClientNetOperation.OnGameOver:
			GameHelperClient.isWin = (msg.data == 0);
			GameHelperClient.IsExitGameOver = false;
			this.StartShowGameResult();
			return;
		case ClientNetOperation.OnStartReady:
			Game.UI.DestroyUI<UI_StartGame>();
			Game.UI.DestroyUI<UI_SelectHero>();
			Game.UI.DestroyUI<UI_LobbyMsg>();
			Game.UI.CloseUI<UI_MyCard>();
			GameHelperClient.PlayerNum = msg.data;
			GameHelperClient.ChallengePlayerNum = GameHelperClient.PlayerNum;
			GameHelperClient.isReady = true;
			GameHelperClient.InDungeon = true;
			if (GameHelperClient.spawnConfig.levelStageType == LevelStageType.Wave)
			{
				GameHelperClient.CountDownTime = (float)GameHelperClient.gameConfig.ReadyTime;
			}
			Game.CameraManager.MyTransform.eulerAngles = GameHelperClient.PlayerCameraEuler;
			Game.UI.OpenUI<UI_Battle>(null);
			Game.UI.OpenUI<UI_Shop>(null);
			Game.UI.OpenUI<UI_DecTip>(null);
			Game.UI.GetUI<UI_Battle>().OnInitReady();
			(Game.UI.OpenUI<UI_Dialog>(null) as UI_Dialog).closeAction = new Action(this.OnGameReadyDialogClose);
			Game.CameraManager.UpdateTargetDistance(Game.CameraManager.MaxDistance);
			return;
		case ClientNetOperation.LobbyPlayerData:
		{
			LobbyManager lobbyManager = EntityStatic.Get<LobbyManager>();
			if (lobbyManager == null)
			{
				return;
			}
			lobbyManager.RefreshData(msg.strs);
			return;
		}
		case ClientNetOperation.EnterDungeon:
			Game.MainLogic.GameStart(false, msg.datas[0]);
			return;
		case ClientNetOperation.EnemyEnterTip:
			Game.UI.GetUI<UI_Battle>().ShowEnemyEnterTip((EnemyType)msg.datas[0]);
			return;
		case ClientNetOperation.OnStartRest:
		{
			Game.ItemManager.ClearAllTalismans();
			GameHelperClient.ClickTrackRole = null;
			GameHelperClient.isWin = true;
			GameHelperClient.isReady = true;
			GameHelperClient.WaveNum = msg.datas[0];
			if (msg.datas[1] == 1)
			{
				GameHelperClient.AddRefreshNum(4);
				UI_Shop ui2 = Game.UI.GetUI<UI_Shop>();
				if (ui2 != null)
				{
					ui2.UpdateRefreshNum();
				}
				Util.ShowTips("刷新次数增加");
			}
			MySystemEvent.Instance.DispatchMessage(38);
			Game.UI.GetUI<UI_Shop>().OnStartRest();
			bool isChallenge = false;
			bool isRemainsRoguelike = false;
			RoguelikeEventType[] eventList = GameHelperClient.spawnConfig.enemySpawnData[GameHelperClient.WaveNum].eventList;
			if (eventList != null && eventList.Length != 0)
			{
				foreach (RoguelikeEventType roguelikeEventType in eventList)
				{
					if (roguelikeEventType != RoguelikeEventType.Challenge)
					{
						if (roguelikeEventType == RoguelikeEventType.Roguelike)
						{
							this.QueueRestRemainsRoguelike(1.5f);
							isRemainsRoguelike = true;
						}
					}
					else
					{
						isChallenge = true;
					}
				}
			}
			Game.UI.GetUI<UI_Battle>().OnStartRest(isChallenge, GameHelperClient.spawnConfig.enemySpawnData[GameHelperClient.WaveNum].bossNum > 0, isRemainsRoguelike);
			if (GameHelperClient.localPlayer.IsDead())
			{
				GameHelperClient.localPlayer.CmdRelife();
			}
			Game.EnemyManagerClient.OnGameOver(GameHelperClient.isWin);
			GameHelperClient.localPlayer.trackRoleBase = null;
			GameHelperClient.localPlayer.playerAttribute.OnWaveAdd();
			return;
		}
		case ClientNetOperation.UpdateReady:
			if (msg.strs != null && msg.strs.Length != 0)
			{
				List<HeroType> list = new List<HeroType>();
				bool isReady = false;
				for (int m = 0; m < msg.strs.Length; m++)
				{
					string[] array2 = msg.strs[m].Split('|', StringSplitOptions.None);
					int num3 = int.Parse(array2[0]);
					HeroType heroType2 = (HeroType)int.Parse(array2[1]);
					if (heroType2 != HeroType.None)
					{
						list.Add(heroType2);
						if (num3 == this.myConnectionId)
						{
							isReady = true;
						}
					}
				}
				Game.UI.GetUI<UI_Battle>().UpdateReadyState(isReady);
				Game.UI.GetUI<UI_Battle>().UpdateReadyTip(list);
				return;
			}
			break;
		case ClientNetOperation.OnStartKing:
		{
			GameHelperClient.isKingBattle = true;
			if (GameHelperClient.localPlayer.IsDead())
			{
				GameHelperClient.localPlayer.CmdRelife();
			}
			GameHelperClient.localPlayer.CmdUpdateHp(GameHelperClient.localPlayer.maxHp, GameHelperClient.localPlayer.netId, -1);
			GameHelperClient.localPlayer.AddMp(GameHelperClient.localPlayer.maxMp);
			GameHelperClient.isReady = false;
			GameHelperClient.CountDownTime = GameHelperClient.GetKingBattleTime();
			Game.UI.GetUI<UI_Battle>().UpdatePlayerNum();
			Game.UI.GetUI<UI_Battle>().OnStartGame();
			Game.UI.GetUI<UI_Battle>().ShowMask();
			Game.UI.GetUI<UI_Shop>().OnStartGame();
			if (Game.UI.GetUI<UI_Shop>().isOpenShop)
			{
				Game.UI.GetUI<UI_Shop>().CloseAnim(false, false);
			}
			GameHelperClient.NoSpellArea = Vector2.zero;
			GameHelperClient.CanSpellArea = new Vector2(12.5f, 12.5f);
			int data = msg.data;
			Vector2 createPos = this.createKingPosOffset[data];
			Game.TimerManager.AddTimer(0.35f, delegate()
			{
				GameObject gameObject6 = GameObject.Find(GameHelperClient.ScenePath);
				if (gameObject6 != null)
				{
					gameObject6.transform.Find("KingBattle").gameObject.SetActive(true);
				}
				Object.Destroy(GameObject.Find(GameHelperClient.ScenePath + "/Center"));
				GameHelperClient.localPlayer.CmdTeleportForPos(new Vector3(-this.createKingPos.x - createPos.x, 0f, -this.createKingPos.y - createPos.y));
				Game.TimerManager.AddTimer(5f, delegate()
				{
					GameHelperClient.localPlayer.roleBuffManager.AddOneBuff<Buff无敌>("Buff无敌", 2f);
					Game.EnemyManagerClient.StartKingBattle();
					GameHelperClient.isGameOver = false;
				});
				Game.UI.GetUI<UI_Battle>().StartKingBattle(5f);
			});
			GameHelperClient.localPlayer.SetRotationY(10f);
			GameHelperClient.localPlayer.UploadLocalKingData();
			return;
		}
		case ClientNetOperation.GameOverResult:
			Game.UI.OpenUI<UI_Settlement>("win");
			return;
		case ClientNetOperation.KingBattleResult:
		{
			GameHelperClient.isGameOver = true;
			bool flag = msg.datas[0] == 1;
			if (flag)
			{
				Util.ShowTips("王位加冕挑战成功");
			}
			else
			{
				Util.ShowTips("王位加冕挑战失败");
			}
			MySystemEvent.Instance.DispatchMessage<bool>(41, flag);
			AnalyticsManager analytics = Game.Analytics;
			if (analytics != null)
			{
				analytics.RecordKingChallengeResult(flag);
			}
			Game.EnemyManagerClient.OnGameOver(flag);
			Game.PlayerManagerClient.OnGameOver(flag);
			if (GameHelperClient.isHost)
			{
				if (flag)
				{
					SteamLeaderboardRankOrder.ApplyChallengeWin(SaveLoadManager.teamBuildDataList, this.challengeTargetTeamBuildData, this.uploadTeamBuildData);
				}
				else
				{
					this.uploadTeamBuildData.rank = -999;
					this.uploadTeamBuildData.order = 10000;
				}
				(Game.UI.OpenUI<UI_Confirm>(null) as UI_Confirm).SetConfirmText((flag ? Game.Language.Get("王位加冕挑战成功", "") : Game.Language.Get("王位加冕挑战失败", "")) + Game.Language.Get("是否上传排名", "") + StringDefine.Wrap + StringDefine.Wrap, new Action(this.OnUpdateLoadKingData), new Action(this.OnCancelLoadKingData), new Action<string>(this.OnUpdateLoadInputField), Game.Language.Get("输入你的留言", ""));
				return;
			}
			Game.TimerManager.AddTimer(3.5f, delegate()
			{
				Game.UI.OpenUI<UI_Settlement>("win");
			});
			return;
		}
		case ClientNetOperation.LobbyChat:
			if (msg.strs != null && msg.strs.Length > 1)
			{
				int colorIndex = (msg.datas != null && msg.datas.Length != 0) ? msg.datas[0] : 0;
				UI_LobbyMsg ui3 = Game.UI.GetUI<UI_LobbyMsg>();
				if (ui3 == null)
				{
					return;
				}
				ui3.ShowLobbyChat(msg.strs[0], msg.strs[1], colorIndex);
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x060016C3 RID: 5827 RVA: 0x0008D8F0 File Offset: 0x0008BAF0
	public void StartShowGameResult()
	{
		GameHelperClient.localPlayer.playerAttribute.OnGameOver();
		Game.ItemManager.ClearAllItems();
		GameHelperClient.isGameOver = true;
		GameHelperClient.CheckCoronationGuard();
		AnalyticsManager analytics = Game.Analytics;
		if (analytics != null)
		{
			analytics.UploadGameOverAnalytics(GameHelperClient.isWin);
		}
		GameHelperClient.localPlayer.StartUploadGameOverData();
		Game.UI.GetUI<UI_Battle>().OnExitDead();
		Game.EnemyManagerClient.OnGameOver(GameHelperClient.isWin);
		Game.PlayerManagerClient.OnGameOver(GameHelperClient.isWin);
		if (GameHelperClient.WaveNum > 30 || !GameHelperClient.isHost || GameHelperClient.IsEquipCardCapacityCheat() || this.HasCoronationCheatPlayer() || !GameHelperClient.CheckCoronationGuard())
		{
			GameHelperClient.CanCoronation = false;
		}
		if (GameHelperClient.isHost && this.HasCoronationCheatPlayer())
		{
			this.BroadcastCoronationCheatTip(null);
		}
		if (GameHelperClient.isWin && GameHelperClient.CanCoronation)
		{
			this.steamLeaderboardManager = new SteamLeaderboardManager();
			this.steamLeaderboardManager.Init();
			this.getKingListOver = false;
			this.getKingListTime = Time.time;
			this.steamLeaderboardBatchLoader.GetChallengePlayersData(SteamLeaderboardManager.GetLEADERBOARDName(GameHelperClient.ChallengePlayerNum), new Action<List<SaveLoadManager.TeamBuildData>>(this.OnLoadRankOver));
		}
		Game.TimerManager.AddTimer(3f, new Action(this.ShowGameOverResult));
		for (int i = GameHelperClient.localPlayer.playerAttribute.bagItemList.Count - 1; i > -1; i--)
		{
			BagItem bagItem = GameHelperClient.localPlayer.playerAttribute.bagItemList[i];
			if (bagItem.bagItemType == BagItemType.Card)
			{
				GameHelperClient.localPlayer.playerAttribute.UseBook(bagItem);
			}
		}
		if (GameHelperClient.isWin)
		{
			Util.ShowTips("tip_win");
			MySystemEvent.Instance.DispatchMessage(32);
			return;
		}
		Util.ShowTips("tip_failed");
	}

	// Token: 0x060016C4 RID: 5828 RVA: 0x0008DA9B File Offset: 0x0008BC9B
	private void QueueRestRemainsRoguelike(float delayShowTime)
	{
		this.hasPendingRestRemainsRoguelike = true;
		this.pendingRestRemainsDelay = delayShowTime;
		this.TryShowPendingRestRemainsRoguelike();
	}

	// Token: 0x060016C5 RID: 5829 RVA: 0x0008DAB4 File Offset: 0x0008BCB4
	private void TryShowPendingRestRemainsRoguelike()
	{
		if (!this.hasPendingRestRemainsRoguelike)
		{
			return;
		}
		UI_Roguelike ui = Game.UI.GetUI<UI_Roguelike>();
		if (ui != null)
		{
			UI_Roguelike ui_Roguelike = ui;
			ui_Roguelike.CloseAction = (Action)Delegate.Remove(ui_Roguelike.CloseAction, new Action(this.TryShowPendingRestRemainsRoguelike));
			if (ui.isOpen)
			{
				this.pendingRestRemainsDelay = 0.5f;
				UI_Roguelike ui_Roguelike2 = ui;
				ui_Roguelike2.CloseAction = (Action)Delegate.Combine(ui_Roguelike2.CloseAction, new Action(this.TryShowPendingRestRemainsRoguelike));
				return;
			}
		}
		UI_PlayerState ui2 = Game.UI.GetUI<UI_PlayerState>();
		if (ui2 != null)
		{
			UI_PlayerState ui_PlayerState = ui2;
			ui_PlayerState.OnGiveupBtnClickCallback = (Action)Delegate.Remove(ui_PlayerState.OnGiveupBtnClickCallback, new Action(this.TryShowPendingRestRemainsRoguelike));
			if (ui2.IsSwitchSkill)
			{
				this.pendingRestRemainsDelay = 0.5f;
				UI_PlayerState ui_PlayerState2 = ui2;
				ui_PlayerState2.OnGiveupBtnClickCallback = (Action)Delegate.Combine(ui_PlayerState2.OnGiveupBtnClickCallback, new Action(this.TryShowPendingRestRemainsRoguelike));
				return;
			}
		}
		this.hasPendingRestRemainsRoguelike = false;
		Util.ShowRemainsRoguelike(null, this.pendingRestRemainsDelay);
	}

	// Token: 0x060016C6 RID: 5830 RVA: 0x0008DBA9 File Offset: 0x0008BDA9
	private void OnLoadRankOver(List<SaveLoadManager.TeamBuildData> datas)
	{
		this.getKingListOver = true;
		if (datas == null || datas.Count == 0)
		{
			return;
		}
		SaveLoadManager.teamBuildDataList = datas;
	}

	// Token: 0x060016C7 RID: 5831 RVA: 0x0008DBC4 File Offset: 0x0008BDC4
	private void OnGameReadyDialogClose()
	{
		Util.ShowRemainsRoguelike(new Action(this.OnGameStartRoguelikeEnd), 0f);
	}

	// Token: 0x060016C8 RID: 5832 RVA: 0x0008DBDC File Offset: 0x0008BDDC
	private void OnGameStartRoguelikeEnd()
	{
		Game.PlayerManagerClient.InitRookieGuideManager();
		Game.UI.GetUI<UI_PlayerState>().InitTeleport();
	}

	// Token: 0x060016C9 RID: 5833 RVA: 0x0008DBF8 File Offset: 0x0008BDF8
	private void ShowGameOverResult()
	{
		if (!GameHelperClient.isWin)
		{
			Game.UI.OpenUI<UI_Settlement>("failed");
			return;
		}
		if (!GameHelperClient.isHost)
		{
			Util.ShowTips("等待房主选择挑战");
			return;
		}
		if (GameHelperClient.CanCoronation && !this.HasCoronationCheatPlayer() && GameHelperClient.CheckCoronationGuard())
		{
			(Game.UI.OpenUI<UI_Confirm>(null) as UI_Confirm).SetConfirmText(Game.Language.Get("加冕挑战提示", ""), new Action(this.WinChallenge), new Action(this.WinGameOver), null, "");
			return;
		}
		NetworkClient.connection.Send<ServerNetMessage>(new ServerNetMessage
		{
			serverNetOperation = ServerNetOperation.GameOverResult
		}, 0);
	}

	// Token: 0x060016CA RID: 5834 RVA: 0x0008DCAC File Offset: 0x0008BEAC
	private void WinGameOver()
	{
		NetworkClient.connection.Send<ServerNetMessage>(new ServerNetMessage
		{
			serverNetOperation = ServerNetOperation.GameOverResult
		}, 0);
	}

	// Token: 0x060016CB RID: 5835 RVA: 0x0008DCD8 File Offset: 0x0008BED8
	private void WinChallenge()
	{
		if (SaveLoadManager.teamBuildDataList != null && SaveLoadManager.teamBuildDataList.Count > 0)
		{
			Util.SelectKingChallenge();
			return;
		}
		(Game.UI.OpenUI<UI_Confirm>(null) as UI_Confirm).SetConfirmText(Game.Language.Get("加冕数据提示", ""), new Action(this.WinChallenge), new Action(this.WinGameOver), null, "");
		if (this.getKingListOver || Time.time - this.getKingListTime > 10f)
		{
			this.getKingListOver = false;
			this.getKingListTime = Time.time;
			this.steamLeaderboardBatchLoader.GetChallengePlayersData(SteamLeaderboardManager.GetLEADERBOARDName(GameHelperClient.ChallengePlayerNum), new Action<List<SaveLoadManager.TeamBuildData>>(this.OnLoadRankOver));
		}
	}

	// Token: 0x060016CC RID: 5836 RVA: 0x0008DD94 File Offset: 0x0008BF94
	private int GetConnectIdByHeroType(HeroType heroType)
	{
		for (int i = 0; i < this.playerHeroes.Count; i++)
		{
			if (this.playerHeroes.ElementAt(i).Value == heroType)
			{
				return this.playerHeroes.ElementAt(i).Key;
			}
		}
		return -1;
	}

	// Token: 0x060016CD RID: 5837 RVA: 0x0008DDE4 File Offset: 0x0008BFE4
	public override void OnClientConnect(NetworkConnection conn)
	{
		base.OnClientConnect(conn);
		if (Launch.quickStart)
		{
			Game.MainLogic.GameStart(true, 1);
		}
	}

	// Token: 0x060016CE RID: 5838 RVA: 0x0008DE00 File Offset: 0x0008C000
	private void UpdateSelectHero()
	{
		if (GameHelperClient.IsJoyStick || EntityStatic.Get<CameraManager>() == null)
		{
			return;
		}
		UI_MyCard ui = Game.UI.GetUI<UI_MyCard>();
		if (ui != null && ui.IsOpen())
		{
			return;
		}
		Ray ray = Game.Camera.ScreenPointToRay(Input.mousePosition);
		GameObject gameObject = null;
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit))
		{
			gameObject = raycastHit.collider.gameObject;
		}
		if (gameObject != null)
		{
			int num = this.heroGameObjects.IndexOf(gameObject);
			if (num >= 0)
			{
				if (Input.GetMouseButtonDown(0))
				{
					this.OnSelectHero(num);
					return;
				}
				gameObject.transform.localScale = new Vector3(1.35f, 1.35f, 1.35f);
				int index = gameObject.name.Split('_', StringSplitOptions.None)[1].ToInt32();
				UI_SelectHero ui2 = Game.UI.GetUI<UI_SelectHero>();
				if (ui2 != null)
				{
					ui2.ShowHeroInfo(index);
				}
			}
			else
			{
				UI_SelectHero ui3 = Game.UI.GetUI<UI_SelectHero>();
				if (ui3 != null)
				{
					ui3.ShowHeroInfo(-1);
				}
			}
			for (int i = 0; i < this.heroGameObjects.Count; i++)
			{
				if (num != i)
				{
					this.heroGameObjects[i].transform.localScale = new Vector3(1f, 1f, 1f);
				}
			}
		}
	}

	// Token: 0x060016CF RID: 5839 RVA: 0x0008DF38 File Offset: 0x0008C138
	public void OnSelectHero(int heroIndex)
	{
		if (Game.AudioManager != null)
		{
			Game.AudioManager.PlayAudio("Audio/btn2", 1f, 3f);
		}
		int connectIdByHeroType = this.GetConnectIdByHeroType((HeroType)this.selectHeros[heroIndex]);
		if (connectIdByHeroType != -1 && connectIdByHeroType != this.myConnectionId)
		{
			return;
		}
		NetworkClient.connection.Send<ServerNetMessage>(new ServerNetMessage
		{
			serverNetOperation = ServerNetOperation.SelectHero,
			datas = new int[]
			{
				this.selectHeros[heroIndex]
			}
		}, 0);
	}

	// Token: 0x060016D0 RID: 5840 RVA: 0x0008DFB8 File Offset: 0x0008C1B8
	public override void OnClientDisconnect(NetworkConnection conn)
	{
		if (GameHelperClient.isGameReset)
		{
			return;
		}
		UGUIManager ui = Game.UI;
		UGUICtrl uguictrl = (ui != null) ? ui.OpenUI<UI_Confirm>(null) : null;
		if (uguictrl != null)
		{
			(uguictrl as UI_Confirm).SetConfirmText(Game.Language.Get("服务器断开提示", ""), new Action(this.OnQuitCallBack), null, null, "");
		}
	}

	// Token: 0x060016D1 RID: 5841 RVA: 0x00080227 File Offset: 0x0007E427
	private void OnQuitCallBack()
	{
		GameHelperClient.OnGameReset();
	}

	// Token: 0x060016D2 RID: 5842 RVA: 0x0008E015 File Offset: 0x0008C215
	public void UploadLocalKingData(SaveLoadManager.PlayerKingData kingData)
	{
		this.uploadTeamBuildData.members.Add(kingData);
	}

	// Token: 0x170000DB RID: 219
	// (get) Token: 0x060016D3 RID: 5843 RVA: 0x0008E028 File Offset: 0x0008C228
	public SteamLeaderboardBatchLoader MySteamLeaderboardBatchLoader
	{
		get
		{
			return this.steamLeaderboardBatchLoader;
		}
	}

	// Token: 0x170000DC RID: 220
	// (get) Token: 0x060016D4 RID: 5844 RVA: 0x0008E030 File Offset: 0x0008C230
	public Dictionary<long, ServerResponse> DiscoveredServers
	{
		get
		{
			return this.discoveredServers;
		}
	}

	// Token: 0x170000DD RID: 221
	// (get) Token: 0x060016D5 RID: 5845 RVA: 0x0008E038 File Offset: 0x0008C238
	public NetworkDiscovery NetworkDiscovery
	{
		get
		{
			return this.networkDiscovery;
		}
	}

	// Token: 0x060016D6 RID: 5846 RVA: 0x0008E040 File Offset: 0x0008C240
	public override void Awake()
	{
		base.Awake();
		this.networkDiscovery = base.gameObject.GetComponent<NetworkDiscovery>();
	}

	// Token: 0x060016D7 RID: 5847 RVA: 0x0008E05C File Offset: 0x0008C25C
	private MyServerNetworkManager()
	{
		this.enemyManagerServer = new EnemyManagerServer();
		this.steamLeaderboardBatchLoader = new SteamLeaderboardBatchLoader();
		GameHelperClient.isGameOver = false;
		GameHelperClient.isGameReset = false;
	}

	// Token: 0x060016D8 RID: 5848 RVA: 0x0008E15C File Offset: 0x0008C35C
	public override void OnServerConnect(NetworkConnection conn)
	{
		base.OnServerConnect(conn);
		Debug.Log("======有玩家加入==========");
		if (GameHelperClient.LobbyID != CSteamID.Nil)
		{
			List<LobbyManager.LobbyPlayerInfo> list = new List<LobbyManager.LobbyPlayerInfo>();
			int numLobbyMembers = SteamMatchmaking.GetNumLobbyMembers(GameHelperClient.LobbyID);
			Debug.Log(string.Format("当前房间成员数量: {0}", numLobbyMembers));
			for (int i = 0; i < numLobbyMembers; i++)
			{
				CSteamID lobbyMemberByIndex = SteamMatchmaking.GetLobbyMemberByIndex(GameHelperClient.LobbyID, i);
				string friendPersonaName = SteamFriends.GetFriendPersonaName(lobbyMemberByIndex);
				list.Add(new LobbyManager.LobbyPlayerInfo
				{
					playerAddress = lobbyMemberByIndex.ToString(),
					playerName = friendPersonaName,
					playerHead = lobbyMemberByIndex.ToString()
				});
			}
			MySystemEvent.Instance.DispatchMessage<List<LobbyManager.LobbyPlayerInfo>>(28, list);
			return;
		}
		LobbyManager.LobbyPlayerInfo data = default(LobbyManager.LobbyPlayerInfo);
		data.playerAddress = conn.address;
		data.playerName = conn.address;
		MySystemEvent.Instance.DispatchMessage<LobbyManager.LobbyPlayerInfo>(27, data);
	}

	// Token: 0x060016D9 RID: 5849 RVA: 0x0008E254 File Offset: 0x0008C454
	public void SetTransport(int type)
	{
		if (type == 1)
		{
			Transport.activeTransport = this.kcpNet;
			this.networkDiscovery.transport = this.kcpNet;
			this.transport = this.kcpNet;
			return;
		}
		Transport.activeTransport = this.steamNet;
		this.networkDiscovery.transport = this.steamNet;
		this.transport = this.steamNet;
	}

	// Token: 0x060016DA RID: 5850 RVA: 0x0008E2B6 File Offset: 0x0008C4B6
	private void Update()
	{
		this.enemyManagerServer.UpdateEvent();
		if (this.severStageManager != null)
		{
			this.severStageManager.OnUpdate();
		}
		if (this.isSelectHero)
		{
			this.UpdateSelectHero();
		}
	}

	// Token: 0x060016DB RID: 5851 RVA: 0x0008E2E4 File Offset: 0x0008C4E4
	public override void OnStartServer()
	{
		base.OnStartServer();
		Debug.Log("OnStartServer");
		GameHelperClient.isHost = true;
		this.coronationCheatPlayerNames.Clear();
		this.coronationCheatTipSent = false;
		NetworkServer.RegisterHandler<ServerNetMessage>(new Action<NetworkConnection, ServerNetMessage>(this.OnServerMatchMessage), true);
		List<HeroType> list = new List<HeroType>();
		foreach (object obj in Enum.GetValues(typeof(HeroType)))
		{
			HeroType heroType = (HeroType)obj;
			if (heroType != HeroType.None)
			{
				if (GameHelperClient.isSaveHero)
				{
					Dictionary<string, RoleAttribute> heroAttributeDic = Game.GameData.HeroAttributeDic;
					int index = (int)heroType;
					if (heroAttributeDic[index.ToString()].isSave)
					{
						Dictionary<string, RoleAttribute> heroAttributeDic2 = Game.GameData.HeroAttributeDic;
						index = (int)heroType;
						if (!heroAttributeDic2[index.ToString()].isSaveMode)
						{
							continue;
						}
					}
				}
				list.Add(heroType);
			}
		}
		int count = list.Count;
		for (int i = 0; i < count; i++)
		{
			int num = Random.Range(0, count);
			List<HeroType> list2 = list;
			int index = i;
			List<HeroType> list3 = list;
			int index2 = num;
			HeroType value = list[num];
			HeroType value2 = list[i];
			list2[index] = value;
			list3[index2] = value2;
		}
		for (int j = 0; j < this.selectHeros.Length; j++)
		{
			this.selectHeros[j] = (int)list[j];
		}
	}

	// Token: 0x060016DC RID: 5852 RVA: 0x0008E458 File Offset: 0x0008C658
	private void OnServerMatchMessage(NetworkConnection conn, ServerNetMessage msg)
	{
		if (!NetworkServer.active)
		{
			return;
		}
		switch (msg.serverNetOperation)
		{
		case ServerNetOperation.CreatePlayer:
			this.ServerSendToClient(conn, new ClientNetMessage
			{
				clientNetOperation = ClientNetOperation.StartSelectHero,
				datas = this.selectHeros,
				data = conn.connectionId
			});
			this.ServerSendToClient(conn, new ClientNetMessage
			{
				clientNetOperation = ClientNetOperation.SelectHero,
				strs = this.GetSendSelectHeroData()
			});
			return;
		case ServerNetOperation.SelectHero:
		{
			if (this.playerHeroes.ContainsKey(conn.connectionId))
			{
				if (this.playerHeroes[conn.connectionId] == (HeroType)msg.datas[0])
				{
					this.playerHeroes[conn.connectionId] = HeroType.None;
				}
				else
				{
					this.playerHeroes[conn.connectionId] = (HeroType)msg.datas[0];
				}
			}
			else
			{
				this.playerHeroes.Add(conn.connectionId, (HeroType)msg.datas[0]);
			}
			bool flag = true;
			for (int i = 0; i < NetworkServer.connections.Keys.Count; i++)
			{
				NetworkConnectionToClient networkConnectionToClient = NetworkServer.connections.Values.ElementAt(i);
				if (!this.playerHeroes.ContainsKey(networkConnectionToClient.connectionId) || this.playerHeroes[networkConnectionToClient.connectionId] == HeroType.None)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				for (int j = 0; j < NetworkServer.connections.Count; j++)
				{
					NetworkConnectionToClient networkConnectionToClient2 = NetworkServer.connections.Values.ElementAt(j);
					if (this.playerHeroes.ContainsKey(networkConnectionToClient2.connectionId))
					{
						this.MyServerAddPlayer(networkConnectionToClient2, this.playerHeroes[networkConnectionToClient2.connectionId]);
					}
				}
				this.OnServerSelectHeroComp();
				return;
			}
			this.ServerSendAllPlayer(new ClientNetMessage
			{
				clientNetOperation = ClientNetOperation.SelectHero,
				strs = this.GetSendSelectHeroData()
			});
			return;
		}
		case ServerNetOperation.OnBuyShop:
		{
			int num = msg.datas[0];
			this.enemyManagerServer.OnAddBuyItemMonster(conn.identity.netId);
			return;
		}
		case ServerNetOperation.EnterDungeon:
			this.ServerSendAllPlayer(new ClientNetMessage
			{
				clientNetOperation = ClientNetOperation.EnterDungeon,
				datas = msg.datas
			});
			return;
		case ServerNetOperation.Ready:
		{
			bool flag2;
			if (this.playerReady.TryGetValue(conn.connectionId, out flag2))
			{
				this.playerReady[conn.connectionId] = !flag2;
			}
			else
			{
				this.playerReady[conn.connectionId] = true;
			}
			bool flag3 = true;
			for (int k = 0; k < NetworkServer.connections.Keys.Count; k++)
			{
				NetworkConnectionToClient networkConnectionToClient3 = NetworkServer.connections.Values.ElementAt(k);
				if (!this.playerReady.ContainsKey(networkConnectionToClient3.connectionId) || !this.playerReady[networkConnectionToClient3.connectionId])
				{
					flag3 = false;
					break;
				}
			}
			if (msg.strData != null && msg.strData.Length > 0)
			{
				this.startGameStrData = msg.strData;
			}
			if (flag3)
			{
				this.OnStartGame();
				return;
			}
			string[] array = new string[this.playerHeroes.Count];
			for (int l = 0; l < this.playerHeroes.Keys.Count; l++)
			{
				int key = this.playerHeroes.Keys.ElementAt(l);
				bool flag4;
				if (this.playerReady.TryGetValue(key, out flag4))
				{
					array[l] = key.ToString() + "|" + ((int)(flag4 ? this.playerHeroes[key] : HeroType.None)).ToString();
				}
				else
				{
					array[l] = key.ToString() + "|" + 0.ToString();
				}
			}
			this.ServerSendAllPlayer(new ClientNetMessage
			{
				clientNetOperation = ClientNetOperation.UpdateReady,
				strs = array
			});
			return;
		}
		case ServerNetOperation.CreateKing:
		{
			int num2 = msg.datas[0];
			SaveLoadManager.PlayerKingData playerKingData = SaveLoadManager.playerKingSave.playerKingDataList[num2];
			this.MyServerAddPlayerKing(conn, playerKingData, (uint)msg.datas[1], GameHelperClient.localPlayer.MyTransform.position + GameHelperClient.localPlayer.MyTransform.forward * 3f, 0U);
			return;
		}
		case ServerNetOperation.KingChallenge:
		{
			int num2 = msg.datas[0];
			List<SaveLoadManager.TeamBuildData> teamBuildDataList = SaveLoadManager.teamBuildDataList;
			if (teamBuildDataList != null && num2 >= 0 && num2 < teamBuildDataList.Count && SteamLeaderboardRankOrder.HasCompleteBuildData(teamBuildDataList[num2]))
			{
				List<SaveLoadManager.PlayerKingData> members = teamBuildDataList[num2].members;
				this.challengeTargetTeamBuildData = teamBuildDataList[num2];
				this.uploadTeamBuildData = new SaveLoadManager.TeamBuildData();
				this.uploadTeamBuildData.members = new List<SaveLoadManager.PlayerKingData>();
				this.uploadTeamBuildData.rank = this.challengeTargetTeamBuildData.rank;
				this.uploadTeamBuildData.order = this.challengeTargetTeamBuildData.order;
				List<uint> list = new List<uint>();
				Dictionary<int, NetworkConnectionToClient>.Enumerator enumerator = NetworkServer.connections.GetEnumerator();
				int num3 = 0;
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, NetworkConnectionToClient> keyValuePair = enumerator.Current;
					NetworkConnectionToClient value = keyValuePair.Value;
					RoleBase roleBase = (value.identity != null) ? value.identity.GetComponent<RoleBase>() : null;
					if (!(roleBase == null))
					{
						ClientNetMessage clientNetMessage = new ClientNetMessage
						{
							clientNetOperation = ClientNetOperation.OnStartKing,
							data = num3
						};
						this.ServerSendToClient(value, clientNetMessage);
						list.Add(roleBase.netId);
						num3++;
					}
				}
				enumerator.Dispose();
				if (list.Count != 0)
				{
					uint num4 = (GameHelperClient.localPlayer != null) ? GameHelperClient.localPlayer.netId : list[0];
					for (int m = 0; m < members.Count; m++)
					{
						SaveLoadManager.PlayerKingData playerKingData = members[m];
						Vector2 vector = this.createKingPosOffset[Mathf.Min(m, this.createKingPosOffset.Length - 1)];
						uint trackPlayerId = (m < list.Count) ? list[m] : num4;
						this.MyServerAddPlayerKing(conn, playerKingData, num4, new Vector3(this.createKingPos.x + vector.x, 0f, this.createKingPos.y + vector.y), trackPlayerId);
					}
					return;
				}
			}
			break;
		}
		case ServerNetOperation.GameOverResult:
			this.ServerSendAllPlayer(new ClientNetMessage
			{
				clientNetOperation = ClientNetOperation.GameOverResult
			});
			return;
		case ServerNetOperation.KingBattleResult:
			GameHelperClient.isGameOver = true;
			this.ServerSendAllPlayer(new ClientNetMessage
			{
				clientNetOperation = ClientNetOperation.KingBattleResult,
				datas = msg.datas
			});
			return;
		case ServerNetOperation.ReportCoronationCheat:
		{
			string coronationCheatRoleName = this.GetCoronationCheatRoleName(conn, msg.strData);
			this.RecordCoronationCheatPlayer(coronationCheatRoleName);
			return;
		}
		case ServerNetOperation.LobbyChat:
			if (!string.IsNullOrWhiteSpace(msg.strData))
			{
				try
				{
					int lobbyChatColorIndex = this.GetLobbyChatColorIndex(conn);
					this.ServerSendAllPlayer(new ClientNetMessage
					{
						clientNetOperation = ClientNetOperation.LobbyChat,
						datas = new int[]
						{
							lobbyChatColorIndex
						},
						strs = new string[]
						{
							this.GetLobbyChatPlayerName(conn, lobbyChatColorIndex),
							msg.strData
						}
					});
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x060016DD RID: 5853 RVA: 0x0008EB8C File Offset: 0x0008CD8C
	private int GetLobbyChatColorIndex(NetworkConnection conn)
	{
		Dictionary<int, NetworkConnectionToClient>.Enumerator enumerator = NetworkServer.connections.GetEnumerator();
		int num = 0;
		while (enumerator.MoveNext())
		{
			KeyValuePair<int, NetworkConnectionToClient> keyValuePair = enumerator.Current;
			if (keyValuePair.Value.connectionId == conn.connectionId)
			{
				enumerator.Dispose();
				return num;
			}
			num++;
		}
		enumerator.Dispose();
		return 0;
	}

	// Token: 0x060016DE RID: 5854 RVA: 0x0008EBE4 File Offset: 0x0008CDE4
	private string GetLobbyChatPlayerName(NetworkConnection conn, int lobbyIndex)
	{
		SteamManager steamManager = EntityStatic.Get<SteamManager>();
		if (steamManager != null && steamManager.Initialized && GameHelperClient.LobbyID != CSteamID.Nil)
		{
			int numLobbyMembers = SteamMatchmaking.GetNumLobbyMembers(GameHelperClient.LobbyID);
			if (lobbyIndex >= 0 && lobbyIndex < numLobbyMembers)
			{
				string friendPersonaName = SteamFriends.GetFriendPersonaName(SteamMatchmaking.GetLobbyMemberByIndex(GameHelperClient.LobbyID, lobbyIndex));
				if (!string.IsNullOrWhiteSpace(friendPersonaName))
				{
					return friendPersonaName;
				}
			}
		}
		return string.Format("Player {0}", conn.connectionId);
	}

	// Token: 0x060016DF RID: 5855 RVA: 0x0008EC59 File Offset: 0x0008CE59
	public bool HasCoronationCheatPlayer()
	{
		return this.coronationCheatPlayerNames.Count > 0;
	}

	// Token: 0x060016E0 RID: 5856 RVA: 0x0008EC69 File Offset: 0x0008CE69
	public void RecordCoronationCheatPlayer(string roleName)
	{
		if (string.IsNullOrWhiteSpace(roleName))
		{
			roleName = "Player";
		}
		if (!this.coronationCheatPlayerNames.Add(roleName))
		{
			return;
		}
		GameHelperClient.CanCoronation = false;
	}

	// Token: 0x060016E1 RID: 5857 RVA: 0x0008EC90 File Offset: 0x0008CE90
	private string GetCoronationCheatRoleName(NetworkConnection conn, string reportedRoleName)
	{
		RoleBase roleBase = (conn != null && conn.identity != null) ? conn.identity.GetComponent<RoleBase>() : null;
		if (roleBase != null && !string.IsNullOrWhiteSpace(roleBase.roleName))
		{
			return roleBase.roleName;
		}
		return reportedRoleName;
	}

	// Token: 0x060016E2 RID: 5858 RVA: 0x0008ECDC File Offset: 0x0008CEDC
	private void BroadcastCoronationCheatTip(string roleName = null)
	{
		if (this.coronationCheatTipSent)
		{
			return;
		}
		this.coronationCheatTipSent = true;
		if (string.IsNullOrWhiteSpace(roleName))
		{
			roleName = ((this.coronationCheatPlayerNames.Count > 0) ? this.coronationCheatPlayerNames.First<string>() : "Player");
		}
		PlayerBase localPlayer = GameHelperClient.localPlayer;
		if (localPlayer == null)
		{
			return;
		}
		localPlayer.ServerChat(PathDefine.Concat(roleName, string.Format(ColorDefine.RedForColor, Game.Language.Get("数据异常提示", ""))));
	}

	// Token: 0x060016E3 RID: 5859 RVA: 0x0008ED56 File Offset: 0x0008CF56
	private void OnUpdateLoadInputField(string msg)
	{
		this.uploadTeamBuildData.teamMessage = msg;
	}

	// Token: 0x060016E4 RID: 5860 RVA: 0x0008ED64 File Offset: 0x0008CF64
	private void OnUpdateLoadKingData()
	{
		Game.TimerManager.AddTimer(3.5f, delegate()
		{
			Game.UI.OpenUI<UI_Settlement>("win");
		});
		if (this.uploadTeamBuildData == null || this.uploadTeamBuildData.members.Count == 0)
		{
			Debug.LogError("上传数据验证错误！");
			return;
		}
		this.steamLeaderboardManager.UploadResult(this.uploadTeamBuildData.rank, GameHelperClient.ChallengePlayerNum, this.uploadTeamBuildData);
	}

	// Token: 0x060016E5 RID: 5861 RVA: 0x0008EDE6 File Offset: 0x0008CFE6
	private void OnCancelLoadKingData()
	{
		Game.UI.OpenUI<UI_Settlement>("win");
	}

	// Token: 0x060016E6 RID: 5862 RVA: 0x0008EDF8 File Offset: 0x0008CFF8
	private string[] GetSendSelectHeroData()
	{
		string[] array = new string[this.playerHeroes.Count];
		for (int i = 0; i < this.playerHeroes.Keys.Count; i++)
		{
			array[i] = this.playerHeroes.Keys.ElementAt(i).ToString() + "|" + ((int)this.playerHeroes.Values.ElementAt(i)).ToString();
		}
		return array;
	}

	// Token: 0x060016E7 RID: 5863 RVA: 0x0008EE71 File Offset: 0x0008D071
	private void ServerSendToClient(NetworkConnection conn, ClientNetMessage clientNetMessage)
	{
		conn.Send<ClientNetMessage>(clientNetMessage, 0);
	}

	// Token: 0x060016E8 RID: 5864 RVA: 0x0008EE7C File Offset: 0x0008D07C
	public void ServerSendAllPlayer(ClientNetMessage clientNetMessage)
	{
		Dictionary<int, NetworkConnectionToClient>.Enumerator enumerator = NetworkServer.connections.GetEnumerator();
		while (enumerator.MoveNext())
		{
			KeyValuePair<int, NetworkConnectionToClient> keyValuePair = enumerator.Current;
			this.ServerSendToClient(keyValuePair.Value, clientNetMessage);
		}
		enumerator.Dispose();
	}

	// Token: 0x060016E9 RID: 5865 RVA: 0x0008EEBC File Offset: 0x0008D0BC
	public void ServerSendAllPlayerNoConnectPlayer(ClientNetMessage clientNetMessage, NetworkConnection conn)
	{
		Dictionary<int, NetworkConnectionToClient>.Enumerator enumerator = NetworkServer.connections.GetEnumerator();
		while (enumerator.MoveNext())
		{
			KeyValuePair<int, NetworkConnectionToClient> keyValuePair = enumerator.Current;
			if (keyValuePair.Value.connectionId != conn.connectionId)
			{
				keyValuePair = enumerator.Current;
				this.ServerSendToClient(keyValuePair.Value, clientNetMessage);
			}
		}
		enumerator.Dispose();
	}

	// Token: 0x060016EA RID: 5866 RVA: 0x0008EF18 File Offset: 0x0008D118
	public void OnStartGame()
	{
		if (!GameHelperClient.isReady)
		{
			return;
		}
		for (int i = 0; i < NetworkServer.connections.Count; i++)
		{
			int key = this.playerReady.ElementAt(i).Key;
			this.playerReady[key] = false;
		}
		int num = 0;
		if (!string.IsNullOrEmpty(this.startGameStrData) && this.startGameStrData.Contains("monster_10"))
		{
			num = 3;
		}
		this.enemyManagerServer.OnGameStart();
		if (this.severStageManager != null)
		{
			this.severStageManager.OnStart();
		}
		this.ServerSendAllPlayer(new ClientNetMessage
		{
			clientNetOperation = ClientNetOperation.OnStartGame,
			data = GameHelperClient.WaveNum,
			datas = new int[]
			{
				num
			}
		});
	}

	// Token: 0x060016EB RID: 5867 RVA: 0x0008EFDC File Offset: 0x0008D1DC
	private void OnServerSelectHeroComp()
	{
		this.ServerSendAllPlayer(new ClientNetMessage
		{
			clientNetOperation = ClientNetOperation.OnStartReady,
			data = NetworkServer.connections.Count
		});
		if (GameHelperClient.spawnConfig.levelStageType == LevelStageType.Stage)
		{
			this.severStageManager = new SeverStageManager();
			this.severStageManager.Init();
		}
	}

	// Token: 0x060016EC RID: 5868 RVA: 0x0008F034 File Offset: 0x0008D234
	private void MyServerAddPlayer(NetworkConnection conn, HeroType heroType)
	{
		string str = "Temp_Prefabs/Player_";
		int num = (int)heroType;
		string text = str + num.ToString();
		GameObject gameObject = null;
		Queue<GameObject> queue;
		if (AssetManagerMirror.assetsQueueDic.TryGetValue(text, out queue) && queue.Count > 0)
		{
			gameObject = AssetManagerMirror.LoadPrefab(text, null, true);
		}
		if (gameObject == null)
		{
			gameObject = AssetManagerMirror.LoadPrefab("Prefabs/PlayerBase", null, true);
		}
		NetworkServer.AddPlayerForConnection((NetworkConnectionToClient)conn, gameObject);
		PlayerBase component = gameObject.GetComponent<PlayerBase>();
		PlayerBase playerBase = component;
		Vector3 pos = GameHelperClient.spawnConfig.playerSpawnPoint[(int)(component.netId - 1U)];
		Dictionary<string, RoleAttribute> heroAttributeDic = Game.GameData.HeroAttributeDic;
		num = (int)heroType;
		playerBase.InitServer(pos, (long)heroAttributeDic[num.ToString()].hp, component.netId, heroType, this.GetPlayerSteamName(conn));
	}

	// Token: 0x060016ED RID: 5869 RVA: 0x0008F0F0 File Offset: 0x0008D2F0
	private string GetPlayerSteamName(NetworkConnection conn)
	{
		SteamManager steamManager = EntityStatic.Get<SteamManager>();
		if (steamManager == null || !steamManager.Initialized || GameHelperClient.LobbyID == CSteamID.Nil)
		{
			return string.Empty;
		}
		int numLobbyMembers = SteamMatchmaking.GetNumLobbyMembers(GameHelperClient.LobbyID);
		for (int i = 0; i < numLobbyMembers; i++)
		{
			CSteamID lobbyMemberByIndex = SteamMatchmaking.GetLobbyMemberByIndex(GameHelperClient.LobbyID, i);
			if (lobbyMemberByIndex.ToString().Equals(conn.address))
			{
				return SteamFriends.GetFriendPersonaName(lobbyMemberByIndex);
			}
		}
		int num = 0;
		Dictionary<int, NetworkConnectionToClient>.Enumerator enumerator = NetworkServer.connections.GetEnumerator();
		while (enumerator.MoveNext())
		{
			KeyValuePair<int, NetworkConnectionToClient> keyValuePair = enumerator.Current;
			if (keyValuePair.Value.connectionId == conn.connectionId)
			{
				enumerator.Dispose();
				if (num >= 0 && num < numLobbyMembers)
				{
					return SteamFriends.GetFriendPersonaName(SteamMatchmaking.GetLobbyMemberByIndex(GameHelperClient.LobbyID, num));
				}
				return string.Empty;
			}
			else
			{
				num++;
			}
		}
		enumerator.Dispose();
		return string.Empty;
	}

	// Token: 0x060016EE RID: 5870 RVA: 0x0008F1DC File Offset: 0x0008D3DC
	public override void OnServerDisconnect(NetworkConnection conn)
	{
		if (conn == null)
		{
			return;
		}
		if (GameHelperClient.LobbyID != CSteamID.Nil)
		{
			List<LobbyManager.LobbyPlayerInfo> list = new List<LobbyManager.LobbyPlayerInfo>();
			int numLobbyMembers = SteamMatchmaking.GetNumLobbyMembers(GameHelperClient.LobbyID);
			Debug.Log(string.Format("当前房间成员数量: {0}", numLobbyMembers));
			for (int i = 0; i < numLobbyMembers; i++)
			{
				CSteamID lobbyMemberByIndex = SteamMatchmaking.GetLobbyMemberByIndex(GameHelperClient.LobbyID, i);
				if (!lobbyMemberByIndex.ToString().Equals(conn.address))
				{
					string friendPersonaName = SteamFriends.GetFriendPersonaName(lobbyMemberByIndex);
					list.Add(new LobbyManager.LobbyPlayerInfo
					{
						playerAddress = lobbyMemberByIndex.ToString(),
						playerName = friendPersonaName,
						playerHead = lobbyMemberByIndex.ToString()
					});
				}
			}
			MySystemEvent.Instance.DispatchMessage<List<LobbyManager.LobbyPlayerInfo>>(28, list);
		}
		else
		{
			MySystemEvent.Instance.DispatchMessage<string>(26, conn.address);
		}
		if (conn.identity == null)
		{
			return;
		}
		uint netId = conn.identity.netId;
		this.ServerSendAllPlayer(new ClientNetMessage
		{
			clientNetOperation = ClientNetOperation.OnPlayerDisconnect,
			data = (int)netId
		});
		this.playerReady.Remove(conn.connectionId);
		base.OnServerDisconnect(conn);
	}

	// Token: 0x060016EF RID: 5871 RVA: 0x0008F31C File Offset: 0x0008D51C
	private void ClearDisconnectPlayer(uint disconnectId)
	{
		List<RoleBase> clientPlayerList = Game.PlayerManagerClient.clientPlayerList;
		for (int i = clientPlayerList.Count - 1; i > -1; i--)
		{
			RoleBase roleBase = clientPlayerList[i];
			if (roleBase.authorityId == disconnectId)
			{
				if (GameHelperClient.isHost)
				{
					NetworkServer.UnSpawn(roleBase.gameObject);
				}
				Game.PlayerManagerClient.RemovePlayer(roleBase);
				AssetManagerMirror.UnLoadPrefab(roleBase.gameObject);
			}
		}
		List<RoleBase> clientEnemyList = Game.EnemyManagerClient.clientEnemyList;
		for (int j = clientEnemyList.Count - 1; j > -1; j--)
		{
			RoleBase roleBase2 = clientEnemyList[j];
			if (roleBase2.authorityId == disconnectId)
			{
				if (GameHelperClient.isHost)
				{
					NetworkServer.UnSpawn(roleBase2.gameObject);
				}
				Game.EnemyManagerClient.RemoveEnemy(roleBase2);
				AssetManagerMirror.UnLoadPrefab(roleBase2.gameObject);
			}
		}
	}

	// Token: 0x060016F0 RID: 5872 RVA: 0x0008F3E4 File Offset: 0x0008D5E4
	public void GameOver(bool isWin)
	{
		if (GameHelperClient.isKingBattle)
		{
			return;
		}
		this.startGameStrData = "";
		this.enemyManagerServer.OnGameOver();
		ClientNetMessage clientNetMessage;
		if (isWin && GameHelperClient.WaveNum < GameHelperClient.spawnConfig.enemySpawnData.Length - 1)
		{
			GameHelperClient.WaveNum++;
			bool flag = this.enemyManagerServer.CheckLevelUp();
			clientNetMessage = new ClientNetMessage
			{
				clientNetOperation = ClientNetOperation.OnStartRest,
				datas = new int[]
				{
					GameHelperClient.WaveNum,
					flag ? 1 : 0
				}
			};
			this.ServerSendAllPlayer(clientNetMessage);
			return;
		}
		GameHelperClient.isGameOver = true;
		if (isWin)
		{
			this.playerReady.Clear();
		}
		clientNetMessage = new ClientNetMessage
		{
			clientNetOperation = ClientNetOperation.OnGameOver,
			data = (isWin ? 0 : 1)
		};
		this.ServerSendAllPlayer(clientNetMessage);
	}

	// Token: 0x060016F1 RID: 5873 RVA: 0x0008F4B0 File Offset: 0x0008D6B0
	public void TestCloseEnemyCreate()
	{
		this.enemyManagerServer.TestCloseEnemyCreate();
	}

	// Token: 0x060016F2 RID: 5874 RVA: 0x0008F4C0 File Offset: 0x0008D6C0
	public void OnDiscoveredServer(ServerResponse info)
	{
		this.discoveredServers[info.serverId] = info;
		UI_StartGame ui = Game.UI.GetUI<UI_StartGame>();
		if (ui != null)
		{
			ui.UpdateServerList();
		}
	}

	// Token: 0x060016F3 RID: 5875 RVA: 0x0008F4F3 File Offset: 0x0008D6F3
	public void Connect(ServerResponse info)
	{
		this.networkDiscovery.StopDiscovery();
		NetworkManager.singleton.StartClient(info.uri);
	}

	// Token: 0x060016F4 RID: 5876 RVA: 0x0008F510 File Offset: 0x0008D710
	private void MyServerAddPlayerKing(NetworkConnection conn, SaveLoadManager.PlayerKingData playerKingData, uint playerId, Vector3 position, uint trackPlayerId = 0U)
	{
		string str = "Temp_Prefabs/Player_";
		int heroType = (int)playerKingData.heroType;
		string text = str + heroType.ToString();
		GameObject gameObject = null;
		Queue<GameObject> queue;
		if (AssetManagerMirror.assetsQueueDic.TryGetValue(text, out queue) && queue.Count > 0)
		{
			gameObject = AssetManagerMirror.LoadPrefab(text, null, true);
		}
		if (gameObject == null)
		{
			gameObject = AssetManagerMirror.LoadPrefab("Prefabs/PlayerBase", null, true);
		}
		NetworkServer.Spawn(gameObject, text, null);
		gameObject.GetComponent<RoleBase>().netIdentity.AssignClientAuthority(NetworkServer.spawned[playerId].connectionToClient);
		gameObject.GetComponent<PlayerBase>().InitKingServer(position, playerId, (trackPlayerId == 0U) ? playerId : trackPlayerId, playerKingData.heroType, playerKingData);
	}

	// Token: 0x0400154E RID: 5454
	private List<GameObject> heroGameObjects;

	// Token: 0x0400154F RID: 5455
	private bool isSelectHero;

	// Token: 0x04001550 RID: 5456
	private int myConnectionId;

	// Token: 0x04001551 RID: 5457
	private bool hasPendingRestRemainsRoguelike;

	// Token: 0x04001552 RID: 5458
	private float pendingRestRemainsDelay;

	// Token: 0x04001553 RID: 5459
	private SteamLeaderboardManager steamLeaderboardManager;

	// Token: 0x04001554 RID: 5460
	private SteamLeaderboardBatchLoader steamLeaderboardBatchLoader;

	// Token: 0x04001555 RID: 5461
	private EnemyManagerServer enemyManagerServer;

	// Token: 0x04001556 RID: 5462
	private SeverStageManager severStageManager;

	// Token: 0x04001557 RID: 5463
	private Dictionary<int, HeroType> playerHeroes = new Dictionary<int, HeroType>();

	// Token: 0x04001558 RID: 5464
	private int[] selectHeros = new int[]
	{
		1,
		2,
		3,
		4,
		5,
		6,
		7,
		8,
		9,
		10,
		11,
		12
	};

	// Token: 0x04001559 RID: 5465
	private Dictionary<int, bool> playerReady = new Dictionary<int, bool>();

	// Token: 0x0400155A RID: 5466
	public KcpTransport kcpNet;

	// Token: 0x0400155B RID: 5467
	public FizzySteamworks steamNet;

	// Token: 0x0400155C RID: 5468
	private string startGameStrData = "";

	// Token: 0x0400155D RID: 5469
	private readonly Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();

	// Token: 0x0400155E RID: 5470
	private readonly Vector2[] createKingPosOffset = new Vector2[]
	{
		new Vector2(0f, 0f),
		new Vector2(1.4f, 1.4f),
		new Vector2(1.4f, 1.4f),
		new Vector2(2f, 2f)
	};

	// Token: 0x0400155F RID: 5471
	private readonly Vector2 createKingPos = new Vector2(5f, 6.5f);

	// Token: 0x04001560 RID: 5472
	private bool getKingListOver;

	// Token: 0x04001561 RID: 5473
	private float getKingListTime;

	// Token: 0x04001562 RID: 5474
	private NetworkDiscovery networkDiscovery;

	// Token: 0x04001563 RID: 5475
	private SaveLoadManager.TeamBuildData uploadTeamBuildData;

	// Token: 0x04001564 RID: 5476
	private SaveLoadManager.TeamBuildData challengeTargetTeamBuildData;

	// Token: 0x04001565 RID: 5477
	private readonly HashSet<string> coronationCheatPlayerNames = new HashSet<string>();

	// Token: 0x04001566 RID: 5478
	private bool coronationCheatTipSent;
}

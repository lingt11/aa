using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000305 RID: 773
public class UI_Battle : UGUICtrl
{
	// Token: 0x060011CF RID: 4559 RVA: 0x00068B18 File Offset: 0x00066D18
	public UI_Battle()
	{
		this.selfView = new UI_Battle_View();
		this.uiBattleJoy = new UI_Battle_Joy(this);
		base.OnCreate(this.selfView, "UI/Prefabs/UI_Battle", base.GetType());
		this.InitHpBar();
		Transform transform = this.selfView.transform.Find("FX_Damage_Vignette");
		RectTransform component = transform.GetChild(0).GetComponent<RectTransform>();
		component.sizeDelta = this.GetScreenSize() / component.localScale;
		this.damageVignetteAnimator = transform.GetComponent<Animator>();
		this.damageVignetteAnimator.gameObject.SetActive(true);
		this.selfView.btn_startGame.AddButtonEvent(new UnityAction(this.OnBtnStartGameClick));
		if (GameHelperClient.PlayerNum > 1)
		{
			this.teamHeadList = new List<UI_Battle_TeamHead>();
			this.selfView.trans_teamUI.gameObject.SetActive(true);
			UI_Battle_TeamHead component2 = this.selfView.trans_TeamHead.gameObject.GetComponent<UI_Battle_TeamHead>();
			this.teamHeadList.Add(component2);
			for (int i = 1; i < GameHelperClient.PlayerNum; i++)
			{
				UI_Battle_TeamHead item = Object.Instantiate<UI_Battle_TeamHead>(component2, this.selfView.trans_TeamHead.parent.transform);
				this.teamHeadList.Add(item);
			}
		}
		else
		{
			this.selfView.trans_teamUI.gameObject.SetActive(false);
		}
		this.rectPickUI = this.selfView.trans_pickUI.GetComponent<RectTransform>();
		this.pickUIStartPos = this.rectPickUI.anchoredPosition;
	}

	// Token: 0x060011D0 RID: 4560 RVA: 0x00068CF0 File Offset: 0x00066EF0
	private void InitHpBar()
	{
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui == null)
		{
			return;
		}
		this.isInitHpBar = true;
		this.hpPrefab = ui.selfView.trans_hpParent.GetChild(0).GetComponent<MyHpBar>();
		this.bossHpPrefab = ui.selfView.trans_hpParent.GetChild(1).GetComponent<MyHpBar>();
		this.playerHpPrefab = ui.selfView.trans_hpParent.GetChild(2).GetComponent<MyHpBar>();
		this.lockRect = ui.selfView.trans_lock.GetComponent<RectTransform>();
		this.hpParent = ui.selfView.trans_hpParent;
	}

	// Token: 0x060011D1 RID: 4561 RVA: 0x00068D90 File Offset: 0x00066F90
	public void OnBtnStartGameClick()
	{
		if (this.needBuyMonster)
		{
			Game.UI.GetUI<UI_Shop>().OnBuyMonsterBtnClick(GameHelperClient.BuyMonsterIndex);
			GameHelperClient.BuyMonsterIndex++;
			this.needBuyMonster = false;
			return;
		}
		if (string.IsNullOrEmpty(this.readySyncData))
		{
			NetworkClient.connection.Send<ServerNetMessage>(new ServerNetMessage
			{
				serverNetOperation = ServerNetOperation.Ready
			}, 0);
			return;
		}
		NetworkClient.connection.Send<ServerNetMessage>(new ServerNetMessage
		{
			serverNetOperation = ServerNetOperation.Ready,
			strData = this.readySyncData
		}, 0);
	}

	// Token: 0x060011D2 RID: 4562 RVA: 0x00068E20 File Offset: 0x00067020
	public override void Update()
	{
		this.UpdateEvent();
		if (!this.isInitPickItemUI)
		{
			UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
			if (ui != null)
			{
				this.selfView.trans_pickItemParent.SetParent(ui.selfView.gameObject.transform);
				this.selfView.trans_pickItemParent.SetAsFirstSibling();
				this.isInitPickItemUI = true;
			}
		}
	}

	// Token: 0x060011D3 RID: 4563 RVA: 0x00068E80 File Offset: 0x00067080
	protected override void OpenPanel(object data)
	{
		this.uiBattleJoy.Open();
		MySystemEvent.Instance.RegisterMessage(33, new Action<Body>(this.OnPickItem));
		MySystemEvent.Instance.RegisterMessage(34, new Action<Body>(this.OnPickAllItem));
	}

	// Token: 0x060011D4 RID: 4564 RVA: 0x00068EC0 File Offset: 0x000670C0
	protected override void ClosePanel()
	{
		base.ClosePanel();
		this.uiBattleJoy.Close();
		MySystemEvent.Instance.UnregisterMessage(33, new Action<Body>(this.OnPickItem));
		MySystemEvent.Instance.UnregisterMessage(34, new Action<Body>(this.OnPickAllItem));
	}

	// Token: 0x060011D5 RID: 4565 RVA: 0x00068F10 File Offset: 0x00067110
	private void OnPickItem(Body body)
	{
		if (Game.GamePlayItemManager.isHasGamePlayItem)
		{
			Game.GamePlayItemManager.StartAction();
			return;
		}
		ItemStruct curPickItemStruct = Game.ItemManager.CurPickItemStruct;
		if (curPickItemStruct != null)
		{
			UI_Battle.Pick(curPickItemStruct);
		}
	}

	// Token: 0x060011D6 RID: 4566 RVA: 0x00068F48 File Offset: 0x00067148
	private void OnPickAllItem(Body body)
	{
		if (Game.ItemManager.CurPickItemStruct != null)
		{
			this.OnKeyPick();
		}
	}

	// Token: 0x060011D7 RID: 4567 RVA: 0x00068F5C File Offset: 0x0006715C
	public void UpdatePlayerNum()
	{
		this.maxEnemyNum = GameHelperClient.spawnConfig.enemySpawnData[GameHelperClient.WaveNum].newMaxEnemyNum[GameHelperClient.PlayerNum - 1] + GameHelperClient.AddEnemyLimit * GameHelperClient.PlayerNum;
	}

	// Token: 0x060011D8 RID: 4568 RVA: 0x00068F90 File Offset: 0x00067190
	public void OnStartRest(bool isChallenge, bool isBoss, bool isRemainsRoguelike)
	{
		this.readySyncData = "";
		RectTransform component = this.selfView.trans_wavebg.GetComponent<RectTransform>();
		component.anchoredPosition = new Vector2(0f, 135f);
		component.DOAnchorPosY(-135f, 0.5f, false);
		this.needBuyMonster = isChallenge;
		this.selfView.trans_YaliBar.gameObject.SetActive(false);
		this.selfView.btn_startGame.gameObject.SetActive(!isRemainsRoguelike);
		this.selfView.trans_wavebg.gameObject.SetActive(true);
		this.selfView.text_time.gameObject.SetActive(false);
		if (isBoss)
		{
			this.selfView.trans_bossImg.gameObject.SetActive(true);
			this.selfView.trans_eliteImg.gameObject.SetActive(false);
		}
		else if (isChallenge)
		{
			this.selfView.trans_bossImg.gameObject.SetActive(false);
			this.selfView.trans_eliteImg.gameObject.SetActive(true);
		}
		else
		{
			this.selfView.trans_bossImg.gameObject.SetActive(false);
			this.selfView.trans_eliteImg.gameObject.SetActive(false);
		}
		this.ShowWaveTip();
		this.HideReadyTip();
	}

	// Token: 0x060011D9 RID: 4569 RVA: 0x000690DA File Offset: 0x000672DA
	public void OnInitReady()
	{
		this.ShowWaveTip();
	}

	// Token: 0x060011DA RID: 4570 RVA: 0x000690E4 File Offset: 0x000672E4
	private void ShowWaveTip()
	{
		this.UpdateReadyState(false);
		this.selfView.ltext_waveTip.text = string.Format(Game.Language.Get("波次提示", ""), PathDefine.Concat(GameHelperClient.WaveNum + 1, StringDefine.Slash, GameHelperClient.spawnConfig.enemySpawnData.Length));
	}

	// Token: 0x060011DB RID: 4571 RVA: 0x00069148 File Offset: 0x00067348
	public void UpdateReadyState(bool isReady)
	{
		if (isReady)
		{
			this.selfView.ltext_startgame.text = Game.Language.Get("等待其他玩家准备", "");
			return;
		}
		this.selfView.ltext_startgame.text = Game.Language.Get("开始冒险", "");
	}

	// Token: 0x060011DC RID: 4572 RVA: 0x000691A4 File Offset: 0x000673A4
	public void UpdateReadyTip(List<HeroType> heroTypes)
	{
		int childCount = this.selfView.trans_readyTip.childCount;
		int count = heroTypes.Count;
		for (int i = 0; i < childCount; i++)
		{
			Transform child = this.selfView.trans_readyTip.GetChild(i);
			if (i < count)
			{
				child.gameObject.SetActive(true);
				child.GetChild(0).GetComponent<Image>().sprite = Util.GetHeroIcon(heroTypes[i]);
			}
			else
			{
				child.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x060011DD RID: 4573 RVA: 0x00069224 File Offset: 0x00067424
	public void HideReadyTip()
	{
		int childCount = this.selfView.trans_readyTip.childCount;
		for (int i = 0; i < childCount; i++)
		{
			this.selfView.trans_readyTip.GetChild(i).gameObject.SetActive(false);
		}
	}

	// Token: 0x060011DE RID: 4574 RVA: 0x0006926C File Offset: 0x0006746C
	public void OnStartGame()
	{
		this.selfView.trans_YaliBar.gameObject.SetActive(true);
		this.selfView.btn_startGame.gameObject.SetActive(false);
		this.selfView.trans_wavebg.gameObject.SetActive(false);
		this.selfView.text_time.gameObject.SetActive(true);
		this.isShowKingBattleFinalTip = false;
	}

	// Token: 0x060011DF RID: 4575 RVA: 0x000692D8 File Offset: 0x000674D8
	public void OnOpenRoguelike()
	{
		if (GameHelperClient.isReady)
		{
			this.selfView.btn_startGame.gameObject.SetActive(false);
		}
	}

	// Token: 0x060011E0 RID: 4576 RVA: 0x000692F7 File Offset: 0x000674F7
	public void OnCloseRoguelike()
	{
		if (GameHelperClient.isReady)
		{
			this.selfView.btn_startGame.gameObject.SetActive(true);
		}
	}

	// Token: 0x060011E1 RID: 4577 RVA: 0x00069318 File Offset: 0x00067518
	public void UpdateEvent()
	{
		this.UpdateHpBar();
		this.UpdateDamageUI(Time.deltaTime);
		this.UpdatePickItem();
		this.UpdateEnemyTip();
		this.UpdateGameTime();
		this.UpdateDamageVignette();
		this.UpdateTeamHead();
		this.UpdateDeadTime();
		this.UpdateEnemyCreateTip();
		this.UpdateCountDown();
	}

	// Token: 0x060011E2 RID: 4578 RVA: 0x00069366 File Offset: 0x00067566
	public void StartKingBattle(float value)
	{
		this.countDownTime = value;
		this.selfView.trans_countDown.gameObject.SetActive(true);
	}

	// Token: 0x060011E3 RID: 4579 RVA: 0x00069388 File Offset: 0x00067588
	private void UpdateCountDown()
	{
		if (this.selfView.trans_countDown.gameObject.activeSelf)
		{
			this.selfView.text_countDownTime.text = Mathf.Ceil(this.countDownTime).ToString();
			this.countDownTime -= Time.deltaTime;
			if (this.countDownTime <= 0f)
			{
				this.selfView.trans_countDown.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x060011E4 RID: 4580 RVA: 0x00069404 File Offset: 0x00067604
	private void UpdateDeadTime()
	{
		if (this.selfView.trans_deadGo.gameObject.activeSelf)
		{
			this.selfView.text_deadTime.text = Mathf.Ceil(Mathf.Max(5f, GameHelperClient.gameConfig.PlayerRelifeTime + (float)GameHelperClient.localPlayer.addRelifeTime) - GameHelperClient.localPlayer.timer).ToString();
		}
	}

	// Token: 0x060011E5 RID: 4581 RVA: 0x00069470 File Offset: 0x00067670
	private void UpdateEnemyCreateTip()
	{
		if (this.showEnemyEnterTime < 3f)
		{
			this.showEnemyEnterTime += Time.deltaTime;
			if (this.showEnemyEnterTime >= 3f && this.selfView.trans_EnemyEnterTip.gameObject.activeSelf)
			{
				this.selfView.trans_EnemyEnterTip.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x060011E6 RID: 4582 RVA: 0x000694D8 File Offset: 0x000676D8
	private void UpdateTeamHead()
	{
		if (this.teamHeadList == null || this.teamHeadList.Count == 0)
		{
			return;
		}
		int count = this.teamHeadList.Count;
		int count2 = Game.PlayerManagerClient.clientPlayerList.Count;
		int num = 0;
		for (int i = 0; i < count2; i++)
		{
			RoleBase roleBase = Game.PlayerManagerClient.clientPlayerList[i];
			if (!(roleBase == null) && roleBase.roleType == RoleType.Player)
			{
				UI_Battle_TeamHead ui_Battle_TeamHead = this.teamHeadList[num];
				num++;
				ui_Battle_TeamHead.UpdatePlayerData(roleBase as PlayerBase);
				if (num >= count)
				{
					break;
				}
			}
		}
	}

	// Token: 0x060011E7 RID: 4583 RVA: 0x0006956C File Offset: 0x0006776C
	private void UpdateEnemyTip()
	{
		if (GameHelperClient.isGameOver || GameHelperClient.isReady)
		{
			return;
		}
		if (GameHelperClient.isHost && !GameHelperClient.isKingBattle)
		{
			if (this.curEnemyNum > this.maxEnemyNum)
			{
				if (this.checkGameOverTime < 0f)
				{
					this.checkGameOverTime = Time.time + 3f;
				}
				else if (Time.time > this.checkGameOverTime)
				{
					(NetworkManager.singleton as MyServerNetworkManager).GameOver(false);
				}
			}
			else
			{
				this.checkGameOverTime = -1f;
			}
		}
		this.selfView.ltext_enemyTipText.text = PathDefine.Concat(PathDefine.Concat(Game.Language.Get("敌人上限", ""), StringDefine.ColonSpace, this.curEnemyNum), StringDefine.Slash, this.maxEnemyNum);
		float num = (float)this.curEnemyNum / ((float)this.maxEnemyNum * 1f);
		GameHelperClient.YaLiValue = num;
		this.selfView.img_yali.fillAmount = num;
		this.selfView.text_yalizhi_bg.text = (this.selfView.text_yalizhi.text = PathDefine.Concat((int)(num * 100f), StringDefine.Percent));
		this.breathTime += Time.deltaTime * Mathf.Lerp(1f, 5f, num);
		float t = (this.breathTime > 1f) ? (2f - this.breathTime) : this.breathTime;
		if (this.breathTime > 2f)
		{
			this.breathTime = 0f;
		}
		Color color = this.selfView.img_bg.color;
		color.a = Mathf.Lerp(0.1f, 0.3f, t);
		this.selfView.img_bg.color = color;
		float num2 = Mathf.Lerp(1f, 1.25f, t);
		this.selfView.img_heart.rectTransform.localScale = new Vector3(num2, num2, num2);
		this.selfView.img_heart_bg.rectTransform.localScale = new Vector3(num2, num2, num2);
	}

	// Token: 0x060011E8 RID: 4584 RVA: 0x00069788 File Offset: 0x00067988
	private void UpdateGameTime()
	{
		if (GameHelperClient.isGameOver || GameHelperClient.isReady)
		{
			return;
		}
		if (GameHelperClient.CountDownTime <= 0f)
		{
			return;
		}
		float lastCountDownTime = GameHelperClient.CountDownTime;
		this.selfView.text_time.text = PathDefine.Concat(Mathf.Floor(GameHelperClient.CountDownTime / 60f), StringDefine.Colon, Mathf.Floor(GameHelperClient.CountDownTime % 60f));
		GameHelperClient.CountDownTime -= Time.deltaTime;
		this.CheckKingBattleFinalTip(lastCountDownTime);
		if (GameHelperClient.CountDownTime < 0f)
		{
			GameHelperClient.CountDownTime = 0f;
			if (GameHelperClient.isHost)
			{
				if (GameHelperClient.isKingBattle)
				{
					NetworkConnection connection = NetworkClient.connection;
					ServerNetMessage message = new ServerNetMessage
					{
						serverNetOperation = ServerNetOperation.KingBattleResult,
						datas = new int[]
						{
							this.CheckAllKingDead() ? 1 : 0
						}
					};
					connection.Send<ServerNetMessage>(message, 0);
				}
				else
				{
					(NetworkManager.singleton as MyServerNetworkManager).GameOver(true);
				}
			}
		}
		if (GameHelperClient.isHost && GameHelperClient.isKingBattle)
		{
			if (this.CheckAllKingDead())
			{
				if (this.checkGameWinTime < 0f)
				{
					this.checkGameWinTime = Time.time + 2f;
				}
				else if (Time.time > this.checkGameWinTime)
				{
					NetworkConnection connection2 = NetworkClient.connection;
					ServerNetMessage message = new ServerNetMessage
					{
						serverNetOperation = ServerNetOperation.KingBattleResult,
						datas = new int[]
						{
							1
						}
					};
					connection2.Send<ServerNetMessage>(message, 0);
				}
			}
			else
			{
				this.checkGameWinTime = -1f;
			}
			if (this.CheckAllPlayerDead())
			{
				if (this.checkGameOverTime < 0f)
				{
					this.checkGameOverTime = Time.time + 2f;
					return;
				}
				if (Time.time > this.checkGameOverTime)
				{
					NetworkConnection connection3 = NetworkClient.connection;
					ServerNetMessage message = new ServerNetMessage
					{
						serverNetOperation = ServerNetOperation.KingBattleResult,
						datas = new int[1]
					};
					connection3.Send<ServerNetMessage>(message, 0);
					return;
				}
			}
			else
			{
				this.checkGameOverTime = -1f;
			}
		}
	}

	// Token: 0x060011E9 RID: 4585 RVA: 0x0006996C File Offset: 0x00067B6C
	private void CheckKingBattleFinalTip(float lastCountDownTime)
	{
		if (!GameHelperClient.isKingBattle || this.isShowKingBattleFinalTip)
		{
			return;
		}
		float kingBattleFinalTipTime = GameHelperClient.GetKingBattleFinalTipTime();
		if (kingBattleFinalTipTime <= 0f)
		{
			return;
		}
		if (lastCountDownTime >= kingBattleFinalTipTime && GameHelperClient.CountDownTime <= kingBattleFinalTipTime)
		{
			this.isShowKingBattleFinalTip = true;
			this.ShowKingBattleFinalTip();
		}
	}

	// Token: 0x060011EA RID: 4586 RVA: 0x000699B4 File Offset: 0x00067BB4
	private bool CheckAllKingDead()
	{
		foreach (RoleBase roleBase in Game.EnemyManagerClient.clientEnemyList)
		{
			if (roleBase != null && roleBase.roleType == RoleType.King && !roleBase.IsDead())
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060011EB RID: 4587 RVA: 0x00069A28 File Offset: 0x00067C28
	private bool CheckAllPlayerDead()
	{
		foreach (RoleBase roleBase in Game.PlayerManagerClient.clientPlayerList)
		{
			if (roleBase != null && roleBase.roleType == RoleType.Player && !roleBase.IsDead())
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060011EC RID: 4588 RVA: 0x00069A98 File Offset: 0x00067C98
	private void UpdatePickItem()
	{
		if (GameHelperClient.localPlayer.CanAction() && Game.GamePlayItemManager.isHasGamePlayItem)
		{
			if (!this.selfView.trans_pickUI.gameObject.activeSelf)
			{
				this.selfView.trans_pickUI.gameObject.SetActive(true);
			}
			GamePlayItemData currentGamePlayItemData = Game.GamePlayItemManager.currentGamePlayItemData;
			if (this.curGamePlayItemId != currentGamePlayItemData.id)
			{
				this.curGamePlayItemId = currentGamePlayItemData.id;
				GamePlayItemType gamePlayItemType = currentGamePlayItemData.gamePlayItemType;
				if (gamePlayItemType != GamePlayItemType.Help)
				{
					if (gamePlayItemType == GamePlayItemType.Talk)
					{
						this.selfView.ltext_pickItem.text = Game.Language.Get("对话", "");
					}
				}
				else
				{
					this.selfView.ltext_pickItem.text = Game.Language.Get("救援", "");
					this.rectPickUI.anchoredPosition = this.pickUIStartPos;
				}
			}
			if (currentGamePlayItemData.gamePlayItemType == GamePlayItemType.Talk)
			{
				this.rectPickUI.anchoredPosition = Util.GetScreenPosition(currentGamePlayItemData.targetRole.MyTransform.position) - new Vector2(50f, 40f);
			}
			else
			{
				this.rectPickUI.anchoredPosition = this.pickUIStartPos;
			}
			if (GameHelperClient.isReady)
			{
				if (this.selfView.trans_keyPick.gameObject.activeSelf)
				{
					this.selfView.trans_keyPick.gameObject.SetActive(false);
				}
			}
			else if (!this.selfView.trans_keyPick.gameObject.activeSelf)
			{
				this.selfView.trans_keyPick.gameObject.SetActive(true);
			}
		}
		else
		{
			if (this.curGamePlayItemId != -1)
			{
				this.curGamePlayItemId = -1;
				this.selfView.ltext_pickItem.text = Game.Language.Get("picktip", "");
				this.rectPickUI.anchoredPosition = this.pickUIStartPos;
			}
			if (!this.selfView.trans_keyPick.gameObject.activeSelf)
			{
				this.selfView.trans_keyPick.gameObject.SetActive(true);
			}
		}
		if (Game.ItemManager.CurPickItemStruct != null)
		{
			if (!this.selfView.trans_pickUI.gameObject.activeSelf)
			{
				this.selfView.trans_pickUI.gameObject.SetActive(true);
			}
		}
		else if (this.curGamePlayItemId == -1 && this.selfView.trans_pickUI.gameObject.activeSelf)
		{
			this.selfView.trans_pickUI.gameObject.SetActive(false);
		}
		int num = 0;
		int count = this.pickItemList.Count;
		Dictionary<ItemType, ItemData> itemDataDic = Game.GameData.ItemDataDic;
		Dictionary<uint, ItemStruct>.Enumerator enumerator = Game.ItemManager.itemStructs.GetEnumerator();
		Vector2 screenSize = this.GetScreenSize();
		while (enumerator.MoveNext())
		{
			KeyValuePair<uint, ItemStruct> keyValuePair = enumerator.Current;
			ItemStruct value = keyValuePair.Value;
			if (value.model != null && value.model.activeSelf)
			{
				Vector3 vector = Game.CameraManager.camera.WorldToViewportPoint(value.modelTransform.position);
				if (vector.z >= 0f)
				{
					UI_Battle.PickItemData pickItemData;
					if (num < count)
					{
						pickItemData = this.pickItemList[num];
						if (!pickItemData.go.activeSelf)
						{
							pickItemData.go.SetActive(true);
						}
					}
					else
					{
						GameObject gameObject = Object.Instantiate<GameObject>(this.selfView.trans_pickItemPrefab.gameObject, this.selfView.trans_pickItemParent);
						pickItemData = new UI_Battle.PickItemData();
						pickItemData.go = gameObject;
						pickItemData.go.SetActive(true);
						pickItemData.rectTransform = gameObject.GetComponent<RectTransform>();
						pickItemData.text = pickItemData.rectTransform.GetChild(0).gameObject.GetComponent<Text>();
						this.pickItemList.Add(pickItemData);
					}
					if (itemDataDic.ContainsKey(value.itemType))
					{
						ItemData itemData = itemDataDic[value.itemType];
						string text = Game.Language.Get(itemData.name, "");
						pickItemData.text.text = ((value.itemNum > 0) ? string.Format("{0}({1})", text, value.itemNum) : text);
						pickItemData.text.color = ColorDefine.QuaColor[itemData.quality];
					}
					pickItemData.rectTransform.anchoredPosition = new Vector2((vector.x - 0.5f) * screenSize.x, (vector.y - 0.5f) * screenSize.y + 45f);
					num++;
				}
			}
		}
		if (num < count)
		{
			for (int i = num; i < count; i++)
			{
				UI_Battle.PickItemData pickItemData2 = this.pickItemList[i];
				if (pickItemData2.go.activeSelf)
				{
					pickItemData2.go.SetActive(false);
				}
			}
		}
		enumerator.Dispose();
	}

	// Token: 0x060011ED RID: 4589 RVA: 0x00069F88 File Offset: 0x00068188
	private static void Pick(ItemStruct itemStruct)
	{
		if (GameHelperClient.localPlayer.hp > 0L && GameHelperClient.localPlayer.RoleState != RoleState.Dead)
		{
			if (!ItemManager.CanLocalPlayerPickItem(itemStruct))
			{
				Util.ShowTips("无法拾取他人物品");
				return;
			}
			EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/拾取物品", 0.9f, 3f);
			if (Util.ItemIsAddBag(itemStruct.itemType) && GameHelperClient.localPlayer.playerAttribute.BagIsFull())
			{
				Util.ShowTips("背包已满");
				return;
			}
			GameHelperClient.localPlayer.CmdPickItem(itemStruct.id);
		}
	}

	// Token: 0x060011EE RID: 4590 RVA: 0x0006A018 File Offset: 0x00068218
	public void OnKeyPick()
	{
		if (GameHelperClient.localPlayer.hp > 0L && GameHelperClient.localPlayer.RoleState != RoleState.Dead)
		{
			Dictionary<uint, ItemStruct>.Enumerator enumerator = Game.ItemManager.itemStructs.GetEnumerator();
			int num = GameHelperClient.localPlayer.playerAttribute.BagNum();
			bool flag = false;
			bool flag2 = false;
			while (enumerator.MoveNext())
			{
				KeyValuePair<uint, ItemStruct> keyValuePair = enumerator.Current;
				ItemStruct value = keyValuePair.Value;
				if (GameHelperClient.localPlayer.GetDistanceV2(value.pos) < 5f && (GameHelperClient.IsKeyPickTalisman || !Util.IsTalisman(value.itemType) || value.itemType == ItemType.Talisman_Experience))
				{
					if (!ItemManager.CanLocalPlayerPickItem(value))
					{
						flag2 = true;
					}
					else
					{
						flag = true;
						if (Util.ItemIsAddBag(value.itemType))
						{
							if (num >= 6)
							{
								Util.ShowTips("背包已满");
								continue;
							}
							num++;
						}
						GameHelperClient.localPlayer.CmdPickItem(value.id);
					}
				}
			}
			enumerator.Dispose();
			if (flag)
			{
				EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/拾取物品", 0.9f, 3f);
				return;
			}
			if (flag2)
			{
				Util.ShowTips("无法拾取他人物品");
			}
		}
	}

	// Token: 0x060011EF RID: 4591 RVA: 0x0006A13C File Offset: 0x0006833C
	private void UpdateHpBar()
	{
		if (!this.isInitHpBar)
		{
			this.InitHpBar();
			if (!this.isInitHpBar)
			{
				return;
			}
		}
		PlayerManagerClient playerManagerClient = EntityStatic.Get<PlayerManagerClient>();
		List<RoleBase> clientPlayerList = playerManagerClient.clientPlayerList;
		Dictionary<uint, PlayerBase> clientPlayerDic = playerManagerClient.clientPlayerDic;
		int count = clientPlayerList.Count;
		List<RoleBase> clientEnemyList = Game.EnemyManagerClient.clientEnemyList;
		int count2 = clientEnemyList.Count;
		int num = count + count2;
		Vector3 position = Game.CameraManager.MyTransform.position;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int count3 = this.hpShowList.Count;
		int count4 = this.bossHpShowList.Count;
		int count5 = this.playerHpShowList.Count;
		this.curEnemyNum = 0;
		Camera camera = Game.CameraManager.camera;
		int num5 = (int)((GameHelperClient.ClickTrackRole != null) ? GameHelperClient.ClickTrackRole.netId : uint.MaxValue);
		bool flag = num5 != -1;
		if (this.lockRect.gameObject.activeSelf != flag)
		{
			this.lockRect.gameObject.SetActive(flag);
		}
		Vector2 screenSize = this.GetScreenSize();
		int i = 0;
		while (i < num)
		{
			RoleBase roleBase;
			if (i < count)
			{
				roleBase = clientPlayerList[i];
				if (roleBase.roleType == RoleType.Player)
				{
					(roleBase as PlayerBase).enemyNum = 0;
				}
				if (!(roleBase == null) && !roleBase.IsDead())
				{
					if (roleBase.gameObject.activeSelf)
					{
						goto IL_1BA;
					}
				}
			}
			else
			{
				roleBase = clientEnemyList[i - count];
				if (!(roleBase == null) && !roleBase.IsDead() && roleBase.gameObject.activeSelf)
				{
					PlayerBase playerBase;
					if ((ulong)roleBase.authorityId <= (ulong)((long)count) && clientPlayerDic.TryGetValue(roleBase.authorityId, out playerBase))
					{
						playerBase.enemyNum++;
					}
					this.curEnemyNum++;
					goto IL_1BA;
				}
			}
			IL_409:
			i++;
			continue;
			IL_1BA:
			float fillAmount = (float)roleBase.hp / (float)roleBase.maxHp;
			Vector3 headUIPos = roleBase.GetHeadUIPos();
			Vector3 vector = headUIPos - position;
			vector.y = 0f;
			if (Vector3.SqrMagnitude(vector) > 200f)
			{
				goto IL_409;
			}
			Vector3 vector2 = camera.WorldToViewportPoint(headUIPos);
			if (vector2.z >= 0f)
			{
				MyHpBar myHpBar;
				if (roleBase.roleType == RoleType.Player)
				{
					if (num4 < count5)
					{
						myHpBar = this.playerHpShowList[num4];
						myHpBar.Show();
					}
					else
					{
						myHpBar = this.GetPlayerHpBar();
					}
					PlayerBase playerBase2 = roleBase as PlayerBase;
					myHpBar.ShowName(GameHelperClient.GetPlayerDisplayName(playerBase2), Color.white, GameHelperClient.IsShowPlayerName());
					num4++;
				}
				else if (roleBase.IsShowName())
				{
					if (num3 < count4)
					{
						myHpBar = this.bossHpShowList[num3];
						myHpBar.Show();
					}
					else
					{
						myHpBar = this.GetBossHpBar();
					}
					myHpBar.ShowName(roleBase.roleName, Color.white, true);
					num3++;
					if ((ulong)roleBase.netId == (ulong)((long)num5))
					{
						this.lockRect.SetParent(myHpBar.MyRectTransform, false);
						this.lockRect.anchoredPosition = new Vector2(-115f, -20f);
						this.lockRect.localScale = new Vector3(2f, 2f, 2f);
					}
				}
				else
				{
					if (num2 < count3)
					{
						myHpBar = this.hpShowList[num2];
						myHpBar.Show();
					}
					else
					{
						myHpBar = this.GetHpBar();
					}
					num2++;
					if ((ulong)roleBase.netId == (ulong)((long)num5))
					{
						this.lockRect.SetParent(myHpBar.MyRectTransform, false);
						this.lockRect.anchoredPosition = new Vector2(-50f, 0f);
						this.lockRect.localScale = Vector3.one;
					}
				}
				float shieldFill = (roleBase.Shield > 0L) ? ((float)roleBase.Shield / (float)roleBase.maxHp) : 0f;
				myHpBar.UpdateValue(fillAmount, shieldFill);
				myHpBar.MyRectTransform.anchoredPosition = new Vector2((vector2.x - 0.5f) * screenSize.x, (vector2.y - 0.5f) * screenSize.y + 20f);
				goto IL_409;
			}
			goto IL_409;
		}
		if (num2 < count3)
		{
			for (int j = num2; j < count3; j++)
			{
				this.hpShowList[j].Hide();
			}
		}
		if (num3 < count4)
		{
			for (int k = num3; k < count4; k++)
			{
				this.bossHpShowList[k].Hide();
			}
		}
		if (num4 < count5)
		{
			for (int l = num4; l < count5; l++)
			{
				this.playerHpShowList[l].Hide();
			}
		}
	}

	// Token: 0x060011F0 RID: 4592 RVA: 0x0006A5E0 File Offset: 0x000687E0
	private Vector2 GetScreenSize()
	{
		float num = 1.7777778f;
		if ((float)Screen.width / (float)Screen.height > num)
		{
			return new Vector2(1080f * (float)Screen.width / (float)Screen.height, 1080f);
		}
		return new Vector2(1920f, 1920f * (float)Screen.height / (float)Screen.width);
	}

	// Token: 0x060011F1 RID: 4593 RVA: 0x0006A640 File Offset: 0x00068840
	private MyHpBar GetHpBar()
	{
		MyHpBar myHpBar = Object.Instantiate<MyHpBar>(this.hpPrefab, this.hpParent);
		myHpBar.gameObject.SetActive(true);
		this.hpShowList.Add(myHpBar);
		return myHpBar;
	}

	// Token: 0x060011F2 RID: 4594 RVA: 0x0006A678 File Offset: 0x00068878
	private MyHpBar GetBossHpBar()
	{
		MyHpBar myHpBar = Object.Instantiate<MyHpBar>(this.bossHpPrefab, this.hpParent);
		myHpBar.gameObject.SetActive(true);
		this.bossHpShowList.Add(myHpBar);
		return myHpBar;
	}

	// Token: 0x060011F3 RID: 4595 RVA: 0x0006A6B0 File Offset: 0x000688B0
	private MyHpBar GetPlayerHpBar()
	{
		MyHpBar myHpBar = Object.Instantiate<MyHpBar>(this.playerHpPrefab, this.hpParent);
		myHpBar.gameObject.SetActive(true);
		this.playerHpShowList.Add(myHpBar);
		return myHpBar;
	}

	// Token: 0x060011F4 RID: 4596 RVA: 0x0006A6E8 File Offset: 0x000688E8
	private void UpdateDamageUI(float time)
	{
		int count = this.damageShowList.Count;
		if (count == 0)
		{
			return;
		}
		for (int i = count - 1; i > -1; i--)
		{
			UI_Battle.DamageUIData damageUIData = this.damageShowList[i];
			damageUIData.life -= time;
			if (damageUIData.life < 0f)
			{
				this.PushDamageText(damageUIData);
				this.damageShowList.RemoveAt(i);
			}
			else if (damageUIData.life > 0.5f)
			{
				damageUIData.myTransform.position += time * 200f * Vector3.up;
				damageUIData.myTransform.localScale = (0.7f - damageUIData.life) * 5f * Vector3.one;
			}
			else if (damageUIData.life < 0.1f)
			{
				damageUIData.myTransform.position += time * 200f * Vector3.up;
				Color color = damageUIData.damageText.color;
				damageUIData.damageText.color = new Color(color.r, color.g, color.b, damageUIData.life / 0.1f);
			}
			else
			{
				damageUIData.myTransform.localScale = Vector3.one;
			}
		}
	}

	// Token: 0x060011F5 RID: 4597 RVA: 0x0006A83C File Offset: 0x00068A3C
	private UI_Battle.DamageUIData GetDamageText()
	{
		int count = this.damageTextTemps.Count;
		if (count > 0)
		{
			UI_Battle.DamageUIData damageUIData = this.damageTextTemps[count - 1];
			this.damageTextTemps.RemoveAt(count - 1);
			damageUIData.go.SetActive(true);
			return damageUIData;
		}
		GameObject gameObject = Object.Instantiate<GameObject>(this.selfView.trans_damagePrefab.gameObject, this.selfView.trans_damageParent);
		gameObject.SetActive(true);
		UI_Battle.DamageUIData damageUIData2 = new UI_Battle.DamageUIData();
		damageUIData2.go = gameObject;
		TextMeshProUGUI component = gameObject.GetComponent<TextMeshProUGUI>();
		component.raycastTarget = false;
		damageUIData2.damageText = component;
		damageUIData2.myTransform = component.rectTransform;
		return damageUIData2;
	}

	// Token: 0x060011F6 RID: 4598 RVA: 0x0006A8D7 File Offset: 0x00068AD7
	private void PushDamageText(UI_Battle.DamageUIData damageUIData)
	{
		damageUIData.go.SetActive(false);
		this.damageTextTemps.Add(damageUIData);
	}

	// Token: 0x060011F7 RID: 4599 RVA: 0x0006A8F4 File Offset: 0x00068AF4
	public void ShowDamage(int damage, Vector3 worldPos, bool isAttackWeek)
	{
		UI_Battle.DamageUIData damageText = this.GetDamageText();
		damageText.damageText.text = damage.ToString();
		damageText.life = 0.6f;
		damageText.myTransform.localScale = Vector3.zero;
		damageText.damageText.color = (isAttackWeek ? ColorDefine.WeekDamage : Color.white);
		this.damageShowList.Add(damageText);
		Vector2 vector = Game.Camera.WorldToScreenPoint(worldPos);
		damageText.myTransform.position = new Vector3(vector.x, vector.y + 35f, 0f);
	}

	// Token: 0x060011F8 RID: 4600 RVA: 0x0006A993 File Offset: 0x00068B93
	public void OnStartDead()
	{
		if (GameHelperClient.isKingBattle)
		{
			return;
		}
		AnalyticsManager analytics = Game.Analytics;
		if (analytics != null)
		{
			analytics.RecordPlayerDead();
		}
		this.selfView.trans_deadGo.gameObject.SetActive(true);
	}

	// Token: 0x060011F9 RID: 4601 RVA: 0x0006A9C3 File Offset: 0x00068BC3
	public void OnExitDead()
	{
		this.selfView.trans_deadGo.gameObject.SetActive(false);
	}

	// Token: 0x060011FA RID: 4602 RVA: 0x0006A9DC File Offset: 0x00068BDC
	private void UpdateDamageVignette()
	{
		float num = (float)GameHelperClient.localPlayer.hp / (float)GameHelperClient.localPlayer.maxHp;
		if (GameHelperClient.localPlayer.hp != 0L && this.lastFill - num > 0.2f)
		{
			this.lastFill = num;
			this.PlayDamageVignetteAnimator();
		}
		else if (num > this.lastFill)
		{
			this.lastFill = num;
		}
		if (Time.time > this.checkHpTime)
		{
			this.checkHpTime = Time.time + 1f;
			if (this.lastFill - num > 0.2f)
			{
				this.PlayDamageVignetteAnimator();
			}
			this.lastFill = num;
		}
		if (GameHelperClient.localPlayer.hp != 0L && num < 0.3f)
		{
			this.damageVignetteAnimator.SetBool(AnimDefine.Active, true);
			return;
		}
		this.damageVignetteAnimator.SetBool(AnimDefine.Active, false);
	}

	// Token: 0x060011FB RID: 4603 RVA: 0x0006AAAC File Offset: 0x00068CAC
	private void PlayDamageVignetteAnimator()
	{
		this.damageVignetteAnimator.Play(AnimDefine.Hit);
	}

	// Token: 0x060011FC RID: 4604 RVA: 0x0006AAC0 File Offset: 0x00068CC0
	public void ShowEnemyEnterTip(EnemyType enemyType)
	{
		this.showEnemyEnterTime = 0f;
		if (!this.selfView.trans_EnemyEnterTip.gameObject.activeSelf)
		{
			this.selfView.trans_EnemyEnterTip.gameObject.SetActive(true);
		}
		RoleAttribute roleAttribute = Game.GameData.HeroAttributeDic[enemyType.ToString()];
		this.selfView.img_bossHead.sprite = Resources.Load<Sprite>(PathDefine.Concat("Bundles/UI/Icon/HeadIcon/Enemy_", roleAttribute.model));
		this.selfView.ltext_bossTip.text = Game.Language.Get("BOSS提示", "");
	}

	// Token: 0x060011FD RID: 4605 RVA: 0x0006AB6C File Offset: 0x00068D6C
	private void ShowKingBattleFinalTip()
	{
		this.showEnemyEnterTime = 0f;
		if (!this.selfView.trans_EnemyEnterTip.gameObject.activeSelf)
		{
			this.selfView.trans_EnemyEnterTip.gameObject.SetActive(true);
		}
		if (GameHelperClient.localPlayer != null)
		{
			this.selfView.img_bossHead.sprite = Util.GetHeroIcon(GameHelperClient.localPlayer.heroType);
		}
		this.selfView.ltext_bossTip.text = Game.Language.Get("最终决战", "");
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/购买怪物", 1f, 3f);
	}

	// Token: 0x060011FE RID: 4606 RVA: 0x0006AC1B File Offset: 0x00068E1B
	public void ShowGameStartBtn(bool isShow)
	{
		this.selfView.btn_startGame.gameObject.SetActive(isShow);
	}

	// Token: 0x060011FF RID: 4607 RVA: 0x0006AC34 File Offset: 0x00068E34
	public void ShowMask()
	{
		float duration = 1.25f;
		this.selfView.img_normalMask.gameObject.SetActive(true);
		this.selfView.img_normalMask.color = new Color(0f, 0f, 0f, 0f);
		this.selfView.img_normalMask.DOColor(new Color(0f, 0f, 0f, 1f), 0.25f).SetEase(Ease.Linear);
		Game.TimerManager.AddTimer(0.6f, delegate()
		{
			this.selfView.img_normalMask.DOColor(new Color(0f, 0f, 0f, 0f), 0.5f).SetEase(Ease.Linear);
		});
		Game.TimerManager.AddTimer(duration, delegate()
		{
			this.selfView.img_normalMask.gameObject.SetActive(false);
			this.showEnemyEnterTime = 0f;
			if (!this.selfView.trans_EnemyEnterTip.gameObject.activeSelf)
			{
				this.selfView.trans_EnemyEnterTip.gameObject.SetActive(true);
			}
			this.selfView.img_bossHead.sprite = Util.GetHeroIcon(GameHelperClient.localPlayer.heroType);
			this.selfView.ltext_bossTip.text = Game.Language.Get("即将开始王位加冕挑战", "");
		});
	}

	// Token: 0x04000FE8 RID: 4072
	public UI_Battle_View selfView;

	// Token: 0x04000FE9 RID: 4073
	private List<UI_Battle.DamageUIData> damageTextTemps = new List<UI_Battle.DamageUIData>();

	// Token: 0x04000FEA RID: 4074
	private List<UI_Battle.DamageUIData> damageShowList = new List<UI_Battle.DamageUIData>();

	// Token: 0x04000FEB RID: 4075
	private const float damageLifeTime = 0.7f;

	// Token: 0x04000FEC RID: 4076
	private List<UI_Battle.PickItemData> pickItemList = new List<UI_Battle.PickItemData>();

	// Token: 0x04000FED RID: 4077
	private int maxEnemyNum;

	// Token: 0x04000FEE RID: 4078
	private int curEnemyNum;

	// Token: 0x04000FEF RID: 4079
	private List<MyHpBar> hpShowList = new List<MyHpBar>();

	// Token: 0x04000FF0 RID: 4080
	private MyHpBar hpPrefab;

	// Token: 0x04000FF1 RID: 4081
	private MyHpBar bossHpPrefab;

	// Token: 0x04000FF2 RID: 4082
	private List<MyHpBar> bossHpShowList = new List<MyHpBar>();

	// Token: 0x04000FF3 RID: 4083
	private MyHpBar playerHpPrefab;

	// Token: 0x04000FF4 RID: 4084
	private List<MyHpBar> playerHpShowList = new List<MyHpBar>();

	// Token: 0x04000FF5 RID: 4085
	private float breathTime;

	// Token: 0x04000FF6 RID: 4086
	private RectTransform lockRect;

	// Token: 0x04000FF7 RID: 4087
	private Animator damageVignetteAnimator;

	// Token: 0x04000FF8 RID: 4088
	private float checkHpTime;

	// Token: 0x04000FF9 RID: 4089
	private float lastFill = 1f;

	// Token: 0x04000FFA RID: 4090
	private List<UI_Battle_TeamHead> teamHeadList;

	// Token: 0x04000FFB RID: 4091
	private UI_Battle_Joy uiBattleJoy;

	// Token: 0x04000FFC RID: 4092
	private float showEnemyEnterTime;

	// Token: 0x04000FFD RID: 4093
	private float showTipAllTime;

	// Token: 0x04000FFE RID: 4094
	private bool needBuyMonster;

	// Token: 0x04000FFF RID: 4095
	private bool isInitHpBar;

	// Token: 0x04001000 RID: 4096
	private Transform hpParent;

	// Token: 0x04001001 RID: 4097
	private int curGamePlayItemId = -1;

	// Token: 0x04001002 RID: 4098
	private float checkGameOverTime;

	// Token: 0x04001003 RID: 4099
	private float checkGameWinTime;

	// Token: 0x04001004 RID: 4100
	private float countDownTime;

	// Token: 0x04001005 RID: 4101
	private Vector2 pickUIStartPos;

	// Token: 0x04001006 RID: 4102
	private RectTransform rectPickUI;

	// Token: 0x04001007 RID: 4103
	private bool isInitPickItemUI;

	// Token: 0x04001008 RID: 4104
	private bool isShowKingBattleFinalTip;

	// Token: 0x04001009 RID: 4105
	public string readySyncData;

	// Token: 0x02000306 RID: 774
	private class DamageUIData
	{
		// Token: 0x0400100A RID: 4106
		public float life;

		// Token: 0x0400100B RID: 4107
		public RectTransform myTransform;

		// Token: 0x0400100C RID: 4108
		public TextMeshProUGUI damageText;

		// Token: 0x0400100D RID: 4109
		public GameObject go;
	}

	// Token: 0x02000307 RID: 775
	private class PickItemData
	{
		// Token: 0x0400100E RID: 4110
		public GameObject go;

		// Token: 0x0400100F RID: 4111
		public RectTransform rectTransform;

		// Token: 0x04001010 RID: 4112
		public Text text;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using Mirror.RemoteCalls;
using RVO;
using UnityEngine;
using UnityEngine.Serialization;

// Token: 0x0200025C RID: 604
public class EnemyBase : RoleBase
{
	// Token: 0x1700004D RID: 77
	// (get) Token: 0x06000ACF RID: 2767 RVA: 0x00037810 File Offset: 0x00035A10
	public Collider CheckCollider
	{
		get
		{
			return this.checkCollider;
		}
	}

	// Token: 0x1700004E RID: 78
	// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x00037818 File Offset: 0x00035A18
	public bool IsAutoDead
	{
		get
		{
			return this.isAutoDead;
		}
	}

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x00037820 File Offset: 0x00035A20
	public EnemyEntriesType[] EnemyEntriesTypes
	{
		get
		{
			return this.enemyEntriesTypes;
		}
	}

	// Token: 0x17000050 RID: 80
	// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x00037828 File Offset: 0x00035A28
	public EnemyCreateType EnemyCreateType
	{
		get
		{
			return this.enemyCreateType;
		}
	}

	// Token: 0x06000AD3 RID: 2771 RVA: 0x00037830 File Offset: 0x00035A30
	public override void InitRoleModeBase(RoleModeBase roleModeBaseValue)
	{
		base.InitRoleModeBase(roleModeBaseValue);
		this.checkCollider = roleModeBaseValue.gameObject.GetComponent<Collider>();
		this.enemyModeBase = (this.roleModeBase as EnemyModeBase);
		this.enemyModeBase.enemyBase = this;
		if (this.roleState == RoleState.ShowPose)
		{
			this.OnStartShowPose();
		}
	}

	// Token: 0x06000AD4 RID: 2772 RVA: 0x00037884 File Offset: 0x00035A84
	public override void ExitDeadState()
	{
		base.ExitDeadState();
		if (GameHelperClient.isHost)
		{
			NetworkServer.UnSpawn(base.gameObject);
		}
		if (this.roleType == RoleType.Enemy)
		{
			Game.EnemyManagerClient.RemoveEnemy(this);
		}
		else if (this.roleType == RoleType.Summon)
		{
			Game.PlayerManagerClient.RemovePlayer(this);
		}
		AssetManagerMirror.UnLoadPrefab(base.gameObject);
	}

	// Token: 0x06000AD5 RID: 2773 RVA: 0x000378E0 File Offset: 0x00035AE0
	public void ServerCheckEnemyDead(RoleBase attackerRole)
	{
		int lucky = 0;
		if (attackerRole.roleType == RoleType.Player)
		{
			lucky = (attackerRole as PlayerBase).lucky;
		}
		SOGameConfig gameConfig = GameHelperClient.gameConfig;
		float luckAddValue = Util.GetLuckAddValue(lucky);
		if (this.isBoss && Random.value < gameConfig.BossDropMedicineProbability * (1f + luckAddValue))
		{
			this.DropBossMedicine();
		}
		EnemyType enemyType = this.enemyType;
		if (enemyType <= EnemyType.Chest)
		{
			if (enemyType == EnemyType.SaiYa)
			{
				goto IL_2E4;
			}
			if (enemyType == EnemyType.SaiYaDark)
			{
				this.DropSaiYa();
				this.CreateDeadAttributeBook(Random.Range(gameConfig.AttributeBook_MinNum, gameConfig.AttributeBook_MaxNum + 1));
				goto IL_2E4;
			}
			if (enemyType == EnemyType.Chest)
			{
				this.DropChest();
				this.CreateDeadAttributeBook(Random.Range(gameConfig.AttributeBook_MinNum, gameConfig.AttributeBook_MaxNum + 1));
				goto IL_2E4;
			}
		}
		else if (enemyType <= EnemyType.Goblin_HellFlame_5)
		{
			if (enemyType - EnemyType.Goblin_Blacksmith_0 > 3)
			{
				if (enemyType - EnemyType.Goblin_HellFlame_0 <= 5)
				{
					float num = 0f;
					List<EntryConditions> entryConditions = null;
					if (attackerRole.roleType == RoleType.Player)
					{
						num = (attackerRole as PlayerBase).RelicAdd;
						entryConditions = (attackerRole as PlayerBase).entryConditions;
					}
					this.DropHellFlameRemains(luckAddValue + num, entryConditions);
					this.CreateDeadAttributeBook(Random.Range(gameConfig.AttributeBook_MinNum, gameConfig.AttributeBook_MaxNum + 1));
					goto IL_2E4;
				}
			}
			else
			{
				this.CreateDeadAttributeBook(Random.Range(gameConfig.AttributeBook_MinNum, gameConfig.AttributeBook_MaxNum + 1));
				PlayerBase connectionPlayer = this.GetConnectionPlayer();
				if (connectionPlayer != null)
				{
					connectionPlayer.OnKillBlacksmith();
					goto IL_2E4;
				}
				goto IL_2E4;
			}
		}
		else if (enemyType - EnemyType.Goblin_Mine_0 > 5)
		{
			if (enemyType - EnemyType.Goblin_HeartMonster_0 <= 5)
			{
				if (Random.value < gameConfig.HeartMonsterDropSkillProbability * (1f + luckAddValue))
				{
					float bookAdd = 0f;
					if (attackerRole.roleType == RoleType.Player)
					{
						bookAdd = (attackerRole as PlayerBase).BookAdd;
					}
					this.DropHeartMonsterSkillBook(bookAdd);
				}
				this.CreateDeadAttributeBook(Random.Range(gameConfig.AttributeBook_MinNum, gameConfig.AttributeBook_MaxNum + 1));
				goto IL_2E4;
			}
		}
		else
		{
			this.CreateDeadAttributeBook(Random.Range(gameConfig.AttributeBook_MinNum, gameConfig.AttributeBook_MaxNum + 1));
			PlayerBase connectionPlayer = this.GetConnectionPlayer();
			if (connectionPlayer != null)
			{
				connectionPlayer.TargetKillGoblinMine();
				goto IL_2E4;
			}
			goto IL_2E4;
		}
		if (this.isBoss)
		{
			if (Random.value < gameConfig.BossDropSkillProbability * (1f + luckAddValue))
			{
				float bookAdd2 = 0f;
				if (attackerRole.roleType == RoleType.Player)
				{
					bookAdd2 = (attackerRole as PlayerBase).BookAdd;
				}
				this.DropBossSkillBook(bookAdd2);
			}
			this.CreateDeadAttributeBook(Random.Range(gameConfig.AttributeBook_MinNum, gameConfig.AttributeBook_MaxNum + 1));
			if (Random.value < gameConfig.BossDropSleepStoneProbability * (1f + luckAddValue))
			{
				this.DropBoss();
			}
		}
		else if (this.isElite)
		{
			if (Random.value < gameConfig.EliteDropProbability * this.GetNormalDropLevel(gameConfig) * (1f + luckAddValue))
			{
				this.DropNormalItem(attackerRole);
			}
		}
		else if (Random.value < gameConfig.NormalDropProbability * this.GetNormalDropLevel(gameConfig) * (1f + luckAddValue))
		{
			this.DropNormalItem(attackerRole);
		}
		IL_2E4:
		RoleAttribute roleAttribute = Game.GameData.HeroAttributeDic[this.enemyType.ToString()];
		ExDropData exDropData;
		if ((this.isBoss || this.isElite || roleAttribute.dropCard) && Game.GameData.ExDropDataDic.TryGetValue(roleAttribute.model, out exDropData) && Random.value < exDropData.allChance * (1f + luckAddValue))
		{
			float num2 = Random.value * exDropData.allChance;
			float num3 = 0f;
			int count = exDropData.exDropChance.Count;
			for (int i = 0; i < count; i++)
			{
				ExDropChance exDropChance = exDropData.exDropChance[i];
				num3 += exDropChance.dropChance;
				if (num2 < num3)
				{
					ItemStruct itemStruct = new ItemStruct();
					UnityEngine.Vector2 pointByRadian = Util.GetPointByRadian(1f, 0f, Random.value * 360f);
					itemStruct.id = ItemManager.GetItemId();
					itemStruct.pos = new Vector3(this.myTransform.position.x + pointByRadian.x, 0f, this.myTransform.position.z + pointByRadian.y);
					itemStruct.itemType = exDropChance.dropCardId + ItemType.Card_0;
					this.RpcDeadItem(new ItemStruct[]
					{
						itemStruct
					});
					return;
				}
			}
		}
	}

	// Token: 0x06000AD6 RID: 2774 RVA: 0x00037D3C File Offset: 0x00035F3C
	private void DropBossSkillBook(float bookAdd)
	{
		SOGameConfig gameConfig = GameHelperClient.gameConfig;
		SOGameConfig.DropData dropData;
		if (!this.TryGetDropData(gameConfig.BossSkillBook_Level, out dropData))
		{
			return;
		}
		ItemType randomSkillBook = Util.GetRandomSkillBook(dropData.dropChance, bookAdd);
		if (randomSkillBook == ItemType.None)
		{
			return;
		}
		ItemStruct itemStruct = new ItemStruct();
		Vector3 pos = new Vector3(this.myTransform.position.x, 0f, this.myTransform.position.z);
		itemStruct.id = ItemManager.GetItemId();
		itemStruct.pos = pos;
		itemStruct.itemType = randomSkillBook;
		this.RpcDeadItem(new ItemStruct[]
		{
			itemStruct
		});
	}

	// Token: 0x06000AD7 RID: 2775 RVA: 0x00037DD0 File Offset: 0x00035FD0
	private void DropHellFlameRemains(float luckyAdd, List<EntryConditions> entryConditions)
	{
		float[] remainDrop = GameHelperClient.gameConfig.RemainDrop;
		float[] remainLucky = GameHelperClient.gameConfig.RemainLucky;
		if (remainDrop == null || remainLucky == null)
		{
			return;
		}
		int num = Mathf.Min(remainDrop.Length, remainLucky.Length);
		if (num <= 0)
		{
			return;
		}
		float num2 = 0f;
		int num3 = 0;
		float num4 = 0f;
		for (int i = 0; i < num; i++)
		{
			num4 += remainDrop[i] * (1f + luckyAdd * remainLucky[i]);
		}
		if (num4 <= 0f)
		{
			return;
		}
		float num5 = Random.value * num4;
		for (int j = 0; j < num; j++)
		{
			num2 += remainDrop[j] * (1f + luckyAdd * remainLucky[j]);
			if (num5 < num2)
			{
				num3 = j;
				break;
			}
		}
		ItemStruct itemStruct = new ItemStruct();
		Vector3 pos = new Vector3(this.myTransform.position.x, 0f, this.myTransform.position.z);
		itemStruct.id = ItemManager.GetItemId();
		itemStruct.pos = pos;
		List<ItemType> list = new List<ItemType>();
		Dictionary<ItemType, RemainsData> remainsDataDic = Game.GameData.RemainsDataDic;
		num = remainsDataDic.Count;
		for (int k = 0; k < num; k++)
		{
			KeyValuePair<ItemType, RemainsData> keyValuePair = remainsDataDic.ElementAt(k);
			if (keyValuePair.Value.grade == num3 && (keyValuePair.Value.conditions == EntryConditions.None || (entryConditions != null && entryConditions.Contains(keyValuePair.Value.conditions))))
			{
				list.Add(keyValuePair.Key);
			}
		}
		if (list.Count <= 0)
		{
			return;
		}
		itemStruct.itemType = list[Random.Range(0, list.Count)];
		this.RpcDeadItem(new ItemStruct[]
		{
			itemStruct
		});
	}

	// Token: 0x06000AD8 RID: 2776 RVA: 0x00037F7C File Offset: 0x0003617C
	private void DropHeartMonsterSkillBook(float bookAdd)
	{
		SOGameConfig gameConfig = GameHelperClient.gameConfig;
		SOGameConfig.DropData dropData;
		if (!this.TryGetDropData(gameConfig.HeartMonsterSkillBook_Level, out dropData))
		{
			return;
		}
		ItemType randomSkillBook = Util.GetRandomSkillBook(dropData.dropChance, bookAdd);
		if (randomSkillBook == ItemType.None)
		{
			return;
		}
		ItemStruct itemStruct = new ItemStruct();
		Vector3 pos = new Vector3(this.myTransform.position.x, 0f, this.myTransform.position.z);
		itemStruct.id = ItemManager.GetItemId();
		itemStruct.pos = pos;
		itemStruct.itemType = randomSkillBook;
		this.RpcDeadItem(new ItemStruct[]
		{
			itemStruct
		});
	}

	// Token: 0x06000AD9 RID: 2777 RVA: 0x00038010 File Offset: 0x00036210
	private void DropSaiYa()
	{
		ItemStruct itemStruct = new ItemStruct();
		Vector3 pos = new Vector3(this.myTransform.position.x, 0f, this.myTransform.position.z);
		itemStruct.id = ItemManager.GetItemId();
		itemStruct.pos = pos;
		itemStruct.itemType = ItemType.HeroSoul + Random.Range(0, 3);
		this.RpcDeadItem(new ItemStruct[]
		{
			itemStruct
		});
	}

	// Token: 0x06000ADA RID: 2778 RVA: 0x00038084 File Offset: 0x00036284
	private void DropChest()
	{
		this.DropItemList("chest_" + GameHelperClient.LevelIndex.ToString(), 1f);
	}

	// Token: 0x06000ADB RID: 2779 RVA: 0x000380A5 File Offset: 0x000362A5
	private void DropBoss()
	{
		this.DropItemList("boss_normal", 1f);
	}

	// Token: 0x06000ADC RID: 2780 RVA: 0x000380B7 File Offset: 0x000362B7
	private void DropBossMedicine()
	{
		this.DropItemList("boss_medicine", 1f);
	}

	// Token: 0x06000ADD RID: 2781 RVA: 0x000380CC File Offset: 0x000362CC
	private void DropItemList(string dropKey, float randomRange)
	{
		List<ItemType> dropItem = Util.GetDropItem(dropKey, Util.GetLuckAddValue(GameHelperClient.localPlayer.lucky));
		if (dropItem == null)
		{
			return;
		}
		int count = dropItem.Count;
		ItemStruct[] array = new ItemStruct[count];
		Vector3 vector = new Vector3(this.myTransform.position.x, 0f, this.myTransform.position.z);
		for (int i = 0; i < count; i++)
		{
			ItemStruct itemStruct = new ItemStruct();
			UnityEngine.Vector2 pointByRadian = Util.GetPointByRadian(Random.value * randomRange, 0f, Random.value * 360f);
			itemStruct.id = ItemManager.GetItemId();
			itemStruct.pos = new Vector3(vector.x + pointByRadian.x, vector.y, vector.z + pointByRadian.y);
			itemStruct.itemType = dropItem[i];
			array[i] = itemStruct;
		}
		this.RpcDeadItem(array);
	}

	// Token: 0x06000ADE RID: 2782 RVA: 0x000381B8 File Offset: 0x000363B8
	private void DropNormalItem(RoleBase attackerRole)
	{
		SOGameConfig gameConfig = GameHelperClient.gameConfig;
		float num = Random.value * (gameConfig.normalEnemyDropType.book + gameConfig.normalEnemyDropType.attribute + gameConfig.normalEnemyDropType.talisman);
		ItemType itemType = ItemType.None;
		if (num < gameConfig.normalEnemyDropType.book)
		{
			float bookAdd = 0f;
			if (attackerRole.roleType == RoleType.Player)
			{
				bookAdd = (attackerRole as PlayerBase).BookAdd;
			}
			SOGameConfig.DropData dropData;
			if (this.TryGetDropData(gameConfig.NormalSkillBook_Level, out dropData))
			{
				itemType = Util.GetRandomSkillBook(dropData.dropChance, bookAdd);
			}
		}
		else if (num < gameConfig.normalEnemyDropType.book + gameConfig.normalEnemyDropType.attribute)
		{
			itemType = Util.GetRandomAttributeBook();
		}
		else
		{
			itemType = Util.GetRandomTalisman();
		}
		if (itemType == ItemType.None)
		{
			return;
		}
		ItemStruct itemStruct = new ItemStruct();
		Vector3 pos = new Vector3(this.myTransform.position.x, 0f, this.myTransform.position.z);
		itemStruct.id = ItemManager.GetItemId();
		itemStruct.pos = pos;
		itemStruct.itemType = itemType;
		this.RpcDeadItem(new ItemStruct[]
		{
			itemStruct
		});
	}

	// Token: 0x06000ADF RID: 2783 RVA: 0x000382CC File Offset: 0x000364CC
	private float GetNormalDropLevel(SOGameConfig soGameSetting)
	{
		if (soGameSetting.NormalDropLevel == null || soGameSetting.NormalDropLevel.Length == 0)
		{
			return 1f;
		}
		int num = Mathf.Clamp(GameHelperClient.LevelIndex, 0, soGameSetting.NormalDropLevel.Length - 1);
		return soGameSetting.NormalDropLevel[num];
	}

	// Token: 0x06000AE0 RID: 2784 RVA: 0x00038310 File Offset: 0x00036510
	private bool TryGetDropData(List<SOGameConfig.DropData> dropDataList, out SOGameConfig.DropData dropData)
	{
		dropData = default(SOGameConfig.DropData);
		if (dropDataList == null || dropDataList.Count <= 0)
		{
			return false;
		}
		int index = Mathf.Clamp(GameHelperClient.LevelIndex, 0, dropDataList.Count - 1);
		dropData = dropDataList[index];
		return dropData.dropChance != null && dropData.dropChance.Length != 0;
	}

	// Token: 0x06000AE1 RID: 2785 RVA: 0x00038368 File Offset: 0x00036568
	private void CreateDeadAttributeBook(int itemNum)
	{
		ItemStruct[] array = new ItemStruct[itemNum];
		Vector3 vector = new Vector3(this.myTransform.position.x, 0f, this.myTransform.position.z);
		for (int i = 0; i < itemNum; i++)
		{
			ItemStruct itemStruct = new ItemStruct();
			UnityEngine.Vector2 pointByRadian = Util.GetPointByRadian(Random.value * 3f, 0f, Random.value * 360f);
			itemStruct.id = ItemManager.GetItemId();
			itemStruct.pos = new Vector3(vector.x + pointByRadian.x, vector.y, vector.z + pointByRadian.y);
			itemStruct.itemType = Util.GetRandomAttributeBook();
			array[i] = itemStruct;
		}
		this.RpcDeadItem(array);
	}

	// Token: 0x06000AE2 RID: 2786 RVA: 0x0003842A File Offset: 0x0003662A
	protected PlayerBase GetConnectionPlayer()
	{
		if (base.connectionToClient != null && base.connectionToClient.identity != null)
		{
			return base.connectionToClient.identity.gameObject.GetComponent<PlayerBase>();
		}
		return null;
	}

	// Token: 0x06000AE3 RID: 2787 RVA: 0x00038460 File Offset: 0x00036660
	public override void UpdateEvent()
	{
		base.UpdateEvent();
		if (this.roleState == RoleState.Idle || this.roleState == RoleState.Run)
		{
			if (base.hasAuthority)
			{
				this.MoveUpdate();
			}
		}
		else if (this.roleState == RoleState.Attack)
		{
			this.AttackUpdate();
		}
		else if (this.roleState == RoleState.Skill)
		{
			this.UpdateSkill1();
		}
		else if (this.roleState == RoleState.Skill2)
		{
			this.UpdateSkill2();
		}
		else if (this.roleState == RoleState.Skill3)
		{
			this.UpdateSkill3();
		}
		this.EnemyBaseUpdate();
	}

	// Token: 0x06000AE4 RID: 2788 RVA: 0x000384DC File Offset: 0x000366DC
	protected void EnemyBaseUpdate()
	{
		if ((!base.hasAuthority || this.roleState != RoleState.Run) && this.sid != -1)
		{
			Simulator.Instance.setAgentPosition(this.sid, new RVO.Vector2(this.myTransform.position.x, this.myTransform.position.z));
			Simulator.Instance.setAgentPrefVelocity(this.sid, new RVO.Vector2(0f, 0f));
		}
		if (base.hasAuthority && this.summonDeadTime > 0f && !base.IsDead())
		{
			if (GameHelperClient.isReady)
			{
				this.needClearSummon = true;
			}
			else if (this.needClearSummon)
			{
				this.summonDeadTime = -1f;
			}
			this.summonDeadTime -= Time.deltaTime;
			if (this.summonDeadTime <= 0f)
			{
				this.CmdAutoDead();
				return;
			}
		}
		if (this.enemyModeBase.isOpenSkillAI && base.hasAuthority)
		{
			this.UpdateAttackCd();
		}
	}

	// Token: 0x06000AE5 RID: 2789 RVA: 0x000385D8 File Offset: 0x000367D8
	protected virtual void MoveUpdate()
	{
		this.enemyModeBase.MoveUpdate();
	}

	// Token: 0x06000AE6 RID: 2790 RVA: 0x000385E8 File Offset: 0x000367E8
	public virtual void TrackMoveUpdate(Vector3 trackPos, bool isTrackRole)
	{
		if (this.enemyModeBase.isOpenSkillAI && this.trackRoleBase != null && this.StartAIAttack())
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		this.timer += deltaTime;
		this.oldRotation = this.myTransform.localEulerAngles.y;
		Vector3 position = this.myTransform.position;
		UnityEngine.Vector2 vector = new UnityEngine.Vector2(trackPos.x - position.x, trackPos.z - position.z);
		if (isTrackRole)
		{
			float num = vector.x * vector.x + vector.y * vector.y;
			float num2 = this.roleModeBase.GetAttackDistance() + this.trackRoleBase.RoleModeBase.addRange;
			if (num < num2 * num2)
			{
				if (this.timer > this.enemyModeBase.enemyAttackOffset / this.attackSpeed)
				{
					float moveAngle = base.GetMoveAngle(vector);
					base.PingHuaZhuanShen(moveAngle, 2f);
					base.OnLocalStartAttack();
					base.UpdateRoleState(RoleState.Attack);
					return;
				}
				if (this.waitTime <= 0f)
				{
					this.waitTime = 0.5f;
					base.UpdateRoleState(RoleState.Idle);
					return;
				}
				this.waitTime -= deltaTime;
				return;
			}
		}
		if (this.waitTime > 0f)
		{
			this.waitTime -= deltaTime;
			return;
		}
		if (this.sid == -1)
		{
			if (this.roleType == RoleType.Summon)
			{
				base.UpdateRoleState(RoleState.Run);
				float moveAngle2 = base.GetMoveAngle(vector);
				base.PingHuaZhuanShen(moveAngle2, 2f);
				base.MyTranslate(this.GetMoveSpeed() * deltaTime);
				return;
			}
			base.UpdateRoleState(RoleState.Idle);
			return;
		}
		else
		{
			RVO.Vector2 agentPosition = Simulator.Instance.getAgentPosition(this.sid);
			RVO.Vector2 agentPrefVelocity = Simulator.Instance.getAgentPrefVelocity(this.sid);
			this.myTransform.position = new Vector3(agentPosition.x(), 0f, agentPosition.y());
			if (this.roleState == RoleState.Run && base.GetDistanceV2(this.lastPos) / deltaTime < 0.15f)
			{
				this.waitTime = 1f;
				base.UpdateRoleState(RoleState.Idle);
				return;
			}
			base.UpdateRoleState(RoleState.Run);
			if (Math.Abs(agentPrefVelocity.x()) > 0.01f && Math.Abs(agentPrefVelocity.y()) > 0.01f)
			{
				this.myTransform.forward = new Vector3(agentPrefVelocity.x(), 0f, agentPrefVelocity.y()).normalized;
			}
			else
			{
				float moveAngle3 = base.GetMoveAngle(vector);
				base.PingHuaZhuanShen(moveAngle3, 2f);
			}
			RVO.Vector2 vector2 = new RVO.Vector2(trackPos.x, trackPos.z) - Simulator.Instance.getAgentPosition(this.sid);
			if (RVOMath.absSq(vector2) > 1f)
			{
				vector2 = RVOMath.normalize(vector2);
			}
			Simulator.Instance.setAgentPrefVelocity(this.sid, vector2 * this.GetMoveSpeed() * Time.deltaTime * 10f);
			float num3 = (float)this.m_random.NextDouble() * 2f * 3.1415927f;
			float scalar = (float)this.m_random.NextDouble() * 0.0001f;
			Simulator.Instance.setAgentPrefVelocity(this.sid, Simulator.Instance.getAgentPrefVelocity(this.sid) + scalar * new RVO.Vector2((float)Math.Cos((double)num3), (float)Math.Sin((double)num3)));
			this.lastPos = this.myTransform.position;
			return;
		}
	}

	// Token: 0x06000AE7 RID: 2791 RVA: 0x0003895F File Offset: 0x00036B5F
	protected override void OnStartRun()
	{
		base.OnStartRun();
		this.lastPos = Vector3.zero;
	}

	// Token: 0x06000AE8 RID: 2792 RVA: 0x00038974 File Offset: 0x00036B74
	public void GetTrackRole(bool isMoveTrack, float minDistance = 17f, bool isRefresh = true, bool isOnlyTrackPlayer = false)
	{
		if (isRefresh)
		{
			this.trackRoleBase = null;
		}
		bool flag = this.roleType == RoleType.Summon;
		List<RoleBase> attackRoles = base.GetAttackRoles();
		int count = attackRoles.Count;
		for (int i = 0; i < count; i++)
		{
			RoleBase roleBase = attackRoles[i];
			if (roleBase != null && !roleBase.IsDead() && (!isOnlyTrackPlayer || roleBase.roleType == RoleType.Player))
			{
				float num = base.GetDistanceV2(roleBase.MyTransform.position) - roleBase.addHatred;
				if (num < minDistance)
				{
					this.trackRoleBase = roleBase;
					minDistance = num;
				}
			}
		}
		if (!flag && isMoveTrack && this.trackRoleBase == null && GameHelperClient.localPlayer.authorityId == this.authorityId && GameHelperClient.localPlayer.IsDead())
		{
			this.trackRoleBase = GameHelperClient.localPlayer;
		}
		this.nextGetTrackTime = Time.time + 5f;
	}

	// Token: 0x06000AE9 RID: 2793 RVA: 0x00038A55 File Offset: 0x00036C55
	protected virtual void AttackUpdate()
	{
		RoleModeBase roleModeBase = this.roleModeBase;
		if (roleModeBase == null)
		{
			return;
		}
		roleModeBase.AttackUpdate();
	}

	// Token: 0x06000AEA RID: 2794 RVA: 0x00038A68 File Offset: 0x00036C68
	public void InitServer(Vector3 pos, RoleAttribute roleAttribute, RoleType roleTypeValue, uint authorityIdValue, bool isElite, EnemyType enemyType, EnemyCreateType enemyCreateType, string roleMode)
	{
		this.fatherId = -1;
		bool flag = false;
		if (isElite && enemyCreateType != EnemyCreateType.Chest)
		{
			this.enemyEntriesTypes = EnemyManagerServer.GetEliteEntriesTypes();
		}
		else if (enemyCreateType == EnemyCreateType.ChallengeAndBOSS && roleAttribute.type.Equals("boss"))
		{
			this.enemyEntriesTypes = EnemyManagerServer.GetBossEntriesTypes();
			flag = true;
		}
		else
		{
			this.enemyEntriesTypes = null;
		}
		pos = Util.GetSaveMapPos(pos);
		SOSpawnConfig.EnemySpawnTime enemySpawnTime = GameHelperClient.spawnConfig.enemySpawnData[GameHelperClient.WaveNum];
		long num = (enemyCreateType == EnemyCreateType.ChallengeAndBOSS) ? ConstDefine.ClampBattleValue((double)((float)roleAttribute.hp * enemySpawnTime.bossHpLevel)) : ConstDefine.ClampBattleValue((double)((float)roleAttribute.hp * enemySpawnTime.hpLevel));
		num = (isElite ? (num * 5L) : num);
		base.NetworkmaxHp = (base.Networkhp = ConstDefine.ClampMaxHp(Util.InitServerEnemyEntries(num, this.enemyEntriesTypes, flag)));
		float num2 = roleAttribute.shiled;
		if (isElite)
		{
			num2 += (float)enemySpawnTime.eliteShield;
		}
		if (num2 > 0f)
		{
			base.ServerSetShield(ConstDefine.ClampBattleValue((double)((float)this.maxHp * num2)));
		}
		else
		{
			base.ServerSetShield(0L);
		}
		if (this.roleModeBase == null)
		{
			RoleModeBase component = AssetManager.LoadPrefab("Prefabs/Enemy_" + roleMode, null, true).GetComponent<RoleModeBase>();
			this.InitRoleModeBase(component);
		}
		base.ServerUpdateStateNoRpc((this.roleModeBase as EnemyModeBase).isShowPose ? RoleState.ShowPose : RoleState.Idle);
		base.NetworksyncPos = pos;
		this.myTransform.position = pos;
		this.ClientRpcPos(pos, roleTypeValue, authorityIdValue, isElite, enemyType, this.enemyEntriesTypes, enemyCreateType, roleMode);
	}

	// Token: 0x06000AEB RID: 2795 RVA: 0x00038BEC File Offset: 0x00036DEC
	public void InitSummon(long newHp, int newAttackPower, float newAttackSpeed, float summonDeadTimeValue, EnemyEntriesType[] enemyEntriesTypesValue, long shieldValue, long curHp, bool isBossValue, int fatherIdValue, RoleType fatherTypeValue, int skillBookIdValue)
	{
		this.enemyEntriesTypes = enemyEntriesTypesValue;
		this.fatherId = fatherIdValue;
		this.fatherType = fatherTypeValue;
		base.NetworkmaxHp = ConstDefine.ClampMaxHp(Util.InitServerEnemyEntries(newHp, enemyEntriesTypesValue, isBossValue));
		if (curHp > 0L)
		{
			base.Networkhp = ((curHp > this.maxHp) ? this.maxHp : curHp);
		}
		else
		{
			base.Networkhp = this.maxHp;
		}
		if (shieldValue > 0L)
		{
			base.ServerSetShield(shieldValue);
		}
		else
		{
			base.ServerSetShield(0L);
		}
		this.RpcSummonAttackLevel(newAttackPower, newAttackSpeed, summonDeadTimeValue, enemyEntriesTypesValue, fatherIdValue, fatherTypeValue, skillBookIdValue);
	}

	// Token: 0x06000AEC RID: 2796 RVA: 0x00038C80 File Offset: 0x00036E80
	[ClientRpc]
	private void RpcSummonAttackLevel(int newAttackPower, float newAttackSpeed, float summonDeadTimeValue, EnemyEntriesType[] enemyEntriesTypesValue, int fatherIdValue, RoleType fatherTypeValue, int skillBookIdValue)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteInt(newAttackPower);
		writer.WriteFloat(newAttackSpeed);
		writer.WriteFloat(summonDeadTimeValue);
		Mirror.GeneratedNetworkCode._Write_EnemyEntriesType[](writer, enemyEntriesTypesValue);
		writer.WriteInt(fatherIdValue);
		Mirror.GeneratedNetworkCode._Write_RoleType(writer, fatherTypeValue);
		writer.WriteInt(skillBookIdValue);
		this.SendRPCInternal(typeof(EnemyBase), "RpcSummonAttackLevel", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000AED RID: 2797 RVA: 0x00038CFC File Offset: 0x00036EFC
	[ClientRpc]
	private void ClientRpcPos(Vector3 pos, RoleType roleTypeValue, uint authorityIdValue, bool isEliteValue, EnemyType enemyTypeValue, EnemyEntriesType[] enemyEntriesTypesValue, EnemyCreateType enemyCreateTypeValue, string roleMode)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteVector3(pos);
		Mirror.GeneratedNetworkCode._Write_RoleType(writer, roleTypeValue);
		writer.WriteUInt(authorityIdValue);
		writer.WriteBool(isEliteValue);
		Mirror.GeneratedNetworkCode._Write_EnemyType(writer, enemyTypeValue);
		Mirror.GeneratedNetworkCode._Write_EnemyEntriesType[](writer, enemyEntriesTypesValue);
		Mirror.GeneratedNetworkCode._Write_EnemyCreateType(writer, enemyCreateTypeValue);
		writer.WriteString(roleMode);
		this.SendRPCInternal(typeof(EnemyBase), "ClientRpcPos", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000AEE RID: 2798 RVA: 0x00038D84 File Offset: 0x00036F84
	protected virtual void OnClientInitEnemy()
	{
		if (this.enemyModeBase.isOpenSkillAI && base.hasAuthority)
		{
			int num = this.enemyModeBase.aiAttackChecks.Length;
			for (int i = 0; i < num; i++)
			{
				AIAttackCheck aiattackCheck = this.enemyModeBase.aiAttackChecks[i];
				if (aiattackCheck.initCdMax > 0f)
				{
					this.SetAttackCd(i, Mathf.Approximately(aiattackCheck.initCdMax, aiattackCheck.initCdMin) ? aiattackCheck.initCdMax : Random.Range(aiattackCheck.initCdMin, aiattackCheck.initCdMax));
				}
				else
				{
					this.SetAttackCd(i, 0f);
				}
			}
		}
		this.enemyModeBase.OnClientInitEnemy();
	}

	// Token: 0x06000AEF RID: 2799 RVA: 0x00038E30 File Offset: 0x00037030
	protected override void OnStartDead()
	{
		base.OnStartDead();
		if (this.checkCollider != null)
		{
			this.checkCollider.enabled = false;
		}
		Game.EnemyManagerClient.DeleteAgent(this);
		if (this.enemyModeBase != null && this.enemyModeBase.isShowPose && !this.isAutoDead)
		{
			Game.EffectManager.PlayEffect(EffectDefine.SummonEffect, 2f, this.myTransform.position, 1.25f + this.roleModeBase.addRange);
		}
		if (GameHelperClient.localPlayer.nearEnemyDeadEvent != null)
		{
			GameHelperClient.localPlayer.nearEnemyDeadEvent(this);
		}
	}

	// Token: 0x06000AF0 RID: 2800 RVA: 0x00038ED8 File Offset: 0x000370D8
	[Command]
	public void CmdAutoDead()
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		base.SendCommandInternal(typeof(EnemyBase), "CmdAutoDead", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000AF1 RID: 2801 RVA: 0x00038F10 File Offset: 0x00037110
	[ClientRpc]
	protected void RpcAutoDead()
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		this.SendRPCInternal(typeof(EnemyBase), "RpcAutoDead", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000AF2 RID: 2802 RVA: 0x00038F45 File Offset: 0x00037145
	protected void RpcDeadItem(ItemStruct[] items)
	{
		this.ApplyDropAuthority(items);
		this.ClientRpcDeadItem(items);
	}

	// Token: 0x06000AF3 RID: 2803 RVA: 0x00038F58 File Offset: 0x00037158
	private void ApplyDropAuthority(ItemStruct[] items)
	{
		if (items == null)
		{
			return;
		}
		uint dropAuthorityId = this.GetDropAuthorityId();
		for (int i = 0; i < items.Length; i++)
		{
			if (items[i] != null)
			{
				items[i].authorityId = dropAuthorityId;
			}
		}
	}

	// Token: 0x06000AF4 RID: 2804 RVA: 0x00038F8C File Offset: 0x0003718C
	private uint GetDropAuthorityId()
	{
		if (this.authorityId != 0U && Game.PlayerManagerClient.clientPlayerDic.ContainsKey(this.authorityId))
		{
			return this.authorityId;
		}
		PlayerBase connectionPlayer = this.GetConnectionPlayer();
		if (!(connectionPlayer != null))
		{
			return 0U;
		}
		return connectionPlayer.netId;
	}

	// Token: 0x06000AF5 RID: 2805 RVA: 0x00038FD8 File Offset: 0x000371D8
	[ClientRpc]
	private void ClientRpcDeadItem(ItemStruct[] items)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_ItemStruct[](writer, items);
		this.SendRPCInternal(typeof(EnemyBase), "ClientRpcDeadItem", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000AF6 RID: 2806 RVA: 0x00039017 File Offset: 0x00037217
	public override bool IsShowName()
	{
		return this.isBoss;
	}

	// Token: 0x06000AF7 RID: 2807 RVA: 0x00039020 File Offset: 0x00037220
	private void UpdateAttackCd()
	{
		if (base.hasAuthority && this.attackCdList != null)
		{
			int num = this.attackCdList.Length;
			for (int i = 0; i < num; i++)
			{
				if (this.attackCdList[i] > 0f)
				{
					this.attackCdList[i] -= Time.deltaTime;
				}
			}
		}
	}

	// Token: 0x06000AF8 RID: 2808 RVA: 0x00039076 File Offset: 0x00037276
	public void SetAttackCd(int checkAIIndex, float attackCd)
	{
		if (this.attackCdList == null)
		{
			this.attackCdList = new float[this.enemyModeBase.aiAttackChecks.Length];
		}
		this.attackCdList[checkAIIndex] = attackCd;
	}

	// Token: 0x06000AF9 RID: 2809 RVA: 0x000390A4 File Offset: 0x000372A4
	protected bool StartAIAttack()
	{
		RoleBase trackRoleBase = this.trackRoleBase;
		int num = this.enemyModeBase.aiAttackChecks.Length;
		float distanceV = base.GetDistanceV2(trackRoleBase.MyTransform.position);
		for (int i = 0; i < num; i++)
		{
			if (this.attackCdList == null || this.attackCdList[i] <= 0f)
			{
				AIAttackCheck aiattackCheck = this.enemyModeBase.aiAttackChecks[i];
				bool flag;
				if (aiattackCheck.isOnlyPlayer)
				{
					this.GetTrackRole(true, 17f, false, true);
					flag = (this.trackRoleBase.roleType == RoleType.Player && distanceV > aiattackCheck.minDinstance && distanceV < aiattackCheck.checkDistance + this.trackRoleBase.RoleModeBase.addRange);
					if (!flag && trackRoleBase != this.trackRoleBase)
					{
						this.trackRoleBase = trackRoleBase;
					}
				}
				else
				{
					flag = (distanceV > aiattackCheck.minDinstance && distanceV < aiattackCheck.checkDistance + trackRoleBase.RoleModeBase.addRange);
				}
				if (flag)
				{
					int num2 = Mathf.FloorToInt(Random.value * (float)aiattackCheck.attackStateList.Length);
					if (aiattackCheck.attackCd > 0f)
					{
						this.SetAttackCd(i, aiattackCheck.attackCd);
					}
					base.UpdateRoleState(aiattackCheck.attackStateList[num2]);
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000AFA RID: 2810 RVA: 0x000391F2 File Offset: 0x000373F2
	protected virtual void UpdateSkill1()
	{
		RoleModeBase roleModeBase = this.roleModeBase;
		if (roleModeBase == null)
		{
			return;
		}
		roleModeBase.UpdateSkill1();
	}

	// Token: 0x06000AFB RID: 2811 RVA: 0x00039204 File Offset: 0x00037404
	protected virtual void UpdateSkill2()
	{
		RoleModeBase roleModeBase = this.roleModeBase;
		if (roleModeBase == null)
		{
			return;
		}
		roleModeBase.UpdateSkill2();
	}

	// Token: 0x06000AFC RID: 2812 RVA: 0x00039216 File Offset: 0x00037416
	protected virtual void UpdateSkill3()
	{
		RoleModeBase roleModeBase = this.roleModeBase;
		if (roleModeBase == null)
		{
			return;
		}
		roleModeBase.UpdateSkill3();
	}

	// Token: 0x06000AFD RID: 2813 RVA: 0x00039228 File Offset: 0x00037428
	[Command]
	public void CmdPlayTipLine(Vector3 pos, float rotation)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteVector3(pos);
		writer.WriteFloat(rotation);
		base.SendCommandInternal(typeof(EnemyBase), "CmdPlayTipLine", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000AFE RID: 2814 RVA: 0x00039274 File Offset: 0x00037474
	[ClientRpc]
	private void RpcPlayTipLine(Vector3 pos, float rotation)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteVector3(pos);
		writer.WriteFloat(rotation);
		this.SendRPCInternal(typeof(EnemyBase), "RpcPlayTipLine", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000AFF RID: 2815 RVA: 0x000392C0 File Offset: 0x000374C0
	[Command]
	public void CmdPlayTipSector(Vector3 pos, float range, float lifeTime)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteVector3(pos);
		writer.WriteFloat(range);
		writer.WriteFloat(lifeTime);
		base.SendCommandInternal(typeof(EnemyBase), "CmdPlayTipSector", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000B00 RID: 2816 RVA: 0x00039314 File Offset: 0x00037514
	[ClientRpc]
	private void RpcPlayTipSector(Vector3 pos, float range, float lifeTime)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteVector3(pos);
		writer.WriteFloat(range);
		writer.WriteFloat(lifeTime);
		this.SendRPCInternal(typeof(EnemyBase), "RpcPlayTipSector", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000B01 RID: 2817 RVA: 0x00039367 File Offset: 0x00037567
	protected override void OnStartXuanYun()
	{
		base.OnStartXuanYun();
		if (this.isBoss)
		{
			this.canXuanYunLastTime = 5f;
		}
	}

	// Token: 0x06000B02 RID: 2818 RVA: 0x00039382 File Offset: 0x00037582
	protected override void OnExitXuanYun()
	{
		base.OnExitXuanYun();
		if (this.isBoss)
		{
			this.canXuanYunLastTime = 5f;
		}
	}

	// Token: 0x06000B03 RID: 2819 RVA: 0x0003939D File Offset: 0x0003759D
	public void SetSummonDeadTime(float value)
	{
		this.summonDeadTime = value;
	}

	// Token: 0x06000B04 RID: 2820 RVA: 0x000393A8 File Offset: 0x000375A8
	[Command]
	public void CmdAddBrotatoWeapon(BrotatoWeaponType brotatoWeaponType, uint skillId, float[] skillValues, int grade)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_BrotatoWeaponType(writer, brotatoWeaponType);
		writer.WriteUInt(skillId);
		Mirror.GeneratedNetworkCode._Write_System.Single[](writer, skillValues);
		writer.WriteInt(grade);
		base.SendCommandInternal(typeof(EnemyBase), "CmdAddBrotatoWeapon", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000B05 RID: 2821 RVA: 0x00039408 File Offset: 0x00037608
	[ClientRpc]
	private void RpcAddBrotatoWeapon(BrotatoWeaponType brotatoWeaponType, uint skillId, float[] skillValues, int grade)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_BrotatoWeaponType(writer, brotatoWeaponType);
		writer.WriteUInt(skillId);
		Mirror.GeneratedNetworkCode._Write_System.Single[](writer, skillValues);
		writer.WriteInt(grade);
		this.SendRPCInternal(typeof(EnemyBase), "RpcAddBrotatoWeapon", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000B06 RID: 2822 RVA: 0x00039465 File Offset: 0x00037665
	public void ShowTip()
	{
		Util.ShowTipsNoLanguage(string.Format(Game.Language.Get("出现提示", ""), string.Format(ColorDefine.NormalColor, this.roleName)));
	}

	// Token: 0x06000B08 RID: 2824 RVA: 0x00002D1D File Offset: 0x00000F1D
	private void MirrorProcessed()
	{
	}

	// Token: 0x06000B09 RID: 2825 RVA: 0x000394A8 File Offset: 0x000376A8
	protected void UserCode_RpcSummonAttackLevel(int newAttackPower, float newAttackSpeed, float summonDeadTimeValue, EnemyEntriesType[] enemyEntriesTypesValue, int fatherIdValue, RoleType fatherTypeValue, int skillBookIdValue)
	{
		this.fatherId = fatherIdValue;
		this.fatherType = fatherTypeValue;
		base.mAttackPower = newAttackPower;
		this.attackSpeed = newAttackSpeed;
		this.summonDeadTime = summonDeadTimeValue;
		this.needClearSummon = false;
		this.enemyEntriesTypes = enemyEntriesTypesValue;
		this.skillBookId = skillBookIdValue;
		Util.InitClientEnemyEntries(this);
	}

	// Token: 0x06000B0A RID: 2826 RVA: 0x000394F8 File Offset: 0x000376F8
	protected static void InvokeUserCode_RpcSummonAttackLevel(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcSummonAttackLevel called on server.");
			return;
		}
		((EnemyBase)obj).UserCode_RpcSummonAttackLevel(reader.ReadInt(), reader.ReadFloat(), reader.ReadFloat(), Mirror.GeneratedNetworkCode._Read_EnemyEntriesType[](reader), reader.ReadInt(), Mirror.GeneratedNetworkCode._Read_RoleType(reader), reader.ReadInt());
	}

	// Token: 0x06000B0B RID: 2827 RVA: 0x00039554 File Offset: 0x00037754
	protected void UserCode_ClientRpcPos(Vector3 pos, RoleType roleTypeValue, uint authorityIdValue, bool isEliteValue, EnemyType enemyTypeValue, EnemyEntriesType[] enemyEntriesTypesValue, EnemyCreateType enemyCreateTypeValue, string roleMode)
	{
		this.fatherId = -1;
		base.ClearAllBuff(false);
		this.enemyType = enemyTypeValue;
		RoleAttribute roleAttribute = Game.GameData.HeroAttributeDic[this.enemyType.ToString()];
		if (this.roleModeBase == null)
		{
			RoleModeBase component = AssetManager.LoadPrefab("Prefabs/Enemy_" + roleMode, null, true).GetComponent<RoleModeBase>();
			this.InitRoleModeBase(component);
		}
		if (this.checkCollider != null)
		{
			this.checkCollider.enabled = true;
		}
		this.isElite = isEliteValue;
		this.authorityId = authorityIdValue;
		this.isAutoDead = false;
		this.roleType = roleTypeValue;
		this.myTransform.position = pos;
		base.NetworksyncPos = pos;
		this.animTransform.localPosition = Vector3.zero;
		this.animTransform.localScale = ((isEliteValue || this.enemyType == EnemyType.Goblin_HeartMonster_0) ? (this.roleModeBase.baseModeScale * 1.5f) : this.roleModeBase.baseModeScale);
		if (this.roleModeBase.materialList != null && this.roleModeBase.materialList.Length != 0)
		{
			if (this.isElite)
			{
				if (enemyEntriesTypesValue != null && enemyEntriesTypesValue.Length != 0)
				{
					EnemyEntriesData enemyEntriesData = Game.GameData.EnemyEntriesDic[enemyEntriesTypesValue[0]];
					base.ChangeMaterial(enemyEntriesData.skin);
				}
				else
				{
					base.ChangeMaterial(Random.Range(2, 6));
				}
			}
			else
			{
				base.ChangeMaterial(roleAttribute.materialIndex);
			}
		}
		this.enemyEntriesTypes = enemyEntriesTypesValue;
		this.enemyCreateType = enemyCreateTypeValue;
		if (enemyCreateTypeValue == EnemyCreateType.Summon && this.roleModeBase != null)
		{
			if (roleTypeValue == RoleType.Summon)
			{
				this.roleModeBase.gameObject.layer = LayerUtil.DefaultLayer;
			}
			else
			{
				this.roleModeBase.gameObject.layer = LayerUtil.EnemyLayer;
			}
		}
		this.InitRole(roleTypeValue, roleAttribute, isEliteValue ? 5 : 1, this.enemyCreateType);
		if (GameHelperClient.isHost)
		{
			AnalyticsManager analytics = Game.Analytics;
			if (analytics != null)
			{
				analytics.RecordServerEnemySpawn(this.enemyType, GameHelperClient.WaveNum, this.isBoss, this.isElite);
			}
		}
		this.trackRoleBase = null;
		if (!base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(true);
		}
		this.OnClientInitEnemy();
		base.ShowMode();
		if (roleTypeValue == RoleType.Enemy)
		{
			Game.EnemyManagerClient.AddEnemy(this);
			float moveAngle = base.GetMoveAngle(new UnityEngine.Vector2(-this.myTransform.position.x, -this.myTransform.position.z));
			base.SetRotationY(moveAngle);
		}
		else
		{
			Game.PlayerManagerClient.AddPlayer(this);
		}
		if (this.enemyModeBase.isShowPose)
		{
			Game.EffectManager.PlayEffect(EffectDefine.SummonEffect, 2f, pos, 1.25f + this.roleModeBase.addRange);
		}
		else
		{
			this.timer = this.enemyModeBase.enemyAttackOffset;
		}
		this.GetTrackRole(true, 17f, true, false);
		Util.InitClientEnemyEntries(this);
	}

	// Token: 0x06000B0C RID: 2828 RVA: 0x00039838 File Offset: 0x00037A38
	protected static void InvokeUserCode_ClientRpcPos(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC ClientRpcPos called on server.");
			return;
		}
		((EnemyBase)obj).UserCode_ClientRpcPos(reader.ReadVector3(), Mirror.GeneratedNetworkCode._Read_RoleType(reader), reader.ReadUInt(), reader.ReadBool(), Mirror.GeneratedNetworkCode._Read_EnemyType(reader), Mirror.GeneratedNetworkCode._Read_EnemyEntriesType[](reader), Mirror.GeneratedNetworkCode._Read_EnemyCreateType(reader), reader.ReadString());
	}

	// Token: 0x06000B0D RID: 2829 RVA: 0x00039896 File Offset: 0x00037A96
	protected void UserCode_CmdAutoDead()
	{
		this.isAutoDead = true;
		this.RpcAutoDead();
		base.ServerUpdateState(RoleState.Dead);
	}

	// Token: 0x06000B0E RID: 2830 RVA: 0x000398AC File Offset: 0x00037AAC
	protected static void InvokeUserCode_CmdAutoDead(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdAutoDead called on client.");
			return;
		}
		((EnemyBase)obj).UserCode_CmdAutoDead();
	}

	// Token: 0x06000B0F RID: 2831 RVA: 0x000398D0 File Offset: 0x00037AD0
	protected void UserCode_RpcAutoDead()
	{
		this.isAutoDead = true;
		if (this.roleModeBase != null)
		{
			Game.EffectManager.PlayEffect(EffectDefine.SummonEffect, 2f, this.myTransform.position, 1.25f + this.roleModeBase.addRange);
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000B10 RID: 2832 RVA: 0x0003992F File Offset: 0x00037B2F
	protected static void InvokeUserCode_RpcAutoDead(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcAutoDead called on server.");
			return;
		}
		((EnemyBase)obj).UserCode_RpcAutoDead();
	}

	// Token: 0x06000B11 RID: 2833 RVA: 0x00039952 File Offset: 0x00037B52
	protected void UserCode_ClientRpcDeadItem(ItemStruct[] items)
	{
		Game.ItemManager.AddItemList(items, this.myTransform.position);
	}

	// Token: 0x06000B12 RID: 2834 RVA: 0x0003996A File Offset: 0x00037B6A
	protected static void InvokeUserCode_ClientRpcDeadItem(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC ClientRpcDeadItem called on server.");
			return;
		}
		((EnemyBase)obj).UserCode_ClientRpcDeadItem(Mirror.GeneratedNetworkCode._Read_ItemStruct[](reader));
	}

	// Token: 0x06000B13 RID: 2835 RVA: 0x00039993 File Offset: 0x00037B93
	protected void UserCode_CmdPlayTipLine(Vector3 pos, float rotation)
	{
		this.RpcPlayTipLine(pos, rotation);
	}

	// Token: 0x06000B14 RID: 2836 RVA: 0x0003999D File Offset: 0x00037B9D
	protected static void InvokeUserCode_CmdPlayTipLine(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdPlayTipLine called on client.");
			return;
		}
		((EnemyBase)obj).UserCode_CmdPlayTipLine(reader.ReadVector3(), reader.ReadFloat());
	}

	// Token: 0x06000B15 RID: 2837 RVA: 0x000399D0 File Offset: 0x00037BD0
	protected void UserCode_RpcPlayTipLine(Vector3 pos, float rotation)
	{
		for (int i = 0; i < 8; i++)
		{
			Game.EffectManager.PlayTipLine(pos, new Vector3(Game.GameData.ActiveSkillDataDic[ActiveSkillEnum.Elder_Wave].range, 1f, Game.GameData.ActiveSkillDataDic[ActiveSkillEnum.Elder_Wave].range * 3.3f), rotation + (float)i * 45f, 1.5f);
		}
	}

	// Token: 0x06000B16 RID: 2838 RVA: 0x00039A46 File Offset: 0x00037C46
	protected static void InvokeUserCode_RpcPlayTipLine(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcPlayTipLine called on server.");
			return;
		}
		((EnemyBase)obj).UserCode_RpcPlayTipLine(reader.ReadVector3(), reader.ReadFloat());
	}

	// Token: 0x06000B17 RID: 2839 RVA: 0x00039A76 File Offset: 0x00037C76
	protected void UserCode_CmdPlayTipSector(Vector3 pos, float range, float lifeTime)
	{
		this.RpcPlayTipSector(pos, range, lifeTime);
	}

	// Token: 0x06000B18 RID: 2840 RVA: 0x00039A81 File Offset: 0x00037C81
	protected static void InvokeUserCode_CmdPlayTipSector(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdPlayTipSector called on client.");
			return;
		}
		((EnemyBase)obj).UserCode_CmdPlayTipSector(reader.ReadVector3(), reader.ReadFloat(), reader.ReadFloat());
	}

	// Token: 0x06000B19 RID: 2841 RVA: 0x00039AB8 File Offset: 0x00037CB8
	protected void UserCode_RpcPlayTipSector(Vector3 pos, float range, float lifeTime)
	{
		Game.EffectManager.PlayTipSector(pos, range * 2f, 0f, 360f, lifeTime, 0f);
	}

	// Token: 0x06000B1A RID: 2842 RVA: 0x00039ADD File Offset: 0x00037CDD
	protected static void InvokeUserCode_RpcPlayTipSector(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcPlayTipSector called on server.");
			return;
		}
		((EnemyBase)obj).UserCode_RpcPlayTipSector(reader.ReadVector3(), reader.ReadFloat(), reader.ReadFloat());
	}

	// Token: 0x06000B1B RID: 2843 RVA: 0x00039B14 File Offset: 0x00037D14
	protected void UserCode_CmdAddBrotatoWeapon(BrotatoWeaponType brotatoWeaponType, uint skillId, float[] skillValues, int grade)
	{
		this.RpcAddBrotatoWeapon(brotatoWeaponType, skillId, skillValues, grade);
	}

	// Token: 0x06000B1C RID: 2844 RVA: 0x00039B21 File Offset: 0x00037D21
	protected static void InvokeUserCode_CmdAddBrotatoWeapon(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdAddBrotatoWeapon called on client.");
			return;
		}
		((EnemyBase)obj).UserCode_CmdAddBrotatoWeapon(Mirror.GeneratedNetworkCode._Read_BrotatoWeaponType(reader), reader.ReadUInt(), Mirror.GeneratedNetworkCode._Read_System.Single[](reader), reader.ReadInt());
	}

	// Token: 0x06000B1D RID: 2845 RVA: 0x00039B5C File Offset: 0x00037D5C
	protected void UserCode_RpcAddBrotatoWeapon(BrotatoWeaponType brotatoWeaponType, uint skillId, float[] skillValues, int grade)
	{
		if (this.roleModeBase != null)
		{
			Enemy_NecromancerMode enemy_NecromancerMode = this.roleModeBase as Enemy_NecromancerMode;
			if (enemy_NecromancerMode != null)
			{
				enemy_NecromancerMode.BrotatoWeaponController.AddBrotatoWeapon(brotatoWeaponType, skillId, this, skillValues, grade);
			}
		}
	}

	// Token: 0x06000B1E RID: 2846 RVA: 0x00039B97 File Offset: 0x00037D97
	protected static void InvokeUserCode_RpcAddBrotatoWeapon(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcAddBrotatoWeapon called on server.");
			return;
		}
		((EnemyBase)obj).UserCode_RpcAddBrotatoWeapon(Mirror.GeneratedNetworkCode._Read_BrotatoWeaponType(reader), reader.ReadUInt(), Mirror.GeneratedNetworkCode._Read_System.Single[](reader), reader.ReadInt());
	}

	// Token: 0x06000B1F RID: 2847 RVA: 0x00039BD4 File Offset: 0x00037DD4
	static EnemyBase()
	{
		RemoteCallHelper.RegisterCommandDelegate(typeof(EnemyBase), "CmdAutoDead", new CmdDelegate(EnemyBase.InvokeUserCode_CmdAutoDead), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(EnemyBase), "CmdPlayTipLine", new CmdDelegate(EnemyBase.InvokeUserCode_CmdPlayTipLine), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(EnemyBase), "CmdPlayTipSector", new CmdDelegate(EnemyBase.InvokeUserCode_CmdPlayTipSector), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(EnemyBase), "CmdAddBrotatoWeapon", new CmdDelegate(EnemyBase.InvokeUserCode_CmdAddBrotatoWeapon), true);
		RemoteCallHelper.RegisterRpcDelegate(typeof(EnemyBase), "RpcSummonAttackLevel", new CmdDelegate(EnemyBase.InvokeUserCode_RpcSummonAttackLevel));
		RemoteCallHelper.RegisterRpcDelegate(typeof(EnemyBase), "ClientRpcPos", new CmdDelegate(EnemyBase.InvokeUserCode_ClientRpcPos));
		RemoteCallHelper.RegisterRpcDelegate(typeof(EnemyBase), "RpcAutoDead", new CmdDelegate(EnemyBase.InvokeUserCode_RpcAutoDead));
		RemoteCallHelper.RegisterRpcDelegate(typeof(EnemyBase), "ClientRpcDeadItem", new CmdDelegate(EnemyBase.InvokeUserCode_ClientRpcDeadItem));
		RemoteCallHelper.RegisterRpcDelegate(typeof(EnemyBase), "RpcPlayTipLine", new CmdDelegate(EnemyBase.InvokeUserCode_RpcPlayTipLine));
		RemoteCallHelper.RegisterRpcDelegate(typeof(EnemyBase), "RpcPlayTipSector", new CmdDelegate(EnemyBase.InvokeUserCode_RpcPlayTipSector));
		RemoteCallHelper.RegisterRpcDelegate(typeof(EnemyBase), "RpcAddBrotatoWeapon", new CmdDelegate(EnemyBase.InvokeUserCode_RpcAddBrotatoWeapon));
	}

	// Token: 0x04000BFC RID: 3068
	private Random m_random = new Random();

	// Token: 0x04000BFD RID: 3069
	protected Collider checkCollider;

	// Token: 0x04000BFE RID: 3070
	[HideInInspector]
	public EnemyType enemyType;

	// Token: 0x04000BFF RID: 3071
	private bool isAutoDead;

	// Token: 0x04000C00 RID: 3072
	[FormerlySerializedAs("elite")]
	[HideInInspector]
	public bool isElite;

	// Token: 0x04000C01 RID: 3073
	[HideInInspector]
	public bool isBoss;

	// Token: 0x04000C02 RID: 3074
	[HideInInspector]
	public float nextGetTrackTime;

	// Token: 0x04000C03 RID: 3075
	private float summonDeadTime;

	// Token: 0x04000C04 RID: 3076
	private bool needClearSummon;

	// Token: 0x04000C05 RID: 3077
	private float[] attackCdList;

	// Token: 0x04000C06 RID: 3078
	private float waitTime;

	// Token: 0x04000C07 RID: 3079
	private Vector3 lastPos;

	// Token: 0x04000C08 RID: 3080
	protected EnemyEntriesType[] enemyEntriesTypes;

	// Token: 0x04000C09 RID: 3081
	private EnemyCreateType enemyCreateType;

	// Token: 0x04000C0A RID: 3082
	[HideInInspector]
	public EnemyModeBase enemyModeBase;
}

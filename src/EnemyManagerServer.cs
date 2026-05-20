using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Mirror;
using UnityEngine;

// Token: 0x020002BB RID: 699
public class EnemyManagerServer
{
	// Token: 0x06001088 RID: 4232 RVA: 0x0005C9A8 File Offset: 0x0005ABA8
	public EnemyManagerServer()
	{
		int length = Enum.GetValues(typeof(EnemyEntriesType)).Length;
		EnemyManagerServer.randomEnemyEntriesType = new EnemyEntriesType[Enum.GetValues(typeof(EnemyEntriesType)).Length];
		for (int i = 0; i < length; i++)
		{
			EnemyManagerServer.randomEnemyEntriesType[i] = (EnemyEntriesType)i;
		}
	}

	// Token: 0x06001089 RID: 4233 RVA: 0x0005CA10 File Offset: 0x0005AC10
	public void OnGameStart()
	{
		this.spawnConfig = GameHelperClient.spawnConfig;
		this.isStart = true;
		this.createElite = 30;
		this.spawnTime = 0f;
		this.gameTime = 0f;
		this.buyItemMonsterDic.Clear();
		SOSpawnConfig.EnemySpawnTime enemySpawnTime = this.spawnConfig.enemySpawnData[GameHelperClient.WaveNum];
		if (enemySpawnTime.bossNum > 0)
		{
			this.createBossTime = 3;
			this.createBoss = new int[enemySpawnTime.bossNum];
			EnemyType[] enemyType = this.spawnConfig.bossSpawnData[GameHelperClient.LevelIndex].enemyType;
			for (int i = 0; i < enemyType.Length; i++)
			{
				int num = Random.Range(0, enemyType.Length);
				ref EnemyType ptr = ref enemyType[num];
				EnemyType[] array = enemyType;
				int num2 = i;
				EnemyType enemyType2 = enemyType[i];
				EnemyType enemyType3 = enemyType[num];
				ptr = enemyType2;
				array[num2] = enemyType3;
			}
			for (int j = 0; j < enemySpawnTime.bossNum; j++)
			{
				this.createBoss[j] = (int)enemyType[j];
			}
			(NetworkManager.singleton as MyServerNetworkManager).ServerSendAllPlayer(new ClientNetMessage
			{
				clientNetOperation = ClientNetOperation.EnemyEnterTip,
				datas = this.createBoss
			});
		}
		else
		{
			this.createBossTime = 0;
			this.createBoss = null;
		}
		if (this.chestCreateData.isInit)
		{
			this.chestCreateData.isInit = true;
			this.chestCreateData.curChance = 0f;
		}
		if (this.chestCreateData.createNum >= GameHelperClient.spawnConfig.chestCreateData.maxCreatNum)
		{
			this.chestCreateData.isCanSpawn = false;
			return;
		}
		if (this.chestCreateData.cdTime > 0)
		{
			this.chestCreateData.cdTime = this.chestCreateData.cdTime - 1;
			this.chestCreateData.isCanSpawn = false;
			return;
		}
		this.chestCreateData.isCanSpawn = true;
		if (!GameHelperClient.spawnConfig.chestCreateData.bossLevelCreate && enemySpawnTime.bossNum > 0)
		{
			this.chestCreateData.isCanSpawn = false;
		}
		if (!GameHelperClient.spawnConfig.chestCreateData.challengeLevelCreate)
		{
			RoguelikeEventType[] eventList = enemySpawnTime.eventList;
			if (eventList != null && eventList.Length != 0)
			{
				for (int k = 0; k < eventList.Length; k++)
				{
					if (eventList[k] == RoguelikeEventType.Challenge)
					{
						this.chestCreateData.isCanSpawn = false;
						break;
					}
				}
			}
		}
		if (this.chestCreateData.isCanSpawn)
		{
			this.chestCreateData.createTime = Random.Range(GameHelperClient.spawnConfig.chestCreateData.createMinTime, GameHelperClient.spawnConfig.chestCreateData.createMaxTime);
		}
	}

	// Token: 0x0600108A RID: 4234 RVA: 0x0005CC74 File Offset: 0x0005AE74
	public void UpdateEvent()
	{
		if (!this.isStart || GameHelperClient.isReady || GameHelperClient.isGameOver)
		{
			return;
		}
		this.gameTime += Time.deltaTime;
		for (int i = this.buyItemMonsterDic.Count - 1; i >= 0; i--)
		{
			EnemyManagerServer.BuyItemMonsterData buyItemMonsterData = this.buyItemMonsterDic[i];
			if (this.gameTime > buyItemMonsterData.createTime * (float)buyItemMonsterData.createNum)
			{
				buyItemMonsterData.createNum++;
				SOSpawnConfig.EnemySpawnTime enemySpawnTime = this.spawnConfig.enemySpawnData[GameHelperClient.WaveNum];
				EnemyType enemyType = enemySpawnTime.enemyType[Random.Range(0, enemySpawnTime.enemyType.Length)];
				Vector3 spawnPos = this.spawnConfig.enemySpawnPoint[(int)(buyItemMonsterData.addPlayerId - 1U)];
				this.AddEnemy(enemyType, buyItemMonsterData.addPlayerId, false, EnemyCreateType.Normal, spawnPos);
				if (buyItemMonsterData.createNum == buyItemMonsterData.addMonsterNum)
				{
					this.buyItemMonsterDic.RemoveAt(i);
				}
			}
		}
		bool isElite = false;
		SOSpawnConfig.EnemySpawnTime enemySpawnTime2 = this.spawnConfig.enemySpawnData[GameHelperClient.WaveNum];
		int num = this.spawnConfig.isTeamWork ? 1 : NetworkServer.connections.Count;
		if (this.gameTime > this.spawnTime)
		{
			this.spawnTime += (float)enemySpawnTime2.spawnTime / (float)enemySpawnTime2.newCreateNum;
			if (this.gameTime > (float)this.createElite)
			{
				isElite = true;
				this.createElite += GameHelperClient.gameConfig.EliteCreateTime;
			}
			else if (Random.value <= GameHelperClient.gameConfig.EliteProbability * (1f + GameHelperClient.EliteProbabilityAdd))
			{
				isElite = true;
			}
			EnemyType enemyType2 = enemySpawnTime2.enemyType[Random.Range(0, enemySpawnTime2.enemyType.Length)];
			int j = 0;
			int num2 = num;
			while (j < num2)
			{
				uint netId = Game.PlayerManagerClient.clientPlayerList[j].netId;
				Vector3 spawnPos2 = this.spawnConfig.enemySpawnPoint[(int)(netId - 1U)];
				this.AddEnemy(enemyType2, netId, isElite, EnemyCreateType.Normal, spawnPos2);
				j++;
			}
		}
		if (this.createBossTime > 0 && this.gameTime > (float)this.createBossTime)
		{
			this.createBossTime = 0;
			for (int k = 0; k < this.createBoss.Length; k++)
			{
				int l = 0;
				int num3 = num;
				while (l < num3)
				{
					uint netId2 = Game.PlayerManagerClient.clientPlayerList[l].netId;
					Vector3 spawnPos3 = this.spawnConfig.enemySpawnPoint[(int)(netId2 - 1U)];
					this.AddEnemy((EnemyType)this.createBoss[k], netId2, false, EnemyCreateType.ChallengeAndBOSS, spawnPos3);
					l++;
				}
			}
		}
		if (this.chestCreateData.isCanSpawn && this.gameTime > (float)this.chestCreateData.createTime)
		{
			this.chestCreateData.isCanSpawn = false;
			if (Random.value < this.chestCreateData.curChance)
			{
				this.chestCreateData.curChance = GameHelperClient.spawnConfig.chestCreateData.startChance;
				this.chestCreateData.cdTime = GameHelperClient.spawnConfig.chestCreateData.createCdTime;
				this.chestCreateData.createNum = this.chestCreateData.createNum + 1;
				int m = 0;
				int num4 = num;
				while (m < num4)
				{
					uint netId3 = Game.PlayerManagerClient.clientPlayerList[m].netId;
					Vector3 spawnPos4 = this.spawnConfig.enemySpawnPoint[(int)(netId3 - 1U)];
					this.AddEnemy(EnemyType.Chest, netId3, true, EnemyCreateType.Chest, spawnPos4);
					m++;
				}
				return;
			}
			this.chestCreateData.curChance = this.chestCreateData.curChance + GameHelperClient.spawnConfig.chestCreateData.waveAddChance;
		}
	}

	// Token: 0x0600108B RID: 4235 RVA: 0x0005D014 File Offset: 0x0005B214
	private Task AddEnemy(EnemyType enemyType, uint playerId, bool isElite, EnemyCreateType enemyCreateType, Vector3 spawnPos)
	{
		EnemyManagerServer.<AddEnemy>d__18 <AddEnemy>d__;
		<AddEnemy>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
		<AddEnemy>d__.enemyType = enemyType;
		<AddEnemy>d__.playerId = playerId;
		<AddEnemy>d__.isElite = isElite;
		<AddEnemy>d__.enemyCreateType = enemyCreateType;
		<AddEnemy>d__.spawnPos = spawnPos;
		<AddEnemy>d__.<>1__state = -1;
		<AddEnemy>d__.<>t__builder.Start<EnemyManagerServer.<AddEnemy>d__18>(ref <AddEnemy>d__);
		return <AddEnemy>d__.<>t__builder.Task;
	}

	// Token: 0x0600108C RID: 4236 RVA: 0x0005D079 File Offset: 0x0005B279
	public void OnGameOver()
	{
		this.isStart = false;
	}

	// Token: 0x0600108D RID: 4237 RVA: 0x0005D084 File Offset: 0x0005B284
	public static EnemyEntriesType[] GetEliteEntriesTypes()
	{
		int levelIndex = GameHelperClient.LevelIndex;
		return EnemyManagerServer.GetRandomEntriesTypes(GameHelperClient.spawnConfig.EliteEnemyEntries[levelIndex].chanceData);
	}

	// Token: 0x0600108E RID: 4238 RVA: 0x0005D0B4 File Offset: 0x0005B2B4
	public static EnemyEntriesType[] GetBossEntriesTypes()
	{
		int levelIndex = GameHelperClient.LevelIndex;
		return EnemyManagerServer.GetRandomEntriesTypes(GameHelperClient.spawnConfig.BossEnemyEntries[levelIndex].chanceData);
	}

	// Token: 0x0600108F RID: 4239 RVA: 0x0005D0E4 File Offset: 0x0005B2E4
	private static EnemyEntriesType[] GetRandomEntriesTypes(float[] entriesProbability)
	{
		float value = Random.value;
		int num = entriesProbability.Length;
		float num2 = 0f;
		int num3 = 0;
		for (int i = 0; i < num; i++)
		{
			num2 += entriesProbability[i];
			if (value < num2)
			{
				num3 = i;
				break;
			}
		}
		if (num3 == 0)
		{
			return null;
		}
		EnemyEntriesType[] array = new EnemyEntriesType[num3];
		int num4 = EnemyManagerServer.randomEnemyEntriesType.Length;
		for (int j = 0; j < num4; j++)
		{
			int num5 = Random.Range(0, num4);
			ref EnemyEntriesType ptr = ref EnemyManagerServer.randomEnemyEntriesType[j];
			EnemyEntriesType[] array2 = EnemyManagerServer.randomEnemyEntriesType;
			int num6 = num5;
			EnemyEntriesType enemyEntriesType = EnemyManagerServer.randomEnemyEntriesType[num5];
			EnemyEntriesType enemyEntriesType2 = EnemyManagerServer.randomEnemyEntriesType[j];
			ptr = enemyEntriesType;
			array2[num6] = enemyEntriesType2;
		}
		for (int k = 0; k < num3; k++)
		{
			array[k] = EnemyManagerServer.randomEnemyEntriesType[k];
		}
		return array;
	}

	// Token: 0x06001090 RID: 4240 RVA: 0x0005D1AC File Offset: 0x0005B3AC
	public void OnAddBuyItemMonster(uint addPlayerId)
	{
		EnemyManagerServer.BuyItemMonsterData buyItemMonsterData = new EnemyManagerServer.BuyItemMonsterData();
		buyItemMonsterData.addPlayerId = addPlayerId;
		buyItemMonsterData.addMonsterNum = 100;
		buyItemMonsterData.createTime = Mathf.Min(100f, GameHelperClient.CountDownTime) / (float)buyItemMonsterData.addMonsterNum;
		this.buyItemMonsterDic.Add(buyItemMonsterData);
	}

	// Token: 0x06001091 RID: 4241 RVA: 0x0005D079 File Offset: 0x0005B279
	public void TestCloseEnemyCreate()
	{
		this.isStart = false;
	}

	// Token: 0x06001092 RID: 4242 RVA: 0x0005D1F7 File Offset: 0x0005B3F7
	public bool CheckLevelUp()
	{
		if (this.createBoss != null && this.createBoss.Length != 0)
		{
			GameHelperClient.LevelIndex++;
			return true;
		}
		return false;
	}

	// Token: 0x04000E74 RID: 3700
	private bool isStart;

	// Token: 0x04000E75 RID: 3701
	private float spawnTime;

	// Token: 0x04000E76 RID: 3702
	private SOSpawnConfig spawnConfig;

	// Token: 0x04000E77 RID: 3703
	private int createElite;

	// Token: 0x04000E78 RID: 3704
	private EnemyManagerServer.SpawnData[] spawnDataList;

	// Token: 0x04000E79 RID: 3705
	private float gameTime;

	// Token: 0x04000E7A RID: 3706
	private bool isShowSpawnBossTip;

	// Token: 0x04000E7B RID: 3707
	private List<EnemyManagerServer.BuyItemMonsterData> buyItemMonsterDic = new List<EnemyManagerServer.BuyItemMonsterData>();

	// Token: 0x04000E7C RID: 3708
	public const int ShowEnemyTipTime = 3;

	// Token: 0x04000E7D RID: 3709
	private static EnemyEntriesType[] randomEnemyEntriesType;

	// Token: 0x04000E7E RID: 3710
	private int createBossTime;

	// Token: 0x04000E7F RID: 3711
	private int[] createBoss;

	// Token: 0x04000E80 RID: 3712
	private EnemyManagerServer.ChestCreateData chestCreateData;

	// Token: 0x020002BC RID: 700
	private struct ChestCreateData
	{
		// Token: 0x04000E81 RID: 3713
		public float curChance;

		// Token: 0x04000E82 RID: 3714
		public int createTime;

		// Token: 0x04000E83 RID: 3715
		public int createNum;

		// Token: 0x04000E84 RID: 3716
		public bool isCanSpawn;

		// Token: 0x04000E85 RID: 3717
		public bool isInit;

		// Token: 0x04000E86 RID: 3718
		public int cdTime;
	}

	// Token: 0x020002BD RID: 701
	private class SpawnData
	{
		// Token: 0x04000E87 RID: 3719
		public bool isSpawnEnd;

		// Token: 0x04000E88 RID: 3720
		public int spawnIndex;

		// Token: 0x04000E89 RID: 3721
		public float spawnTimer;
	}

	// Token: 0x020002BE RID: 702
	private class BuyItemMonsterData
	{
		// Token: 0x04000E8A RID: 3722
		public uint addPlayerId;

		// Token: 0x04000E8B RID: 3723
		public int addMonsterNum;

		// Token: 0x04000E8C RID: 3724
		public float createTime;

		// Token: 0x04000E8D RID: 3725
		public int createNum;
	}
}

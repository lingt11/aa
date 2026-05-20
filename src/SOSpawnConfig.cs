using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200009E RID: 158
[CreateAssetMenu(menuName = "ScriptableObject/SOSpawnConfig")]
public class SOSpawnConfig : ScriptableObject
{
	// Token: 0x040002FF RID: 767
	[Header("小怪出怪时间和数量配置")]
	public SOSpawnConfig.EnemySpawnTime[] enemySpawnData;

	// Token: 0x04000300 RID: 768
	[Header("特殊怪出怪配置（BOSS和宝箱怪）")]
	public SOSpawnConfig.EnemySpawnData[] bossSpawnData;

	// Token: 0x04000301 RID: 769
	[Header("怪物挑战刷新时间")]
	public BuyMonsterData[] BuyMonsterTime;

	// Token: 0x04000302 RID: 770
	[Header("每个阶段精英怪词条概率")]
	public List<SOSpawnConfig.ChanceData> EliteEnemyEntries;

	// Token: 0x04000303 RID: 771
	[Header("每个阶段BOSS怪词条概率")]
	public List<SOSpawnConfig.ChanceData> BossEnemyEntries;

	// Token: 0x04000304 RID: 772
	[Header("场景相关")]
	public string ScenePath;

	// Token: 0x04000305 RID: 773
	public Vector3[] playerSpawnPoint;

	// Token: 0x04000306 RID: 774
	public Vector3[] enemySpawnPoint;

	// Token: 0x04000307 RID: 775
	public Vector2 CanSpellArea;

	// Token: 0x04000308 RID: 776
	public Vector2 NoSpellArea;

	// Token: 0x04000309 RID: 777
	[Header("模式相关")]
	public bool isTeamWork;

	// Token: 0x0400030A RID: 778
	public LevelStageType levelStageType;

	// Token: 0x0400030B RID: 779
	public SOLevelStageData soLevelStageData;

	// Token: 0x0400030C RID: 780
	[Header("宝箱怪")]
	public SOSpawnConfig.ChestCreateData chestCreateData;

	// Token: 0x0200009F RID: 159
	[Serializable]
	public class EnemySpawnData
	{
		// Token: 0x0400030D RID: 781
		public EnemyType[] enemyType;
	}

	// Token: 0x020000A0 RID: 160
	[Serializable]
	public class EnemySpawnTime
	{
		// Token: 0x0400030E RID: 782
		[Header("刷怪时间")]
		public int spawnTime = 55;

		// Token: 0x0400030F RID: 783
		[Header("怪物数量")]
		public int newCreateNum = 60;

		// Token: 0x04000310 RID: 784
		[Header("怪物上限")]
		public int[] newMaxEnemyNum;

		// Token: 0x04000311 RID: 785
		public int createNum = 60;

		// Token: 0x04000312 RID: 786
		public int[] MaxEnemyNum;

		// Token: 0x04000313 RID: 787
		[Header("怪物类型")]
		public EnemyType[] enemyType = new EnemyType[]
		{
			EnemyType.Goblin_1,
			EnemyType.Goblin_2
		};

		// Token: 0x04000314 RID: 788
		[Header("攻击倍率")]
		public float attackLevel = 1f;

		// Token: 0x04000315 RID: 789
		[Header("血量倍率")]
		public float hpLevel = 1f;

		// Token: 0x04000316 RID: 790
		[Header("BOSS数量")]
		public int bossNum;

		// Token: 0x04000317 RID: 791
		[Header("特殊事件")]
		public RoguelikeEventType[] eventList;

		// Token: 0x04000318 RID: 792
		[Header("精英怪护盾值")]
		public int eliteShield;

		// Token: 0x04000319 RID: 793
		[Header("BOSS攻击倍率")]
		public float bossAttackLevel = 1f;

		// Token: 0x0400031A RID: 794
		[Header("BOSS血量倍率")]
		public float bossHpLevel = 1f;
	}

	// Token: 0x020000A1 RID: 161
	[Serializable]
	public struct ChanceData
	{
		// Token: 0x0400031B RID: 795
		public float[] chanceData;
	}

	// Token: 0x020000A2 RID: 162
	[Serializable]
	public struct ChestCreateData
	{
		// Token: 0x0400031C RID: 796
		public int createMinTime;

		// Token: 0x0400031D RID: 797
		public int createMaxTime;

		// Token: 0x0400031E RID: 798
		public float startChance;

		// Token: 0x0400031F RID: 799
		public float waveAddChance;

		// Token: 0x04000320 RID: 800
		public int maxCreatNum;

		// Token: 0x04000321 RID: 801
		public int createCdTime;

		// Token: 0x04000322 RID: 802
		public bool bossLevelCreate;

		// Token: 0x04000323 RID: 803
		public bool challengeLevelCreate;
	}
}

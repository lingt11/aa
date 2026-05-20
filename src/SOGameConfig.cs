using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000092 RID: 146
[CreateAssetMenu(menuName = "ScriptableObject/SOGameConfig")]
public class SOGameConfig : ScriptableObject
{
	// Token: 0x040002BE RID: 702
	public int GameTime;

	// Token: 0x040002BF RID: 703
	public int ReadyTime;

	// Token: 0x040002C0 RID: 704
	public FindPathData[] findPathData;

	// Token: 0x040002C1 RID: 705
	public float EliteProbability = 0.025f;

	// Token: 0x040002C2 RID: 706
	public int EliteCreateTime = 30;

	// Token: 0x040002C3 RID: 707
	public float PlayerRelifeTime = 5f;

	// Token: 0x040002C4 RID: 708
	[Header("King Battle")]
	public float KingBattleTime = 90f;

	// Token: 0x040002C5 RID: 709
	public float KingBattleFinalTipTime = 45f;

	// Token: 0x040002C6 RID: 710
	public KingBattleRateConfig KingDamageLevel = new KingBattleRateConfig(0.05f, 45f, 0f, 0.2f);

	// Token: 0x040002C7 RID: 711
	public KingBattleRateConfig KingReduceLevel = new KingBattleRateConfig(4f, 45f, 0f, 4f);

	// Token: 0x040002C8 RID: 712
	public KingBattleRateConfig KingBattleMagicAddHpLevel = new KingBattleRateConfig(8f, 45f, 0f, 8f);

	// Token: 0x040002C9 RID: 713
	public KingBattleRateConfig KingAttackAddHpLevel = new KingBattleRateConfig(0.5f, 45f, 0f, 0.5f);

	// Token: 0x040002CA RID: 714
	public KingBattleRateConfig KingAttackPercentAddHpLevel = new KingBattleRateConfig(1f, 45f, 0f, 1f);

	// Token: 0x040002CB RID: 715
	public KingBattleRateConfig KingBattleAddHpLevel = new KingBattleRateConfig(1f, 45f, 0f, 1f);

	// Token: 0x040002CC RID: 716
	public Texture2D NormalCursor;

	// Token: 0x040002CD RID: 717
	public Texture2D AttackCursor;

	// Token: 0x040002CE RID: 718
	public Texture2D TargetCursor;

	// Token: 0x040002CF RID: 719
	public Texture2D DisableCursor;

	// Token: 0x040002D0 RID: 720
	public Vector4 HitColor;

	// Token: 0x040002D1 RID: 721
	[Header("幸运值曲线")]
	public AnimationCurve LuckCurve;

	// Token: 0x040002D2 RID: 722
	[Header("小怪掉落类型概率概率")]
	public NormalEnemyDropType normalEnemyDropType;

	// Token: 0x040002D3 RID: 723
	[Header("普通小怪掉落概率")]
	public float NormalDropProbability = 0.01f;

	// Token: 0x040002D4 RID: 724
	[Header("精英怪掉落概率")]
	public float EliteDropProbability = 0.025f;

	// Token: 0x040002D5 RID: 725
	[Header("每个阶段小怪掉落比率")]
	public float[] NormalDropLevel = new float[]
	{
		1f,
		0.8f,
		0.6f,
		0.4f,
		0.3f
	};

	// Token: 0x040002D6 RID: 726
	[Header("BOSS掉落技能书概率")]
	public float BossDropSkillProbability = 0.5f;

	// Token: 0x040002D7 RID: 727
	[Header("心魔掉落技能书概率")]
	public float HeartMonsterDropSkillProbability = 0.5f;

	// Token: 0x040002D8 RID: 728
	[Header("每个阶段小怪掉书品质 D~S")]
	public List<SOGameConfig.DropData> NormalSkillBook_Level;

	// Token: 0x040002D9 RID: 729
	[Header("BOSS掉落沉睡之石概率")]
	public float BossDropSleepStoneProbability = 0.5f;

	// Token: 0x040002DA RID: 730
	[Header("BOSS掉落药水概率")]
	public float BossDropMedicineProbability = 0.35f;

	// Token: 0x040002DB RID: 731
	[Header("每个阶段BOSS掉书品质 D~S")]
	public List<SOGameConfig.DropData> BossSkillBook_Level;

	// Token: 0x040002DC RID: 732
	[Header("每个阶段心魔掉书品质 D~S")]
	public List<SOGameConfig.DropData> HeartMonsterSkillBook_Level;

	// Token: 0x040002DD RID: 733
	[Header("遗物掉落概率 D~S")]
	public float[] RemainDrop = new float[]
	{
		0.5f,
		0.25f,
		0.2f,
		0.04f,
		0.01f
	};

	// Token: 0x040002DE RID: 734
	[Header("遗物掉落受幸运影响 D~S")]
	public float[] RemainLucky = new float[]
	{
		-0.5f,
		0f,
		0.5f,
		1f,
		2f
	};

	// Token: 0x040002DF RID: 735
	[Header("属性锻造器概率 D~S")]
	public float[] ForgingDrop = new float[]
	{
		0.88f,
		0.09f,
		0.03f
	};

	// Token: 0x040002E0 RID: 736
	[Header("属性锻造器受幸运影响 D~S")]
	public float[] ForgingLucky = new float[]
	{
		-0.5f,
		0.5f,
		1f
	};

	// Token: 0x040002E1 RID: 737
	[Header("技能书掉落影响 D~S")]
	public float[] BookDropLevel = new float[]
	{
		0f,
		0f,
		2f,
		4f,
		8f
	};

	// Token: 0x040002E2 RID: 738
	[Header("BOSS掉落属性书最小数量")]
	public int AttributeBook_MinNum = 6;

	// Token: 0x040002E3 RID: 739
	[Header("BOSS掉落属性书最大数量")]
	public int AttributeBook_MaxNum = 9;

	// Token: 0x040002E4 RID: 740
	public Vector3 selectHeroPoint;

	// Token: 0x02000093 RID: 147
	[Serializable]
	public struct DropData
	{
		// Token: 0x040002E5 RID: 741
		public float[] dropChance;
	}
}

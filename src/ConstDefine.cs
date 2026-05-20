using System;

// Token: 0x02000107 RID: 263
public class ConstDefine
{
	// Token: 0x06000580 RID: 1408 RVA: 0x0001FEF0 File Offset: 0x0001E0F0
	public static long ClampBattleValue(double value)
	{
		if (double.IsNaN(value))
		{
			return 0L;
		}
		if (value >= 1E+18)
		{
			return 999999999999999999L;
		}
		if (value <= -1E+18)
		{
			return -999999999999999999L;
		}
		return (long)Math.Round(value, MidpointRounding.AwayFromZero);
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x0001FF3C File Offset: 0x0001E13C
	public static int ClampIntValue(double value)
	{
		if (double.IsNaN(value))
		{
			return 0;
		}
		if (value >= 2147483647.0)
		{
			return int.MaxValue;
		}
		if (value <= -2147483648.0)
		{
			return int.MinValue;
		}
		return (int)Math.Round(value, MidpointRounding.AwayFromZero);
	}

	// Token: 0x06000582 RID: 1410 RVA: 0x0001FF74 File Offset: 0x0001E174
	public static long ClampMaxHp(long value)
	{
		if (value >= 1L)
		{
			return value;
		}
		return 1L;
	}

	// Token: 0x06000583 RID: 1411 RVA: 0x0001FF7F File Offset: 0x0001E17F
	public static long ClampMaxHp(double value)
	{
		return Math.Max(1L, ConstDefine.ClampBattleValue(value));
	}

	// Token: 0x040004DF RID: 1247
	public const int EliteLevel = 5;

	// Token: 0x040004E0 RID: 1248
	public const float EliteScale = 1.5f;

	// Token: 0x040004E1 RID: 1249
	public const float TrackRefreshTime = 5f;

	// Token: 0x040004E2 RID: 1250
	public const float SyncOffsetTime = 0.1f;

	// Token: 0x040004E3 RID: 1251
	public const float MinAttackOffset = 0.135f;

	// Token: 0x040004E4 RID: 1252
	public static string[] SkillAttributeStr = new string[]
	{
		"无属性",
		"火焰",
		"冰冻",
		"雷电"
	};

	// Token: 0x040004E5 RID: 1253
	public const int StrAddAttack = 1;

	// Token: 0x040004E6 RID: 1254
	public const int StaAddHp = 10;

	// Token: 0x040004E7 RID: 1255
	public const float AgiAddAttackSpeed = 0.2f;

	// Token: 0x040004E8 RID: 1256
	public const float SoundDistance = 20f;

	// Token: 0x040004E9 RID: 1257
	public const float EffectDistance = 35f;

	// Token: 0x040004EA RID: 1258
	public const float TrackDistance = 12f;

	// Token: 0x040004EB RID: 1259
	public const float DeadTime = 10f;

	// Token: 0x040004EC RID: 1260
	public const float HideRoleModeDistance = 22f;

	// Token: 0x040004ED RID: 1261
	public const float BossPercentLevel = 0.2f;

	// Token: 0x040004EE RID: 1262
	public const float CurPercentShieldLevel = 0.25f;

	// Token: 0x040004EF RID: 1263
	public const float FinalSkillAddLevel = 0.5f;

	// Token: 0x040004F0 RID: 1264
	public const float FinalSkillAddShieldLevel = 0.5f;

	// Token: 0x040004F1 RID: 1265
	public const float AttackEffectAddDamageLevel = 0.75f;

	// Token: 0x040004F2 RID: 1266
	public const int MaxDamage = 99999999;

	// Token: 0x040004F3 RID: 1267
	public const long MaxBattleValue = 999999999999999999L;

	// Token: 0x040004F4 RID: 1268
	public const long MinMaxHp = 1L;

	// Token: 0x040004F5 RID: 1269
	public const int MinRelifeTime = 5;

	// Token: 0x040004F6 RID: 1270
	public const int CardMaxCapacity = 40;

	// Token: 0x040004F7 RID: 1271
	public const int CardMaxCount = 999;

	// Token: 0x040004F8 RID: 1272
	public const float SimulatorRadius = 0.5f;

	// Token: 0x040004F9 RID: 1273
	public const int PickSunGold = 25;

	// Token: 0x040004FA RID: 1274
	public static int[] RelicSellGold = new int[]
	{
		500,
		750,
		1000,
		1250,
		1500
	};

	// Token: 0x040004FB RID: 1275
	public const float BossXuanYunCd = 5f;

	// Token: 0x040004FC RID: 1276
	public const float PlayerXuanYunCd = 4f;
}

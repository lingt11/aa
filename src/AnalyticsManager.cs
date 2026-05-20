using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x02000043 RID: 67
public class AnalyticsManager : IApplicationQuit
{
	// Token: 0x06000104 RID: 260 RVA: 0x00006D58 File Offset: 0x00004F58
	public AnalyticsManager()
	{
		this.provider = new GameAnalyticsProvider();
		this.provider.Initialize();
		this.ResetRunData();
	}

	// Token: 0x06000105 RID: 261 RVA: 0x00006DC4 File Offset: 0x00004FC4
	public void RecordEnterDungeon()
	{
		if (this.enterDungeonTracked)
		{
			return;
		}
		this.EnsureRun();
		this.enterDungeonTracked = true;
		this.provider.TrackDesignEvent(this.BuildEventId(new string[]
		{
			this.GetMapEventPart(),
			"enter_dungeon",
			"enter",
			this.GetHeroEventPart()
		}), 1f);
	}

	// Token: 0x06000106 RID: 262 RVA: 0x00006E28 File Offset: 0x00005028
	public void RecordRoguelikeShown(string source, RoguelikeUIData[] roguelikeDataAry)
	{
		if (string.IsNullOrEmpty(source) || roguelikeDataAry == null)
		{
			return;
		}
		this.EnsureRun();
		for (int i = 0; i < roguelikeDataAry.Length; i++)
		{
			if (!string.IsNullOrEmpty(roguelikeDataAry[i].name))
			{
				this.GetRoguelikeMetricData(source, roguelikeDataAry[i]).shownCount++;
			}
		}
	}

	// Token: 0x06000107 RID: 263 RVA: 0x00006E83 File Offset: 0x00005083
	public void RecordRoguelikeSelected(string source, RoguelikeUIData roguelikeData)
	{
		if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(roguelikeData.name))
		{
			return;
		}
		this.EnsureRun();
		this.GetRoguelikeMetricData(source, roguelikeData).selectedCount++;
	}

	// Token: 0x06000108 RID: 264 RVA: 0x00006EB6 File Offset: 0x000050B6
	public void RecordRoguelikeRefreshAway(string source, RoguelikeUIData roguelikeData)
	{
		if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(roguelikeData.name))
		{
			return;
		}
		this.EnsureRun();
		this.GetRoguelikeMetricData(source, roguelikeData).refreshAwayCount++;
	}

	// Token: 0x06000109 RID: 265 RVA: 0x00006EEC File Offset: 0x000050EC
	public void RecordEarlyResetAtWaveZero()
	{
		this.EnsureRun();
		this.provider.TrackDesignEvent(this.BuildEventId(new string[]
		{
			this.GetMapEventPart(),
			"enter_dungeon",
			"wave0_reset",
			this.GetHeroEventPart()
		}), 1f);
	}

	// Token: 0x0600010A RID: 266 RVA: 0x00006F40 File Offset: 0x00005140
	public void RecordGiveUpMidRun()
	{
		if (this.giveUpUploaded)
		{
			return;
		}
		this.EnsureRun();
		this.giveUpUploaded = true;
		this.provider.TrackDesignEvent(this.BuildEventId(new string[]
		{
			this.GetMapEventPart(),
			"enter_dungeon",
			"give_up",
			this.GetWaveEventPart()
		}), 1f);
	}

	// Token: 0x0600010B RID: 267 RVA: 0x00006FA4 File Offset: 0x000051A4
	public void RecordKingChallengeSelection(int rankIndex, HeroType heroType)
	{
		this.EnsureRun();
		this.kingChallengeRank = rankIndex;
		this.kingChallengeTargetHeroId = (int)heroType;
		this.provider.TrackDesignEvent(this.BuildEventId(new string[]
		{
			this.GetMapEventPart(),
			"king",
			"challenge",
			"selected",
			"rank_" + rankIndex.ToString()
		}), 1f);
	}

	// Token: 0x0600010C RID: 268 RVA: 0x00007018 File Offset: 0x00005218
	public void RecordKingChallengeResult(bool isWin)
	{
		if (this.kingChallengeRank < 0)
		{
			return;
		}
		this.EnsureRun();
		this.provider.TrackDesignEvent(this.BuildEventId(new string[]
		{
			this.GetMapEventPart(),
			"king",
			"challenge_result",
			"rank_" + this.kingChallengeRank.ToString(),
			isWin ? "win" : "lose"
		}), 1f);
		if (this.kingChallengeTargetHeroId >= 0)
		{
			this.provider.TrackDesignEvent(this.BuildEventId(new string[]
			{
				this.GetMapEventPart(),
				"king",
				"challenge_target",
				this.GetHeroEventPart(this.kingChallengeTargetHeroId),
				isWin ? "win" : "lose"
			}), 1f);
		}
	}

	// Token: 0x0600010D RID: 269 RVA: 0x000070F4 File Offset: 0x000052F4
	public void RecordServerEnemySpawn(EnemyType enemyType, int waveNum, bool isBoss, bool isElite)
	{
		if (!this.ShouldTrackEnemy(isBoss, isElite))
		{
			return;
		}
		this.EnsureRun();
		if (isBoss)
		{
			string bossMetricKey = this.GetBossMetricKey(enemyType, waveNum);
			AnalyticsManager.BossMetricData bossMetricData;
			if (!this.bossMetricDic.TryGetValue(bossMetricKey, out bossMetricData))
			{
				bossMetricData = new AnalyticsManager.BossMetricData
				{
					enemyType = enemyType,
					waveNum = waveNum
				};
				this.bossMetricDic.Add(bossMetricKey, bossMetricData);
			}
			bossMetricData.spawnCount++;
			return;
		}
		AnalyticsManager.EliteMetricData eliteMetricData;
		if (!this.eliteMetricDic.TryGetValue(waveNum, out eliteMetricData))
		{
			eliteMetricData = new AnalyticsManager.EliteMetricData
			{
				waveNum = waveNum
			};
			this.eliteMetricDic.Add(waveNum, eliteMetricData);
		}
		eliteMetricData.spawnCount++;
	}

	// Token: 0x0600010E RID: 270 RVA: 0x00007198 File Offset: 0x00005398
	public void RecordServerEnemyKill(EnemyType enemyType, int waveNum, bool isBoss, bool isElite)
	{
		if (!this.ShouldTrackEnemy(isBoss, isElite))
		{
			return;
		}
		this.EnsureRun();
		if (isBoss)
		{
			string bossMetricKey = this.GetBossMetricKey(enemyType, waveNum);
			AnalyticsManager.BossMetricData bossMetricData;
			if (!this.bossMetricDic.TryGetValue(bossMetricKey, out bossMetricData))
			{
				bossMetricData = new AnalyticsManager.BossMetricData
				{
					enemyType = enemyType,
					waveNum = waveNum
				};
				this.bossMetricDic.Add(bossMetricKey, bossMetricData);
			}
			bossMetricData.killCount++;
			return;
		}
		AnalyticsManager.EliteMetricData eliteMetricData;
		if (!this.eliteMetricDic.TryGetValue(waveNum, out eliteMetricData))
		{
			eliteMetricData = new AnalyticsManager.EliteMetricData
			{
				waveNum = waveNum
			};
			this.eliteMetricDic.Add(waveNum, eliteMetricData);
		}
		eliteMetricData.killCount++;
	}

	// Token: 0x0600010F RID: 271 RVA: 0x0000723C File Offset: 0x0000543C
	public void RecordPlayerDead()
	{
		this.EnsureRun();
		int heroId = (int)((GameHelperClient.localPlayer != null) ? GameHelperClient.localPlayer.heroType : ((HeroType)(-1)));
		int waveNum = GameHelperClient.WaveNum;
		string key = heroId.ToString() + "|" + waveNum.ToString();
		AnalyticsManager.PlayerDeadMetricData playerDeadMetricData;
		if (!this.playerDeadMetricDic.TryGetValue(key, out playerDeadMetricData))
		{
			playerDeadMetricData = new AnalyticsManager.PlayerDeadMetricData
			{
				heroId = heroId,
				waveNum = waveNum
			};
			this.playerDeadMetricDic.Add(key, playerDeadMetricData);
		}
		playerDeadMetricData.deadCount++;
	}

	// Token: 0x06000110 RID: 272 RVA: 0x000072C8 File Offset: 0x000054C8
	public void UploadGameOverAnalytics(bool isWin)
	{
		if (this.gameOverUploaded)
		{
			return;
		}
		this.EnsureRun();
		this.gameOverUploaded = true;
		this.provider.TrackDesignEvent(this.BuildEventId(new string[]
		{
			this.GetMapEventPart(),
			"hero",
			"result",
			this.GetHeroEventPart(),
			isWin ? "win" : "lose"
		}), 1f);
		this.provider.TrackDesignEvent(this.BuildEventId(new string[]
		{
			this.GetMapEventPart(),
			"hero",
			"result_reason",
			this.GetHeroEventPart(),
			this.GetGameResultReason(isWin)
		}), 1f);
		foreach (AnalyticsManager.PlayerDeadMetricData playerDeadMetricData in this.playerDeadMetricDic.Values)
		{
			if (playerDeadMetricData.deadCount > 0)
			{
				this.provider.TrackDesignEvent(this.BuildEventId(new string[]
				{
					this.GetMapEventPart(),
					"hero",
					"dead",
					this.GetHeroEventPart(playerDeadMetricData.heroId),
					this.GetWaveEventPart(playerDeadMetricData.waveNum)
				}), (float)playerDeadMetricData.deadCount);
			}
		}
		if (!isWin)
		{
			this.provider.TrackDesignEvent(this.BuildEventId(new string[]
			{
				this.GetMapEventPart(),
				"match",
				"failed_wave",
				this.GetWaveEventPart()
			}), 1f);
		}
		foreach (AnalyticsManager.RoguelikeMetricData roguelikeMetricData in this.roguelikeMetricDic.Values)
		{
			if (roguelikeMetricData.shownCount > 0)
			{
				this.provider.TrackDesignEvent(this.BuildEventId(new string[]
				{
					this.GetMapEventPart(),
					"choice",
					"shown",
					roguelikeMetricData.source,
					roguelikeMetricData.optionEventPart
				}), (float)roguelikeMetricData.shownCount);
			}
			if (roguelikeMetricData.refreshAwayCount > 0)
			{
				this.provider.TrackDesignEvent(this.BuildEventId(new string[]
				{
					this.GetMapEventPart(),
					"choice",
					"refresh_away",
					roguelikeMetricData.source,
					roguelikeMetricData.optionEventPart
				}), (float)roguelikeMetricData.refreshAwayCount);
			}
			if (roguelikeMetricData.selectedCount > 0)
			{
				this.provider.TrackDesignEvent(this.BuildEventId(new string[]
				{
					this.GetMapEventPart(),
					"choice",
					"selected",
					roguelikeMetricData.source,
					roguelikeMetricData.optionEventPart
				}), (float)roguelikeMetricData.selectedCount);
				this.provider.TrackDesignEvent(this.BuildEventId(new string[]
				{
					this.GetMapEventPart(),
					"choice",
					"selected_result",
					this.BuildChoiceResultPart(roguelikeMetricData),
					isWin ? "win" : "lose"
				}), (float)roguelikeMetricData.selectedCount);
			}
		}
		if (GameHelperClient.isHost)
		{
			foreach (AnalyticsManager.BossMetricData bossMetricData in this.bossMetricDic.Values)
			{
				if (bossMetricData.spawnCount > 0)
				{
					this.provider.TrackDesignEvent(this.BuildEventId(new string[]
					{
						this.GetMapEventPart(),
						"enemy",
						"boss_spawn",
						this.GetWaveEventPart(bossMetricData.waveNum),
						this.GetEnemyEventPart(bossMetricData.enemyType)
					}), (float)bossMetricData.spawnCount);
				}
				if (bossMetricData.killCount > 0)
				{
					this.provider.TrackDesignEvent(this.BuildEventId(new string[]
					{
						this.GetMapEventPart(),
						"enemy",
						"boss_kill",
						this.GetWaveEventPart(bossMetricData.waveNum),
						this.GetEnemyEventPart(bossMetricData.enemyType)
					}), (float)bossMetricData.killCount);
				}
			}
			foreach (AnalyticsManager.EliteMetricData eliteMetricData in this.eliteMetricDic.Values)
			{
				if (eliteMetricData.spawnCount > 0)
				{
					this.provider.TrackDesignEvent(this.BuildEventId(new string[]
					{
						this.GetMapEventPart(),
						"enemy",
						"elite_spawn",
						this.GetWaveEventPart(eliteMetricData.waveNum)
					}), (float)eliteMetricData.spawnCount);
				}
				if (eliteMetricData.killCount > 0)
				{
					this.provider.TrackDesignEvent(this.BuildEventId(new string[]
					{
						this.GetMapEventPart(),
						"enemy",
						"elite_kill",
						this.GetWaveEventPart(eliteMetricData.waveNum)
					}), (float)eliteMetricData.killCount);
				}
			}
		}
	}

	// Token: 0x06000111 RID: 273 RVA: 0x0000782C File Offset: 0x00005A2C
	public void ResetRunData()
	{
		this.sessionId = string.Empty;
		this.enterDungeonTracked = false;
		this.gameOverUploaded = false;
		this.giveUpUploaded = false;
		this.kingChallengeRank = -1;
		this.kingChallengeTargetHeroId = -1;
		this.roguelikeMetricDic.Clear();
		this.bossMetricDic.Clear();
		this.eliteMetricDic.Clear();
		this.playerDeadMetricDic.Clear();
	}

	// Token: 0x06000112 RID: 274 RVA: 0x00007893 File Offset: 0x00005A93
	public void OnApplicationQuit()
	{
		this.provider.Flush();
	}

	// Token: 0x06000113 RID: 275 RVA: 0x000078A0 File Offset: 0x00005AA0
	private void EnsureRun()
	{
		if (string.IsNullOrEmpty(this.sessionId))
		{
			this.sessionId = Guid.NewGuid().ToString("N");
		}
	}

	// Token: 0x06000114 RID: 276 RVA: 0x000078D4 File Offset: 0x00005AD4
	private AnalyticsManager.RoguelikeMetricData GetRoguelikeMetricData(string source, RoguelikeUIData roguelikeData)
	{
		string optionId = this.GetOptionId(roguelikeData);
		string key = source + "|" + optionId;
		AnalyticsManager.RoguelikeMetricData roguelikeMetricData;
		if (!this.roguelikeMetricDic.TryGetValue(key, out roguelikeMetricData))
		{
			roguelikeMetricData = new AnalyticsManager.RoguelikeMetricData
			{
				source = this.SanitizeEventPart(source),
				optionId = optionId,
				optionName = roguelikeData.name,
				optionEventPart = this.SanitizeEventPart(optionId)
			};
			this.roguelikeMetricDic.Add(key, roguelikeMetricData);
		}
		return roguelikeMetricData;
	}

	// Token: 0x06000115 RID: 277 RVA: 0x00007948 File Offset: 0x00005B48
	private string GetOptionId(RoguelikeUIData roguelikeData)
	{
		if (!string.IsNullOrEmpty(roguelikeData.data))
		{
			return roguelikeData.data;
		}
		if (!string.IsNullOrEmpty(roguelikeData.name))
		{
			return "name_" + this.GetDeterministicHash(roguelikeData.name).ToString();
		}
		return "none";
	}

	// Token: 0x06000116 RID: 278 RVA: 0x0000799A File Offset: 0x00005B9A
	private bool ShouldTrackEnemy(bool isBoss, bool isElite)
	{
		return GameHelperClient.isHost && (isBoss || isElite);
	}

	// Token: 0x06000117 RID: 279 RVA: 0x000079AE File Offset: 0x00005BAE
	private string GetBossMetricKey(EnemyType enemyType, int waveNum)
	{
		return waveNum.ToString() + "|" + enemyType.ToString();
	}

	// Token: 0x06000118 RID: 280 RVA: 0x000079CE File Offset: 0x00005BCE
	private string GetHeroEventPart()
	{
		return this.GetHeroEventPart((int)((GameHelperClient.localPlayer != null) ? GameHelperClient.localPlayer.heroType : ((HeroType)(-1))));
	}

	// Token: 0x06000119 RID: 281 RVA: 0x000079F0 File Offset: 0x00005BF0
	private string GetHeroEventPart(int heroId)
	{
		return "hero_" + Mathf.Max(heroId, -1).ToString();
	}

	// Token: 0x0600011A RID: 282 RVA: 0x00007A18 File Offset: 0x00005C18
	private string GetMapEventPart()
	{
		return "map_" + Mathf.Max(GameHelperClient.MapLevel, 0).ToString();
	}

	// Token: 0x0600011B RID: 283 RVA: 0x00007A42 File Offset: 0x00005C42
	private string GetWaveEventPart()
	{
		return this.GetWaveEventPart(GameHelperClient.WaveNum);
	}

	// Token: 0x0600011C RID: 284 RVA: 0x00007A50 File Offset: 0x00005C50
	private string GetWaveEventPart(int waveNum)
	{
		return "wave_" + Mathf.Max(waveNum, 0).ToString();
	}

	// Token: 0x0600011D RID: 285 RVA: 0x00007A76 File Offset: 0x00005C76
	private string GetEnemyEventPart(EnemyType enemyType)
	{
		return this.SanitizeEventPart(enemyType.ToString());
	}

	// Token: 0x0600011E RID: 286 RVA: 0x00007A8B File Offset: 0x00005C8B
	private string BuildChoiceResultPart(AnalyticsManager.RoguelikeMetricData metricData)
	{
		return this.SanitizeEventPart(metricData.source + "_" + metricData.optionEventPart);
	}

	// Token: 0x0600011F RID: 287 RVA: 0x00007AA9 File Offset: 0x00005CA9
	private string GetGameResultReason(bool isWin)
	{
		if (isWin)
		{
			return "win_reason_clear";
		}
		if (!GameHelperClient.IsExitGameOver)
		{
			return "lose_reason_battle";
		}
		return "lose_reason_exit";
	}

	// Token: 0x06000120 RID: 288 RVA: 0x00007AC8 File Offset: 0x00005CC8
	private string BuildEventId(params string[] parts)
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < parts.Length; i++)
		{
			if (!string.IsNullOrEmpty(parts[i]))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(':');
				}
				stringBuilder.Append(this.SanitizeEventPart(parts[i]));
			}
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06000121 RID: 289 RVA: 0x00007B1C File Offset: 0x00005D1C
	private string SanitizeEventPart(string value)
	{
		if (string.IsNullOrEmpty(value))
		{
			return "none";
		}
		StringBuilder stringBuilder = new StringBuilder(value.Length);
		for (int i = 0; i < value.Length; i++)
		{
			char c = char.ToLowerInvariant(value[i]);
			if (char.IsLetterOrDigit(c))
			{
				stringBuilder.Append(c);
			}
			else if (c == '_' || c == '-')
			{
				stringBuilder.Append(c);
			}
			else
			{
				stringBuilder.Append('_');
			}
		}
		string text = stringBuilder.ToString().Trim('_');
		if (text.Length > 32)
		{
			text = text.Substring(0, 32);
		}
		if (!string.IsNullOrEmpty(text))
		{
			return text;
		}
		return "none";
	}

	// Token: 0x06000122 RID: 290 RVA: 0x00007BC4 File Offset: 0x00005DC4
	private int GetDeterministicHash(string value)
	{
		int num = 23;
		for (int i = 0; i < value.Length; i++)
		{
			num = num * 31 + (int)value[i];
		}
		return Mathf.Abs(num);
	}

	// Token: 0x0400012D RID: 301
	private readonly IAnalyticsProvider provider;

	// Token: 0x0400012E RID: 302
	private readonly Dictionary<string, AnalyticsManager.RoguelikeMetricData> roguelikeMetricDic = new Dictionary<string, AnalyticsManager.RoguelikeMetricData>();

	// Token: 0x0400012F RID: 303
	private readonly Dictionary<string, AnalyticsManager.BossMetricData> bossMetricDic = new Dictionary<string, AnalyticsManager.BossMetricData>();

	// Token: 0x04000130 RID: 304
	private readonly Dictionary<int, AnalyticsManager.EliteMetricData> eliteMetricDic = new Dictionary<int, AnalyticsManager.EliteMetricData>();

	// Token: 0x04000131 RID: 305
	private readonly Dictionary<string, AnalyticsManager.PlayerDeadMetricData> playerDeadMetricDic = new Dictionary<string, AnalyticsManager.PlayerDeadMetricData>();

	// Token: 0x04000132 RID: 306
	private string sessionId;

	// Token: 0x04000133 RID: 307
	private bool enterDungeonTracked;

	// Token: 0x04000134 RID: 308
	private bool gameOverUploaded;

	// Token: 0x04000135 RID: 309
	private bool giveUpUploaded;

	// Token: 0x04000136 RID: 310
	private int kingChallengeRank = -1;

	// Token: 0x04000137 RID: 311
	private int kingChallengeTargetHeroId = -1;

	// Token: 0x02000044 RID: 68
	private class RoguelikeMetricData
	{
		// Token: 0x04000138 RID: 312
		public string source;

		// Token: 0x04000139 RID: 313
		public string optionId;

		// Token: 0x0400013A RID: 314
		public string optionName;

		// Token: 0x0400013B RID: 315
		public string optionEventPart;

		// Token: 0x0400013C RID: 316
		public int shownCount;

		// Token: 0x0400013D RID: 317
		public int selectedCount;

		// Token: 0x0400013E RID: 318
		public int refreshAwayCount;
	}

	// Token: 0x02000045 RID: 69
	private class BossMetricData
	{
		// Token: 0x0400013F RID: 319
		public EnemyType enemyType;

		// Token: 0x04000140 RID: 320
		public int waveNum;

		// Token: 0x04000141 RID: 321
		public int spawnCount;

		// Token: 0x04000142 RID: 322
		public int killCount;
	}

	// Token: 0x02000046 RID: 70
	private class EliteMetricData
	{
		// Token: 0x04000143 RID: 323
		public int waveNum;

		// Token: 0x04000144 RID: 324
		public int spawnCount;

		// Token: 0x04000145 RID: 325
		public int killCount;
	}

	// Token: 0x02000047 RID: 71
	private class PlayerDeadMetricData
	{
		// Token: 0x04000146 RID: 326
		public int heroId;

		// Token: 0x04000147 RID: 327
		public int waveNum;

		// Token: 0x04000148 RID: 328
		public int deadCount;
	}
}

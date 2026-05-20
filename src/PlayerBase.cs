using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Mirror;
using Mirror.RemoteCalls;
using UnityEngine;

// Token: 0x02000292 RID: 658
public class PlayerBase : RoleBase
{
	// Token: 0x17000057 RID: 87
	// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x0004A0C8 File Offset: 0x000482C8
	// (set) Token: 0x06000CA6 RID: 3238 RVA: 0x0004A0D0 File Offset: 0x000482D0
	public float equipAddValue { get; set; }

	// Token: 0x17000058 RID: 88
	// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x0004A0D9 File Offset: 0x000482D9
	// (set) Token: 0x06000CA8 RID: 3240 RVA: 0x0004A0E1 File Offset: 0x000482E1
	public int equipSTR { get; set; }

	// Token: 0x17000059 RID: 89
	// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x0004A0EA File Offset: 0x000482EA
	// (set) Token: 0x06000CAA RID: 3242 RVA: 0x0004A0F2 File Offset: 0x000482F2
	public int equipAGI { get; set; }

	// Token: 0x1700005A RID: 90
	// (get) Token: 0x06000CAB RID: 3243 RVA: 0x0004A0FB File Offset: 0x000482FB
	// (set) Token: 0x06000CAC RID: 3244 RVA: 0x0004A103 File Offset: 0x00048303
	public int equipSTA { get; set; }

	// Token: 0x1700005B RID: 91
	// (get) Token: 0x06000CAD RID: 3245 RVA: 0x0004A10C File Offset: 0x0004830C
	// (set) Token: 0x06000CAE RID: 3246 RVA: 0x0004A114 File Offset: 0x00048314
	public int equipAttack { get; set; }

	// Token: 0x1700005C RID: 92
	// (get) Token: 0x06000CAF RID: 3247 RVA: 0x0004A11D File Offset: 0x0004831D
	// (set) Token: 0x06000CB0 RID: 3248 RVA: 0x0004A125 File Offset: 0x00048325
	public float equipAttackSpeed { get; set; }

	// Token: 0x1700005D RID: 93
	// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0004A12E File Offset: 0x0004832E
	// (set) Token: 0x06000CB2 RID: 3250 RVA: 0x0004A136 File Offset: 0x00048336
	public int equipArmor { get; set; }

	// Token: 0x1700005E RID: 94
	// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x0004A13F File Offset: 0x0004833F
	// (set) Token: 0x06000CB4 RID: 3252 RVA: 0x0004A147 File Offset: 0x00048347
	public int equipHp { get; set; }

	// Token: 0x1700005F RID: 95
	// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x0004A150 File Offset: 0x00048350
	// (set) Token: 0x06000CB6 RID: 3254 RVA: 0x0004A158 File Offset: 0x00048358
	public int equipMp { get; set; }

	// Token: 0x17000060 RID: 96
	// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x0004A161 File Offset: 0x00048361
	// (set) Token: 0x06000CB8 RID: 3256 RVA: 0x0004A169 File Offset: 0x00048369
	public float equipMoveSpeed { get; set; }

	// Token: 0x17000061 RID: 97
	// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0004A172 File Offset: 0x00048372
	// (set) Token: 0x06000CBA RID: 3258 RVA: 0x0004A17A File Offset: 0x0004837A
	public int equipDoge { get; set; }

	// Token: 0x17000062 RID: 98
	// (get) Token: 0x06000CBB RID: 3259 RVA: 0x0004A183 File Offset: 0x00048383
	// (set) Token: 0x06000CBC RID: 3260 RVA: 0x0004A18B File Offset: 0x0004838B
	public int equipLuck { get; set; }

	// Token: 0x17000063 RID: 99
	// (get) Token: 0x06000CBD RID: 3261 RVA: 0x0004A194 File Offset: 0x00048394
	// (set) Token: 0x06000CBE RID: 3262 RVA: 0x0004A19C File Offset: 0x0004839C
	public int skillReduction { get; set; }

	// Token: 0x17000064 RID: 100
	// (get) Token: 0x06000CBF RID: 3263 RVA: 0x0004A1A5 File Offset: 0x000483A5
	// (set) Token: 0x06000CC0 RID: 3264 RVA: 0x0004A1AD File Offset: 0x000483AD
	public int equipSkillReduction { get; set; }

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x0004A1B6 File Offset: 0x000483B6
	// (set) Token: 0x06000CC2 RID: 3266 RVA: 0x0004A1BE File Offset: 0x000483BE
	public int equipHpAddSec { get; set; }

	// Token: 0x17000066 RID: 102
	// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x0004A1C7 File Offset: 0x000483C7
	// (set) Token: 0x06000CC4 RID: 3268 RVA: 0x0004A1CF File Offset: 0x000483CF
	public int equipMpAddSec { get; set; }

	// Token: 0x17000067 RID: 103
	// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x0004A1D8 File Offset: 0x000483D8
	// (set) Token: 0x06000CC6 RID: 3270 RVA: 0x0004A1E0 File Offset: 0x000483E0
	public int equipBaoJiLv { get; set; }

	// Token: 0x17000068 RID: 104
	// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x0004A1E9 File Offset: 0x000483E9
	// (set) Token: 0x06000CC8 RID: 3272 RVA: 0x0004A1F1 File Offset: 0x000483F1
	public int equipBaoJiDamage { get; set; }

	// Token: 0x17000069 RID: 105
	// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x0004A1FA File Offset: 0x000483FA
	// (set) Token: 0x06000CCA RID: 3274 RVA: 0x0004A202 File Offset: 0x00048402
	public int equipXiXue { get; set; }

	// Token: 0x1700006A RID: 106
	// (get) Token: 0x06000CCB RID: 3275 RVA: 0x0004A20B File Offset: 0x0004840B
	// (set) Token: 0x06000CCC RID: 3276 RVA: 0x0004A213 File Offset: 0x00048413
	public float equipXiXueLV { get; set; }

	// Token: 0x1700006B RID: 107
	// (get) Token: 0x06000CCD RID: 3277 RVA: 0x0004A21C File Offset: 0x0004841C
	// (set) Token: 0x06000CCE RID: 3278 RVA: 0x0004A224 File Offset: 0x00048424
	public int extraDamage { get; set; }

	// Token: 0x1700006C RID: 108
	// (get) Token: 0x06000CCF RID: 3279 RVA: 0x0004A22D File Offset: 0x0004842D
	// (set) Token: 0x06000CD0 RID: 3280 RVA: 0x0004A235 File Offset: 0x00048435
	public float equipSkillDamage { get; set; }

	// Token: 0x1700006D RID: 109
	// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x0004A23E File Offset: 0x0004843E
	// (set) Token: 0x06000CD2 RID: 3282 RVA: 0x0004A246 File Offset: 0x00048446
	public float equipNormalBreakingShield { get; set; }

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x0004A24F File Offset: 0x0004844F
	// (set) Token: 0x06000CD4 RID: 3284 RVA: 0x0004A257 File Offset: 0x00048457
	public float equipSkillBreakingShield { get; set; }

	// Token: 0x1700006F RID: 111
	// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0004A260 File Offset: 0x00048460
	// (set) Token: 0x06000CD6 RID: 3286 RVA: 0x0004A268 File Offset: 0x00048468
	public int gold { get; set; }

	// Token: 0x17000070 RID: 112
	// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x0004A271 File Offset: 0x00048471
	// (set) Token: 0x06000CD8 RID: 3288 RVA: 0x0004A279 File Offset: 0x00048479
	public int gem { get; set; }

	// Token: 0x17000071 RID: 113
	// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x0004A282 File Offset: 0x00048482
	// (set) Token: 0x06000CDA RID: 3290 RVA: 0x0004A28A File Offset: 0x0004848A
	public int equipSkillCd { get; set; }

	// Token: 0x17000072 RID: 114
	// (get) Token: 0x06000CDB RID: 3291 RVA: 0x0004A293 File Offset: 0x00048493
	// (set) Token: 0x06000CDC RID: 3292 RVA: 0x0004A29B File Offset: 0x0004849B
	public int addRelifeTime { get; set; }

	// Token: 0x17000073 RID: 115
	// (get) Token: 0x06000CDD RID: 3293 RVA: 0x0004A2A4 File Offset: 0x000484A4
	public override float XiXueLvAll
	{
		get
		{
			return this.xiXueLv + this.equipXiXueLV;
		}
	}

	// Token: 0x17000074 RID: 116
	// (get) Token: 0x06000CDE RID: 3294 RVA: 0x0004A2B3 File Offset: 0x000484B3
	// (set) Token: 0x06000CDF RID: 3295 RVA: 0x0004A2BB File Offset: 0x000484BB
	public int Level
	{
		get
		{
			return this.level;
		}
		set
		{
			this.level = value;
			if (base.isLocalPlayer)
			{
				UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
				if (ui != null)
				{
					ui.RefreshPlayerStateUI();
				}
				UI_PlayerState ui2 = Game.UI.GetUI<UI_PlayerState>();
				if (ui2 == null)
				{
					return;
				}
				ui2.RefreshPlayerStateUI();
			}
		}
	}

	// Token: 0x17000075 RID: 117
	// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x0004A2F8 File Offset: 0x000484F8
	public override long FinalAttackPower
	{
		get
		{
			long num = (long)base.mAttackPower + (long)this.STR + (long)this.equipAttack;
			if (this.attackPercent <= 0f)
			{
				return num;
			}
			return ConstDefine.ClampBattleValue((double)num * (1.0 + (double)this.attackPercent));
		}
	}

	// Token: 0x17000076 RID: 118
	// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x0004A345 File Offset: 0x00048545
	public int FinalSkillReduction
	{
		get
		{
			return this.skillReduction + this.equipSkillReduction;
		}
	}

	// Token: 0x17000077 RID: 119
	// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x0004A354 File Offset: 0x00048554
	// (set) Token: 0x06000CE3 RID: 3299 RVA: 0x0004A363 File Offset: 0x00048563
	public override float moveSpeed
	{
		get
		{
			return this.mMoveSpeed + this.equipMoveSpeed;
		}
		set
		{
			this.mMoveSpeed = value;
		}
	}

	// Token: 0x17000078 RID: 120
	// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x0004A36C File Offset: 0x0004856C
	public override int FinalDoge
	{
		get
		{
			return this.doge + this.equipDoge;
		}
	}

	// Token: 0x17000079 RID: 121
	// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x0004A37B File Offset: 0x0004857B
	// (set) Token: 0x06000CE6 RID: 3302 RVA: 0x0004A38A File Offset: 0x0004858A
	public override float attackSpeed
	{
		get
		{
			return base.mAttackSpeed + this.equipAttackSpeed;
		}
		set
		{
			if (base.isLocalPlayer && !Mathf.Approximately(base.mAttackSpeed, value))
			{
				UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
				if (ui != null)
				{
					ui.RefreshBaoJi();
				}
			}
			base.mAttackSpeed = value;
		}
	}

	// Token: 0x1700007A RID: 122
	// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x0004A3BE File Offset: 0x000485BE
	public override int armor
	{
		get
		{
			return base.mArmor + this.equipArmor;
		}
	}

	// Token: 0x1700007B RID: 123
	// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x0004A3CD File Offset: 0x000485CD
	// (set) Token: 0x06000CE9 RID: 3305 RVA: 0x0004A3E3 File Offset: 0x000485E3
	public override float critical
	{
		get
		{
			return this.mCritical + (float)this.equipBaoJiLv * 0.01f;
		}
		set
		{
			this.mCritical = value;
		}
	}

	// Token: 0x1700007C RID: 124
	// (get) Token: 0x06000CEA RID: 3306 RVA: 0x0004A3EC File Offset: 0x000485EC
	// (set) Token: 0x06000CEB RID: 3307 RVA: 0x0004A402 File Offset: 0x00048602
	public override float criticalDamage
	{
		get
		{
			return this.mCriticalDamage + (float)this.equipBaoJiDamage * 0.01f;
		}
		set
		{
			this.mCriticalDamage = value;
		}
	}

	// Token: 0x1700007D RID: 125
	// (get) Token: 0x06000CEC RID: 3308 RVA: 0x0004A40B File Offset: 0x0004860B
	public override int hpAddSec
	{
		get
		{
			return this.mHpAddSec + this.equipHpAddSec;
		}
	}

	// Token: 0x1700007E RID: 126
	// (get) Token: 0x06000CED RID: 3309 RVA: 0x0004A41A File Offset: 0x0004861A
	// (set) Token: 0x06000CEE RID: 3310 RVA: 0x0004A424 File Offset: 0x00048624
	public float StaAllAdd
	{
		get
		{
			return this.staAllAdd;
		}
		set
		{
			int sta = this.STA;
			this.staAllAdd = value;
			if (base.isLocalPlayer)
			{
				UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
				if (ui != null)
				{
					ui.RefreshPlayerStateUI();
				}
				UI_PlayerState ui2 = Game.UI.GetUI<UI_PlayerState>();
				if (ui2 != null)
				{
					ui2.RefreshPlayerStateUI();
				}
				base.AddPlayerSTAHp(this.STA - sta);
			}
		}
	}

	// Token: 0x1700007F RID: 127
	// (get) Token: 0x06000CEF RID: 3311 RVA: 0x0004A47F File Offset: 0x0004867F
	public override int STA
	{
		get
		{
			if (!Mathf.Approximately(this.staAllAdd, 0f))
			{
				return (int)((float)(base.sta + this.equipSTA) * (1f + this.staAllAdd));
			}
			return base.sta + this.equipSTA;
		}
	}

	// Token: 0x17000080 RID: 128
	// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x0004A4BD File Offset: 0x000486BD
	// (set) Token: 0x06000CF1 RID: 3313 RVA: 0x0004A4C5 File Offset: 0x000486C5
	public float AgiAllAdd
	{
		get
		{
			return this.agiAllAdd;
		}
		set
		{
			this.agiAllAdd = value;
			if (base.isLocalPlayer)
			{
				UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
				if (ui != null)
				{
					ui.RefreshPlayerStateUI();
				}
				UI_PlayerState ui2 = Game.UI.GetUI<UI_PlayerState>();
				if (ui2 == null)
				{
					return;
				}
				ui2.RefreshPlayerStateUI();
			}
		}
	}

	// Token: 0x17000081 RID: 129
	// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x0004A4FF File Offset: 0x000486FF
	public override int AGI
	{
		get
		{
			if (!Mathf.Approximately(this.agiAllAdd, 0f))
			{
				return (int)((float)(base.agi + this.equipAGI) * (1f + this.agiAllAdd));
			}
			return base.agi + this.equipAGI;
		}
	}

	// Token: 0x17000082 RID: 130
	// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x0004A53D File Offset: 0x0004873D
	// (set) Token: 0x06000CF4 RID: 3316 RVA: 0x0004A545 File Offset: 0x00048745
	public float StrAllAdd
	{
		get
		{
			return this.strAllAdd;
		}
		set
		{
			this.strAllAdd = value;
			if (base.isLocalPlayer)
			{
				UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
				if (ui != null)
				{
					ui.RefreshPlayerStateUI();
				}
				UI_PlayerState ui2 = Game.UI.GetUI<UI_PlayerState>();
				if (ui2 == null)
				{
					return;
				}
				ui2.RefreshPlayerStateUI();
			}
		}
	}

	// Token: 0x17000083 RID: 131
	// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x0004A57F File Offset: 0x0004877F
	public override int STR
	{
		get
		{
			if (!Mathf.Approximately(this.strAllAdd, 0f))
			{
				return (int)((float)(base.mSTR + this.equipSTR) * (1f + this.strAllAdd));
			}
			return base.mSTR + this.equipSTR;
		}
	}

	// Token: 0x17000084 RID: 132
	// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x0004A5BD File Offset: 0x000487BD
	// (set) Token: 0x06000CF7 RID: 3319 RVA: 0x0004A5CD File Offset: 0x000487CD
	public override float xiXue
	{
		get
		{
			return this.mXiXue + (float)this.equipXiXue;
		}
		set
		{
			this.mXiXue = value;
		}
	}

	// Token: 0x17000085 RID: 133
	// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x0004A5D6 File Offset: 0x000487D6
	public CharacterController CharacterController
	{
		get
		{
			return this.characterController;
		}
	}

	// Token: 0x06000CF9 RID: 3321 RVA: 0x0004A5DE File Offset: 0x000487DE
	public void AddInitGold(int value)
	{
		this.initGold += value;
	}

	// Token: 0x06000CFA RID: 3322 RVA: 0x0004A5EE File Offset: 0x000487EE
	public void AddInitGem(int value)
	{
		this.initGem += value;
	}

	// Token: 0x17000086 RID: 134
	// (get) Token: 0x06000CFB RID: 3323 RVA: 0x0004A5FE File Offset: 0x000487FE
	// (set) Token: 0x06000CFC RID: 3324 RVA: 0x0004A606 File Offset: 0x00048806
	public int mp { get; set; } = 400;

	// Token: 0x17000087 RID: 135
	// (get) Token: 0x06000CFD RID: 3325 RVA: 0x0004A60F File Offset: 0x0004880F
	// (set) Token: 0x06000CFE RID: 3326 RVA: 0x0004A617 File Offset: 0x00048817
	public int maxMp { get; set; } = 400;

	// Token: 0x06000CFF RID: 3327 RVA: 0x0004A620 File Offset: 0x00048820
	public void AddMpAddSec(int num)
	{
		this.mpAddSecRate += num;
	}

	// Token: 0x06000D00 RID: 3328 RVA: 0x0004A630 File Offset: 0x00048830
	[Command]
	public void CmdUpdateLucky(int value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteInt(value);
		base.SendCommandInternal(typeof(PlayerBase), "CmdUpdateLucky", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D01 RID: 3329 RVA: 0x0004A670 File Offset: 0x00048870
	[Command]
	public void CmdUpdatePickShare(bool value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteBool(value);
		base.SendCommandInternal(typeof(PlayerBase), "CmdUpdatePickShare", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x17000088 RID: 136
	// (get) Token: 0x06000D02 RID: 3330 RVA: 0x0004A6AF File Offset: 0x000488AF
	public float SkillExDamageAll
	{
		get
		{
			return this.skillExDamage + this.equipSkillDamage;
		}
	}

	// Token: 0x06000D03 RID: 3331 RVA: 0x0004A6C0 File Offset: 0x000488C0
	[Command]
	public void CmdUpdateSkillRange(float skillRangeValue)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteFloat(skillRangeValue);
		base.SendCommandInternal(typeof(PlayerBase), "CmdUpdateSkillRange", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D04 RID: 3332 RVA: 0x0004A700 File Offset: 0x00048900
	[Command]
	public void CmdUpdateSkillAddTime(float skillAddTimeValue)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteFloat(skillAddTimeValue);
		base.SendCommandInternal(typeof(PlayerBase), "CmdUpdateSkillAddTime", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x17000089 RID: 137
	// (get) Token: 0x06000D05 RID: 3333 RVA: 0x0004A73F File Offset: 0x0004893F
	public int AllSkillCd
	{
		get
		{
			return this.skillCdReduce + this.equipSkillCd;
		}
	}

	// Token: 0x06000D06 RID: 3334 RVA: 0x0004A750 File Offset: 0x00048950
	public void UpdateAttackPercent(float updateValue)
	{
		if (base.isLocalPlayer)
		{
			this.attackPercent += updateValue;
			UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
			if (ui != null)
			{
				ui.RefreshPlayerStateUI();
			}
			UI_PlayerState ui2 = Game.UI.GetUI<UI_PlayerState>();
			if (ui2 == null)
			{
				return;
			}
			ui2.RefreshPlayerStateUI();
		}
	}

	// Token: 0x06000D07 RID: 3335 RVA: 0x0004A79C File Offset: 0x0004899C
	[Command]
	public void UpdateAddHenshinTime(float updateValue)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteFloat(updateValue);
		base.SendCommandInternal(typeof(PlayerBase), "UpdateAddHenshinTime", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x1700008A RID: 138
	// (get) Token: 0x06000D08 RID: 3336 RVA: 0x0004A7DB File Offset: 0x000489DB
	public float RelicAdd
	{
		get
		{
			return this.relicAdd;
		}
	}

	// Token: 0x1700008B RID: 139
	// (get) Token: 0x06000D09 RID: 3337 RVA: 0x0004A7E3 File Offset: 0x000489E3
	public float BookAdd
	{
		get
		{
			return this.bookAdd;
		}
	}

	// Token: 0x1700008C RID: 140
	// (get) Token: 0x06000D0A RID: 3338 RVA: 0x0004A7EB File Offset: 0x000489EB
	public float ForgingAdd
	{
		get
		{
			return this.forgingAdd;
		}
	}

	// Token: 0x06000D0B RID: 3339 RVA: 0x0004A7F4 File Offset: 0x000489F4
	[Server]
	public void SetBaseMaxHp(long value)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void PlayerBase::SetBaseMaxHp(System.Int64)' called when server was not active");
			return;
		}
		this.baseMaxHp = ConstDefine.ClampMaxHp(value);
		if (!Mathf.Approximately(this.maxHpAddPercent, 0f))
		{
			base.NetworkmaxHp = ConstDefine.ClampMaxHp((double)this.baseMaxHp * (1.0 + (double)this.maxHpAddPercent));
		}
		else
		{
			base.NetworkmaxHp = this.baseMaxHp;
		}
		if (this.hp > this.maxHp)
		{
			base.Networkhp = this.maxHp;
		}
	}

	// Token: 0x06000D0C RID: 3340 RVA: 0x0004A880 File Offset: 0x00048A80
	[Command]
	public void CmdUpdateMaxHpAddPercent(float addPercent)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteFloat(addPercent);
		base.SendCommandInternal(typeof(PlayerBase), "CmdUpdateMaxHpAddPercent", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D0D RID: 3341 RVA: 0x0004A8C0 File Offset: 0x00048AC0
	[Command]
	public void CmdUpdateCastSpeed(float updateValue)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteFloat(updateValue);
		base.SendCommandInternal(typeof(PlayerBase), "CmdUpdateCastSpeed", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D0E RID: 3342 RVA: 0x0004A900 File Offset: 0x00048B00
	[ClientRpc]
	private void RpcUpdateCastSpeed(float value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteFloat(value);
		this.SendRPCInternal(typeof(PlayerBase), "RpcUpdateCastSpeed", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D0F RID: 3343 RVA: 0x0004A940 File Offset: 0x00048B40
	[Command]
	public void CmdUpdateHaloRangeAdd(float updateValue)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteFloat(updateValue);
		base.SendCommandInternal(typeof(PlayerBase), "CmdUpdateHaloRangeAdd", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D10 RID: 3344 RVA: 0x0004A980 File Offset: 0x00048B80
	[ClientRpc]
	private void RpcUpdateHaloRangeAdd(float value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteFloat(value);
		this.SendRPCInternal(typeof(PlayerBase), "RpcUpdateHaloRangeAdd", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D11 RID: 3345 RVA: 0x0004A9C0 File Offset: 0x00048BC0
	public float GetShopDiscountAdd()
	{
		if (this.shopDiscount == null)
		{
			return 0f;
		}
		int count = this.shopDiscount.Count;
		if (count == 0)
		{
			return 0f;
		}
		float num = 1f;
		for (int i = 0; i < count; i++)
		{
			num *= this.shopDiscount[i];
		}
		return num - 1f;
	}

	// Token: 0x06000D12 RID: 3346 RVA: 0x0004AA18 File Offset: 0x00048C18
	public void AddShopDiscount(float updateValue)
	{
		if (this.shopDiscount == null)
		{
			this.shopDiscount = new List<float>();
		}
		this.shopDiscount.Add(updateValue);
	}

	// Token: 0x06000D13 RID: 3347 RVA: 0x0004AA3C File Offset: 0x00048C3C
	public void RemoveShopDiscount(float updateValue)
	{
		if (this.shopDiscount == null)
		{
			return;
		}
		for (int i = this.shopDiscount.Count - 1; i > -1; i--)
		{
			if (Mathf.Approximately(this.shopDiscount[i], updateValue))
			{
				this.shopDiscount.RemoveAt(i);
				return;
			}
		}
	}

	// Token: 0x1700008D RID: 141
	// (get) Token: 0x06000D14 RID: 3348 RVA: 0x0004AA8B File Offset: 0x00048C8B
	public PlayerKingAI PlayerKingAI
	{
		get
		{
			return this.playerKingAI;
		}
	}

	// Token: 0x06000D15 RID: 3349 RVA: 0x0004AA94 File Offset: 0x00048C94
	public override void InitRoleModeBase(RoleModeBase roleModeBaseValue)
	{
		base.InitRoleModeBase(roleModeBaseValue);
		this.playerModeBase = (this.roleModeBase as PlayerModeBase);
		this.characterController = base.GetComponent<CharacterController>();
		this.characterController.center = new Vector3(0f, 1f - this.playerModeBase.modelOffsetY, 0f);
	}

	// Token: 0x06000D16 RID: 3350 RVA: 0x0004AAF0 File Offset: 0x00048CF0
	public void InitPlayer()
	{
		this.playerAttribute = new PlayerAttribute();
		this.playerAttribute.playerBase = this;
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui == null)
		{
			return;
		}
		ui.RefreshPlayerEquip();
	}

	// Token: 0x06000D17 RID: 3351 RVA: 0x0004AB20 File Offset: 0x00048D20
	public override void UpdateEvent()
	{
		base.UpdateEvent();
		if (this.roleState == RoleState.Idle || this.roleState == RoleState.Run)
		{
			if (base.hasAuthority)
			{
				this.PlayerMove(true);
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
		if (base.hasAuthority)
		{
			int count = this.roleSkillList.Count;
			for (int i = 0; i < count; i++)
			{
				this.roleSkillList[i].Update();
			}
			if (this.mp < this.maxMp)
			{
				this.addMpTime += Time.deltaTime;
				if (this.addMpTime >= 1f)
				{
					this.AddMp(this.mpAddSecRate);
					this.addMpTime = 0f;
				}
			}
			this.playerAttribute.Update();
			if (this.roleType == RoleType.King)
			{
				this.playerKingAI.UpdateEvent();
			}
		}
		this.brotatoWeaponController.UpdateEvent();
	}

	// Token: 0x06000D18 RID: 3352 RVA: 0x00039204 File Offset: 0x00037404
	protected virtual void UpdateSkill2()
	{
		RoleModeBase roleModeBase = this.roleModeBase;
		if (roleModeBase == null)
		{
			return;
		}
		roleModeBase.UpdateSkill2();
	}

	// Token: 0x06000D19 RID: 3353 RVA: 0x00039216 File Offset: 0x00037416
	protected virtual void UpdateSkill3()
	{
		RoleModeBase roleModeBase = this.roleModeBase;
		if (roleModeBase == null)
		{
			return;
		}
		roleModeBase.UpdateSkill3();
	}

	// Token: 0x06000D1A RID: 3354 RVA: 0x0004AC3C File Offset: 0x00048E3C
	protected virtual void UpdateSkill1()
	{
		if (!base.hasAuthority)
		{
			return;
		}
		if (this.isOverrideAnim)
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		this.timer += deltaTime;
		if (this.timer > 1.2f / this.aniSpeed)
		{
			base.UpdateRoleState(RoleState.Idle);
			return;
		}
		if (this.timer > 0.7f / this.aniSpeed)
		{
			float horizontal = InputManager.Horizontal;
			float vertical = InputManager.Vertical;
			if (!Mathf.Approximately(horizontal, 0f) || !Mathf.Approximately(vertical, 0f))
			{
				base.UpdateRoleState(RoleState.Run);
			}
		}
	}

	// Token: 0x06000D1B RID: 3355 RVA: 0x0004ACCC File Offset: 0x00048ECC
	public bool CheckIsInputMove(float normolizedTime)
	{
		if (this.roleType != RoleType.King && (normolizedTime > 0.5f || GameHelperClient.CanMoveCancel))
		{
			float horizontal = InputManager.Horizontal;
			float vertical = InputManager.Vertical;
			if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
			{
				base.UpdateRoleState(RoleState.Run);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000D1C RID: 3356 RVA: 0x0004AD1F File Offset: 0x00048F1F
	protected override void OnExitSkill()
	{
		RoleModeBase roleModeBase = this.roleModeBase;
		if (roleModeBase != null)
		{
			roleModeBase.OnExitSkill();
		}
		if (base.hasAuthority)
		{
			this.timer = base.GetRealAttackOffset();
		}
	}

	// Token: 0x06000D1D RID: 3357 RVA: 0x00038A55 File Offset: 0x00036C55
	protected virtual void AttackUpdate()
	{
		RoleModeBase roleModeBase = this.roleModeBase;
		if (roleModeBase == null)
		{
			return;
		}
		roleModeBase.AttackUpdate();
	}

	// Token: 0x06000D1E RID: 3358 RVA: 0x0004AD48 File Offset: 0x00048F48
	public void PlayerMove(bool isChangeState)
	{
		if (this.roleType == RoleType.King)
		{
			this.KingTrackMoveUpdate(isChangeState);
			return;
		}
		float deltaTime = Time.deltaTime;
		float horizontal = InputManager.Horizontal;
		float vertical = InputManager.Vertical;
		this.timer += deltaTime;
		if (Mathf.Approximately(horizontal, 0f) && Mathf.Approximately(vertical, 0f))
		{
			if (GameHelperClient.IsAutoBattle || ((GameHelperClient.ClickTrackRole != null || GameHelperClient.IsMoveToAttack) && this.roleModeBase.canAttack))
			{
				if (this.moveTimer < 1f)
				{
					this.moveTimer += deltaTime * 5f;
					if (this.moveTimer > 1f)
					{
						this.moveTimer = 1f;
					}
				}
				if (GameHelperClient.ClickTrackRole != null)
				{
					this.trackRoleBase = GameHelperClient.ClickTrackRole;
				}
				else
				{
					this.trackRoleBase = this.GetTrackRole(20f);
				}
				if (this.trackRoleBase == null)
				{
					if (this.characterController.enabled)
					{
						this.characterController.Move(deltaTime * Vector3.down);
					}
					if (isChangeState)
					{
						base.UpdateRoleState(RoleState.Idle);
						return;
					}
				}
				else
				{
					float distanceV = base.GetDistanceV2(this.trackRoleBase.MyTransform.position);
					if (isChangeState && distanceV < this.roleModeBase.GetAttackDistance() + this.trackRoleBase.RoleModeBase.addRange)
					{
						float realAttackOffset = base.GetRealAttackOffset();
						if (this.roleModeBase.canAttack && this.timer > realAttackOffset)
						{
							base.OnLocalStartAttack();
							base.UpdateRoleState(RoleState.Attack);
							return;
						}
						base.UpdateRoleState(RoleState.Idle);
						return;
					}
					else
					{
						base.TrackRotation(2f);
						if (this.characterController.enabled && !this.playerModeBase.myAnim.applyRootMotion)
						{
							float v2Angle = base.GetV2Angle(this.trackRoleBase.MyTransform.position);
							if (isChangeState || distanceV > 1.5f)
							{
								this.characterController.Move(Quaternion.Euler(0f, v2Angle, 0f) * Vector3.forward * (this.GetMoveSpeed() * deltaTime * this.moveTimer) + deltaTime * Vector3.down);
							}
						}
						if (isChangeState)
						{
							base.UpdateRoleState(RoleState.Run);
							return;
						}
					}
				}
			}
			else
			{
				if (Time.time - this.lastMoveTime < 1f || GameHelperClient.IsMoveToAttack)
				{
					this.trackRoleBase = this.GetTrackRole(this.roleModeBase.GetAttackDistance() + 1f);
					if (this.trackRoleBase != null)
					{
						GameHelperClient.IsMoveToAttack = true;
					}
					else
					{
						GameHelperClient.IsMoveToAttack = false;
					}
				}
				if (this.moveTimer < 1f)
				{
					this.moveTimer += deltaTime * 5f;
					if (this.moveTimer > 1f)
					{
						this.moveTimer = 1f;
					}
				}
				if (this.characterController.enabled)
				{
					this.characterController.Move(deltaTime * Vector3.down);
				}
				float realAttackOffset2 = base.GetRealAttackOffset();
				if (this.roleModeBase.canAttack && this.timer > realAttackOffset2 && isChangeState)
				{
					this.trackRoleBase = this.GetTrackRole(this.roleModeBase.GetAttackDistance());
					if (this.trackRoleBase != null)
					{
						base.OnLocalStartAttack();
						base.UpdateRoleState(RoleState.Attack);
						return;
					}
				}
				if (isChangeState)
				{
					base.UpdateRoleState(RoleState.Idle);
					return;
				}
			}
		}
		else
		{
			if (isChangeState)
			{
				base.UpdateRoleState(RoleState.Run);
			}
			if (this.moveTimer < 1f)
			{
				this.moveTimer += deltaTime * 5f;
				if (this.moveTimer > 1f)
				{
					this.moveTimer = 1f;
				}
			}
			Vector3 normalized = new Vector3(horizontal, 0f, vertical).normalized;
			if (this.trackRoleBase != null)
			{
				Vector3 lhs = this.trackRoleBase.MyTransform.position - base.MyTransform.position;
				lhs.y = 0f;
				if (Vector3.Dot(lhs, normalized) > 0f)
				{
					this.lastMoveTime = Time.time;
				}
				else
				{
					this.lastMoveTime = -1f;
				}
			}
			else
			{
				this.lastMoveTime = Time.time;
			}
			GameHelperClient.IsMoveToAttack = false;
			if (this.characterController.enabled && !this.playerModeBase.myAnim.applyRootMotion)
			{
				this.characterController.Move(normalized * (this.GetMoveSpeed() * deltaTime * this.moveTimer) + deltaTime * Vector3.down);
			}
			float num = Mathf.Atan2(horizontal, vertical) * 180f / 3.1415927f;
			float num2 = this.lerpAngle;
			if (num2 - num > 180f)
			{
				num += 360f;
			}
			else if (num2 - num < -180f)
			{
				num -= 360f;
			}
			float num3 = Mathf.Lerp(num2, num, Time.deltaTime * 15f);
			if (num3 > 180f)
			{
				num3 -= 360f;
			}
			else if (num3 < -180f)
			{
				num3 += 360f;
			}
			this.lerpAngle = num3;
			this.myTransform.localEulerAngles = new Vector3(0f, this.lerpAngle, 0f);
		}
	}

	// Token: 0x06000D1F RID: 3359 RVA: 0x0004B27C File Offset: 0x0004947C
	public RelicBase AddRelic(int index, int initLevel)
	{
		Dictionary<string, object> dictionary = (Dictionary<string, object>)ExcelManager.allExcelData["remains"].DIC(index.ToString());
		if (this.roleType == RoleType.King && dictionary.DIC("kingLock"))
		{
			return null;
		}
		string text = (string)dictionary["skill"];
		uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
		RelicBase relicBase;
		if (num <= 2238198742U)
		{
			if (num <= 1154925651U)
			{
				if (num <= 435644271U)
				{
					if (num <= 326073063U)
					{
						if (num <= 86253892U)
						{
							if (num != 9014859U)
							{
								if (num != 23010697U)
								{
									if (num == 86253892U)
									{
										if (text == "SpeedMan")
										{
											relicBase = new RelicSpeedMan();
											goto IL_13D0;
										}
									}
								}
								else if (text == "MythMan")
								{
									relicBase = new RelicMythMan();
									goto IL_13D0;
								}
							}
							else if (text == "FireMan")
							{
								relicBase = new RelicFireMan();
								goto IL_13D0;
							}
						}
						else if (num != 190119301U)
						{
							if (num != 257253533U)
							{
								if (num == 326073063U)
								{
									if (text == "ArmedAdd")
									{
										relicBase = new RelicArmedAdd();
										goto IL_13D0;
									}
								}
							}
							else if (text == "LuckyCat")
							{
								relicBase = new RelicLuckyCat();
								goto IL_13D0;
							}
						}
						else if (text == "CallLighting")
						{
							relicBase = new RelicCallLighting();
							goto IL_13D0;
						}
					}
					else if (num <= 377566267U)
					{
						if (num != 358975409U)
						{
							if (num != 362275396U)
							{
								if (num == 377566267U)
								{
									if (text == "AlchemyPotion")
									{
										relicBase = new RelicAlchemyPotion();
										goto IL_13D0;
									}
								}
							}
							else if (text == "IceMan")
							{
								relicBase = new RelicIceMan();
								goto IL_13D0;
							}
						}
						else if (text == "OnePunch")
						{
							relicBase = new RelicOnePunch();
							goto IL_13D0;
						}
					}
					else if (num != 399922361U)
					{
						if (num != 430717701U)
						{
							if (num == 435644271U)
							{
								if (text == "TwoBladeMan")
								{
									relicBase = new RelicTwoBladeMan();
									goto IL_13D0;
								}
							}
						}
						else if (text == "LuckyMan")
						{
							relicBase = new RelicLuckyMan();
							goto IL_13D0;
						}
					}
					else if (text == "Sta")
					{
						relicBase = new RelicSta();
						goto IL_13D0;
					}
				}
				else if (num <= 829229530U)
				{
					if (num <= 599491898U)
					{
						if (num != 503474442U)
						{
							if (num != 541893081U)
							{
								if (num == 599491898U)
								{
									if (text == "AddGold")
									{
										relicBase = new RelicAddGold();
										goto IL_13D0;
									}
								}
							}
							else if (text == "RichMan")
							{
								relicBase = new RelicRichMan();
								goto IL_13D0;
							}
						}
						else if (text == "ButterflyWind")
						{
							relicBase = new RelicButterflyWind();
							goto IL_13D0;
						}
					}
					else if (num != 605960633U)
					{
						if (num != 618031408U)
						{
							if (num == 829229530U)
							{
								if (text == "DemonContract")
								{
									relicBase = new RelicDemonContract();
									goto IL_13D0;
								}
							}
						}
						else if (text == "Str")
						{
							relicBase = new RelicStr();
							goto IL_13D0;
						}
					}
					else if (text == "HeavySword")
					{
						relicBase = new RelicHeavySword();
						goto IL_13D0;
					}
				}
				else if (num <= 991636046U)
				{
					if (num != 865009828U)
					{
						if (num != 869642994U)
						{
							if (num == 991636046U)
							{
								if (text == "FullyArmed")
								{
									relicBase = new RelicFullyArmed();
									goto IL_13D0;
								}
							}
						}
						else if (text == "Muscle")
						{
							relicBase = new RelicMuscle();
							goto IL_13D0;
						}
					}
					else if (text == "BrokeWind")
					{
						relicBase = new RelicBrokeWind();
						goto IL_13D0;
					}
				}
				else if (num <= 1033877364U)
				{
					if (num != 1001555304U)
					{
						if (num == 1033877364U)
						{
							if (text == "IncludedDamage")
							{
								relicBase = new RelicIncludedDamage();
								goto IL_13D0;
							}
						}
					}
					else if (text == "Scopes")
					{
						relicBase = new RelicScopes();
						goto IL_13D0;
					}
				}
				else if (num != 1041612137U)
				{
					if (num == 1154925651U)
					{
						if (text == "SkillDamage")
						{
							relicBase = new RelicSkillDamage();
							goto IL_13D0;
						}
					}
				}
				else if (text == "CriticalDamage")
				{
					relicBase = new RelicCriticalDamage();
					goto IL_13D0;
				}
			}
			else if (num <= 1844535330U)
			{
				if (num <= 1422202925U)
				{
					if (num <= 1375818801U)
					{
						if (num != 1248914015U)
						{
							if (num != 1353753645U)
							{
								if (num == 1375818801U)
								{
									if (text == "EquipMaster")
									{
										relicBase = new RelicEquipMaster();
										goto IL_13D0;
									}
								}
							}
							else if (text == "BuffCritical")
							{
								relicBase = new RelicBuffCritical();
								goto IL_13D0;
							}
						}
						else if (text == "MagicXiXue")
						{
							relicBase = new RelicMagicXiXue();
							goto IL_13D0;
						}
					}
					else if (num != 1384611911U)
					{
						if (num != 1390163588U)
						{
							if (num == 1422202925U)
							{
								if (text == "SkillBaoJi")
								{
									relicBase = new RelicSkillBaoJi();
									goto IL_13D0;
								}
							}
						}
						else if (text == "BeefEater")
						{
							relicBase = new RelicBeefEater();
							goto IL_13D0;
						}
					}
					else if (text == "AddLevelDamage")
					{
						relicBase = new RelicAddLevelDamage();
						goto IL_13D0;
					}
				}
				else if (num <= 1708233569U)
				{
					if (num != 1455708012U)
					{
						if (num != 1591602169U)
						{
							if (num == 1708233569U)
							{
								if (text == "TimeRedMoney")
								{
									relicBase = new RelicTimeRedMoney();
									goto IL_13D0;
								}
							}
						}
						else if (text == "TeamMp")
						{
							relicBase = new RelicTeamMp();
							goto IL_13D0;
						}
					}
					else if (text == "NoMage")
					{
						relicBase = new RelicNoMage();
						goto IL_13D0;
					}
				}
				else if (num <= 1749868637U)
				{
					if (num != 1741327499U)
					{
						if (num == 1749868637U)
						{
							if (text == "Override")
							{
								relicBase = new RelicOverride();
								goto IL_13D0;
							}
						}
					}
					else if (text == "STAMan")
					{
						relicBase = new RelicSTAMan();
						goto IL_13D0;
					}
				}
				else if (num != 1761917223U)
				{
					if (num == 1844535330U)
					{
						if (text == "Suffering")
						{
							relicBase = new RelicSuffering();
							goto IL_13D0;
						}
					}
				}
				else if (text == "TabooBook")
				{
					relicBase = new RelicTabooBook();
					goto IL_13D0;
				}
			}
			else if (num <= 1990646291U)
			{
				if (num <= 1911440745U)
				{
					if (num != 1848117323U)
					{
						if (num != 1894040596U)
						{
							if (num == 1911440745U)
							{
								if (text == "ExpHarvest")
								{
									relicBase = new RelicExpHarvest();
									goto IL_13D0;
								}
							}
						}
						else if (text == "TeamHp")
						{
							relicBase = new RelicTeamHp();
							goto IL_13D0;
						}
					}
					else if (text == "DeathContract")
					{
						relicBase = new RelicDeathContract();
						goto IL_13D0;
					}
				}
				else if (num != 1933526673U)
				{
					if (num != 1981494445U)
					{
						if (num == 1990646291U)
						{
							if (text == "SummonTears")
							{
								relicBase = new RelicSummonTears();
								goto IL_13D0;
							}
						}
					}
					else if (text == "Voidwalker")
					{
						relicBase = new RelicVoidwalker();
						goto IL_13D0;
					}
				}
				else if (text == "SkillCD")
				{
					relicBase = new RelicSkillCD();
					goto IL_13D0;
				}
			}
			else if (num <= 2046648535U)
			{
				if (num != 2016752703U)
				{
					if (num != 2042689401U)
					{
						if (num == 2046648535U)
						{
							if (text == "StartAddDamage")
							{
								relicBase = new RelicStartAddDamage();
								goto IL_13D0;
							}
						}
					}
					else if (text == "BloodMan")
					{
						relicBase = new RelicBloodMan();
						goto IL_13D0;
					}
				}
				else if (text == "MonsterHunter")
				{
					relicBase = new RelicMonsterHunter();
					goto IL_13D0;
				}
			}
			else if (num <= 2152981995U)
			{
				if (num != 2055923966U)
				{
					if (num == 2152981995U)
					{
						if (text == "AddGem")
						{
							relicBase = new RelicAddGem();
							goto IL_13D0;
						}
					}
				}
				else if (text == "ForgingAdd")
				{
					relicBase = new RelicForgingAdd();
					goto IL_13D0;
				}
			}
			else if (num != 2226667892U)
			{
				if (num == 2238198742U)
				{
					if (text == "Eater")
					{
						relicBase = new RelicEater();
						goto IL_13D0;
					}
				}
			}
			else if (text == "Armor")
			{
				relicBase = new RelicArmor();
				goto IL_13D0;
			}
		}
		else if (num <= 3165606577U)
		{
			if (num <= 2694719388U)
			{
				if (num <= 2515997407U)
				{
					if (num <= 2462836616U)
					{
						if (num != 2278596520U)
						{
							if (num != 2430288931U)
							{
								if (num == 2462836616U)
								{
									if (text == "Agi")
									{
										relicBase = new RelicAgi();
										goto IL_13D0;
									}
								}
							}
							else if (text == "AddHenshin")
							{
								relicBase = new RelicAddHenshin();
								goto IL_13D0;
							}
						}
						else if (text == "Critical")
						{
							relicBase = new RelicCritical();
							goto IL_13D0;
						}
					}
					else if (num != 2477531539U)
					{
						if (num != 2489755262U)
						{
							if (num == 2515997407U)
							{
								if (text == "FeiWu")
								{
									relicBase = new RelicFeiWu();
									goto IL_13D0;
								}
							}
						}
						else if (text == "AddSkillCd")
						{
							relicBase = new RelicAddSkillCd();
							goto IL_13D0;
						}
					}
					else if (text == "Withered")
					{
						relicBase = new RelicWithered();
						goto IL_13D0;
					}
				}
				else if (num <= 2621872088U)
				{
					if (num != 2543586459U)
					{
						if (num != 2584123664U)
						{
							if (num == 2621872088U)
							{
								if (text == "TeamMoney")
								{
									relicBase = new RelicTeamMoney();
									goto IL_13D0;
								}
							}
						}
						else if (text == "FastCast")
						{
							relicBase = new RelicFastCast();
							goto IL_13D0;
						}
					}
					else if (text == "Scavenger")
					{
						relicBase = new RelicScavenger();
						goto IL_13D0;
					}
				}
				else if (num != 2654411952U)
				{
					if (num != 2691014562U)
					{
						if (num == 2694719388U)
						{
							if (text == "AGIMan")
							{
								relicBase = new RelicAGIMan();
								goto IL_13D0;
							}
						}
					}
					else if (text == "MagicCombo")
					{
						relicBase = new RelicMagicCombo();
						goto IL_13D0;
					}
				}
				else if (text == "WuZi")
				{
					relicBase = new RelicWuZi();
					goto IL_13D0;
				}
			}
			else if (num <= 2847919344U)
			{
				if (num <= 2761207332U)
				{
					if (num != 2701147892U)
					{
						if (num != 2758499339U)
						{
							if (num == 2761207332U)
							{
								if (text == "AttackAddHp")
								{
									relicBase = new RelicAttackAddHp();
									goto IL_13D0;
								}
							}
						}
						else if (text == "TeamStr")
						{
							relicBase = new RelicTeamStr();
							goto IL_13D0;
						}
					}
					else if (text == "STRMan")
					{
						relicBase = new RelicSTRMan();
						goto IL_13D0;
					}
				}
				else if (num != 2769572948U)
				{
					if (num != 2810573823U)
					{
						if (num == 2847919344U)
						{
							if (text == "Guy")
							{
								relicBase = new RelicGuy();
								goto IL_13D0;
							}
						}
					}
					else if (text == "QiJiXingZhe")
					{
						relicBase = new RelicQiJiXingZhe();
						goto IL_13D0;
					}
				}
				else if (text == "SecAddMp")
				{
					relicBase = new RelicSecAddMp();
					goto IL_13D0;
				}
			}
			else if (num <= 3049164281U)
			{
				if (num != 2891527555U)
				{
					if (num != 2892517655U)
					{
						if (num == 3049164281U)
						{
							if (text == "BattleFocus")
							{
								relicBase = new RelicBattleFocus();
								goto IL_13D0;
							}
						}
					}
					else if (text == "Fitness")
					{
						relicBase = new RelicFitness();
						goto IL_13D0;
					}
				}
				else if (text == "AddExp")
				{
					relicBase = new RelicAddExp();
					goto IL_13D0;
				}
			}
			else if (num <= 3113840163U)
			{
				if (num != 3081021566U)
				{
					if (num == 3113840163U)
					{
						if (text == "SkillRange")
						{
							relicBase = new RelicSkillRange();
							goto IL_13D0;
						}
					}
				}
				else if (text == "Discount")
				{
					relicBase = new RelicDiscount();
					goto IL_13D0;
				}
			}
			else if (num != 3136781124U)
			{
				if (num == 3165606577U)
				{
					if (text == "LightMan")
					{
						relicBase = new RelicLightMan();
						goto IL_13D0;
					}
				}
			}
			else if (text == "TeamMoveSpeed")
			{
				relicBase = new RelicTeamMoveSpeed();
				goto IL_13D0;
			}
		}
		else if (num <= 3581048735U)
		{
			if (num <= 3365943077U)
			{
				if (num <= 3219619508U)
				{
					if (num != 3191505356U)
					{
						if (num != 3218173022U)
						{
							if (num == 3219619508U)
							{
								if (text == "DodgeMan")
								{
									relicBase = new RelicDodgeMan();
									goto IL_13D0;
								}
							}
						}
						else if (text == "Bin")
						{
							relicBase = new RelicBin();
							goto IL_13D0;
						}
					}
					else if (text == "MageHat")
					{
						relicBase = new RelicMageHat();
						goto IL_13D0;
					}
				}
				else if (num != 3234619453U)
				{
					if (num != 3295912346U)
					{
						if (num == 3365943077U)
						{
							if (text == "AddDamage")
							{
								relicBase = new RelicAddDamage();
								goto IL_13D0;
							}
						}
					}
					else if (text == "AddHenshinTime")
					{
						relicBase = new RelicAddHenshinTime();
						goto IL_13D0;
					}
				}
				else if (text == "Balanced")
				{
					relicBase = new RelicBalanced();
					goto IL_13D0;
				}
			}
			else if (num <= 3493294028U)
			{
				if (num != 3414555978U)
				{
					if (num != 3439798920U)
					{
						if (num == 3493294028U)
						{
							if (text == "TabooBlade")
							{
								relicBase = new RelicTabooBlade();
								goto IL_13D0;
							}
						}
					}
					else if (text == "AttackSpeed")
					{
						relicBase = new RelicAttackSpeed();
						goto IL_13D0;
					}
				}
				else if (text == "Grindstone")
				{
					relicBase = new RelicGrindstone();
					goto IL_13D0;
				}
			}
			else if (num <= 3526123425U)
			{
				if (num != 3519536084U)
				{
					if (num == 3526123425U)
					{
						if (text == "Savior")
						{
							relicBase = new RelicSavior();
							goto IL_13D0;
						}
					}
				}
				else if (text == "HpAdd")
				{
					relicBase = new RelicHpAdd();
					goto IL_13D0;
				}
			}
			else if (num != 3558132554U)
			{
				if (num == 3581048735U)
				{
					if (text == "MoveSpeed")
					{
						relicBase = new RelicMoveSpeed();
						goto IL_13D0;
					}
				}
			}
			else if (text == "Mowing")
			{
				relicBase = new RelicMowing();
				goto IL_13D0;
			}
		}
		else if (num <= 4100294365U)
		{
			if (num <= 4021403888U)
			{
				if (num != 3723948151U)
				{
					if (num != 3933273268U)
					{
						if (num == 4021403888U)
						{
							if (text == "GoldHarvest")
							{
								relicBase = new RelicGoldHarvest();
								goto IL_13D0;
							}
						}
					}
					else if (text == "MagicBlood")
					{
						relicBase = new RelicMagicBlood();
						goto IL_13D0;
					}
				}
				else if (text == "AddSummon")
				{
					relicBase = new RelicAddSummon();
					goto IL_13D0;
				}
			}
			else if (num != 4091696661U)
			{
				if (num != 4092723402U)
				{
					if (num == 4100294365U)
					{
						if (text == "AddBuff")
						{
							relicBase = new RelicAddBuff();
							goto IL_13D0;
						}
					}
				}
				else if (text == "AddMaxHp")
				{
					relicBase = new RelicAddMaxHp();
					goto IL_13D0;
				}
			}
			else if (text == "Revenge")
			{
				relicBase = new RelicRevenge();
				goto IL_13D0;
			}
		}
		else if (num <= 4152737125U)
		{
			if (num != 4127912270U)
			{
				if (num != 4144658574U)
				{
					if (num == 4152737125U)
					{
						if (text == "TimeAddMoney")
						{
							relicBase = new RelicTimeAddMoney();
							goto IL_13D0;
						}
					}
				}
				else if (text == "TimeAddDamage")
				{
					relicBase = new RelicTimeAddDamage();
					goto IL_13D0;
				}
			}
			else if (text == "GlassCannon")
			{
				relicBase = new RelicGlassCannon();
				goto IL_13D0;
			}
		}
		else if (num <= 4173709675U)
		{
			if (num != 4172402353U)
			{
				if (num == 4173709675U)
				{
					if (text == "TeamArmor")
					{
						relicBase = new RelicTeamArmor();
						goto IL_13D0;
					}
				}
			}
			else if (text == "AbyssEyes")
			{
				relicBase = new RelicAbyssEyes();
				goto IL_13D0;
			}
		}
		else if (num != 4235678502U)
		{
			if (num == 4258940124U)
			{
				if (text == "SwordMaster")
				{
					relicBase = new RelicSwordMaster();
					goto IL_13D0;
				}
			}
		}
		else if (text == "ThreeTerms")
		{
			relicBase = new RelicThreeTerms();
			goto IL_13D0;
		}
		relicBase = new RelicBase();
		IL_13D0:
		relicBase.keyIndex = index.ToString();
		relicBase.relicData = (Dictionary<string, object>)ExcelManager.allExcelData["remains"].DIC(relicBase.keyIndex);
		relicBase.quality = Game.GameData.RemainsDataDic[(ItemType)index].grade;
		relicBase.level = initLevel;
		string text2 = relicBase.relicData.DIC("values");
		if (!string.IsNullOrEmpty(text2))
		{
			relicBase.values = RelicBase.ParseValues(text2);
		}
		string text3 = relicBase.relicData.DIC("valueTypes");
		if (!string.IsNullOrEmpty(text3))
		{
			relicBase.valueTypes = RelicBase.ParseValueTypes(text3);
		}
		string text4 = relicBase.relicData.DIC("levelup");
		if (!string.IsNullOrEmpty(text4))
		{
			relicBase.levelUpValues = RelicBase.ParseValues(text4);
		}
		string icon = "Remains/" + relicBase.relicData.DIC("icon");
		this.AddRelic(relicBase, icon);
		return relicBase;
	}

	// Token: 0x06000D20 RID: 3360 RVA: 0x0004C748 File Offset: 0x0004A948
	public void AddRelic(RelicBase relicBase, string icon)
	{
		this.playerAttribute.relicList.Add(relicBase);
		relicBase.icon = icon;
		relicBase.playerBase = this;
		relicBase.Enter();
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui == null)
		{
			return;
		}
		ui.RefreshRelic();
	}

	// Token: 0x06000D21 RID: 3361 RVA: 0x0004C783 File Offset: 0x0004A983
	public void RemoveRelic(RelicBase relicBase)
	{
		relicBase.Exit();
		this.playerAttribute.relicList.Remove(relicBase);
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui == null)
		{
			return;
		}
		ui.RefreshRelic();
	}

	// Token: 0x06000D22 RID: 3362 RVA: 0x0004C7B1 File Offset: 0x0004A9B1
	public void AddRelicLevel(RelicBase relicBase)
	{
		relicBase.OnLevelUp();
	}

	// Token: 0x06000D23 RID: 3363 RVA: 0x0004C7B9 File Offset: 0x0004A9B9
	public void RedRelicLevel(RelicBase relicBase)
	{
		relicBase.OnLevelRed();
	}

	// Token: 0x06000D24 RID: 3364 RVA: 0x0004C7C1 File Offset: 0x0004A9C1
	public void AddSkillLevel(SkillBase skillBase)
	{
		skillBase.OnLevelUp();
	}

	// Token: 0x06000D25 RID: 3365 RVA: 0x0004C7C9 File Offset: 0x0004A9C9
	public void RedSkillLevel(SkillBase skillBase)
	{
		skillBase.OnLevelRed();
	}

	// Token: 0x06000D26 RID: 3366 RVA: 0x0004C7D1 File Offset: 0x0004A9D1
	protected override void OnStartRun()
	{
		base.OnStartRun();
		this.moveTimer = 0f;
	}

	// Token: 0x06000D27 RID: 3367 RVA: 0x0004C7E4 File Offset: 0x0004A9E4
	protected override void OnStartIdle()
	{
		base.OnStartIdle();
		this.moveTimer = 0f;
	}

	// Token: 0x06000D28 RID: 3368 RVA: 0x0004C7F8 File Offset: 0x0004A9F8
	public RoleBase GetTrackRole(float minDistance)
	{
		RoleBase result = null;
		List<RoleBase> attackRoles = base.GetAttackRoles();
		int count = attackRoles.Count;
		for (int i = 0; i < count; i++)
		{
			RoleBase roleBase = attackRoles[i];
			if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead())
			{
				float distanceV = base.GetDistanceV2(roleBase.MyTransform.position);
				if (distanceV < minDistance + roleBase.RoleModeBase.addRange)
				{
					result = roleBase;
					minDistance = distanceV;
				}
			}
		}
		return result;
	}

	// Token: 0x06000D29 RID: 3369 RVA: 0x0004C87C File Offset: 0x0004AA7C
	private RoleBase NewPlayerGetTrackRole(float minDistance)
	{
		RoleBase result = null;
		List<RoleBase> attackRoles = base.GetAttackRoles();
		int count = attackRoles.Count;
		for (int i = 0; i < count; i++)
		{
			RoleBase roleBase = attackRoles[i];
			if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead())
			{
				float distanceV = base.GetDistanceV2(roleBase.MyTransform.position);
				if (distanceV < minDistance + roleBase.RoleModeBase.addRange - roleBase.addHatred)
				{
					result = roleBase;
					minDistance = distanceV;
				}
			}
		}
		return result;
	}

	// Token: 0x06000D2A RID: 3370 RVA: 0x0004C908 File Offset: 0x0004AB08
	public List<RoleBase> GetCanAttackRoleList(float minDistance, int attackNumValue)
	{
		List<RoleBase> attackList = GameHelperClient.attackList;
		attackList.Clear();
		List<RoleBase> attackRoles = base.GetAttackRoles();
		int count = attackRoles.Count;
		for (int i = 0; i < count; i++)
		{
			RoleBase roleBase = attackRoles[i];
			if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead())
			{
				float distanceV = base.GetDistanceV2(roleBase.MyTransform.position);
				if (roleBase != this.trackRoleBase && distanceV < minDistance + roleBase.RoleModeBase.addRange)
				{
					attackList.Add(roleBase);
					attackNumValue--;
					if (attackNumValue == 0)
					{
						return attackList;
					}
				}
			}
		}
		return attackList;
	}

	// Token: 0x06000D2B RID: 3371 RVA: 0x0004C9AD File Offset: 0x0004ABAD
	protected override void OnStartDead()
	{
		base.OnStartDead();
		this.characterController.enabled = false;
		if (base.isLocalPlayer)
		{
			Game.UI.GetUI<UI_Battle>().OnStartDead();
		}
		this.dieNum++;
	}

	// Token: 0x06000D2C RID: 3372 RVA: 0x0004C9E8 File Offset: 0x0004ABE8
	public override void ExitDeadState()
	{
		base.ExitDeadState();
		if (this.roleType == RoleType.King)
		{
			if (GameHelperClient.isHost)
			{
				NetworkServer.UnSpawn(base.gameObject);
			}
			Game.EnemyManagerClient.RemoveEnemy(this);
			AssetManagerMirror.UnLoadPrefab(base.gameObject);
			return;
		}
		if (GameHelperClient.isHost && !GameHelperClient.isKingBattle)
		{
			Vector3 vector = GameHelperClient.spawnConfig.playerSpawnPoint[(int)(base.netId - 1U)];
			base.NetworksyncPos = vector;
			this.ClientRelifePos(vector);
			base.ServerRelife();
		}
	}

	// Token: 0x06000D2D RID: 3373 RVA: 0x0004CA68 File Offset: 0x0004AC68
	public void PickItem(ItemType itemType, int itemNum = 0)
	{
		if (Util.IsTalisman(itemType))
		{
			GameHelperClient.localPlayer.playerAttribute.AddBook(itemType, BagItemType.HuFu, "");
			return;
		}
		if (itemType <= (ItemType)GameHelperClient.RemainsNum)
		{
			this.playerAttribute.AddBook(itemType, BagItemType.Remains, itemType.ToString());
			return;
		}
		if (Util.IsMedicineItem(itemType))
		{
			this.playerAttribute.AddBook(itemType, BagItemType.UseItem, Util.GetMedicineShopId(itemType));
			return;
		}
		if (itemType < ItemType.Card_0)
		{
			if (itemType <= ItemType.Passsive_Book_S)
			{
				switch (itemType)
				{
				case ItemType.STRBook:
				{
					GameHelperClient.localPlayer.AddSTR(2);
					UI_Msg ui = Game.UI.GetUI<UI_Msg>();
					if (ui == null)
					{
						return;
					}
					ui.ShowMsg(Game.Language.Get("pickstr", ""), false);
					return;
				}
				case ItemType.AGIBook:
				{
					GameHelperClient.localPlayer.AddAGI(2);
					UI_Msg ui2 = Game.UI.GetUI<UI_Msg>();
					if (ui2 == null)
					{
						return;
					}
					ui2.ShowMsg(Game.Language.Get("pickdex", ""), false);
					return;
				}
				case ItemType.STABook:
				{
					GameHelperClient.localPlayer.AddSTA(2);
					UI_Msg ui3 = Game.UI.GetUI<UI_Msg>();
					if (ui3 == null)
					{
						return;
					}
					ui3.ShowMsg(Game.Language.Get("picksta", ""), false);
					return;
				}
				case ItemType.AllBook:
				{
					GameHelperClient.localPlayer.AddSTR(5);
					GameHelperClient.localPlayer.AddAGI(5);
					GameHelperClient.localPlayer.AddSTA(5);
					UI_Msg ui4 = Game.UI.GetUI<UI_Msg>();
					if (ui4 == null)
					{
						return;
					}
					ui4.ShowMsg(Game.Language.Get("pickallbook", ""), false);
					return;
				}
				default:
					switch (itemType)
					{
					case ItemType.Active_Book_D:
						this.playerAttribute.AddBook(itemType, BagItemType.Book, "book_1");
						return;
					case ItemType.Active_Book_C:
						this.playerAttribute.AddBook(itemType, BagItemType.Book, "book_3");
						return;
					case ItemType.Active_Book_B:
						this.playerAttribute.AddBook(itemType, BagItemType.Book, "book_5");
						return;
					case ItemType.Active_Book_A:
						this.playerAttribute.AddBook(itemType, BagItemType.Book, "book_7");
						return;
					case ItemType.Active_Book_S:
						this.playerAttribute.AddBook(itemType, BagItemType.Book, "sbook1");
						return;
					default:
						switch (itemType)
						{
						case ItemType.Passsive_Book_D:
							this.playerAttribute.AddBook(itemType, BagItemType.Book, "book_2");
							return;
						case ItemType.Passsive_Book_C:
							this.playerAttribute.AddBook(itemType, BagItemType.Book, "book_4");
							return;
						case ItemType.Passsive_Book_B:
							this.playerAttribute.AddBook(itemType, BagItemType.Book, "book_6");
							return;
						case ItemType.Passsive_Book_A:
							this.playerAttribute.AddBook(itemType, BagItemType.Book, "book_8");
							return;
						case ItemType.Passsive_Book_S:
							this.playerAttribute.AddBook(itemType, BagItemType.Book, "sbook2");
							return;
						}
						break;
					}
					break;
				}
			}
			else if (itemType <= ItemType.XieHuangBao)
			{
				if (itemType == ItemType.Pick_Sun)
				{
					GameHelperClient.localPlayer.AddGold(GameHelperClient.localPlayer.GetHeadUIPos(), 25, true);
					return;
				}
				if (itemType == ItemType.XieHuangBao)
				{
					this.playerAttribute.AddBook(itemType, BagItemType.XieHuangBao, "item_10");
					return;
				}
			}
			else
			{
				if (itemType == ItemType.Gold)
				{
					this.AddGold(base.GetHeadUIPos(), Mathf.Max(1, itemNum), false);
					return;
				}
				if (itemType == ItemType.Gem)
				{
					this.AddGem(base.GetHeadUIPos(), Mathf.Max(1, itemNum), true);
					return;
				}
			}
			this.playerAttribute.AddBook(itemType, BagItemType.UseItem, itemType.ToString());
			return;
		}
		if (GameHelperClient.IsAutoUseCard)
		{
			int cardId = itemType - ItemType.Card_0;
			Util.ShowTipsNoLanguage(PathDefine.Concat(Game.Language.Get("get", ""), string.Format(ColorDefine.NormalColor, PathDefine.Concat(Game.Language.Get("【卡牌】", ""), Game.Language.Get("card_" + cardId.ToString(), "")))));
			EntityStatic.Get<CardManager>().GetCard(cardId);
			return;
		}
		this.playerAttribute.AddBook(itemType, BagItemType.Card, itemType.ToString());
	}

	// Token: 0x06000D2E RID: 3374 RVA: 0x0004CE30 File Offset: 0x0004B030
	public void UseHuFu(ItemType itemType)
	{
		if (itemType != ItemType.Talisman_Experience)
		{
			string str = Game.Language.Get("pickhufu", "");
			ItemData itemData;
			if (Game.GameData.ItemDataDic.TryGetValue(itemType, out itemData))
			{
				UI_Msg ui = Game.UI.GetUI<UI_Msg>();
				if (ui != null)
				{
					ui.ShowMsg(str + ":" + Game.Language.Get(itemData.name, ""), false);
				}
			}
			int num = itemType.ToInt32();
			string name = ExcelManager.allExcelData["amulet"].DIC(num.ToString()).DIC("script");
			string buffName = ExcelManager.allExcelData["amulet"].DIC(num.ToString()).DIC("name");
			float lifeTime = ExcelManager.allExcelData["amulet"].DIC(num.ToString()).DIC("time");
			RoleBuff roleBuff = Util.GetRoleBuff(name);
			GameHelperClient.localPlayer.roleBuffManager.AddOneBuff(buffName, lifeTime, roleBuff);
			return;
		}
		int num2 = Random.Range(200, 1001);
		string format = Game.Language.Get("pickexpbook", "");
		UI_Msg ui2 = Game.UI.GetUI<UI_Msg>();
		if (ui2 != null)
		{
			ui2.ShowMsg(string.Format(format, num2), false);
		}
		GameHelperClient.localPlayer.GainExp(num2);
	}

	// Token: 0x06000D2F RID: 3375 RVA: 0x0004CF9C File Offset: 0x0004B19C
	[ClientRpc]
	private void ClientRelifePos(Vector3 pos)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteVector3(pos);
		this.SendRPCInternal(typeof(PlayerBase), "ClientRelifePos", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D30 RID: 3376 RVA: 0x0004CFDC File Offset: 0x0004B1DC
	protected override void OnExitDead()
	{
		base.OnExitDead();
		Game.EffectManager.PlayEffect(EffectDefine.RelifeEffect, 2f, this.syncPos, 2f);
		this.animTransform.localPosition = Vector3.zero;
		this.timer = 0f;
		this.characterController.enabled = base.hasAuthority;
		if (base.isLocalPlayer)
		{
			Game.UI.GetUI<UI_Battle>().OnExitDead();
			this.timer = base.GetRealAttackOffset();
		}
		EntityStatic.Get<AudioManager>().PlayAudioByPos("Audio/Battle_Audio/Game/teleport", this.syncPos, 1f);
	}

	// Token: 0x06000D31 RID: 3377 RVA: 0x0004D078 File Offset: 0x0004B278
	public int GainExp(int _exp)
	{
		int num = (int)((float)_exp * (1f + this.addExp));
		this.playerAttribute.NowExp += num;
		if (this.playerAttribute.NowExp >= this.playerAttribute.maxExp)
		{
			this.Level++;
			base.AddSTR(base.STRAdd);
			base.AddAGI(base.AGIAdd);
			base.AddSTA(base.STAAdd);
			GameHelperClient.localPlayer.CmdPlayEffectAddRole(EffectDefine.LevelUpEffect, 2f, base.MyTransform.position, 1f);
			Util.ShowTips("tip_levelUp");
			UI_Msg ui = Game.UI.GetUI<UI_Msg>();
			if (ui != null)
			{
				ui.ShowMsg(Game.Language.Get("levelstr", "") + base.STRAdd.ToString(), false);
			}
			UI_Msg ui2 = Game.UI.GetUI<UI_Msg>();
			if (ui2 != null)
			{
				ui2.ShowMsg(Game.Language.Get("leveldex", "") + base.AGIAdd.ToString(), false);
			}
			UI_Msg ui3 = Game.UI.GetUI<UI_Msg>();
			if (ui3 != null)
			{
				ui3.ShowMsg(Game.Language.Get("levelsta", "") + base.STAAdd.ToString(), false);
			}
			EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/玩家升级", 1f, 3f);
			Game.UI.GetUI<UI_PlayerState>().PlayerLevelUp();
			Action action = this.onPlayerLevelUp;
			if (action != null)
			{
				action();
			}
		}
		UI_DecTip ui4 = Game.UI.GetUI<UI_DecTip>();
		if (ui4 != null)
		{
			ui4.RefreshPlayerStateUI();
		}
		UI_PlayerState ui5 = Game.UI.GetUI<UI_PlayerState>();
		if (ui5 != null)
		{
			ui5.RefreshPlayerStateUI();
		}
		if (this.playerAttribute.NowExp >= this.playerAttribute.maxExp)
		{
			this.GainExp(0);
		}
		return num;
	}

	// Token: 0x06000D32 RID: 3378 RVA: 0x0004D260 File Offset: 0x0004B460
	[Command]
	public void CmdAddMusicBuff(Vector3 pos)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteVector3(pos);
		base.SendCommandInternal(typeof(PlayerBase), "CmdAddMusicBuff", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D33 RID: 3379 RVA: 0x0004D2A0 File Offset: 0x0004B4A0
	[ClientRpc]
	private void RpcAddMusicBuff(Vector3 pos)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteVector3(pos);
		this.SendRPCInternal(typeof(PlayerBase), "RpcAddMusicBuff", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D34 RID: 3380 RVA: 0x0004D2E0 File Offset: 0x0004B4E0
	[Command]
	public void CmdPlayEffect(string effectName, float lifeTime, Vector3 pos, float localScale)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteString(effectName);
		writer.WriteFloat(lifeTime);
		writer.WriteVector3(pos);
		writer.WriteFloat(localScale);
		base.SendCommandInternal(typeof(PlayerBase), "CmdPlayEffect", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D35 RID: 3381 RVA: 0x0004D340 File Offset: 0x0004B540
	[ClientRpc]
	private void RpcPlayEffect(string effectName, float lifeTime, Vector3 pos, float localScale)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteString(effectName);
		writer.WriteFloat(lifeTime);
		writer.WriteVector3(pos);
		writer.WriteFloat(localScale);
		this.SendRPCInternal(typeof(PlayerBase), "RpcPlayEffect", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D36 RID: 3382 RVA: 0x0004D3A0 File Offset: 0x0004B5A0
	[Command]
	public void CmdPlayEffectObstruction(string effectName, float lifeTime, Vector3 pos, float localScale)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteString(effectName);
		writer.WriteFloat(lifeTime);
		writer.WriteVector3(pos);
		writer.WriteFloat(localScale);
		base.SendCommandInternal(typeof(PlayerBase), "CmdPlayEffectObstruction", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D37 RID: 3383 RVA: 0x0004D400 File Offset: 0x0004B600
	[ClientRpc]
	private void RpcPlayEffectObstruction(string effectName, float lifeTime, Vector3 pos, float localScale)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteString(effectName);
		writer.WriteFloat(lifeTime);
		writer.WriteVector3(pos);
		writer.WriteFloat(localScale);
		this.SendRPCInternal(typeof(PlayerBase), "RpcPlayEffectObstruction", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D38 RID: 3384 RVA: 0x0004D460 File Offset: 0x0004B660
	[Command]
	public void CmdPlayEffectEuler(string effectName, float lifeTime, Vector3 pos, Vector3 localScale, Vector3 eulerAngles)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteString(effectName);
		writer.WriteFloat(lifeTime);
		writer.WriteVector3(pos);
		writer.WriteVector3(localScale);
		writer.WriteVector3(eulerAngles);
		base.SendCommandInternal(typeof(PlayerBase), "CmdPlayEffectEuler", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D39 RID: 3385 RVA: 0x0004D4C8 File Offset: 0x0004B6C8
	[ClientRpc]
	private void RpcPlayEffectEuler(string effectName, float lifeTime, Vector3 pos, Vector3 localScale, Vector3 eulerAngles)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteString(effectName);
		writer.WriteFloat(lifeTime);
		writer.WriteVector3(pos);
		writer.WriteVector3(localScale);
		writer.WriteVector3(eulerAngles);
		this.SendRPCInternal(typeof(PlayerBase), "RpcPlayEffectEuler", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D3A RID: 3386 RVA: 0x0004D530 File Offset: 0x0004B730
	[Command]
	public void CmdPlayEffectAddRole(string effectName, float lifeTime, Vector3 pos, float localScale)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteString(effectName);
		writer.WriteFloat(lifeTime);
		writer.WriteVector3(pos);
		writer.WriteFloat(localScale);
		base.SendCommandInternal(typeof(PlayerBase), "CmdPlayEffectAddRole", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D3B RID: 3387 RVA: 0x0004D590 File Offset: 0x0004B790
	[ClientRpc]
	private void RpcPlayEffectAddRole(string effectName, float lifeTime, Vector3 pos, float localScale)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteString(effectName);
		writer.WriteFloat(lifeTime);
		writer.WriteVector3(pos);
		writer.WriteFloat(localScale);
		this.SendRPCInternal(typeof(PlayerBase), "RpcPlayEffectAddRole", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D3C RID: 3388 RVA: 0x0004D5F0 File Offset: 0x0004B7F0
	public void InitServer(Vector3 pos, long maxHpValue, uint authorityIdValue, HeroType heroTypeValue, string steamNameValue = "")
	{
		this.SetBaseMaxHp(maxHpValue);
		base.Networkhp = this.maxHp;
		this.NetworksteamName = steamNameValue;
		if (this.roleModeBase == null)
		{
			RoleModeBase component = AssetManager.LoadPrefab(Util.GetHeroModePath(heroTypeValue), null, true).GetComponent<RoleModeBase>();
			this.InitRoleModeBase(component);
		}
		base.ServerUpdateState(RoleState.Idle);
		base.NetworksyncPos = pos;
		this.ClientRpcBornPos(pos, authorityIdValue, heroTypeValue, steamNameValue);
	}

	// Token: 0x06000D3D RID: 3389 RVA: 0x0004D65C File Offset: 0x0004B85C
	[ClientRpc]
	private void ClientRpcBornPos(Vector3 pos, uint authorityIdValue, HeroType heroTypeValue, string steamNameValue)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteVector3(pos);
		writer.WriteUInt(authorityIdValue);
		Mirror.GeneratedNetworkCode._Write_HeroType(writer, heroTypeValue);
		writer.WriteString(steamNameValue);
		this.SendRPCInternal(typeof(PlayerBase), "ClientRpcBornPos", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D3E RID: 3390 RVA: 0x0004D6BC File Offset: 0x0004B8BC
	[Command]
	public void CmdUploadCard(int[] teamCards)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_System.Int32[](writer, teamCards);
		base.SendCommandInternal(typeof(PlayerBase), "CmdUploadCard", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D3F RID: 3391 RVA: 0x0004D6FC File Offset: 0x0004B8FC
	[ClientRpc]
	public void RpcUploadCard(int[] teamCards)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_System.Int32[](writer, teamCards);
		this.SendRPCInternal(typeof(PlayerBase), "RpcUploadCard", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D40 RID: 3392 RVA: 0x0004D73C File Offset: 0x0004B93C
	[Command]
	public void CmdCreateEnemy(EnemyType enemyType, bool isRandomPos)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_EnemyType(writer, enemyType);
		writer.WriteBool(isRandomPos);
		base.SendCommandInternal(typeof(PlayerBase), "CmdCreateEnemy", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D41 RID: 3393 RVA: 0x0004D788 File Offset: 0x0004B988
	[Command]
	public void CmdCreateEnemyByPos(EnemyType enemyType, Vector3 pos)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_EnemyType(writer, enemyType);
		writer.WriteVector3(pos);
		base.SendCommandInternal(typeof(PlayerBase), "CmdCreateEnemyByPos", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D42 RID: 3394 RVA: 0x0004D7D4 File Offset: 0x0004B9D4
	[Command]
	public void CmdCreateLocalTyrant(int buyCount)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteInt(buyCount);
		base.SendCommandInternal(typeof(PlayerBase), "CmdCreateLocalTyrant", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D43 RID: 3395 RVA: 0x0004D814 File Offset: 0x0004BA14
	private Task PlayerCreateEnemy(EnemyType enemyType, bool isRandomPos, Vector3 spawnPos, string exInfo = "")
	{
		PlayerBase.<PlayerCreateEnemy>d__325 <PlayerCreateEnemy>d__;
		<PlayerCreateEnemy>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
		<PlayerCreateEnemy>d__.<>4__this = this;
		<PlayerCreateEnemy>d__.enemyType = enemyType;
		<PlayerCreateEnemy>d__.isRandomPos = isRandomPos;
		<PlayerCreateEnemy>d__.spawnPos = spawnPos;
		<PlayerCreateEnemy>d__.exInfo = exInfo;
		<PlayerCreateEnemy>d__.<>1__state = -1;
		<PlayerCreateEnemy>d__.<>t__builder.Start<PlayerBase.<PlayerCreateEnemy>d__325>(ref <PlayerCreateEnemy>d__);
		return <PlayerCreateEnemy>d__.<>t__builder.Task;
	}

	// Token: 0x06000D44 RID: 3396 RVA: 0x0004D878 File Offset: 0x0004BA78
	[TargetRpc]
	public void OnKillBlacksmith()
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		this.SendTargetRPCInternal(null, typeof(PlayerBase), "OnKillBlacksmith", writer, 0);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D45 RID: 3397 RVA: 0x0004D8B0 File Offset: 0x0004BAB0
	[Command]
	public void CmdPickItem(uint itemId)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteUInt(itemId);
		base.SendCommandInternal(typeof(PlayerBase), "CmdPickItem", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D46 RID: 3398 RVA: 0x0004D8F0 File Offset: 0x0004BAF0
	[ClientRpc]
	public void RpcPickItem(uint itemId)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteUInt(itemId);
		this.SendRPCInternal(typeof(PlayerBase), "RpcPickItem", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D47 RID: 3399 RVA: 0x0004D930 File Offset: 0x0004BB30
	[Command]
	public void CmdTeleport(uint index)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteUInt(index);
		base.SendCommandInternal(typeof(PlayerBase), "CmdTeleport", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D48 RID: 3400 RVA: 0x0004D970 File Offset: 0x0004BB70
	[Command]
	public void CmdTeleportForPos(Vector3 pos)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteVector3(pos);
		base.SendCommandInternal(typeof(PlayerBase), "CmdTeleportForPos", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D49 RID: 3401 RVA: 0x0004D9AF File Offset: 0x0004BBAF
	[Server]
	public void ServerTeleport(Vector3 pos)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void PlayerBase::ServerTeleport(UnityEngine.Vector3)' called when server was not active");
			return;
		}
		base.NetworksyncPos = pos;
		this.ClientTeleportPos(pos);
	}

	// Token: 0x06000D4A RID: 3402 RVA: 0x0004D9D4 File Offset: 0x0004BBD4
	[ClientRpc]
	private void ClientTeleportPos(Vector3 pos)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteVector3(pos);
		this.SendRPCInternal(typeof(PlayerBase), "ClientTeleportPos", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D4B RID: 3403 RVA: 0x0004DA14 File Offset: 0x0004BC14
	[Command]
	public void CmdCreateItem(BagItem bagItem)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_BagItem(writer, bagItem);
		base.SendCommandInternal(typeof(PlayerBase), "CmdCreateItem", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D4C RID: 3404 RVA: 0x0004DA54 File Offset: 0x0004BC54
	[Command]
	public void CmdCreateItemByPos(ItemType itemType, Vector3 pos)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_ItemType(writer, itemType);
		writer.WriteVector3(pos);
		base.SendCommandInternal(typeof(PlayerBase), "CmdCreateItemByPos", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D4D RID: 3405 RVA: 0x0004DAA0 File Offset: 0x0004BCA0
	[Command]
	public void CmdCreateItemByPosWithNum(ItemType itemType, Vector3 pos, int itemNum, bool isPickProtected)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_ItemType(writer, itemType);
		writer.WriteVector3(pos);
		writer.WriteInt(itemNum);
		writer.WriteBool(isPickProtected);
		base.SendCommandInternal(typeof(PlayerBase), "CmdCreateItemByPosWithNum", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D4E RID: 3406 RVA: 0x0004DB00 File Offset: 0x0004BD00
	private void CreateItemByPos(ItemType itemType, Vector3 pos, int itemNum, bool isPickProtected)
	{
		ItemStruct itemStruct = new ItemStruct();
		Vector2 pointByRadian = Util.GetPointByRadian(1f, 0f, Random.value * 360f);
		itemStruct.id = ItemManager.GetItemId();
		itemStruct.pos = new Vector3(pos.x + pointByRadian.x, 0f, pos.z + pointByRadian.y);
		itemStruct.itemType = itemType;
		itemStruct.itemNum = Mathf.Max(0, itemNum);
		itemStruct.authorityId = (isPickProtected ? base.netId : 0U);
		this.RpcCreateItem(itemStruct);
	}

	// Token: 0x06000D4F RID: 3407 RVA: 0x0004DB91 File Offset: 0x0004BD91
	public void DropGold(int num)
	{
		this.DropItemNum(ItemType.Gold, num);
	}

	// Token: 0x06000D50 RID: 3408 RVA: 0x0004DB9F File Offset: 0x0004BD9F
	public void DropGem(int num)
	{
		this.DropItemNum(ItemType.Gem, num);
	}

	// Token: 0x06000D51 RID: 3409 RVA: 0x0004DBB0 File Offset: 0x0004BDB0
	private void DropItemNum(ItemType itemType, int num)
	{
		if (num <= 0)
		{
			return;
		}
		if (itemType == ItemType.Gold)
		{
			num = Mathf.Min(num, this.gold);
			if (num <= 0)
			{
				return;
			}
			this.AddGold(base.GetHeadUIPos(), -num, false);
		}
		else
		{
			if (itemType != ItemType.Gem)
			{
				return;
			}
			num = Mathf.Min(num, this.gem);
			if (num <= 0)
			{
				return;
			}
			this.AddGem(base.GetHeadUIPos(), -num, false);
		}
		this.CmdCreateItemByPosWithNum(itemType, base.MyTransform.position, num, false);
	}

	// Token: 0x06000D52 RID: 3410 RVA: 0x0004DC30 File Offset: 0x0004BE30
	[ClientRpc]
	private void RpcCreateItem(ItemStruct item)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_ItemStruct(writer, item);
		this.SendRPCInternal(typeof(PlayerBase), "RpcCreateItem", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D53 RID: 3411 RVA: 0x0004DC70 File Offset: 0x0004BE70
	[Command]
	public void CmdCreateHeartDemon(int buyCount)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteInt(buyCount);
		base.SendCommandInternal(typeof(PlayerBase), "CmdCreateHeartDemon", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D54 RID: 3412 RVA: 0x0004DCAF File Offset: 0x0004BEAF
	public virtual void OnSkillKeyUp(int index)
	{
		this.playerModeBase.OnSkillKeyUp(index);
	}

	// Token: 0x06000D55 RID: 3413 RVA: 0x0004DCBD File Offset: 0x0004BEBD
	public override bool IsShowName()
	{
		return true;
	}

	// Token: 0x06000D56 RID: 3414 RVA: 0x0004DCC0 File Offset: 0x0004BEC0
	public void OnCloseSwitchSkill(SkillBase skill, uint skillId)
	{
		skill.isSwitch = false;
		if (skill.skillUI.switchGo.activeSelf)
		{
			skill.skillUI.switchGo.SetActive(false);
		}
		this.CmdClearSkill(skillId);
	}

	// Token: 0x06000D57 RID: 3415 RVA: 0x0004DCF4 File Offset: 0x0004BEF4
	[Command]
	public void CmdClearSkill(uint skillId)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteUInt(skillId);
		base.SendCommandInternal(typeof(PlayerBase), "CmdClearSkill", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D58 RID: 3416 RVA: 0x0004DD34 File Offset: 0x0004BF34
	[ClientRpc]
	public void RpcClearSkill(uint skillId)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteUInt(skillId);
		this.SendRPCInternal(typeof(PlayerBase), "RpcClearSkill", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D59 RID: 3417 RVA: 0x0004DD74 File Offset: 0x0004BF74
	[Command]
	public void CmdClearSkillByData(uint skillId, int clearData)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteUInt(skillId);
		writer.WriteInt(clearData);
		base.SendCommandInternal(typeof(PlayerBase), "CmdClearSkillByData", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D5A RID: 3418 RVA: 0x0004DDC0 File Offset: 0x0004BFC0
	[ClientRpc]
	public void RpcClearSkillByData(uint skillId, int clearData)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteUInt(skillId);
		writer.WriteInt(clearData);
		this.SendRPCInternal(typeof(PlayerBase), "RpcClearSkillByData", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D5B RID: 3419 RVA: 0x0004DE0C File Offset: 0x0004C00C
	[Command]
	public void CmdStartSkillAciton(uint skillId)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteUInt(skillId);
		base.SendCommandInternal(typeof(PlayerBase), "CmdStartSkillAciton", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D5C RID: 3420 RVA: 0x0004DE4C File Offset: 0x0004C04C
	[ClientRpc]
	public void RpcStartSkillAciton(uint skillId)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteUInt(skillId);
		this.SendRPCInternal(typeof(PlayerBase), "RpcStartSkillAciton", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D5D RID: 3421 RVA: 0x0004DE8C File Offset: 0x0004C08C
	[Command]
	public void CmdEndSkillAciton(uint skillId)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteUInt(skillId);
		base.SendCommandInternal(typeof(PlayerBase), "CmdEndSkillAciton", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D5E RID: 3422 RVA: 0x0004DECC File Offset: 0x0004C0CC
	[ClientRpc]
	public void RpcEndSkillAciton(uint skillId)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteUInt(skillId);
		this.SendRPCInternal(typeof(PlayerBase), "RpcEndSkillAciton", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D5F RID: 3423 RVA: 0x0004DF0C File Offset: 0x0004C10C
	[Command]
	public void CmdAddBuff(uint buffNetId, uint attackNetId, LocalBuffType localBuffType, float buffValue, float buffTime, int level)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteUInt(buffNetId);
		writer.WriteUInt(attackNetId);
		Mirror.GeneratedNetworkCode._Write_LocalBuffType(writer, localBuffType);
		writer.WriteFloat(buffValue);
		writer.WriteFloat(buffTime);
		writer.WriteInt(level);
		base.SendCommandInternal(typeof(PlayerBase), "CmdAddBuff", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D60 RID: 3424 RVA: 0x0004DF80 File Offset: 0x0004C180
	[Command]
	public void CmdRemoveuff(uint buffNetId, LocalBuffType localBuffType)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteUInt(buffNetId);
		Mirror.GeneratedNetworkCode._Write_LocalBuffType(writer, localBuffType);
		base.SendCommandInternal(typeof(PlayerBase), "CmdRemoveuff", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D61 RID: 3425 RVA: 0x0004DFCC File Offset: 0x0004C1CC
	[TargetRpc]
	public void TargetKillGoblinMine()
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		this.SendTargetRPCInternal(null, typeof(PlayerBase), "TargetKillGoblinMine", writer, 0);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D62 RID: 3426 RVA: 0x0004E004 File Offset: 0x0004C204
	public void MakeXieHuangBao()
	{
		BagItem bagItem = new BagItem();
		bagItem.bookType = ItemType.XieHuangBao;
		bagItem.bagItemType = BagItemType.XieHuangBao;
		bagItem.id = "item_10";
		GameHelperClient.localPlayer.CmdCreateItem(bagItem);
	}

	// Token: 0x06000D63 RID: 3427 RVA: 0x0004E040 File Offset: 0x0004C240
	[Command]
	public void CmdXuanYun(uint netId, float timer)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteUInt(netId);
		writer.WriteFloat(timer);
		base.SendCommandInternal(typeof(PlayerBase), "CmdXuanYun", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D64 RID: 3428 RVA: 0x0004E08C File Offset: 0x0004C28C
	[Command]
	public void CmdAddAttackTarget(List<uint> roleList)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_System.Collections.Generic.List`1<System.UInt32>(writer, roleList);
		base.SendCommandInternal(typeof(PlayerBase), "CmdAddAttackTarget", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D65 RID: 3429 RVA: 0x0004E0CC File Offset: 0x0004C2CC
	[ClientRpc]
	private void RpcAddAttackTarget(List<uint> roleList)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_System.Collections.Generic.List`1<System.UInt32>(writer, roleList);
		this.SendRPCInternal(typeof(PlayerBase), "RpcAddAttackTarget", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D66 RID: 3430 RVA: 0x0004E10C File Offset: 0x0004C30C
	[Command]
	public void CmdUpdateSyncActiveSkillEnum(ActiveSkillEnum skillEnum)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_ActiveSkillEnum(writer, skillEnum);
		base.SendCommandInternal(typeof(PlayerBase), "CmdUpdateSyncActiveSkillEnum", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D67 RID: 3431 RVA: 0x0004E14B File Offset: 0x0004C34B
	public void UpdateReduce(int updateValue)
	{
		base.reduceInjury += updateValue;
		this.CmdReduce(base.reduceInjury);
	}

	// Token: 0x06000D68 RID: 3432 RVA: 0x0004E168 File Offset: 0x0004C368
	[Command]
	public void CmdReduce(int value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteInt(value);
		base.SendCommandInternal(typeof(PlayerBase), "CmdReduce", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D69 RID: 3433 RVA: 0x0004E1A8 File Offset: 0x0004C3A8
	[ClientRpc]
	public void RpcReduce(int value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteInt(value);
		this.SendRPCInternal(typeof(PlayerBase), "RpcReduce", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D6A RID: 3434 RVA: 0x0004E1E7 File Offset: 0x0004C3E7
	public void UpdateSkillHitDamage(int updateValue)
	{
		this.skillReduction += updateValue;
	}

	// Token: 0x06000D6B RID: 3435 RVA: 0x0004E1F8 File Offset: 0x0004C3F8
	[Command]
	public void CmdEquipArmor(int value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteInt(value);
		base.SendCommandInternal(typeof(PlayerBase), "CmdEquipArmor", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D6C RID: 3436 RVA: 0x0004E238 File Offset: 0x0004C438
	[ClientRpc]
	public void RpcEquipArmor(int value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteInt(value);
		this.SendRPCInternal(typeof(PlayerBase), "RpcEquipArmor", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D6D RID: 3437 RVA: 0x0004E278 File Offset: 0x0004C478
	[Command]
	public void CmdEquipDoge(int value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteInt(value);
		base.SendCommandInternal(typeof(PlayerBase), "CmdEquipDoge", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D6E RID: 3438 RVA: 0x0004E2B8 File Offset: 0x0004C4B8
	[ClientRpc]
	public void RpcEquipDoge(int value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteInt(value);
		this.SendRPCInternal(typeof(PlayerBase), "RpcEquipDoge", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D6F RID: 3439 RVA: 0x0004E2F7 File Offset: 0x0004C4F7
	public void GetRandomPassiveSkill(string quality)
	{
		Game.SkillManager.GetRandomPassiveSkill(quality);
	}

	// Token: 0x06000D70 RID: 3440 RVA: 0x0004E304 File Offset: 0x0004C504
	public void GetRandomActiveSkill(string quality)
	{
		Game.SkillManager.GetRandomActiveSkill(quality);
	}

	// Token: 0x06000D71 RID: 3441 RVA: 0x0004E314 File Offset: 0x0004C514
	public int AddGold(Vector3 pos, int num, bool useAddGoldPercent = true)
	{
		GameHelperClient.CheckCoronationGuard();
		if (this.gold >= 1000000000)
		{
			this.gold = 1000000000;
		}
		if (num > 0)
		{
			if (useAddGoldPercent)
			{
				num = Mathf.RoundToInt((float)num * (1f + this.addGoldPercent));
				this.getGoldNum += num;
			}
			UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
			if (ui != null)
			{
				ui.ShowAddum(num, pos);
			}
		}
		this.gold = Mathf.Max(0, this.gold + num);
		GameHelperClient.TrackGold(this);
		return num;
	}

	// Token: 0x06000D72 RID: 3442 RVA: 0x0004E39C File Offset: 0x0004C59C
	public void AddGem(Vector3 pos, int num, bool isPlayerDrop = false)
	{
		GameHelperClient.CheckCoronationGuard();
		this.gem += num;
		if (num > 0)
		{
			UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
			if (ui != null)
			{
				ui.ShowAddum(num, pos);
			}
			if (!isPlayerDrop)
			{
				this.getGemNum += num;
			}
		}
		GameHelperClient.TrackGem(this);
	}

	// Token: 0x06000D73 RID: 3443 RVA: 0x0004E3F0 File Offset: 0x0004C5F0
	public void UpdateBreakShield()
	{
		float num = this.normalBreakShieldBase + this.equipNormalBreakingShield;
		if (!Mathf.Approximately(num, this.normalBreakShield))
		{
			this.normalBreakShield = num;
		}
		float num2 = this.skillBreakShieldBase + this.equipSkillBreakingShield;
		if (!Mathf.Approximately(num2, this.skillBreakShield))
		{
			this.skillBreakShield = num2;
		}
	}

	// Token: 0x06000D74 RID: 3444 RVA: 0x0004E444 File Offset: 0x0004C644
	[Command]
	public void CmdUpdateBreakShield(float newBreakShield)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteFloat(newBreakShield);
		base.SendCommandInternal(typeof(PlayerBase), "CmdUpdateBreakShield", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D75 RID: 3445 RVA: 0x0004E484 File Offset: 0x0004C684
	[Command]
	public void CmdUpdateSkillBreakShield(float newBreakShield)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteFloat(newBreakShield);
		base.SendCommandInternal(typeof(PlayerBase), "CmdUpdateSkillBreakShield", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D76 RID: 3446 RVA: 0x0004E4C4 File Offset: 0x0004C6C4
	public void AddMaxMp(int num)
	{
		if (num > 0)
		{
			this.maxMp += num;
			this.AddMp(num);
			return;
		}
		this.maxMp += num;
		if (this.mp > this.maxMp)
		{
			this.mp = this.maxMp;
		}
	}

	// Token: 0x06000D77 RID: 3447 RVA: 0x0004E513 File Offset: 0x0004C713
	public void AddMp(int num)
	{
		this.mp += num;
		if (this.mp >= this.maxMp)
		{
			this.mp = this.maxMp;
		}
		if (this.mp < 0)
		{
			this.mp = 0;
		}
	}

	// Token: 0x06000D78 RID: 3448 RVA: 0x0004E550 File Offset: 0x0004C750
	public override bool GetIsAttackWeek(AttackType attackType)
	{
		if (attackType == AttackType.Normal)
		{
			if (this.critical > 0f)
			{
				return Random.value < this.critical;
			}
		}
		else if (attackType == AttackType.Skill)
		{
			if (this.canSkillCritical && this.critical > 0f)
			{
				return Random.value < this.critical;
			}
		}
		else if (attackType == AttackType.Buff && this.canBuffCritical && this.critical > 0f)
		{
			return Random.value < this.critical;
		}
		return false;
	}

	// Token: 0x06000D79 RID: 3449 RVA: 0x0004E5CC File Offset: 0x0004C7CC
	[Command]
	public void CmdAddAllPlayerItem(ItemType itemType)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_ItemType(writer, itemType);
		base.SendCommandInternal(typeof(PlayerBase), "CmdAddAllPlayerItem", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D7A RID: 3450 RVA: 0x0004E60C File Offset: 0x0004C80C
	[ClientRpc]
	public void RpcAddAllPlayerItem(ItemType itemType)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_ItemType(writer, itemType);
		this.SendRPCInternal(typeof(PlayerBase), "RpcAddAllPlayerItem", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D7B RID: 3451 RVA: 0x0004E64C File Offset: 0x0004C84C
	[Command]
	public void CmdEliteProbabilityAdd(float probability)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteFloat(probability);
		base.SendCommandInternal(typeof(PlayerBase), "CmdEliteProbabilityAdd", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D7C RID: 3452 RVA: 0x0004E68B File Offset: 0x0004C88B
	public long GetPlayerNormalAttackPower()
	{
		return ConstDefine.ClampBattleValue((double)this.FinalAttackPower * (1.0 + (double)this.normalAttackAddDamage));
	}

	// Token: 0x06000D7D RID: 3453 RVA: 0x0004E6AC File Offset: 0x0004C8AC
	[Command]
	public void CmdUpdateAddHatred(float updateValue)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteFloat(updateValue);
		base.SendCommandInternal(typeof(PlayerBase), "CmdUpdateAddHatred", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D7E RID: 3454 RVA: 0x0004E6EC File Offset: 0x0004C8EC
	[ClientRpc]
	public void RpcUpdateAddHatred(float updateValue)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteFloat(updateValue);
		this.SendRPCInternal(typeof(PlayerBase), "RpcUpdateAddHatred", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D7F RID: 3455 RVA: 0x0004E72C File Offset: 0x0004C92C
	[Command]
	public void CmdChat(string textStr)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteString(textStr);
		base.SendCommandInternal(typeof(PlayerBase), "CmdChat", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D80 RID: 3456 RVA: 0x0004E76B File Offset: 0x0004C96B
	[Server]
	public void ServerChat(string textStr)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void PlayerBase::ServerChat(System.String)' called when server was not active");
			return;
		}
		this.RpcChat(textStr);
	}

	// Token: 0x06000D81 RID: 3457 RVA: 0x0004E78C File Offset: 0x0004C98C
	[ClientRpc]
	private void RpcChat(string textStr)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteString(textStr);
		this.SendRPCInternal(typeof(PlayerBase), "RpcChat", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D82 RID: 3458 RVA: 0x0004E7CC File Offset: 0x0004C9CC
	[Command]
	public void CmdAttackOtherPlayer(double damage, AttackType attackType, uint attackRoleId, float attackEulerY, uint hitPlayerId)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteDouble(damage);
		Mirror.GeneratedNetworkCode._Write_AttackType(writer, attackType);
		writer.WriteUInt(attackRoleId);
		writer.WriteFloat(attackEulerY);
		writer.WriteUInt(hitPlayerId);
		base.SendCommandInternal(typeof(PlayerBase), "CmdAttackOtherPlayer", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D83 RID: 3459 RVA: 0x0004E834 File Offset: 0x0004CA34
	[TargetRpc]
	private void TargetAttackOtherPlayer(double damage, AttackType attackType, uint attackRoleId, float attackEulerY)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteDouble(damage);
		Mirror.GeneratedNetworkCode._Write_AttackType(writer, attackType);
		writer.WriteUInt(attackRoleId);
		writer.WriteFloat(attackEulerY);
		this.SendTargetRPCInternal(null, typeof(PlayerBase), "TargetAttackOtherPlayer", writer, 0);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D84 RID: 3460 RVA: 0x0004E894 File Offset: 0x0004CA94
	[Command]
	public void CmdUpdateAddRelifeTime(int updateValue)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteInt(updateValue);
		base.SendCommandInternal(typeof(PlayerBase), "CmdUpdateAddRelifeTime", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D85 RID: 3461 RVA: 0x0004E8D4 File Offset: 0x0004CAD4
	[ClientRpc]
	private void RpcUpdateAddRelifeTime(int updateValue)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteInt(updateValue);
		this.SendRPCInternal(typeof(PlayerBase), "RpcUpdateAddRelifeTime", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D86 RID: 3462 RVA: 0x0004E913 File Offset: 0x0004CB13
	public void StartUploadGameOverData()
	{
		this.CmdUploadGameOverData(this.damageStatic, this.getGoldNum, this.getGemNum, this.killBossNum);
	}

	// Token: 0x06000D87 RID: 3463 RVA: 0x0004E934 File Offset: 0x0004CB34
	[Command]
	private void CmdUploadGameOverData(long damageValue, int goldNum, int gemNum, int killBossNumValue)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteLong(damageValue);
		writer.WriteInt(goldNum);
		writer.WriteInt(gemNum);
		writer.WriteInt(killBossNumValue);
		base.SendCommandInternal(typeof(PlayerBase), "CmdUploadGameOverData", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D88 RID: 3464 RVA: 0x0004E994 File Offset: 0x0004CB94
	[ClientRpc]
	private void RpcUploadGameOverData(long damageValue, int goldNum, int gemNum, int killBossNumValue)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteLong(damageValue);
		writer.WriteInt(goldNum);
		writer.WriteInt(gemNum);
		writer.WriteInt(killBossNumValue);
		this.SendRPCInternal(typeof(PlayerBase), "RpcUploadGameOverData", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D89 RID: 3465 RVA: 0x0004E9F1 File Offset: 0x0004CBF1
	public bool CanAction()
	{
		return this.hp > 0L && this.roleState != RoleState.Dead && this.roleState != RoleState.XuanYun && this.roleState != RoleState.Action;
	}

	// Token: 0x06000D8A RID: 3466 RVA: 0x0004EA1C File Offset: 0x0004CC1C
	[Command]
	public void CmdUpdateSaiYaDarkBuff(SaiYaDarkBuff.ReData data, uint buffNetId)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_SaiYaDarkBuff/ReData(writer, data);
		writer.WriteUInt(buffNetId);
		base.SendCommandInternal(typeof(PlayerBase), "CmdUpdateSaiYaDarkBuff", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D8B RID: 3467 RVA: 0x0004EA68 File Offset: 0x0004CC68
	[ClientRpc]
	private void RpcUpdateSaiYaDarkBuff(SaiYaDarkBuff.ReData data, uint buffNetId)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_SaiYaDarkBuff/ReData(writer, data);
		writer.WriteUInt(buffNetId);
		this.SendRPCInternal(typeof(PlayerBase), "RpcUpdateSaiYaDarkBuff", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D8C RID: 3468 RVA: 0x0004EAB4 File Offset: 0x0004CCB4
	[Command]
	public void CmdUpdateSaiYaSkill3(uint enemyNetId, int addNum)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteUInt(enemyNetId);
		writer.WriteInt(addNum);
		base.SendCommandInternal(typeof(PlayerBase), "CmdUpdateSaiYaSkill3", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D8D RID: 3469 RVA: 0x0004EAFD File Offset: 0x0004CCFD
	public void UpdateRelicAdd(float value)
	{
		this.relicAdd += value;
		this.CmdUpdateRelicAdd(value);
	}

	// Token: 0x06000D8E RID: 3470 RVA: 0x0004EB14 File Offset: 0x0004CD14
	[Command]
	private void CmdUpdateRelicAdd(float value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteFloat(value);
		base.SendCommandInternal(typeof(PlayerBase), "CmdUpdateRelicAdd", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D8F RID: 3471 RVA: 0x0004EB54 File Offset: 0x0004CD54
	[ClientRpc]
	private void RpcUpdateRelicAdd(float value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteFloat(value);
		this.SendRPCInternal(typeof(PlayerBase), "RpcUpdateRelicAdd", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D90 RID: 3472 RVA: 0x0004EB93 File Offset: 0x0004CD93
	public void UpdateBookAdd(float value)
	{
		this.bookAdd += value;
		this.CmdUpdateBookAdd(value);
	}

	// Token: 0x06000D91 RID: 3473 RVA: 0x0004EBAC File Offset: 0x0004CDAC
	[Command]
	private void CmdUpdateBookAdd(float value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteFloat(value);
		base.SendCommandInternal(typeof(PlayerBase), "CmdUpdateBookAdd", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D92 RID: 3474 RVA: 0x0004EBEC File Offset: 0x0004CDEC
	[ClientRpc]
	private void RpcUpdateBookAdd(float value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteFloat(value);
		this.SendRPCInternal(typeof(PlayerBase), "RpcUpdateBookAdd", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D93 RID: 3475 RVA: 0x0004EC2B File Offset: 0x0004CE2B
	public void UpdateForgingAdd(float value)
	{
		this.forgingAdd += value;
		this.CmdUpdateForgingAdd(value);
	}

	// Token: 0x06000D94 RID: 3476 RVA: 0x0004EC44 File Offset: 0x0004CE44
	[Command]
	private void CmdUpdateForgingAdd(float value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteFloat(value);
		base.SendCommandInternal(typeof(PlayerBase), "CmdUpdateForgingAdd", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D95 RID: 3477 RVA: 0x0004EC84 File Offset: 0x0004CE84
	[ClientRpc]
	private void RpcUpdateForgingAdd(float value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteFloat(value);
		this.SendRPCInternal(typeof(PlayerBase), "RpcUpdateForgingAdd", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D96 RID: 3478 RVA: 0x0004ECC3 File Offset: 0x0004CEC3
	public void SetActionCallBack(Action successCallback, Action failCallback)
	{
		this.actionSucess = successCallback;
		this.actionFail = failCallback;
	}

	// Token: 0x06000D97 RID: 3479 RVA: 0x0004ECD3 File Offset: 0x0004CED3
	protected override void OnStartAction()
	{
		base.OnStartAction();
		this.inputTime = 0f;
	}

	// Token: 0x06000D98 RID: 3480 RVA: 0x0004ECE8 File Offset: 0x0004CEE8
	protected override void UpdateAction()
	{
		base.UpdateAction();
		if (base.hasAuthority)
		{
			float deltaTime = Time.deltaTime;
			this.timer -= deltaTime;
			if (this.timer < 0f)
			{
				Action action = this.actionSucess;
				if (action != null)
				{
					action();
				}
				this.actionFail = null;
				base.UpdateRoleState(RoleState.Idle);
			}
			float horizontal = InputManager.Horizontal;
			float vertical = InputManager.Vertical;
			if (!Mathf.Approximately(horizontal, 0f) || !Mathf.Approximately(vertical, 0f))
			{
				this.inputTime += deltaTime;
				if (this.inputTime >= 0.15f)
				{
					base.UpdateRoleState(RoleState.Run);
					return;
				}
			}
			else
			{
				this.inputTime = 0f;
			}
		}
	}

	// Token: 0x06000D99 RID: 3481 RVA: 0x0004ED98 File Offset: 0x0004CF98
	protected override void OnExitAction()
	{
		base.OnExitAction();
		Action action = this.actionFail;
		if (action != null)
		{
			action();
		}
		this.actionSucess = null;
	}

	// Token: 0x06000D9A RID: 3482 RVA: 0x0004EDB8 File Offset: 0x0004CFB8
	[Command]
	public void CmdHelpNpc(uint npcId)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteUInt(npcId);
		base.SendCommandInternal(typeof(PlayerBase), "CmdHelpNpc", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D9B RID: 3483 RVA: 0x0004EDF8 File Offset: 0x0004CFF8
	public void InitKingServer(Vector3 pos, uint authorityIdValue, uint trackPlayerId, HeroType heroTypeValue, SaveLoadManager.PlayerKingData playerKingData)
	{
		this.SetBaseMaxHp(playerKingData.maxHp);
		base.Networkhp = this.maxHp;
		if (this.roleModeBase == null)
		{
			RoleModeBase component = AssetManager.LoadPrefab(Util.GetHeroModePath(heroTypeValue), null, true).GetComponent<RoleModeBase>();
			this.InitRoleModeBase(component);
		}
		base.ServerUpdateState(RoleState.Idle);
		base.NetworksyncPos = pos;
		this.ClientRpcKingBornPos(pos, authorityIdValue, trackPlayerId, heroTypeValue, playerKingData);
	}

	// Token: 0x06000D9C RID: 3484 RVA: 0x0004EE64 File Offset: 0x0004D064
	[ClientRpc]
	private void ClientRpcKingBornPos(Vector3 pos, uint authorityIdValue, uint trackPlayerId, HeroType heroTypeValue, SaveLoadManager.PlayerKingData playerKingData)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteVector3(pos);
		writer.WriteUInt(authorityIdValue);
		writer.WriteUInt(trackPlayerId);
		Mirror.GeneratedNetworkCode._Write_HeroType(writer, heroTypeValue);
		Mirror.GeneratedNetworkCode._Write_SaveLoadManager/PlayerKingData(writer, playerKingData);
		this.SendRPCInternal(typeof(PlayerBase), "ClientRpcKingBornPos", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000D9D RID: 3485 RVA: 0x0004EECC File Offset: 0x0004D0CC
	private void InitPlayerKingAI(SaveLoadManager.PlayerKingData playerKingData)
	{
		this.critical = playerKingData.critical;
		this.criticalDamage = playerKingData.criticalDamage;
		base.mAttackPower = playerKingData.attack - playerKingData.str;
		base.extendAttackSpeed = playerKingData.attackSpeed - this.attackSpeed - base.mAttackSpeed * (float)playerKingData.agi * 0.002f;
		this.xiXue = playerKingData.attackAddHp;
		this.moveSpeed = playerKingData.moveSpeed;
		base.sta = playerKingData.sta;
		base.agi = playerKingData.agi;
		base.mSTR = playerKingData.str;
		base.mArmor = playerKingData.armor;
		this.mHpAddSec = playerKingData.hpAdd;
		this.mpAddSecRate = playerKingData.mpAdd;
		this.Networklucky = playerKingData.lucky;
		this.skillExDamage = playerKingData.skillDamage;
		this.NetworkskillRange = playerKingData.skillRange;
		this.NetworkskillAddTime = playerKingData.skillTime;
		this.skillMpUsed = playerKingData.skillExpend;
		this.skillCdReduce = playerKingData.skillCd;
		this.normalAttackAddDamage = playerKingData.normalDamage;
		this.maxMp = playerKingData.maxMp;
		this.normalBreakShield = playerKingData.normalBreak;
		this.skillBreakShield = playerKingData.skillBreak;
		this.addDamagePercent = playerKingData.allDamage;
		this.xiXueLv = playerKingData.lifeStealing;
		base.reduceInjury = playerKingData.reduceInjury;
		this.extraDamage = playerKingData.extraDamage;
		this.doge = playerKingData.dodge;
		this.hpAddSecRate = playerKingData.hpSecRate;
		this.skillReduction = playerKingData.skillReduction;
		this.exAttackDistance = playerKingData.attackDistance;
		this.skillFireAdd = playerKingData.fireDamage;
		this.skillIceAdd = playerKingData.iceDamage;
		this.skillLightingAdd = playerKingData.lightDamage;
		this.skillNoneAdd = playerKingData.skillNoneDamage;
		this.addAttackEffectDamage = playerKingData.effectDamage;
		this.buffAddDamage = playerKingData.buffDamage;
		this.addRelifeTime = playerKingData.relifeTime;
		this.addCallMonsterAttack = playerKingData.addCallMonsterAttack;
		this.addCallMonsterHp = playerKingData.addCallMonsterHp;
		this.addCallMonsterSize = playerKingData.addCallMonsterSize;
		this.addCallMonsterTime = playerKingData.addCallMonsterTime;
		this.addHenshin = playerKingData.addHenshin;
		this.NetworkaddHenshinTime = playerKingData.addHenshinTime;
		this.castSpeed = playerKingData.castSpeed;
		this.magicXiXue = playerKingData.magicXiXue;
		this.hpAddUpgrade = playerKingData.hpAddUpgrade;
		this.addHatred = playerKingData.addHatred;
		this.haloRangeAdd = playerKingData.haloRangeAdd;
		this.armedAdd = playerKingData.armedAdd;
		this.equipAddValue = playerKingData.equipAdd;
		this.level = playerKingData.level;
		this.getGoldNum = playerKingData.allMoney;
		this.getGemNum = playerKingData.allGem;
	}

	// Token: 0x06000D9E RID: 3486 RVA: 0x0004F178 File Offset: 0x0004D378
	private void KingTrackMoveUpdate(bool isChangeState)
	{
		if (this.trackRoleBase == null || this.trackRoleBase.IsDead())
		{
			this.trackRoleBase = this.NewPlayerGetTrackRole(20f);
			if (this.trackRoleBase == null || this.trackRoleBase.IsDead())
			{
				base.UpdateRoleState(RoleState.Idle);
				return;
			}
		}
		PlayerKingAI.KingAISkillCheck kingAISkillCheck = this.playerKingAI.StartAIAttack();
		if (kingAISkillCheck.skill != ActiveSkillEnum.None && isChangeState)
		{
			ActiveSkillData activeSkillData = Game.GameData.ActiveSkillDataDic[kingAISkillCheck.skill];
			Vector3 position2;
			float attackRotation;
			if (activeSkillData.indicator.Equals(IndicatorDefine.Vector))
			{
				Vector3 position = this.trackRoleBase.MyTransform.position;
				float moveAngle = base.GetMoveAngle(new Vector2(position.x - base.MyTransform.position.x, position.z - base.MyTransform.position.z));
				base.SetSyncRotationY(moveAngle);
				position2 = base.MyTransform.position;
				attackRotation = moveAngle;
			}
			else
			{
				attackRotation = base.GetV2Angle(this.trackRoleBase.MyTransform.position);
				position2 = this.trackRoleBase.MyTransform.position;
			}
			position2.y = 0f;
			if (Util.OnLocalCmdSkill(this, kingAISkillCheck.skill, position2, activeSkillData, attackRotation, (int)this.trackRoleBase.netId, -1))
			{
				this.playerKingAI.SetCd(kingAISkillCheck);
			}
			return;
		}
		float deltaTime = Time.deltaTime;
		this.timer += deltaTime;
		this.oldRotation = this.myTransform.localEulerAngles.y;
		Vector3 position3 = this.myTransform.position;
		Vector3 position4 = this.trackRoleBase.MyTransform.position;
		Vector2 vector = new Vector2(position4.x - position3.x, position4.z - position3.z);
		float num = vector.x * vector.x + vector.y * vector.y;
		float num2 = this.roleModeBase.GetAttackDistance() + this.trackRoleBase.RoleModeBase.addRange;
		float realAttackOffset = base.GetRealAttackOffset();
		if (num >= num2 * num2 || this.timer <= realAttackOffset)
		{
			base.UpdateRoleState(RoleState.Run);
			float moveAngle2 = base.GetMoveAngle(vector);
			base.PingHuaZhuanShen(moveAngle2, 2f);
			if (this.moveTimer < 1f)
			{
				this.moveTimer += deltaTime * 5f;
				if (this.moveTimer > 1f)
				{
					this.moveTimer = 1f;
				}
			}
			if (this.characterController.enabled && !this.playerModeBase.myAnim.applyRootMotion)
			{
				this.characterController.Move(this.myTransform.forward * (this.GetMoveSpeed() * deltaTime * this.moveTimer) + deltaTime * Vector3.down);
			}
			return;
		}
		float moveAngle3 = base.GetMoveAngle(vector);
		base.PingHuaZhuanShen(moveAngle3, 2f);
		if (!this.roleModeBase.canAttack || this.trackRoleBase.IsDead())
		{
			base.UpdateRoleState(RoleState.Idle);
			return;
		}
		base.OnLocalStartAttack();
		base.UpdateRoleState(RoleState.Attack);
	}

	// Token: 0x06000D9F RID: 3487 RVA: 0x0004F4A7 File Offset: 0x0004D6A7
	public void ClearSwitchSkill(ActiveSkillEnum activeSkill)
	{
		PlayerKingAI playerKingAI = this.playerKingAI;
		if (playerKingAI == null)
		{
			return;
		}
		playerKingAI.ClearSwitchSkill(activeSkill);
	}

	// Token: 0x06000DA0 RID: 3488 RVA: 0x0004F4BA File Offset: 0x0004D6BA
	public void StartSwitchSkill(ActiveSkillEnum activeSkill)
	{
		PlayerKingAI playerKingAI = this.playerKingAI;
		if (playerKingAI == null)
		{
			return;
		}
		playerKingAI.StartSwitchSkill(activeSkill);
	}

	// Token: 0x06000DA1 RID: 3489 RVA: 0x0004F4CD File Offset: 0x0004D6CD
	protected override void OnStartXuanYun()
	{
		base.OnStartXuanYun();
		this.canXuanYunLastTime = 4f;
	}

	// Token: 0x06000DA2 RID: 3490 RVA: 0x0004F4E0 File Offset: 0x0004D6E0
	protected override void OnExitXuanYun()
	{
		base.OnExitXuanYun();
		this.canXuanYunLastTime = 4f;
	}

	// Token: 0x06000DA3 RID: 3491 RVA: 0x0004F4F4 File Offset: 0x0004D6F4
	public void LoadKingDataForLocal(SaveLoadManager.PlayerKingData playerKingData)
	{
		if (playerKingData.skill != null && playerKingData.skill.Length != 0)
		{
			int num = playerKingData.skill.Length;
			for (int i = 1; i < num; i++)
			{
				string[] array = playerKingData.skill[i].skillName.Split("_", StringSplitOptions.None);
				if (array[0].Equals("a"))
				{
					ActiveSkillEnum activeSkill = (ActiveSkillEnum)int.Parse(array[1]);
					GameHelperClient.localPlayer.AddActiveSkillBook(activeSkill, null);
				}
				else
				{
					string s = array[1];
					GameHelperClient.localPlayer.AddPasssiveSkillBook((PasssiveSkillEnum)int.Parse(s), null);
				}
			}
		}
		if (playerKingData.equip != null && playerKingData.equip.Length != 0)
		{
			for (int j = 0; j < playerKingData.equip.Length; j++)
			{
				SaveLoadManager.PlayerKingEquipData playerKingEquipData = playerKingData.equip[j];
				List<EquipEvolutionEntryData> skillEntries = EquipEvolutionEntryData.GetSkillEntries(playerKingEquipData.equip, playerKingEquipData.equipEvolutionSkill);
				ShopManager.OnBuyEquipSuccess("equip_" + playerKingEquipData.equip, playerKingEquipData.equipData, skillEntries);
			}
		}
		List<RelicBase> relicList = this.playerAttribute.relicList;
		for (int k = relicList.Count - 1; k > -1; k--)
		{
			this.RemoveRelic(relicList[k]);
		}
		if (playerKingData.relic != null && playerKingData.relic.Length != 0)
		{
			for (int l = 0; l < playerKingData.relic.Length; l++)
			{
				SaveLoadManager.PlayerKingRelicData playerKingRelicData = playerKingData.relic[l];
				this.AddRelic(int.Parse(playerKingRelicData.relicName), playerKingRelicData.relicLevel);
			}
		}
		this.InitPlayerKingAI(playerKingData);
		base.CmdDoge(this.doge);
		this.CmdReduce(base.reduceInjury);
		this.SetBaseMaxHp(playerKingData.maxHp);
		base.Networkhp = this.maxHp;
		if (base.isLocalPlayer)
		{
			UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
			if (ui != null)
			{
				ui.RefreshPlayerStateUI();
			}
			UI_PlayerState ui2 = Game.UI.GetUI<UI_PlayerState>();
			if (ui2 == null)
			{
				return;
			}
			ui2.RefreshPlayerStateUI();
		}
	}

	// Token: 0x06000DA4 RID: 3492 RVA: 0x0004F6D9 File Offset: 0x0004D8D9
	public void UploadLocalKingData()
	{
		this.CmdUploadLocalKingData(Util.GetLocalPlayerKingData());
	}

	// Token: 0x06000DA5 RID: 3493 RVA: 0x0004F6E8 File Offset: 0x0004D8E8
	[Command]
	private void CmdUploadLocalKingData(SaveLoadManager.PlayerKingData playerKingData)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_SaveLoadManager/PlayerKingData(writer, playerKingData);
		base.SendCommandInternal(typeof(PlayerBase), "CmdUploadLocalKingData", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000DA6 RID: 3494 RVA: 0x0004F728 File Offset: 0x0004D928
	[Command]
	public void CmdAddBrotatoWeapon(BrotatoWeaponType brotatoWeaponType, uint skillId, float[] skillValues, int grade)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_BrotatoWeaponType(writer, brotatoWeaponType);
		writer.WriteUInt(skillId);
		Mirror.GeneratedNetworkCode._Write_System.Single[](writer, skillValues);
		writer.WriteInt(grade);
		base.SendCommandInternal(typeof(PlayerBase), "CmdAddBrotatoWeapon", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000DA7 RID: 3495 RVA: 0x0004F788 File Offset: 0x0004D988
	[ClientRpc]
	private void RpcAddBrotatoWeapon(BrotatoWeaponType brotatoWeaponType, uint skillId, float[] skillValues, int grade)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_BrotatoWeaponType(writer, brotatoWeaponType);
		writer.WriteUInt(skillId);
		Mirror.GeneratedNetworkCode._Write_System.Single[](writer, skillValues);
		writer.WriteInt(grade);
		this.SendRPCInternal(typeof(PlayerBase), "RpcAddBrotatoWeapon", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000DA8 RID: 3496 RVA: 0x0004F7E8 File Offset: 0x0004D9E8
	[Command]
	public void CmdRemoveBrotatoWeapon(uint skillId)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteUInt(skillId);
		base.SendCommandInternal(typeof(PlayerBase), "CmdRemoveBrotatoWeapon", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000DA9 RID: 3497 RVA: 0x0004F828 File Offset: 0x0004DA28
	[ClientRpc]
	private void RpcRemoveBrotatoWeapon(uint skillId)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteUInt(skillId);
		this.SendRPCInternal(typeof(PlayerBase), "RpcRemoveBrotatoWeapon", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000DAA RID: 3498 RVA: 0x0004F868 File Offset: 0x0004DA68
	public int GetSkillChargingMax(ActiveSkillEnum activeSkillEnum, int chargingMax)
	{
		if (activeSkillEnum == ActiveSkillEnum.PlantBomb)
		{
			ActiveSkillData activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillEnum];
			int[] plantBombSkillData = PlantBombActiveSkill.PlantBombSkillData;
			int num = 0;
			for (int i = 0; i < plantBombSkillData.Length; i++)
			{
				if (this.AGI > plantBombSkillData[i])
				{
					num = i + 1;
				}
			}
			if (this.playerAttribute.cardSkillListDic.ContainsKey(CardSkillType.ArtExplosion))
			{
				num += 2;
			}
			return activeSkillData.chargingNum + num;
		}
		return chargingMax;
	}

	// Token: 0x06000DAB RID: 3499 RVA: 0x0004F8D8 File Offset: 0x0004DAD8
	public float GetSkillChargingCd(ActiveSkillEnum activeSkillEnum, float chargingCd)
	{
		if (activeSkillEnum == ActiveSkillEnum.PlantBomb)
		{
			ActiveSkillData activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillEnum];
			int[] plantBombSkillData = PlantBombActiveSkill.PlantBombSkillData;
			int num = 0;
			for (int i = 0; i < plantBombSkillData.Length; i++)
			{
				if (this.AGI > plantBombSkillData[i])
				{
					num = i + 1;
				}
			}
			return activeSkillData.chargingCd - (float)num * 0.5f;
		}
		return chargingCd;
	}

	// Token: 0x06000DAC RID: 3500 RVA: 0x0004F934 File Offset: 0x0004DB34
	public int GetBrotatoWeaponCount()
	{
		return this.brotatoWeaponController.GetBrotatoWeaponCount();
	}

	// Token: 0x06000DAD RID: 3501 RVA: 0x0004F944 File Offset: 0x0004DB44
	public SkillBase GetSkillByBookId(int skillBookId)
	{
		int count = this.roleSkillList.Count;
		for (int i = 0; i < count; i++)
		{
			SkillBase skillBase = this.roleSkillList[i];
			if (skillBase.skillBookId == skillBookId)
			{
				return skillBase;
			}
		}
		return null;
	}

	// Token: 0x06000DAE RID: 3502 RVA: 0x0004F984 File Offset: 0x0004DB84
	[Command]
	public void CmdAddEntryConditions(int value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteInt(value);
		base.SendCommandInternal(typeof(PlayerBase), "CmdAddEntryConditions", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000DAF RID: 3503 RVA: 0x0004F9C4 File Offset: 0x0004DBC4
	[ClientRpc]
	private void RpdAddEntryConditions(int value)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteInt(value);
		this.SendRPCInternal(typeof(PlayerBase), "RpdAddEntryConditions", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000DB0 RID: 3504 RVA: 0x0004FA04 File Offset: 0x0004DC04
	[TargetRpc]
	public void TargetCreateDemonContract(Vector3 pos)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		writer.WriteVector3(pos);
		this.SendTargetRPCInternal(null, typeof(PlayerBase), "TargetCreateDemonContract", writer, 0);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000DB1 RID: 3505 RVA: 0x0004FA44 File Offset: 0x0004DC44
	[Command]
	public void CmdDemonContract()
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		base.SendCommandInternal(typeof(PlayerBase), "CmdDemonContract", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000DB2 RID: 3506 RVA: 0x0004FA7C File Offset: 0x0004DC7C
	[Command]
	public void CmdDummy(EnemyType enemyType, Vector3 pos, uint playerId, float newAttackSpeed, int newHp, int newAttackPower, float summonDeadTimeValue)
	{
		PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
		Mirror.GeneratedNetworkCode._Write_EnemyType(writer, enemyType);
		writer.WriteVector3(pos);
		writer.WriteUInt(playerId);
		writer.WriteFloat(newAttackSpeed);
		writer.WriteInt(newHp);
		writer.WriteInt(newAttackPower);
		writer.WriteFloat(summonDeadTimeValue);
		base.SendCommandInternal(typeof(PlayerBase), "CmdDummy", writer, 0, true);
		NetworkWriterPool.Recycle(writer);
	}

	// Token: 0x06000DB3 RID: 3507 RVA: 0x0004FAF8 File Offset: 0x0004DCF8
	private Task AddDummy(EnemyType enemyType, Vector3 pos, uint playerId, float newAttackSpeed, int newHp, int newAttackPower, float summonDeadTimeValue)
	{
		PlayerBase.<AddDummy>d__437 <AddDummy>d__;
		<AddDummy>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
		<AddDummy>d__.<>4__this = this;
		<AddDummy>d__.enemyType = enemyType;
		<AddDummy>d__.pos = pos;
		<AddDummy>d__.playerId = playerId;
		<AddDummy>d__.newAttackSpeed = newAttackSpeed;
		<AddDummy>d__.newHp = newHp;
		<AddDummy>d__.newAttackPower = newAttackPower;
		<AddDummy>d__.summonDeadTimeValue = summonDeadTimeValue;
		<AddDummy>d__.<>1__state = -1;
		<AddDummy>d__.<>t__builder.Start<PlayerBase.<AddDummy>d__437>(ref <AddDummy>d__);
		return <AddDummy>d__.<>t__builder.Task;
	}

	// Token: 0x06000DB5 RID: 3509 RVA: 0x00002D1D File Offset: 0x00000F1D
	private void MirrorProcessed()
	{
	}

	// Token: 0x1700008E RID: 142
	// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x0004FBF0 File Offset: 0x0004DDF0
	// (set) Token: 0x06000DB7 RID: 3511 RVA: 0x0004FC04 File Offset: 0x0004DE04
	public string NetworksteamName
	{
		get
		{
			return this.steamName;
		}
		[param: In]
		set
		{
			if (!NetworkBehaviour.SyncVarEqual<string>(value, ref this.steamName))
			{
				string text = this.steamName;
				base.SetSyncVar<string>(value, ref this.steamName, 512UL);
			}
		}
	}

	// Token: 0x1700008F RID: 143
	// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x0004FC44 File Offset: 0x0004DE44
	// (set) Token: 0x06000DB9 RID: 3513 RVA: 0x0004FC58 File Offset: 0x0004DE58
	public bool NetworkisPickShare
	{
		get
		{
			return this.isPickShare;
		}
		[param: In]
		set
		{
			if (!NetworkBehaviour.SyncVarEqual<bool>(value, ref this.isPickShare))
			{
				bool flag = this.isPickShare;
				base.SetSyncVar<bool>(value, ref this.isPickShare, 1024UL);
			}
		}
	}

	// Token: 0x17000090 RID: 144
	// (get) Token: 0x06000DBA RID: 3514 RVA: 0x0004FC98 File Offset: 0x0004DE98
	// (set) Token: 0x06000DBB RID: 3515 RVA: 0x0004FCAC File Offset: 0x0004DEAC
	public int NetworkkillEnemyNum
	{
		get
		{
			return this.killEnemyNum;
		}
		[param: In]
		set
		{
			if (!NetworkBehaviour.SyncVarEqual<int>(value, ref this.killEnemyNum))
			{
				int num = this.killEnemyNum;
				base.SetSyncVar<int>(value, ref this.killEnemyNum, 2048UL);
			}
		}
	}

	// Token: 0x17000091 RID: 145
	// (get) Token: 0x06000DBC RID: 3516 RVA: 0x0004FCEC File Offset: 0x0004DEEC
	// (set) Token: 0x06000DBD RID: 3517 RVA: 0x0004FD00 File Offset: 0x0004DF00
	public int Networklucky
	{
		get
		{
			return this.lucky;
		}
		[param: In]
		set
		{
			if (!NetworkBehaviour.SyncVarEqual<int>(value, ref this.lucky))
			{
				int num = this.lucky;
				base.SetSyncVar<int>(value, ref this.lucky, 4096UL);
			}
		}
	}

	// Token: 0x17000092 RID: 146
	// (get) Token: 0x06000DBE RID: 3518 RVA: 0x0004FD40 File Offset: 0x0004DF40
	// (set) Token: 0x06000DBF RID: 3519 RVA: 0x0004FD54 File Offset: 0x0004DF54
	public float NetworkskillRange
	{
		get
		{
			return this.skillRange;
		}
		[param: In]
		set
		{
			if (!NetworkBehaviour.SyncVarEqual<float>(value, ref this.skillRange))
			{
				float num = this.skillRange;
				base.SetSyncVar<float>(value, ref this.skillRange, 8192UL);
			}
		}
	}

	// Token: 0x17000093 RID: 147
	// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x0004FD94 File Offset: 0x0004DF94
	// (set) Token: 0x06000DC1 RID: 3521 RVA: 0x0004FDA8 File Offset: 0x0004DFA8
	public float NetworkskillAddTime
	{
		get
		{
			return this.skillAddTime;
		}
		[param: In]
		set
		{
			if (!NetworkBehaviour.SyncVarEqual<float>(value, ref this.skillAddTime))
			{
				float num = this.skillAddTime;
				base.SetSyncVar<float>(value, ref this.skillAddTime, 16384UL);
			}
		}
	}

	// Token: 0x17000094 RID: 148
	// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x0004FDE8 File Offset: 0x0004DFE8
	// (set) Token: 0x06000DC3 RID: 3523 RVA: 0x0004FDFC File Offset: 0x0004DFFC
	public float NetworkaddHenshinTime
	{
		get
		{
			return this.addHenshinTime;
		}
		[param: In]
		set
		{
			if (!NetworkBehaviour.SyncVarEqual<float>(value, ref this.addHenshinTime))
			{
				float num = this.addHenshinTime;
				base.SetSyncVar<float>(value, ref this.addHenshinTime, 32768UL);
			}
		}
	}

	// Token: 0x17000095 RID: 149
	// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x0004FE3C File Offset: 0x0004E03C
	// (set) Token: 0x06000DC5 RID: 3525 RVA: 0x0004FE50 File Offset: 0x0004E050
	public float NetworkmaxHpAddPercent
	{
		get
		{
			return this.maxHpAddPercent;
		}
		[param: In]
		set
		{
			if (!NetworkBehaviour.SyncVarEqual<float>(value, ref this.maxHpAddPercent))
			{
				float num = this.maxHpAddPercent;
				base.SetSyncVar<float>(value, ref this.maxHpAddPercent, 65536UL);
			}
		}
	}

	// Token: 0x17000096 RID: 150
	// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x0004FE90 File Offset: 0x0004E090
	// (set) Token: 0x06000DC7 RID: 3527 RVA: 0x0004FEA4 File Offset: 0x0004E0A4
	public ActiveSkillEnum NetworksyncActiveSkillEnum
	{
		get
		{
			return this.syncActiveSkillEnum;
		}
		[param: In]
		set
		{
			if (!NetworkBehaviour.SyncVarEqual<ActiveSkillEnum>(value, ref this.syncActiveSkillEnum))
			{
				ActiveSkillEnum activeSkillEnum = this.syncActiveSkillEnum;
				base.SetSyncVar<ActiveSkillEnum>(value, ref this.syncActiveSkillEnum, 131072UL);
			}
		}
	}

	// Token: 0x06000DC8 RID: 3528 RVA: 0x0004FEE3 File Offset: 0x0004E0E3
	protected void UserCode_CmdUpdateLucky(int value)
	{
		this.Networklucky = this.lucky + value;
	}

	// Token: 0x06000DC9 RID: 3529 RVA: 0x0004FEF3 File Offset: 0x0004E0F3
	protected static void InvokeUserCode_CmdUpdateLucky(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdUpdateLucky called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdUpdateLucky(reader.ReadInt());
	}

	// Token: 0x06000DCA RID: 3530 RVA: 0x0004FF1C File Offset: 0x0004E11C
	protected void UserCode_CmdUpdatePickShare(bool value)
	{
		this.NetworkisPickShare = value;
	}

	// Token: 0x06000DCB RID: 3531 RVA: 0x0004FF25 File Offset: 0x0004E125
	protected static void InvokeUserCode_CmdUpdatePickShare(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdUpdatePickShare called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdUpdatePickShare(reader.ReadBool());
	}

	// Token: 0x06000DCC RID: 3532 RVA: 0x0004FF4E File Offset: 0x0004E14E
	protected void UserCode_CmdUpdateSkillRange(float skillRangeValue)
	{
		this.NetworkskillRange = this.skillRange + skillRangeValue;
	}

	// Token: 0x06000DCD RID: 3533 RVA: 0x0004FF5E File Offset: 0x0004E15E
	protected static void InvokeUserCode_CmdUpdateSkillRange(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdUpdateSkillRange called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdUpdateSkillRange(reader.ReadFloat());
	}

	// Token: 0x06000DCE RID: 3534 RVA: 0x0004FF88 File Offset: 0x0004E188
	protected void UserCode_CmdUpdateSkillAddTime(float skillAddTimeValue)
	{
		this.NetworkskillAddTime = this.skillAddTime + skillAddTimeValue;
	}

	// Token: 0x06000DCF RID: 3535 RVA: 0x0004FF98 File Offset: 0x0004E198
	protected static void InvokeUserCode_CmdUpdateSkillAddTime(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdUpdateSkillAddTime called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdUpdateSkillAddTime(reader.ReadFloat());
	}

	// Token: 0x06000DD0 RID: 3536 RVA: 0x0004FFC2 File Offset: 0x0004E1C2
	protected void UserCode_UpdateAddHenshinTime(float updateValue)
	{
		this.NetworkaddHenshinTime = this.addHenshinTime + updateValue;
	}

	// Token: 0x06000DD1 RID: 3537 RVA: 0x0004FFD2 File Offset: 0x0004E1D2
	protected static void InvokeUserCode_UpdateAddHenshinTime(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command UpdateAddHenshinTime called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_UpdateAddHenshinTime(reader.ReadFloat());
	}

	// Token: 0x06000DD2 RID: 3538 RVA: 0x0004FFFC File Offset: 0x0004E1FC
	protected void UserCode_CmdUpdateMaxHpAddPercent(float addPercent)
	{
		this.NetworkmaxHpAddPercent = this.maxHpAddPercent + addPercent;
		long maxHp = this.maxHp;
		base.NetworkmaxHp = ConstDefine.ClampMaxHp((double)this.baseMaxHp * (1.0 + (double)this.maxHpAddPercent));
		long num = this.maxHp - maxHp;
		if (num > 0L)
		{
			base.ServerUpdateHp(num);
		}
		if (this.hp > this.maxHp)
		{
			base.Networkhp = this.maxHp;
		}
	}

	// Token: 0x06000DD3 RID: 3539 RVA: 0x00050072 File Offset: 0x0004E272
	protected static void InvokeUserCode_CmdUpdateMaxHpAddPercent(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdUpdateMaxHpAddPercent called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdUpdateMaxHpAddPercent(reader.ReadFloat());
	}

	// Token: 0x06000DD4 RID: 3540 RVA: 0x0005009C File Offset: 0x0004E29C
	protected void UserCode_CmdUpdateCastSpeed(float updateValue)
	{
		this.castSpeed += updateValue;
		this.RpcUpdateCastSpeed(this.castSpeed);
	}

	// Token: 0x06000DD5 RID: 3541 RVA: 0x000500B8 File Offset: 0x0004E2B8
	protected static void InvokeUserCode_CmdUpdateCastSpeed(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdUpdateCastSpeed called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdUpdateCastSpeed(reader.ReadFloat());
	}

	// Token: 0x06000DD6 RID: 3542 RVA: 0x000500E2 File Offset: 0x0004E2E2
	protected void UserCode_RpcUpdateCastSpeed(float value)
	{
		this.castSpeed = value;
	}

	// Token: 0x06000DD7 RID: 3543 RVA: 0x000500EB File Offset: 0x0004E2EB
	protected static void InvokeUserCode_RpcUpdateCastSpeed(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcUpdateCastSpeed called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcUpdateCastSpeed(reader.ReadFloat());
	}

	// Token: 0x06000DD8 RID: 3544 RVA: 0x00050115 File Offset: 0x0004E315
	protected void UserCode_CmdUpdateHaloRangeAdd(float updateValue)
	{
		this.haloRangeAdd += updateValue;
		this.RpcUpdateHaloRangeAdd(this.haloRangeAdd);
	}

	// Token: 0x06000DD9 RID: 3545 RVA: 0x00050131 File Offset: 0x0004E331
	protected static void InvokeUserCode_CmdUpdateHaloRangeAdd(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdUpdateHaloRangeAdd called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdUpdateHaloRangeAdd(reader.ReadFloat());
	}

	// Token: 0x06000DDA RID: 3546 RVA: 0x0005015B File Offset: 0x0004E35B
	protected void UserCode_RpcUpdateHaloRangeAdd(float value)
	{
		this.haloRangeAdd = value;
	}

	// Token: 0x06000DDB RID: 3547 RVA: 0x00050164 File Offset: 0x0004E364
	protected static void InvokeUserCode_RpcUpdateHaloRangeAdd(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcUpdateHaloRangeAdd called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcUpdateHaloRangeAdd(reader.ReadFloat());
	}

	// Token: 0x06000DDC RID: 3548 RVA: 0x00050190 File Offset: 0x0004E390
	protected void UserCode_ClientRelifePos(Vector3 pos)
	{
		this.myTransform.position = pos;
		base.NetworksyncPos = pos;
		if (base.HasAuthority)
		{
			GameHelperClient.localPlayer.trackRoleBase = null;
			GameHelperClient.ClickTrackRole = null;
			GameHelperClient.IsMoveToAttack = false;
			this.timer = base.GetRealAttackOffset();
			this.roleBuffManager.AddOneBuff<Buff无敌>("Buff无敌", 3.5f);
			this.AddMp(this.playerAttribute.maxExp);
			Action action = this.onPlayerRelife;
			if (action == null)
			{
				return;
			}
			action();
		}
	}

	// Token: 0x06000DDD RID: 3549 RVA: 0x00050212 File Offset: 0x0004E412
	protected static void InvokeUserCode_ClientRelifePos(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC ClientRelifePos called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_ClientRelifePos(reader.ReadVector3());
	}

	// Token: 0x06000DDE RID: 3550 RVA: 0x0005023B File Offset: 0x0004E43B
	protected void UserCode_CmdAddMusicBuff(Vector3 pos)
	{
		this.RpcAddMusicBuff(pos);
	}

	// Token: 0x06000DDF RID: 3551 RVA: 0x00050244 File Offset: 0x0004E444
	protected static void InvokeUserCode_CmdAddMusicBuff(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdAddMusicBuff called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdAddMusicBuff(reader.ReadVector3());
	}

	// Token: 0x06000DE0 RID: 3552 RVA: 0x00050270 File Offset: 0x0004E470
	protected void UserCode_RpcAddMusicBuff(Vector3 pos)
	{
		List<RoleBase> friendRoles = base.GetFriendRoles();
		for (int i = 0; i < friendRoles.Count; i++)
		{
			RoleBase roleBase = friendRoles[i];
			if (roleBase != null)
			{
				PlayerBase playerBase = roleBase as PlayerBase;
				if (playerBase != null && roleBase.HasAuthority && playerBase.GetDistanceV2(pos) < 5f)
				{
					Buff音乐鼓舞 buff = new Buff音乐鼓舞();
					roleBase.roleBuffManager.AddOneBuff("Buff音乐鼓舞", 15f, buff);
				}
			}
		}
	}

	// Token: 0x06000DE1 RID: 3553 RVA: 0x000502E5 File Offset: 0x0004E4E5
	protected static void InvokeUserCode_RpcAddMusicBuff(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcAddMusicBuff called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcAddMusicBuff(reader.ReadVector3());
	}

	// Token: 0x06000DE2 RID: 3554 RVA: 0x0005030E File Offset: 0x0004E50E
	protected void UserCode_CmdPlayEffect(string effectName, float lifeTime, Vector3 pos, float localScale)
	{
		this.RpcPlayEffect(effectName, lifeTime, pos, localScale);
	}

	// Token: 0x06000DE3 RID: 3555 RVA: 0x0005031B File Offset: 0x0004E51B
	protected static void InvokeUserCode_CmdPlayEffect(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdPlayEffect called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdPlayEffect(reader.ReadString(), reader.ReadFloat(), reader.ReadVector3(), reader.ReadFloat());
	}

	// Token: 0x06000DE4 RID: 3556 RVA: 0x00050358 File Offset: 0x0004E558
	protected void UserCode_RpcPlayEffect(string effectName, float lifeTime, Vector3 pos, float localScale)
	{
		Game.EffectManager.PlayEffect(effectName, lifeTime, pos, localScale);
	}

	// Token: 0x06000DE5 RID: 3557 RVA: 0x0005036A File Offset: 0x0004E56A
	protected static void InvokeUserCode_RpcPlayEffect(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcPlayEffect called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcPlayEffect(reader.ReadString(), reader.ReadFloat(), reader.ReadVector3(), reader.ReadFloat());
	}

	// Token: 0x06000DE6 RID: 3558 RVA: 0x000503A7 File Offset: 0x0004E5A7
	protected void UserCode_CmdPlayEffectObstruction(string effectName, float lifeTime, Vector3 pos, float localScale)
	{
		this.RpcPlayEffectObstruction(effectName, lifeTime, pos, localScale);
	}

	// Token: 0x06000DE7 RID: 3559 RVA: 0x000503B4 File Offset: 0x0004E5B4
	protected static void InvokeUserCode_CmdPlayEffectObstruction(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdPlayEffectObstruction called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdPlayEffectObstruction(reader.ReadString(), reader.ReadFloat(), reader.ReadVector3(), reader.ReadFloat());
	}

	// Token: 0x06000DE8 RID: 3560 RVA: 0x000503F1 File Offset: 0x0004E5F1
	protected void UserCode_RpcPlayEffectObstruction(string effectName, float lifeTime, Vector3 pos, float localScale)
	{
		Game.EffectManager.PlayEffectByPos(effectName, lifeTime, pos, localScale);
	}

	// Token: 0x06000DE9 RID: 3561 RVA: 0x00050403 File Offset: 0x0004E603
	protected static void InvokeUserCode_RpcPlayEffectObstruction(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcPlayEffectObstruction called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcPlayEffectObstruction(reader.ReadString(), reader.ReadFloat(), reader.ReadVector3(), reader.ReadFloat());
	}

	// Token: 0x06000DEA RID: 3562 RVA: 0x00050440 File Offset: 0x0004E640
	protected void UserCode_CmdPlayEffectEuler(string effectName, float lifeTime, Vector3 pos, Vector3 localScale, Vector3 eulerAngles)
	{
		this.RpcPlayEffectEuler(effectName, lifeTime, pos, localScale, eulerAngles);
	}

	// Token: 0x06000DEB RID: 3563 RVA: 0x00050450 File Offset: 0x0004E650
	protected static void InvokeUserCode_CmdPlayEffectEuler(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdPlayEffectEuler called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdPlayEffectEuler(reader.ReadString(), reader.ReadFloat(), reader.ReadVector3(), reader.ReadVector3(), reader.ReadVector3());
	}

	// Token: 0x06000DEC RID: 3564 RVA: 0x0005049D File Offset: 0x0004E69D
	protected void UserCode_RpcPlayEffectEuler(string effectName, float lifeTime, Vector3 pos, Vector3 localScale, Vector3 eulerAngles)
	{
		Game.EffectManager.PlayEffect(effectName, lifeTime, pos, localScale, eulerAngles);
	}

	// Token: 0x06000DED RID: 3565 RVA: 0x000504B4 File Offset: 0x0004E6B4
	protected static void InvokeUserCode_RpcPlayEffectEuler(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcPlayEffectEuler called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcPlayEffectEuler(reader.ReadString(), reader.ReadFloat(), reader.ReadVector3(), reader.ReadVector3(), reader.ReadVector3());
	}

	// Token: 0x06000DEE RID: 3566 RVA: 0x00050501 File Offset: 0x0004E701
	protected void UserCode_CmdPlayEffectAddRole(string effectName, float lifeTime, Vector3 pos, float localScale)
	{
		this.RpcPlayEffectAddRole(effectName, lifeTime, pos, localScale);
	}

	// Token: 0x06000DEF RID: 3567 RVA: 0x0005050E File Offset: 0x0004E70E
	protected static void InvokeUserCode_CmdPlayEffectAddRole(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdPlayEffectAddRole called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdPlayEffectAddRole(reader.ReadString(), reader.ReadFloat(), reader.ReadVector3(), reader.ReadFloat());
	}

	// Token: 0x06000DF0 RID: 3568 RVA: 0x0005054B File Offset: 0x0004E74B
	protected void UserCode_RpcPlayEffectAddRole(string effectName, float lifeTime, Vector3 pos, float localScale)
	{
		Game.EffectManager.PlayEffect(effectName, lifeTime, pos, localScale).SetParent(this.myTransform);
	}

	// Token: 0x06000DF1 RID: 3569 RVA: 0x00050567 File Offset: 0x0004E767
	protected static void InvokeUserCode_RpcPlayEffectAddRole(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcPlayEffectAddRole called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcPlayEffectAddRole(reader.ReadString(), reader.ReadFloat(), reader.ReadVector3(), reader.ReadFloat());
	}

	// Token: 0x06000DF2 RID: 3570 RVA: 0x000505A4 File Offset: 0x0004E7A4
	protected void UserCode_ClientRpcBornPos(Vector3 pos, uint authorityIdValue, HeroType heroTypeValue, string steamNameValue)
	{
		base.ClearAllBuff(false);
		this.heroType = heroTypeValue;
		this.NetworksteamName = steamNameValue;
		if (this.roleModeBase == null)
		{
			RoleModeBase component = AssetManager.LoadPrefab(Util.GetHeroModePath(heroTypeValue), null, true).GetComponent<RoleModeBase>();
			this.InitRoleModeBase(component);
		}
		Dictionary<string, RoleAttribute> heroAttributeDic = Game.GameData.HeroAttributeDic;
		int num = (int)this.heroType;
		RoleAttribute roleAttribute = heroAttributeDic[num.ToString()];
		this.authorityId = authorityIdValue;
		this.myTransform.position = pos;
		base.NetworksyncPos = pos;
		this.characterController.enabled = base.hasAuthority;
		this.timer = base.GetRealAttackOffset();
		this.trackRoleBase = null;
		this.InitRole(RoleType.Player, roleAttribute, 1, EnemyCreateType.Normal);
		this.mpAddSecRate = roleAttribute.mpRecover;
		Game.PlayerManagerClient.AddPlayer(this);
		Game.PlayerManagerClient.AddRealPlayer(this);
		if (base.HasAuthority)
		{
			GameHelperClient.localPlayer = this;
			this.CmdUpdatePickShare(GameHelperClient.IsPickShare);
			GameHelperClient.BeginCoronationGuard(this);
			GameHelperClient.localPlayer.InitPlayer();
			LocalHeroModelService.TryApplyLocalOverride(this);
			GameHelperClient.localPlayer.CmdUpdateSyncActiveSkillEnum(ActiveSkillEnum.None);
			this.mp = (this.maxMp = roleAttribute.mp);
			AnalyticsManager analytics = Game.Analytics;
			if (analytics != null)
			{
				analytics.RecordEnterDungeon();
			}
			CardManager cardManager = EntityStatic.Get<CardManager>();
			if (cardManager != null)
			{
				cardManager.ApplyDeck();
				int[] uploadTeamCards = cardManager.GetUploadTeamCards();
				if (uploadTeamCards != null && uploadTeamCards.Length != 0)
				{
					this.CmdUploadCard(uploadTeamCards);
				}
				Game.GameData.cardTotalManager.OnGameStartCheck();
			}
			object dic = ExcelManager.allExcelData["hero"];
			num = (int)heroTypeValue;
			object dic2 = dic.DIC(num.ToString());
			int passsiveSkillEnum = int.Parse(dic2.DIC("skill"));
			if (!dic2.DIC("zhuDong"))
			{
				base.AddHeroPasssiveSkillBook((PasssiveSkillEnum)passsiveSkillEnum);
			}
			else
			{
				int activeSkill = dic2.DIC("skill");
				base.AddHeroSkill((ActiveSkillEnum)activeSkill, null);
			}
			Game.UI.OpenUI<UI_PlayerState>(null);
			Game.UI.OpenUI<UI_Msg>(null);
			(NetworkManager.singleton as MyServerNetworkManager).OnSelectHeroOver();
			Debug.Log("当前难度" + GameHelperClient.MapLevel.ToString());
			this.AddGold(base.GetHeadUIPos(), this.initGold, true);
			this.AddGem(base.GetHeadUIPos(), this.initGem, false);
			this.CmdReduce(base.reduceInjury);
			UI_DecTip ui = Game.UI.GetUI<UI_DecTip>();
			if (ui == null)
			{
				return;
			}
			ui.RefreshBaoJi();
		}
	}

	// Token: 0x06000DF3 RID: 3571 RVA: 0x000507F9 File Offset: 0x0004E9F9
	protected static void InvokeUserCode_ClientRpcBornPos(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC ClientRpcBornPos called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_ClientRpcBornPos(reader.ReadVector3(), reader.ReadUInt(), Mirror.GeneratedNetworkCode._Read_HeroType(reader), reader.ReadString());
	}

	// Token: 0x06000DF4 RID: 3572 RVA: 0x00050834 File Offset: 0x0004EA34
	protected void UserCode_CmdUploadCard(int[] teamCards)
	{
		this.RpcUploadCard(teamCards);
	}

	// Token: 0x06000DF5 RID: 3573 RVA: 0x0005083D File Offset: 0x0004EA3D
	protected static void InvokeUserCode_CmdUploadCard(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdUploadCard called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdUploadCard(Mirror.GeneratedNetworkCode._Read_System.Int32[](reader));
	}

	// Token: 0x06000DF6 RID: 3574 RVA: 0x00050868 File Offset: 0x0004EA68
	protected void UserCode_RpcUploadCard(int[] teamCards)
	{
		if (base.hasAuthority)
		{
			return;
		}
		CardManager cardManager = EntityStatic.Get<CardManager>();
		if (cardManager != null)
		{
			cardManager.AddTeamCard(teamCards);
		}
	}

	// Token: 0x06000DF7 RID: 3575 RVA: 0x0005088E File Offset: 0x0004EA8E
	protected static void InvokeUserCode_RpcUploadCard(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcUploadCard called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcUploadCard(Mirror.GeneratedNetworkCode._Read_System.Int32[](reader));
	}

	// Token: 0x06000DF8 RID: 3576 RVA: 0x000508B8 File Offset: 0x0004EAB8
	protected void UserCode_CmdCreateEnemy(EnemyType enemyType, bool isRandomPos)
	{
		Vector3 spawnPos = Vector3.zero;
		spawnPos = this.myTransform.position + this.myTransform.forward * 3f;
		this.PlayerCreateEnemy(enemyType, isRandomPos, spawnPos, "");
	}

	// Token: 0x06000DF9 RID: 3577 RVA: 0x00050900 File Offset: 0x0004EB00
	protected static void InvokeUserCode_CmdCreateEnemy(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdCreateEnemy called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdCreateEnemy(Mirror.GeneratedNetworkCode._Read_EnemyType(reader), reader.ReadBool());
	}

	// Token: 0x06000DFA RID: 3578 RVA: 0x0005092F File Offset: 0x0004EB2F
	protected void UserCode_CmdCreateEnemyByPos(EnemyType enemyType, Vector3 pos)
	{
		this.PlayerCreateEnemy(enemyType, false, pos, "");
	}

	// Token: 0x06000DFB RID: 3579 RVA: 0x00050940 File Offset: 0x0004EB40
	protected static void InvokeUserCode_CmdCreateEnemyByPos(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdCreateEnemyByPos called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdCreateEnemyByPos(Mirror.GeneratedNetworkCode._Read_EnemyType(reader), reader.ReadVector3());
	}

	// Token: 0x06000DFC RID: 3580 RVA: 0x00050970 File Offset: 0x0004EB70
	protected void UserCode_CmdCreateLocalTyrant(int buyCount)
	{
		Vector3 spawnPos = this.myTransform.position + this.myTransform.forward * 3f;
		for (int i = 0; i < 10; i++)
		{
			this.PlayerCreateEnemy(EnemyType.Goblin_LocalTyrant_0 + buyCount, true, spawnPos, "");
		}
	}

	// Token: 0x06000DFD RID: 3581 RVA: 0x000509C5 File Offset: 0x0004EBC5
	protected static void InvokeUserCode_CmdCreateLocalTyrant(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdCreateLocalTyrant called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdCreateLocalTyrant(reader.ReadInt());
	}

	// Token: 0x06000DFE RID: 3582 RVA: 0x000509EE File Offset: 0x0004EBEE
	protected void UserCode_OnKillBlacksmith()
	{
		Util.ShowTips("刷新次数增加");
		GameHelperClient.AddRefreshNum(5);
		UI_Shop ui = Game.UI.GetUI<UI_Shop>();
		if (ui == null)
		{
			return;
		}
		ui.UpdateRefreshNum();
	}

	// Token: 0x06000DFF RID: 3583 RVA: 0x00050A14 File Offset: 0x0004EC14
	protected static void InvokeUserCode_OnKillBlacksmith(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("TargetRPC OnKillBlacksmith called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_OnKillBlacksmith();
	}

	// Token: 0x06000E00 RID: 3584 RVA: 0x00050A38 File Offset: 0x0004EC38
	protected void UserCode_CmdPickItem(uint itemId)
	{
		ItemStruct itemStruct;
		if (Game.ItemManager.itemStructs.TryGetValue(itemId, out itemStruct) && !ItemManager.CanPlayerPickItem(this, itemStruct))
		{
			return;
		}
		this.RpcPickItem(itemId);
	}

	// Token: 0x06000E01 RID: 3585 RVA: 0x00050A6A File Offset: 0x0004EC6A
	protected static void InvokeUserCode_CmdPickItem(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdPickItem called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdPickItem(reader.ReadUInt());
	}

	// Token: 0x06000E02 RID: 3586 RVA: 0x00050A94 File Offset: 0x0004EC94
	protected void UserCode_RpcPickItem(uint itemId)
	{
		ItemStruct itemStruct;
		if (Game.ItemManager.itemStructs.TryGetValue(itemId, out itemStruct))
		{
			if (base.isLocalPlayer)
			{
				Util.OnLocalPlayerPickItem(itemStruct.itemType, itemStruct.itemNum);
			}
			if (itemStruct.model != null)
			{
				AssetManager.UnLoadPrefab(itemStruct.model, false);
			}
			if (itemStruct.effect != null)
			{
				AssetManager.UnLoadPrefab(itemStruct.effect, false);
			}
			itemStruct.modelTransform = null;
			itemStruct.model = null;
			Game.ItemManager.itemStructs.Remove(itemId);
		}
	}

	// Token: 0x06000E03 RID: 3587 RVA: 0x00050B21 File Offset: 0x0004ED21
	protected static void InvokeUserCode_RpcPickItem(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcPickItem called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcPickItem(reader.ReadUInt());
	}

	// Token: 0x06000E04 RID: 3588 RVA: 0x00050B4C File Offset: 0x0004ED4C
	protected void UserCode_CmdTeleport(uint index)
	{
		if (this.hp <= 0L || this.roleState == RoleState.Dead)
		{
			return;
		}
		if (base.netId == index)
		{
			this.ServerTeleport(GameHelperClient.spawnConfig.playerSpawnPoint[(int)(index - 1U)]);
			return;
		}
		PlayerBase playerBase;
		if (Game.PlayerManagerClient.clientPlayerDic.TryGetValue(index, out playerBase))
		{
			this.ServerTeleport(playerBase.MyTransform.position);
		}
	}

	// Token: 0x06000E05 RID: 3589 RVA: 0x00050BB4 File Offset: 0x0004EDB4
	protected static void InvokeUserCode_CmdTeleport(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdTeleport called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdTeleport(reader.ReadUInt());
	}

	// Token: 0x06000E06 RID: 3590 RVA: 0x00050BDD File Offset: 0x0004EDDD
	protected void UserCode_CmdTeleportForPos(Vector3 pos)
	{
		this.ServerTeleport(pos);
	}

	// Token: 0x06000E07 RID: 3591 RVA: 0x00050BE6 File Offset: 0x0004EDE6
	protected static void InvokeUserCode_CmdTeleportForPos(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdTeleportForPos called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdTeleportForPos(reader.ReadVector3());
	}

	// Token: 0x06000E08 RID: 3592 RVA: 0x00050C10 File Offset: 0x0004EE10
	protected void UserCode_ClientTeleportPos(Vector3 pos)
	{
		if (base.hasAuthority)
		{
			this.characterController.enabled = false;
			this.myTransform.position = pos;
			this.characterController.enabled = true;
		}
		else
		{
			this.myTransform.position = pos;
		}
		base.NetworksyncPos = pos;
		if (Vector3.Distance(this.syncPos, GameHelperClient.localPlayer.MyTransform.position) < 20f)
		{
			EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/Game/teleport", 1f, 3f);
		}
		Game.EffectManager.PlayEffect(EffectDefine.RelifeEffect, 2f, pos, 2f);
	}

	// Token: 0x06000E09 RID: 3593 RVA: 0x00050CB4 File Offset: 0x0004EEB4
	protected static void InvokeUserCode_ClientTeleportPos(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC ClientTeleportPos called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_ClientTeleportPos(reader.ReadVector3());
	}

	// Token: 0x06000E0A RID: 3594 RVA: 0x00050CE0 File Offset: 0x0004EEE0
	protected void UserCode_CmdCreateItem(BagItem bagItem)
	{
		ItemStruct itemStruct = new ItemStruct();
		Vector2 pointByRadian = Util.GetPointByRadian(1f, 0f, Random.value * 360f);
		itemStruct.id = ItemManager.GetItemId();
		itemStruct.pos = new Vector3(this.myTransform.position.x + pointByRadian.x, 0f, this.myTransform.position.z + pointByRadian.y);
		itemStruct.itemType = bagItem.bookType;
		itemStruct.authorityId = base.netId;
		this.RpcCreateItem(itemStruct);
	}

	// Token: 0x06000E0B RID: 3595 RVA: 0x00050D76 File Offset: 0x0004EF76
	protected static void InvokeUserCode_CmdCreateItem(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdCreateItem called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdCreateItem(Mirror.GeneratedNetworkCode._Read_BagItem(reader));
	}

	// Token: 0x06000E0C RID: 3596 RVA: 0x00050D9F File Offset: 0x0004EF9F
	protected void UserCode_CmdCreateItemByPos(ItemType itemType, Vector3 pos)
	{
		this.CreateItemByPos(itemType, pos, 0, true);
	}

	// Token: 0x06000E0D RID: 3597 RVA: 0x00050DAB File Offset: 0x0004EFAB
	protected static void InvokeUserCode_CmdCreateItemByPos(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdCreateItemByPos called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdCreateItemByPos(Mirror.GeneratedNetworkCode._Read_ItemType(reader), reader.ReadVector3());
	}

	// Token: 0x06000E0E RID: 3598 RVA: 0x00050DDA File Offset: 0x0004EFDA
	protected void UserCode_CmdCreateItemByPosWithNum(ItemType itemType, Vector3 pos, int itemNum, bool isPickProtected)
	{
		this.CreateItemByPos(itemType, pos, itemNum, isPickProtected);
	}

	// Token: 0x06000E0F RID: 3599 RVA: 0x00050DE7 File Offset: 0x0004EFE7
	protected static void InvokeUserCode_CmdCreateItemByPosWithNum(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdCreateItemByPosWithNum called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdCreateItemByPosWithNum(Mirror.GeneratedNetworkCode._Read_ItemType(reader), reader.ReadVector3(), reader.ReadInt(), reader.ReadBool());
	}

	// Token: 0x06000E10 RID: 3600 RVA: 0x00050E22 File Offset: 0x0004F022
	protected void UserCode_RpcCreateItem(ItemStruct item)
	{
		Game.ItemManager.AddItem(item);
	}

	// Token: 0x06000E11 RID: 3601 RVA: 0x00050E2F File Offset: 0x0004F02F
	protected static void InvokeUserCode_RpcCreateItem(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcCreateItem called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcCreateItem(Mirror.GeneratedNetworkCode._Read_ItemStruct(reader));
	}

	// Token: 0x06000E12 RID: 3602 RVA: 0x00050E58 File Offset: 0x0004F058
	protected void UserCode_CmdCreateHeartDemon(int buyCount)
	{
		int[] array = new int[3];
		array[0] = (int)this.heroType;
		List<int> list = new List<int>();
		foreach (object obj in Enum.GetValues(typeof(HeroType)))
		{
			HeroType heroType = (HeroType)obj;
			if (heroType != HeroType.None && heroType <= HeroType.Hero_31)
			{
				if (GameHelperClient.isSaveHero)
				{
					Dictionary<string, RoleAttribute> heroAttributeDic = Game.GameData.HeroAttributeDic;
					int index = (int)heroType;
					if (heroAttributeDic[index.ToString()].isSave)
					{
						continue;
					}
				}
				list.Add((int)heroType);
			}
		}
		int count = list.Count;
		for (int i = 0; i < count; i++)
		{
			int num = Random.Range(0, count);
			List<int> list2 = list;
			int index = i;
			List<int> list3 = list;
			int index2 = num;
			int value = list[num];
			int value2 = list[i];
			list2[index] = value;
			list3[index2] = value2;
		}
		int num2 = 1;
		for (int j = 0; j < 3; j++)
		{
			if (list[j] != array[0])
			{
				array[num2] = list[j];
				num2++;
				if (num2 == 3)
				{
					break;
				}
			}
		}
		Vector3 spawnPos = this.myTransform.position + this.myTransform.forward * 3f;
		for (int k = 0; k < 3; k++)
		{
			this.PlayerCreateEnemy(EnemyType.Goblin_HeartMonster_0 + buyCount, true, spawnPos, list[k].ToString());
		}
	}

	// Token: 0x06000E13 RID: 3603 RVA: 0x00050FF0 File Offset: 0x0004F1F0
	protected static void InvokeUserCode_CmdCreateHeartDemon(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdCreateHeartDemon called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdCreateHeartDemon(reader.ReadInt());
	}

	// Token: 0x06000E14 RID: 3604 RVA: 0x00051019 File Offset: 0x0004F219
	protected void UserCode_CmdClearSkill(uint skillId)
	{
		this.RpcClearSkill(skillId);
	}

	// Token: 0x06000E15 RID: 3605 RVA: 0x00051022 File Offset: 0x0004F222
	protected static void InvokeUserCode_CmdClearSkill(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdClearSkill called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdClearSkill(reader.ReadUInt());
	}

	// Token: 0x06000E16 RID: 3606 RVA: 0x0005104B File Offset: 0x0004F24B
	protected void UserCode_RpcClearSkill(uint skillId)
	{
		Game.SkillManager.ClearSkill(skillId);
	}

	// Token: 0x06000E17 RID: 3607 RVA: 0x00051058 File Offset: 0x0004F258
	protected static void InvokeUserCode_RpcClearSkill(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcClearSkill called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcClearSkill(reader.ReadUInt());
	}

	// Token: 0x06000E18 RID: 3608 RVA: 0x00051081 File Offset: 0x0004F281
	protected void UserCode_CmdClearSkillByData(uint skillId, int clearData)
	{
		this.RpcClearSkillByData(skillId, clearData);
	}

	// Token: 0x06000E19 RID: 3609 RVA: 0x0005108B File Offset: 0x0004F28B
	protected static void InvokeUserCode_CmdClearSkillByData(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdClearSkillByData called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdClearSkillByData(reader.ReadUInt(), reader.ReadInt());
	}

	// Token: 0x06000E1A RID: 3610 RVA: 0x000510BA File Offset: 0x0004F2BA
	protected void UserCode_RpcClearSkillByData(uint skillId, int clearData)
	{
		Game.SkillManager.ClearSkilByData(skillId, clearData);
	}

	// Token: 0x06000E1B RID: 3611 RVA: 0x000510C8 File Offset: 0x0004F2C8
	protected static void InvokeUserCode_RpcClearSkillByData(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcClearSkillByData called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcClearSkillByData(reader.ReadUInt(), reader.ReadInt());
	}

	// Token: 0x06000E1C RID: 3612 RVA: 0x000510F7 File Offset: 0x0004F2F7
	protected void UserCode_CmdStartSkillAciton(uint skillId)
	{
		this.RpcStartSkillAciton(skillId);
	}

	// Token: 0x06000E1D RID: 3613 RVA: 0x00051100 File Offset: 0x0004F300
	protected static void InvokeUserCode_CmdStartSkillAciton(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdStartSkillAciton called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdStartSkillAciton(reader.ReadUInt());
	}

	// Token: 0x06000E1E RID: 3614 RVA: 0x00051129 File Offset: 0x0004F329
	protected void UserCode_RpcStartSkillAciton(uint skillId)
	{
		Game.SkillManager.StartSkillAciton(skillId);
	}

	// Token: 0x06000E1F RID: 3615 RVA: 0x00051136 File Offset: 0x0004F336
	protected static void InvokeUserCode_RpcStartSkillAciton(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcStartSkillAciton called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcStartSkillAciton(reader.ReadUInt());
	}

	// Token: 0x06000E20 RID: 3616 RVA: 0x0005115F File Offset: 0x0004F35F
	protected void UserCode_CmdEndSkillAciton(uint skillId)
	{
		this.RpcEndSkillAciton(skillId);
	}

	// Token: 0x06000E21 RID: 3617 RVA: 0x00051168 File Offset: 0x0004F368
	protected static void InvokeUserCode_CmdEndSkillAciton(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdEndSkillAciton called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdEndSkillAciton(reader.ReadUInt());
	}

	// Token: 0x06000E22 RID: 3618 RVA: 0x00051191 File Offset: 0x0004F391
	protected void UserCode_RpcEndSkillAciton(uint skillId)
	{
		Game.SkillManager.EndSkillAciton(skillId);
	}

	// Token: 0x06000E23 RID: 3619 RVA: 0x0005119E File Offset: 0x0004F39E
	protected static void InvokeUserCode_RpcEndSkillAciton(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcEndSkillAciton called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcEndSkillAciton(reader.ReadUInt());
	}

	// Token: 0x06000E24 RID: 3620 RVA: 0x000511C8 File Offset: 0x0004F3C8
	protected void UserCode_CmdAddBuff(uint buffNetId, uint attackNetId, LocalBuffType localBuffType, float buffValue, float buffTime, int level)
	{
		NetworkIdentity networkIdentity;
		if (NetworkServer.spawned.TryGetValue(buffNetId, out networkIdentity))
		{
			networkIdentity.GetComponent<RoleBase>().RpcAddBuff(attackNetId, localBuffType, buffValue, buffTime, level);
		}
	}

	// Token: 0x06000E25 RID: 3621 RVA: 0x000511F8 File Offset: 0x0004F3F8
	protected static void InvokeUserCode_CmdAddBuff(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdAddBuff called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdAddBuff(reader.ReadUInt(), reader.ReadUInt(), Mirror.GeneratedNetworkCode._Read_LocalBuffType(reader), reader.ReadFloat(), reader.ReadFloat(), reader.ReadInt());
	}

	// Token: 0x06000E26 RID: 3622 RVA: 0x0005124C File Offset: 0x0004F44C
	protected void UserCode_CmdRemoveuff(uint buffNetId, LocalBuffType localBuffType)
	{
		NetworkIdentity networkIdentity;
		if (NetworkServer.spawned.TryGetValue(buffNetId, out networkIdentity))
		{
			networkIdentity.GetComponent<RoleBase>().RpcRemoveuff(localBuffType);
		}
	}

	// Token: 0x06000E27 RID: 3623 RVA: 0x00051274 File Offset: 0x0004F474
	protected static void InvokeUserCode_CmdRemoveuff(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdRemoveuff called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdRemoveuff(reader.ReadUInt(), Mirror.GeneratedNetworkCode._Read_LocalBuffType(reader));
	}

	// Token: 0x06000E28 RID: 3624 RVA: 0x000512A4 File Offset: 0x0004F4A4
	protected void UserCode_TargetKillGoblinMine()
	{
		this.AddGold(base.GetHeadUIPos(), 5000, true);
		this.AddGem(base.GetHeadUIPos(), 10, false);
		int num = 25;
		base.AddSTA(num);
		base.AddAGI(num);
		base.AddSTR(num);
	}

	// Token: 0x06000E29 RID: 3625 RVA: 0x000512EB File Offset: 0x0004F4EB
	protected static void InvokeUserCode_TargetKillGoblinMine(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("TargetRPC TargetKillGoblinMine called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_TargetKillGoblinMine();
	}

	// Token: 0x06000E2A RID: 3626 RVA: 0x00051310 File Offset: 0x0004F510
	protected void UserCode_CmdXuanYun(uint netId, float timer)
	{
		NetworkIdentity networkIdentity;
		if (NetworkServer.spawned.TryGetValue(netId, out networkIdentity))
		{
			RoleBase component = networkIdentity.GetComponent<RoleBase>();
			if (!component.IsDead() && !component.XuanYunImmunity && component.CanXuanYunLastTime <= 0f)
			{
				component.TargetRpcUpdateXuanYun(timer);
			}
		}
	}

	// Token: 0x06000E2B RID: 3627 RVA: 0x00051357 File Offset: 0x0004F557
	protected static void InvokeUserCode_CmdXuanYun(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdXuanYun called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdXuanYun(reader.ReadUInt(), reader.ReadFloat());
	}

	// Token: 0x06000E2C RID: 3628 RVA: 0x00051387 File Offset: 0x0004F587
	protected void UserCode_CmdAddAttackTarget(List<uint> roleList)
	{
		this.RpcAddAttackTarget(roleList);
	}

	// Token: 0x06000E2D RID: 3629 RVA: 0x00051390 File Offset: 0x0004F590
	protected static void InvokeUserCode_CmdAddAttackTarget(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdAddAttackTarget called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdAddAttackTarget(Mirror.GeneratedNetworkCode._Read_System.Collections.Generic.List`1<System.UInt32>(reader));
	}

	// Token: 0x06000E2E RID: 3630 RVA: 0x000513B9 File Offset: 0x0004F5B9
	protected void UserCode_RpcAddAttackTarget(List<uint> roleList)
	{
		if (this.playerModeBase is PlayerDeathNoteMode)
		{
			(this.playerModeBase as PlayerDeathNoteMode).RpcAddAttackTarget(roleList);
		}
	}

	// Token: 0x06000E2F RID: 3631 RVA: 0x000513D9 File Offset: 0x0004F5D9
	protected static void InvokeUserCode_RpcAddAttackTarget(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcAddAttackTarget called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcAddAttackTarget(Mirror.GeneratedNetworkCode._Read_System.Collections.Generic.List`1<System.UInt32>(reader));
	}

	// Token: 0x06000E30 RID: 3632 RVA: 0x00051402 File Offset: 0x0004F602
	protected void UserCode_CmdUpdateSyncActiveSkillEnum(ActiveSkillEnum skillEnum)
	{
		this.NetworksyncActiveSkillEnum = skillEnum;
	}

	// Token: 0x06000E31 RID: 3633 RVA: 0x0005140B File Offset: 0x0004F60B
	protected static void InvokeUserCode_CmdUpdateSyncActiveSkillEnum(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdUpdateSyncActiveSkillEnum called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdUpdateSyncActiveSkillEnum(Mirror.GeneratedNetworkCode._Read_ActiveSkillEnum(reader));
	}

	// Token: 0x06000E32 RID: 3634 RVA: 0x00051434 File Offset: 0x0004F634
	protected void UserCode_CmdReduce(int value)
	{
		this.RpcReduce(value);
	}

	// Token: 0x06000E33 RID: 3635 RVA: 0x0005143D File Offset: 0x0004F63D
	protected static void InvokeUserCode_CmdReduce(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdReduce called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdReduce(reader.ReadInt());
	}

	// Token: 0x06000E34 RID: 3636 RVA: 0x00051466 File Offset: 0x0004F666
	protected void UserCode_RpcReduce(int value)
	{
		base.reduceInjury = value;
	}

	// Token: 0x06000E35 RID: 3637 RVA: 0x0005146F File Offset: 0x0004F66F
	protected static void InvokeUserCode_RpcReduce(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcReduce called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcReduce(reader.ReadInt());
	}

	// Token: 0x06000E36 RID: 3638 RVA: 0x00051498 File Offset: 0x0004F698
	protected void UserCode_CmdEquipArmor(int value)
	{
		this.RpcEquipArmor(value);
	}

	// Token: 0x06000E37 RID: 3639 RVA: 0x000514A1 File Offset: 0x0004F6A1
	protected static void InvokeUserCode_CmdEquipArmor(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdEquipArmor called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdEquipArmor(reader.ReadInt());
	}

	// Token: 0x06000E38 RID: 3640 RVA: 0x000514CA File Offset: 0x0004F6CA
	protected void UserCode_RpcEquipArmor(int value)
	{
		this.equipArmor = value;
	}

	// Token: 0x06000E39 RID: 3641 RVA: 0x000514D3 File Offset: 0x0004F6D3
	protected static void InvokeUserCode_RpcEquipArmor(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcEquipArmor called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcEquipArmor(reader.ReadInt());
	}

	// Token: 0x06000E3A RID: 3642 RVA: 0x000514FC File Offset: 0x0004F6FC
	protected void UserCode_CmdEquipDoge(int value)
	{
		this.RpcEquipDoge(value);
	}

	// Token: 0x06000E3B RID: 3643 RVA: 0x00051505 File Offset: 0x0004F705
	protected static void InvokeUserCode_CmdEquipDoge(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdEquipDoge called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdEquipDoge(reader.ReadInt());
	}

	// Token: 0x06000E3C RID: 3644 RVA: 0x0005152E File Offset: 0x0004F72E
	protected void UserCode_RpcEquipDoge(int value)
	{
		this.equipDoge = value;
	}

	// Token: 0x06000E3D RID: 3645 RVA: 0x00051537 File Offset: 0x0004F737
	protected static void InvokeUserCode_RpcEquipDoge(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcEquipDoge called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcEquipDoge(reader.ReadInt());
	}

	// Token: 0x06000E3E RID: 3646 RVA: 0x00051560 File Offset: 0x0004F760
	protected void UserCode_CmdUpdateBreakShield(float newBreakShield)
	{
		this.normalBreakShield = newBreakShield;
	}

	// Token: 0x06000E3F RID: 3647 RVA: 0x00051569 File Offset: 0x0004F769
	protected static void InvokeUserCode_CmdUpdateBreakShield(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdUpdateBreakShield called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdUpdateBreakShield(reader.ReadFloat());
	}

	// Token: 0x06000E40 RID: 3648 RVA: 0x00051593 File Offset: 0x0004F793
	protected void UserCode_CmdUpdateSkillBreakShield(float newBreakShield)
	{
		this.skillBreakShield = newBreakShield;
	}

	// Token: 0x06000E41 RID: 3649 RVA: 0x0005159C File Offset: 0x0004F79C
	protected static void InvokeUserCode_CmdUpdateSkillBreakShield(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdUpdateSkillBreakShield called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdUpdateSkillBreakShield(reader.ReadFloat());
	}

	// Token: 0x06000E42 RID: 3650 RVA: 0x000515C6 File Offset: 0x0004F7C6
	protected void UserCode_CmdAddAllPlayerItem(ItemType itemType)
	{
		this.RpcAddAllPlayerItem(itemType);
	}

	// Token: 0x06000E43 RID: 3651 RVA: 0x000515CF File Offset: 0x0004F7CF
	protected static void InvokeUserCode_CmdAddAllPlayerItem(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdAddAllPlayerItem called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdAddAllPlayerItem(Mirror.GeneratedNetworkCode._Read_ItemType(reader));
	}

	// Token: 0x06000E44 RID: 3652 RVA: 0x000515F8 File Offset: 0x0004F7F8
	protected void UserCode_RpcAddAllPlayerItem(ItemType itemType)
	{
		GameHelperClient.localPlayer.AddRelic((int)itemType, 0);
	}

	// Token: 0x06000E45 RID: 3653 RVA: 0x00051607 File Offset: 0x0004F807
	protected static void InvokeUserCode_RpcAddAllPlayerItem(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcAddAllPlayerItem called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcAddAllPlayerItem(Mirror.GeneratedNetworkCode._Read_ItemType(reader));
	}

	// Token: 0x06000E46 RID: 3654 RVA: 0x00051630 File Offset: 0x0004F830
	protected void UserCode_CmdEliteProbabilityAdd(float probability)
	{
		GameHelperClient.EliteProbabilityAdd += probability;
	}

	// Token: 0x06000E47 RID: 3655 RVA: 0x0005163E File Offset: 0x0004F83E
	protected static void InvokeUserCode_CmdEliteProbabilityAdd(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdEliteProbabilityAdd called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdEliteProbabilityAdd(reader.ReadFloat());
	}

	// Token: 0x06000E48 RID: 3656 RVA: 0x00051668 File Offset: 0x0004F868
	protected void UserCode_CmdUpdateAddHatred(float updateValue)
	{
		this.RpcUpdateAddHatred(updateValue);
	}

	// Token: 0x06000E49 RID: 3657 RVA: 0x00051671 File Offset: 0x0004F871
	protected static void InvokeUserCode_CmdUpdateAddHatred(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdUpdateAddHatred called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdUpdateAddHatred(reader.ReadFloat());
	}

	// Token: 0x06000E4A RID: 3658 RVA: 0x0005169B File Offset: 0x0004F89B
	protected void UserCode_RpcUpdateAddHatred(float updateValue)
	{
		this.addHatred += updateValue;
	}

	// Token: 0x06000E4B RID: 3659 RVA: 0x000516AB File Offset: 0x0004F8AB
	protected static void InvokeUserCode_RpcUpdateAddHatred(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcUpdateAddHatred called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcUpdateAddHatred(reader.ReadFloat());
	}

	// Token: 0x06000E4C RID: 3660 RVA: 0x000516D5 File Offset: 0x0004F8D5
	protected void UserCode_CmdChat(string textStr)
	{
		this.RpcChat(textStr);
	}

	// Token: 0x06000E4D RID: 3661 RVA: 0x000516DE File Offset: 0x0004F8DE
	protected static void InvokeUserCode_CmdChat(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdChat called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdChat(reader.ReadString());
	}

	// Token: 0x06000E4E RID: 3662 RVA: 0x00051708 File Offset: 0x0004F908
	protected void UserCode_RpcChat(string textStr)
	{
		textStr = PathDefine.Concat(this.roleName, StringDefine.Colon, textStr);
		int num = Mathf.Min(ColorDefine.ChatColor.Length - 1, Mathf.Max(0, Game.PlayerManagerClient.clientPlayerList.IndexOf(this)));
		UI_Msg ui = Game.UI.GetUI<UI_Msg>();
		if (ui == null)
		{
			return;
		}
		ui.ShowMsg(string.Format(ColorDefine.ChatColor[num], textStr), true);
	}

	// Token: 0x06000E4F RID: 3663 RVA: 0x0005176F File Offset: 0x0004F96F
	protected static void InvokeUserCode_RpcChat(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcChat called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcChat(reader.ReadString());
	}

	// Token: 0x06000E50 RID: 3664 RVA: 0x00051798 File Offset: 0x0004F998
	protected void UserCode_CmdAttackOtherPlayer(double damage, AttackType attackType, uint attackRoleId, float attackEulerY, uint hitPlayerId)
	{
		NetworkIdentity networkIdentity;
		if (NetworkServer.spawned.TryGetValue(hitPlayerId, out networkIdentity))
		{
			PlayerBase component = networkIdentity.GetComponent<PlayerBase>();
			if (component != null)
			{
				component.TargetAttackOtherPlayer(damage, attackType, attackRoleId, attackEulerY);
			}
		}
	}

	// Token: 0x06000E51 RID: 3665 RVA: 0x000517D0 File Offset: 0x0004F9D0
	protected static void InvokeUserCode_CmdAttackOtherPlayer(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdAttackOtherPlayer called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdAttackOtherPlayer(reader.ReadDouble(), Mirror.GeneratedNetworkCode._Read_AttackType(reader), reader.ReadUInt(), reader.ReadFloat(), reader.ReadUInt());
	}

	// Token: 0x06000E52 RID: 3666 RVA: 0x00051820 File Offset: 0x0004FA20
	protected void UserCode_TargetAttackOtherPlayer(double damage, AttackType attackType, uint attackRoleId, float attackEulerY)
	{
		NetworkIdentity networkIdentity;
		if (NetworkClient.spawned.TryGetValue(attackRoleId, out networkIdentity))
		{
			RoleBase component = networkIdentity.GetComponent<RoleBase>();
			if (attackType == AttackType.Normal && this.FinalDoge > 0)
			{
				float num = Random.Range(0f, 1f);
				float num2 = 1f - Util.GetArmorLevel(this.FinalDoge);
				if (num <= num2)
				{
					if ((base.isLocalPlayer && damage > (double)((float)this.maxHp * 0.15f)) || this.roleType == RoleType.Enemy)
					{
						Game.UI.GetUI<UI_PlayerState>().ShowDoge(base.GetAttackPos());
					}
					RoleBase.DogeEvent dogeEvent = this.dogeEvent;
					if (dogeEvent == null)
					{
						return;
					}
					dogeEvent();
					return;
				}
			}
			if (attackType == AttackType.Skill)
			{
				damage *= (double)Util.GetArmorLevel(this.FinalSkillReduction);
			}
			bool flag = (base.IsFromRoleType(RoleType.King) && component.IsFromRoleType(RoleType.Player)) || (base.IsFromRoleType(RoleType.Player) && component.IsFromRoleType(RoleType.King));
			if (attackType != AttackType.TrueDamage)
			{
				float armorLevel = Util.GetArmorLevel(this.armor);
				damage *= (double)armorLevel;
				damage -= (double)(flag ? ((float)base.reduceInjury * GameHelperClient.GetKingBattleReduceLevel()) : ((float)base.reduceInjury));
				if (damage < 0.0)
				{
					damage = 0.0;
				}
				if (this.damageEvent != null)
				{
					float num3 = RoleBase.ToFloatBattleValue(damage);
					damage = (double)this.damageEvent(component, this, attackType, ref num3);
				}
				if (damage < 0.0)
				{
					damage = 0.0;
				}
			}
			base.OnHitUpdateHp(damage, attackType, component, attackEulerY, flag);
		}
	}

	// Token: 0x06000E53 RID: 3667 RVA: 0x00051992 File Offset: 0x0004FB92
	protected static void InvokeUserCode_TargetAttackOtherPlayer(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("TargetRPC TargetAttackOtherPlayer called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_TargetAttackOtherPlayer(reader.ReadDouble(), Mirror.GeneratedNetworkCode._Read_AttackType(reader), reader.ReadUInt(), reader.ReadFloat());
	}

	// Token: 0x06000E54 RID: 3668 RVA: 0x000519CF File Offset: 0x0004FBCF
	protected void UserCode_CmdUpdateAddRelifeTime(int updateValue)
	{
		this.addRelifeTime += updateValue;
		this.RpcUpdateAddRelifeTime(this.addRelifeTime);
	}

	// Token: 0x06000E55 RID: 3669 RVA: 0x000519EB File Offset: 0x0004FBEB
	protected static void InvokeUserCode_CmdUpdateAddRelifeTime(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdUpdateAddRelifeTime called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdUpdateAddRelifeTime(reader.ReadInt());
	}

	// Token: 0x06000E56 RID: 3670 RVA: 0x00051A14 File Offset: 0x0004FC14
	protected void UserCode_RpcUpdateAddRelifeTime(int updateValue)
	{
		this.addRelifeTime = updateValue;
	}

	// Token: 0x06000E57 RID: 3671 RVA: 0x00051A1D File Offset: 0x0004FC1D
	protected static void InvokeUserCode_RpcUpdateAddRelifeTime(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcUpdateAddRelifeTime called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcUpdateAddRelifeTime(reader.ReadInt());
	}

	// Token: 0x06000E58 RID: 3672 RVA: 0x00051A46 File Offset: 0x0004FC46
	protected void UserCode_CmdUploadGameOverData(long damageValue, int goldNum, int gemNum, int killBossNumValue)
	{
		this.RpcUploadGameOverData(damageValue, goldNum, gemNum, killBossNumValue);
	}

	// Token: 0x06000E59 RID: 3673 RVA: 0x00051A53 File Offset: 0x0004FC53
	protected static void InvokeUserCode_CmdUploadGameOverData(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdUploadGameOverData called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdUploadGameOverData(reader.ReadLong(), reader.ReadInt(), reader.ReadInt(), reader.ReadInt());
	}

	// Token: 0x06000E5A RID: 3674 RVA: 0x00051A8E File Offset: 0x0004FC8E
	protected void UserCode_RpcUploadGameOverData(long damageValue, int goldNum, int gemNum, int killBossNumValue)
	{
		this.damageStatic = damageValue;
		this.getGoldNum = goldNum;
		this.getGemNum = gemNum;
		this.killBossNum = killBossNumValue;
	}

	// Token: 0x06000E5B RID: 3675 RVA: 0x00051AAD File Offset: 0x0004FCAD
	protected static void InvokeUserCode_RpcUploadGameOverData(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcUploadGameOverData called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcUploadGameOverData(reader.ReadLong(), reader.ReadInt(), reader.ReadInt(), reader.ReadInt());
	}

	// Token: 0x06000E5C RID: 3676 RVA: 0x00051AE8 File Offset: 0x0004FCE8
	protected void UserCode_CmdUpdateSaiYaDarkBuff(SaiYaDarkBuff.ReData data, uint buffNetId)
	{
		this.RpcUpdateSaiYaDarkBuff(data, buffNetId);
	}

	// Token: 0x06000E5D RID: 3677 RVA: 0x00051AF2 File Offset: 0x0004FCF2
	protected static void InvokeUserCode_CmdUpdateSaiYaDarkBuff(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdUpdateSaiYaDarkBuff called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdUpdateSaiYaDarkBuff(Mirror.GeneratedNetworkCode._Read_SaiYaDarkBuff/ReData(reader), reader.ReadUInt());
	}

	// Token: 0x06000E5E RID: 3678 RVA: 0x00051B24 File Offset: 0x0004FD24
	protected void UserCode_RpcUpdateSaiYaDarkBuff(SaiYaDarkBuff.ReData data, uint buffNetId)
	{
		NetworkIdentity networkIdentity;
		if (NetworkClient.spawned.TryGetValue(buffNetId, out networkIdentity))
		{
			RoleBase component = networkIdentity.GetComponent<RoleBase>();
			if (component != null)
			{
				EnemySaiYaDark enemySaiYaDark = component.RoleModeBase as EnemySaiYaDark;
				if (enemySaiYaDark != null)
				{
					enemySaiYaDark.OnUpdateSaiYaDarkBuff(data);
				}
			}
		}
	}

	// Token: 0x06000E5F RID: 3679 RVA: 0x00051B66 File Offset: 0x0004FD66
	protected static void InvokeUserCode_RpcUpdateSaiYaDarkBuff(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcUpdateSaiYaDarkBuff called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcUpdateSaiYaDarkBuff(Mirror.GeneratedNetworkCode._Read_SaiYaDarkBuff/ReData(reader), reader.ReadUInt());
	}

	// Token: 0x06000E60 RID: 3680 RVA: 0x00051B98 File Offset: 0x0004FD98
	protected void UserCode_CmdUpdateSaiYaSkill3(uint enemyNetId, int addNum)
	{
		NetworkIdentity networkIdentity;
		if (NetworkServer.spawned.TryGetValue(enemyNetId, out networkIdentity))
		{
			RoleBase component = networkIdentity.GetComponent<RoleBase>();
			if (component != null)
			{
				component.SyncSkillData += (float)addNum;
			}
		}
	}

	// Token: 0x06000E61 RID: 3681 RVA: 0x00051BD3 File Offset: 0x0004FDD3
	protected static void InvokeUserCode_CmdUpdateSaiYaSkill3(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdUpdateSaiYaSkill3 called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdUpdateSaiYaSkill3(reader.ReadUInt(), reader.ReadInt());
	}

	// Token: 0x06000E62 RID: 3682 RVA: 0x00051C02 File Offset: 0x0004FE02
	protected void UserCode_CmdUpdateRelicAdd(float value)
	{
		this.relicAdd = value;
		this.RpcUpdateRelicAdd(this.relicAdd);
	}

	// Token: 0x06000E63 RID: 3683 RVA: 0x00051C17 File Offset: 0x0004FE17
	protected static void InvokeUserCode_CmdUpdateRelicAdd(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdUpdateRelicAdd called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdUpdateRelicAdd(reader.ReadFloat());
	}

	// Token: 0x06000E64 RID: 3684 RVA: 0x00051C41 File Offset: 0x0004FE41
	protected void UserCode_RpcUpdateRelicAdd(float value)
	{
		this.relicAdd = value;
	}

	// Token: 0x06000E65 RID: 3685 RVA: 0x00051C4A File Offset: 0x0004FE4A
	protected static void InvokeUserCode_RpcUpdateRelicAdd(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcUpdateRelicAdd called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcUpdateRelicAdd(reader.ReadFloat());
	}

	// Token: 0x06000E66 RID: 3686 RVA: 0x00051C74 File Offset: 0x0004FE74
	protected void UserCode_CmdUpdateBookAdd(float value)
	{
		this.bookAdd = value;
		this.RpcUpdateBookAdd(value);
	}

	// Token: 0x06000E67 RID: 3687 RVA: 0x00051C84 File Offset: 0x0004FE84
	protected static void InvokeUserCode_CmdUpdateBookAdd(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdUpdateBookAdd called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdUpdateBookAdd(reader.ReadFloat());
	}

	// Token: 0x06000E68 RID: 3688 RVA: 0x00051CAE File Offset: 0x0004FEAE
	protected void UserCode_RpcUpdateBookAdd(float value)
	{
		this.bookAdd = value;
	}

	// Token: 0x06000E69 RID: 3689 RVA: 0x00051CB7 File Offset: 0x0004FEB7
	protected static void InvokeUserCode_RpcUpdateBookAdd(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcUpdateBookAdd called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcUpdateBookAdd(reader.ReadFloat());
	}

	// Token: 0x06000E6A RID: 3690 RVA: 0x00051CE1 File Offset: 0x0004FEE1
	protected void UserCode_CmdUpdateForgingAdd(float value)
	{
		this.forgingAdd = value;
		this.RpcUpdateForgingAdd(value);
	}

	// Token: 0x06000E6B RID: 3691 RVA: 0x00051CF1 File Offset: 0x0004FEF1
	protected static void InvokeUserCode_CmdUpdateForgingAdd(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdUpdateForgingAdd called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdUpdateForgingAdd(reader.ReadFloat());
	}

	// Token: 0x06000E6C RID: 3692 RVA: 0x00051D1B File Offset: 0x0004FF1B
	protected void UserCode_RpcUpdateForgingAdd(float value)
	{
		this.forgingAdd = value;
	}

	// Token: 0x06000E6D RID: 3693 RVA: 0x00051D24 File Offset: 0x0004FF24
	protected static void InvokeUserCode_RpcUpdateForgingAdd(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcUpdateForgingAdd called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcUpdateForgingAdd(reader.ReadFloat());
	}

	// Token: 0x06000E6E RID: 3694 RVA: 0x00051D50 File Offset: 0x0004FF50
	protected void UserCode_CmdHelpNpc(uint npcId)
	{
		NetworkIdentity networkIdentity;
		if (NetworkServer.spawned.TryGetValue(npcId, out networkIdentity))
		{
			RoleBase component = networkIdentity.GetComponent<RoleBase>();
			if (component != null)
			{
				component.ServerRelife();
			}
		}
	}

	// Token: 0x06000E6F RID: 3695 RVA: 0x00051D82 File Offset: 0x0004FF82
	protected static void InvokeUserCode_CmdHelpNpc(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdHelpNpc called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdHelpNpc(reader.ReadUInt());
	}

	// Token: 0x06000E70 RID: 3696 RVA: 0x00051DAC File Offset: 0x0004FFAC
	protected void UserCode_ClientRpcKingBornPos(Vector3 pos, uint authorityIdValue, uint trackPlayerId, HeroType heroTypeValue, SaveLoadManager.PlayerKingData playerKingData)
	{
		base.ClearAllBuff(false);
		this.heroType = heroTypeValue;
		if (this.roleModeBase == null)
		{
			RoleModeBase component = AssetManager.LoadPrefab(Util.GetHeroModePath(heroTypeValue), null, true).GetComponent<RoleModeBase>();
			this.InitRoleModeBase(component);
		}
		Dictionary<string, RoleAttribute> heroAttributeDic = Game.GameData.HeroAttributeDic;
		int num = (int)this.heroType;
		RoleAttribute roleAttribute = heroAttributeDic[num.ToString()];
		this.authorityId = authorityIdValue;
		this.myTransform.position = pos;
		base.NetworksyncPos = pos;
		this.characterController.enabled = base.hasAuthority;
		this.timer = base.GetRealAttackOffset();
		this.InitRole(RoleType.King, roleAttribute, 1, EnemyCreateType.Normal);
		this.mpAddSecRate = roleAttribute.mpRecover;
		Game.EnemyManagerClient.AddEnemyNoAgent(this);
		base.gameObject.layer = LayerUtil.EnemyLayer;
		PlayerBase trackRoleBase;
		if (Game.PlayerManagerClient.clientPlayerDic.TryGetValue(trackPlayerId, out trackRoleBase))
		{
			this.trackRoleBase = trackRoleBase;
		}
		else
		{
			this.trackRoleBase = GameHelperClient.localPlayer;
		}
		this.roleName = playerKingData.kingName;
		this.InitPlayerKingAI(playerKingData);
		if (base.HasAuthority)
		{
			this.playerAttribute = new PlayerAttribute();
			this.playerAttribute.playerBase = this;
			this.playerKingAI = new PlayerKingAI();
			this.CmdUpdateSyncActiveSkillEnum(ActiveSkillEnum.None);
			this.playerKingAI.InitKingAI(playerKingData, this);
			this.mp = (this.maxMp = roleAttribute.mp);
			base.SetRotationY(-145f);
		}
	}

	// Token: 0x06000E71 RID: 3697 RVA: 0x00051F14 File Offset: 0x00050114
	protected static void InvokeUserCode_ClientRpcKingBornPos(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC ClientRpcKingBornPos called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_ClientRpcKingBornPos(reader.ReadVector3(), reader.ReadUInt(), reader.ReadUInt(), Mirror.GeneratedNetworkCode._Read_HeroType(reader), Mirror.GeneratedNetworkCode._Read_SaveLoadManager/PlayerKingData(reader));
	}

	// Token: 0x06000E72 RID: 3698 RVA: 0x00051F60 File Offset: 0x00050160
	protected void UserCode_CmdUploadLocalKingData(SaveLoadManager.PlayerKingData playerKingData)
	{
		MyServerNetworkManager myServerNetworkManager = NetworkManager.singleton as MyServerNetworkManager;
		if (myServerNetworkManager == null)
		{
			return;
		}
		myServerNetworkManager.UploadLocalKingData(playerKingData);
	}

	// Token: 0x06000E73 RID: 3699 RVA: 0x00051F77 File Offset: 0x00050177
	protected static void InvokeUserCode_CmdUploadLocalKingData(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdUploadLocalKingData called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdUploadLocalKingData(Mirror.GeneratedNetworkCode._Read_SaveLoadManager/PlayerKingData(reader));
	}

	// Token: 0x06000E74 RID: 3700 RVA: 0x00051FA0 File Offset: 0x000501A0
	protected void UserCode_CmdAddBrotatoWeapon(BrotatoWeaponType brotatoWeaponType, uint skillId, float[] skillValues, int grade)
	{
		this.RpcAddBrotatoWeapon(brotatoWeaponType, skillId, skillValues, grade);
	}

	// Token: 0x06000E75 RID: 3701 RVA: 0x00051FAD File Offset: 0x000501AD
	protected static void InvokeUserCode_CmdAddBrotatoWeapon(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdAddBrotatoWeapon called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdAddBrotatoWeapon(Mirror.GeneratedNetworkCode._Read_BrotatoWeaponType(reader), reader.ReadUInt(), Mirror.GeneratedNetworkCode._Read_System.Single[](reader), reader.ReadInt());
	}

	// Token: 0x06000E76 RID: 3702 RVA: 0x00051FE8 File Offset: 0x000501E8
	protected void UserCode_RpcAddBrotatoWeapon(BrotatoWeaponType brotatoWeaponType, uint skillId, float[] skillValues, int grade)
	{
		this.brotatoWeaponController.AddBrotatoWeapon(brotatoWeaponType, skillId, this, skillValues, grade);
	}

	// Token: 0x06000E77 RID: 3703 RVA: 0x00051FFB File Offset: 0x000501FB
	protected static void InvokeUserCode_RpcAddBrotatoWeapon(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcAddBrotatoWeapon called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcAddBrotatoWeapon(Mirror.GeneratedNetworkCode._Read_BrotatoWeaponType(reader), reader.ReadUInt(), Mirror.GeneratedNetworkCode._Read_System.Single[](reader), reader.ReadInt());
	}

	// Token: 0x06000E78 RID: 3704 RVA: 0x00052036 File Offset: 0x00050236
	protected void UserCode_CmdRemoveBrotatoWeapon(uint skillId)
	{
		this.RpcRemoveBrotatoWeapon(skillId);
	}

	// Token: 0x06000E79 RID: 3705 RVA: 0x0005203F File Offset: 0x0005023F
	protected static void InvokeUserCode_CmdRemoveBrotatoWeapon(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdRemoveBrotatoWeapon called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdRemoveBrotatoWeapon(reader.ReadUInt());
	}

	// Token: 0x06000E7A RID: 3706 RVA: 0x00052068 File Offset: 0x00050268
	protected void UserCode_RpcRemoveBrotatoWeapon(uint skillId)
	{
		this.brotatoWeaponController.RemoveBrotatoWeapon(skillId);
	}

	// Token: 0x06000E7B RID: 3707 RVA: 0x00052076 File Offset: 0x00050276
	protected static void InvokeUserCode_RpcRemoveBrotatoWeapon(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcRemoveBrotatoWeapon called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpcRemoveBrotatoWeapon(reader.ReadUInt());
	}

	// Token: 0x06000E7C RID: 3708 RVA: 0x0005209F File Offset: 0x0005029F
	protected void UserCode_CmdAddEntryConditions(int value)
	{
		this.RpdAddEntryConditions(value);
	}

	// Token: 0x06000E7D RID: 3709 RVA: 0x000520A8 File Offset: 0x000502A8
	protected static void InvokeUserCode_CmdAddEntryConditions(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdAddEntryConditions called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdAddEntryConditions(reader.ReadInt());
	}

	// Token: 0x06000E7E RID: 3710 RVA: 0x000520D1 File Offset: 0x000502D1
	protected void UserCode_RpdAddEntryConditions(int value)
	{
		if (!this.entryConditions.Contains((EntryConditions)value))
		{
			this.entryConditions.Add((EntryConditions)value);
		}
	}

	// Token: 0x06000E7F RID: 3711 RVA: 0x000520ED File Offset: 0x000502ED
	protected static void InvokeUserCode_RpdAddEntryConditions(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpdAddEntryConditions called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_RpdAddEntryConditions(reader.ReadInt());
	}

	// Token: 0x06000E80 RID: 3712 RVA: 0x00052118 File Offset: 0x00050318
	protected void UserCode_TargetCreateDemonContract(Vector3 pos)
	{
		if (this.noKillBossTime < 0)
		{
			return;
		}
		this.noKillBossTime++;
		if (GameHelperClient.isReady)
		{
			GameHelperClient.localPlayer.StartSummon(EnemyType.NPC_Ghost, pos, GameHelperClient.localPlayer.netId, 1f, 1000L, 50, 9999f, null, 0L, 0L, -1);
		}
	}

	// Token: 0x06000E81 RID: 3713 RVA: 0x00052176 File Offset: 0x00050376
	protected static void InvokeUserCode_TargetCreateDemonContract(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("TargetRPC TargetCreateDemonContract called on server.");
			return;
		}
		((PlayerBase)obj).UserCode_TargetCreateDemonContract(reader.ReadVector3());
	}

	// Token: 0x06000E82 RID: 3714 RVA: 0x0005219F File Offset: 0x0005039F
	protected void UserCode_CmdDemonContract()
	{
		Game.EnemyManagerClient.playerDemonContract.Add(base.netId);
	}

	// Token: 0x06000E83 RID: 3715 RVA: 0x000521B6 File Offset: 0x000503B6
	protected static void InvokeUserCode_CmdDemonContract(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdDemonContract called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdDemonContract();
	}

	// Token: 0x06000E84 RID: 3716 RVA: 0x000521D9 File Offset: 0x000503D9
	protected void UserCode_CmdDummy(EnemyType enemyType, Vector3 pos, uint playerId, float newAttackSpeed, int newHp, int newAttackPower, float summonDeadTimeValue)
	{
		this.AddDummy(enemyType, pos, playerId, newAttackSpeed, newHp, newAttackPower, summonDeadTimeValue);
	}

	// Token: 0x06000E85 RID: 3717 RVA: 0x000521F0 File Offset: 0x000503F0
	protected static void InvokeUserCode_CmdDummy(NetworkBehaviour obj, NetworkReader reader, NetworkConnectionToClient senderConnection)
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("Command CmdDummy called on client.");
			return;
		}
		((PlayerBase)obj).UserCode_CmdDummy(Mirror.GeneratedNetworkCode._Read_EnemyType(reader), reader.ReadVector3(), reader.ReadUInt(), reader.ReadFloat(), reader.ReadInt(), reader.ReadInt(), reader.ReadFloat());
	}

	// Token: 0x06000E86 RID: 3718 RVA: 0x0005224C File Offset: 0x0005044C
	static PlayerBase()
	{
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdUpdateLucky", new CmdDelegate(PlayerBase.InvokeUserCode_CmdUpdateLucky), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdUpdatePickShare", new CmdDelegate(PlayerBase.InvokeUserCode_CmdUpdatePickShare), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdUpdateSkillRange", new CmdDelegate(PlayerBase.InvokeUserCode_CmdUpdateSkillRange), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdUpdateSkillAddTime", new CmdDelegate(PlayerBase.InvokeUserCode_CmdUpdateSkillAddTime), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "UpdateAddHenshinTime", new CmdDelegate(PlayerBase.InvokeUserCode_UpdateAddHenshinTime), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdUpdateMaxHpAddPercent", new CmdDelegate(PlayerBase.InvokeUserCode_CmdUpdateMaxHpAddPercent), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdUpdateCastSpeed", new CmdDelegate(PlayerBase.InvokeUserCode_CmdUpdateCastSpeed), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdUpdateHaloRangeAdd", new CmdDelegate(PlayerBase.InvokeUserCode_CmdUpdateHaloRangeAdd), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdAddMusicBuff", new CmdDelegate(PlayerBase.InvokeUserCode_CmdAddMusicBuff), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdPlayEffect", new CmdDelegate(PlayerBase.InvokeUserCode_CmdPlayEffect), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdPlayEffectObstruction", new CmdDelegate(PlayerBase.InvokeUserCode_CmdPlayEffectObstruction), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdPlayEffectEuler", new CmdDelegate(PlayerBase.InvokeUserCode_CmdPlayEffectEuler), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdPlayEffectAddRole", new CmdDelegate(PlayerBase.InvokeUserCode_CmdPlayEffectAddRole), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdUploadCard", new CmdDelegate(PlayerBase.InvokeUserCode_CmdUploadCard), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdCreateEnemy", new CmdDelegate(PlayerBase.InvokeUserCode_CmdCreateEnemy), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdCreateEnemyByPos", new CmdDelegate(PlayerBase.InvokeUserCode_CmdCreateEnemyByPos), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdCreateLocalTyrant", new CmdDelegate(PlayerBase.InvokeUserCode_CmdCreateLocalTyrant), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdPickItem", new CmdDelegate(PlayerBase.InvokeUserCode_CmdPickItem), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdTeleport", new CmdDelegate(PlayerBase.InvokeUserCode_CmdTeleport), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdTeleportForPos", new CmdDelegate(PlayerBase.InvokeUserCode_CmdTeleportForPos), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdCreateItem", new CmdDelegate(PlayerBase.InvokeUserCode_CmdCreateItem), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdCreateItemByPos", new CmdDelegate(PlayerBase.InvokeUserCode_CmdCreateItemByPos), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdCreateItemByPosWithNum", new CmdDelegate(PlayerBase.InvokeUserCode_CmdCreateItemByPosWithNum), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdCreateHeartDemon", new CmdDelegate(PlayerBase.InvokeUserCode_CmdCreateHeartDemon), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdClearSkill", new CmdDelegate(PlayerBase.InvokeUserCode_CmdClearSkill), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdClearSkillByData", new CmdDelegate(PlayerBase.InvokeUserCode_CmdClearSkillByData), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdStartSkillAciton", new CmdDelegate(PlayerBase.InvokeUserCode_CmdStartSkillAciton), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdEndSkillAciton", new CmdDelegate(PlayerBase.InvokeUserCode_CmdEndSkillAciton), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdAddBuff", new CmdDelegate(PlayerBase.InvokeUserCode_CmdAddBuff), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdRemoveuff", new CmdDelegate(PlayerBase.InvokeUserCode_CmdRemoveuff), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdXuanYun", new CmdDelegate(PlayerBase.InvokeUserCode_CmdXuanYun), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdAddAttackTarget", new CmdDelegate(PlayerBase.InvokeUserCode_CmdAddAttackTarget), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdUpdateSyncActiveSkillEnum", new CmdDelegate(PlayerBase.InvokeUserCode_CmdUpdateSyncActiveSkillEnum), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdReduce", new CmdDelegate(PlayerBase.InvokeUserCode_CmdReduce), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdEquipArmor", new CmdDelegate(PlayerBase.InvokeUserCode_CmdEquipArmor), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdEquipDoge", new CmdDelegate(PlayerBase.InvokeUserCode_CmdEquipDoge), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdUpdateBreakShield", new CmdDelegate(PlayerBase.InvokeUserCode_CmdUpdateBreakShield), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdUpdateSkillBreakShield", new CmdDelegate(PlayerBase.InvokeUserCode_CmdUpdateSkillBreakShield), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdAddAllPlayerItem", new CmdDelegate(PlayerBase.InvokeUserCode_CmdAddAllPlayerItem), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdEliteProbabilityAdd", new CmdDelegate(PlayerBase.InvokeUserCode_CmdEliteProbabilityAdd), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdUpdateAddHatred", new CmdDelegate(PlayerBase.InvokeUserCode_CmdUpdateAddHatred), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdChat", new CmdDelegate(PlayerBase.InvokeUserCode_CmdChat), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdAttackOtherPlayer", new CmdDelegate(PlayerBase.InvokeUserCode_CmdAttackOtherPlayer), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdUpdateAddRelifeTime", new CmdDelegate(PlayerBase.InvokeUserCode_CmdUpdateAddRelifeTime), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdUploadGameOverData", new CmdDelegate(PlayerBase.InvokeUserCode_CmdUploadGameOverData), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdUpdateSaiYaDarkBuff", new CmdDelegate(PlayerBase.InvokeUserCode_CmdUpdateSaiYaDarkBuff), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdUpdateSaiYaSkill3", new CmdDelegate(PlayerBase.InvokeUserCode_CmdUpdateSaiYaSkill3), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdUpdateRelicAdd", new CmdDelegate(PlayerBase.InvokeUserCode_CmdUpdateRelicAdd), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdUpdateBookAdd", new CmdDelegate(PlayerBase.InvokeUserCode_CmdUpdateBookAdd), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdUpdateForgingAdd", new CmdDelegate(PlayerBase.InvokeUserCode_CmdUpdateForgingAdd), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdHelpNpc", new CmdDelegate(PlayerBase.InvokeUserCode_CmdHelpNpc), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdUploadLocalKingData", new CmdDelegate(PlayerBase.InvokeUserCode_CmdUploadLocalKingData), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdAddBrotatoWeapon", new CmdDelegate(PlayerBase.InvokeUserCode_CmdAddBrotatoWeapon), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdRemoveBrotatoWeapon", new CmdDelegate(PlayerBase.InvokeUserCode_CmdRemoveBrotatoWeapon), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdAddEntryConditions", new CmdDelegate(PlayerBase.InvokeUserCode_CmdAddEntryConditions), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdDemonContract", new CmdDelegate(PlayerBase.InvokeUserCode_CmdDemonContract), true);
		RemoteCallHelper.RegisterCommandDelegate(typeof(PlayerBase), "CmdDummy", new CmdDelegate(PlayerBase.InvokeUserCode_CmdDummy), true);
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcUpdateCastSpeed", new CmdDelegate(PlayerBase.InvokeUserCode_RpcUpdateCastSpeed));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcUpdateHaloRangeAdd", new CmdDelegate(PlayerBase.InvokeUserCode_RpcUpdateHaloRangeAdd));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "ClientRelifePos", new CmdDelegate(PlayerBase.InvokeUserCode_ClientRelifePos));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcAddMusicBuff", new CmdDelegate(PlayerBase.InvokeUserCode_RpcAddMusicBuff));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcPlayEffect", new CmdDelegate(PlayerBase.InvokeUserCode_RpcPlayEffect));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcPlayEffectObstruction", new CmdDelegate(PlayerBase.InvokeUserCode_RpcPlayEffectObstruction));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcPlayEffectEuler", new CmdDelegate(PlayerBase.InvokeUserCode_RpcPlayEffectEuler));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcPlayEffectAddRole", new CmdDelegate(PlayerBase.InvokeUserCode_RpcPlayEffectAddRole));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "ClientRpcBornPos", new CmdDelegate(PlayerBase.InvokeUserCode_ClientRpcBornPos));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcUploadCard", new CmdDelegate(PlayerBase.InvokeUserCode_RpcUploadCard));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcPickItem", new CmdDelegate(PlayerBase.InvokeUserCode_RpcPickItem));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "ClientTeleportPos", new CmdDelegate(PlayerBase.InvokeUserCode_ClientTeleportPos));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcCreateItem", new CmdDelegate(PlayerBase.InvokeUserCode_RpcCreateItem));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcClearSkill", new CmdDelegate(PlayerBase.InvokeUserCode_RpcClearSkill));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcClearSkillByData", new CmdDelegate(PlayerBase.InvokeUserCode_RpcClearSkillByData));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcStartSkillAciton", new CmdDelegate(PlayerBase.InvokeUserCode_RpcStartSkillAciton));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcEndSkillAciton", new CmdDelegate(PlayerBase.InvokeUserCode_RpcEndSkillAciton));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcAddAttackTarget", new CmdDelegate(PlayerBase.InvokeUserCode_RpcAddAttackTarget));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcReduce", new CmdDelegate(PlayerBase.InvokeUserCode_RpcReduce));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcEquipArmor", new CmdDelegate(PlayerBase.InvokeUserCode_RpcEquipArmor));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcEquipDoge", new CmdDelegate(PlayerBase.InvokeUserCode_RpcEquipDoge));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcAddAllPlayerItem", new CmdDelegate(PlayerBase.InvokeUserCode_RpcAddAllPlayerItem));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcUpdateAddHatred", new CmdDelegate(PlayerBase.InvokeUserCode_RpcUpdateAddHatred));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcChat", new CmdDelegate(PlayerBase.InvokeUserCode_RpcChat));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcUpdateAddRelifeTime", new CmdDelegate(PlayerBase.InvokeUserCode_RpcUpdateAddRelifeTime));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcUploadGameOverData", new CmdDelegate(PlayerBase.InvokeUserCode_RpcUploadGameOverData));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcUpdateSaiYaDarkBuff", new CmdDelegate(PlayerBase.InvokeUserCode_RpcUpdateSaiYaDarkBuff));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcUpdateRelicAdd", new CmdDelegate(PlayerBase.InvokeUserCode_RpcUpdateRelicAdd));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcUpdateBookAdd", new CmdDelegate(PlayerBase.InvokeUserCode_RpcUpdateBookAdd));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcUpdateForgingAdd", new CmdDelegate(PlayerBase.InvokeUserCode_RpcUpdateForgingAdd));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "ClientRpcKingBornPos", new CmdDelegate(PlayerBase.InvokeUserCode_ClientRpcKingBornPos));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcAddBrotatoWeapon", new CmdDelegate(PlayerBase.InvokeUserCode_RpcAddBrotatoWeapon));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpcRemoveBrotatoWeapon", new CmdDelegate(PlayerBase.InvokeUserCode_RpcRemoveBrotatoWeapon));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "RpdAddEntryConditions", new CmdDelegate(PlayerBase.InvokeUserCode_RpdAddEntryConditions));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "OnKillBlacksmith", new CmdDelegate(PlayerBase.InvokeUserCode_OnKillBlacksmith));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "TargetKillGoblinMine", new CmdDelegate(PlayerBase.InvokeUserCode_TargetKillGoblinMine));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "TargetAttackOtherPlayer", new CmdDelegate(PlayerBase.InvokeUserCode_TargetAttackOtherPlayer));
		RemoteCallHelper.RegisterRpcDelegate(typeof(PlayerBase), "TargetCreateDemonContract", new CmdDelegate(PlayerBase.InvokeUserCode_TargetCreateDemonContract));
	}

	// Token: 0x06000E87 RID: 3719 RVA: 0x00052E74 File Offset: 0x00051074
	public override bool SerializeSyncVars(NetworkWriter writer, bool forceAll)
	{
		bool result = base.SerializeSyncVars(writer, forceAll);
		if (forceAll)
		{
			writer.WriteString(this.steamName);
			writer.WriteBool(this.isPickShare);
			writer.WriteInt(this.killEnemyNum);
			writer.WriteInt(this.lucky);
			writer.WriteFloat(this.skillRange);
			writer.WriteFloat(this.skillAddTime);
			writer.WriteFloat(this.addHenshinTime);
			writer.WriteFloat(this.maxHpAddPercent);
			Mirror.GeneratedNetworkCode._Write_ActiveSkillEnum(writer, this.syncActiveSkillEnum);
			return true;
		}
		writer.WriteULong(base.syncVarDirtyBits);
		if ((base.syncVarDirtyBits & 512UL) != 0UL)
		{
			writer.WriteString(this.steamName);
			result = true;
		}
		if ((base.syncVarDirtyBits & 1024UL) != 0UL)
		{
			writer.WriteBool(this.isPickShare);
			result = true;
		}
		if ((base.syncVarDirtyBits & 2048UL) != 0UL)
		{
			writer.WriteInt(this.killEnemyNum);
			result = true;
		}
		if ((base.syncVarDirtyBits & 4096UL) != 0UL)
		{
			writer.WriteInt(this.lucky);
			result = true;
		}
		if ((base.syncVarDirtyBits & 8192UL) != 0UL)
		{
			writer.WriteFloat(this.skillRange);
			result = true;
		}
		if ((base.syncVarDirtyBits & 16384UL) != 0UL)
		{
			writer.WriteFloat(this.skillAddTime);
			result = true;
		}
		if ((base.syncVarDirtyBits & 32768UL) != 0UL)
		{
			writer.WriteFloat(this.addHenshinTime);
			result = true;
		}
		if ((base.syncVarDirtyBits & 65536UL) != 0UL)
		{
			writer.WriteFloat(this.maxHpAddPercent);
			result = true;
		}
		if ((base.syncVarDirtyBits & 131072UL) != 0UL)
		{
			Mirror.GeneratedNetworkCode._Write_ActiveSkillEnum(writer, this.syncActiveSkillEnum);
			result = true;
		}
		return result;
	}

	// Token: 0x06000E88 RID: 3720 RVA: 0x00053050 File Offset: 0x00051250
	public override void DeserializeSyncVars(NetworkReader reader, bool initialState)
	{
		base.DeserializeSyncVars(reader, initialState);
		if (initialState)
		{
			string text = this.steamName;
			this.NetworksteamName = reader.ReadString();
			bool flag = this.isPickShare;
			this.NetworkisPickShare = reader.ReadBool();
			int num = this.killEnemyNum;
			this.NetworkkillEnemyNum = reader.ReadInt();
			int num2 = this.lucky;
			this.Networklucky = reader.ReadInt();
			float num3 = this.skillRange;
			this.NetworkskillRange = reader.ReadFloat();
			float num4 = this.skillAddTime;
			this.NetworkskillAddTime = reader.ReadFloat();
			float num5 = this.addHenshinTime;
			this.NetworkaddHenshinTime = reader.ReadFloat();
			float num6 = this.maxHpAddPercent;
			this.NetworkmaxHpAddPercent = reader.ReadFloat();
			ActiveSkillEnum activeSkillEnum = this.syncActiveSkillEnum;
			this.NetworksyncActiveSkillEnum = Mirror.GeneratedNetworkCode._Read_ActiveSkillEnum(reader);
			return;
		}
		long num7 = (long)reader.ReadULong();
		if ((num7 & 512L) != 0L)
		{
			string text2 = this.steamName;
			this.NetworksteamName = reader.ReadString();
		}
		if ((num7 & 1024L) != 0L)
		{
			bool flag2 = this.isPickShare;
			this.NetworkisPickShare = reader.ReadBool();
		}
		if ((num7 & 2048L) != 0L)
		{
			int num8 = this.killEnemyNum;
			this.NetworkkillEnemyNum = reader.ReadInt();
		}
		if ((num7 & 4096L) != 0L)
		{
			int num9 = this.lucky;
			this.Networklucky = reader.ReadInt();
		}
		if ((num7 & 8192L) != 0L)
		{
			float num10 = this.skillRange;
			this.NetworkskillRange = reader.ReadFloat();
		}
		if ((num7 & 16384L) != 0L)
		{
			float num11 = this.skillAddTime;
			this.NetworkskillAddTime = reader.ReadFloat();
		}
		if ((num7 & 32768L) != 0L)
		{
			float num12 = this.addHenshinTime;
			this.NetworkaddHenshinTime = reader.ReadFloat();
		}
		if ((num7 & 65536L) != 0L)
		{
			float num13 = this.maxHpAddPercent;
			this.NetworkmaxHpAddPercent = reader.ReadFloat();
		}
		if ((num7 & 131072L) != 0L)
		{
			ActiveSkillEnum activeSkillEnum2 = this.syncActiveSkillEnum;
			this.NetworksyncActiveSkillEnum = Mirror.GeneratedNetworkCode._Read_ActiveSkillEnum(reader);
		}
	}

	// Token: 0x04000D27 RID: 3367
	private BrotatoWeaponController brotatoWeaponController = new BrotatoWeaponController();

	// Token: 0x04000D28 RID: 3368
	private Action actionSucess;

	// Token: 0x04000D29 RID: 3369
	private Action actionFail;

	// Token: 0x04000D2A RID: 3370
	protected float inputTime;

	// Token: 0x04000D2B RID: 3371
	public List<SkillBase> roleSkillList = new List<SkillBase>();

	// Token: 0x04000D2C RID: 3372
	[HideInInspector]
	public List<EntryConditions> entryConditions = new List<EntryConditions>();

	// Token: 0x04000D49 RID: 3401
	private int level = 1;

	// Token: 0x04000D4A RID: 3402
	[HideInInspector]
	public long damageStatic;

	// Token: 0x04000D4B RID: 3403
	[HideInInspector]
	public int getGoldNum;

	// Token: 0x04000D4C RID: 3404
	[HideInInspector]
	public int getGemNum;

	// Token: 0x04000D4D RID: 3405
	[HideInInspector]
	public int dieNum;

	// Token: 0x04000D4E RID: 3406
	[HideInInspector]
	public int noKillBossTime;

	// Token: 0x04000D4F RID: 3407
	private float staAllAdd;

	// Token: 0x04000D50 RID: 3408
	private float agiAllAdd;

	// Token: 0x04000D51 RID: 3409
	private float strAllAdd;

	// Token: 0x04000D52 RID: 3410
	private float lastMoveTime;

	// Token: 0x04000D53 RID: 3411
	private float lerpAngle;

	// Token: 0x04000D54 RID: 3412
	protected CharacterController characterController;

	// Token: 0x04000D55 RID: 3413
	private float moveTimer;

	// Token: 0x04000D56 RID: 3414
	public PlayerAttribute playerAttribute;

	// Token: 0x04000D57 RID: 3415
	[HideInInspector]
	public HeroType heroType;

	// Token: 0x04000D58 RID: 3416
	[HideInInspector]
	[SyncVar]
	public string steamName;

	// Token: 0x04000D59 RID: 3417
	[HideInInspector]
	[SyncVar]
	public bool isPickShare = true;

	// Token: 0x04000D5A RID: 3418
	[HideInInspector]
	[SyncVar]
	public int killEnemyNum;

	// Token: 0x04000D5B RID: 3419
	[HideInInspector]
	public int killBossNum;

	// Token: 0x04000D5C RID: 3420
	[HideInInspector]
	public int enemyNum;

	// Token: 0x04000D5D RID: 3421
	private int initGold = 2500;

	// Token: 0x04000D5E RID: 3422
	private int initGem = 1;

	// Token: 0x04000D61 RID: 3425
	[HideInInspector]
	public int mpAddSecRate = 5;

	// Token: 0x04000D62 RID: 3426
	private float addMpTime;

	// Token: 0x04000D63 RID: 3427
	[HideInInspector]
	[SyncVar]
	public int lucky;

	// Token: 0x04000D64 RID: 3428
	[HideInInspector]
	public float normalAttackAddDamage;

	// Token: 0x04000D65 RID: 3429
	[HideInInspector]
	public float skillExDamage;

	// Token: 0x04000D66 RID: 3430
	[HideInInspector]
	[SyncVar]
	public float skillRange;

	// Token: 0x04000D67 RID: 3431
	[HideInInspector]
	public float hpAddUpgrade;

	// Token: 0x04000D68 RID: 3432
	[HideInInspector]
	[SyncVar]
	public float skillAddTime;

	// Token: 0x04000D69 RID: 3433
	[HideInInspector]
	public float skillMpUsed;

	// Token: 0x04000D6A RID: 3434
	[HideInInspector]
	public int skillCdReduce;

	// Token: 0x04000D6B RID: 3435
	[HideInInspector]
	public float addNormalEnemy;

	// Token: 0x04000D6C RID: 3436
	[HideInInspector]
	public float addBossEnemy;

	// Token: 0x04000D6D RID: 3437
	[HideInInspector]
	public float attackPercent;

	// Token: 0x04000D6E RID: 3438
	[HideInInspector]
	public float addEliteEnemy;

	// Token: 0x04000D6F RID: 3439
	[HideInInspector]
	public float buffAddDamage;

	// Token: 0x04000D70 RID: 3440
	[HideInInspector]
	public float addAttackEffectDamage;

	// Token: 0x04000D71 RID: 3441
	[HideInInspector]
	public float addHenshin;

	// Token: 0x04000D72 RID: 3442
	[HideInInspector]
	[SyncVar]
	public float addHenshinTime;

	// Token: 0x04000D73 RID: 3443
	[HideInInspector]
	public float armedAdd;

	// Token: 0x04000D74 RID: 3444
	[HideInInspector]
	public float addDamagePercent;

	// Token: 0x04000D75 RID: 3445
	[HideInInspector]
	public float addExp;

	// Token: 0x04000D76 RID: 3446
	[HideInInspector]
	public float addGoldPercent;

	// Token: 0x04000D77 RID: 3447
	public Action onPlayerLevelUp;

	// Token: 0x04000D78 RID: 3448
	[HideInInspector]
	public float addCallMonsterSize;

	// Token: 0x04000D79 RID: 3449
	[HideInInspector]
	public float addCallMonsterHp;

	// Token: 0x04000D7A RID: 3450
	[HideInInspector]
	public float addCallMonsterAttack;

	// Token: 0x04000D7B RID: 3451
	[HideInInspector]
	public float addCallMonsterTime;

	// Token: 0x04000D7C RID: 3452
	[HideInInspector]
	public float magicXiXue;

	// Token: 0x04000D7D RID: 3453
	[HideInInspector]
	public float skillNoneAdd;

	// Token: 0x04000D7E RID: 3454
	[HideInInspector]
	public float skillFireAdd;

	// Token: 0x04000D7F RID: 3455
	[HideInInspector]
	public float skillIceAdd;

	// Token: 0x04000D80 RID: 3456
	[HideInInspector]
	public float skillLightingAdd;

	// Token: 0x04000D81 RID: 3457
	[HideInInspector]
	public int attackEffectTime;

	// Token: 0x04000D82 RID: 3458
	[HideInInspector]
	public long baseMaxHp;

	// Token: 0x04000D83 RID: 3459
	public Action onPlayerRelife;

	// Token: 0x04000D84 RID: 3460
	private float relicAdd;

	// Token: 0x04000D85 RID: 3461
	private float bookAdd;

	// Token: 0x04000D86 RID: 3462
	private float forgingAdd;

	// Token: 0x04000D87 RID: 3463
	[HideInInspector]
	[SyncVar]
	public float maxHpAddPercent;

	// Token: 0x04000D88 RID: 3464
	[HideInInspector]
	public float castSpeed;

	// Token: 0x04000D89 RID: 3465
	[HideInInspector]
	public float haloRangeAdd;

	// Token: 0x04000D8A RID: 3466
	[HideInInspector]
	public PlayerModeBase playerModeBase;

	// Token: 0x04000D8B RID: 3467
	[HideInInspector]
	[SyncVar]
	public ActiveSkillEnum syncActiveSkillEnum = ActiveSkillEnum.None;

	// Token: 0x04000D8C RID: 3468
	public PlayerBase.UseItem useItemEvent;

	// Token: 0x04000D8D RID: 3469
	public PlayerBase.BuyItem buyItemEvent;

	// Token: 0x04000D8E RID: 3470
	public PlayerBase.NearEnemyDead nearEnemyDeadEvent;

	// Token: 0x04000D8F RID: 3471
	public PlayerBase.AddBagItem addBagItemEvent;

	// Token: 0x04000D90 RID: 3472
	[HideInInspector]
	public bool isMageHat;

	// Token: 0x04000D91 RID: 3473
	[HideInInspector]
	public List<float> shopDiscount;

	// Token: 0x04000D92 RID: 3474
	private PlayerKingAI playerKingAI;

	// Token: 0x02000293 RID: 659
	// (Invoke) Token: 0x06000E8A RID: 3722
	public delegate void UseItem(ItemType itemType);

	// Token: 0x02000294 RID: 660
	// (Invoke) Token: 0x06000E8E RID: 3726
	public delegate void BuyItem();

	// Token: 0x02000295 RID: 661
	// (Invoke) Token: 0x06000E92 RID: 3730
	public delegate void NearEnemyDead(RoleBase deadkRole);

	// Token: 0x02000296 RID: 662
	// (Invoke) Token: 0x06000E96 RID: 3734
	public delegate void AddBagItem(ItemType itemType);
}

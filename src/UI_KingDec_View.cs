using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200033E RID: 830
public class UI_KingDec_View : UGUIView
{
	// Token: 0x060012F7 RID: 4855 RVA: 0x00072340 File Offset: 0x00070540
	public override void Init(Transform trans)
	{
		this.ltext_level = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>();
		this.ltext_allDamage = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>();
		this.ltext_allMoney = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>();
		this.ltext_allGem = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(1).GetComponent<Text>();
		this.ltext_maxHp = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>();
		this.ltext_maxMp = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(1).GetComponent<Text>();
		this.ltext_str = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(4).GetChild(0).GetComponent<Text>();
		this.ltext_agi = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(4).GetChild(1).GetComponent<Text>();
		this.ltext_sta = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(5).GetChild(0).GetComponent<Text>();
		this.ltext_armor = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(5).GetChild(1).GetComponent<Text>();
		this.ltext_dodge = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(6).GetChild(0).GetComponent<Text>();
		this.ltext_skillReduction = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(6).GetChild(1).GetComponent<Text>();
		this.ltext_moveSpeed = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(7).GetChild(0).GetComponent<Text>();
		this.ltext_lucky = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(7).GetChild(1).GetComponent<Text>();
		this.ltext_hpAdd = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(8).GetChild(0).GetComponent<Text>();
		this.ltext_mpAdd = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(8).GetChild(1).GetComponent<Text>();
		this.ltext_hpSecRate = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(9).GetChild(0).GetComponent<Text>();
		this.ltext_attackAddHp = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(9).GetChild(1).GetComponent<Text>();
		this.ltext_lifeStealing = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(10).GetChild(0).GetComponent<Text>();
		this.ltext_magicXiXue = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(10).GetChild(1).GetComponent<Text>();
		this.ltext_attack = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(11).GetChild(0).GetComponent<Text>();
		this.ltext_attackSpeed = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(11).GetChild(1).GetComponent<Text>();
		this.ltext_critical = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(12).GetChild(0).GetComponent<Text>();
		this.ltext_criticalDamage = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(12).GetChild(1).GetComponent<Text>();
		this.ltext_normalDamage = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(13).GetChild(0).GetComponent<Text>();
		this.ltext_normalBreak = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(13).GetChild(1).GetComponent<Text>();
		this.ltext_skillDamage = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(14).GetChild(0).GetComponent<Text>();
		this.ltext_skillBreak = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(14).GetChild(1).GetComponent<Text>();
		this.ltext_skillCd = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(15).GetChild(0).GetComponent<Text>();
		this.ltext_skillRange = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(15).GetChild(1).GetComponent<Text>();
		this.ltext_skillTime = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(16).GetChild(0).GetComponent<Text>();
		this.ltext_skillExpend = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(16).GetChild(1).GetComponent<Text>();
		this.ltext_reduceInjury = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(17).GetChild(0).GetComponent<Text>();
		this.ltext_extraDamage = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(17).GetChild(1).GetComponent<Text>();
		this.ltext_attackDistance = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(18).GetChild(0).GetComponent<Text>();
		this.ltext_castSpeed = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(18).GetChild(1).GetComponent<Text>();
		this.ltext_skillNoneDamage = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(19).GetChild(0).GetComponent<Text>();
		this.ltext_fireDamage = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(19).GetChild(1).GetComponent<Text>();
		this.ltext_iceDamage = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(20).GetChild(0).GetComponent<Text>();
		this.ltext_lightDamage = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(20).GetChild(1).GetComponent<Text>();
		this.ltext_effectDamage = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(21).GetChild(0).GetComponent<Text>();
		this.ltext_hpAddUpgrade = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(21).GetChild(1).GetComponent<Text>();
		this.ltext_buffDamage = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(22).GetChild(0).GetComponent<Text>();
		this.ltext_haloRangeAdd = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(22).GetChild(1).GetComponent<Text>();
		this.ltext_addCallMonsterAttack = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(23).GetChild(0).GetComponent<Text>();
		this.ltext_addCallMonsterTime = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(23).GetChild(1).GetComponent<Text>();
		this.ltext_addHenshin = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(24).GetChild(0).GetComponent<Text>();
		this.ltext_addHenshinTime = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(24).GetChild(1).GetComponent<Text>();
		this.ltext_armedAdd = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(25).GetChild(0).GetComponent<Text>();
		this.ltext_equipAdd = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(25).GetChild(1).GetComponent<Text>();
		this.ltext_forgeAdd = trans.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(26).GetChild(0).GetComponent<Text>();
		this.btn_back = trans.GetChild(3).GetChild(0).GetComponent<Button>();
		this.btn_challenge = trans.GetChild(4).GetComponent<Button>();
		this.btn_copy = trans.GetChild(5).GetComponent<Button>();
	}

	// Token: 0x04001157 RID: 4439
	public Text ltext_level;

	// Token: 0x04001158 RID: 4440
	public Text ltext_allDamage;

	// Token: 0x04001159 RID: 4441
	public Text ltext_allMoney;

	// Token: 0x0400115A RID: 4442
	public Text ltext_allGem;

	// Token: 0x0400115B RID: 4443
	public Text ltext_maxHp;

	// Token: 0x0400115C RID: 4444
	public Text ltext_maxMp;

	// Token: 0x0400115D RID: 4445
	public Text ltext_str;

	// Token: 0x0400115E RID: 4446
	public Text ltext_agi;

	// Token: 0x0400115F RID: 4447
	public Text ltext_sta;

	// Token: 0x04001160 RID: 4448
	public Text ltext_armor;

	// Token: 0x04001161 RID: 4449
	public Text ltext_dodge;

	// Token: 0x04001162 RID: 4450
	public Text ltext_skillReduction;

	// Token: 0x04001163 RID: 4451
	public Text ltext_moveSpeed;

	// Token: 0x04001164 RID: 4452
	public Text ltext_lucky;

	// Token: 0x04001165 RID: 4453
	public Text ltext_hpAdd;

	// Token: 0x04001166 RID: 4454
	public Text ltext_mpAdd;

	// Token: 0x04001167 RID: 4455
	public Text ltext_hpSecRate;

	// Token: 0x04001168 RID: 4456
	public Text ltext_attackAddHp;

	// Token: 0x04001169 RID: 4457
	public Text ltext_lifeStealing;

	// Token: 0x0400116A RID: 4458
	public Text ltext_magicXiXue;

	// Token: 0x0400116B RID: 4459
	public Text ltext_attack;

	// Token: 0x0400116C RID: 4460
	public Text ltext_attackSpeed;

	// Token: 0x0400116D RID: 4461
	public Text ltext_critical;

	// Token: 0x0400116E RID: 4462
	public Text ltext_criticalDamage;

	// Token: 0x0400116F RID: 4463
	public Text ltext_normalDamage;

	// Token: 0x04001170 RID: 4464
	public Text ltext_normalBreak;

	// Token: 0x04001171 RID: 4465
	public Text ltext_skillDamage;

	// Token: 0x04001172 RID: 4466
	public Text ltext_skillBreak;

	// Token: 0x04001173 RID: 4467
	public Text ltext_skillCd;

	// Token: 0x04001174 RID: 4468
	public Text ltext_skillRange;

	// Token: 0x04001175 RID: 4469
	public Text ltext_skillTime;

	// Token: 0x04001176 RID: 4470
	public Text ltext_skillExpend;

	// Token: 0x04001177 RID: 4471
	public Text ltext_reduceInjury;

	// Token: 0x04001178 RID: 4472
	public Text ltext_extraDamage;

	// Token: 0x04001179 RID: 4473
	public Text ltext_attackDistance;

	// Token: 0x0400117A RID: 4474
	public Text ltext_castSpeed;

	// Token: 0x0400117B RID: 4475
	public Text ltext_skillNoneDamage;

	// Token: 0x0400117C RID: 4476
	public Text ltext_fireDamage;

	// Token: 0x0400117D RID: 4477
	public Text ltext_iceDamage;

	// Token: 0x0400117E RID: 4478
	public Text ltext_lightDamage;

	// Token: 0x0400117F RID: 4479
	public Text ltext_effectDamage;

	// Token: 0x04001180 RID: 4480
	public Text ltext_hpAddUpgrade;

	// Token: 0x04001181 RID: 4481
	public Text ltext_buffDamage;

	// Token: 0x04001182 RID: 4482
	public Text ltext_haloRangeAdd;

	// Token: 0x04001183 RID: 4483
	public Text ltext_addCallMonsterAttack;

	// Token: 0x04001184 RID: 4484
	public Text ltext_addCallMonsterTime;

	// Token: 0x04001185 RID: 4485
	public Text ltext_addHenshin;

	// Token: 0x04001186 RID: 4486
	public Text ltext_addHenshinTime;

	// Token: 0x04001187 RID: 4487
	public Text ltext_armedAdd;

	// Token: 0x04001188 RID: 4488
	public Text ltext_equipAdd;

	// Token: 0x04001189 RID: 4489
	public Text ltext_forgeAdd;

	// Token: 0x0400118A RID: 4490
	public Button btn_back;

	// Token: 0x0400118B RID: 4491
	public Button btn_challenge;

	// Token: 0x0400118C RID: 4492
	public Button btn_copy;
}

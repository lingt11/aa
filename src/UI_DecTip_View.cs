using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000311 RID: 785
public class UI_DecTip_View : UGUIView
{
	// Token: 0x0600122D RID: 4653 RVA: 0x0006CA54 File Offset: 0x0006AC54
	public override void Init(Transform trans)
	{
		this.trans_SkillDetail = trans.GetChild(0).GetComponent<Transform>();
		this.trans_roleAttribute = trans.GetChild(1).GetComponent<Transform>();
		this.ltext_attack = trans.GetChild(1).GetChild(2).GetChild(0).GetComponent<Text>();
		this.ltext_xixue = trans.GetChild(1).GetChild(2).GetChild(1).GetComponent<Text>();
		this.ltext_xixueRate = trans.GetChild(1).GetChild(2).GetChild(2).GetComponent<Text>();
		this.ltext_normalAddDamage = trans.GetChild(1).GetChild(2).GetChild(3).GetComponent<Text>();
		this.ltext_normalShieldDamage = trans.GetChild(1).GetChild(2).GetChild(4).GetComponent<Text>();
		this.ltext_attackSpeed = trans.GetChild(1).GetChild(2).GetChild(5).GetComponent<Text>();
		this.ltext_coolCd = trans.GetChild(1).GetChild(2).GetChild(6).GetComponent<Text>();
		this.ltext_skillAddDamage = trans.GetChild(1).GetChild(2).GetChild(7).GetComponent<Text>();
		this.ltext_skillShiledDamage = trans.GetChild(1).GetChild(2).GetChild(8).GetComponent<Text>();
		this.ltext_baoji = trans.GetChild(1).GetChild(2).GetChild(9).GetComponent<Text>();
		this.ltext_baojiDamage = trans.GetChild(1).GetChild(2).GetChild(10).GetComponent<Text>();
		this.ltext_armor = trans.GetChild(1).GetChild(2).GetChild(11).GetComponent<Text>();
		this.ltext_reduce = trans.GetChild(1).GetChild(2).GetChild(12).GetComponent<Text>();
		this.ltext_exDamage = trans.GetChild(1).GetChild(2).GetChild(13).GetComponent<Text>();
		this.ltext_buffAddDamage = trans.GetChild(1).GetChild(2).GetChild(14).GetComponent<Text>();
		this.ltext_effectAddDamage = trans.GetChild(1).GetChild(2).GetChild(15).GetComponent<Text>();
		this.ltext_allAddDamage = trans.GetChild(1).GetChild(2).GetChild(16).GetComponent<Text>();
		this.ltext_doge = trans.GetChild(1).GetChild(2).GetChild(17).GetComponent<Text>();
		this.ltext_lucky = trans.GetChild(1).GetChild(2).GetChild(18).GetComponent<Text>();
		this.ltext_hpAdd = trans.GetChild(1).GetChild(2).GetChild(19).GetComponent<Text>();
		this.ltext_mpAdd = trans.GetChild(1).GetChild(2).GetChild(20).GetComponent<Text>();
		this.ltext_moveSpeed = trans.GetChild(1).GetChild(2).GetChild(21).GetComponent<Text>();
		this.ltext_hpPercent = trans.GetChild(1).GetChild(2).GetChild(22).GetComponent<Text>();
		this.ltext_skillHit = trans.GetChild(1).GetChild(2).GetChild(23).GetComponent<Text>();
		this.ltext_forgingAdd = trans.GetChild(1).GetChild(2).GetChild(24).GetComponent<Text>();
		this.trans_forging = trans.GetChild(2).GetComponent<Transform>();
		this.ltext_forging = trans.GetChild(2).GetChild(4).GetComponent<Text>();
	}

	// Token: 0x0400105C RID: 4188
	public Transform trans_SkillDetail;

	// Token: 0x0400105D RID: 4189
	public Transform trans_roleAttribute;

	// Token: 0x0400105E RID: 4190
	public Text ltext_attack;

	// Token: 0x0400105F RID: 4191
	public Text ltext_xixue;

	// Token: 0x04001060 RID: 4192
	public Text ltext_xixueRate;

	// Token: 0x04001061 RID: 4193
	public Text ltext_normalAddDamage;

	// Token: 0x04001062 RID: 4194
	public Text ltext_normalShieldDamage;

	// Token: 0x04001063 RID: 4195
	public Text ltext_attackSpeed;

	// Token: 0x04001064 RID: 4196
	public Text ltext_coolCd;

	// Token: 0x04001065 RID: 4197
	public Text ltext_skillAddDamage;

	// Token: 0x04001066 RID: 4198
	public Text ltext_skillShiledDamage;

	// Token: 0x04001067 RID: 4199
	public Text ltext_baoji;

	// Token: 0x04001068 RID: 4200
	public Text ltext_baojiDamage;

	// Token: 0x04001069 RID: 4201
	public Text ltext_armor;

	// Token: 0x0400106A RID: 4202
	public Text ltext_reduce;

	// Token: 0x0400106B RID: 4203
	public Text ltext_exDamage;

	// Token: 0x0400106C RID: 4204
	public Text ltext_buffAddDamage;

	// Token: 0x0400106D RID: 4205
	public Text ltext_effectAddDamage;

	// Token: 0x0400106E RID: 4206
	public Text ltext_allAddDamage;

	// Token: 0x0400106F RID: 4207
	public Text ltext_doge;

	// Token: 0x04001070 RID: 4208
	public Text ltext_lucky;

	// Token: 0x04001071 RID: 4209
	public Text ltext_hpAdd;

	// Token: 0x04001072 RID: 4210
	public Text ltext_mpAdd;

	// Token: 0x04001073 RID: 4211
	public Text ltext_moveSpeed;

	// Token: 0x04001074 RID: 4212
	public Text ltext_hpPercent;

	// Token: 0x04001075 RID: 4213
	public Text ltext_skillHit;

	// Token: 0x04001076 RID: 4214
	public Text ltext_forgingAdd;

	// Token: 0x04001077 RID: 4215
	public Transform trans_forging;

	// Token: 0x04001078 RID: 4216
	public Text ltext_forging;
}

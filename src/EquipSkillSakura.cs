using System;
using UnityEngine;

// Token: 0x020000F5 RID: 245
public class EquipSkillSakura : EquipSkillBase
{
	// Token: 0x0600050C RID: 1292 RVA: 0x0001E024 File Offset: 0x0001C224
	public override void Init()
	{
		base.Init();
		this.equipValue = Mathf.RoundToInt(this.skillValueAry[0]);
		this.equipUpValue = Mathf.RoundToInt(this.skillValueUpAry[0]);
		this.playerBase.extraDamage += this.equipValue;
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x0001E075 File Offset: 0x0001C275
	public override void AddEquipNum()
	{
		base.AddEquipNum();
		this.playerBase.extraDamage += this.equipValue;
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x0001E095 File Offset: 0x0001C295
	public override void RemoveEquipNum()
	{
		base.RemoveEquipNum();
		this.playerBase.extraDamage -= this.equipValue;
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x0001E0B5 File Offset: 0x0001C2B5
	public override void Clear()
	{
		base.Clear();
		if (this.equipNum > 0)
		{
			this.playerBase.extraDamage -= this.equipValue;
		}
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x0001E0DE File Offset: 0x0001C2DE
	public override void OnUpdateStrengLevel(int updateLevel)
	{
		base.OnUpdateStrengLevel(updateLevel);
		this.playerBase.extraDamage += updateLevel * this.equipUpValue;
	}

	// Token: 0x0400047D RID: 1149
	private int equipValue;

	// Token: 0x0400047E RID: 1150
	private int equipUpValue;
}

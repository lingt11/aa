using System;

// Token: 0x020000ED RID: 237
public class EquipSkillMadMan : EquipSkillBase
{
	// Token: 0x060004E3 RID: 1251 RVA: 0x0001D3E4 File Offset: 0x0001B5E4
	public override void Init()
	{
		base.Init();
		this.equipValue = this.skillValueAry[0] * 0.01f;
		this.equipUpValue = this.skillValueUpAry[0] * 0.01f;
		this.playerBase.hpAddSecRate += this.equipValue;
	}

	// Token: 0x060004E4 RID: 1252 RVA: 0x0001D437 File Offset: 0x0001B637
	public override void AddEquipNum()
	{
		base.AddEquipNum();
		this.playerBase.hpAddSecRate += this.equipValue;
	}

	// Token: 0x060004E5 RID: 1253 RVA: 0x0001D457 File Offset: 0x0001B657
	public override void RemoveEquipNum()
	{
		base.RemoveEquipNum();
		this.playerBase.hpAddSecRate -= this.equipValue;
	}

	// Token: 0x060004E6 RID: 1254 RVA: 0x0001D477 File Offset: 0x0001B677
	public override void Clear()
	{
		base.Clear();
		if (this.equipNum > 0)
		{
			this.playerBase.hpAddSecRate -= this.equipValue;
		}
	}

	// Token: 0x060004E7 RID: 1255 RVA: 0x0001D4A0 File Offset: 0x0001B6A0
	public override void OnUpdateStrengLevel(int updateLevel)
	{
		base.OnUpdateStrengLevel(updateLevel);
		this.playerBase.hpAddSecRate += (float)updateLevel * this.equipUpValue;
	}

	// Token: 0x04000469 RID: 1129
	private float equipValue;

	// Token: 0x0400046A RID: 1130
	private float equipUpValue;
}

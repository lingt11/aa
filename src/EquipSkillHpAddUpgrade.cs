using System;

// Token: 0x020000EC RID: 236
public class EquipSkillHpAddUpgrade : EquipSkillBase
{
	// Token: 0x060004DD RID: 1245 RVA: 0x0001D304 File Offset: 0x0001B504
	public override void Init()
	{
		base.Init();
		this.equipValue = this.skillValueAry[0] * 0.01f;
		this.equipUpValue = this.skillValueUpAry[0] * 0.01f;
		this.playerBase.hpAddUpgrade += this.equipValue;
	}

	// Token: 0x060004DE RID: 1246 RVA: 0x0001D357 File Offset: 0x0001B557
	public override void AddEquipNum()
	{
		base.AddEquipNum();
		this.playerBase.hpAddUpgrade += this.equipValue;
	}

	// Token: 0x060004DF RID: 1247 RVA: 0x0001D377 File Offset: 0x0001B577
	public override void RemoveEquipNum()
	{
		base.RemoveEquipNum();
		this.playerBase.hpAddUpgrade -= this.equipValue;
	}

	// Token: 0x060004E0 RID: 1248 RVA: 0x0001D397 File Offset: 0x0001B597
	public override void Clear()
	{
		base.Clear();
		if (this.equipNum > 0)
		{
			this.playerBase.hpAddUpgrade -= this.equipValue;
		}
	}

	// Token: 0x060004E1 RID: 1249 RVA: 0x0001D3C0 File Offset: 0x0001B5C0
	public override void OnUpdateStrengLevel(int updateLevel)
	{
		base.OnUpdateStrengLevel(updateLevel);
		this.playerBase.hpAddUpgrade += (float)updateLevel * this.equipUpValue;
	}

	// Token: 0x04000467 RID: 1127
	private float equipValue;

	// Token: 0x04000468 RID: 1128
	private float equipUpValue;
}

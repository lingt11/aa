using System;

// Token: 0x020000FD RID: 253
public class EquipSkillStoneMask : EquipSkillBase
{
	// Token: 0x06000532 RID: 1330 RVA: 0x0001E8B0 File Offset: 0x0001CAB0
	public override void Init()
	{
		base.Init();
		this.equipValue = this.skillValueAry[0] * 0.01f;
		this.equipUpValue = this.skillValueUpAry[0] * 0.01f;
		this.playerBase.hpAddSecRate -= this.equipValue;
	}

	// Token: 0x06000533 RID: 1331 RVA: 0x0001E903 File Offset: 0x0001CB03
	public override void AddEquipNum()
	{
		base.AddEquipNum();
		this.playerBase.hpAddSecRate -= this.equipValue;
	}

	// Token: 0x06000534 RID: 1332 RVA: 0x0001E923 File Offset: 0x0001CB23
	public override void RemoveEquipNum()
	{
		base.RemoveEquipNum();
		this.playerBase.hpAddSecRate += this.equipValue;
	}

	// Token: 0x06000535 RID: 1333 RVA: 0x0001E943 File Offset: 0x0001CB43
	public override void Clear()
	{
		base.Clear();
		if (this.equipNum > 0)
		{
			this.playerBase.hpAddSecRate += this.equipValue;
		}
	}

	// Token: 0x06000536 RID: 1334 RVA: 0x0001E96C File Offset: 0x0001CB6C
	public override void OnUpdateStrengLevel(int updateLevel)
	{
		base.OnUpdateStrengLevel(updateLevel);
		this.playerBase.hpAddSecRate -= (float)updateLevel * this.equipUpValue;
	}

	// Token: 0x04000486 RID: 1158
	private float equipValue;

	// Token: 0x04000487 RID: 1159
	private float equipUpValue;
}

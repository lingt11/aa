using System;

// Token: 0x020000E6 RID: 230
public class EquipSkillFrierenStaff : EquipSkillBase
{
	// Token: 0x060004BE RID: 1214 RVA: 0x0001CC68 File Offset: 0x0001AE68
	public override void Init()
	{
		base.Init();
		this.equipValue = this.skillValueAry[0] * 0.01f;
		this.equipUpValue = this.skillValueUpAry[0] * 0.01f;
		this.playerBase.skillMpUsed -= this.equipValue;
	}

	// Token: 0x060004BF RID: 1215 RVA: 0x0001CCBB File Offset: 0x0001AEBB
	public override void AddEquipNum()
	{
		base.AddEquipNum();
		this.playerBase.skillMpUsed -= this.equipValue;
	}

	// Token: 0x060004C0 RID: 1216 RVA: 0x0001CCDB File Offset: 0x0001AEDB
	public override void RemoveEquipNum()
	{
		base.RemoveEquipNum();
		this.playerBase.skillMpUsed += this.equipValue;
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x0001CCFB File Offset: 0x0001AEFB
	public override void Clear()
	{
		base.Clear();
		if (this.equipNum > 0)
		{
			this.playerBase.skillMpUsed += this.equipValue;
		}
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x0001CD24 File Offset: 0x0001AF24
	public override void OnUpdateStrengLevel(int updateLevel)
	{
		base.OnUpdateStrengLevel(updateLevel);
		this.playerBase.skillMpUsed -= (float)updateLevel * this.equipUpValue;
	}

	// Token: 0x0400045F RID: 1119
	private float equipValue;

	// Token: 0x04000460 RID: 1120
	private float equipUpValue;
}

using System;

// Token: 0x02000101 RID: 257
public class EquipSkillVampireCode : EquipSkillBase
{
	// Token: 0x06000547 RID: 1351 RVA: 0x0001ED34 File Offset: 0x0001CF34
	public override void Init()
	{
		base.Init();
		this.equipValue = this.skillValueAry[0] * 0.01f;
		this.equipUpValue = this.skillValueUpAry[0] * 0.01f;
		this.playerBase.magicXiXue += this.equipValue;
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x0001ED87 File Offset: 0x0001CF87
	public override void AddEquipNum()
	{
		base.AddEquipNum();
		this.playerBase.magicXiXue += this.equipValue;
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x0001EDA7 File Offset: 0x0001CFA7
	public override void RemoveEquipNum()
	{
		base.RemoveEquipNum();
		this.playerBase.magicXiXue -= this.equipValue;
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x0001EDC7 File Offset: 0x0001CFC7
	public override void Clear()
	{
		base.Clear();
		if (this.equipNum > 0)
		{
			this.playerBase.magicXiXue -= this.equipValue;
		}
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x0001EDF0 File Offset: 0x0001CFF0
	public override void OnUpdateStrengLevel(int updateLevel)
	{
		base.OnUpdateStrengLevel(updateLevel);
		this.playerBase.magicXiXue += (float)updateLevel * this.equipUpValue;
	}

	// Token: 0x0400048C RID: 1164
	private float equipValue;

	// Token: 0x0400048D RID: 1165
	private float equipUpValue;
}

using System;

// Token: 0x020000EE RID: 238
public class EquipSkillMagicalGirl : EquipSkillBase
{
	// Token: 0x060004E9 RID: 1257 RVA: 0x0001D4C4 File Offset: 0x0001B6C4
	public override void Init()
	{
		base.Init();
		this.equipValueRange = this.skillValueAry[0] * 0.01f;
		this.equipUpValueRange = this.skillValueUpAry[0] * 0.01f;
		this.equipValueTime = this.skillValueAry[1] * 0.01f;
		this.equipUpValueTime = this.skillValueUpAry[1] * 0.01f;
		this.playerBase.CmdUpdateSkillRange(this.equipValueRange);
		this.playerBase.CmdUpdateSkillAddTime(this.equipValueTime);
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x0001D549 File Offset: 0x0001B749
	public override void AddEquipNum()
	{
		base.AddEquipNum();
		this.playerBase.CmdUpdateSkillRange(this.equipValueRange);
		this.playerBase.CmdUpdateSkillAddTime(this.equipValueTime);
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x0001D573 File Offset: 0x0001B773
	public override void RemoveEquipNum()
	{
		base.RemoveEquipNum();
		this.playerBase.CmdUpdateSkillRange(-this.equipValueRange);
		this.playerBase.CmdUpdateSkillAddTime(-this.equipValueTime);
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x0001D59F File Offset: 0x0001B79F
	public override void Clear()
	{
		base.Clear();
		if (this.equipNum > 0)
		{
			this.playerBase.CmdUpdateSkillRange(-this.equipValueRange);
			this.playerBase.CmdUpdateSkillAddTime(-this.equipValueTime);
		}
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x0001D5D4 File Offset: 0x0001B7D4
	public override void OnUpdateStrengLevel(int updateLevel)
	{
		base.OnUpdateStrengLevel(updateLevel);
		this.playerBase.CmdUpdateSkillRange((float)updateLevel * this.equipUpValueRange);
		this.playerBase.CmdUpdateSkillAddTime((float)updateLevel * this.equipUpValueTime);
	}

	// Token: 0x0400046B RID: 1131
	private float equipValueRange;

	// Token: 0x0400046C RID: 1132
	private float equipUpValueRange;

	// Token: 0x0400046D RID: 1133
	private float equipValueTime;

	// Token: 0x0400046E RID: 1134
	private float equipUpValueTime;
}

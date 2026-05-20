using System;

// Token: 0x020000FF RID: 255
public class EquipSkillTimeCore : EquipSkillBase
{
	// Token: 0x0600053D RID: 1341 RVA: 0x0001EB8C File Offset: 0x0001CD8C
	public override void Init()
	{
		base.Init();
		this.equipValue = this.skillValueAry[0] * 0.01f;
		this.equipUpValue = this.skillValueUpAry[0] * 0.01f;
		this.playerBase.CmdUpdateSkillAddTime(this.equipValue);
	}

	// Token: 0x0600053E RID: 1342 RVA: 0x0001EBD8 File Offset: 0x0001CDD8
	public override void AddEquipNum()
	{
		base.AddEquipNum();
		this.playerBase.CmdUpdateSkillAddTime(this.equipValue);
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x0001EBF1 File Offset: 0x0001CDF1
	public override void RemoveEquipNum()
	{
		base.RemoveEquipNum();
		this.playerBase.CmdUpdateSkillAddTime(-this.equipValue);
	}

	// Token: 0x06000540 RID: 1344 RVA: 0x0001EC0B File Offset: 0x0001CE0B
	public override void Clear()
	{
		base.Clear();
		if (this.equipNum > 0)
		{
			this.playerBase.CmdUpdateSkillAddTime(-this.equipValue);
		}
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x0001EC2E File Offset: 0x0001CE2E
	public override void OnUpdateStrengLevel(int updateLevel)
	{
		base.OnUpdateStrengLevel(updateLevel);
		this.playerBase.CmdUpdateSkillAddTime((float)updateLevel * this.equipUpValue);
	}

	// Token: 0x0400048A RID: 1162
	private float equipValue;

	// Token: 0x0400048B RID: 1163
	private float equipUpValue;
}

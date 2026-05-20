using System;

// Token: 0x020000E8 RID: 232
public class EquipSkillGorgeousCape : EquipSkillBase
{
	// Token: 0x060004C8 RID: 1224 RVA: 0x0001CE00 File Offset: 0x0001B000
	public override void Init()
	{
		base.Init();
		this.equipValue = this.skillValueAry[0] * 0.01f;
		this.equipUpValue = this.skillValueUpAry[0] * 0.01f;
		this.playerBase.CmdUpdateCastSpeed(this.equipValue);
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x0001CE4C File Offset: 0x0001B04C
	public override void AddEquipNum()
	{
		base.AddEquipNum();
		this.playerBase.CmdUpdateCastSpeed(this.equipValue);
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x0001CE65 File Offset: 0x0001B065
	public override void RemoveEquipNum()
	{
		base.RemoveEquipNum();
		this.playerBase.CmdUpdateCastSpeed(-this.equipValue);
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x0001CE7F File Offset: 0x0001B07F
	public override void Clear()
	{
		base.Clear();
		if (this.equipNum > 0)
		{
			this.playerBase.CmdUpdateCastSpeed(-this.equipValue);
		}
	}

	// Token: 0x060004CC RID: 1228 RVA: 0x0001CEA2 File Offset: 0x0001B0A2
	public override void OnUpdateStrengLevel(int updateLevel)
	{
		base.OnUpdateStrengLevel(updateLevel);
		this.playerBase.CmdUpdateCastSpeed((float)updateLevel * this.equipUpValue);
	}

	// Token: 0x04000461 RID: 1121
	private float equipValue;

	// Token: 0x04000462 RID: 1122
	private float equipUpValue;
}

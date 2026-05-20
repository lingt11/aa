using System;

// Token: 0x020000F4 RID: 244
public class EquipSkillRingOfLife : EquipSkillBase
{
	// Token: 0x06000506 RID: 1286 RVA: 0x0001DEE0 File Offset: 0x0001C0E0
	public override void Init()
	{
		base.Init();
		this.equipValueHp = this.skillValueAry[0] * 0.01f;
		this.equipUpValueHp = this.skillValueUpAry[0] * 0.01f;
		this.equipValueHalo = this.skillValueAry[1];
		this.equipUpValueHalo = this.skillValueUpAry[1];
		if (this.playerBase.roleType != RoleType.King)
		{
			this.playerBase.CmdUpdateMaxHpAddPercent(this.equipValueHp);
		}
		this.playerBase.CmdUpdateHaloRangeAdd(this.equipValueHalo);
	}

	// Token: 0x06000507 RID: 1287 RVA: 0x0001DF67 File Offset: 0x0001C167
	public override void AddEquipNum()
	{
		base.AddEquipNum();
		this.playerBase.CmdUpdateMaxHpAddPercent(this.equipValueHp);
		this.playerBase.CmdUpdateHaloRangeAdd(this.equipValueHalo);
	}

	// Token: 0x06000508 RID: 1288 RVA: 0x0001DF91 File Offset: 0x0001C191
	public override void RemoveEquipNum()
	{
		base.RemoveEquipNum();
		this.playerBase.CmdUpdateMaxHpAddPercent(-this.equipValueHp);
		this.playerBase.CmdUpdateHaloRangeAdd(-this.equipValueHalo);
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x0001DFBD File Offset: 0x0001C1BD
	public override void Clear()
	{
		base.Clear();
		if (this.equipNum > 0)
		{
			this.playerBase.CmdUpdateMaxHpAddPercent(-this.equipValueHp);
			this.playerBase.CmdUpdateHaloRangeAdd(-this.equipValueHalo);
		}
	}

	// Token: 0x0600050A RID: 1290 RVA: 0x0001DFF2 File Offset: 0x0001C1F2
	public override void OnUpdateStrengLevel(int updateLevel)
	{
		base.OnUpdateStrengLevel(updateLevel);
		this.playerBase.CmdUpdateMaxHpAddPercent((float)updateLevel * this.equipUpValueHp);
		this.playerBase.CmdUpdateHaloRangeAdd((float)updateLevel * this.equipUpValueHalo);
	}

	// Token: 0x04000479 RID: 1145
	private float equipValueHp;

	// Token: 0x0400047A RID: 1146
	private float equipUpValueHp;

	// Token: 0x0400047B RID: 1147
	private float equipValueHalo;

	// Token: 0x0400047C RID: 1148
	private float equipUpValueHalo;
}

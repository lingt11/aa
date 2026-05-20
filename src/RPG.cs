using System;

// Token: 0x020001CF RID: 463
public class RPG : PasssiveSkill
{
	// Token: 0x0600087C RID: 2172 RVA: 0x0003064C File Offset: 0x0002E84C
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		if (roleBase != null)
		{
			this.passSkillIndex = SkillManager.GetSyncPassSkillIndex();
			roleBase.CmdAddBrotatoWeapon(BrotatoWeaponType.RPG, this.passSkillIndex, this.skillValues, DropDefine.QualityAry.IndexOf(this.data.DIC("quality")));
		}
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x0003069C File Offset: 0x0002E89C
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		if (roleBase != null)
		{
			roleBase.CmdRemoveBrotatoWeapon(this.passSkillIndex);
		}
	}

	// Token: 0x04000B8C RID: 2956
	public uint passSkillIndex;
}

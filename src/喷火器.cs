using System;

// Token: 0x020001D9 RID: 473
public class 喷火器 : PasssiveSkill
{
	// Token: 0x0600089C RID: 2204 RVA: 0x00030C90 File Offset: 0x0002EE90
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		if (roleBase != null)
		{
			this.passSkillIndex = SkillManager.GetSyncPassSkillIndex();
			roleBase.CmdAddBrotatoWeapon(BrotatoWeaponType.Flamethrower, this.passSkillIndex, this.skillValues, DropDefine.QualityAry.IndexOf(this.data.DIC("quality")));
		}
	}

	// Token: 0x0600089D RID: 2205 RVA: 0x00030CE0 File Offset: 0x0002EEE0
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		if (roleBase != null)
		{
			roleBase.CmdRemoveBrotatoWeapon(this.passSkillIndex);
		}
	}

	// Token: 0x04000B8E RID: 2958
	public uint passSkillIndex;
}

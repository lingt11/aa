using System;

// Token: 0x020001E3 RID: 483
public class 枪灵 : PasssiveSkill
{
	// Token: 0x060008C0 RID: 2240 RVA: 0x00031514 File Offset: 0x0002F714
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		if (roleBase != null)
		{
			this.passSkillIndex = SkillManager.GetSyncPassSkillIndex();
			roleBase.CmdAddBrotatoWeapon(BrotatoWeaponType.Pistol, this.passSkillIndex, this.skillValues, DropDefine.QualityAry.IndexOf(this.data.DIC("quality")));
		}
	}

	// Token: 0x060008C1 RID: 2241 RVA: 0x00031564 File Offset: 0x0002F764
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		if (roleBase != null)
		{
			roleBase.CmdRemoveBrotatoWeapon(this.passSkillIndex);
		}
	}

	// Token: 0x04000B9B RID: 2971
	public uint passSkillIndex;
}

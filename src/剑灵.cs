using System;

// Token: 0x020001D6 RID: 470
public class 剑灵 : PasssiveSkill
{
	// Token: 0x06000893 RID: 2195 RVA: 0x00030B18 File Offset: 0x0002ED18
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		if (roleBase != null)
		{
			this.passSkillIndex = SkillManager.GetSyncPassSkillIndex();
			roleBase.CmdAddBrotatoWeapon(BrotatoWeaponType.Sword, this.passSkillIndex, this.skillValues, DropDefine.QualityAry.IndexOf(this.data.DIC("quality")));
		}
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x00030B68 File Offset: 0x0002ED68
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		if (roleBase != null)
		{
			roleBase.CmdRemoveBrotatoWeapon(this.passSkillIndex);
		}
	}

	// Token: 0x04000B8D RID: 2957
	public uint passSkillIndex;
}

using System;

// Token: 0x020001E6 RID: 486
public class 火箭炮 : PasssiveSkill
{
	// Token: 0x060008CB RID: 2251 RVA: 0x00031724 File Offset: 0x0002F924
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		if (roleBase != null)
		{
			this.passSkillIndex = SkillManager.GetSyncPassSkillIndex();
			roleBase.CmdAddBrotatoWeapon(BrotatoWeaponType.RPG, this.passSkillIndex, this.skillValues, DropDefine.QualityAry.IndexOf(this.data.DIC("quality")));
		}
	}

	// Token: 0x060008CC RID: 2252 RVA: 0x00031774 File Offset: 0x0002F974
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		if (roleBase != null)
		{
			roleBase.CmdRemoveBrotatoWeapon(this.passSkillIndex);
		}
	}

	// Token: 0x04000B9D RID: 2973
	public uint passSkillIndex;
}

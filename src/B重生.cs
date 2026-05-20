using System;

// Token: 0x02000195 RID: 405
public class B重生 : PasssiveSkill
{
	// Token: 0x060007A7 RID: 1959 RVA: 0x0002D326 File Offset: 0x0002B526
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.dieEvent = (RoleBase.DieEvent)Delegate.Combine(roleBase.dieEvent, new RoleBase.DieEvent(this.DieEvent));
	}

	// Token: 0x060007A8 RID: 1960 RVA: 0x0002D34F File Offset: 0x0002B54F
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.dieEvent = (RoleBase.DieEvent)Delegate.Remove(roleBase.dieEvent, new RoleBase.DieEvent(this.DieEvent));
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x0002B155 File Offset: 0x00029355
	private void DieEvent(RoleBase role)
	{
		if (base.CheckCD())
		{
			return;
		}
		if (GameHelperClient.isKingBattle)
		{
			this.updateCd = 999f;
		}
		if (role.hasAuthority)
		{
			role.CmdRelife();
		}
	}
}

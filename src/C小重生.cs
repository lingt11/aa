using System;

// Token: 0x020001A1 RID: 417
public class C小重生 : PasssiveSkill
{
	// Token: 0x060007D6 RID: 2006 RVA: 0x0002DFBE File Offset: 0x0002C1BE
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.dieEvent = (RoleBase.DieEvent)Delegate.Combine(roleBase.dieEvent, new RoleBase.DieEvent(this.DieEvent));
	}

	// Token: 0x060007D7 RID: 2007 RVA: 0x0002DFE7 File Offset: 0x0002C1E7
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.dieEvent = (RoleBase.DieEvent)Delegate.Remove(roleBase.dieEvent, new RoleBase.DieEvent(this.DieEvent));
	}

	// Token: 0x060007D8 RID: 2008 RVA: 0x0002B155 File Offset: 0x00029355
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

using System;

// Token: 0x0200016A RID: 362
public class A几乎无限重生 : PasssiveSkill
{
	// Token: 0x06000717 RID: 1815 RVA: 0x0002B103 File Offset: 0x00029303
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.dieEvent = (RoleBase.DieEvent)Delegate.Combine(roleBase.dieEvent, new RoleBase.DieEvent(this.DieEvent));
	}

	// Token: 0x06000718 RID: 1816 RVA: 0x0002B12C File Offset: 0x0002932C
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.dieEvent = (RoleBase.DieEvent)Delegate.Remove(roleBase.dieEvent, new RoleBase.DieEvent(this.DieEvent));
	}

	// Token: 0x06000719 RID: 1817 RVA: 0x0002B155 File Offset: 0x00029355
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

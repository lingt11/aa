using System;

// Token: 0x020001DD RID: 477
public class 攻速加成 : PasssiveSkill
{
	// Token: 0x060008A9 RID: 2217 RVA: 0x0003107F File Offset: 0x0002F27F
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.criticalEvent = (RoleBase.Critical)Delegate.Combine(roleBase.criticalEvent, new RoleBase.Critical(this.CriticalEvent));
	}

	// Token: 0x060008AA RID: 2218 RVA: 0x000310A8 File Offset: 0x0002F2A8
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.criticalEvent = (RoleBase.Critical)Delegate.Remove(roleBase.criticalEvent, new RoleBase.Critical(this.CriticalEvent));
	}

	// Token: 0x060008AB RID: 2219 RVA: 0x000310D4 File Offset: 0x0002F2D4
	private void CriticalEvent(RoleBase hurtRole, long damage)
	{
		Buff攻速加成 buff攻速加成 = new Buff攻速加成();
		buff攻速加成.addValue = this.skillValues[0] / 100f;
		this.roleBase.roleBuffManager.AddOneBuff("攻速加成", 5f, buff攻速加成);
	}
}

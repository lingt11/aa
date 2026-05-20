using System;

// Token: 0x020001B9 RID: 441
public class D长臂猿 : PasssiveSkill
{
	// Token: 0x0600082E RID: 2094 RVA: 0x0002B727 File Offset: 0x00029927
	public override void Enter()
	{
		this.roleBase.exAttackDistance += base.Distance;
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x0002B741 File Offset: 0x00029941
	public override void Exit()
	{
		this.roleBase.exAttackDistance -= base.Distance;
	}
}

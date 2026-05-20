using System;

// Token: 0x02000171 RID: 369
public class A究超长臂猿 : PasssiveSkill
{
	// Token: 0x06000731 RID: 1841 RVA: 0x0002B727 File Offset: 0x00029927
	public override void Enter()
	{
		this.roleBase.exAttackDistance += base.Distance;
	}

	// Token: 0x06000732 RID: 1842 RVA: 0x0002B741 File Offset: 0x00029941
	public override void Exit()
	{
		this.roleBase.exAttackDistance -= base.Distance;
	}
}

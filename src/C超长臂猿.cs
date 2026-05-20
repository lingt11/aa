using System;

// Token: 0x020001A7 RID: 423
public class C超长臂猿 : PasssiveSkill
{
	// Token: 0x060007ED RID: 2029 RVA: 0x0002B727 File Offset: 0x00029927
	public override void Enter()
	{
		this.roleBase.exAttackDistance += base.Distance;
	}

	// Token: 0x060007EE RID: 2030 RVA: 0x0002B741 File Offset: 0x00029941
	public override void Exit()
	{
		this.roleBase.exAttackDistance -= base.Distance;
	}
}

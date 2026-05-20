using System;

// Token: 0x02000192 RID: 402
public class B真超长臂猿 : PasssiveSkill
{
	// Token: 0x0600079C RID: 1948 RVA: 0x0002B727 File Offset: 0x00029927
	public override void Enter()
	{
		this.roleBase.exAttackDistance += base.Distance;
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x0002B741 File Offset: 0x00029941
	public override void Exit()
	{
		this.roleBase.exAttackDistance -= base.Distance;
	}
}

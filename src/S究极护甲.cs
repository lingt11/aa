using System;

// Token: 0x020001D2 RID: 466
public class S究极护甲 : PasssiveSkill
{
	// Token: 0x06000885 RID: 2181 RVA: 0x000307E2 File Offset: 0x0002E9E2
	public override void Enter()
	{
		this.roleBase.AddArmor(999);
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x000307F4 File Offset: 0x0002E9F4
	public override void Exit()
	{
		this.roleBase.AddArmor(-999);
	}
}

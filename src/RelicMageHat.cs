using System;

// Token: 0x02000225 RID: 549
public class RelicMageHat : RelicBase
{
	// Token: 0x060009EF RID: 2543 RVA: 0x00034BFA File Offset: 0x00032DFA
	public override void Enter()
	{
		this.playerBase.isMageHat = true;
	}

	// Token: 0x060009F0 RID: 2544 RVA: 0x00034C08 File Offset: 0x00032E08
	public override void Exit()
	{
		this.playerBase.isMageHat = false;
	}
}

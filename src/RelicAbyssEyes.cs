using System;

// Token: 0x020001ED RID: 493
public class RelicAbyssEyes : RelicBase
{
	// Token: 0x060008E4 RID: 2276 RVA: 0x00031CA2 File Offset: 0x0002FEA2
	public override void Enter()
	{
		this.playerBase.CmdEliteProbabilityAdd(base.GetValue(0, 1f));
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x00031CBB File Offset: 0x0002FEBB
	public override void Exit()
	{
		this.playerBase.CmdEliteProbabilityAdd(-base.GetValue(0, 1f));
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x00031CD5 File Offset: 0x0002FED5
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.CmdEliteProbabilityAdd(base.GetLevelValueDelta(0, 0.75f, oldLevel, newLevel));
	}
}

using System;

// Token: 0x0200024C RID: 588
public class RelicTeamMp : RelicBase
{
	// Token: 0x06000A90 RID: 2704 RVA: 0x00035D27 File Offset: 0x00033F27
	public override void Enter()
	{
		this.playerBase.AddMpAddSec(base.GetIntValue(0, 5));
	}

	// Token: 0x06000A91 RID: 2705 RVA: 0x00035D3C File Offset: 0x00033F3C
	public override void Exit()
	{
		this.playerBase.AddMpAddSec(-base.GetIntValue(0, 5));
	}

	// Token: 0x06000A92 RID: 2706 RVA: 0x00035D52 File Offset: 0x00033F52
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.AddMpAddSec(base.GetLevelIntValueDelta(0, 5, oldLevel, newLevel));
	}
}

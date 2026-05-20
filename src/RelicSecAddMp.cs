using System;

// Token: 0x02000238 RID: 568
public class RelicSecAddMp : RelicBase
{
	// Token: 0x06000A40 RID: 2624 RVA: 0x00035D27 File Offset: 0x00033F27
	public override void Enter()
	{
		this.playerBase.AddMpAddSec(base.GetIntValue(0, 5));
	}

	// Token: 0x06000A41 RID: 2625 RVA: 0x00035D3C File Offset: 0x00033F3C
	public override void Exit()
	{
		this.playerBase.AddMpAddSec(-base.GetIntValue(0, 5));
	}

	// Token: 0x06000A42 RID: 2626 RVA: 0x00035D52 File Offset: 0x00033F52
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.AddMpAddSec(base.GetLevelIntValueDelta(0, 5, oldLevel, newLevel));
	}
}

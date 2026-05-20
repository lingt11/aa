using System;

// Token: 0x02000224 RID: 548
public class RelicLuckyMan : RelicBase
{
	// Token: 0x060009EB RID: 2539 RVA: 0x00034BAC File Offset: 0x00032DAC
	public override void Enter()
	{
		this.playerBase.CmdUpdateLucky(base.GetIntValue(0, 150));
	}

	// Token: 0x060009EC RID: 2540 RVA: 0x00034BC5 File Offset: 0x00032DC5
	public override void Exit()
	{
		this.playerBase.CmdUpdateLucky(-base.GetIntValue(0, 150));
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x00034BDF File Offset: 0x00032DDF
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.CmdUpdateLucky(base.GetLevelIntValueDelta(0, 150, oldLevel, newLevel));
	}
}

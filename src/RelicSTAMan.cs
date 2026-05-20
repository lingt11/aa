using System;

// Token: 0x0200023F RID: 575
public class RelicSTAMan : RelicBase
{
	// Token: 0x06000A5C RID: 2652 RVA: 0x00036104 File Offset: 0x00034304
	public override void Enter()
	{
		this.playerBase.StaAllAdd += base.GetValue(0, 0.3f);
	}

	// Token: 0x06000A5D RID: 2653 RVA: 0x00036124 File Offset: 0x00034324
	public override void Exit()
	{
		this.playerBase.StaAllAdd -= base.GetValue(0, 0.3f);
	}

	// Token: 0x06000A5E RID: 2654 RVA: 0x00036144 File Offset: 0x00034344
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.StaAllAdd += base.GetLevelValueDelta(0, 0.3f, oldLevel, newLevel);
	}
}

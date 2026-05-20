using System;

// Token: 0x02000242 RID: 578
public class RelicSTRMan : RelicBase
{
	// Token: 0x06000A69 RID: 2665 RVA: 0x00036351 File Offset: 0x00034551
	public override void Enter()
	{
		this.playerBase.StrAllAdd += base.GetValue(0, 0.3f);
	}

	// Token: 0x06000A6A RID: 2666 RVA: 0x00036371 File Offset: 0x00034571
	public override void Exit()
	{
		this.playerBase.StrAllAdd -= base.GetValue(0, 0.3f);
	}

	// Token: 0x06000A6B RID: 2667 RVA: 0x00036391 File Offset: 0x00034591
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.StrAllAdd += base.GetLevelValueDelta(0, 0.3f, oldLevel, newLevel);
	}
}

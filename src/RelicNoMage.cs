using System;

// Token: 0x0200022E RID: 558
public class RelicNoMage : RelicBase
{
	// Token: 0x06000A15 RID: 2581 RVA: 0x00035384 File Offset: 0x00033584
	public override void Enter()
	{
		this.playerBase.skillNoneAdd += base.GetValue(0, 0.35f);
	}

	// Token: 0x06000A16 RID: 2582 RVA: 0x000353A4 File Offset: 0x000335A4
	public override void Exit()
	{
		this.playerBase.skillNoneAdd -= base.GetValue(0, 0.35f);
	}

	// Token: 0x06000A17 RID: 2583 RVA: 0x000353C4 File Offset: 0x000335C4
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.skillNoneAdd += base.GetLevelValueDelta(0, 0.35f, oldLevel, newLevel);
	}
}

using System;

// Token: 0x02000214 RID: 532
public class RelicFastCast : RelicBase
{
	// Token: 0x060009AB RID: 2475 RVA: 0x00033F7A File Offset: 0x0003217A
	public override void Enter()
	{
		this.playerBase.castSpeed += base.GetValue(0, 0.5f);
	}

	// Token: 0x060009AC RID: 2476 RVA: 0x00033F9A File Offset: 0x0003219A
	public override void Exit()
	{
		this.playerBase.castSpeed -= base.GetValue(0, 0.5f);
	}

	// Token: 0x060009AD RID: 2477 RVA: 0x00033FBA File Offset: 0x000321BA
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.castSpeed += base.GetLevelValueDelta(0, 0.5f, oldLevel, newLevel);
	}
}

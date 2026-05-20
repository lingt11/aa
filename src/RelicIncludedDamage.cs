using System;

// Token: 0x02000221 RID: 545
public class RelicIncludedDamage : RelicBase
{
	// Token: 0x060009E0 RID: 2528 RVA: 0x00034A9A File Offset: 0x00032C9A
	public override void Enter()
	{
		this.playerBase.extraDamage += base.GetIntValue(0, 175);
	}

	// Token: 0x060009E1 RID: 2529 RVA: 0x00034ABA File Offset: 0x00032CBA
	public override void Exit()
	{
		this.playerBase.extraDamage -= base.GetIntValue(0, 175);
	}

	// Token: 0x060009E2 RID: 2530 RVA: 0x00034ADA File Offset: 0x00032CDA
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.extraDamage += base.GetLevelIntValueDelta(0, 175, oldLevel, newLevel);
	}
}

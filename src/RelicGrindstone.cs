using System;

// Token: 0x0200021C RID: 540
public class RelicGrindstone : RelicBase
{
	// Token: 0x060009CC RID: 2508 RVA: 0x00034685 File Offset: 0x00032885
	public override void Enter()
	{
		this.playerBase.AddAttackPower(base.GetIntValue(0, 150));
	}

	// Token: 0x060009CD RID: 2509 RVA: 0x0003469E File Offset: 0x0003289E
	public override void Exit()
	{
		this.playerBase.AddAttackPower(-base.GetIntValue(0, 150));
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x000346B8 File Offset: 0x000328B8
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.AddAttackPower(base.GetLevelIntValueDelta(0, 150, oldLevel, newLevel));
	}
}

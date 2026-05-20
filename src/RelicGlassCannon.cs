using System;

// Token: 0x0200021A RID: 538
public class RelicGlassCannon : RelicBase
{
	// Token: 0x060009C4 RID: 2500 RVA: 0x0003451C File Offset: 0x0003271C
	public override void Enter()
	{
		this.playerBase.addDamagePercent += base.GetValue(0, 0.35f);
		this.playerBase.AddArmor(-base.GetIntValue(1, 45));
	}

	// Token: 0x060009C5 RID: 2501 RVA: 0x00034551 File Offset: 0x00032751
	public override void Exit()
	{
		this.playerBase.addDamagePercent -= base.GetValue(0, 0.35f);
		this.playerBase.AddArmor(base.GetIntValue(1, 45));
	}

	// Token: 0x060009C6 RID: 2502 RVA: 0x00034585 File Offset: 0x00032785
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.addDamagePercent += base.GetLevelValueDelta(0, 0.35f, oldLevel, newLevel);
		this.playerBase.AddArmor(-base.GetLevelIntValueDelta(1, 45, oldLevel, newLevel));
	}
}

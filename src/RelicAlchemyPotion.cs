using System;

// Token: 0x020001FB RID: 507
public class RelicAlchemyPotion : RelicBase
{
	// Token: 0x0600091C RID: 2332 RVA: 0x00032369 File Offset: 0x00030569
	public override void Enter()
	{
		this.playerBase.CmdUpdateMaxHpAddPercent(base.GetValue(0, 0.3f));
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x00032382 File Offset: 0x00030582
	public override void Exit()
	{
		this.playerBase.CmdUpdateMaxHpAddPercent(-base.GetValue(0, 0.3f));
	}

	// Token: 0x0600091E RID: 2334 RVA: 0x0003239C File Offset: 0x0003059C
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.CmdUpdateMaxHpAddPercent(base.GetLevelValueDelta(0, 0.3f, oldLevel, newLevel));
	}
}

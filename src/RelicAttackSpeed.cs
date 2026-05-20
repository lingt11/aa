using System;

// Token: 0x020001FF RID: 511
public class RelicAttackSpeed : RelicBase
{
	// Token: 0x0600092C RID: 2348 RVA: 0x000324A0 File Offset: 0x000306A0
	public override void Enter()
	{
		GameHelperClient.localPlayer.AddAttackSpeed(base.GetValue(0, 0.6f));
	}

	// Token: 0x0600092D RID: 2349 RVA: 0x000324B8 File Offset: 0x000306B8
	public override void Exit()
	{
		GameHelperClient.localPlayer.AddAttackSpeed(-base.GetValue(0, 0.6f));
	}

	// Token: 0x0600092E RID: 2350 RVA: 0x000324D1 File Offset: 0x000306D1
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		GameHelperClient.localPlayer.AddAttackSpeed(base.GetLevelValueDelta(0, 0.6f, oldLevel, newLevel));
	}
}

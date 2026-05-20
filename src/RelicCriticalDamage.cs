using System;

// Token: 0x0200020D RID: 525
public class RelicCriticalDamage : RelicBase
{
	// Token: 0x0600098D RID: 2445 RVA: 0x00033948 File Offset: 0x00031B48
	public override void Enter()
	{
		GameHelperClient.localPlayer.AddCriticalDamage(base.GetValue(0, 0.4f));
	}

	// Token: 0x0600098E RID: 2446 RVA: 0x00033960 File Offset: 0x00031B60
	public override void Exit()
	{
		GameHelperClient.localPlayer.AddCriticalDamage(-base.GetValue(0, 0.4f));
	}

	// Token: 0x0600098F RID: 2447 RVA: 0x00033979 File Offset: 0x00031B79
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		GameHelperClient.localPlayer.AddCriticalDamage(base.GetLevelValueDelta(0, 0.4f, oldLevel, newLevel));
	}
}

using System;

// Token: 0x0200020C RID: 524
public class RelicCritical : RelicBase
{
	// Token: 0x06000989 RID: 2441 RVA: 0x000338FD File Offset: 0x00031AFD
	public override void Enter()
	{
		GameHelperClient.localPlayer.AddCritical(base.GetValue(0, 0.25f));
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x00033915 File Offset: 0x00031B15
	public override void Exit()
	{
		GameHelperClient.localPlayer.AddCritical(-base.GetValue(0, 0.25f));
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x0003392E File Offset: 0x00031B2E
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		GameHelperClient.localPlayer.AddCritical(base.GetLevelValueDelta(0, 0.25f, oldLevel, newLevel));
	}
}

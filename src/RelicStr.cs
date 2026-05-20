using System;

// Token: 0x02000241 RID: 577
public class RelicStr : RelicBase
{
	// Token: 0x06000A65 RID: 2661 RVA: 0x0003630F File Offset: 0x0003450F
	public override void Enter()
	{
		GameHelperClient.localPlayer.AddSTR(base.GetIntValue(0, 50));
	}

	// Token: 0x06000A66 RID: 2662 RVA: 0x00036324 File Offset: 0x00034524
	public override void Exit()
	{
		GameHelperClient.localPlayer.AddSTR(-base.GetIntValue(0, 50));
	}

	// Token: 0x06000A67 RID: 2663 RVA: 0x0003633A File Offset: 0x0003453A
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		GameHelperClient.localPlayer.AddSTR(base.GetLevelIntValueDelta(0, 50, oldLevel, newLevel));
	}
}

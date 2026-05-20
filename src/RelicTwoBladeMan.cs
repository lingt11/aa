using System;

// Token: 0x02000252 RID: 594
public class RelicTwoBladeMan : RelicBase
{
	// Token: 0x06000AAB RID: 2731 RVA: 0x00036CF6 File Offset: 0x00034EF6
	public override void Enter()
	{
		GameHelperClient.localPlayer.attackEffectTime += base.GetIntValue(0, 1);
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x00036D11 File Offset: 0x00034F11
	public override void Exit()
	{
		GameHelperClient.localPlayer.attackEffectTime -= base.GetIntValue(0, 1);
	}

	// Token: 0x06000AAD RID: 2733 RVA: 0x00036D2C File Offset: 0x00034F2C
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		GameHelperClient.localPlayer.attackEffectTime += base.GetLevelIntValueDelta(0, 1, oldLevel, newLevel);
	}
}

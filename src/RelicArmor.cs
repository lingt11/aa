using System;

// Token: 0x020001FD RID: 509
public class RelicArmor : RelicBase
{
	// Token: 0x06000924 RID: 2340 RVA: 0x00032419 File Offset: 0x00030619
	public override void Enter()
	{
		GameHelperClient.localPlayer.AddArmor(base.GetIntValue(0, 35));
	}

	// Token: 0x06000925 RID: 2341 RVA: 0x0003242E File Offset: 0x0003062E
	public override void Exit()
	{
		GameHelperClient.localPlayer.AddArmor(-base.GetIntValue(0, 35));
	}

	// Token: 0x06000926 RID: 2342 RVA: 0x00032444 File Offset: 0x00030644
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		GameHelperClient.localPlayer.AddArmor(base.GetLevelIntValueDelta(0, 35, oldLevel, newLevel));
	}
}

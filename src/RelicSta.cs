using System;

// Token: 0x0200023E RID: 574
public class RelicSta : RelicBase
{
	// Token: 0x06000A58 RID: 2648 RVA: 0x000360C2 File Offset: 0x000342C2
	public override void Enter()
	{
		GameHelperClient.localPlayer.AddSTA(base.GetIntValue(0, 50));
	}

	// Token: 0x06000A59 RID: 2649 RVA: 0x000360D7 File Offset: 0x000342D7
	public override void Exit()
	{
		GameHelperClient.localPlayer.AddSTA(-base.GetIntValue(0, 50));
	}

	// Token: 0x06000A5A RID: 2650 RVA: 0x000360ED File Offset: 0x000342ED
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		GameHelperClient.localPlayer.AddSTA(base.GetLevelIntValueDelta(0, 50, oldLevel, newLevel));
	}
}

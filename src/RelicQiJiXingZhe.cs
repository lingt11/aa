using System;

// Token: 0x02000231 RID: 561
public class RelicQiJiXingZhe : RelicBase
{
	// Token: 0x06000A23 RID: 2595 RVA: 0x000355F1 File Offset: 0x000337F1
	public override void Enter()
	{
		GameHelperClient.IsQiJiXingZhe += base.GetIntValue(0, 1);
	}

	// Token: 0x06000A24 RID: 2596 RVA: 0x00035606 File Offset: 0x00033806
	public override void Exit()
	{
		GameHelperClient.IsQiJiXingZhe -= base.GetIntValue(0, 1);
	}

	// Token: 0x06000A25 RID: 2597 RVA: 0x0003561B File Offset: 0x0003381B
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		GameHelperClient.IsQiJiXingZhe += base.GetLevelIntValueDelta(0, 1, oldLevel, newLevel);
	}
}

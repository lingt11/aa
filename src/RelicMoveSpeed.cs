using System;

// Token: 0x0200022A RID: 554
public class RelicMoveSpeed : RelicBase
{
	// Token: 0x06000A04 RID: 2564 RVA: 0x0003501F File Offset: 0x0003321F
	public override void Enter()
	{
		GameHelperClient.localPlayer.AddMoveSpeed(base.GetValue(0, 1.5f));
	}

	// Token: 0x06000A05 RID: 2565 RVA: 0x00035037 File Offset: 0x00033237
	public override void Exit()
	{
		GameHelperClient.localPlayer.AddMoveSpeed(-base.GetValue(0, 1.5f));
	}

	// Token: 0x06000A06 RID: 2566 RVA: 0x00035050 File Offset: 0x00033250
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		GameHelperClient.localPlayer.AddMoveSpeed(base.GetLevelValueDelta(0, 1.5f, oldLevel, newLevel));
	}
}

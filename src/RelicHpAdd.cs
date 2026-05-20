using System;

// Token: 0x0200021F RID: 543
public class RelicHpAdd : RelicBase
{
	// Token: 0x060009D8 RID: 2520 RVA: 0x000349F6 File Offset: 0x00032BF6
	public override void Enter()
	{
		GameHelperClient.localPlayer.AddHpAddSec(base.GetIntValue(0, 100));
	}

	// Token: 0x060009D9 RID: 2521 RVA: 0x00034A0B File Offset: 0x00032C0B
	public override void Exit()
	{
		GameHelperClient.localPlayer.AddHpAddSec(-base.GetIntValue(0, 100));
	}

	// Token: 0x060009DA RID: 2522 RVA: 0x00034A21 File Offset: 0x00032C21
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		GameHelperClient.localPlayer.AddHpAddSec(base.GetLevelIntValueDelta(0, 100, oldLevel, newLevel));
	}
}

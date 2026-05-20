using System;

// Token: 0x020001FE RID: 510
public class RelicAttackAddHp : RelicBase
{
	// Token: 0x06000928 RID: 2344 RVA: 0x0003245B File Offset: 0x0003065B
	public override void Enter()
	{
		GameHelperClient.localPlayer.AddXiXue((float)base.GetIntValue(0, 50));
	}

	// Token: 0x06000929 RID: 2345 RVA: 0x00032471 File Offset: 0x00030671
	public override void Exit()
	{
		GameHelperClient.localPlayer.AddXiXue((float)(-(float)base.GetIntValue(0, 50)));
	}

	// Token: 0x0600092A RID: 2346 RVA: 0x00032488 File Offset: 0x00030688
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		GameHelperClient.localPlayer.AddXiXue((float)base.GetLevelIntValueDelta(0, 50, oldLevel, newLevel));
	}
}

using System;

// Token: 0x020001F9 RID: 505
public class RelicAgi : RelicBase
{
	// Token: 0x06000914 RID: 2324 RVA: 0x000322C5 File Offset: 0x000304C5
	public override void Enter()
	{
		GameHelperClient.localPlayer.AddAGI(base.GetIntValue(0, 50));
	}

	// Token: 0x06000915 RID: 2325 RVA: 0x000322DA File Offset: 0x000304DA
	public override void Exit()
	{
		GameHelperClient.localPlayer.AddAGI(-base.GetIntValue(0, 50));
	}

	// Token: 0x06000916 RID: 2326 RVA: 0x000322F0 File Offset: 0x000304F0
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		GameHelperClient.localPlayer.AddAGI(base.GetLevelIntValueDelta(0, 50, oldLevel, newLevel));
	}
}

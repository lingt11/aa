using System;

// Token: 0x02000243 RID: 579
public class RelicSuffering : RelicBase
{
	// Token: 0x06000A6D RID: 2669 RVA: 0x000363B3 File Offset: 0x000345B3
	public override void Enter()
	{
		this.playerBase.CmdUpdateAddHatred(base.GetValue(0, 7f));
		this.playerBase.AddArmor(base.GetIntValue(1, 50));
	}

	// Token: 0x06000A6E RID: 2670 RVA: 0x000363E0 File Offset: 0x000345E0
	public override void Exit()
	{
		this.playerBase.CmdUpdateAddHatred(-base.GetValue(0, 7f));
		this.playerBase.AddArmor(-base.GetIntValue(1, 50));
	}

	// Token: 0x06000A6F RID: 2671 RVA: 0x0003640F File Offset: 0x0003460F
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		GameHelperClient.localPlayer.AddArmor(base.GetLevelIntValueDelta(1, 50, oldLevel, newLevel));
	}
}

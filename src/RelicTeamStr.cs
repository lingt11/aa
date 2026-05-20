using System;

// Token: 0x0200024D RID: 589
public class RelicTeamStr : RelicBase
{
	// Token: 0x06000A94 RID: 2708 RVA: 0x00036748 File Offset: 0x00034948
	public override void Enter()
	{
		this.playerBase.AddSTA(base.GetIntValue(0, 25));
		this.playerBase.AddSTR(base.GetIntValue(0, 25));
		this.playerBase.AddAGI(base.GetIntValue(0, 25));
	}

	// Token: 0x06000A95 RID: 2709 RVA: 0x00036788 File Offset: 0x00034988
	public override void Exit()
	{
		this.playerBase.AddSTA(-base.GetIntValue(0, 25));
		this.playerBase.AddSTR(-base.GetIntValue(0, 25));
		this.playerBase.AddAGI(-base.GetIntValue(0, 25));
	}

	// Token: 0x06000A96 RID: 2710 RVA: 0x000367D4 File Offset: 0x000349D4
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		int levelIntValueDelta = base.GetLevelIntValueDelta(0, 25, oldLevel, newLevel);
		this.playerBase.AddSTA(levelIntValueDelta);
		this.playerBase.AddSTR(levelIntValueDelta);
		this.playerBase.AddAGI(levelIntValueDelta);
	}
}

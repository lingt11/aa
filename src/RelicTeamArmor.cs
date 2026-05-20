using System;

// Token: 0x02000248 RID: 584
public class RelicTeamArmor : RelicBase
{
	// Token: 0x06000A81 RID: 2689 RVA: 0x00036605 File Offset: 0x00034805
	public override void Enter()
	{
		this.playerBase.AddArmor(base.GetIntValue(0, 30));
	}

	// Token: 0x06000A82 RID: 2690 RVA: 0x0003661B File Offset: 0x0003481B
	public override void Exit()
	{
		this.playerBase.AddArmor(-base.GetIntValue(0, 30));
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x00036632 File Offset: 0x00034832
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.AddArmor(base.GetLevelIntValueDelta(0, 30, oldLevel, newLevel));
	}
}

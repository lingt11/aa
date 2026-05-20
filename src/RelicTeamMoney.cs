using System;

// Token: 0x0200024A RID: 586
public class RelicTeamMoney : RelicBase
{
	// Token: 0x06000A89 RID: 2697 RVA: 0x000366AC File Offset: 0x000348AC
	public override void Enter()
	{
		this.playerBase.AddGold(this.playerBase.GetHeadUIPos(), base.GetIntValue(0, 1500), true);
	}

	// Token: 0x06000A8A RID: 2698 RVA: 0x000366D2 File Offset: 0x000348D2
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.AddGold(this.playerBase.GetHeadUIPos(), base.GetLevelIntValueDelta(0, 1500, oldLevel, newLevel), true);
	}
}

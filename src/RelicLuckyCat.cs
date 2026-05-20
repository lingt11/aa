using System;

// Token: 0x02000223 RID: 547
public class RelicLuckyCat : RelicBase
{
	// Token: 0x060009E8 RID: 2536 RVA: 0x00034B5E File Offset: 0x00032D5E
	public override void Enter()
	{
		this.playerBase.AddGold(this.playerBase.GetHeadUIPos(), base.GetIntValue(0, 2500), true);
	}

	// Token: 0x060009E9 RID: 2537 RVA: 0x00034B84 File Offset: 0x00032D84
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.AddGold(this.playerBase.GetHeadUIPos(), base.GetLevelIntValueDelta(0, 2500, oldLevel, newLevel), true);
	}
}

using System;

// Token: 0x020001F1 RID: 497
public class RelicAddGem : RelicBase
{
	// Token: 0x060008F4 RID: 2292 RVA: 0x00031E1E File Offset: 0x0003001E
	public override void Enter()
	{
		this.playerBase.AddGem(this.playerBase.GetHeadUIPos(), base.GetIntValue(0, 10), false);
	}

	// Token: 0x060008F5 RID: 2293 RVA: 0x00031E40 File Offset: 0x00030040
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.AddGem(this.playerBase.GetHeadUIPos(), base.GetLevelIntValueDelta(0, 10, oldLevel, newLevel), false);
	}
}

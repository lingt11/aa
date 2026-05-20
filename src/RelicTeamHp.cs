using System;

// Token: 0x02000249 RID: 585
public class RelicTeamHp : RelicBase
{
	// Token: 0x06000A85 RID: 2693 RVA: 0x0003664A File Offset: 0x0003484A
	public override void Enter()
	{
		this.playerBase.hpAddSecRate += base.GetValue(0, 0.02f);
	}

	// Token: 0x06000A86 RID: 2694 RVA: 0x0003666A File Offset: 0x0003486A
	public override void Exit()
	{
		this.playerBase.hpAddSecRate -= base.GetValue(0, 0.02f);
	}

	// Token: 0x06000A87 RID: 2695 RVA: 0x0003668A File Offset: 0x0003488A
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.hpAddSecRate += base.GetLevelValueDelta(0, 0.02f, oldLevel, newLevel);
	}
}

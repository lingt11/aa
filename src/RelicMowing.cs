using System;

// Token: 0x0200022B RID: 555
public class RelicMowing : RelicBase
{
	// Token: 0x06000A08 RID: 2568 RVA: 0x0003506A File Offset: 0x0003326A
	public override void Enter()
	{
		this.playerBase.addNormalEnemy += base.GetValue(0, 0.35f);
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x0003508A File Offset: 0x0003328A
	public override void Exit()
	{
		this.playerBase.addNormalEnemy -= base.GetValue(0, 0.35f);
	}

	// Token: 0x06000A0A RID: 2570 RVA: 0x000350AA File Offset: 0x000332AA
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.addNormalEnemy += base.GetLevelValueDelta(0, 0.35f, oldLevel, newLevel);
	}
}

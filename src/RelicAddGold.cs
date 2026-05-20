using System;

// Token: 0x020001F2 RID: 498
public class RelicAddGold : RelicBase
{
	// Token: 0x060008F7 RID: 2295 RVA: 0x00031E64 File Offset: 0x00030064
	public override void Enter()
	{
		this.playerBase.addGoldPercent += base.GetValue(0, 0.15f);
	}

	// Token: 0x060008F8 RID: 2296 RVA: 0x00031E84 File Offset: 0x00030084
	public override void Exit()
	{
		this.playerBase.addGoldPercent -= base.GetValue(0, 0.15f);
	}

	// Token: 0x060008F9 RID: 2297 RVA: 0x00031EA4 File Offset: 0x000300A4
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.addGoldPercent += base.GetLevelValueDelta(0, 0.15f, oldLevel, newLevel);
	}
}

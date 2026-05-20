using System;

// Token: 0x020001F3 RID: 499
public class RelicAddHenshin : RelicBase
{
	// Token: 0x060008FB RID: 2299 RVA: 0x00031EC6 File Offset: 0x000300C6
	public override void Enter()
	{
		this.playerBase.addHenshin += base.GetValue(0, 0.5f);
	}

	// Token: 0x060008FC RID: 2300 RVA: 0x00031EE6 File Offset: 0x000300E6
	public override void Exit()
	{
		this.playerBase.addHenshin -= base.GetValue(0, 0.5f);
	}

	// Token: 0x060008FD RID: 2301 RVA: 0x00031F06 File Offset: 0x00030106
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.addHenshin += base.GetLevelValueDelta(0, 0.5f, oldLevel, newLevel);
	}
}

using System;

// Token: 0x02000216 RID: 534
public class RelicFireMan : RelicBase
{
	// Token: 0x060009B4 RID: 2484 RVA: 0x0003417C File Offset: 0x0003237C
	public override void Enter()
	{
		this.playerBase.skillFireAdd += base.GetValue(0, 0.35f);
	}

	// Token: 0x060009B5 RID: 2485 RVA: 0x0003419C File Offset: 0x0003239C
	public override void Exit()
	{
		this.playerBase.skillFireAdd -= base.GetValue(0, 0.35f);
	}

	// Token: 0x060009B6 RID: 2486 RVA: 0x000341BC File Offset: 0x000323BC
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.skillFireAdd += base.GetLevelValueDelta(0, 0.35f, oldLevel, newLevel);
	}
}

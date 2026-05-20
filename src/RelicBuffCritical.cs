using System;

// Token: 0x02000209 RID: 521
public class RelicBuffCritical : RelicBase
{
	// Token: 0x0600097C RID: 2428 RVA: 0x00033682 File Offset: 0x00031882
	public override void Enter()
	{
		this.playerBase.canBuffCritical = true;
		this.playerBase.buffCriticalLevel += base.GetValue(0, 0.5f);
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x000336AE File Offset: 0x000318AE
	public override void Exit()
	{
		this.playerBase.buffCriticalLevel -= base.GetValue(0, 0.5f);
		if (this.playerBase.buffCriticalLevel <= 0f)
		{
			this.playerBase.canBuffCritical = false;
		}
	}

	// Token: 0x0600097E RID: 2430 RVA: 0x000336EC File Offset: 0x000318EC
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.buffCriticalLevel += base.GetLevelValueDelta(0, 0.5f, oldLevel, newLevel);
	}
}

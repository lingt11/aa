using System;

// Token: 0x020001FA RID: 506
public class RelicAGIMan : RelicBase
{
	// Token: 0x06000918 RID: 2328 RVA: 0x00032307 File Offset: 0x00030507
	public override void Enter()
	{
		this.playerBase.AgiAllAdd += base.GetValue(0, 0.3f);
	}

	// Token: 0x06000919 RID: 2329 RVA: 0x00032327 File Offset: 0x00030527
	public override void Exit()
	{
		this.playerBase.AgiAllAdd -= base.GetValue(0, 0.3f);
	}

	// Token: 0x0600091A RID: 2330 RVA: 0x00032347 File Offset: 0x00030547
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.AgiAllAdd += base.GetLevelValueDelta(0, 0.3f, oldLevel, newLevel);
	}
}

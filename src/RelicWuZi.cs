using System;

// Token: 0x02000255 RID: 597
public class RelicWuZi : RelicBase
{
	// Token: 0x06000AB7 RID: 2743 RVA: 0x00036F0C File Offset: 0x0003510C
	public override void Enter()
	{
		this.playerBase.StaAllAdd += base.GetValue(0, 0.3f);
		this.playerBase.StrAllAdd += base.GetValue(0, 0.3f);
		this.playerBase.AgiAllAdd += base.GetValue(0, 0.3f);
	}

	// Token: 0x06000AB8 RID: 2744 RVA: 0x00036F74 File Offset: 0x00035174
	public override void Exit()
	{
		this.playerBase.StaAllAdd -= base.GetValue(0, 0.3f);
		this.playerBase.StrAllAdd -= base.GetValue(0, 0.3f);
		this.playerBase.AgiAllAdd -= base.GetValue(0, 0.3f);
	}

	// Token: 0x06000AB9 RID: 2745 RVA: 0x00036FDC File Offset: 0x000351DC
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		float levelValueDelta = base.GetLevelValueDelta(0, 0.3f, oldLevel, newLevel);
		this.playerBase.StaAllAdd += levelValueDelta;
		this.playerBase.StrAllAdd += levelValueDelta;
		this.playerBase.AgiAllAdd += levelValueDelta;
	}
}

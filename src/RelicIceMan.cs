using System;

// Token: 0x02000220 RID: 544
public class RelicIceMan : RelicBase
{
	// Token: 0x060009DC RID: 2524 RVA: 0x00034A38 File Offset: 0x00032C38
	public override void Enter()
	{
		this.playerBase.skillIceAdd += base.GetValue(0, 0.35f);
	}

	// Token: 0x060009DD RID: 2525 RVA: 0x00034A58 File Offset: 0x00032C58
	public override void Exit()
	{
		this.playerBase.skillIceAdd -= base.GetValue(0, 0.35f);
	}

	// Token: 0x060009DE RID: 2526 RVA: 0x00034A78 File Offset: 0x00032C78
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.skillIceAdd += base.GetLevelValueDelta(0, 0.35f, oldLevel, newLevel);
	}
}

using System;

// Token: 0x02000244 RID: 580
public class RelicSummonTears : RelicBase
{
	// Token: 0x06000A71 RID: 2673 RVA: 0x00036426 File Offset: 0x00034626
	public override void Enter()
	{
		this.playerBase.addCallMonsterTime += base.GetValue(0, 0.35f);
	}

	// Token: 0x06000A72 RID: 2674 RVA: 0x00036446 File Offset: 0x00034646
	public override void Exit()
	{
		this.playerBase.addCallMonsterTime -= base.GetValue(0, 0.35f);
	}

	// Token: 0x06000A73 RID: 2675 RVA: 0x00036466 File Offset: 0x00034666
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.addCallMonsterTime += base.GetLevelValueDelta(0, 0.35f, oldLevel, newLevel);
	}
}

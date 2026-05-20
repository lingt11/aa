using System;

// Token: 0x020001F7 RID: 503
public class RelicAddSkillCd : RelicBase
{
	// Token: 0x0600090C RID: 2316 RVA: 0x00032147 File Offset: 0x00030347
	public override void Enter()
	{
		this.playerBase.skillCdReduce += base.GetIntValue(0, 20);
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x00032164 File Offset: 0x00030364
	public override void Exit()
	{
		this.playerBase.skillCdReduce -= base.GetIntValue(0, 20);
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x00032181 File Offset: 0x00030381
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.skillCdReduce += base.GetLevelIntValueDelta(0, 20, oldLevel, newLevel);
	}
}

using System;

// Token: 0x0200023C RID: 572
public class RelicSkillRange : RelicBase
{
	// Token: 0x06000A50 RID: 2640 RVA: 0x00035F32 File Offset: 0x00034132
	public override void Enter()
	{
		this.playerBase.CmdUpdateSkillRange(base.GetValue(0, 0.25f));
	}

	// Token: 0x06000A51 RID: 2641 RVA: 0x00035F4B File Offset: 0x0003414B
	public override void Exit()
	{
		this.playerBase.CmdUpdateSkillRange(-base.GetValue(0, 0.25f));
	}

	// Token: 0x06000A52 RID: 2642 RVA: 0x00035F65 File Offset: 0x00034165
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.CmdUpdateSkillRange(base.GetLevelValueDelta(0, 0.25f, oldLevel, newLevel));
	}
}

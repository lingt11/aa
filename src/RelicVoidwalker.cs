using System;

// Token: 0x02000253 RID: 595
public class RelicVoidwalker : RelicBase
{
	// Token: 0x06000AAF RID: 2735 RVA: 0x00036D4C File Offset: 0x00034F4C
	public override void Enter()
	{
		this.playerBase.doge += base.GetIntValue(0, 100);
		this.playerBase.CmdDoge(this.playerBase.doge);
		this.playerBase.UpdateSkillHitDamage(-base.GetIntValue(1, 200));
	}

	// Token: 0x06000AB0 RID: 2736 RVA: 0x00036DA4 File Offset: 0x00034FA4
	public override void Exit()
	{
		this.playerBase.doge -= base.GetIntValue(0, 100);
		this.playerBase.CmdDoge(this.playerBase.doge);
		this.playerBase.UpdateSkillHitDamage(base.GetIntValue(1, 200));
	}

	// Token: 0x06000AB1 RID: 2737 RVA: 0x00036DFC File Offset: 0x00034FFC
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.doge += base.GetLevelIntValueDelta(0, 100, oldLevel, newLevel);
		this.playerBase.CmdDoge(this.playerBase.doge);
		this.playerBase.UpdateSkillHitDamage(-base.GetLevelIntValueDelta(1, 200, oldLevel, newLevel));
	}
}

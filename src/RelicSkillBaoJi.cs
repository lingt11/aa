using System;

// Token: 0x02000239 RID: 569
public class RelicSkillBaoJi : RelicBase
{
	// Token: 0x06000A44 RID: 2628 RVA: 0x00035D69 File Offset: 0x00033F69
	public override void Enter()
	{
		this.playerBase.canSkillCritical = true;
		this.playerBase.skillCriticalLevel += base.GetValue(0, 0.5f);
	}

	// Token: 0x06000A45 RID: 2629 RVA: 0x00035D95 File Offset: 0x00033F95
	public override void Exit()
	{
		this.playerBase.skillCriticalLevel -= base.GetValue(0, 0.5f);
		if (this.playerBase.skillCriticalLevel <= 0f)
		{
			this.playerBase.canSkillCritical = false;
		}
	}

	// Token: 0x06000A46 RID: 2630 RVA: 0x00035DD3 File Offset: 0x00033FD3
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.skillCriticalLevel += base.GetLevelValueDelta(0, 0.5f, oldLevel, newLevel);
	}
}

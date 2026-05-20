using System;

// Token: 0x0200023B RID: 571
public class RelicSkillDamage : RelicBase
{
	// Token: 0x06000A4C RID: 2636 RVA: 0x00035ED0 File Offset: 0x000340D0
	public override void Enter()
	{
		this.playerBase.skillExDamage += base.GetValue(0, 0.3f);
	}

	// Token: 0x06000A4D RID: 2637 RVA: 0x00035EF0 File Offset: 0x000340F0
	public override void Exit()
	{
		this.playerBase.skillExDamage -= base.GetValue(0, 0.3f);
	}

	// Token: 0x06000A4E RID: 2638 RVA: 0x00035F10 File Offset: 0x00034110
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.skillExDamage += base.GetLevelValueDelta(0, 0.3f, oldLevel, newLevel);
	}
}

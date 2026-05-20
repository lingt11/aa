using System;

// Token: 0x02000247 RID: 583
public class RelicTabooBook : RelicBase
{
	// Token: 0x06000A7D RID: 2685 RVA: 0x00036528 File Offset: 0x00034728
	public override void Enter()
	{
		this.playerBase.skillExDamage += base.GetValue(0, 0.45f);
		this.playerBase.normalAttackAddDamage -= base.GetValue(0, 0.45f);
	}

	// Token: 0x06000A7E RID: 2686 RVA: 0x000364EA File Offset: 0x000346EA
	public override void Exit()
	{
		this.playerBase.skillExDamage -= base.GetValue(0, 0.45f);
		this.playerBase.normalAttackAddDamage += base.GetValue(0, 0.45f);
	}

	// Token: 0x06000A7F RID: 2687 RVA: 0x000365B8 File Offset: 0x000347B8
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.skillExDamage += base.GetLevelValueDelta(0, 0.25f, oldLevel, newLevel);
		this.playerBase.normalAttackAddDamage -= base.GetLevelValueDelta(0, 0.25f, oldLevel, newLevel);
	}
}

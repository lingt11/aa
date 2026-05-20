using System;

// Token: 0x02000246 RID: 582
public class RelicTabooBlade : RelicBase
{
	// Token: 0x06000A79 RID: 2681 RVA: 0x000364EA File Offset: 0x000346EA
	public override void Enter()
	{
		this.playerBase.skillExDamage -= base.GetValue(0, 0.45f);
		this.playerBase.normalAttackAddDamage += base.GetValue(0, 0.45f);
	}

	// Token: 0x06000A7A RID: 2682 RVA: 0x00036528 File Offset: 0x00034728
	public override void Exit()
	{
		this.playerBase.skillExDamage += base.GetValue(0, 0.45f);
		this.playerBase.normalAttackAddDamage -= base.GetValue(0, 0.45f);
	}

	// Token: 0x06000A7B RID: 2683 RVA: 0x00036568 File Offset: 0x00034768
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.skillExDamage -= base.GetLevelValueDelta(0, 0.25f, oldLevel, newLevel);
		this.playerBase.normalAttackAddDamage += base.GetLevelValueDelta(0, 0.25f, oldLevel, newLevel);
	}
}

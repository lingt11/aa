using System;

// Token: 0x02000230 RID: 560
public class RelicOverride : RelicBase
{
	// Token: 0x06000A1F RID: 2591 RVA: 0x00035528 File Offset: 0x00033728
	public override void Enter()
	{
		this.playerBase.skillMpUsed += base.GetValue(0, 1f);
		this.playerBase.skillExDamage += base.GetValue(1, 0.75f);
	}

	// Token: 0x06000A20 RID: 2592 RVA: 0x00035566 File Offset: 0x00033766
	public override void Exit()
	{
		this.playerBase.skillMpUsed -= base.GetValue(0, 1f);
		this.playerBase.skillExDamage -= base.GetValue(1, 0.75f);
	}

	// Token: 0x06000A21 RID: 2593 RVA: 0x000355A4 File Offset: 0x000337A4
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.skillMpUsed += base.GetLevelValueDelta(0, 1f, oldLevel, newLevel);
		this.playerBase.skillExDamage += base.GetLevelValueDelta(1, 0.75f, oldLevel, newLevel);
	}
}

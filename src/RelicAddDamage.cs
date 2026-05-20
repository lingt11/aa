using System;

// Token: 0x020001EF RID: 495
public class RelicAddDamage : RelicBase
{
	// Token: 0x060008EC RID: 2284 RVA: 0x00031D5A File Offset: 0x0002FF5A
	public override void Enter()
	{
		this.playerBase.addDamagePercent += base.GetValue(0, 0.1f);
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x00031D7A File Offset: 0x0002FF7A
	public override void Exit()
	{
		this.playerBase.addDamagePercent -= base.GetValue(0, 0.1f);
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x00031D9A File Offset: 0x0002FF9A
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.addDamagePercent += base.GetLevelValueDelta(0, 0.1f, oldLevel, newLevel);
	}
}

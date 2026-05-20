using System;

// Token: 0x020001EE RID: 494
public class RelicAddBuff : RelicBase
{
	// Token: 0x060008E8 RID: 2280 RVA: 0x00031CF8 File Offset: 0x0002FEF8
	public override void Enter()
	{
		this.playerBase.buffAddDamage += base.GetValue(0, 1f);
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x00031D18 File Offset: 0x0002FF18
	public override void Exit()
	{
		this.playerBase.buffAddDamage -= base.GetValue(0, 1f);
	}

	// Token: 0x060008EA RID: 2282 RVA: 0x00031D38 File Offset: 0x0002FF38
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.buffAddDamage += base.GetLevelValueDelta(0, 1f, oldLevel, newLevel);
	}
}

using System;

// Token: 0x02000245 RID: 581
public class RelicSwordMaster : RelicBase
{
	// Token: 0x06000A75 RID: 2677 RVA: 0x00036488 File Offset: 0x00034688
	public override void Enter()
	{
		this.playerBase.addAttackEffectDamage += base.GetValue(0, 0.75f);
	}

	// Token: 0x06000A76 RID: 2678 RVA: 0x000364A8 File Offset: 0x000346A8
	public override void Exit()
	{
		this.playerBase.addAttackEffectDamage -= base.GetValue(0, 0.75f);
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x000364C8 File Offset: 0x000346C8
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.addAttackEffectDamage += base.GetLevelValueDelta(0, 0.75f, oldLevel, newLevel);
	}
}

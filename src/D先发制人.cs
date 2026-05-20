using System;

// Token: 0x020001AB RID: 427
public class D先发制人 : PasssiveSkill
{
	// Token: 0x060007FB RID: 2043 RVA: 0x0002E678 File Offset: 0x0002C878
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.finalAttackEvent = (RoleBase.FinalAttackDamage)Delegate.Combine(roleBase.finalAttackEvent, new RoleBase.FinalAttackDamage(this.AttackEvent));
	}

	// Token: 0x060007FC RID: 2044 RVA: 0x0002E6A1 File Offset: 0x0002C8A1
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.finalAttackEvent = (RoleBase.FinalAttackDamage)Delegate.Remove(roleBase.finalAttackEvent, new RoleBase.FinalAttackDamage(this.AttackEvent));
	}

	// Token: 0x060007FD RID: 2045 RVA: 0x0002E6CC File Offset: 0x0002C8CC
	private float AttackEvent(RoleBase attackrole, RoleBase hurtrole, AttackType attackType, ref float damage)
	{
		float num = (attackType == AttackType.Normal) ? 0.5f : 0.25f;
		if (hurtrole.Shield > 0L)
		{
			num *= 0.5f;
		}
		if ((float)hurtrole.hp * 1f / (float)hurtrole.maxHp >= 0.95f)
		{
			damage *= 1f + num;
		}
		return damage;
	}
}

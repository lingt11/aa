using System;

// Token: 0x0200019A RID: 410
public class C大先发制人 : PasssiveSkill
{
	// Token: 0x060007BA RID: 1978 RVA: 0x0002D734 File Offset: 0x0002B934
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.finalAttackEvent = (RoleBase.FinalAttackDamage)Delegate.Combine(roleBase.finalAttackEvent, new RoleBase.FinalAttackDamage(this.AttackEvent));
	}

	// Token: 0x060007BB RID: 1979 RVA: 0x0002D75D File Offset: 0x0002B95D
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.finalAttackEvent = (RoleBase.FinalAttackDamage)Delegate.Remove(roleBase.finalAttackEvent, new RoleBase.FinalAttackDamage(this.AttackEvent));
	}

	// Token: 0x060007BC RID: 1980 RVA: 0x0002D788 File Offset: 0x0002B988
	private float AttackEvent(RoleBase attackrole, RoleBase hurtrole, AttackType attackType, ref float damage)
	{
		float num = (attackType == AttackType.Normal) ? 1f : 0.5f;
		if (hurtrole.Shield > 0L)
		{
			num *= 0.5f;
		}
		if ((float)hurtrole.hp * 1f / (float)hurtrole.maxHp >= 0.85f)
		{
			damage *= 1f + num;
		}
		return damage;
	}
}

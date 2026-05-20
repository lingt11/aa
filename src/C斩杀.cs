using System;
using UnityEngine;

// Token: 0x020001A4 RID: 420
public class C斩杀 : PasssiveSkill
{
	// Token: 0x060007E1 RID: 2017 RVA: 0x0002E190 File Offset: 0x0002C390
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.finalAttackEvent = (RoleBase.FinalAttackDamage)Delegate.Combine(roleBase.finalAttackEvent, new RoleBase.FinalAttackDamage(this.CheckZhaSha));
		this.randomValue = (float)Mathf.RoundToInt(this.skillValues[0]) / 100f;
		this.addValue = (float)Mathf.RoundToInt(this.skillValues[1]) / 100f;
	}

	// Token: 0x060007E2 RID: 2018 RVA: 0x0002E1F8 File Offset: 0x0002C3F8
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.finalAttackEvent = (RoleBase.FinalAttackDamage)Delegate.Remove(roleBase.finalAttackEvent, new RoleBase.FinalAttackDamage(this.CheckZhaSha));
	}

	// Token: 0x060007E3 RID: 2019 RVA: 0x0002E224 File Offset: 0x0002C424
	private float CheckZhaSha(RoleBase attackRole, RoleBase hurtRole, AttackType attackType, ref float damage)
	{
		float num = (attackType == AttackType.Normal) ? this.addValue : (this.addValue * 0.5f);
		if ((float)hurtRole.hp / (float)hurtRole.maxHp <= this.randomValue)
		{
			damage *= 1f + num;
		}
		return damage;
	}

	// Token: 0x04000B61 RID: 2913
	private float randomValue;

	// Token: 0x04000B62 RID: 2914
	private float addValue;
}

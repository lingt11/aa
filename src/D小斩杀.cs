using System;
using UnityEngine;

// Token: 0x020001AF RID: 431
public class D小斩杀 : PasssiveSkill
{
	// Token: 0x0600080B RID: 2059 RVA: 0x0002EA78 File Offset: 0x0002CC78
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.finalAttackEvent = (RoleBase.FinalAttackDamage)Delegate.Combine(roleBase.finalAttackEvent, new RoleBase.FinalAttackDamage(this.CheckZhaSha));
		this.randomValue = (float)Mathf.RoundToInt(this.skillValues[0]) / 100f;
		this.addValue = (float)Mathf.RoundToInt(this.skillValues[1]) / 100f;
	}

	// Token: 0x0600080C RID: 2060 RVA: 0x0002EAE0 File Offset: 0x0002CCE0
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.finalAttackEvent = (RoleBase.FinalAttackDamage)Delegate.Remove(roleBase.finalAttackEvent, new RoleBase.FinalAttackDamage(this.CheckZhaSha));
	}

	// Token: 0x0600080D RID: 2061 RVA: 0x0002EB0C File Offset: 0x0002CD0C
	private float CheckZhaSha(RoleBase attackRole, RoleBase hurtRole, AttackType attackType, ref float damage)
	{
		float num = (attackType == AttackType.Normal) ? this.addValue : (this.addValue * 0.5f);
		if ((float)hurtRole.hp / (float)hurtRole.maxHp <= this.randomValue)
		{
			damage *= 1f + num;
		}
		return damage;
	}

	// Token: 0x04000B6D RID: 2925
	private float randomValue;

	// Token: 0x04000B6E RID: 2926
	private float addValue;
}

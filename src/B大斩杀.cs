using System;
using UnityEngine;

// Token: 0x0200018B RID: 395
public class B大斩杀 : PasssiveSkill
{
	// Token: 0x06000781 RID: 1921 RVA: 0x0002C9B0 File Offset: 0x0002ABB0
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.finalAttackEvent = (RoleBase.FinalAttackDamage)Delegate.Combine(roleBase.finalAttackEvent, new RoleBase.FinalAttackDamage(this.CheckZhaSha));
		this.randomValue = (float)Mathf.RoundToInt(this.skillValues[0]) / 100f;
		this.addValue = (float)Mathf.RoundToInt(this.skillValues[1]) / 100f;
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x0002CA18 File Offset: 0x0002AC18
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.finalAttackEvent = (RoleBase.FinalAttackDamage)Delegate.Remove(roleBase.finalAttackEvent, new RoleBase.FinalAttackDamage(this.CheckZhaSha));
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x0002CA44 File Offset: 0x0002AC44
	private float CheckZhaSha(RoleBase attackRole, RoleBase hurtRole, AttackType attackType, ref float damage)
	{
		float num = (attackType == AttackType.Normal) ? this.addValue : (this.addValue * 0.5f);
		if ((float)hurtRole.hp / (float)hurtRole.maxHp <= this.randomValue)
		{
			damage *= 1f + num;
		}
		return damage;
	}

	// Token: 0x04000B3F RID: 2879
	private float randomValue;

	// Token: 0x04000B40 RID: 2880
	private float addValue;
}

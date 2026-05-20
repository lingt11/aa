using System;
using UnityEngine;

// Token: 0x02000193 RID: 403
public class B超荆棘皮肤 : PasssiveSkill
{
	// Token: 0x0600079F RID: 1951 RVA: 0x0002D10C File Offset: 0x0002B30C
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.damageEvent = (RoleBase.DamageEnemy)Delegate.Combine(roleBase.damageEvent, new RoleBase.DamageEnemy(this.DamageEvent));
		this.randomValue = Mathf.RoundToInt(this.skillValues[0]);
		this.baseValue = Mathf.RoundToInt(this.skillValues[1]);
		this.levelValue = Mathf.RoundToInt(this.skillValues[2]);
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x0002D179 File Offset: 0x0002B379
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.damageEvent = (RoleBase.DamageEnemy)Delegate.Remove(roleBase.damageEvent, new RoleBase.DamageEnemy(this.DamageEvent));
	}

	// Token: 0x060007A1 RID: 1953 RVA: 0x0002D1A4 File Offset: 0x0002B3A4
	private float DamageEvent(RoleBase attackRole, RoleBase hurtRole, AttackType attackType, ref float damage)
	{
		if (Random.value * 100f < (float)this.randomValue)
		{
			if (base.CheckCD())
			{
				return damage;
			}
			float num = (float)(hurtRole.STA * this.levelValue + this.baseValue);
			num = (float)Util.GetPassSkillDamage(this.roleBase, this.skillAttribute, (double)num, false);
			bool isAttackWeek = this.roleBase.GetIsAttackWeek(AttackType.Skill);
			Util.OnLocalPlayerHit(this.roleBase, attackRole, (double)((int)num), Util.GetV2Angle(attackRole.MyTransform.position, this.roleBase.MyTransform.position), AttackType.Skill, isAttackWeek);
		}
		return damage;
	}

	// Token: 0x04000B4B RID: 2891
	private int randomValue;

	// Token: 0x04000B4C RID: 2892
	private int baseValue;

	// Token: 0x04000B4D RID: 2893
	private int levelValue;
}

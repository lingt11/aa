using System;
using UnityEngine;

// Token: 0x0200019C RID: 412
public class C大荆棘皮肤 : PasssiveSkill
{
	// Token: 0x060007C2 RID: 1986 RVA: 0x0002D88C File Offset: 0x0002BA8C
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.damageEvent = (RoleBase.DamageEnemy)Delegate.Combine(roleBase.damageEvent, new RoleBase.DamageEnemy(this.DamageEvent));
		this.randomValue = Mathf.RoundToInt(this.skillValues[0]);
		this.baseValue = Mathf.RoundToInt(this.skillValues[1]);
		this.levelValue = Mathf.RoundToInt(this.skillValues[2]);
	}

	// Token: 0x060007C3 RID: 1987 RVA: 0x0002D8F9 File Offset: 0x0002BAF9
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.damageEvent = (RoleBase.DamageEnemy)Delegate.Remove(roleBase.damageEvent, new RoleBase.DamageEnemy(this.DamageEvent));
	}

	// Token: 0x060007C4 RID: 1988 RVA: 0x0002D924 File Offset: 0x0002BB24
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

	// Token: 0x04000B54 RID: 2900
	private int randomValue;

	// Token: 0x04000B55 RID: 2901
	private int baseValue;

	// Token: 0x04000B56 RID: 2902
	private int levelValue;
}

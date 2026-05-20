using System;
using UnityEngine;

// Token: 0x020001B7 RID: 439
public class D荆棘皮肤 : PasssiveSkill
{
	// Token: 0x06000826 RID: 2086 RVA: 0x0002EFF8 File Offset: 0x0002D1F8
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.damageEvent = (RoleBase.DamageEnemy)Delegate.Combine(roleBase.damageEvent, new RoleBase.DamageEnemy(this.DamageEvent));
		this.randomValue = Mathf.RoundToInt(this.skillValues[0]);
		this.baseValue = Mathf.RoundToInt(this.skillValues[1]);
		this.levelValue = Mathf.RoundToInt(this.skillValues[2]);
	}

	// Token: 0x06000827 RID: 2087 RVA: 0x0002F065 File Offset: 0x0002D265
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.damageEvent = (RoleBase.DamageEnemy)Delegate.Remove(roleBase.damageEvent, new RoleBase.DamageEnemy(this.DamageEvent));
	}

	// Token: 0x06000828 RID: 2088 RVA: 0x0002F090 File Offset: 0x0002D290
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

	// Token: 0x04000B72 RID: 2930
	private int randomValue;

	// Token: 0x04000B73 RID: 2931
	private int baseValue;

	// Token: 0x04000B74 RID: 2932
	private int levelValue;
}

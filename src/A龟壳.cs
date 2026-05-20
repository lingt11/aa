using System;

// Token: 0x02000175 RID: 373
public class A龟壳 : PasssiveSkill
{
	// Token: 0x0600073F RID: 1855 RVA: 0x0002BB60 File Offset: 0x00029D60
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.damageEvent = (RoleBase.DamageEnemy)Delegate.Combine(roleBase.damageEvent, new RoleBase.DamageEnemy(this.DamageEvent));
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x0002BB89 File Offset: 0x00029D89
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.damageEvent = (RoleBase.DamageEnemy)Delegate.Remove(roleBase.damageEvent, new RoleBase.DamageEnemy(this.DamageEvent));
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x0002BBB4 File Offset: 0x00029DB4
	private float DamageEvent(RoleBase attackRole, RoleBase hurtRole, AttackType attackType, ref float f)
	{
		bool flag = attackRole.roleType == RoleType.Enemy && (attackRole as EnemyBase).isBoss;
		float num = (float)hurtRole.maxHp * (flag ? this.skillValues[1] : this.skillValues[0]) * 0.01f;
		if ((hurtRole.roleType == RoleType.King && attackRole.IsFromRoleType(RoleType.Player)) || (hurtRole.roleType == RoleType.Player && attackRole.IsFromRoleType(RoleType.King)))
		{
			float kingBattleDamageLevel = GameHelperClient.GetKingBattleDamageLevel();
			if (kingBattleDamageLevel > 0f)
			{
				num /= kingBattleDamageLevel;
			}
		}
		if (f > num)
		{
			f = num;
		}
		return f;
	}
}

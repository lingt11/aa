using System;

// Token: 0x020001C7 RID: 455
public class H电锯恶魔 : PasssiveSkill
{
	// Token: 0x0600085D RID: 2141 RVA: 0x0002FD18 File Offset: 0x0002DF18
	public override void Enter()
	{
		this.damage = this.skillValues[0] * 0.01f;
		this.hurt = this.skillValues[1] * 0.01f;
		this.bossHurt = this.skillValues[2] * 0.01f;
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
		PlayerBase roleBase2 = this.roleBase;
		roleBase2.damageEvent = (RoleBase.DamageEnemy)Delegate.Combine(roleBase2.damageEvent, new RoleBase.DamageEnemy(this.DamageEvent));
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x0002FDB0 File Offset: 0x0002DFB0
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
		PlayerBase roleBase2 = this.roleBase;
		roleBase2.damageEvent = (RoleBase.DamageEnemy)Delegate.Remove(roleBase2.damageEvent, new RoleBase.DamageEnemy(this.DamageEvent));
	}

	// Token: 0x0600085F RID: 2143 RVA: 0x0002FE0C File Offset: 0x0002E00C
	private float AttackEnemyEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		float num = (float)hurtrole.maxHp * this.damage + damage;
		damage = num;
		return num;
	}

	// Token: 0x06000860 RID: 2144 RVA: 0x0002FE30 File Offset: 0x0002E030
	private float DamageEvent(RoleBase attackRole, RoleBase hurtRole, AttackType attackType, ref float f)
	{
		bool flag = attackRole.roleType == RoleType.Enemy && (attackRole as EnemyBase).isBoss;
		float num = (float)hurtRole.maxHp * (flag ? this.bossHurt : this.hurt);
		if ((hurtRole.roleType == RoleType.King && attackRole.IsFromRoleType(RoleType.Player)) || (hurtRole.roleType == RoleType.Player && attackRole.IsFromRoleType(RoleType.King)))
		{
			float kingBattleDamageLevel = GameHelperClient.GetKingBattleDamageLevel();
			if (kingBattleDamageLevel > 0f)
			{
				num /= kingBattleDamageLevel;
			}
		}
		f = num;
		return num;
	}

	// Token: 0x04000B85 RID: 2949
	private float damage;

	// Token: 0x04000B86 RID: 2950
	private float hurt;

	// Token: 0x04000B87 RID: 2951
	private float bossHurt;
}

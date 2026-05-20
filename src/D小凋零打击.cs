using System;

// Token: 0x020001AD RID: 429
public class D小凋零打击 : PasssiveSkill
{
	// Token: 0x06000803 RID: 2051 RVA: 0x0002E7CA File Offset: 0x0002C9CA
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x06000804 RID: 2052 RVA: 0x0002E7F3 File Offset: 0x0002C9F3
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x06000805 RID: 2053 RVA: 0x0002E81C File Offset: 0x0002CA1C
	private float AttackEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		float num = this.skillValues[0] * 0.01f;
		if (hurtrole.roleType == RoleType.Enemy)
		{
			EnemyBase enemyBase = hurtrole as EnemyBase;
			if (enemyBase != null && enemyBase.isBoss)
			{
				num *= 0.2f;
			}
		}
		if (hurtrole.Shield > 0L)
		{
			num *= 0.25f;
		}
		float num2 = (float)hurtrole.hp * num;
		bool isAttackWeek = attackrole.GetIsAttackWeek(AttackType.AttackEffect);
		Util.OnLocalPlayerHit(attackrole, hurtrole, (double)((int)num2), 0f, AttackType.AttackEffect, isAttackWeek);
		return damage;
	}
}

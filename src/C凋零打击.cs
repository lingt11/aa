using System;

// Token: 0x02000198 RID: 408
public class C凋零打击 : PasssiveSkill
{
	// Token: 0x060007B2 RID: 1970 RVA: 0x0002D483 File Offset: 0x0002B683
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x0002D4AC File Offset: 0x0002B6AC
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x0002D4D8 File Offset: 0x0002B6D8
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

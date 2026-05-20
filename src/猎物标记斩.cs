using System;

// Token: 0x020001E7 RID: 487
public class 猎物标记斩 : PasssiveSkill
{
	// Token: 0x060008CE RID: 2254 RVA: 0x00031797 File Offset: 0x0002F997
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x000317C0 File Offset: 0x0002F9C0
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x000317EC File Offset: 0x0002F9EC
	private float AttackEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		this.count++;
		if (this.count >= 3)
		{
			this.count = 0;
			float num = this.skillValues[0] * 0.01f;
			if (hurtrole.roleType == RoleType.Enemy && (hurtrole as EnemyBase).isBoss)
			{
				num *= 0.2f;
			}
			float num2 = (float)hurtrole.maxHp * num;
			bool isAttackWeek = attackrole.GetIsAttackWeek(AttackType.AttackEffect);
			Util.OnLocalPlayerHit(attackrole, hurtrole, (double)((int)num2), 0f, AttackType.AttackEffect, isAttackWeek);
		}
		return damage;
	}

	// Token: 0x04000B9E RID: 2974
	public int count;
}

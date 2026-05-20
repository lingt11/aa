using System;

// Token: 0x020000F6 RID: 246
public class EquipSkillScythe : EquipSkillBase
{
	// Token: 0x06000512 RID: 1298 RVA: 0x0001E101 File Offset: 0x0001C301
	public override void Init()
	{
		base.Init();
		PlayerBase playerBase = this.playerBase;
		playerBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(playerBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x0001E130 File Offset: 0x0001C330
	private float AttackEnemyEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		float num = ((float)this.equipNum * this.skillValueAry[0] + this.skillValueUpAry[0] * (float)this.strengLevel) * 0.01f;
		if (hurtrole.roleType == RoleType.Enemy && (hurtrole as EnemyBase).isBoss)
		{
			num *= 0.2f;
		}
		bool isAttackWeek = attackrole.GetIsAttackWeek(AttackType.AttackEffect);
		Util.OnLocalPlayerHit(attackrole, hurtrole, (double)((float)hurtrole.maxHp * num), 0f, AttackType.AttackEffect, isAttackWeek);
		return damage;
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x0001E1A6 File Offset: 0x0001C3A6
	public override void Clear()
	{
		base.Clear();
		PlayerBase playerBase = this.playerBase;
		playerBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(playerBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}
}

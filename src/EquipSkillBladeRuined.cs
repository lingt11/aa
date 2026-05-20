using System;

// Token: 0x020000E0 RID: 224
public class EquipSkillBladeRuined : EquipSkillBase
{
	// Token: 0x060004A6 RID: 1190 RVA: 0x0001C413 File Offset: 0x0001A613
	public override void Init()
	{
		base.Init();
		PlayerBase playerBase = this.playerBase;
		playerBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(playerBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x0001C444 File Offset: 0x0001A644
	private float AttackEnemyEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		float num = ((float)this.equipNum * this.skillValueAry[0] + this.skillValueUpAry[0] * (float)this.strengLevel) * 0.01f;
		if (hurtrole.roleType == RoleType.Enemy && (hurtrole as EnemyBase).isBoss)
		{
			num *= 0.2f;
		}
		if (hurtrole.Shield > 0L)
		{
			num *= 0.25f;
		}
		bool isAttackWeek = attackrole.GetIsAttackWeek(AttackType.AttackEffect);
		Util.OnLocalPlayerHit(attackrole, hurtrole, (double)((float)hurtrole.hp * num), 0f, AttackType.AttackEffect, isAttackWeek);
		return damage;
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x0001C4CC File Offset: 0x0001A6CC
	public override void Clear()
	{
		base.Clear();
		PlayerBase playerBase = this.playerBase;
		playerBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(playerBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}
}

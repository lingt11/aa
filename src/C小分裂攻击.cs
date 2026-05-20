using System;
using System.Collections.Generic;

// Token: 0x0200019D RID: 413
public class C小分裂攻击 : PasssiveSkill
{
	// Token: 0x060007C6 RID: 1990 RVA: 0x0002D9C1 File Offset: 0x0002BBC1
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}

	// Token: 0x060007C7 RID: 1991 RVA: 0x0002D9EA File Offset: 0x0002BBEA
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}

	// Token: 0x060007C8 RID: 1992 RVA: 0x0002DA14 File Offset: 0x0002BC14
	private float AttackEnemyEvent(RoleBase attackRole, RoleBase hurtRole, ref float damage)
	{
		if (true)
		{
			float num = damage * this.skillValues[0] * 0.01f;
			List<RoleBase> list = (this.roleBase.roleType == RoleType.King) ? Game.PlayerManagerClient.GetRangeEnemy(base.Distance, hurtRole.MyTransform.position) : Game.EnemyManagerClient.GetRangeEnemy(base.Distance, hurtRole.MyTransform.position);
			bool isAttackWeek = this.roleBase.GetIsAttackWeek(AttackType.Skill);
			num = (float)Util.GetPassSkillDamage(this.roleBase, this.skillAttribute, (double)num, false);
			foreach (RoleBase roleBase in list)
			{
				if (roleBase != hurtRole)
				{
					Util.OnLocalPlayerHit(this.roleBase, roleBase, (double)((int)num), Util.GetV2Angle(roleBase.MyTransform.position, this.roleBase.MyTransform.position), AttackType.Skill, isAttackWeek);
				}
			}
			Game.EffectManager.PlayEffect(EffectDefine.SwordImpactEpicGold, 1f, hurtRole.MyTransform.position, base.Distance / 2.5f * 0.833f);
		}
		return damage;
	}
}

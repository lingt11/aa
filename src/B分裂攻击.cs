using System;
using System.Collections.Generic;

// Token: 0x02000187 RID: 391
public class B分裂攻击 : PasssiveSkill
{
	// Token: 0x06000772 RID: 1906 RVA: 0x0002C4AA File Offset: 0x0002A6AA
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x0002C4D3 File Offset: 0x0002A6D3
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}

	// Token: 0x06000774 RID: 1908 RVA: 0x0002C4FC File Offset: 0x0002A6FC
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

using System;
using System.Collections.Generic;

// Token: 0x020001D4 RID: 468
public class S超分裂攻击 : PasssiveSkill
{
	// Token: 0x0600088C RID: 2188 RVA: 0x00030904 File Offset: 0x0002EB04
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}

	// Token: 0x0600088D RID: 2189 RVA: 0x0003092D File Offset: 0x0002EB2D
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x00030958 File Offset: 0x0002EB58
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

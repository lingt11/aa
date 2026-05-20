using System;
using System.Collections.Generic;

// Token: 0x0200016D RID: 365
public class A大分裂攻击 : PasssiveSkill
{
	// Token: 0x06000722 RID: 1826 RVA: 0x0002B33F File Offset: 0x0002953F
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x0002B368 File Offset: 0x00029568
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x0002B394 File Offset: 0x00029594
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

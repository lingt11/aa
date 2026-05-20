using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200018A RID: 394
public class B大刀扇装甲 : PasssiveSkill
{
	// Token: 0x0600077D RID: 1917 RVA: 0x0002C7CC File Offset: 0x0002A9CC
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.damageEvent = (RoleBase.DamageEnemy)Delegate.Combine(roleBase.damageEvent, new RoleBase.DamageEnemy(this.DamageEvent));
		this.randomValue = Mathf.RoundToInt(this.skillValues[0]);
		this.baseValue = Mathf.RoundToInt(this.skillValues[1]);
		this.levelValue = Mathf.RoundToInt(this.skillValues[2]);
	}

	// Token: 0x0600077E RID: 1918 RVA: 0x0002C839 File Offset: 0x0002AA39
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.damageEvent = (RoleBase.DamageEnemy)Delegate.Remove(roleBase.damageEvent, new RoleBase.DamageEnemy(this.DamageEvent));
	}

	// Token: 0x0600077F RID: 1919 RVA: 0x0002C864 File Offset: 0x0002AA64
	private float DamageEvent(RoleBase attackRole, RoleBase hurtRole, AttackType attackType, ref float damage)
	{
		if (Random.value * 100f < (float)this.randomValue)
		{
			if (base.CheckCD())
			{
				return damage;
			}
			float num = (float)(hurtRole.STA * this.levelValue + this.baseValue);
			num = (float)Util.GetPassSkillDamage(this.roleBase, this.skillAttribute, (double)num, false);
			float distance = base.Distance;
			List<RoleBase> list = (this.roleBase.roleType == RoleType.King) ? Game.PlayerManagerClient.GetRangeEnemy(distance, hurtRole.MyTransform.position) : Game.EnemyManagerClient.GetRangeEnemy(distance, hurtRole.MyTransform.position);
			bool isAttackWeek = this.roleBase.GetIsAttackWeek(AttackType.Skill);
			foreach (RoleBase roleBase in list)
			{
				Util.OnLocalPlayerHit(this.roleBase, roleBase, (double)((int)num), Util.GetV2Angle(roleBase.MyTransform.position, this.roleBase.MyTransform.position), AttackType.Skill, isAttackWeek);
			}
			this.roleBase.CmdPlayEffect(EffectDefine.SpikeWaveIce, 1f, this.roleBase.MyTransform.position, distance / 3.25f);
		}
		return damage;
	}

	// Token: 0x04000B3C RID: 2876
	private int randomValue;

	// Token: 0x04000B3D RID: 2877
	private int baseValue;

	// Token: 0x04000B3E RID: 2878
	private int levelValue;
}

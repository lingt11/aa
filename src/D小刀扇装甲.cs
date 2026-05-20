using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001AE RID: 430
public class D小刀扇装甲 : PasssiveSkill
{
	// Token: 0x06000807 RID: 2055 RVA: 0x0002E894 File Offset: 0x0002CA94
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.damageEvent = (RoleBase.DamageEnemy)Delegate.Combine(roleBase.damageEvent, new RoleBase.DamageEnemy(this.DamageEvent));
		this.randomValue = Mathf.RoundToInt(this.skillValues[0]);
		this.baseValue = Mathf.RoundToInt(this.skillValues[1]);
		this.levelValue = Mathf.RoundToInt(this.skillValues[2]);
	}

	// Token: 0x06000808 RID: 2056 RVA: 0x0002E901 File Offset: 0x0002CB01
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.damageEvent = (RoleBase.DamageEnemy)Delegate.Remove(roleBase.damageEvent, new RoleBase.DamageEnemy(this.DamageEvent));
	}

	// Token: 0x06000809 RID: 2057 RVA: 0x0002E92C File Offset: 0x0002CB2C
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

	// Token: 0x04000B6A RID: 2922
	private int randomValue;

	// Token: 0x04000B6B RID: 2923
	private int baseValue;

	// Token: 0x04000B6C RID: 2924
	private int levelValue;
}

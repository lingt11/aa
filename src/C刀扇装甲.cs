using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000199 RID: 409
public class C刀扇装甲 : PasssiveSkill
{
	// Token: 0x060007B6 RID: 1974 RVA: 0x0002D550 File Offset: 0x0002B750
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.damageEvent = (RoleBase.DamageEnemy)Delegate.Combine(roleBase.damageEvent, new RoleBase.DamageEnemy(this.DamageEvent));
		this.randomValue = Mathf.RoundToInt(this.skillValues[0]);
		this.baseValue = Mathf.RoundToInt(this.skillValues[1]);
		this.levelValue = Mathf.RoundToInt(this.skillValues[2]);
	}

	// Token: 0x060007B7 RID: 1975 RVA: 0x0002D5BD File Offset: 0x0002B7BD
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.damageEvent = (RoleBase.DamageEnemy)Delegate.Remove(roleBase.damageEvent, new RoleBase.DamageEnemy(this.DamageEvent));
	}

	// Token: 0x060007B8 RID: 1976 RVA: 0x0002D5E8 File Offset: 0x0002B7E8
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

	// Token: 0x04000B51 RID: 2897
	private int randomValue;

	// Token: 0x04000B52 RID: 2898
	private int baseValue;

	// Token: 0x04000B53 RID: 2899
	private int levelValue;
}

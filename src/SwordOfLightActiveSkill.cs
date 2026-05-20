using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002FC RID: 764
public class SwordOfLightActiveSkill : ActiveSkillBase
{
	// Token: 0x060011A5 RID: 4517 RVA: 0x00067384 File Offset: 0x00065584
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, float rangeValue, float interval, float duration)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.skillTime = 2f;
		this.skillRange = rangeValue;
		Transform transform = Game.EffectManager.PlayEffect(EffectDefine.SwordOfLightSkill, this.skillTime, pos + new Vector3(0f, 0.07f, 0f), Vector3.one * (this.skillRange / 6.5f), new Vector3(0f, Random.value * 360f, 0f));
		this.particleCollisionInstance = transform.gameObject.GetComponent<ParticleCollisionInstance>();
		this.particleCollisionInstance.Init(this.skillRange / 6.5f);
	}

	// Token: 0x060011A6 RID: 4518 RVA: 0x00067450 File Offset: 0x00065650
	protected override void UpdateLocalSkill(float time)
	{
		if (this.attackRoleBase == null)
		{
			return;
		}
		if (this.particleCollisionInstance.CheckPosAry.Count > this.checkIndex)
		{
			Vector3 vector = this.particleCollisionInstance.CheckPosAry[this.checkIndex];
			this.checkIndex++;
			bool flag = this.attackRoleBase.roleType == RoleType.Enemy;
			List<RoleBase> attackRoles = this.attackRoleBase.GetAttackRoles();
			int count = attackRoles.Count;
			bool isAttackWeek = this.attackRoleBase.GetIsAttackWeek(AttackType.Skill);
			for (int i = 0; i < count; i++)
			{
				RoleBase roleBase = attackRoles[i];
				if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckYuanXing(vector, roleBase.MyTransform.position, 2f * (this.skillRange / 6.5f) + roleBase.RoleModeBase.addRange, false))
				{
					long skillDamage = Util.GetSkillDamage(this.activeSkillData, this.attackRoleBase);
					if (flag)
					{
						roleBase.OnHit(this.attackRoleBase, (double)skillDamage, Util.GetV2Angle(roleBase.MyTransform.position, vector), AttackType.Skill, isAttackWeek);
					}
					else
					{
						Util.OnLocalPlayerHit(this.attackRoleBase, roleBase, (double)skillDamage, Util.GetV2Angle(roleBase.MyTransform.position, vector), AttackType.Skill, isAttackWeek);
					}
				}
			}
		}
	}

	// Token: 0x04000FC8 RID: 4040
	private ParticleCollisionInstance particleCollisionInstance;

	// Token: 0x04000FC9 RID: 4041
	private int checkIndex;

	// Token: 0x04000FCA RID: 4042
	private float skillRange;
}

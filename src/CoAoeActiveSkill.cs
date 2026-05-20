using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002D1 RID: 721
public class CoAoeActiveSkill : ActiveSkillBase
{
	// Token: 0x060010D9 RID: 4313 RVA: 0x0005ED28 File Offset: 0x0005CF28
	public virtual void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, float rangeValue, string effectName, float interval, float duration, float effectScale)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.attackRange = rangeValue;
		this.checkTimer = duration - 0.25f;
		this.skillTime = duration;
		this.checkPos = pos;
		this.checkOffset = interval;
		Game.EffectManager.PlayEffect(effectName, duration, pos, rangeValue * effectScale);
	}

	// Token: 0x060010DA RID: 4314 RVA: 0x0005ED9C File Offset: 0x0005CF9C
	protected override void UpdateLocalSkill(float time)
	{
		if (this.skillTime < this.checkTimer - (float)this.checkNum * this.checkOffset)
		{
			this.checkNum++;
			bool flag = this.attackRoleBase.roleType == RoleType.Enemy;
			List<RoleBase> attackRoles = this.attackRoleBase.GetAttackRoles();
			int count = attackRoles.Count;
			bool isAttackWeek = this.attackRoleBase.GetIsAttackWeek(AttackType.Skill);
			for (int i = 0; i < count; i++)
			{
				RoleBase roleBase = attackRoles[i];
				if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckYuanXing(this.checkPos, roleBase.MyTransform.position, this.attackRange + roleBase.RoleModeBase.addRange, false))
				{
					long skillDamage = Util.GetSkillDamage(this.activeSkillData, this.attackRoleBase);
					if (flag)
					{
						roleBase.OnHit(this.attackRoleBase, (double)skillDamage, Util.GetV2Angle(roleBase.MyTransform.position, this.checkPos), AttackType.Skill, isAttackWeek);
					}
					else
					{
						Util.OnLocalPlayerHit(this.attackRoleBase, roleBase, (double)skillDamage, Util.GetV2Angle(roleBase.MyTransform.position, this.checkPos), AttackType.Skill, isAttackWeek);
					}
				}
			}
		}
	}

	// Token: 0x04000ECA RID: 3786
	protected float attackRange;

	// Token: 0x04000ECB RID: 3787
	protected float checkTimer;

	// Token: 0x04000ECC RID: 3788
	protected Vector3 checkPos;

	// Token: 0x04000ECD RID: 3789
	protected int checkNum;

	// Token: 0x04000ECE RID: 3790
	protected float checkOffset;
}

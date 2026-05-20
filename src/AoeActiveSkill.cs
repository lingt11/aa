using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002C7 RID: 711
public class AoeActiveSkill : ActiveSkillBase
{
	// Token: 0x060010B5 RID: 4277 RVA: 0x0005DC68 File Offset: 0x0005BE68
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, float rangeValue)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.attackRange = rangeValue;
		this.checkTimer = 1.75f;
		this.skillTime = 2f;
		this.checkPos = pos;
		Game.EffectManager.PlayEffect(EffectDefine.SpellThunder, 1.5f, pos, rangeValue / 1.5f);
	}

	// Token: 0x060010B6 RID: 4278 RVA: 0x0005DCDC File Offset: 0x0005BEDC
	protected override void UpdateLocalSkill(float time)
	{
		if (!this.isCheck && this.skillTime < this.checkTimer)
		{
			this.isCheck = true;
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

	// Token: 0x060010B7 RID: 4279 RVA: 0x0005DE20 File Offset: 0x0005C020
	public float GetDistanceV2(Vector3 pos)
	{
		Vector3 vector = this.checkPos;
		return Mathf.Sqrt(Mathf.Pow(pos.x - vector.x, 2f) + Mathf.Pow(pos.z - vector.z, 2f));
	}

	// Token: 0x04000EAF RID: 3759
	private float checkTimer;

	// Token: 0x04000EB0 RID: 3760
	private bool isCheck;

	// Token: 0x04000EB1 RID: 3761
	private Vector3 checkPos;

	// Token: 0x04000EB2 RID: 3762
	private float attackRange;
}

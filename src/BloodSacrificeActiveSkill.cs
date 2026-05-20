using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002CD RID: 717
public class BloodSacrificeActiveSkill : ActiveSkillBase
{
	// Token: 0x060010C9 RID: 4297 RVA: 0x0005E6DC File Offset: 0x0005C8DC
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, float rangeValue)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.attackRange = rangeValue;
		this.checkTimer = 1f;
		this.skillTime = 1.5f;
		this.checkPos = pos;
		Game.EffectManager.PlayEffect(EffectDefine.BloodSacrifice, 2f, pos + new Vector3(0f, 0.15f, 0f), new Vector3(rangeValue / 3f, rangeValue / 3f, rangeValue / 3f), new Vector3(0f, Random.value * 360f, 0f));
	}

	// Token: 0x060010CA RID: 4298 RVA: 0x0005E798 File Offset: 0x0005C998
	protected override void UpdateLocalSkill(float time)
	{
		if (!this.isCheck && this.skillTime < this.checkTimer)
		{
			this.isCheck = true;
			bool flag = this.attackRoleBase.roleType == RoleType.Enemy;
			List<RoleBase> attackRoles = this.attackRoleBase.GetAttackRoles();
			int count = attackRoles.Count;
			float num = 0f;
			bool isAttackWeek = this.attackRoleBase.GetIsAttackWeek(AttackType.Skill);
			for (int i = 0; i < count; i++)
			{
				RoleBase roleBase = attackRoles[i];
				if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckYuanXing(this.checkPos, roleBase.MyTransform.position, this.attackRange + roleBase.RoleModeBase.addRange, false))
				{
					long skillDamage = Util.GetSkillDamage(this.activeSkillData, this.attackRoleBase);
					if (flag)
					{
						num += (float)roleBase.OnHit(this.attackRoleBase, (double)skillDamage, Util.GetV2Angle(roleBase.MyTransform.position, this.checkPos), AttackType.Skill, isAttackWeek);
					}
					else
					{
						num += (float)Util.OnLocalPlayerHit(this.attackRoleBase, roleBase, (double)skillDamage, Util.GetV2Angle(roleBase.MyTransform.position, this.checkPos), AttackType.Skill, isAttackWeek);
					}
				}
			}
			if (num > 0f)
			{
				this.attackRoleBase.StartHealthHp((double)(num * 0.05f), this.attackRoleBase);
			}
		}
	}

	// Token: 0x04000EC0 RID: 3776
	private float checkTimer;

	// Token: 0x04000EC1 RID: 3777
	private bool isCheck;

	// Token: 0x04000EC2 RID: 3778
	private Vector3 checkPos;

	// Token: 0x04000EC3 RID: 3779
	private float attackRange;
}

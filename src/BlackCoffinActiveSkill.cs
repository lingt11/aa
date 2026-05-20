using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002CA RID: 714
public class BlackCoffinActiveSkill : ActiveSkillBase
{
	// Token: 0x060010C0 RID: 4288 RVA: 0x0005E184 File Offset: 0x0005C384
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, float rangeValue)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.attackRange = rangeValue;
		this.checkTimer = 1f;
		this.skillTime = 1.5f;
		this.checkPos = pos;
		Game.EffectManager.PlayEffect(EffectDefine.BlackCoffin, 1.5f, pos + new Vector3(0f, 0.15f, 0f), new Vector3(rangeValue / 3f, rangeValue / 1.5f, rangeValue / 3f), new Vector3(0f, Random.value * 360f, 0f));
	}

	// Token: 0x060010C1 RID: 4289 RVA: 0x0005E240 File Offset: 0x0005C440
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

	// Token: 0x04000EB7 RID: 3767
	private float checkTimer;

	// Token: 0x04000EB8 RID: 3768
	private bool isCheck;

	// Token: 0x04000EB9 RID: 3769
	private Vector3 checkPos;

	// Token: 0x04000EBA RID: 3770
	private float attackRange;
}

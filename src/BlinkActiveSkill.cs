using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002CC RID: 716
public class BlinkActiveSkill : ActiveSkillBase
{
	// Token: 0x060010C5 RID: 4293 RVA: 0x0005E4E0 File Offset: 0x0005C6E0
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, float rangeValue)
	{
		if (attackRole.hasAuthority)
		{
			attackRole.CmdTeleportBlink(pos);
		}
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.attackRange = rangeValue;
		this.checkTimer = 1.75f;
		this.skillTime = 2f;
		this.checkPos = pos;
		Game.EffectManager.PlayEffect(EffectDefine.SpellThunder, 1.5f, pos, rangeValue / 1.5f);
	}

	// Token: 0x060010C6 RID: 4294 RVA: 0x0005E564 File Offset: 0x0005C764
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
				if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && this.GetDistanceV2(roleBase.MyTransform.position) < this.attackRange)
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

	// Token: 0x060010C7 RID: 4295 RVA: 0x0005E694 File Offset: 0x0005C894
	public float GetDistanceV2(Vector3 pos)
	{
		Vector3 vector = this.checkPos;
		return Mathf.Sqrt(Mathf.Pow(pos.x - vector.x, 2f) + Mathf.Pow(pos.z - vector.z, 2f));
	}

	// Token: 0x04000EBC RID: 3772
	private float checkTimer;

	// Token: 0x04000EBD RID: 3773
	private bool isCheck;

	// Token: 0x04000EBE RID: 3774
	private Vector3 checkPos;

	// Token: 0x04000EBF RID: 3775
	private float attackRange;
}

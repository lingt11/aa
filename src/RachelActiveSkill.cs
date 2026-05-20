using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002F1 RID: 753
public class RachelActiveSkill : AnimationOverrideActiveSkill
{
	// Token: 0x06001160 RID: 4448 RVA: 0x00064D20 File Offset: 0x00062F20
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, float rangeValue, float interval, float duration)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.attackRange = rangeValue;
		this.checkTimer = duration - 0.05f;
		this.skillTime = duration;
		this.checkOffset = interval;
		Game.EffectManager.PlayEffect(EffectDefine.SpellThunder, duration, pos, rangeValue / 1.5f).SetParent(this.attackRoleBase.MyTransform);
		base.LoadAnimatorController("Bundles/Animator/RachelSkill");
		this.attackRoleBase.UpdateAnimSpeed(1.81f);
	}

	// Token: 0x06001161 RID: 4449 RVA: 0x00064DBC File Offset: 0x00062FBC
	protected override void UpdateLocalSkill(float time)
	{
		if (this.attackRoleBase == null)
		{
			return;
		}
		PlayerBase playerBase = this.attackRoleBase as PlayerBase;
		if (playerBase != null)
		{
			playerBase.CharacterController.Move(time * 35f * Mathf.Sqrt(Mathf.Max(0f, this.skillTime - 0.2f)) * playerBase.MyTransform.forward + Time.deltaTime * Vector3.down);
		}
		if (this.skillTime < this.checkTimer - (float)this.checkNum * this.checkOffset)
		{
			this.checkNum++;
			bool flag = this.attackRoleBase.roleType == RoleType.Enemy;
			List<RoleBase> attackRoles = this.attackRoleBase.GetAttackRoles();
			int count = attackRoles.Count;
			Vector3 position = this.attackRoleBase.MyTransform.position;
			bool isAttackWeek = this.attackRoleBase.GetIsAttackWeek(AttackType.Skill);
			for (int i = 0; i < count; i++)
			{
				RoleBase roleBase = attackRoles[i];
				if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckYuanXing(position, roleBase.MyTransform.position, this.attackRange + roleBase.RoleModeBase.addRange, false))
				{
					long skillDamage = Util.GetSkillDamage(this.activeSkillData, this.attackRoleBase);
					if (flag)
					{
						roleBase.OnHit(this.attackRoleBase, (double)skillDamage, Util.GetV2Angle(roleBase.MyTransform.position, position), AttackType.Skill, isAttackWeek);
					}
					else
					{
						Util.OnLocalPlayerHit(this.attackRoleBase, roleBase, (double)skillDamage, Util.GetV2Angle(roleBase.MyTransform.position, position), AttackType.Skill, isAttackWeek);
					}
				}
			}
		}
	}

	// Token: 0x04000F79 RID: 3961
	protected float attackRange;

	// Token: 0x04000F7A RID: 3962
	private float checkTimer;

	// Token: 0x04000F7B RID: 3963
	private int checkNum;

	// Token: 0x04000F7C RID: 3964
	private float checkOffset;
}

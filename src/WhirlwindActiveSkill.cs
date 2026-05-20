using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002FE RID: 766
public class WhirlwindActiveSkill : AnimationOverrideActiveSkill
{
	// Token: 0x060011AF RID: 4527 RVA: 0x000676CC File Offset: 0x000658CC
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, float rangeValue, float interval, float duration)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.attackRange = rangeValue;
		this.skillTime = duration;
		this.checkTimer = duration - 0.1f;
		this.checkOffset = interval;
		this.effectTransform = AssetManager.LoadPrefab(EffectDefine.WhirlwindSkill, null, true).transform;
		this.effectTransform.localPosition = this.attackRoleBase.MyTransform.position + new Vector3(0f, 1f, 0f);
		this.effectTransform.localRotation = Quaternion.identity;
		float num = this.attackRange / 3f;
		this.effectTransform.localScale = new Vector3(num, num, num);
		base.LoadAnimatorController("Bundles/Animator/GreatSword_Whirlwind_Loop_Root");
	}

	// Token: 0x060011B0 RID: 4528 RVA: 0x000677A8 File Offset: 0x000659A8
	protected override void UpdateLocalSkill(float time)
	{
		if (this.attackRoleBase == null)
		{
			return;
		}
		if (this.attackRoleBase == GameHelperClient.localPlayer)
		{
			GameHelperClient.IsMoveToAttack = false;
			GameHelperClient.localPlayer.PlayerMove(false);
		}
		else
		{
			PlayerBase playerBase = this.attackRoleBase as PlayerBase;
			if (playerBase != null)
			{
				playerBase.PlayerMove(false);
			}
		}
		if (this.skillTime < this.checkTimer - (float)this.checkNum * this.checkOffset)
		{
			this.checkNum++;
			bool flag = this.attackRoleBase.roleType == RoleType.Enemy;
			List<RoleBase> attackRoles = this.attackRoleBase.GetAttackRoles();
			int count = attackRoles.Count;
			long skillDamage = Util.GetSkillDamage(this.activeSkillData, this.attackRoleBase);
			bool isAttackWeek = this.attackRoleBase.GetIsAttackWeek(AttackType.Skill);
			for (int i = 0; i < count; i++)
			{
				RoleBase roleBase = attackRoles[i];
				if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckYuanXing(this.attackRoleBase.MyTransform.position, roleBase.MyTransform.position, this.attackRange + roleBase.RoleModeBase.addRange, false))
				{
					if (flag)
					{
						roleBase.OnHit(this.attackRoleBase, (double)skillDamage, this.effectTransform.eulerAngles.y, AttackType.Skill, isAttackWeek);
					}
					else
					{
						Util.OnLocalPlayerHit(this.attackRoleBase, roleBase, (double)skillDamage, this.effectTransform.eulerAngles.y, AttackType.Skill, isAttackWeek);
					}
				}
			}
		}
	}

	// Token: 0x060011B1 RID: 4529 RVA: 0x00067940 File Offset: 0x00065B40
	protected override void UpdateSkill(float time)
	{
		base.UpdateSkill(time);
		if (this.attackRoleBase != null)
		{
			if (this.attackRoleBase.IsDead())
			{
				this.skillTime = -1f;
			}
			this.effectTransform.eulerAngles = new Vector3(0f, this.effectTransform.eulerAngles.y + time * 30f, 0f);
			this.effectTransform.localPosition = this.attackRoleBase.MyTransform.position + new Vector3(0f, 1.3f, 0f);
		}
	}

	// Token: 0x060011B2 RID: 4530 RVA: 0x000679E0 File Offset: 0x00065BE0
	public override void Clear(int clearData)
	{
		base.Clear(clearData);
		if (this.effectTransform != null)
		{
			AssetManager.UnLoadPrefab(this.effectTransform.gameObject, false);
			this.effectTransform = null;
		}
	}

	// Token: 0x04000FCC RID: 4044
	private float attackRange;

	// Token: 0x04000FCD RID: 4045
	private float checkTimer;

	// Token: 0x04000FCE RID: 4046
	private int checkNum;

	// Token: 0x04000FCF RID: 4047
	private float checkOffset;

	// Token: 0x04000FD0 RID: 4048
	private Transform effectTransform;
}

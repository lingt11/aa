using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002F5 RID: 757
public class SacrificeActiveSkill : SwitchActiveSkill
{
	// Token: 0x06001170 RID: 4464 RVA: 0x000657D8 File Offset: 0x000639D8
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, float rangeValue, float interval, float duration, int skillBookId)
	{
		this.attackRoleBase = attackRole;
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRange = rangeValue;
		this.skillTime = duration;
		this.checkTimer = duration - 0.1f;
		this.checkOffset = interval;
		this.effectTransform = AssetManager.LoadPrefab(EffectDefine.SacrificeSkill, null, true).transform;
		this.effectTransform.localPosition = this.attackRoleBase.MyTransform.position + new Vector3(0f, 1f, 0f);
		this.effectTransform.localScale = new Vector3(this.attackRange / 1.5f, this.attackRange / 1.5f, this.attackRange / 1.5f);
		this.effectTransform.eulerAngles = new Vector3(-90f, 0f, 0f);
		base.InitSwitchSkill(activeSkillType, this.attackRoleBase, skillBookId);
		this.nextHpCostTime = this.skillTime;
	}

	// Token: 0x06001171 RID: 4465 RVA: 0x000658E8 File Offset: 0x00063AE8
	protected override void UpdateLocalSkill(float time)
	{
		base.UpdateLocalSkill(time);
		if (this.attackRoleBase == null)
		{
			return;
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
		if (GameHelperClient.isReady)
		{
			this.nextHpCostTime = this.skillTime;
			return;
		}
		if (this.skillTime < this.nextHpCostTime)
		{
			this.nextHpCostTime -= 0.5f;
			this.attackRoleBase.StartUpdateHealthHp((double)((float)(-(float)this.attackRoleBase.maxHp) * 0.05f), this.attackRoleBase);
		}
	}

	// Token: 0x06001172 RID: 4466 RVA: 0x00065AA0 File Offset: 0x00063CA0
	protected override void UpdateSkill(float time)
	{
		base.UpdateSkill(time);
		if (this.attackRoleBase != null)
		{
			this.effectTransform.eulerAngles = new Vector3(-90f, this.effectTransform.eulerAngles.y + time * 30f, 0f);
			this.effectTransform.localPosition = this.attackRoleBase.MyTransform.position + new Vector3(0f, 1.3f, 0f);
		}
	}

	// Token: 0x06001173 RID: 4467 RVA: 0x00065B28 File Offset: 0x00063D28
	public override void Clear(int clearData)
	{
		base.Clear(clearData);
		if (this.effectTransform != null)
		{
			AssetManager.UnLoadPrefab(this.effectTransform.gameObject, false);
			this.effectTransform = null;
		}
	}

	// Token: 0x04000F8F RID: 3983
	private float attackRange;

	// Token: 0x04000F90 RID: 3984
	private float checkTimer;

	// Token: 0x04000F91 RID: 3985
	private int checkNum;

	// Token: 0x04000F92 RID: 3986
	private float checkOffset;

	// Token: 0x04000F93 RID: 3987
	private Transform effectTransform;

	// Token: 0x04000F94 RID: 3988
	private float nextHpCostTime;
}

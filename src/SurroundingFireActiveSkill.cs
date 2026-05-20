using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002FA RID: 762
public class SurroundingFireActiveSkill : ActiveSkillBase
{
	// Token: 0x0600119C RID: 4508 RVA: 0x00066EC4 File Offset: 0x000650C4
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, float rangeValue, float interval, float duration)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.attackRange = rangeValue;
		this.skillTime = duration;
		this.checkTimer = duration - 0.1f;
		this.checkOffset = interval;
		this.effectTransform = AssetManager.LoadPrefab(EffectDefine.SurroundingFire, null, true).transform;
		this.effectTransform.localPosition = this.attackRoleBase.MyTransform.position + new Vector3(0f, 1f, 0f);
		this.effectTransform.localScale = new Vector3(this.attackRange / 1.8f, this.attackRange / 1.8f, this.attackRange / 1.8f);
		this.effectTransform.eulerAngles = new Vector3(-90f, 0f, 0f);
	}

	// Token: 0x0600119D RID: 4509 RVA: 0x00066FB8 File Offset: 0x000651B8
	protected override void UpdateLocalSkill(float time)
	{
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
	}

	// Token: 0x0600119E RID: 4510 RVA: 0x00067110 File Offset: 0x00065310
	protected override void UpdateSkill(float time)
	{
		base.UpdateSkill(time);
		if (this.attackRoleBase != null)
		{
			this.effectTransform.eulerAngles = new Vector3(-90f, this.effectTransform.eulerAngles.y + time * 30f, 0f);
			this.effectTransform.localPosition = this.attackRoleBase.MyTransform.position + new Vector3(0f, 1.3f, 0f);
		}
	}

	// Token: 0x0600119F RID: 4511 RVA: 0x00067198 File Offset: 0x00065398
	public override void Clear(int clearData)
	{
		base.Clear(clearData);
		if (this.effectTransform != null)
		{
			AssetManager.UnLoadPrefab(this.effectTransform.gameObject, false);
			this.effectTransform = null;
		}
	}

	// Token: 0x04000FC0 RID: 4032
	private float attackRange;

	// Token: 0x04000FC1 RID: 4033
	private float checkTimer;

	// Token: 0x04000FC2 RID: 4034
	private int checkNum;

	// Token: 0x04000FC3 RID: 4035
	private float checkOffset;

	// Token: 0x04000FC4 RID: 4036
	private Transform effectTransform;
}

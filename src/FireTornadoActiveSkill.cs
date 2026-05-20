using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002DE RID: 734
public class FireTornadoActiveSkill : ActiveSkillBase
{
	// Token: 0x06001104 RID: 4356 RVA: 0x000610C0 File Offset: 0x0005F2C0
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, float rangeValue, float interval, float duration)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.attackRange = rangeValue;
		this.skillTime = duration;
		this.checkTimer = duration - 0.2f;
		this.checkOffset = interval;
		this.effectTransform = AssetManager.LoadPrefab(EffectDefine.FireTornado, null, true).transform;
		this.effectTransform.localPosition = pos;
		this.syncTimer = 10f;
		this.effectTransform.localScale = new Vector3(this.attackRange * 2f, this.attackRange * 2f, this.attackRange * 2f);
	}

	// Token: 0x06001105 RID: 4357 RVA: 0x0006117C File Offset: 0x0005F37C
	protected override void UpdateLocalSkill(float time)
	{
		if (this.attackRoleBase == null)
		{
			return;
		}
		if (this.targetRole != null)
		{
			Vector3 normalized = new Vector3(this.targetRole.MyTransform.position.x - this.effectTransform.position.x, 0f, this.targetRole.MyTransform.position.z - this.effectTransform.position.z).normalized;
			this.effectTransform.position += 2f * time * normalized;
		}
		this.syncTimer += time;
		if (this.syncTimer > 0.05f)
		{
			int num = Mathf.RoundToInt(this.effectTransform.position.x * 10f);
			int num2 = Mathf.RoundToInt(this.effectTransform.position.z * 10f);
			this.attackRoleBase.UpdateSkillData((float)(num * 1000 + num2));
			this.syncTimer = 0f;
		}
		float num3 = 9999f;
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
				if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead())
				{
					float v2Distance = Util.GetV2Distance(this.effectTransform.position, roleBase.MyTransform.position);
					if (v2Distance < num3)
					{
						num3 = v2Distance;
						this.targetRole = roleBase;
					}
					if (v2Distance < this.attackRange + roleBase.RoleModeBase.addRange)
					{
						if (flag)
						{
							roleBase.OnHit(this.attackRoleBase, (double)skillDamage, Util.GetV2Angle(this.effectTransform.position, this.effectTransform.position), AttackType.Skill, isAttackWeek);
						}
						else
						{
							Util.OnLocalPlayerHit(this.attackRoleBase, roleBase, (double)skillDamage, Util.GetV2Angle(this.effectTransform.position, this.effectTransform.position), AttackType.Skill, isAttackWeek);
						}
					}
				}
			}
		}
	}

	// Token: 0x06001106 RID: 4358 RVA: 0x00061410 File Offset: 0x0005F610
	protected override void UpdateSkill(float time)
	{
		base.UpdateSkill(time);
		if (this.attackRoleBase != null && !this.attackRoleBase.hasAuthority)
		{
			float x = this.attackRoleBase.SyncSkillData / 10000f;
			float z = (this.attackRoleBase.SyncSkillData - Mathf.Round(this.attackRoleBase.SyncSkillData / 1000f) * 1000f) / 10f;
			this.effectTransform.localPosition = Vector3.Lerp(this.effectTransform.localPosition, new Vector3(x, 0f, z), 8f * time);
		}
	}

	// Token: 0x06001107 RID: 4359 RVA: 0x000614AE File Offset: 0x0005F6AE
	public override void Clear(int clearData)
	{
		base.Clear(clearData);
		if (this.effectTransform != null)
		{
			AssetManager.UnLoadPrefab(this.effectTransform.gameObject, false);
			this.effectTransform = null;
		}
	}

	// Token: 0x04000F0A RID: 3850
	private float attackRange;

	// Token: 0x04000F0B RID: 3851
	private float checkTimer;

	// Token: 0x04000F0C RID: 3852
	private int checkNum;

	// Token: 0x04000F0D RID: 3853
	private float checkOffset;

	// Token: 0x04000F0E RID: 3854
	private Transform effectTransform;

	// Token: 0x04000F0F RID: 3855
	private float syncTimer;

	// Token: 0x04000F10 RID: 3856
	private const float MoveSpeed = 2f;

	// Token: 0x04000F11 RID: 3857
	private RoleBase targetRole;
}

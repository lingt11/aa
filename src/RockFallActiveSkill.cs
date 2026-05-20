using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002F4 RID: 756
public class RockFallActiveSkill : ActiveSkillBase
{
	// Token: 0x0600116B RID: 4459 RVA: 0x00065474 File Offset: 0x00063674
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, float rangeValue)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.attackRange = rangeValue;
		this.checkTimer = 0.5f;
		this.skillTime = 2f;
		this.checkPos = pos;
		this.effectTransform = AssetManager.LoadPrefab(EffectDefine.YunShi, null, true).transform;
		this.startPos = pos + this.attackRoleBase.MyTransform.forward * -5f + new Vector3(0f, 8f, 0f);
		this.effectTransform.position = this.startPos;
		this.effectTransform.localScale = rangeValue / 5f * Vector3.one;
		this.effectTransform.LookAt(this.checkPos);
	}

	// Token: 0x0600116C RID: 4460 RVA: 0x00065560 File Offset: 0x00063760
	protected override void UpdateSkill(float time)
	{
		if (this.skillTime > 0.5f)
		{
			this.effectTransform.position = Vector3.Lerp(this.startPos, this.checkPos, (2f - this.skillTime) / 1.5f);
		}
		else if (this.effectTransform != null)
		{
			AssetManager.UnLoadPrefab(this.effectTransform.gameObject, false);
			this.effectTransform = null;
		}
		if (!this.isCheck && this.skillTime < this.checkTimer)
		{
			this.isCheck = true;
			Game.EffectManager.PlayEffect(EffectDefine.SmokeEffect, 3f, this.checkPos + new Vector3(0f, 0.5f, 0f), this.attackRange / 2f);
			Game.CameraManager.ShakeCameraByPos(this.checkPos, 0.1f, 0.75f, 15, false);
			if (this.attackRoleBase.hasAuthority)
			{
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
	}

	// Token: 0x0600116D RID: 4461 RVA: 0x00065760 File Offset: 0x00063960
	public float GetDistanceV2(Vector3 pos)
	{
		Vector3 vector = this.checkPos;
		return Mathf.Sqrt(Mathf.Pow(pos.x - vector.x, 2f) + Mathf.Pow(pos.z - vector.z, 2f));
	}

	// Token: 0x0600116E RID: 4462 RVA: 0x000657A8 File Offset: 0x000639A8
	public override void Clear(int clearData)
	{
		base.Clear(clearData);
		if (this.effectTransform != null)
		{
			AssetManager.UnLoadPrefab(this.effectTransform.gameObject, false);
			this.effectTransform = null;
		}
	}

	// Token: 0x04000F89 RID: 3977
	private float checkTimer;

	// Token: 0x04000F8A RID: 3978
	private bool isCheck;

	// Token: 0x04000F8B RID: 3979
	private Vector3 checkPos;

	// Token: 0x04000F8C RID: 3980
	private float attackRange;

	// Token: 0x04000F8D RID: 3981
	private Transform effectTransform;

	// Token: 0x04000F8E RID: 3982
	private Vector3 startPos;
}

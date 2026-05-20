using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002DB RID: 731
public class FireStepActiveSkill : ActiveSkillBase
{
	// Token: 0x060010FE RID: 4350 RVA: 0x00060B7C File Offset: 0x0005ED7C
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, float rangeValue, float interval, float duration)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.attackRange = rangeValue;
		this.skillTime = duration;
		this.lasPos = attackRole.MyTransform.position;
		this.fireStepCheckDatas = new List<FireStepActiveSkill.FireStepCheckData>();
		this.checkOffset = interval;
		this.fireStepCheckRoles = new List<FireStepActiveSkill.FireStepCheckRole>();
		this.effectTransform = AssetManager.LoadPrefab(EffectDefine.FireStepRole, null, true).transform;
		this.effectTransform.SetParent(this.attackRoleBase.MyTransform);
		this.effectTransform.localPosition = Vector3.zero;
		this.effectTransform.localScale = Vector3.one * this.attackRange;
	}

	// Token: 0x060010FF RID: 4351 RVA: 0x00060C44 File Offset: 0x0005EE44
	protected override void UpdateSkill(float time)
	{
		base.UpdateSkill(time);
		if (this.attackRoleBase != null)
		{
			if (this.attackRoleBase.IsDead())
			{
				this.skillTime = -1f;
			}
			if (this.skillTime > 3f)
			{
				Vector3 position = this.attackRoleBase.MyTransform.position;
				this.moveDistance += Util.GetV2Distance(position, this.lasPos);
				if (this.moveDistance > this.attackRange / 2f)
				{
					this.moveDistance = 0f;
					FireStepActiveSkill.FireStepCheckData fireStepCheckData = new FireStepActiveSkill.FireStepCheckData();
					Transform transform = AssetManager.LoadPrefab(EffectDefine.FireStepGround, null, true).transform;
					transform.localPosition = this.attackRoleBase.MyTransform.position + new Vector3(0f, 0.1f, 0f);
					transform.localScale = new Vector3(this.attackRange * 2f, this.attackRange * 2f, this.attackRange * 2f);
					fireStepCheckData.effectTransform = transform;
					this.fireStepCheckDatas.Add(fireStepCheckData);
				}
				this.lasPos = position;
			}
			else if (this.effectTransform != null)
			{
				AssetManager.UnLoadPrefab(this.effectTransform.gameObject, false);
				this.effectTransform = null;
			}
			for (int i = this.fireStepCheckDatas.Count - 1; i > -1; i--)
			{
				FireStepActiveSkill.FireStepCheckData fireStepCheckData2 = this.fireStepCheckDatas[i];
				fireStepCheckData2.checkTimer += time;
				if (fireStepCheckData2.checkTimer >= 3f)
				{
					AssetManager.UnLoadPrefab(fireStepCheckData2.effectTransform.gameObject, false);
					this.fireStepCheckDatas.RemoveAt(i);
				}
				if (this.attackRoleBase.hasAuthority && fireStepCheckData2.checkTimer >= fireStepCheckData2.nextCheckTimer)
				{
					fireStepCheckData2.nextCheckTimer += this.checkOffset;
					bool flag = this.attackRoleBase.roleType == RoleType.Enemy;
					List<RoleBase> attackRoles = this.attackRoleBase.GetAttackRoles();
					int count = attackRoles.Count;
					Vector3 position2 = fireStepCheckData2.effectTransform.position;
					bool isAttackWeek = this.attackRoleBase.GetIsAttackWeek(AttackType.Skill);
					for (int j = 0; j < count; j++)
					{
						RoleBase roleBase = attackRoles[j];
						if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead())
						{
							bool flag2 = true;
							int count2 = this.fireStepCheckRoles.Count;
							for (int k = 0; k < count2; k++)
							{
								if (this.fireStepCheckRoles[k].role != null && this.fireStepCheckRoles[k].role == roleBase)
								{
									flag2 = false;
									break;
								}
							}
							if (flag2 && Util.NewCheckYuanXing(position2, roleBase.MyTransform.position, this.attackRange + roleBase.RoleModeBase.addRange, false))
							{
								FireStepActiveSkill.FireStepCheckRole fireStepCheckRole = new FireStepActiveSkill.FireStepCheckRole();
								fireStepCheckRole.role = roleBase;
								this.fireStepCheckRoles.Add(fireStepCheckRole);
								long skillDamage = Util.GetSkillDamage(this.activeSkillData, this.attackRoleBase);
								if (flag)
								{
									roleBase.OnHit(this.attackRoleBase, (double)skillDamage, Util.GetV2Angle(roleBase.MyTransform.position, position2), AttackType.Skill, isAttackWeek);
								}
								else
								{
									Util.OnLocalPlayerHit(this.attackRoleBase, roleBase, (double)skillDamage, Util.GetV2Angle(roleBase.MyTransform.position, position2), AttackType.Skill, isAttackWeek);
								}
							}
						}
					}
				}
			}
			if (this.attackRoleBase.hasAuthority)
			{
				for (int l = this.fireStepCheckRoles.Count - 1; l > -1; l--)
				{
					this.fireStepCheckRoles[l].checkTimer += time;
					if (this.fireStepCheckRoles[l].checkTimer > this.checkOffset)
					{
						this.fireStepCheckRoles.RemoveAt(l);
					}
				}
			}
		}
	}

	// Token: 0x06001100 RID: 4352 RVA: 0x00061040 File Offset: 0x0005F240
	public override void Clear(int clearData)
	{
		base.Clear(clearData);
		if (this.fireStepCheckDatas != null)
		{
			int count = this.fireStepCheckDatas.Count;
			for (int i = 0; i < count; i++)
			{
				AssetManager.UnLoadPrefab(this.fireStepCheckDatas[i].effectTransform.gameObject, false);
			}
			this.fireStepCheckDatas = null;
		}
		if (this.effectTransform != null)
		{
			AssetManager.UnLoadPrefab(this.effectTransform.gameObject, false);
			this.effectTransform = null;
		}
	}

	// Token: 0x04000EFC RID: 3836
	private float attackRange;

	// Token: 0x04000EFD RID: 3837
	private float checkTimer;

	// Token: 0x04000EFE RID: 3838
	private float checkOffset;

	// Token: 0x04000EFF RID: 3839
	private List<FireStepActiveSkill.FireStepCheckData> fireStepCheckDatas;

	// Token: 0x04000F00 RID: 3840
	private Vector3 lasPos;

	// Token: 0x04000F01 RID: 3841
	private float moveDistance;

	// Token: 0x04000F02 RID: 3842
	private const float FireTime = 3f;

	// Token: 0x04000F03 RID: 3843
	private Transform effectTransform;

	// Token: 0x04000F04 RID: 3844
	private List<FireStepActiveSkill.FireStepCheckRole> fireStepCheckRoles;

	// Token: 0x020002DC RID: 732
	private class FireStepCheckData
	{
		// Token: 0x04000F05 RID: 3845
		public Transform effectTransform;

		// Token: 0x04000F06 RID: 3846
		public float checkTimer;

		// Token: 0x04000F07 RID: 3847
		public float nextCheckTimer;
	}

	// Token: 0x020002DD RID: 733
	private class FireStepCheckRole
	{
		// Token: 0x04000F08 RID: 3848
		public RoleBase role;

		// Token: 0x04000F09 RID: 3849
		public float checkTimer;
	}
}

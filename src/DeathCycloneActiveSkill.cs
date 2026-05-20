using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002D6 RID: 726
public class DeathCycloneActiveSkill : AnimationOverrideActiveSkill
{
	// Token: 0x060010E9 RID: 4329 RVA: 0x0005F820 File Offset: 0x0005DA20
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, float rangeValue, float interval, float duration)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.attackRange = rangeValue;
		this.skillTime = 2.1666667f;
		this.checkTimer = this.skillTime - 0.625f;
		this.checkOffset = interval;
		base.LoadAnimatorController("Bundles/Animator/Frank_RPG_2Hand_Skill01_WhirlWind");
		this.attackRoleBase.UpdateAnimSpeed(1.2f);
	}

	// Token: 0x060010EA RID: 4330 RVA: 0x0005F89C File Offset: 0x0005DA9C
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

	// Token: 0x060010EB RID: 4331 RVA: 0x0005FA00 File Offset: 0x0005DC00
	protected override void UpdateSkill(float time)
	{
		base.UpdateSkill(time);
		if (this.attackRoleBase != null)
		{
			if (this.attackRoleBase.IsDead())
			{
				this.skillTime = -1f;
			}
			if (this.skillTime < this.checkTimer && this.skillTime > 0.5416666f)
			{
				if (this.effectTransform == null)
				{
					this.effectTransform = AssetManager.LoadPrefab(EffectDefine.WhirlwindSkill, null, true).transform;
					this.effectTransform.localPosition = this.attackRoleBase.MyTransform.position + new Vector3(0f, 1f, 0f);
					this.effectTransform.localRotation = Quaternion.identity;
					float num = this.attackRange / 3f;
					this.effectTransform.localScale = new Vector3(num, num, num);
				}
				PlayerBase playerBase = this.attackRoleBase as PlayerBase;
				if (playerBase != null)
				{
					playerBase.CharacterController.Move(time * 10f * playerBase.MyTransform.forward + Time.deltaTime * Vector3.down);
				}
			}
			if (this.effectTransform != null)
			{
				this.effectTransform.eulerAngles = new Vector3(0f, this.effectTransform.eulerAngles.y + time * 30f, 0f);
				this.effectTransform.localPosition = this.attackRoleBase.MyTransform.position + new Vector3(0f, 1.3f, 0f);
			}
		}
	}

	// Token: 0x060010EC RID: 4332 RVA: 0x0005FBA1 File Offset: 0x0005DDA1
	public override void Clear(int clearData)
	{
		base.Clear(clearData);
		if (this.effectTransform != null)
		{
			AssetManager.UnLoadPrefab(this.effectTransform.gameObject, false);
			this.effectTransform = null;
		}
	}

	// Token: 0x04000EDB RID: 3803
	protected float attackRange;

	// Token: 0x04000EDC RID: 3804
	private float checkTimer;

	// Token: 0x04000EDD RID: 3805
	private int checkNum;

	// Token: 0x04000EDE RID: 3806
	private float checkOffset;

	// Token: 0x04000EDF RID: 3807
	private Transform effectTransform;

	// Token: 0x04000EE0 RID: 3808
	private const float AniSpeed = 1.2f;
}

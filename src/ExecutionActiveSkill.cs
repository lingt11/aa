using System;
using Mirror;
using UnityEngine;

// Token: 0x020002D8 RID: 728
public class ExecutionActiveSkill : AnimationOverrideActiveSkill
{
	// Token: 0x060010F3 RID: 4339 RVA: 0x00060184 File Offset: 0x0005E384
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, ActiveSkillData activeSkillDataValue, int targetRoleId, int skillBookIdValue)
	{
		NetworkIdentity networkIdentity;
		if (NetworkClient.spawned.TryGetValue((uint)targetRoleId, out networkIdentity))
		{
			this.targetRole = networkIdentity.GetComponent<RoleBase>();
		}
		if (this.targetRole == null)
		{
			this.skillTime = -1f;
			return;
		}
		this.skillBookId = skillBookIdValue;
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = activeSkillDataValue;
		this.attackRoleBase = attackRole;
		float num = 1.5f;
		this.skillTime = 3.8f / num;
		this.checkTimer = this.skillTime * 0.28f;
		base.LoadAnimatorController("Bundles/Animator/core_main_riposte_01");
		this.attackRoleBase.UpdateAnimSpeed(num);
		if (this.attackRoleBase.hasAuthority)
		{
			this.attackRoleBase.roleBuffManager.AddOneBuff<Buff无敌>("Buff无敌", this.skillTime);
		}
	}

	// Token: 0x060010F4 RID: 4340 RVA: 0x0006024C File Offset: 0x0005E44C
	protected override void UpdateLocalSkill(float time)
	{
		if (this.attackRoleBase == null || this.targetRole == null)
		{
			return;
		}
		if (this.skillTime < this.checkTimer)
		{
			if (!this.isCheck)
			{
				this.isCheck = true;
				bool flag = this.attackRoleBase.roleType == RoleType.Enemy;
				long num = Util.GetSkillDamage(this.activeSkillData, this.attackRoleBase);
				num += ConstDefine.ClampBattleValue((double)this.targetRole.maxHp * 0.2);
				long num2 = this.targetRole.hp + this.targetRole.Shield;
				bool isAttackWeek = this.attackRoleBase.GetIsAttackWeek(AttackType.Skill);
				float num3;
				if (flag)
				{
					num3 = (float)this.targetRole.OnHit(this.attackRoleBase, (double)num, Util.GetV2Angle(this.targetRole.MyTransform.position, this.attackRoleBase.MyTransform.position), AttackType.Skill, isAttackWeek);
				}
				else
				{
					num3 = (float)Util.OnLocalPlayerHit(this.attackRoleBase, this.targetRole, (double)num, Util.GetV2Angle(this.targetRole.MyTransform.position, this.attackRoleBase.MyTransform.position), AttackType.Skill, isAttackWeek);
				}
				if (this.attackRoleBase is PlayerBase && num3 >= (float)num2)
				{
					int num4 = 2;
					if (this.targetRole.roleType == RoleType.Enemy && (this.targetRole as EnemyBase).isBoss)
					{
						num4 = 10;
					}
					this.attackRoleBase.AddSTA(num4);
					this.attackRoleBase.AddSTR(num4);
					this.attackRoleBase.AddAGI(num4);
					SkillBase skillByBookId = GameHelperClient.localPlayer.GetSkillByBookId(this.skillBookId);
					if (skillByBookId != null)
					{
						skillByBookId.totals[0] += num4;
					}
				}
				Game.CameraManager.ShakeCamera(0.2f, 0.3f, 15, false);
				return;
			}
		}
		else
		{
			PlayerBase playerBase = this.attackRoleBase as PlayerBase;
			if (playerBase != null)
			{
				Vector3 motion = Vector3.Lerp(this.attackRoleBase.MyTransform.position, this.targetRole.MyTransform.position - this.attackRoleBase.MyTransform.forward * (1f + this.attackRoleBase.RoleModeBase.addRange), time * 5f) - this.attackRoleBase.MyTransform.position;
				playerBase.CharacterController.Move(motion);
				return;
			}
			this.attackRoleBase.MyTransform.position = Vector3.Lerp(this.attackRoleBase.MyTransform.position, this.targetRole.MyTransform.position - this.attackRoleBase.MyTransform.forward * (1f + this.attackRoleBase.RoleModeBase.addRange), time * 5f);
		}
	}

	// Token: 0x04000EE8 RID: 3816
	private float checkTimer;

	// Token: 0x04000EE9 RID: 3817
	private RoleBase targetRole;

	// Token: 0x04000EEA RID: 3818
	private bool isCheck;

	// Token: 0x04000EEB RID: 3819
	private int skillBookId;
}

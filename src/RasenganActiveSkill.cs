using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002F2 RID: 754
public class RasenganActiveSkill : AnimationOverrideActiveSkill
{
	// Token: 0x06001163 RID: 4451 RVA: 0x00064F84 File Offset: 0x00063184
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, float rangeValue, float interval, float duration)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.attackRange = rangeValue;
		this.allTime = duration;
		this.checkTimer = 0.5f;
		this.skillTime = duration;
		this.checkOffset = interval;
		this.checkPos = pos;
		base.LoadAnimatorController("Bundles/Animator/RasenganSkill");
		this.attackRoleBase.UpdateAnimSpeed(1f);
		if (this.attackRoleBase.RoleModeBase.myAnim.isHuman)
		{
			this.handTransform = this.attackRoleBase.RoleModeBase.myAnim.GetBoneTransform(HumanBodyBones.RightHand);
		}
		else
		{
			this.handTransform = this.attackRoleBase.MyTransform;
		}
		this.effectTransform = AssetManager.LoadPrefab(EffectDefine.NaShenGan, null, true).transform;
		this.effectTransform.position = this.handTransform.position + new Vector3(0f, 1f, 0f);
		this.effectTransform.LookAt(new Vector3(this.effectTransform.position.x, 0f, this.effectTransform.position.z));
		this.effectTransform.localScale = rangeValue / 5f * Vector3.one;
	}

	// Token: 0x06001164 RID: 4452 RVA: 0x000650E4 File Offset: 0x000632E4
	protected override void UpdateSkill(float time)
	{
		base.UpdateSkill(time);
		if (this.skillTime > 0.5f)
		{
			if (this.skillTime < this.allTime - 0.5f)
			{
				if (!this.isStartFly)
				{
					this.isStartFly = true;
					this.startPos = this.effectTransform.position;
					Game.AudioManager.PlayAudioByPos("Audio/Battle_Audio/Skill/NaShenGan", this.startPos, 1f);
				}
				this.effectTransform.position = Vector3.Lerp(this.startPos, this.checkPos, (this.allTime - this.skillTime - 0.5f) / 1f);
			}
			else
			{
				this.effectTransform.position = this.handTransform.position + new Vector3(0f, 1f, 0f);
			}
		}
		else if (this.effectTransform != null)
		{
			AssetManager.UnLoadPrefab(this.effectTransform.gameObject, false);
			this.effectTransform = null;
		}
		if (this.skillTime < this.checkTimer - (float)this.checkNum * this.checkOffset)
		{
			this.checkNum++;
			if (!this.isCheck)
			{
				this.isCheck = true;
				Game.EffectManager.PlayEffect(EffectDefine.SmokeEffect, 3f, this.checkPos + new Vector3(0f, 0.5f, 0f), this.attackRange / 2f);
				if (this.attackRoleBase.HasAuthority)
				{
					Game.CameraManager.ShakeCameraByPos(this.checkPos, 0.1f, 0.75f, 15, false);
				}
			}
			if (this.attackRoleBase.HasAuthority)
			{
				bool isAttackWeek = this.attackRoleBase.GetIsAttackWeek(AttackType.Skill);
				bool flag = this.attackRoleBase.roleType == RoleType.Enemy;
				List<RoleBase> attackRoles = this.attackRoleBase.GetAttackRoles();
				int count = attackRoles.Count;
				for (int i = 0; i < count; i++)
				{
					RoleBase roleBase = attackRoles[i];
					if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckYuanXing(this.checkPos, roleBase.MyTransform.position, this.attackRange + roleBase.RoleModeBase.addRange, false))
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

	// Token: 0x06001165 RID: 4453 RVA: 0x000653AD File Offset: 0x000635AD
	public override void Clear(int clearData)
	{
		base.Clear(clearData);
		if (this.effectTransform != null)
		{
			AssetManager.UnLoadPrefab(this.effectTransform.gameObject, false);
			this.effectTransform = null;
		}
		this.handTransform = null;
	}

	// Token: 0x04000F7D RID: 3965
	protected float attackRange;

	// Token: 0x04000F7E RID: 3966
	private float checkTimer;

	// Token: 0x04000F7F RID: 3967
	private int checkNum;

	// Token: 0x04000F80 RID: 3968
	private float checkOffset;

	// Token: 0x04000F81 RID: 3969
	private Vector3 checkPos;

	// Token: 0x04000F82 RID: 3970
	private Transform effectTransform;

	// Token: 0x04000F83 RID: 3971
	private Vector3 startPos;

	// Token: 0x04000F84 RID: 3972
	private bool isCheck;

	// Token: 0x04000F85 RID: 3973
	private float allTime;

	// Token: 0x04000F86 RID: 3974
	private bool isStartFly;

	// Token: 0x04000F87 RID: 3975
	private Transform handTransform;
}

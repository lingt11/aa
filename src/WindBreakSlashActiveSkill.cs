using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002FF RID: 767
public class WindBreakSlashActiveSkill : AnimationOverrideActiveSkill
{
	// Token: 0x060011B4 RID: 4532 RVA: 0x00067A10 File Offset: 0x00065C10
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, float rangeValue)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.attackRange = rangeValue;
		this.skillTime = 3f;
		this.attackTimer = new float[]
		{
			this.skillTime - 0.44995502f,
			this.skillTime - 0.73326f,
			this.skillTime - 1.0665599f,
			this.skillTime - 1.43319f,
			this.skillTime - 1.6998299f,
			this.skillTime - 2.03313f
		};
		base.LoadAnimatorController("Bundles/Animator/Frank_RPG_2Hand_Combo03_All");
		this.skillEffectTran = AssetManager.LoadPrefab(EffectDefine.WindBreakSword, null, true).transform;
		this.skillEffectTran.localScale = Vector3.one * (this.attackRange / 12.5f);
		if (attackRole.RoleModeBase.myAnim.isHuman)
		{
			this.attackTransform = attackRole.RoleModeBase.myAnim.GetBoneTransform(HumanBodyBones.RightHand);
			return;
		}
		this.attackTransform = attackRole.MyTransform;
	}

	// Token: 0x060011B5 RID: 4533 RVA: 0x00067B34 File Offset: 0x00065D34
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
		if (this.skillTime < 0.75f)
		{
			return;
		}
		if (this.attackNormal < 0f)
		{
			if (this.checkRoleBases != null && this.checkRoleBases.Count > 0)
			{
				this.checkRoleBases.Clear();
			}
			this.isPlaySound = false;
			return;
		}
		if (this.checkRoleBases == null)
		{
			this.checkRoleBases = new List<RoleBase>();
		}
		bool flag = this.attackRoleBase.roleType == RoleType.Enemy;
		List<RoleBase> attackRoles = this.attackRoleBase.GetAttackRoles();
		int count = attackRoles.Count;
		long skillDamage = Util.GetSkillDamage(this.activeSkillData, this.attackRoleBase);
		bool isAttackWeek = this.attackRoleBase.GetIsAttackWeek(AttackType.Skill);
		for (int i = 0; i < count; i++)
		{
			RoleBase roleBase = attackRoles[i];
			if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && !this.checkRoleBases.Contains(roleBase))
			{
				Vector3 position = roleBase.MyTransform.position;
				Vector3 vector = this.attackRoleBase.MyTransform.position - this.attackRoleBase.MyTransform.forward * (this.attackRange * 0.15f);
				if (Util.NewCheckJuXing(vector, this.attackRoleBase.MyTransform.localEulerAngles.y - 270f + 180f - 360f * this.attackNormal, this.attackRange * 0.25f, this.attackRange, roleBase.MyTransform.position, roleBase.RoleModeBase.addRange, false, false))
				{
					this.checkRoleBases.Add(roleBase);
					if (flag)
					{
						roleBase.OnHit(this.attackRoleBase, (double)skillDamage, Util.GetV2Angle(position, vector), AttackType.Skill, isAttackWeek);
					}
					else
					{
						Util.OnLocalPlayerHit(this.attackRoleBase, roleBase, (double)skillDamage, Util.GetV2Angle(position, vector), AttackType.Skill, isAttackWeek);
					}
				}
			}
		}
		if (!this.isPlaySound)
		{
			Game.AudioManager.PlayAudioByPos("Audio/Battle_Audio/Skill/WindBreakSwordAttack", this.attackRoleBase.MyTransform.position, 1f);
			this.isPlaySound = true;
		}
	}

	// Token: 0x060011B6 RID: 4534 RVA: 0x00067DAC File Offset: 0x00065FAC
	protected override void UpdateSkill(float time)
	{
		base.UpdateSkill(time);
		if (this.attackRoleBase != null)
		{
			if (this.attackTransform != null)
			{
				this.skillEffectTran.position = this.attackTransform.position;
				int num = this.attackTimer.Length;
				this.attackNormal = -1f;
				for (int i = 0; i < num; i += 2)
				{
					float num2 = this.attackTimer[i];
					float num3 = this.attackTimer[i + 1];
					if (this.skillTime < num2 && this.skillTime > num3)
					{
						this.attackNormal = (num2 - this.skillTime) / (num2 - num3);
						break;
					}
				}
				if (this.attackNormal >= 0f)
				{
					this.skillEffectTran.localEulerAngles = new Vector3(0f, this.attackRoleBase.MyTransform.localEulerAngles.y - 235f - 360f * this.attackNormal, 0f);
				}
			}
			if (this.attackRoleBase.IsDead())
			{
				this.skillTime = -1f;
			}
		}
	}

	// Token: 0x060011B7 RID: 4535 RVA: 0x00067EB9 File Offset: 0x000660B9
	public override void Clear(int clearData)
	{
		base.Clear(clearData);
		if (this.skillEffectTran != null)
		{
			AssetManager.UnLoadPrefab(this.skillEffectTran.gameObject, false);
			this.skillEffectTran = null;
		}
	}

	// Token: 0x04000FD1 RID: 4049
	private float attackRange;

	// Token: 0x04000FD2 RID: 4050
	private Transform skillEffectTran;

	// Token: 0x04000FD3 RID: 4051
	private Transform attackTransform;

	// Token: 0x04000FD4 RID: 4052
	private float[] attackTimer;

	// Token: 0x04000FD5 RID: 4053
	private float attackNormal;

	// Token: 0x04000FD6 RID: 4054
	private List<RoleBase> checkRoleBases;

	// Token: 0x04000FD7 RID: 4055
	private bool isPlaySound;
}

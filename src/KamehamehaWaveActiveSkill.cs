using System;
using System.Collections.Generic;
using PolygonArsenal;
using UnityEngine;

// Token: 0x020002E4 RID: 740
public class KamehamehaWaveActiveSkill : AnimationOverrideActiveSkill
{
	// Token: 0x06001120 RID: 4384 RVA: 0x00062478 File Offset: 0x00060678
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, ActiveSkillData activeSkillData, float range)
	{
		this.activeSkillEnum = activeSkillType;
		this.attackRoleBase = attackRole;
		base.LoadAnimatorController("Bundles/Animator/龟派气功");
		this.attackRoleBase.UpdateAnimSpeed(1f);
		this.activeSkillData = activeSkillData;
		this.attackRange = range;
		this.skillTime = Util.GetRealSkillDuration(attackRole, activeSkillData.duration);
		this.checkTimer = this.skillTime - 1f;
		this.checkOffset = activeSkillData.interval;
	}

	// Token: 0x06001121 RID: 4385 RVA: 0x000624F0 File Offset: 0x000606F0
	protected override void UpdateLocalSkill(float time)
	{
		if (this.attackRoleBase == null)
		{
			return;
		}
		if (this.attackRoleBase.isLocalPlayer)
		{
			float v2Angle = Util.GetV2Angle(Game.EffectManager.GetMouseGroundPos(), this.attackRoleBase.MyTransform.position);
			this.attackRoleBase.MyTransform.eulerAngles = new Vector3(0f, v2Angle, 0f);
		}
		else if (this.attackRoleBase.trackRoleBase != null)
		{
			float v2Angle2 = Util.GetV2Angle(this.attackRoleBase.trackRoleBase.MyTransform.position, this.attackRoleBase.MyTransform.position);
			this.attackRoleBase.MyTransform.eulerAngles = new Vector3(0f, v2Angle2, 0f);
		}
		if (!this.isLoadEffect)
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
				if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckYuanXing(this.polygonBeamStatic.BeamEndTransform.position, roleBase.MyTransform.position, this.attackRange + roleBase.RoleModeBase.addRange, false))
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

	// Token: 0x06001122 RID: 4386 RVA: 0x00062704 File Offset: 0x00060904
	protected override void UpdateSkill(float time)
	{
		base.UpdateSkill(time);
		if (this.attackRoleBase != null)
		{
			if (this.skillTime < 0.75f)
			{
				this.checkTimer = -1f;
				this.isLoadEffect = false;
				if (this.effectTransform != null)
				{
					AssetManager.UnLoadPrefab(this.effectTransform.gameObject, false);
					this.effectTransform = null;
				}
				this.attackRoleBase.UpdateAnimSpeed(1f);
			}
			else
			{
				if (this.attackRoleBase.RoleModeBase.myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.78f)
				{
					this.attackRoleBase.UpdateAnimSpeed(0f);
				}
				if (this.skillTime < this.checkTimer && !this.isLoadEffect)
				{
					this.isLoadEffect = true;
					this.effectTransform = AssetManager.LoadPrefab(EffectDefine.KamehamehaWave, null, true).transform;
					this.effectTransform.localScale = new Vector3(this.attackRange * 1.5f, this.attackRange, this.attackRange);
					this.effectTransform.SetParent(this.attackRoleBase.transform);
					this.effectTransform.localPosition = new Vector3(0f, this.attackRoleBase.GetAttackPos().y * 1.25f, 0.6f + this.attackRoleBase.RoleModeBase.addRange);
					this.effectTransform.localRotation = Quaternion.identity;
					this.polygonBeamStatic = this.effectTransform.gameObject.GetComponent<PolygonBeamStatic>();
					this.polygonBeamStatic.InitRadius(0.5f);
				}
			}
			if (this.attackRoleBase.IsDead())
			{
				this.skillTime = -1f;
			}
			if (GameHelperClient.localPlayer != null && GameHelperClient.localPlayer.netId != this.attackRoleBase.netId)
			{
				if (Util.GetV2Distance(GameHelperClient.localPlayer.MyTransform.position, this.attackRoleBase.MyTransform.position) > 22f)
				{
					if (this.effectTransform != null && this.effectTransform.gameObject.activeSelf)
					{
						this.effectTransform.gameObject.SetActive(false);
					}
					if (this.polygonBeamStatic != null && this.polygonBeamStatic.BeamStart != null && this.polygonBeamStatic.BeamStart.activeSelf)
					{
						this.polygonBeamStatic.BeamStart.SetActive(false);
					}
					if (this.polygonBeamStatic != null && this.polygonBeamStatic.BeamEnd != null && this.polygonBeamStatic.BeamEnd.activeSelf)
					{
						this.polygonBeamStatic.BeamEnd.SetActive(false);
						return;
					}
				}
				else
				{
					if (this.effectTransform != null && !this.effectTransform.gameObject.activeSelf)
					{
						this.effectTransform.gameObject.SetActive(true);
					}
					if (this.polygonBeamStatic != null && this.polygonBeamStatic.BeamStart != null && !this.polygonBeamStatic.BeamStart.activeSelf)
					{
						this.polygonBeamStatic.BeamStart.SetActive(true);
					}
					if (this.polygonBeamStatic != null && this.polygonBeamStatic.BeamEnd != null && !this.polygonBeamStatic.BeamEnd.activeSelf)
					{
						this.polygonBeamStatic.BeamEnd.SetActive(true);
					}
				}
			}
		}
	}

	// Token: 0x06001123 RID: 4387 RVA: 0x00062A93 File Offset: 0x00060C93
	public override void Clear(int clearData)
	{
		base.Clear(clearData);
		if (this.effectTransform != null)
		{
			AssetManager.UnLoadPrefab(this.effectTransform.gameObject, false);
			this.effectTransform = null;
		}
	}

	// Token: 0x04000F26 RID: 3878
	private float attackRange;

	// Token: 0x04000F27 RID: 3879
	private float checkOffset;

	// Token: 0x04000F28 RID: 3880
	private int checkNum;

	// Token: 0x04000F29 RID: 3881
	private float checkTimer;

	// Token: 0x04000F2A RID: 3882
	private Transform effectTransform;

	// Token: 0x04000F2B RID: 3883
	private PolygonBeamStatic polygonBeamStatic;

	// Token: 0x04000F2C RID: 3884
	private bool isLoadEffect;
}

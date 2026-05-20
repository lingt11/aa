using System;
using UnityEngine;

// Token: 0x02000263 RID: 611
public class EnemyForestGuardianMode : EnemyTrajectoryMode
{
	// Token: 0x06000B34 RID: 2868 RVA: 0x0003A2E8 File Offset: 0x000384E8
	public override void OnClientInitEnemy()
	{
		base.OnClientInitEnemy();
		if (this.roleBase.hasAuthority)
		{
			int value = Random.Range(0, 2);
			this.roleBase.CmdUpdateModeData(value);
		}
		if (!this.isInit)
		{
			this.isInit = true;
			RoleBase roleBase = this.roleBase;
			roleBase.damageEvent = (RoleBase.DamageEnemy)Delegate.Combine(roleBase.damageEvent, new RoleBase.DamageEnemy(this.DamageEvent));
		}
	}

	// Token: 0x06000B35 RID: 2869 RVA: 0x0003A352 File Offset: 0x00038552
	public override void OnRemove()
	{
		base.OnRemove();
		if (this.isInit)
		{
			this.isInit = false;
			RoleBase roleBase = this.roleBase;
			roleBase.damageEvent = (RoleBase.DamageEnemy)Delegate.Remove(roleBase.damageEvent, new RoleBase.DamageEnemy(this.DamageEvent));
		}
	}

	// Token: 0x06000B36 RID: 2870 RVA: 0x0003A390 File Offset: 0x00038590
	private float DamageEvent(RoleBase attackRole, RoleBase hurtRole, AttackType attackType, ref float damage)
	{
		if ((this.forestGuardianSkillType == EnemyForestGuardianMode.ForestGuardianSkillType.Normal && attackType == AttackType.Normal) || (this.forestGuardianSkillType == EnemyForestGuardianMode.ForestGuardianSkillType.Skill && attackType == AttackType.Skill))
		{
			float time = Time.time;
			if (time > this.playEffectCd)
			{
				Game.EffectManager.PlayEffect(EffectDefine.Healing, 1.5f, this.enemyBase.GetAttackPos(), 1f).SetParent(this.enemyBase.MyTransform);
				this.playEffectCd = time + 0.3f;
			}
			GameHelperClient.localPlayer.StartHealthHp((double)damage, this.enemyBase);
			damage = 0f;
		}
		return damage;
	}

	// Token: 0x06000B37 RID: 2871 RVA: 0x0003A424 File Offset: 0x00038624
	public override void OnUpdateModeData(int value)
	{
		base.OnUpdateModeData(value);
		this.forestGuardianSkillType = (EnemyForestGuardianMode.ForestGuardianSkillType)value;
	}

	// Token: 0x06000B38 RID: 2872 RVA: 0x0003A434 File Offset: 0x00038634
	public override void OnStartSkill()
	{
		base.OnStartSkill();
		if (this.forestGuardianSkillType == EnemyForestGuardianMode.ForestGuardianSkillType.Normal)
		{
			this.forestGuardianSkillType = EnemyForestGuardianMode.ForestGuardianSkillType.Skill;
			this.normalShield.gameObject.SetActive(false);
			this.skillShield.gameObject.SetActive(true);
			return;
		}
		this.forestGuardianSkillType = EnemyForestGuardianMode.ForestGuardianSkillType.Normal;
		this.normalShield.gameObject.SetActive(true);
		this.skillShield.gameObject.SetActive(false);
	}

	// Token: 0x06000B39 RID: 2873 RVA: 0x0003A4A4 File Offset: 0x000386A4
	public override void UpdateSkill1()
	{
		if (!this.roleBase.hasAuthority)
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		this.roleBase.timer += deltaTime;
		if (this.roleBase.timer > 1.2f / this.roleBase.AniSpeed)
		{
			this.roleBase.UpdateRoleState(RoleState.Idle);
		}
	}

	// Token: 0x06000B3A RID: 2874 RVA: 0x00039EFE File Offset: 0x000380FE
	public override void OnExitSkill()
	{
		base.OnExitSkill();
	}

	// Token: 0x06000B3B RID: 2875 RVA: 0x0003A504 File Offset: 0x00038704
	public override void OnStartSkill2()
	{
		this.roleBase.timer = 0f;
		this.roleBase.PlayAni(AnimDefine.Skill2, 1f, 0.1f);
		Game.AudioManager.PlaySkillAudio(this.skillSoundType, this.roleBase.MyTransform.position);
		this.checkIndex = 0;
		this.roleBase.isCheckAttack = false;
	}

	// Token: 0x06000B3C RID: 2876 RVA: 0x0003A570 File Offset: 0x00038770
	public override void UpdateSkill2()
	{
		if (!this.roleBase.hasAuthority)
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		this.roleBase.timer += deltaTime;
		if (this.roleBase.timer < 2.466667f)
		{
			this.roleBase.TrackRotation(3f);
		}
		else if (this.roleBase.timer < 2.766667f)
		{
			if (!this.roleBase.isCheckAttack)
			{
				this.roleBase.isCheckAttack = true;
				this.skill2Rotation = this.roleBase.MyTransform.eulerAngles.y + 45f;
			}
			this.enemyBase.oldRotation = this.enemyBase.MyTransform.localEulerAngles.y;
			this.enemyBase.PingHuaZhuanShen(this.skill2Rotation, 2f);
		}
		else if (this.roleBase.timer < 4.6555557f)
		{
			this.enemyBase.SetRotationY(this.skill2Rotation + -47.64706f * (this.roleBase.timer - 2.766667f));
		}
		if (this.roleBase.timer > 2.766667f + (float)this.checkIndex * 0.25f && this.roleBase.timer < 4.6555557f)
		{
			this.checkIndex++;
			Vector3 pos = this.createTransform.position - this.createTransform.forward * 1.75f;
			pos.y = 1.25f;
			this.roleBase.CmdCreateSkill(ActiveSkillEnum.GuardianBullet, pos, this.enemyBase.MyTransform.eulerAngles.y, -1, 0);
		}
		if (this.roleBase.timer > 6.1000004f / this.roleBase.AniSpeed)
		{
			this.roleBase.UpdateRoleState(RoleState.Idle);
		}
	}

	// Token: 0x06000B3D RID: 2877 RVA: 0x0003A74E File Offset: 0x0003894E
	public override void OnExitSkill2()
	{
		base.OnExitSkill2();
	}

	// Token: 0x04000C16 RID: 3094
	private const float Skill2Time = 6.1000004f;

	// Token: 0x04000C17 RID: 3095
	private const float Skill2AttackTime = 2.766667f;

	// Token: 0x04000C18 RID: 3096
	private const float Skill2EndTime = 4.6555557f;

	// Token: 0x04000C19 RID: 3097
	private EnemyForestGuardianMode.ForestGuardianSkillType forestGuardianSkillType;

	// Token: 0x04000C1A RID: 3098
	[SerializeField]
	private GameObject normalShield;

	// Token: 0x04000C1B RID: 3099
	[SerializeField]
	private GameObject skillShield;

	// Token: 0x04000C1C RID: 3100
	private bool isInit;

	// Token: 0x04000C1D RID: 3101
	private float playEffectCd = -1f;

	// Token: 0x04000C1E RID: 3102
	private int checkIndex;

	// Token: 0x04000C1F RID: 3103
	private float skill2Rotation;

	// Token: 0x02000264 RID: 612
	private enum ForestGuardianSkillType
	{
		// Token: 0x04000C21 RID: 3105
		Normal,
		// Token: 0x04000C22 RID: 3106
		Skill
	}
}

using System;
using UnityEngine;

// Token: 0x02000273 RID: 627
public class EnemySaiYaDark : EnemyMeleeMode
{
	// Token: 0x06000B9C RID: 2972 RVA: 0x0003EA30 File Offset: 0x0003CC30
	public override void OnClientInitEnemy()
	{
		base.OnClientInitEnemy();
		this.enemyAttackOffset = this.enemyBase.GetRealAttackOffset();
		this.enemyBase.doge = 0;
		this.skill1Effect.gameObject.SetActive(false);
		this.enemyBase.deadMoveSpeed = 0.75f;
		this.enemyBase.XuanYunImmunity = true;
		if (this.enemyBase.damageEvent == null)
		{
			EnemyBase enemyBase = this.enemyBase;
			enemyBase.damageEvent = (RoleBase.DamageEnemy)Delegate.Combine(enemyBase.damageEvent, new RoleBase.DamageEnemy(this.DamageEvent));
		}
		this.isUseSkill1 = false;
		this.enemyBase.reduceInjury = 1000;
		if (this.roleBase.hasAuthority)
		{
			this.roleBase.SetWuDi(true);
		}
		Game.UI.GetUI<UI_PlayerState>().ShowTalkUI(this.enemyBase, new string[]
		{
			Game.Language.Get("Talk_Ghost_Relife_0", "")
		}, 5f);
	}

	// Token: 0x06000B9D RID: 2973 RVA: 0x0003EB28 File Offset: 0x0003CD28
	public override void OnStartShowPose()
	{
		this.roleBase.timer = 0f;
		this.roleBase.PlayAni(AnimDefine.ShowPose, 0.75f);
	}

	// Token: 0x06000B9E RID: 2974 RVA: 0x0003EB50 File Offset: 0x0003CD50
	public override void UpdateShowPose()
	{
		float deltaTime = Time.deltaTime;
		this.roleBase.timer += deltaTime;
		if (this.roleBase.hasAuthority && this.roleBase.timer > 1.9106666f)
		{
			this.roleBase.UpdateRoleState(RoleState.Idle);
		}
	}

	// Token: 0x06000B9F RID: 2975 RVA: 0x00039E65 File Offset: 0x00038065
	public override void OnExitShowPose()
	{
		base.OnExitShowPose();
	}

	// Token: 0x06000BA0 RID: 2976 RVA: 0x0003EBA4 File Offset: 0x0003CDA4
	public override void OnStartSkill()
	{
		this.roleBase.timer = 0f;
		this.roleBase.PlayAni(AnimDefine.Skill, 1f, 0.1f);
		Game.AudioManager.PlayAudioByPos("Audio/Battle_Audio/Skill/SaiYaDarkCast", this.roleBase.MyTransform.position, 1f);
		this.enemyBase.isCheckAttack = false;
		RoleBase trackRoleBase = this.enemyBase.trackRoleBase;
		this.skill1Effect.gameObject.SetActive(true);
		this.skill1Effect.localPosition = new Vector3(0f, 1f, 0f);
		this.playerShake = false;
		this.isUseSkill1 = true;
		if (trackRoleBase != null)
		{
			trackRoleBase.XuanYun(3.8f);
		}
	}

	// Token: 0x06000BA1 RID: 2977 RVA: 0x0003EC6C File Offset: 0x0003CE6C
	public override void UpdateSkill1()
	{
		float deltaTime = Time.deltaTime;
		this.enemyBase.timer += deltaTime;
		if (this.enemyBase.timer > 0.55f && this.enemyBase.timer < 0.8f)
		{
			this.skill1Effect.localPosition = new Vector3(0f, 1f, (this.enemyBase.timer - 0.55f) / 0.25f * 1.8f);
		}
		if (!this.enemyBase.hasAuthority)
		{
			return;
		}
		if (this.enemyBase.timer > 3.8f)
		{
			this.enemyBase.UpdateRoleState(RoleState.Run);
			return;
		}
		this.enemyBase.TrackRotation(2f);
		RoleBase trackRoleBase = this.enemyBase.trackRoleBase;
		if (trackRoleBase != null)
		{
			if (trackRoleBase.hasAuthority)
			{
				float v2Angle = trackRoleBase.GetV2Angle(this.enemyBase.MyTransform.position);
				trackRoleBase.oldRotation = trackRoleBase.MyTransform.localEulerAngles.y;
				trackRoleBase.PingHuaZhuanShen(v2Angle, 2f);
			}
			this.enemyBase.MyTransform.position = Vector3.Lerp(this.enemyBase.MyTransform.position, trackRoleBase.MyTransform.position - this.enemyBase.MyTransform.forward * (1.8f + this.enemyBase.RoleModeBase.addRange), Time.deltaTime * 3f);
			if (!this.enemyBase.isCheckAttack && this.enemyBase.timer > 0.75f)
			{
				this.enemyBase.isCheckAttack = true;
				Game.AudioManager.PlayAudioByPos("Audio/Battle_Audio/Skill/SaiYaDarkSkill1", this.roleBase.MyTransform.position, 1f);
			}
		}
		if (!this.playerShake && this.enemyBase.timer > 1.45f)
		{
			this.playerShake = true;
			Game.CameraManager.ShakeCameraByPos(this.enemyBase.MyTransform.position, 0.25f, 0.3f, 20, false);
			GameHelperClient.localPlayer.CmdAddBuff(trackRoleBase.netId, this.enemyBase.netId, LocalBuffType.SaiYaDark, 0.5f, 9999f, 1);
		}
	}

	// Token: 0x06000BA2 RID: 2978 RVA: 0x0003EEAC File Offset: 0x0003D0AC
	public override void OnExitSkill()
	{
		base.OnExitSkill();
		this.skill1Effect.gameObject.SetActive(false);
		if (this.roleBase.hasAuthority)
		{
			this.roleBase.SetWuDi(false);
		}
	}

	// Token: 0x06000BA3 RID: 2979 RVA: 0x0003EEE0 File Offset: 0x0003D0E0
	public void OnUpdateSaiYaDarkBuff(SaiYaDarkBuff.ReData data)
	{
		this.enemyBase.mAttackPower += data.reAttack + data.reStr;
		this.enemyBase.attackSpeed += data.reAttackSpeed + (float)data.reAgi * 0.002f;
		this.enemyBase.AddMoveSpeed(data.reMoveSpeed);
		this.enemyBase.doge += data.dodge;
		this.enemyAttackOffset = this.enemyBase.GetRealAttackOffset();
		if (this.enemyBase.hasAuthority)
		{
			float num = 1f - Util.GetArmorLevel(data.reArmor);
			int num2 = (data.reMaxHp + data.reSta * 10) * 100;
			long num3 = this.enemyBase.maxHp + (long)num2;
			if (num > 0f)
			{
				this.enemyBase.CmdUpdateShield((long)Mathf.RoundToInt(num * 10f * (float)num3));
			}
			this.enemyBase.CmdUpdateMaxHp((long)num2, this.enemyBase.netId);
		}
	}

	// Token: 0x06000BA4 RID: 2980 RVA: 0x0003EFEC File Offset: 0x0003D1EC
	public override void OnStartSkill2()
	{
		this.roleBase.timer = 0f;
		this.roleBase.PlayAni(AnimDefine.Skill2, 1.5f, 0.1f);
		Game.AudioManager.PlaySkillAudio(this.skillSoundType, this.roleBase.MyTransform.position);
		Game.AudioManager.PlayAudioByPos("Audio/Battle_Audio/Skill/SaiYaDarkCast", this.roleBase.MyTransform.position, 1f);
		this.enemyBase.isCheckAttack = false;
	}

	// Token: 0x06000BA5 RID: 2981 RVA: 0x0003F074 File Offset: 0x0003D274
	public override void UpdateSkill2()
	{
		base.UpdateSkill2();
		if (!this.enemyBase.hasAuthority)
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		this.enemyBase.timer += deltaTime;
		if (this.enemyBase.timer > 1.2f / this.enemyBase.AniSpeed)
		{
			this.enemyBase.UpdateRoleState(RoleState.Idle);
			return;
		}
		if (this.enemyBase.timer > 1.2f / this.enemyBase.AniSpeed * 0.38f && !this.enemyBase.isCheckAttack)
		{
			this.enemyBase.isCheckAttack = true;
			this.enemyBase.CmdCreateSkill(ActiveSkillEnum.SaiyaCall, this.enemyBase.MyTransform.position, 0f, -1, 0);
		}
	}

	// Token: 0x06000BA6 RID: 2982 RVA: 0x0003F140 File Offset: 0x0003D340
	public override void OnStartDead()
	{
		this.roleBase.SetWuDi(false);
		this.roleBase.PlayAni(GameHelperClient.isReady ? AnimDefine.Idle : AnimDefine.Dead, 1f, 0.1f);
		if (!GameHelperClient.isReady)
		{
			Game.AudioManager.PlayDeadAudio(this.deadSound, this.roleBase.MyTransform.position);
		}
	}

	// Token: 0x06000BA7 RID: 2983 RVA: 0x0003F1A8 File Offset: 0x0003D3A8
	private float DamageEvent(RoleBase attackrole, RoleBase hurtrole, AttackType attackType, ref float f)
	{
		float num = (float)hurtrole.maxHp * 0.04f;
		if (f > num)
		{
			f = num;
		}
		return f;
	}

	// Token: 0x06000BA8 RID: 2984 RVA: 0x0003F1D0 File Offset: 0x0003D3D0
	public override void UpdateEvent()
	{
		base.UpdateEvent();
		if (this.isUseSkill1 && this.enemyBase.hasAuthority && this.enemyBase.RoleState != RoleState.Skill && this.enemyBase.RoleState != RoleState.ShowPose && this.enemyBase.wudi)
		{
			this.enemyBase.SetWuDi(false);
		}
	}

	// Token: 0x04000C7D RID: 3197
	private const float Skill1Time = 3.8f;

	// Token: 0x04000C7E RID: 3198
	[SerializeField]
	private Transform skill1Effect;

	// Token: 0x04000C7F RID: 3199
	private bool playerShake;

	// Token: 0x04000C80 RID: 3200
	private bool isUseSkill1;
}

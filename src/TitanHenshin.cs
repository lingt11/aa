using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000284 RID: 644
public class TitanHenshin : PlayerModeBase
{
	// Token: 0x06000C04 RID: 3076 RVA: 0x000427F4 File Offset: 0x000409F4
	private void OnAnimatorMove()
	{
		if (this.myAnim.applyRootMotion && this.playerBase.HasAuthority)
		{
			this.playerBase.CharacterController.Move(this.myAnim.deltaPosition);
			if (EntityStatic.Get<CameraManager>() != null)
			{
				Game.CameraManager.TryEndAttackCameraFollowLimitByDistance(this.playerBase.MyTransform.position, this.attackCameraMaxFollowDistance);
			}
		}
	}

	// Token: 0x06000C05 RID: 3077 RVA: 0x0004285F File Offset: 0x00040A5F
	public override void OnStartSkill()
	{
		base.OnStartSkill();
		this.myAnim.applyRootMotion = false;
	}

	// Token: 0x06000C06 RID: 3078 RVA: 0x00042873 File Offset: 0x00040A73
	public override void OnExitSkill()
	{
		base.OnExitSkill();
		this.myAnim.applyRootMotion = true;
	}

	// Token: 0x06000C07 RID: 3079 RVA: 0x00042888 File Offset: 0x00040A88
	public override void AttackUpdate()
	{
		if (!this.myAnim.GetAnimatorTransitionInfo(0).anyState)
		{
			float normalizedTime = this.myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;
			if (normalizedTime > 0.9f)
			{
				this.playerBase.UpdateRoleState(RoleState.Idle);
				return;
			}
			if (normalizedTime <= 0.9f)
			{
				if (normalizedTime > 0.48f)
				{
					if (!this.playerBase.isCheckAttack)
					{
						this.playerBase.isCheckAttack = true;
						Vector3 vector = this.playerBase.MyTransform.position + this.playerBase.MyTransform.forward * 2.5f;
						float num = 5f * (1f + this.playerBase.skillRange);
						Game.EffectManager.PlayEffect(EffectDefine.SmokeEffect, 3f, vector + new Vector3(0f, 0.5f, 0f), num / 2f);
						if (this.playerBase.hasAuthority)
						{
							List<RoleBase> attackRoles = this.roleBase.GetAttackRoles();
							int count = attackRoles.Count;
							bool isAttackWeek = this.playerBase.GetIsAttackWeek(AttackType.Normal);
							long num2 = this.playerBase.GetPlayerNormalAttackPower() * 5L;
							for (int i = 0; i < count; i++)
							{
								RoleBase roleBase = attackRoles[i];
								if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckYuanXing(vector, roleBase.MyTransform.position, num + roleBase.RoleModeBase.addRange, false))
								{
									Util.OnLocalPlayerHit(this.playerBase, roleBase, (double)num2 * (1.0 + (double)this.playerBase.addHenshin), Util.GetV2Angle(vector, roleBase.MyTransform.position), AttackType.Normal, isAttackWeek);
								}
							}
							return;
						}
					}
				}
				else if (this.playerBase.hasAuthority)
				{
					this.playerBase.TrackRotation(1.5f);
					return;
				}
			}
		}
		else if (this.playerBase.hasAuthority)
		{
			this.playerBase.TrackRotation(1.5f);
		}
	}

	// Token: 0x06000C08 RID: 3080 RVA: 0x00042AB0 File Offset: 0x00040CB0
	public override void OnStartAttack()
	{
		base.OnStartAttack();
		this.playerBase.isCheckAttack = false;
		if (this.playerBase.hasAuthority)
		{
			if (EntityStatic.Get<CameraManager>() != null)
			{
				Game.CameraManager.BeginAttackCameraFollowLimit(this.attackCameraFollowSpeed, this.attackCameraReturnSmoothTime, this.attackCameraMaxLimitTime);
			}
			Game.AudioManager.PlaySkillAudio(SkillSoundType.Boss, this.playerBase.MyTransform.position);
		}
	}

	// Token: 0x06000C09 RID: 3081 RVA: 0x00042B1A File Offset: 0x00040D1A
	public override void OnExitAttack()
	{
		base.OnExitAttack();
		if (this.playerBase.hasAuthority && EntityStatic.Get<CameraManager>() != null)
		{
			Game.CameraManager.EndAttackCameraFollowLimit();
		}
	}

	// Token: 0x06000C0A RID: 3082 RVA: 0x00042B40 File Offset: 0x00040D40
	public override void OnInitMode()
	{
		base.OnInitMode();
		if (this.roleBase.hasAuthority)
		{
			this.addMaxHp = ConstDefine.ClampBattleValue((double)this.roleBase.maxHp * (1.0 + (double)this.playerBase.addHenshin));
			this.roleBase.CmdUpdateMaxHp(this.addMaxHp, this.roleBase.netId);
			this.roleBase.StartHealthHp((double)((float)this.roleBase.maxHp * 2f), this.roleBase);
		}
	}

	// Token: 0x06000C0B RID: 3083 RVA: 0x00042BD0 File Offset: 0x00040DD0
	public override void OnClearMode()
	{
		base.OnClearMode();
		if (this.playerBase.hasAuthority && EntityStatic.Get<CameraManager>() != null)
		{
			Game.CameraManager.EndAttackCameraFollowLimit();
		}
		if (this.roleBase.hasAuthority)
		{
			this.roleBase.CmdUpdateMaxHp(-this.addMaxHp, this.roleBase.netId);
		}
	}

	// Token: 0x04000CD6 RID: 3286
	[SerializeField]
	private float attackCameraFollowSpeed;

	// Token: 0x04000CD7 RID: 3287
	[SerializeField]
	private float attackCameraReturnSmoothTime = 0.25f;

	// Token: 0x04000CD8 RID: 3288
	[SerializeField]
	private float attackCameraMaxLimitTime = 4f;

	// Token: 0x04000CD9 RID: 3289
	[SerializeField]
	private float attackCameraMaxFollowDistance = 10f;

	// Token: 0x04000CDA RID: 3290
	public long addMaxHp;
}

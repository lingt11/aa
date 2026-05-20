using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000267 RID: 615
public class EnemyGoblinGeneralMode : EnemyMeleeMode
{
	// Token: 0x06000B47 RID: 2887 RVA: 0x00039DB8 File Offset: 0x00037FB8
	public override void OnClientInitEnemy()
	{
		base.OnClientInitEnemy();
	}

	// Token: 0x06000B48 RID: 2888 RVA: 0x0003AD38 File Offset: 0x00038F38
	public override void UpdateSkill1()
	{
		float deltaTime = Time.deltaTime;
		this.enemyBase.timer += deltaTime;
		if (!this.enemyBase.isCheckAttack && this.enemyBase.timer > 1.6520001f)
		{
			this.enemyBase.isCheckAttack = true;
			this.effectTransform = AssetManager.LoadPrefab(EffectDefine.WhirlwindSkill, null, true).transform;
			this.effectTransform.localPosition = this.enemyBase.MyTransform.position + new Vector3(0f, 1.65f, 0f);
			this.effectTransform.localRotation = Quaternion.identity;
			float num = 1.6666666f;
			this.effectTransform.localScale = new Vector3(num, num, num);
			this.effectTransform.SetParent(this.enemyBase.MyTransform);
		}
		else if (this.enemyBase.timer > 3.276f && this.effectTransform != null)
		{
			AssetManager.UnLoadPrefab(this.effectTransform.gameObject, false);
			this.effectTransform = null;
		}
		if (this.enemyBase.timer > 0.9f && !this.isPlayEffect)
		{
			this.isPlayEffect = true;
			Game.EffectManager.PlayTipLine(this.enemyBase.MyTransform.position - this.enemyBase.MyTransform.forward * 5f, new Vector3(10f, 1f, 23f), this.enemyBase.MyTransform.localEulerAngles.y, 0.8f);
		}
		if (!this.enemyBase.hasAuthority)
		{
			return;
		}
		if (this.enemyBase.timer > 3.776f)
		{
			if (this.enemyBase.hasAuthority)
			{
				this.enemyBase.UpdateRoleState(RoleState.Run);
				return;
			}
		}
		else if (this.enemyBase.timer < 3.276f && this.enemyBase.timer > 1.6520001f)
		{
			if (this.enemyBase.timer > this.checkTimer)
			{
				this.checkTimer += 0.1f;
				List<RoleBase> attackRoles = this.roleBase.GetAttackRoles();
				int count = attackRoles.Count;
				Vector3 position = this.enemyBase.MyTransform.position;
				for (int i = 0; i < count; i++)
				{
					RoleBase roleBase = attackRoles[i];
					if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckYuanXing(position, roleBase.MyTransform.position, 5f + roleBase.RoleModeBase.addRange, false))
					{
						roleBase.OnHit(this.enemyBase, (double)((float)this.enemyBase.FinalAttackPower * 0.8f), Util.GetV2Angle(roleBase.MyTransform.position, position), AttackType.Skill, false);
					}
				}
			}
			if (this.enemyBase.timer < 2.776f)
			{
				Vector3 saveMapPos = Util.GetSaveMapPos(this.enemyBase.MyTransform.position + this.enemyBase.MyTransform.forward * (12f * deltaTime));
				this.enemyBase.MyTransform.position = saveMapPos;
				return;
			}
		}
		else if (this.enemyBase.timer < 0.8f)
		{
			this.enemyBase.TrackRotation(2f);
		}
	}

	// Token: 0x06000B49 RID: 2889 RVA: 0x0003B0B0 File Offset: 0x000392B0
	public override void OnStartSkill()
	{
		this.enemyBase.timer = 0f;
		this.enemyBase.PlayAni(AnimDefine.Skill, 1f, 0.1f);
		Game.AudioManager.PlaySkillAudio(this.skillSoundType, this.enemyBase.MyTransform.position);
		this.checkTimer = 1.6520001f;
		this.enemyBase.isCheckAttack = false;
		this.isPlayEffect = false;
	}

	// Token: 0x06000B4A RID: 2890 RVA: 0x0003B125 File Offset: 0x00039325
	public override void OnExitSkill()
	{
		base.OnExitSkill();
		if (this.effectTransform != null)
		{
			AssetManager.UnLoadPrefab(this.effectTransform.gameObject, false);
			this.effectTransform = null;
		}
	}

	// Token: 0x06000B4B RID: 2891 RVA: 0x0003B154 File Offset: 0x00039354
	public override void OnStartSkill2()
	{
		this.enemyBase.timer = 0f;
		this.enemyBase.PlayAni(AnimDefine.Skill2, 0.3f, 0.05f);
		Game.AudioManager.PlaySkillAudio(this.skillSoundType, this.enemyBase.MyTransform.position);
		this.checkTimer = 1f;
		this.enemyBase.isCheckAttack = false;
		this.skill2Index = 0;
		this.skill2TipIndex = 0;
		this.skill2EffectIndex = 0;
		if (this.enemyBase.hasAuthority)
		{
			this.myAnim.applyRootMotion = true;
		}
		this.skill2TipEffect = null;
	}

	// Token: 0x06000B4C RID: 2892 RVA: 0x0003B1F7 File Offset: 0x000393F7
	public override void OnExitSkill2()
	{
		base.OnExitSkill();
		if (this.enemyBase.hasAuthority)
		{
			this.myAnim.applyRootMotion = false;
		}
	}

	// Token: 0x06000B4D RID: 2893 RVA: 0x0003B218 File Offset: 0x00039418
	public override void UpdateSkill2()
	{
		base.UpdateSkill2();
		AnimatorTransitionInfo animatorTransitionInfo = this.myAnim.GetAnimatorTransitionInfo(0);
		AnimatorStateInfo currentAnimatorStateInfo = this.myAnim.GetCurrentAnimatorStateInfo(0);
		if (!animatorTransitionInfo.anyState && currentAnimatorStateInfo.shortNameHash == AnimDefine.Skill2)
		{
			float normalizedTime = currentAnimatorStateInfo.normalizedTime;
			if (normalizedTime <= 0.09f)
			{
				this.Track(0.75f);
				if (this.skill2TipIndex == 0)
				{
					this.skill2TipEffect = Game.EffectManager.PlayTipSector(this.enemyBase.MyTransform.position, 16f, this.enemyBase.MyTransform.localEulerAngles.y, 90f, 4.217f / this.enemyBase.AniSpeed * (0.09f - normalizedTime) + 4.217f * (this.skill2CheckTime[0] - 0.09f), 0f);
					this.skill2TipIndex++;
				}
			}
			else if (normalizedTime > 0.09f && normalizedTime <= 0.15f)
			{
				this.enemyBase.UpdateAnimSpeed(1f);
				if (this.skill2EffectIndex == 0)
				{
					this.skill2EffectIndex++;
					Vector3 pos = this.enemyBase.MyTransform.position + this.enemyBase.MyTransform.forward;
					pos.y = 1f;
					Game.EffectManager.PlayEffect(EffectDefine.GeneralSwordSlash, 4f, pos, Vector3.one * 2.8f, new Vector3(0f, this.enemyBase.MyTransform.localEulerAngles.y, 180f));
				}
			}
			else if (normalizedTime > 0.15f && normalizedTime <= 0.25f)
			{
				this.enemyBase.UpdateAnimSpeed(0.5f);
				this.Track(0.75f);
				if (this.skill2TipIndex == 1)
				{
					this.skill2TipEffect = Game.EffectManager.PlayTipSector(this.enemyBase.MyTransform.position, 16f, this.enemyBase.MyTransform.localEulerAngles.y, 90f, 0.8434f + 4.217f * (this.skill2CheckTime[1] - 0.25f), 0f);
					this.skill2TipIndex++;
				}
			}
			else if (normalizedTime > 0.25f && normalizedTime <= 0.32f)
			{
				this.enemyBase.UpdateAnimSpeed(1f);
				if (this.skill2EffectIndex == 1)
				{
					this.skill2EffectIndex++;
					Vector3 pos2 = this.enemyBase.MyTransform.position + this.enemyBase.MyTransform.forward;
					pos2.y = 1f;
					Game.EffectManager.PlayEffect(EffectDefine.GeneralSwordSlash, 4f, pos2, Vector3.one * 2.8f, new Vector3(0f, this.enemyBase.MyTransform.localEulerAngles.y, 0f));
				}
			}
			else if (normalizedTime > 0.32f && normalizedTime <= 0.42f)
			{
				this.enemyBase.UpdateAnimSpeed(0.5f);
				this.Track(0.2f);
				if (this.skill2TipIndex == 2)
				{
					this.skill2TipEffect = Game.EffectManager.PlayTipLine(this.enemyBase.MyTransform.position, new Vector3(4f, 1f, 15f), this.enemyBase.MyTransform.localEulerAngles.y, 0.8434f + 4.217f * (this.skill2CheckTime[2] - 0.42f));
					this.skill2TipIndex++;
				}
			}
			else if (normalizedTime > 0.42f && normalizedTime <= 0.5f)
			{
				this.enemyBase.UpdateAnimSpeed(1f);
				if (this.skill2EffectIndex == 2 && normalizedTime > 0.44f)
				{
					this.skill2EffectIndex++;
					Vector3 pos3 = this.enemyBase.MyTransform.position + this.enemyBase.MyTransform.forward * 6.5f;
					pos3.y = 0.15f;
					Game.EffectManager.PlayEffect(EffectDefine.GeneralSkill2Effect3, 3f, pos3, Vector3.one * 1.5f, new Vector3(0f, this.enemyBase.MyTransform.localEulerAngles.y, 0f));
				}
			}
			else if (normalizedTime > 0.5f && normalizedTime <= 0.6f)
			{
				this.enemyBase.UpdateAnimSpeed(0.5f);
				this.Track(0.55f);
				if (this.skill2TipIndex == 3)
				{
					this.skill2TipEffect = Game.EffectManager.PlayTipSector(this.enemyBase.MyTransform.position + this.enemyBase.MyTransform.forward * 4f, 14f, this.enemyBase.MyTransform.localEulerAngles.y, 180f, 0.8434f + 4.217f * (this.skill2CheckTime[3] - 0.6f), 0f);
					this.skill2TipIndex++;
				}
			}
			else if (normalizedTime > 0.6f)
			{
				this.enemyBase.UpdateAnimSpeed(1f);
			}
			if (normalizedTime < this.skill2CheckTime[3] && this.skill2TipEffect != null)
			{
				Vector3 vector = this.enemyBase.MyTransform.position;
				vector.y = 0.35f;
				if (this.skill2TipIndex == 3)
				{
					this.skill2TipEffect.transform.position = vector;
					this.skill2TipEffect.transform.localEulerAngles = new Vector3(this.skill2TipEffect.transform.localEulerAngles.x, this.enemyBase.MyTransform.localEulerAngles.y, this.skill2TipEffect.transform.localEulerAngles.z);
				}
				else if (this.skill2TipIndex == 4)
				{
					vector += this.enemyBase.MyTransform.forward * 4f;
					this.skill2TipEffect.transform.position = vector;
				}
				else
				{
					this.skill2TipEffect.transform.position = vector;
					this.skill2TipEffect.transform.localEulerAngles = new Vector3(this.skill2TipEffect.transform.localEulerAngles.x, this.enemyBase.MyTransform.localEulerAngles.y + 180f, this.skill2TipEffect.transform.localEulerAngles.z);
				}
			}
			if (this.enemyBase.hasAuthority && normalizedTime > 1f)
			{
				this.enemyBase.UpdateRoleState(RoleState.Run);
			}
			if (this.skill2Index < this.skill2CheckTime.Length && normalizedTime > this.skill2CheckTime[this.skill2Index])
			{
				List<RoleBase> attackRoles = this.enemyBase.GetAttackRoles();
				int count = attackRoles.Count;
				if (this.skill2Index < 2)
				{
					if (this.enemyBase.hasAuthority)
					{
						for (int i = 0; i < count; i++)
						{
							RoleBase roleBase = attackRoles[i];
							if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckShanXing(this.enemyBase.MyTransform.position, roleBase.MyTransform.position, 180f, 8f + roleBase.RoleModeBase.addRange, this.enemyBase.MyTransform.eulerAngles.y, false))
							{
								roleBase.OnHit(this.enemyBase, (double)(this.enemyBase.FinalAttackPower * 3L), Util.GetV2Angle(roleBase.MyTransform.position, this.enemyBase.MyTransform.position), AttackType.Skill, false);
							}
						}
					}
					this.skill2Index++;
					return;
				}
				if (this.skill2Index == 2)
				{
					Game.CameraManager.ShakeCameraByPos(this.enemyBase.MyTransform.position, 0.1f, 0.5f, 15, false);
					if (this.enemyBase.hasAuthority)
					{
						for (int j = 0; j < count; j++)
						{
							RoleBase roleBase2 = attackRoles[j];
							if (roleBase2 != null && roleBase2.gameObject.activeSelf && !roleBase2.IsDead() && Util.NewCheckJuXing(this.enemyBase.MyTransform.position, this.enemyBase.MyTransform.eulerAngles.y, 4f, 15f, roleBase2.MyTransform.position, roleBase2.RoleModeBase.addRange, false, false))
							{
								roleBase2.OnHit(this.enemyBase, (double)(this.enemyBase.FinalAttackPower * 4L), Util.GetV2Angle(roleBase2.MyTransform.position, this.enemyBase.MyTransform.position), AttackType.Skill, false);
							}
						}
					}
					this.skill2Index++;
					return;
				}
				if (this.skill2Index == 3)
				{
					Vector3 vector2 = this.enemyBase.MyTransform.position + this.enemyBase.MyTransform.forward * 4f;
					Game.CameraManager.ShakeCameraByPos(vector2, 0.15f, 0.75f, 15, false);
					Game.EffectManager.PlayEffect(EffectDefine.SmokeEffect, 3f, vector2 + new Vector3(0f, 0.5f, 0f), 3.5f);
					this.skill2Index++;
					if (this.enemyBase.hasAuthority)
					{
						for (int k = 0; k < count; k++)
						{
							RoleBase roleBase3 = attackRoles[k];
							if (roleBase3 != null && roleBase3.gameObject.activeSelf && !roleBase3.IsDead() && Util.NewCheckYuanXing(vector2, roleBase3.MyTransform.position, 7f + roleBase3.RoleModeBase.addRange, false))
							{
								roleBase3.OnHit(this.enemyBase, (double)(this.enemyBase.FinalAttackPower * 5L), Util.GetV2Angle(roleBase3.MyTransform.position, this.enemyBase.MyTransform.position), AttackType.Skill, false);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000B4E RID: 2894 RVA: 0x0003BCE8 File Offset: 0x00039EE8
	private void Track(float level)
	{
		if (this.enemyBase.hasAuthority)
		{
			if (this.enemyBase.trackRoleBase == null)
			{
				this.enemyBase.GetTrackRole(false, 15f, false, false);
			}
			this.enemyBase.TrackRotation(0.65f * level);
		}
	}

	// Token: 0x06000B4F RID: 2895 RVA: 0x0003BD3C File Offset: 0x00039F3C
	private void OnAnimatorMove()
	{
		if (this.myAnim.applyRootMotion)
		{
			Vector3 saveMapPos = Util.GetSaveMapPos(this.enemyBase.MyTransform.position + this.myAnim.deltaPosition * 1.5f);
			this.enemyBase.MyTransform.position = saveMapPos;
		}
	}

	// Token: 0x04000C29 RID: 3113
	private const float Skill1Time = 3.776f;

	// Token: 0x04000C2A RID: 3114
	private const float Skill1Range = 5f;

	// Token: 0x04000C2B RID: 3115
	private Transform effectTransform;

	// Token: 0x04000C2C RID: 3116
	private float checkTimer;

	// Token: 0x04000C2D RID: 3117
	private bool isPlayEffect;

	// Token: 0x04000C2E RID: 3118
	private const float Skill2AllTime = 4.217f;

	// Token: 0x04000C2F RID: 3119
	private const float Skill2ChargeSpeed = 0.5f;

	// Token: 0x04000C30 RID: 3120
	private const float Skill2Range1 = 8f;

	// Token: 0x04000C31 RID: 3121
	private const float Skill2Range3W = 4f;

	// Token: 0x04000C32 RID: 3122
	private const float Skill2Range3H = 15f;

	// Token: 0x04000C33 RID: 3123
	private const float Skill2Range4 = 7f;

	// Token: 0x04000C34 RID: 3124
	private readonly float[] skill2CheckTime = new float[]
	{
		0.12f,
		0.275f,
		0.455f,
		0.625f
	};

	// Token: 0x04000C35 RID: 3125
	private int skill2Index;

	// Token: 0x04000C36 RID: 3126
	private int skill2TipIndex;

	// Token: 0x04000C37 RID: 3127
	private int skill2EffectIndex;

	// Token: 0x04000C38 RID: 3128
	private TipEffect skill2TipEffect;
}

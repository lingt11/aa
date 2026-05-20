using System;
using UnityEngine;

// Token: 0x02000275 RID: 629
public class EnemySkeletonSoldierMode : EnemyDualSwordMode
{
	// Token: 0x06000BB0 RID: 2992 RVA: 0x0003F5E8 File Offset: 0x0003D7E8
	public override void OnClientInitEnemy()
	{
		base.OnClientInitEnemy();
		this.isRelife = false;
		this.weaponL.SetParent(this.weaponBackL);
		this.weaponL.localPosition = Vector3.zero;
		this.weaponL.localRotation = Quaternion.identity;
		this.weaponR.SetParent(this.weaponBackR);
		this.weaponR.localPosition = Vector3.zero;
		this.weaponR.localRotation = Quaternion.identity;
	}

	// Token: 0x06000BB1 RID: 2993 RVA: 0x0003F664 File Offset: 0x0003D864
	public override void OnStartShowPose()
	{
		base.OnStartShowPose();
		this.roleBase.isCheckAttack = false;
		Game.UI.GetUI<UI_PlayerState>().ShowTalkUI(this.roleBase, new string[]
		{
			Game.Language.Get("Talk_SkeletonSoldier_Start_1", ""),
			Game.Language.Get("Talk_SkeletonSoldier_Start_2", ""),
			Game.Language.Get("Talk_SkeletonSoldier_Start_3", ""),
			Game.Language.Get("Talk_SkeletonSoldier_Start_4", "")
		}, 5f);
	}

	// Token: 0x06000BB2 RID: 2994 RVA: 0x00002D1D File Offset: 0x00000F1D
	public override void UpdateShowPose()
	{
	}

	// Token: 0x06000BB3 RID: 2995 RVA: 0x00039E65 File Offset: 0x00038065
	public override void OnExitShowPose()
	{
		base.OnExitShowPose();
	}

	// Token: 0x06000BB4 RID: 2996 RVA: 0x0003F700 File Offset: 0x0003D900
	public override void OnStartDead()
	{
		if (this.isRelife)
		{
			base.OnStartDead();
			if (this.isRegister)
			{
				this.isRegister = false;
				MySystemEvent.Instance.UnregisterMessage<RoleBase>(45, new Action<Body, RoleBase>(this.OnUseSkill));
				return;
			}
		}
		else
		{
			this.isRelife = true;
			Game.AudioManager.PlayDeadAudio(this.deadSound, this.roleBase.MyTransform.position);
			Game.UI.GetUI<UI_PlayerState>().ShowTalkUI(this.roleBase, new string[]
			{
				Game.Language.Get("Talk_SkeletonSoldier_Relife_1", ""),
				Game.Language.Get("Talk_SkeletonSoldier_Relife_2", ""),
				Game.Language.Get("Talk_SkeletonSoldier_Relife_3", ""),
				Game.Language.Get("Talk_SkeletonSoldier_Relife_4", "")
			}, 5f);
			if (this.roleBase.hasAuthority)
			{
				this.roleBase.CmdRelifeByState(RoleState.Skill);
				this.roleBase.CmdUpdateShield(this.roleBase.maxHp * 100L);
			}
		}
	}

	// Token: 0x06000BB5 RID: 2997 RVA: 0x0003F81C File Offset: 0x0003DA1C
	public override void OnStartSkill()
	{
		if (this.enemyBase.CheckCollider != null)
		{
			this.enemyBase.CheckCollider.enabled = true;
		}
		Game.EnemyManagerClient.CreatAgent(this.enemyBase);
		this.roleBase.timer = 0f;
		this.roleBase.isCheckAttack = false;
		this.roleBase.PlayAni(AnimDefine.Active, 1f, 0.1f);
		Game.UI.GetUI<UI_PlayerState>().ShowTalkUI(this.roleBase, new string[]
		{
			Game.Language.Get("Talk_SkeletonSoldier_Relife_1", ""),
			Game.Language.Get("Talk_SkeletonSoldier_Relife_2", ""),
			Game.Language.Get("Talk_SkeletonSoldier_Relife_3", ""),
			Game.Language.Get("Talk_SkeletonSoldier_Relife_4", "")
		}, 5f);
	}

	// Token: 0x06000BB6 RID: 2998 RVA: 0x0003F910 File Offset: 0x0003DB10
	public override void UpdateSkill1()
	{
		float deltaTime = Time.deltaTime;
		this.roleBase.timer += deltaTime;
		if (this.roleBase.timer > 2f)
		{
			if (!this.roleBase.isCheckAttack)
			{
				this.roleBase.isCheckAttack = true;
				this.weaponR.SetParent(this.weaponNodeR);
				this.weaponL.SetParent(this.weaponNodeL);
			}
			this.weaponR.localRotation = Quaternion.Lerp(this.weaponR.localRotation, Quaternion.identity, deltaTime * 5f);
			this.weaponR.localPosition = Vector3.Lerp(this.weaponR.localPosition, Vector3.zero, deltaTime * 5f);
			this.weaponL.localRotation = Quaternion.Lerp(this.weaponL.localRotation, Quaternion.identity, deltaTime * 5f);
			this.weaponL.localPosition = Vector3.Lerp(this.weaponL.localPosition, Vector3.zero, deltaTime * 5f);
		}
		if (this.roleBase.hasAuthority && this.roleBase.timer > 2.8f)
		{
			this.roleBase.UpdateRoleState(RoleState.Run);
		}
	}

	// Token: 0x06000BB7 RID: 2999 RVA: 0x0003FA50 File Offset: 0x0003DC50
	public override void OnExitSkill()
	{
		base.OnExitSkill();
		this.weaponL.localPosition = Vector3.zero;
		this.weaponL.localRotation = Quaternion.identity;
		this.weaponR.localPosition = Vector3.zero;
		this.weaponR.localRotation = Quaternion.identity;
		if (this.roleBase.hasAuthority)
		{
			this.ResetSkill();
			if (!this.isRegister)
			{
				this.isRegister = true;
				MySystemEvent.Instance.RegisterMessage<RoleBase>(45, new Action<Body, RoleBase>(this.OnUseSkill));
			}
		}
	}

	// Token: 0x06000BB8 RID: 3000 RVA: 0x0003FAE0 File Offset: 0x0003DCE0
	private void ResetSkill()
	{
		AIAttackCheck aiattackCheck = this.aiAttackChecks[0];
		this.enemyBase.SetAttackCd(0, aiattackCheck.attackCd);
	}

	// Token: 0x06000BB9 RID: 3001 RVA: 0x0003FB0C File Offset: 0x0003DD0C
	private void OnUseSkill(Body body, RoleBase role)
	{
		if (this.enemyBase.hasAuthority && role.roleType == RoleType.Player && role.GetDistanceV2(this.enemyBase.MyTransform.position) < 13.5f)
		{
			this.enemyBase.trackRoleBase = role;
			this.skill2Offset = (role.MyTransform.position - this.enemyBase.MyTransform.position).normalized;
			this.ResetSkill();
			this.enemyBase.UpdateRoleState(RoleState.Skill2);
		}
	}

	// Token: 0x06000BBA RID: 3002 RVA: 0x0003FB98 File Offset: 0x0003DD98
	public override void OnStartSkill2()
	{
		this.roleBase.timer = 0f;
		this.roleBase.PlayAni(AnimDefine.Skill2, 1f, 0.1f);
		Game.AudioManager.PlaySkillAudio(this.skillSoundType, this.roleBase.MyTransform.position);
	}

	// Token: 0x06000BBB RID: 3003 RVA: 0x0003FBF0 File Offset: 0x0003DDF0
	public override void UpdateSkill2()
	{
		base.UpdateSkill2();
		if (this.enemyBase.hasAuthority)
		{
			if (this.enemyBase.trackRoleBase != null)
			{
				this.enemyBase.MyTransform.position = Vector3.Lerp(this.enemyBase.MyTransform.position, this.enemyBase.trackRoleBase.MyTransform.position + this.skill2Offset * 1.5f, Time.deltaTime * 5f);
			}
			this.roleBase.timer += Time.deltaTime;
			if (this.roleBase.timer > 0.5f)
			{
				this.roleBase.timer = this.enemyBase.GetRealAttackOffset();
				this.roleBase.UpdateRoleState(RoleState.Skill3);
			}
		}
	}

	// Token: 0x06000BBC RID: 3004 RVA: 0x0003A74E File Offset: 0x0003894E
	public override void OnExitSkill2()
	{
		base.OnExitSkill2();
	}

	// Token: 0x06000BBD RID: 3005 RVA: 0x0003FCCC File Offset: 0x0003DECC
	private void OnAnimatorMove()
	{
		if (this.myAnim.applyRootMotion && this.enemyBase.RoleState == RoleState.Skill3)
		{
			Vector3 saveMapPos = Util.GetSaveMapPos(this.enemyBase.MyTransform.position + this.myAnim.deltaPosition * this.rootMotionLevel);
			this.enemyBase.MyTransform.position = saveMapPos;
		}
	}

	// Token: 0x06000BBE RID: 3006 RVA: 0x0003FD38 File Offset: 0x0003DF38
	public override void OnStartSkill3()
	{
		this.roleBase.timer = 0f;
		this.roleBase.PlayAni(AnimDefine.Skill3, 1f, 0.1f);
		Game.AudioManager.PlaySkillAudio(this.skillSoundType, this.roleBase.MyTransform.position);
		this.skill2EffectIndex = 0;
		if (this.roleBase.hasAuthority)
		{
			this.rootMotionLevel = 1.5f;
			this.myAnim.applyRootMotion = true;
		}
	}

	// Token: 0x06000BBF RID: 3007 RVA: 0x0003FDBC File Offset: 0x0003DFBC
	public override void UpdateSkill3()
	{
		this.roleBase.timer += Time.deltaTime;
		AnimatorTransitionInfo animatorTransitionInfo = this.myAnim.GetAnimatorTransitionInfo(0);
		AnimatorStateInfo currentAnimatorStateInfo = this.myAnim.GetCurrentAnimatorStateInfo(0);
		if (this.roleBase.timer < 0.35f)
		{
			this.roleBase.TrackRotation(3.5f);
		}
		else if (this.skill2EffectIndex == 0)
		{
			this.skill2EffectIndex = 1;
			Game.EffectManager.PlayTipLine(this.enemyBase.MyTransform.position, new Vector3(5f, 1f, 8f), this.enemyBase.MyTransform.localEulerAngles.y, 0.5f);
		}
		if (!animatorTransitionInfo.anyState)
		{
			float normalizedTime = currentAnimatorStateInfo.normalizedTime;
			if (this.enemyBase.hasAuthority && normalizedTime > 1f && currentAnimatorStateInfo.shortNameHash == AnimDefine.Skill3End)
			{
				this.enemyBase.UpdateRoleState(RoleState.Run);
			}
			if (currentAnimatorStateInfo.shortNameHash == AnimDefine.Skill3_3)
			{
				if (normalizedTime < 0.488f)
				{
					if (this.roleBase.hasAuthority)
					{
						this.roleBase.TrackRotation(3.5f);
					}
				}
				else if (this.skill2EffectIndex == 1)
				{
					this.skill2EffectIndex = 2;
					this.rootMotionLevel = 4f;
					Game.EffectManager.PlayTipLine(this.enemyBase.MyTransform.position, new Vector3(5f, 1f, 10f), this.enemyBase.MyTransform.localEulerAngles.y, 0.5f);
				}
			}
			if (currentAnimatorStateInfo.shortNameHash == AnimDefine.Skill3End && this.enemyBase.hasAuthority && normalizedTime > 0.6f)
			{
				this.rootMotionLevel = 1f;
			}
		}
	}

	// Token: 0x06000BC0 RID: 3008 RVA: 0x0003FF83 File Offset: 0x0003E183
	public override void OnExitSkill3()
	{
		if (this.roleBase.hasAuthority)
		{
			this.myAnim.applyRootMotion = false;
		}
	}

	// Token: 0x04000C88 RID: 3208
	private bool isRelife;

	// Token: 0x04000C89 RID: 3209
	[SerializeField]
	private Transform weaponNodeL;

	// Token: 0x04000C8A RID: 3210
	[SerializeField]
	private Transform weaponNodeR;

	// Token: 0x04000C8B RID: 3211
	[SerializeField]
	private Transform weaponBackL;

	// Token: 0x04000C8C RID: 3212
	[SerializeField]
	private Transform weaponBackR;

	// Token: 0x04000C8D RID: 3213
	[SerializeField]
	private Transform weaponL;

	// Token: 0x04000C8E RID: 3214
	[SerializeField]
	private Transform weaponR;

	// Token: 0x04000C8F RID: 3215
	private bool isRegister;

	// Token: 0x04000C90 RID: 3216
	private Vector3 skill2Offset;

	// Token: 0x04000C91 RID: 3217
	private int skill2EffectIndex;

	// Token: 0x04000C92 RID: 3218
	private TipEffect skill2TipEffect;

	// Token: 0x04000C93 RID: 3219
	private float rootMotionLevel;
}

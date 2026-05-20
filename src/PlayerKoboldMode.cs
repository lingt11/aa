using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200029D RID: 669
public class PlayerKoboldMode : MeleePlayerMode
{
	// Token: 0x17000098 RID: 152
	// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x0005401D File Offset: 0x0005221D
	// (set) Token: 0x06000EB1 RID: 3761 RVA: 0x00054025 File Offset: 0x00052225
	public int SkillLevel
	{
		get
		{
			return this.skillLevel;
		}
		set
		{
			this.skillLevel = value;
		}
	}

	// Token: 0x17000099 RID: 153
	// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x0005402E File Offset: 0x0005222E
	public bool IsUseSkillAttack
	{
		get
		{
			return this.isUseSkillAttack;
		}
	}

	// Token: 0x06000EB3 RID: 3763 RVA: 0x00054036 File Offset: 0x00052236
	public void StartSkill()
	{
		if (this.isUseSkillAttack)
		{
			return;
		}
		this.StartUseSkill();
	}

	// Token: 0x06000EB4 RID: 3764 RVA: 0x00054047 File Offset: 0x00052247
	public override void OnStartAttack()
	{
		if (this.isUseSkillAttack)
		{
			this.StartUseSkillAttack();
			return;
		}
		base.OnStartAttack();
	}

	// Token: 0x06000EB5 RID: 3765 RVA: 0x00054060 File Offset: 0x00052260
	public override void AttackUpdate()
	{
		float deltaTime = Time.deltaTime;
		this.playerBase.timer += deltaTime;
		float realOffsetInAttack = this.playerBase.GetRealOffsetInAttack();
		if (this.playerBase.timer > realOffsetInAttack)
		{
			if (this.playerBase.hasAuthority)
			{
				this.playerBase.UpdateRoleState(RoleState.Idle);
				return;
			}
		}
		else
		{
			float num = this.playerBase.timer / realOffsetInAttack;
			if (!this.playerBase.isCheckAttack && num > 0.35f)
			{
				this.playerBase.isCheckAttack = true;
				if (this.playerBase.hasAuthority && this.playerBase.trackRoleBase != null)
				{
					bool isAttackWeek = this.playerBase.GetIsAttackWeek(AttackType.Normal);
					long num2 = this.playerBase.GetPlayerNormalAttackPower();
					if (this.isUseSkillAttack)
					{
						num2 = (long)Mathf.RoundToInt((float)num2 * (2f + (float)this.skillLevel * 0.005f));
					}
					long num3 = this.playerBase.trackRoleBase.hp + this.playerBase.trackRoleBase.Shield;
					long num4 = Util.OnLocalPlayerHit(this.playerBase, this.playerBase.trackRoleBase, (double)num2, this.playerBase.MyTransform.eulerAngles.y, AttackType.Normal, isAttackWeek);
					if (this.isUseSkillAttack && num4 >= num3)
					{
						this.OnSkillKillEnemy(this.playerBase.trackRoleBase);
					}
					if (this.playerBase.attackNum > 1)
					{
						List<RoleBase> canAttackRoleList = this.playerBase.GetCanAttackRoleList(base.GetAttackDistance() + 0.5f, this.playerBase.attackNum);
						if (canAttackRoleList.Count > 0)
						{
							int i = 0;
							int count = canAttackRoleList.Count;
							while (i < count)
							{
								RoleBase roleBase = canAttackRoleList[i];
								num3 = roleBase.hp + roleBase.Shield;
								num4 = Util.OnLocalPlayerHit(this.playerBase, roleBase, (double)num2, this.playerBase.MyTransform.eulerAngles.y, AttackType.Normal, isAttackWeek);
								if (this.isUseSkillAttack && num4 >= num3)
								{
									this.OnSkillKillEnemy(roleBase);
								}
								i++;
							}
						}
					}
				}
				this.EndUseSkill();
			}
			if (this.playerBase.hasAuthority)
			{
				if (num < 0.5f && num < 0.3f)
				{
					this.playerBase.TrackRotation(3f);
				}
				if (this.playerBase.CheckIsInputMove(num) && !this.playerBase.isCheckAttack)
				{
					this.playerBase.timer = realOffsetInAttack;
				}
			}
		}
	}

	// Token: 0x06000EB6 RID: 3766 RVA: 0x000542DC File Offset: 0x000524DC
	private void OnSkillKillEnemy(RoleBase hitEnemy)
	{
		if (hitEnemy.roleType == RoleType.Enemy)
		{
			EnemyBase enemyBase = hitEnemy as EnemyBase;
			if (enemyBase != null)
			{
				if (enemyBase.isBoss)
				{
					this.UpdateSkillLevel(75);
					return;
				}
				if (enemyBase.isElite)
				{
					this.UpdateSkillLevel(12);
					return;
				}
				this.UpdateSkillLevel(3);
			}
		}
	}

	// Token: 0x06000EB7 RID: 3767 RVA: 0x00054325 File Offset: 0x00052525
	private void UpdateSkillLevel(int updateValue)
	{
		this.skillLevel += updateValue;
		this.skillLevelBuff.SetSpecialStr(this.skillLevel.ToString());
	}

	// Token: 0x06000EB8 RID: 3768 RVA: 0x0005434C File Offset: 0x0005254C
	private void StartUseSkill()
	{
		this.isUseSkillAttack = true;
		this.heroSkillEffect.SetActive(true);
		if (this.roleBase.isLocalPlayer)
		{
			if (this.skillLevelBuff == null)
			{
				this.skillLevelBuff = GameHelperClient.AddShowBuff(Game.Language.Get(PathDefine.Concat("a_", 512), ""), Game.Language.Get(PathDefine.Concat("a_", 512), ""), "Skill/SoulDevourer_buff", -1f);
				this.skillLevelBuff.SetSpecialStr(this.skillLevel.ToString());
			}
			Game.AudioManager.PlaySkillAudio(this.skillSoundType, this.roleBase.MyTransform.position);
			RoleBuff roleBuff = this.attackSkillBuff;
		}
		if (this.roleBase.RoleState == RoleState.Attack)
		{
			if (this.roleBase.isLocalPlayer && (this.playerBase.trackRoleBase == null || this.playerBase.trackRoleBase.IsDead()))
			{
				this.playerBase.trackRoleBase = this.playerBase.GetTrackRole(base.GetAttackDistance() + 0.5f);
				if (this.playerBase.trackRoleBase == null)
				{
					return;
				}
			}
			this.roleBase.ResetAnim();
			this.StartUseSkillAttack();
		}
	}

	// Token: 0x06000EB9 RID: 3769 RVA: 0x000544A4 File Offset: 0x000526A4
	private void StartUseSkillAttack()
	{
		this.roleBase.timer = 0f;
		this.roleBase.PlayAni(AnimDefine.Skill2, this.roleBase.syncAttackSpeed, 0.1f);
		this.roleBase.isCheckAttack = false;
		if (this.roleBase.hasAuthority)
		{
			RoleBase.OnStartAttackEvent onStartAttackEvent = this.roleBase.onStartAttackEvent;
			if (onStartAttackEvent != null)
			{
				onStartAttackEvent(this.roleBase.trackRoleBase, this.roleBase.GetRealAttackOffset());
			}
		}
		if (this.playerBase.hasAuthority)
		{
			Game.AudioManager.PlayAttackAudio(this.attackHitSound);
		}
	}

	// Token: 0x06000EBA RID: 3770 RVA: 0x00054544 File Offset: 0x00052744
	private void EndUseSkill()
	{
		this.heroSkillEffect.SetActive(false);
		this.isUseSkillAttack = false;
		if (this.roleBase.isLocalPlayer && this.attackSkillBuff != null)
		{
			GameHelperClient.localPlayer.roleBuffManager.RemoveBuff(this.attackSkillBuff);
			this.attackSkillBuff = null;
		}
	}

	// Token: 0x04000DB1 RID: 3505
	private int skillLevel;

	// Token: 0x04000DB2 RID: 3506
	private RoleBuff attackSkillBuff;

	// Token: 0x04000DB3 RID: 3507
	private bool isUseSkillAttack;

	// Token: 0x04000DB4 RID: 3508
	[SerializeField]
	private GameObject heroSkillEffect;

	// Token: 0x04000DB5 RID: 3509
	private RoleBuff skillLevelBuff;
}

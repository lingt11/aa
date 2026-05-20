using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002A8 RID: 680
public class WarGreymonMode : MeleePlayerMode
{
	// Token: 0x06000EE5 RID: 3813 RVA: 0x00055FA3 File Offset: 0x000541A3
	public override void OnStartAttack()
	{
		base.OnStartAttack();
	}

	// Token: 0x06000EE6 RID: 3814 RVA: 0x00055FAC File Offset: 0x000541AC
	public override void OnStartSkill2()
	{
		this.playerBase.PlayAni(AnimDefine.Skill2, Mathf.Max(0.8f, this.roleBase.syncAttackSpeed / 2f), 0.1f);
		this.playerBase.timer = 0f;
		this.checkAttackTime = 0.3f / this.playerBase.AniSpeed;
		if (this.playerBase.hasAuthority)
		{
			this.myAnim.applyRootMotion = true;
		}
		this.skillTransform.gameObject.SetActive(true);
		this.skillTransform.localScale = Vector3.one * (1f + this.playerBase.skillRange);
	}

	// Token: 0x06000EE7 RID: 3815 RVA: 0x00056060 File Offset: 0x00054260
	public override void UpdateSkill2()
	{
		if (this.playerBase.hasAuthority)
		{
			float deltaTime = Time.deltaTime;
			this.playerBase.timer += deltaTime;
			if (this.playerBase.timer < 0.7f / this.playerBase.AniSpeed && this.playerBase.timer > this.checkAttackTime)
			{
				this.checkAttackTime += 0.05f / this.playerBase.AniSpeed;
				List<RoleBase> attackRoles = this.playerBase.GetAttackRoles();
				int count = attackRoles.Count;
				bool isAttackWeek = this.playerBase.GetIsAttackWeek(AttackType.Normal);
				for (int i = 0; i < count; i++)
				{
					RoleBase roleBase = attackRoles[i];
					if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckJuXing(this.playerBase.MyTransform.position, this.playerBase.MyTransform.localEulerAngles.y, 3f * (1f + this.playerBase.skillRange), 4f * (1f + this.playerBase.skillRange), roleBase.MyTransform.position, roleBase.RoleModeBase.addRange, true, false))
					{
						long num = this.playerBase.GetPlayerNormalAttackPower() * 2L;
						Util.OnLocalPlayerHit(this.playerBase, roleBase, (double)num, Util.GetV2Angle(roleBase.MyTransform.position, this.playerBase.MyTransform.position), AttackType.Normal, isAttackWeek);
					}
				}
			}
			if (this.playerBase.timer > 1.3333334f / this.playerBase.AniSpeed)
			{
				this.playerBase.UpdateRoleState(RoleState.Idle);
			}
		}
	}

	// Token: 0x06000EE8 RID: 3816 RVA: 0x00056237 File Offset: 0x00054437
	private void OnAnimatorMove()
	{
		if (this.myAnim.applyRootMotion)
		{
			this.playerBase.CharacterController.Move(this.myAnim.deltaPosition * 0.65f);
		}
	}

	// Token: 0x06000EE9 RID: 3817 RVA: 0x0005626C File Offset: 0x0005446C
	public override void OnExitSkill2()
	{
		base.OnExitSkill2();
		if (this.playerBase.hasAuthority)
		{
			this.myAnim.applyRootMotion = false;
		}
		this.skillTransform.gameObject.SetActive(false);
	}

	// Token: 0x06000EEA RID: 3818 RVA: 0x000562A0 File Offset: 0x000544A0
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
			if (!this.playerBase.isCheckAttack && num > 0.35f && this.playerBase.trackRoleBase != null)
			{
				this.playerBase.isCheckAttack = true;
				bool isAttackWeek = this.playerBase.GetIsAttackWeek(AttackType.Normal);
				long playerNormalAttackPower = this.playerBase.GetPlayerNormalAttackPower();
				if (this.playerBase.hasAuthority)
				{
					Util.OnLocalPlayerHit(this.playerBase, this.playerBase.trackRoleBase, (double)playerNormalAttackPower, this.playerBase.MyTransform.eulerAngles.y, AttackType.Normal, isAttackWeek);
					if (this.playerBase.attackNum > 1)
					{
						List<RoleBase> canAttackRoleList = this.playerBase.GetCanAttackRoleList(base.GetAttackDistance(), this.playerBase.attackNum);
						if (canAttackRoleList.Count > 0)
						{
							int i = 0;
							int count = canAttackRoleList.Count;
							while (i < count)
							{
								Util.OnLocalPlayerHit(this.playerBase, canAttackRoleList[i], (double)playerNormalAttackPower, this.playerBase.MyTransform.eulerAngles.y, AttackType.Normal, isAttackWeek);
								i++;
							}
						}
					}
					if (Random.value < 0.25f)
					{
						this.playerBase.UpdateRoleState(RoleState.Skill2);
						Game.AudioManager.PlayAudio("Audio/Battle_Audio/Skill/WarGreyWind", 1f, 3f);
						return;
					}
				}
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

	// Token: 0x04000DE6 RID: 3558
	private float checkAttackTime;

	// Token: 0x04000DE7 RID: 3559
	[SerializeField]
	private Transform skillTransform;
}

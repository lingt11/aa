using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200028E RID: 654
public class MeleePlayerMode : PlayerModeBase
{
	// Token: 0x06000C3A RID: 3130 RVA: 0x00047214 File Offset: 0x00045414
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
				if (this.playerBase.hasAuthority)
				{
					bool isAttackWeek = this.playerBase.GetIsAttackWeek(AttackType.Normal);
					long playerNormalAttackPower = this.playerBase.GetPlayerNormalAttackPower();
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

	// Token: 0x06000C3B RID: 3131 RVA: 0x000473EA File Offset: 0x000455EA
	public override void OnStartAttack()
	{
		base.OnStartAttack();
		if (this.attackEffect != null)
		{
			this.attackEffect.SetActive(true);
		}
		if (this.playerBase.hasAuthority)
		{
			Game.AudioManager.PlayAttackAudio(this.attackHitSound);
		}
	}

	// Token: 0x06000C3C RID: 3132 RVA: 0x00047429 File Offset: 0x00045629
	public override void OnExitAttack()
	{
		base.OnExitAttack();
		if (this.attackEffect != null)
		{
			this.attackEffect.SetActive(false);
		}
	}

	// Token: 0x04000CF7 RID: 3319
	[Header("近战攻击")]
	[SerializeField]
	protected GameObject attackEffect;
}

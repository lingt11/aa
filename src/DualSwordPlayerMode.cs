using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200028D RID: 653
public class DualSwordPlayerMode : MeleePlayerMode
{
	// Token: 0x06000C36 RID: 3126 RVA: 0x00046E96 File Offset: 0x00045096
	public override void OnStartAttack()
	{
		base.OnStartAttack();
		if (this.attackEffect2 != null)
		{
			this.attackEffect2.SetActive(true);
		}
		this.isCheckTwo = false;
	}

	// Token: 0x06000C37 RID: 3127 RVA: 0x00046EBF File Offset: 0x000450BF
	public override void OnExitAttack()
	{
		base.OnExitAttack();
		if (this.attackEffect2 != null)
		{
			this.attackEffect2.SetActive(false);
		}
	}

	// Token: 0x06000C38 RID: 3128 RVA: 0x00046EE4 File Offset: 0x000450E4
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
			if (!this.playerBase.isCheckAttack && num > 0.32f && this.playerBase.trackRoleBase != null)
			{
				this.playerBase.isCheckAttack = true;
				if (this.playerBase.hasAuthority)
				{
					bool isAttackWeek = this.playerBase.GetIsAttackWeek(AttackType.Normal);
					long playerNormalAttackPower = this.playerBase.GetPlayerNormalAttackPower();
					Util.OnLocalPlayerHit(this.playerBase, this.playerBase.trackRoleBase, (double)Mathf.RoundToInt((float)playerNormalAttackPower * this.attackDamageLevel), this.playerBase.MyTransform.eulerAngles.y, AttackType.Normal, isAttackWeek);
					if (this.playerBase.attackNum > 1)
					{
						List<RoleBase> canAttackRoleList = this.playerBase.GetCanAttackRoleList(base.GetAttackDistance(), this.playerBase.attackNum);
						if (canAttackRoleList.Count > 0)
						{
							int i = 0;
							int count = canAttackRoleList.Count;
							while (i < count)
							{
								Util.OnLocalPlayerHit(this.playerBase, canAttackRoleList[i], (double)Mathf.RoundToInt((float)playerNormalAttackPower * this.attackDamageLevel), this.playerBase.MyTransform.eulerAngles.y, AttackType.Normal, isAttackWeek);
								i++;
							}
						}
					}
				}
			}
			if (!this.isCheckTwo && num > 0.425f && this.playerBase.trackRoleBase != null)
			{
				this.isCheckTwo = true;
				if (this.playerBase.hasAuthority)
				{
					bool isAttackWeek2 = this.playerBase.GetIsAttackWeek(AttackType.Normal);
					long playerNormalAttackPower2 = this.playerBase.GetPlayerNormalAttackPower();
					Util.OnLocalPlayerHit(this.playerBase, this.playerBase.trackRoleBase, (double)Mathf.RoundToInt((float)playerNormalAttackPower2 * this.attackDamageLevel), this.playerBase.MyTransform.eulerAngles.y, AttackType.Normal, isAttackWeek2);
					if (this.playerBase.attackNum > 1)
					{
						List<RoleBase> canAttackRoleList2 = this.playerBase.GetCanAttackRoleList(base.GetAttackDistance(), this.playerBase.attackNum);
						if (canAttackRoleList2.Count > 0)
						{
							int j = 0;
							int count2 = canAttackRoleList2.Count;
							while (j < count2)
							{
								Util.OnLocalPlayerHit(this.playerBase, canAttackRoleList2[j], (double)Mathf.RoundToInt((float)playerNormalAttackPower2 * this.attackDamageLevel), this.playerBase.MyTransform.eulerAngles.y, AttackType.Normal, isAttackWeek2);
								j++;
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

	// Token: 0x04000CF4 RID: 3316
	[SerializeField]
	protected GameObject attackEffect2;

	// Token: 0x04000CF5 RID: 3317
	private bool isCheckTwo;

	// Token: 0x04000CF6 RID: 3318
	[SerializeField]
	private float attackDamageLevel = 0.75f;
}

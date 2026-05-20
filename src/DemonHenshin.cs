using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000282 RID: 642
public class DemonHenshin : MeleePlayerMode
{
	// Token: 0x06000BFD RID: 3069 RVA: 0x00042392 File Offset: 0x00040592
	public override void OnInitMode()
	{
		base.OnInitMode();
		if (this.roleBase.hasAuthority)
		{
			this.addAttackSpeed = 1f + this.playerBase.addHenshin;
			this.roleBase.AddAttackSpeed(this.addAttackSpeed);
		}
	}

	// Token: 0x06000BFE RID: 3070 RVA: 0x000423CF File Offset: 0x000405CF
	public override void OnClearMode()
	{
		base.OnClearMode();
		if (this.roleBase.hasAuthority)
		{
			this.roleBase.AddAttackSpeed(-this.addAttackSpeed);
		}
	}

	// Token: 0x06000BFF RID: 3071 RVA: 0x000423F8 File Offset: 0x000405F8
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
					long num2 = 0L;
					bool isAttackWeek = this.playerBase.GetIsAttackWeek(AttackType.Normal);
					long playerNormalAttackPower = this.playerBase.GetPlayerNormalAttackPower();
					num2 += Util.OnLocalPlayerHit(this.playerBase, this.playerBase.trackRoleBase, (double)playerNormalAttackPower, this.playerBase.MyTransform.eulerAngles.y, AttackType.Normal, isAttackWeek);
					if (this.playerBase.attackNum > 1)
					{
						List<RoleBase> canAttackRoleList = this.playerBase.GetCanAttackRoleList(base.GetAttackDistance(), this.playerBase.attackNum);
						if (canAttackRoleList.Count > 0)
						{
							int i = 0;
							int count = canAttackRoleList.Count;
							while (i < count)
							{
								num2 += Util.OnLocalPlayerHit(this.playerBase, canAttackRoleList[i], (double)playerNormalAttackPower, this.playerBase.MyTransform.eulerAngles.y, AttackType.Normal, isAttackWeek);
								i++;
							}
						}
					}
					if (num2 > 0L)
					{
						this.playerBase.StartHealthHp((double)((float)num2 * 0.1f * (1f + this.playerBase.addHenshin)), this.playerBase);
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

	// Token: 0x04000CD0 RID: 3280
	public float addAttackSpeed;
}

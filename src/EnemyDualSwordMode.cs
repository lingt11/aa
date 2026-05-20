using System;
using UnityEngine;

// Token: 0x02000261 RID: 609
public class EnemyDualSwordMode : EnemyMeleeMode
{
	// Token: 0x06000B2E RID: 2862 RVA: 0x0003A0BC File Offset: 0x000382BC
	public override void AttackUpdate()
	{
		if (!this.enemyBase.hasAuthority)
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		this.enemyBase.timer += deltaTime;
		float realOffsetInAttack = this.enemyBase.GetRealOffsetInAttack();
		if (this.enemyBase.timer > realOffsetInAttack)
		{
			this.enemyBase.UpdateRoleState(RoleState.Run);
			return;
		}
		float num = this.enemyBase.timer / realOffsetInAttack;
		if (!this.enemyBase.isCheckAttack && num > 0.32f && this.enemyBase.trackRoleBase != null)
		{
			this.enemyBase.isCheckAttack = true;
			this.enemyBase.trackRoleBase.OnHit(this.enemyBase, (double)this.enemyBase.FinalAttackPower, this.enemyBase.MyTransform.eulerAngles.y, AttackType.Normal, false);
		}
		if (!this.isCheckTwo && num > 0.425f && this.enemyBase.trackRoleBase != null)
		{
			this.isCheckTwo = true;
			this.enemyBase.trackRoleBase.OnHit(this.enemyBase, (double)this.enemyBase.FinalAttackPower, this.enemyBase.MyTransform.eulerAngles.y, AttackType.Normal, false);
		}
		if (num < 0.5f)
		{
			if (num < 0.3f)
			{
				this.enemyBase.TrackRotation(3f);
			}
			if (this.enemyBase.trackRoleBase != null)
			{
				float num2 = base.GetAttackDistance() + this.enemyBase.trackRoleBase.RoleModeBase.addRange;
				if (this.enemyBase.GetDistanceV2(this.enemyBase.trackRoleBase.MyTransform.position) > num2 * 0.75f)
				{
					this.enemyBase.MyTranslate(deltaTime * 20f * (0.25f - Mathf.Abs(0.25f - num)));
				}
			}
		}
	}

	// Token: 0x06000B2F RID: 2863 RVA: 0x0003A298 File Offset: 0x00038498
	public override void OnStartAttack()
	{
		base.OnStartAttack();
		this.isCheckTwo = false;
	}

	// Token: 0x04000C14 RID: 3092
	private bool isCheckTwo;
}

using System;
using UnityEngine;

// Token: 0x0200026B RID: 619
public class EnemyMeleeMode : EnemyModeBase
{
	// Token: 0x06000B5B RID: 2907 RVA: 0x0003C4A0 File Offset: 0x0003A6A0
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
		if (!this.enemyBase.isCheckAttack && num > this.checkNormolized && this.enemyBase.trackRoleBase != null)
		{
			this.enemyBase.isCheckAttack = true;
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

	// Token: 0x06000B5C RID: 2908 RVA: 0x0003C619 File Offset: 0x0003A819
	public override void OnStartAttack()
	{
		base.OnStartAttack();
		if (this.attackEffect != null)
		{
			this.attackEffect.SetActive(true);
		}
	}

	// Token: 0x06000B5D RID: 2909 RVA: 0x0003C63B File Offset: 0x0003A83B
	public override void OnExitAttack()
	{
		base.OnExitAttack();
		if (this.attackEffect != null)
		{
			this.attackEffect.SetActive(false);
		}
	}

	// Token: 0x04000C44 RID: 3140
	[Header("近战攻击")]
	[SerializeField]
	private GameObject attackEffect;

	// Token: 0x04000C45 RID: 3141
	[SerializeField]
	private float checkNormolized = 0.23f;
}

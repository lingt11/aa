using System;
using UnityEngine;

// Token: 0x02000301 RID: 769
public class SummonPeashooterMode : EnemyTrajectoryMode
{
	// Token: 0x060011BC RID: 4540 RVA: 0x00068238 File Offset: 0x00066438
	public override void OnClientInitEnemy()
	{
		base.OnClientInitEnemy();
		Transform transform = base.transform;
		transform.localEulerAngles = new Vector3(0f, 180f, 0f);
		transform.localPosition = new Vector3(0f, 0f, 0f);
		Game.EffectManager.PlayEffect(EffectDefine.SummonEffect, 2f, transform.position - new Vector3(0f, 1f, 0f), 1.75f);
	}

	// Token: 0x060011BD RID: 4541 RVA: 0x000682C0 File Offset: 0x000664C0
	public override void MoveUpdate()
	{
		if (this.enemyBase.roleType == RoleType.Enemy && Time.time > this.enemyBase.nextGetTrackTime)
		{
			this.enemyBase.GetTrackRole(true, 12f, false, false);
		}
		if (this.enemyBase.trackRoleBase == null || !this.enemyBase.trackRoleBase.gameObject.activeSelf || this.enemyBase.trackRoleBase.IsDead())
		{
			if (this.enemyBase.trackRoleBase == null || this.enemyBase.roleType != RoleType.Enemy)
			{
				this.enemyBase.GetTrackRole(true, 17f, true, false);
			}
			else
			{
				this.enemyBase.GetTrackRole(true, 12f, false, false);
			}
			if (this.enemyBase.trackRoleBase == null || !this.enemyBase.trackRoleBase.gameObject.activeSelf || this.enemyBase.trackRoleBase.IsDead())
			{
				this.enemyBase.timer = this.enemyBase.GetRealAttackOffset();
				this.enemyBase.UpdateRoleState(RoleState.Idle);
				return;
			}
		}
		if (this.enemyBase.trackRoleBase != null)
		{
			this.enemyBase.trackRoleBase.TrackRotation(3f);
			float num = base.GetAttackDistance() + this.enemyBase.trackRoleBase.RoleModeBase.addRange;
			if (Util.GetV2Distance(this.enemyBase.MyTransform.position, this.enemyBase.trackRoleBase.MyTransform.position) < num)
			{
				this.enemyBase.OnLocalStartAttack();
				this.enemyBase.UpdateRoleState(RoleState.Attack);
			}
		}
	}

	// Token: 0x060011BE RID: 4542 RVA: 0x00068470 File Offset: 0x00066670
	public override void OnStartDead()
	{
		base.OnStartDead();
		Transform transform = base.transform;
		Game.EffectManager.PlayEffect(EffectDefine.SummonEffect, 2f, transform.position - new Vector3(0f, 1f, 0f), 1.75f);
		base.gameObject.SetActive(false);
	}
}

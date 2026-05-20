using System;
using UnityEngine;

// Token: 0x02000260 RID: 608
public class EnemyChestMode : EnemyMeleeMode
{
	// Token: 0x06000B25 RID: 2853 RVA: 0x00039DC8 File Offset: 0x00037FC8
	public override void OnClientInitEnemy()
	{
		base.OnClientInitEnemy();
		if (this.enemyBase.hasAuthority)
		{
			this.enemyBase.ShowTip();
			this.isFirstMove = false;
			this.spawnPosition = GameHelperClient.spawnConfig.playerSpawnPoint[(int)(GameHelperClient.localPlayer.netId - 1U)];
		}
	}

	// Token: 0x06000B26 RID: 2854 RVA: 0x00039E1B File Offset: 0x0003801B
	public override void OnStartShowPose()
	{
		this.roleBase.animTransform.localPosition = Vector3.zero;
	}

	// Token: 0x06000B27 RID: 2855 RVA: 0x00039E32 File Offset: 0x00038032
	public override void UpdateShowPose()
	{
		if (this.enemyBase.hasAuthority && this.enemyBase.hp < this.enemyBase.maxHp)
		{
			this.enemyBase.UpdateRoleState(RoleState.Skill);
		}
	}

	// Token: 0x06000B28 RID: 2856 RVA: 0x00039E65 File Offset: 0x00038065
	public override void OnExitShowPose()
	{
		base.OnExitShowPose();
	}

	// Token: 0x06000B29 RID: 2857 RVA: 0x00039E70 File Offset: 0x00038070
	public override void OnStartSkill()
	{
		this.roleBase.timer = 0f;
		this.roleBase.PlayAni(AnimDefine.Skill, 1f, 0.1f);
		Game.AudioManager.PlayDeadAudio(this.deadSound, this.roleBase.MyTransform.position);
	}

	// Token: 0x06000B2A RID: 2858 RVA: 0x00039EC7 File Offset: 0x000380C7
	public override void UpdateSkill1()
	{
		this.roleBase.timer += Time.deltaTime;
		if (this.roleBase.timer > 0.8333f)
		{
			this.enemyBase.UpdateRoleState(RoleState.Run);
		}
	}

	// Token: 0x06000B2B RID: 2859 RVA: 0x00039EFE File Offset: 0x000380FE
	public override void OnExitSkill()
	{
		base.OnExitSkill();
	}

	// Token: 0x06000B2C RID: 2860 RVA: 0x00039F08 File Offset: 0x00038108
	public override void MoveUpdate()
	{
		if (!this.isFirstMove)
		{
			if (this.enemyBase.GetDistanceV2(this.spawnPosition) >= 2.5f)
			{
				this.enemyBase.TrackMoveUpdate(this.spawnPosition, false);
				return;
			}
			this.isFirstMove = true;
		}
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
		this.enemyBase.TrackMoveUpdate(Util.GetSaveMapPos(this.enemyBase.MyTransform.position - this.enemyBase.trackRoleBase.MyTransform.position + this.enemyBase.MyTransform.position), false);
	}

	// Token: 0x04000C12 RID: 3090
	private bool isFirstMove;

	// Token: 0x04000C13 RID: 3091
	private Vector3 spawnPosition;
}

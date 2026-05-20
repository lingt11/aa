using System;
using UnityEngine;

// Token: 0x0200026C RID: 620
public class EnemyModeBase : RoleModeBase
{
	// Token: 0x06000B5F RID: 2911 RVA: 0x0003C670 File Offset: 0x0003A870
	public virtual void OnClientInitEnemy()
	{
		if (this.tmpSpine == null && this.myAnim != null && this.myAnim.isHuman)
		{
			this.tmpSpine = this.myAnim.GetBoneTransform(HumanBodyBones.Spine);
		}
		EnemyType enemyType = this.enemyBase.enemyType;
		if (enemyType >= EnemyType.Goblin_HeartMonster_0 && enemyType <= EnemyType.Goblin_HeartMonster_5)
		{
			this.enemyBase.animTransform.localScale = this.baseModeScale * 1.5f;
		}
	}

	// Token: 0x06000B60 RID: 2912 RVA: 0x0003C6F4 File Offset: 0x0003A8F4
	public override void OnClearMode()
	{
		this.enemyBase.animTransform.localScale = this.baseModeScale;
		base.OnClearMode();
	}

	// Token: 0x06000B61 RID: 2913 RVA: 0x0003C714 File Offset: 0x0003A914
	public virtual void MoveUpdate()
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
		this.enemyBase.TrackMoveUpdate(this.enemyBase.trackRoleBase.MyTransform.position, true);
	}

	// Token: 0x04000C46 RID: 3142
	[Header("出场动画")]
	public bool isShowPose;

	// Token: 0x04000C47 RID: 3143
	[SerializeField]
	public bool isPlayShowPoseAnim;

	// Token: 0x04000C48 RID: 3144
	[Header("怪物攻击参数")]
	public float enemyAttackOffset = 3f;

	// Token: 0x04000C49 RID: 3145
	public bool isOpenSkillAI;

	// Token: 0x04000C4A RID: 3146
	public AIAttackCheck[] aiAttackChecks;

	// Token: 0x04000C4B RID: 3147
	[Header("骨骼抖动")]
	public Transform tmpSpine;

	// Token: 0x04000C4C RID: 3148
	public Vector3 punchScale = new Vector3(0.3f, 0f, 0.3f);

	// Token: 0x04000C4D RID: 3149
	public float punchTime = 0.1f;

	// Token: 0x04000C4E RID: 3150
	[HideInInspector]
	public EnemyBase enemyBase;

	// Token: 0x04000C4F RID: 3151
	[Header("怪物包含技能")]
	public int[] activeSkillAry;
}

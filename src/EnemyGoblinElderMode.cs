using System;
using UnityEngine;

// Token: 0x02000266 RID: 614
public class EnemyGoblinElderMode : EnemyTrajectoryMode
{
	// Token: 0x06000B44 RID: 2884 RVA: 0x0003ABBC File Offset: 0x00038DBC
	public override void UpdateSkill1()
	{
		if (!this.enemyBase.hasAuthority)
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		this.enemyBase.timer += deltaTime;
		if (this.enemyBase.timer > 3.327273f)
		{
			this.enemyBase.UpdateRoleState(RoleState.Run);
			return;
		}
		if (this.enemyBase.timer > 0.727273f)
		{
			if (!this.isPlayerEffect)
			{
				this.isPlayerEffect = true;
				Vector3 position = this.enemyBase.MyTransform.position;
				this.enemyBase.CmdPlayTipLine(position, this.enemyBase.MyTransform.localEulerAngles.y);
			}
			if (!this.enemyBase.isCheckAttack && this.enemyBase.timer > 2.227273f)
			{
				this.enemyBase.isCheckAttack = true;
				float y = this.enemyBase.MyTransform.localEulerAngles.y;
				for (int i = 0; i < 8; i++)
				{
					this.enemyBase.CmdCreateSkillBySyncData(ActiveSkillEnum.Elder_Wave, this.enemyBase.MyTransform.position, i, y + (float)i * 45f, -1, 0);
				}
				return;
			}
		}
		else if (this.enemyBase.timer < 0.62727284f && this.enemyBase.hasAuthority)
		{
			this.enemyBase.TrackRotation(3f);
		}
	}

	// Token: 0x06000B45 RID: 2885 RVA: 0x0003AD15 File Offset: 0x00038F15
	public override void OnStartSkill()
	{
		base.OnStartSkill();
		this.enemyBase.isCheckAttack = false;
		this.isPlayerEffect = false;
	}

	// Token: 0x04000C27 RID: 3111
	private const float skill1Time = 3.327273f;

	// Token: 0x04000C28 RID: 3112
	private bool isPlayerEffect;
}

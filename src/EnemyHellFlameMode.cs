using System;
using UnityEngine;

// Token: 0x0200026A RID: 618
public class EnemyHellFlameMode : EnemyMeleeMode
{
	// Token: 0x06000B58 RID: 2904 RVA: 0x0003C31C File Offset: 0x0003A51C
	public override void UpdateSkill1()
	{
		if (!this.enemyBase.hasAuthority)
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		this.enemyBase.timer += deltaTime;
		if (this.enemyBase.timer > this.skill1Time)
		{
			this.enemyBase.UpdateRoleState(RoleState.Run);
			return;
		}
		if (this.enemyBase.timer > this.skill1Time - 2.6f)
		{
			if (!this.isPlayerEffect)
			{
				this.isPlayerEffect = true;
				Vector3 pos = (this.enemyBase.trackRoleBase != null) ? this.enemyBase.trackRoleBase.MyTransform.position : (this.enemyBase.MyTransform.position + this.enemyBase.MyTransform.forward * 5f);
				this.enemyBase.CmdPlayTipSector(pos, Game.GameData.ActiveSkillDataDic[ActiveSkillEnum.HellFire_Call].range, 1.5f);
				this.enemyBase.CmdCreateSkill(ActiveSkillEnum.HellFire_Call, pos, 0f, -1, 0);
				return;
			}
		}
		else if (this.enemyBase.timer < this.skill1Time - 2.7f && this.enemyBase.hasAuthority)
		{
			this.enemyBase.TrackRotation(3f);
		}
	}

	// Token: 0x06000B59 RID: 2905 RVA: 0x0003C471 File Offset: 0x0003A671
	public override void OnStartSkill()
	{
		base.OnStartSkill();
		this.enemyBase.isCheckAttack = false;
		this.isPlayerEffect = false;
	}

	// Token: 0x04000C42 RID: 3138
	private float skill1Time = 3.327273f;

	// Token: 0x04000C43 RID: 3139
	private bool isPlayerEffect;
}

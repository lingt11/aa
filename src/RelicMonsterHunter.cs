using System;
using UnityEngine;

// Token: 0x02000229 RID: 553
public class RelicMonsterHunter : RelicBase
{
	// Token: 0x06000A00 RID: 2560 RVA: 0x00034F34 File Offset: 0x00033134
	public override void Enter()
	{
		this.bossKillCount = 0;
		this.isTotalPercent = true;
		this.totals = new int[1];
		PlayerBase playerBase = this.playerBase;
		playerBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Combine(playerBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
	}

	// Token: 0x06000A01 RID: 2561 RVA: 0x00034F84 File Offset: 0x00033184
	private void KillEvent(RoleBase attackrole, RoleBase hurtrole)
	{
		EnemyBase enemyBase = hurtrole as EnemyBase;
		if (enemyBase != null && enemyBase.isBoss)
		{
			this.bossKillCount++;
			this.playerBase.addDamagePercent += base.GetValue(0, 0.1f);
			this.totals[0] += Mathf.RoundToInt(base.GetValue(0, 0.1f) * 100f);
		}
	}

	// Token: 0x06000A02 RID: 2562 RVA: 0x00034FF6 File Offset: 0x000331F6
	public override void Exit()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Remove(playerBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
	}

	// Token: 0x04000BD2 RID: 3026
	private int bossKillCount;
}

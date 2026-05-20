using System;

// Token: 0x02000205 RID: 517
public class RelicBeefEater : RelicBase
{
	// Token: 0x06000969 RID: 2409 RVA: 0x00032FDF File Offset: 0x000311DF
	public override void Enter()
	{
		this.totals = new int[1];
		PlayerBase playerBase = this.playerBase;
		playerBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Combine(playerBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x00033014 File Offset: 0x00031214
	private void KillEvent(RoleBase attackrole, RoleBase hurtrole)
	{
		EnemyBase enemyBase = hurtrole as EnemyBase;
		if (enemyBase != null)
		{
			int intValue;
			if (enemyBase.isBoss)
			{
				intValue = base.GetIntValue(0, 300);
			}
			else if (enemyBase.isElite)
			{
				intValue = base.GetIntValue(1, 15);
			}
			else
			{
				intValue = base.GetIntValue(2, 1);
			}
			this.playerBase.CmdUpdateMaxHp((long)intValue, attackrole.netId);
			this.totals[0] += intValue;
		}
	}

	// Token: 0x0600096B RID: 2411 RVA: 0x00033085 File Offset: 0x00031285
	public override void Exit()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Remove(playerBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
	}
}

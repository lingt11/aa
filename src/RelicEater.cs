using System;

// Token: 0x02000232 RID: 562
public class RelicEater : RelicBase
{
	// Token: 0x06000A27 RID: 2599 RVA: 0x00035632 File Offset: 0x00033832
	public override void Enter()
	{
		this.totals = new int[1];
		PlayerBase playerBase = this.playerBase;
		playerBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Combine(playerBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
	}

	// Token: 0x06000A28 RID: 2600 RVA: 0x00035668 File Offset: 0x00033868
	private void KillEvent(RoleBase attackrole, RoleBase hurtrole)
	{
		if (hurtrole is EnemyBase)
		{
			this.killIndex++;
			if (this.killIndex >= base.GetIntValue(0, 6))
			{
				this.playerBase.AddAttackPower(base.GetIntValue(1, 1));
				this.killIndex = 0;
				this.totals[0] += base.GetIntValue(1, 1);
			}
		}
	}

	// Token: 0x06000A29 RID: 2601 RVA: 0x000356CD File Offset: 0x000338CD
	public override void Exit()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Remove(playerBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
	}

	// Token: 0x04000BD5 RID: 3029
	private int killIndex;
}

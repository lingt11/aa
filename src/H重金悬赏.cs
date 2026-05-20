using System;

// Token: 0x020001CB RID: 459
public class H重金悬赏 : PasssiveSkill
{
	// Token: 0x0600086E RID: 2158 RVA: 0x000303A4 File Offset: 0x0002E5A4
	public override void Enter()
	{
		this.totalName = Game.Language.Get(PathDefine.Concat("p_", this.skillId, StringDefine.Total), "");
		this.totals = new int[3];
		this.roleBase.addBossEnemy += 0.3f;
		this.roleBase.addEliteEnemy += 0.3f;
		PlayerBase roleBase = this.roleBase;
		roleBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Combine(roleBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x0003043C File Offset: 0x0002E63C
	public override void Exit()
	{
		this.roleBase.addBossEnemy -= 0.3f;
		this.roleBase.addEliteEnemy -= 0.3f;
		PlayerBase roleBase = this.roleBase;
		roleBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Remove(roleBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x000304A0 File Offset: 0x0002E6A0
	private void KillEvent(RoleBase attackrole, RoleBase hurtrole)
	{
		EnemyBase enemyBase = hurtrole as EnemyBase;
		if (enemyBase != null)
		{
			if (enemyBase.isElite)
			{
				PlayerBase playerBase = attackrole as PlayerBase;
				if (playerBase != null)
				{
					playerBase.AddGem(attackrole.GetHeadUIPos(), 1, false);
				}
				this.totals[0]++;
			}
			if (enemyBase.isBoss)
			{
				attackrole.AddSTR(8);
				attackrole.AddSTA(8);
				attackrole.AddAGI(8);
				this.totals[2] += 8;
				PlayerBase playerBase2 = attackrole as PlayerBase;
				if (playerBase2 != null)
				{
					int num = playerBase2.AddGold(attackrole.GetHeadUIPos(), 1000, true);
					this.totals[1] += num;
				}
			}
		}
	}
}

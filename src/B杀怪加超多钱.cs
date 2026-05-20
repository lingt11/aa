using System;

// Token: 0x02000191 RID: 401
public class B杀怪加超多钱 : PasssiveSkill
{
	// Token: 0x06000798 RID: 1944 RVA: 0x0002D030 File Offset: 0x0002B230
	public override void Enter()
	{
		this.totalName = Game.Language.Get(PathDefine.Concat("p_", this.skillId, StringDefine.Total), "");
		this.totals = new int[1];
		PlayerBase roleBase = this.roleBase;
		roleBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Combine(roleBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
	}

	// Token: 0x06000799 RID: 1945 RVA: 0x0002D09A File Offset: 0x0002B29A
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Remove(roleBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
	}

	// Token: 0x0600079A RID: 1946 RVA: 0x0002D0C4 File Offset: 0x0002B2C4
	private void KillEvent(RoleBase attackrole, RoleBase hurtrole)
	{
		if (hurtrole != null)
		{
			PlayerBase playerBase = attackrole as PlayerBase;
			if (playerBase != null)
			{
				int num = playerBase.AddGold(hurtrole.GetHeadUIPos(), base.GetSkillIntValue(0, 0), true);
				this.totals[0] += num;
			}
		}
	}
}

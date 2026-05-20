using System;

// Token: 0x020001B5 RID: 437
public class D杀怪加钱 : PasssiveSkill
{
	// Token: 0x06000821 RID: 2081 RVA: 0x0002EF1C File Offset: 0x0002D11C
	public override void Enter()
	{
		this.totalName = Game.Language.Get(PathDefine.Concat("p_", this.skillId, StringDefine.Total), "");
		this.totals = new int[1];
		PlayerBase roleBase = this.roleBase;
		roleBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Combine(roleBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
	}

	// Token: 0x06000822 RID: 2082 RVA: 0x0002EF86 File Offset: 0x0002D186
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Remove(roleBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
	}

	// Token: 0x06000823 RID: 2083 RVA: 0x0002EFB0 File Offset: 0x0002D1B0
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

using System;

// Token: 0x020001A5 RID: 421
public class C杀怪加很多钱 : PasssiveSkill
{
	// Token: 0x060007E5 RID: 2021 RVA: 0x0002E274 File Offset: 0x0002C474
	public override void Enter()
	{
		this.totalName = Game.Language.Get(PathDefine.Concat("p_", this.skillId, StringDefine.Total), "");
		this.totals = new int[1];
		PlayerBase roleBase = this.roleBase;
		roleBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Combine(roleBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
	}

	// Token: 0x060007E6 RID: 2022 RVA: 0x0002E2DE File Offset: 0x0002C4DE
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Remove(roleBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
	}

	// Token: 0x060007E7 RID: 2023 RVA: 0x0002E308 File Offset: 0x0002C508
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

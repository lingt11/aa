using System;

// Token: 0x02000170 RID: 368
public class A杀怪加超级巨多钱 : PasssiveSkill
{
	// Token: 0x0600072D RID: 1837 RVA: 0x0002B64C File Offset: 0x0002984C
	public override void Enter()
	{
		this.totalName = Game.Language.Get(PathDefine.Concat("p_", this.skillId, StringDefine.Total), "");
		this.totals = new int[1];
		PlayerBase roleBase = this.roleBase;
		roleBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Combine(roleBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x0002B6B6 File Offset: 0x000298B6
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Remove(roleBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
	}

	// Token: 0x0600072F RID: 1839 RVA: 0x0002B6E0 File Offset: 0x000298E0
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

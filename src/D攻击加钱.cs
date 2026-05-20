using System;

// Token: 0x020001B4 RID: 436
public class D攻击加钱 : PasssiveSkill
{
	// Token: 0x0600081D RID: 2077 RVA: 0x0002EE34 File Offset: 0x0002D034
	public override void Enter()
	{
		this.totalName = Game.Language.Get(PathDefine.Concat("p_", this.skillId, StringDefine.Total), "");
		this.totals = new int[1];
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x0600081E RID: 2078 RVA: 0x0002EE9E File Offset: 0x0002D09E
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x0600081F RID: 2079 RVA: 0x0002EEC8 File Offset: 0x0002D0C8
	private float AttackEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		if (GameHelperClient.isReady)
		{
			return damage;
		}
		if (!base.CheckCD())
		{
			PlayerBase playerBase = attackrole as PlayerBase;
			if (playerBase != null)
			{
				int num = playerBase.AddGold(hurtrole.GetHeadUIPos(), base.GetSkillIntValue(0, 0), true);
				this.totals[0] += num;
			}
		}
		return damage;
	}
}

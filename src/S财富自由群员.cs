using System;
using UnityEngine;

// Token: 0x020001D3 RID: 467
public class S财富自由群员 : PasssiveSkill
{
	// Token: 0x06000888 RID: 2184 RVA: 0x00030808 File Offset: 0x0002EA08
	public override void Enter()
	{
		this.totalName = Game.Language.Get(PathDefine.Concat("p_", this.skillId, StringDefine.Total), "");
		this.totals = new int[1];
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x00030872 File Offset: 0x0002EA72
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x0003089C File Offset: 0x0002EA9C
	private float AttackEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		if (GameHelperClient.isReady)
		{
			return damage;
		}
		if (Random.value * 100f < (float)base.GetSkillIntValue(0, 0) && !base.CheckCD())
		{
			PlayerBase playerBase = attackrole as PlayerBase;
			if (playerBase != null)
			{
				int num = playerBase.AddGold(hurtrole.GetHeadUIPos(), base.GetSkillIntValue(1, 0), true);
				this.totals[0] += num;
			}
		}
		return damage;
	}
}

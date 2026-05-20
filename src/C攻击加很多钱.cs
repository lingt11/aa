using System;
using UnityEngine;

// Token: 0x020001A3 RID: 419
public class C攻击加很多钱 : PasssiveSkill
{
	// Token: 0x060007DD RID: 2013 RVA: 0x0002E094 File Offset: 0x0002C294
	public override void Enter()
	{
		this.totalName = Game.Language.Get(PathDefine.Concat("p_", this.skillId, StringDefine.Total), "");
		this.totals = new int[1];
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x060007DE RID: 2014 RVA: 0x0002E0FE File Offset: 0x0002C2FE
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x060007DF RID: 2015 RVA: 0x0002E128 File Offset: 0x0002C328
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

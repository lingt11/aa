using System;
using UnityEngine;

// Token: 0x0200016F RID: 367
public class A攻击加超级巨多钱 : PasssiveSkill
{
	// Token: 0x06000729 RID: 1833 RVA: 0x0002B550 File Offset: 0x00029750
	public override void Enter()
	{
		this.totalName = Game.Language.Get(PathDefine.Concat("p_", this.skillId, StringDefine.Total), "");
		this.totals = new int[1];
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x0600072A RID: 1834 RVA: 0x0002B5BA File Offset: 0x000297BA
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x0600072B RID: 1835 RVA: 0x0002B5E4 File Offset: 0x000297E4
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

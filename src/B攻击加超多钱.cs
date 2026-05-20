using System;
using UnityEngine;

// Token: 0x02000190 RID: 400
public class B攻击加超多钱 : PasssiveSkill
{
	// Token: 0x06000794 RID: 1940 RVA: 0x0002CF34 File Offset: 0x0002B134
	public override void Enter()
	{
		this.totalName = Game.Language.Get(PathDefine.Concat("p_", this.skillId, StringDefine.Total), "");
		this.totals = new int[1];
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x0002CF9E File Offset: 0x0002B19E
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x0002CFC8 File Offset: 0x0002B1C8
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

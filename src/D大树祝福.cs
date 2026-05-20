using System;
using UnityEngine;

// Token: 0x020001AC RID: 428
public class D大树祝福 : PasssiveSkill
{
	// Token: 0x060007FF RID: 2047 RVA: 0x0002E728 File Offset: 0x0002C928
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x06000800 RID: 2048 RVA: 0x0002E751 File Offset: 0x0002C951
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x06000801 RID: 2049 RVA: 0x0002E77C File Offset: 0x0002C97C
	private float AttackEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		if (Random.value * 100f < 10f)
		{
			Vector3 position = attackrole.transform.position;
			position.y = 0f;
			this.roleBase.CmdCreateSkill(ActiveSkillEnum.D_SummomTree, position, 0f, 0, 0);
		}
		return damage;
	}
}

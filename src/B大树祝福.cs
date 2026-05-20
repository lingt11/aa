using System;
using UnityEngine;

// Token: 0x0200018C RID: 396
public class B大树祝福 : PasssiveSkill
{
	// Token: 0x06000785 RID: 1925 RVA: 0x0002CA91 File Offset: 0x0002AC91
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x06000786 RID: 1926 RVA: 0x0002CABA File Offset: 0x0002ACBA
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x06000787 RID: 1927 RVA: 0x0002CAE4 File Offset: 0x0002ACE4
	private float AttackEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		if (Random.value * 100f < 10f)
		{
			Vector3 position = attackrole.MyTransform.position;
			position.y = 0f;
			this.roleBase.CmdCreateSkill(ActiveSkillEnum.B_SummomTree, position, 0f, 0, 0);
		}
		return damage;
	}
}

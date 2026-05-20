using System;
using UnityEngine;

// Token: 0x0200019B RID: 411
public class C大树祝福 : PasssiveSkill
{
	// Token: 0x060007BE RID: 1982 RVA: 0x0002D7E4 File Offset: 0x0002B9E4
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x060007BF RID: 1983 RVA: 0x0002D80D File Offset: 0x0002BA0D
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x0002D838 File Offset: 0x0002BA38
	private float AttackEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		if (Random.value * 100f < 10f)
		{
			Vector3 position = attackrole.transform.position;
			position.y = 0f;
			this.roleBase.CmdCreateSkill(ActiveSkillEnum.C_SummomTree, position, 0f, 0, 0);
		}
		return damage;
	}
}

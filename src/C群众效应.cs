using System;
using UnityEngine;

// Token: 0x020001A6 RID: 422
public class C群众效应 : PasssiveSkill
{
	// Token: 0x060007E9 RID: 2025 RVA: 0x0002E34F File Offset: 0x0002C54F
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x060007EA RID: 2026 RVA: 0x0002E378 File Offset: 0x0002C578
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x060007EB RID: 2027 RVA: 0x0002E3A4 File Offset: 0x0002C5A4
	private float AttackEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		if (GameHelperClient.isReady)
		{
			return damage;
		}
		this.count++;
		if (this.count >= Mathf.RoundToInt(this.skillValues[0]))
		{
			this.count = 0;
			this.roleBase.AddAttackPower(Mathf.RoundToInt(this.skillValues[1]));
		}
		return damage;
	}

	// Token: 0x04000B63 RID: 2915
	private int count;
}

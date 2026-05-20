using System;
using UnityEngine;

// Token: 0x02000197 RID: 407
public class B高级活性护甲 : PasssiveSkill
{
	// Token: 0x060007AE RID: 1966 RVA: 0x0002D400 File Offset: 0x0002B600
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.damageEvent = (RoleBase.DamageEnemy)Delegate.Combine(roleBase.damageEvent, new RoleBase.DamageEnemy(this.DamageEvent));
	}

	// Token: 0x060007AF RID: 1967 RVA: 0x0002D429 File Offset: 0x0002B629
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.damageEvent = (RoleBase.DamageEnemy)Delegate.Remove(roleBase.damageEvent, new RoleBase.DamageEnemy(this.DamageEvent));
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x0002D452 File Offset: 0x0002B652
	private float DamageEvent(RoleBase attackrole, RoleBase hurtrole, AttackType attackType, ref float damage)
	{
		(hurtrole.roleBuffManager.AddOneBuff<Buff回血固定>("Buff回血固定", 5f) as Buff回血固定).addValue = Mathf.RoundToInt(this.skillValues[0]);
		return damage;
	}
}

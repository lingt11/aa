using System;
using UnityEngine;

// Token: 0x020001A8 RID: 424
public class C重击 : PasssiveSkill
{
	// Token: 0x060007F0 RID: 2032 RVA: 0x0002E400 File Offset: 0x0002C600
	public override void Enter()
	{
		this.randomValue = Mathf.RoundToInt(this.skillValues[0]);
		this.skillDamage = Mathf.RoundToInt(this.skillValues[1]);
		this.buffTime = this.skillValues[2];
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x060007F1 RID: 2033 RVA: 0x0002E468 File Offset: 0x0002C668
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x060007F2 RID: 2034 RVA: 0x0002E494 File Offset: 0x0002C694
	private float AttackEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		if (Random.value * 100f < (float)this.randomValue)
		{
			damage += (float)Util.GetPassSkillDamage(this.roleBase, this.skillAttribute, (double)this.skillDamage, false);
			hurtrole.XuanYun(this.buffTime);
		}
		return damage;
	}

	// Token: 0x04000B64 RID: 2916
	private int randomValue;

	// Token: 0x04000B65 RID: 2917
	private int skillDamage;

	// Token: 0x04000B66 RID: 2918
	private float buffTime;
}

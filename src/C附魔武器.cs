using System;
using UnityEngine;

// Token: 0x020001AA RID: 426
public class C附魔武器 : PasssiveSkill
{
	// Token: 0x060007F7 RID: 2039 RVA: 0x0002E56C File Offset: 0x0002C76C
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
		this.randomValue = Mathf.RoundToInt(this.skillValues[0]);
		this.baseValue = Mathf.RoundToInt(this.skillValues[1]);
		this.levelValue = Mathf.RoundToInt(this.skillValues[2]);
	}

	// Token: 0x060007F8 RID: 2040 RVA: 0x0002E5D9 File Offset: 0x0002C7D9
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x060007F9 RID: 2041 RVA: 0x0002E604 File Offset: 0x0002C804
	private float AttackEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		if (Random.value * 100f < (float)this.randomValue)
		{
			if (base.CheckCD())
			{
				return damage;
			}
			damage += (float)Util.GetPassSkillDamage(this.roleBase, this.skillAttribute, (double)(this.roleBase.STR * this.levelValue + this.baseValue), false);
			PlayerBase playerBase = attackrole as PlayerBase;
			if (playerBase != null)
			{
				playerBase.AddMp(7);
			}
		}
		return damage;
	}

	// Token: 0x04000B67 RID: 2919
	private int randomValue;

	// Token: 0x04000B68 RID: 2920
	private int baseValue;

	// Token: 0x04000B69 RID: 2921
	private int levelValue;
}

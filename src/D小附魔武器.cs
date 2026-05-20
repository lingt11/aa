using System;
using UnityEngine;

// Token: 0x020001B1 RID: 433
public class D小附魔武器 : PasssiveSkill
{
	// Token: 0x06000812 RID: 2066 RVA: 0x0002EBE4 File Offset: 0x0002CDE4
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
		this.randomValue = Mathf.RoundToInt(this.skillValues[0]);
		this.baseValue = Mathf.RoundToInt(this.skillValues[1]);
		this.levelValue = Mathf.RoundToInt(this.skillValues[2]);
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x0002EC51 File Offset: 0x0002CE51
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x0002EC7C File Offset: 0x0002CE7C
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
				playerBase.AddMp(5);
			}
		}
		return damage;
	}

	// Token: 0x04000B6F RID: 2927
	private int randomValue;

	// Token: 0x04000B70 RID: 2928
	private int baseValue;

	// Token: 0x04000B71 RID: 2929
	private int levelValue;
}

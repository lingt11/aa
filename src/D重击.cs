using System;
using UnityEngine;

// Token: 0x020001B8 RID: 440
public class D重击 : PasssiveSkill
{
	// Token: 0x0600082A RID: 2090 RVA: 0x0002F130 File Offset: 0x0002D330
	public override void Enter()
	{
		this.randomValue = Mathf.RoundToInt(this.skillValues[0]);
		this.skillDamage = Mathf.RoundToInt(this.skillValues[1]);
		this.buffTime = this.skillValues[2];
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x0600082B RID: 2091 RVA: 0x0002F198 File Offset: 0x0002D398
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x0002F1C4 File Offset: 0x0002D3C4
	private float AttackEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		if (Random.value * 100f < (float)this.randomValue)
		{
			damage += (float)Util.GetPassSkillDamage(this.roleBase, this.skillAttribute, (double)this.skillDamage, false);
			hurtrole.XuanYun(this.buffTime);
		}
		return damage;
	}

	// Token: 0x04000B75 RID: 2933
	private int randomValue;

	// Token: 0x04000B76 RID: 2934
	private int skillDamage;

	// Token: 0x04000B77 RID: 2935
	private float buffTime;
}

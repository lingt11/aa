using System;
using UnityEngine;

// Token: 0x02000194 RID: 404
public class B重击 : PasssiveSkill
{
	// Token: 0x060007A3 RID: 1955 RVA: 0x0002D244 File Offset: 0x0002B444
	public override void Enter()
	{
		this.randomValue = Mathf.RoundToInt(this.skillValues[0]);
		this.skillDamage = Mathf.RoundToInt(this.skillValues[1]);
		this.buffTime = this.skillValues[2];
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x0002D2AC File Offset: 0x0002B4AC
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x060007A5 RID: 1957 RVA: 0x0002D2D8 File Offset: 0x0002B4D8
	private float AttackEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		if (Random.value * 100f < (float)this.randomValue)
		{
			damage += (float)Util.GetPassSkillDamage(this.roleBase, this.skillAttribute, (double)this.skillDamage, false);
			hurtrole.XuanYun(this.buffTime);
		}
		return damage;
	}

	// Token: 0x04000B4E RID: 2894
	private int randomValue;

	// Token: 0x04000B4F RID: 2895
	private int skillDamage;

	// Token: 0x04000B50 RID: 2896
	private float buffTime;
}

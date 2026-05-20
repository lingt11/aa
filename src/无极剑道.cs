using System;
using UnityEngine;

// Token: 0x020001DF RID: 479
public class 无极剑道 : PasssiveSkill
{
	// Token: 0x060008B0 RID: 2224 RVA: 0x0003114C File Offset: 0x0002F34C
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x060008B1 RID: 2225 RVA: 0x00031175 File Offset: 0x0002F375
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x060008B2 RID: 2226 RVA: 0x000311A0 File Offset: 0x0002F3A0
	private float AttackEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		this.count++;
		if (this.count > Mathf.RoundToInt(this.skillValues[0]))
		{
			if (base.CheckCD())
			{
				return damage;
			}
			this.count = 0;
			PlayerBase playerBase = attackrole as PlayerBase;
			if (playerBase != null)
			{
				bool isAttackWeek = playerBase.GetIsAttackWeek(AttackType.Normal);
				long playerNormalAttackPower = playerBase.GetPlayerNormalAttackPower();
				Util.OnLocalPlayerHit(attackrole, hurtrole, (double)playerNormalAttackPower, Util.GetV2Angle(hurtrole.MyTransform.position, attackrole.MyTransform.position), AttackType.Normal, isAttackWeek);
			}
		}
		return damage;
	}

	// Token: 0x04000B96 RID: 2966
	public int count = 1;
}

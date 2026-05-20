using System;
using UnityEngine;

// Token: 0x02000204 RID: 516
public class RelicBattleFocus : RelicBase
{
	// Token: 0x06000965 RID: 2405 RVA: 0x00032E99 File Offset: 0x00031099
	public override void Enter()
	{
		this.isTotalPercent = true;
		this.totals = new int[1];
		PlayerBase playerBase = this.playerBase;
		playerBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(playerBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}

	// Token: 0x06000966 RID: 2406 RVA: 0x00032ED8 File Offset: 0x000310D8
	private float AttackEnemyEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		if (this.curAttackRole != null && this.curAttackRole == hurtrole)
		{
			this.addDamage += base.GetValue(0, 0.02f);
			this.playerBase.normalAttackAddDamage += base.GetValue(0, 0.02f);
			this.totals[0] = Mathf.RoundToInt(this.addDamage * 100f);
		}
		else
		{
			this.playerBase.normalAttackAddDamage -= this.addDamage;
			this.addDamage = 0f;
			this.curAttackRole = hurtrole;
			this.totals[0] = 0;
		}
		return damage;
	}

	// Token: 0x06000967 RID: 2407 RVA: 0x00032F88 File Offset: 0x00031188
	public override void Exit()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(playerBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
		this.playerBase.normalAttackAddDamage -= this.addDamage;
		this.addDamage = 0f;
	}

	// Token: 0x04000BBC RID: 3004
	private float addDamage;

	// Token: 0x04000BBD RID: 3005
	private RoleBase curAttackRole;
}

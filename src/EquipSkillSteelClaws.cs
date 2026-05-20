using System;
using UnityEngine;

// Token: 0x020000FB RID: 251
public class EquipSkillSteelClaws : EquipSkillBase
{
	// Token: 0x06000528 RID: 1320 RVA: 0x0001E748 File Offset: 0x0001C948
	public override void Init()
	{
		base.Init();
		PlayerBase playerBase = this.playerBase;
		playerBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(playerBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}

	// Token: 0x06000529 RID: 1321 RVA: 0x0001E778 File Offset: 0x0001C978
	private float AttackEnemyEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		bool isAttackWeek = attackrole.GetIsAttackWeek(AttackType.AttackEffect);
		float num = (float)this.equipNum * this.skillValueAry[0] + this.skillValueUpAry[0] * (float)this.strengLevel;
		Util.OnLocalPlayerHit(attackrole, hurtrole, (double)Mathf.RoundToInt(num * (float)attackrole.armor), 0f, AttackType.AttackEffect, isAttackWeek);
		return damage;
	}

	// Token: 0x0600052A RID: 1322 RVA: 0x0001E7CF File Offset: 0x0001C9CF
	public override void Clear()
	{
		base.Clear();
		PlayerBase playerBase = this.playerBase;
		playerBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(playerBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}
}

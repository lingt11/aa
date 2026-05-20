using System;

// Token: 0x020000E7 RID: 231
public class EquipSkillGodGloves : EquipSkillBase
{
	// Token: 0x060004C4 RID: 1220 RVA: 0x0001CD48 File Offset: 0x0001AF48
	public override void Init()
	{
		base.Init();
		PlayerBase playerBase = this.playerBase;
		playerBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(playerBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}

	// Token: 0x060004C5 RID: 1221 RVA: 0x0001CD78 File Offset: 0x0001AF78
	private float AttackEnemyEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		bool isAttackWeek = attackrole.GetIsAttackWeek(AttackType.AttackEffect);
		float num = ((float)this.equipNum * this.skillValueAry[0] + this.skillValueUpAry[0] * (float)this.strengLevel) * 0.01f;
		Util.OnLocalPlayerHit(attackrole, hurtrole, (double)(num * (float)attackrole.maxHp), 0f, AttackType.AttackEffect, isAttackWeek);
		return damage;
	}

	// Token: 0x060004C6 RID: 1222 RVA: 0x0001CDD0 File Offset: 0x0001AFD0
	public override void Clear()
	{
		base.Clear();
		PlayerBase playerBase = this.playerBase;
		playerBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(playerBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}
}

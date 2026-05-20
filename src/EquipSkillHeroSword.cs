using System;

// Token: 0x020000EB RID: 235
public class EquipSkillHeroSword : EquipSkillBase
{
	// Token: 0x060004D8 RID: 1240 RVA: 0x0001D1DC File Offset: 0x0001B3DC
	public override void Init()
	{
		base.Init();
		PlayerBase playerBase = this.playerBase;
		playerBase.finalAttackEvent = (RoleBase.FinalAttackDamage)Delegate.Combine(playerBase.finalAttackEvent, new RoleBase.FinalAttackDamage(this.AttackEvent));
		this.equipValue = this.skillValueAry[0] * 0.01f;
		this.equipUpValue = this.skillValueUpAry[0] * 0.01f;
		if (this.playerBase.roleType != RoleType.King)
		{
			this.playerBase.addDamagePercent += this.equipValue;
		}
	}

	// Token: 0x060004D9 RID: 1241 RVA: 0x0001D264 File Offset: 0x0001B464
	private float AttackEvent(RoleBase attackrole, RoleBase hurtrole, AttackType attackType, ref float damage)
	{
		if (hurtrole.roleType == RoleType.Enemy && !(hurtrole as EnemyBase).isBoss)
		{
			damage += 100000000f;
		}
		return damage;
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x0001D28C File Offset: 0x0001B48C
	public override void Clear()
	{
		base.Clear();
		PlayerBase playerBase = this.playerBase;
		playerBase.finalAttackEvent = (RoleBase.FinalAttackDamage)Delegate.Remove(playerBase.finalAttackEvent, new RoleBase.FinalAttackDamage(this.AttackEvent));
		this.playerBase.addDamagePercent -= this.equipValue;
	}

	// Token: 0x060004DB RID: 1243 RVA: 0x0001D2DE File Offset: 0x0001B4DE
	public override void OnUpdateStrengLevel(int updateLevel)
	{
		base.OnUpdateStrengLevel(updateLevel);
		this.playerBase.addDamagePercent += (float)updateLevel * this.equipUpValue;
	}

	// Token: 0x04000465 RID: 1125
	private float equipValue;

	// Token: 0x04000466 RID: 1126
	private float equipUpValue;
}

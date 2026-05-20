using System;

// Token: 0x02000102 RID: 258
public class EquipSkillWizardGloves : EquipSkillBase
{
	// Token: 0x0600054D RID: 1357 RVA: 0x0001EE14 File Offset: 0x0001D014
	public override void Init()
	{
		base.Init();
		this.equipValue = this.skillValueAry[0] * 0.01f;
		this.equipUpValue = this.skillValueUpAry[0] * 0.01f;
		PlayerBase playerBase = this.playerBase;
		playerBase.useSkillEvent = (RoleBase.UseSkillEvent)Delegate.Combine(playerBase.useSkillEvent, new RoleBase.UseSkillEvent(this.UseSkillEvent));
		PlayerBase playerBase2 = this.playerBase;
		playerBase2.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(playerBase2.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x0001EEA0 File Offset: 0x0001D0A0
	private ActiveSkillEnum UseSkillEvent(ActiveSkillEnum activeSkillEnum)
	{
		if (!this.isAddDamage)
		{
			this.isAddDamage = true;
			this.playerBase.normalAttackAddDamage -= this.addNormalAttackDamage;
			this.addNormalAttackDamage = this.equipValue + this.equipUpValue * (float)this.strengLevel;
			this.playerBase.normalAttackAddDamage += this.addNormalAttackDamage;
			if (this.playerBase.isLocalPlayer)
			{
				this.roleBuff = GameHelperClient.AddShowBuff(Game.Language.Get(PathDefine.Concat("equip_", this.equipIndex), ""), GameHelperClient.localPlayer.playerAttribute.GetEquipSkillInfo(this.equipSkillType), PathDefine.Concat("Shop/equip_", this.equipIndex), -1f);
			}
		}
		return activeSkillEnum;
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x0001EF6C File Offset: 0x0001D16C
	private float AttackEnemyEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		if (this.isAddDamage)
		{
			this.isAddDamage = false;
			this.playerBase.normalAttackAddDamage -= this.addNormalAttackDamage;
			this.addNormalAttackDamage = 0f;
			if (this.roleBuff != null)
			{
				this.playerBase.roleBuffManager.RemoveBuff(this.roleBuff);
				this.roleBuff = null;
			}
		}
		return damage;
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x0001EFD4 File Offset: 0x0001D1D4
	public override void Clear()
	{
		base.Clear();
		this.playerBase.normalAttackAddDamage -= this.addNormalAttackDamage;
		PlayerBase playerBase = this.playerBase;
		playerBase.useSkillEvent = (RoleBase.UseSkillEvent)Delegate.Remove(playerBase.useSkillEvent, new RoleBase.UseSkillEvent(this.UseSkillEvent));
		PlayerBase playerBase2 = this.playerBase;
		playerBase2.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(playerBase2.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
		if (this.roleBuff != null)
		{
			this.playerBase.roleBuffManager.RemoveBuff(this.roleBuff);
			this.roleBuff = null;
		}
	}

	// Token: 0x0400048E RID: 1166
	private bool isAddDamage;

	// Token: 0x0400048F RID: 1167
	private float addNormalAttackDamage;

	// Token: 0x04000490 RID: 1168
	private RoleBuff roleBuff;

	// Token: 0x04000491 RID: 1169
	private float equipValue;

	// Token: 0x04000492 RID: 1170
	private float equipUpValue;
}

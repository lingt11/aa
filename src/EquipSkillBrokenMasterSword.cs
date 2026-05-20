using System;

// Token: 0x020000E1 RID: 225
public class EquipSkillBrokenMasterSword : EquipEventBase
{
	// Token: 0x060004AA RID: 1194 RVA: 0x0001C4FC File Offset: 0x0001A6FC
	public override void Init(EquipBase equipBaseValue)
	{
		base.Init(equipBaseValue);
		PlayerBase playerBase = this.playerBase;
		playerBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Combine(playerBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
		this.equipBase.totals = new int[1];
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x0001C548 File Offset: 0x0001A748
	public override void Clear()
	{
		base.Clear();
		PlayerBase playerBase = this.playerBase;
		playerBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Remove(playerBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x0001C578 File Offset: 0x0001A778
	private void KillEvent(RoleBase attackrole, RoleBase hurtrole)
	{
		EnemyBase enemyBase = hurtrole as EnemyBase;
		if (enemyBase != null && enemyBase.isBoss)
		{
			this.killCount++;
			if (this.killCount == 3)
			{
				int level = this.equipBase.level;
				this.playerBase.playerAttribute.SellEquip(this.equipBase, true);
				EquipBase equipBase = ShopManager.OnBuyEquipSuccess("equip_1000", 0, null);
				if (level > 0)
				{
					equipBase.OnLevelUpSuccess(false, level);
				}
				Util.ShowTips("剑之试炼已完成");
				return;
			}
			this.equipBase.totals[0] = this.killCount;
		}
	}

	// Token: 0x04000452 RID: 1106
	private int killCount;
}

using System;
using System.Collections.Generic;

// Token: 0x0200021E RID: 542
public class RelicHeavySword : RelicBase
{
	// Token: 0x060009D4 RID: 2516 RVA: 0x00034869 File Offset: 0x00032A69
	public override void Enter()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.onEquipChange = (RoleBase.OnEquipChange)Delegate.Combine(playerBase.onEquipChange, new RoleBase.OnEquipChange(this.OnEquipChange));
		this.OnEquipChange();
	}

	// Token: 0x060009D5 RID: 2517 RVA: 0x00034898 File Offset: 0x00032A98
	private void OnEquipChange()
	{
		if (this.isCreate)
		{
			return;
		}
		List<EquipBase> equipList = this.playerBase.playerAttribute.equipList;
		int count = equipList.Count;
		bool flag = false;
		bool flag2 = false;
		for (int i = count - 1; i > -1; i--)
		{
			EquipBase equipBase = equipList[i];
			if (equipBase.equipIndex.Equals("111"))
			{
				flag = true;
			}
			else if (equipBase.equipIndex.Equals("112"))
			{
				flag2 = true;
			}
		}
		int num = 0;
		if (flag2 && flag)
		{
			for (int j = count - 1; j > -1; j--)
			{
				EquipBase equipBase2 = equipList[j];
				if (equipBase2.equipIndex.Equals("111"))
				{
					if (flag)
					{
						flag = false;
						num += equipBase2.level;
						this.playerBase.playerAttribute.SellEquip(equipBase2, true);
					}
				}
				else if (equipBase2.equipIndex.Equals("112") && flag2)
				{
					flag2 = false;
					num += equipBase2.level;
					this.playerBase.playerAttribute.SellEquip(equipBase2, true);
				}
			}
			ShopManager.OnBuyEquipSuccess("equip_1001", 0, null).OnLevelUpSuccess(false, num);
			Util.ShowTips("获得《玄铁重剑》");
		}
	}

	// Token: 0x060009D6 RID: 2518 RVA: 0x000349CD File Offset: 0x00032BCD
	public override void Exit()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.onEquipChange = (RoleBase.OnEquipChange)Delegate.Remove(playerBase.onEquipChange, new RoleBase.OnEquipChange(this.OnEquipChange));
	}

	// Token: 0x04000BCD RID: 3021
	private bool isCreate;
}

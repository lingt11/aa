using System;

// Token: 0x02000163 RID: 355
public class MedicineSkillDamage : MedicineBase
{
	// Token: 0x060006F1 RID: 1777 RVA: 0x0002A867 File Offset: 0x00028A67
	public override void Init(ShopItem shopItemValue, PlayerBase playerBaseValue)
	{
		base.Init(shopItemValue, playerBaseValue);
		this.playerBase.skillExDamage += this.shopItem.values[0] * 0.01f;
	}

	// Token: 0x060006F2 RID: 1778 RVA: 0x0002A896 File Offset: 0x00028A96
	public override void Clear()
	{
		this.playerBase.skillExDamage -= this.shopItem.values[0] * 0.01f;
		base.Clear();
	}
}

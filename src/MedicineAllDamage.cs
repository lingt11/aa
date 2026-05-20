using System;

// Token: 0x0200015B RID: 347
public class MedicineAllDamage : MedicineBase
{
	// Token: 0x060006D8 RID: 1752 RVA: 0x0002A3E6 File Offset: 0x000285E6
	public override void Init(ShopItem shopItemValue, PlayerBase playerBaseValue)
	{
		base.Init(shopItemValue, playerBaseValue);
		this.playerBase.addDamagePercent += this.shopItem.values[0] * 0.01f;
	}

	// Token: 0x060006D9 RID: 1753 RVA: 0x0002A415 File Offset: 0x00028615
	public override void Clear()
	{
		this.playerBase.addDamagePercent -= this.shopItem.values[0] * 0.01f;
		base.Clear();
	}
}

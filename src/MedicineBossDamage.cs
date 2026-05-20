using System;

// Token: 0x02000160 RID: 352
public class MedicineBossDamage : MedicineBase
{
	// Token: 0x060006E6 RID: 1766 RVA: 0x0002A6B7 File Offset: 0x000288B7
	public override void Init(ShopItem shopItemValue, PlayerBase playerBaseValue)
	{
		base.Init(shopItemValue, playerBaseValue);
		this.playerBase.addBossEnemy += this.shopItem.values[0] * 0.01f;
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x0002A6E6 File Offset: 0x000288E6
	public override void Clear()
	{
		this.playerBase.addBossEnemy -= this.shopItem.values[0] * 0.01f;
		base.Clear();
	}
}

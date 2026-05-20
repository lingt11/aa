using System;

// Token: 0x0200015D RID: 349
public class MedicineAttackAdd : MedicineBase
{
	// Token: 0x060006DE RID: 1758 RVA: 0x0002A497 File Offset: 0x00028697
	public override void Init(ShopItem shopItemValue, PlayerBase playerBaseValue)
	{
		base.Init(shopItemValue, playerBaseValue);
		this.playerBase.UpdateAttackPercent(this.shopItem.values[0] * 0.01f);
	}

	// Token: 0x060006DF RID: 1759 RVA: 0x0002A4BF File Offset: 0x000286BF
	public override void Clear()
	{
		this.playerBase.UpdateAttackPercent(-this.shopItem.values[0] * 0.01f);
		base.Clear();
	}
}

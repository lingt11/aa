using System;
using UnityEngine;

// Token: 0x0200015C RID: 348
public class MedicineArmor : MedicineBase
{
	// Token: 0x060006DB RID: 1755 RVA: 0x0002A44A File Offset: 0x0002864A
	public override void Init(ShopItem shopItemValue, PlayerBase playerBaseValue)
	{
		base.Init(shopItemValue, playerBaseValue);
		this.playerBase.AddArmor(Mathf.RoundToInt(this.shopItem.values[0]));
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x0002A471 File Offset: 0x00028671
	public override void Clear()
	{
		this.playerBase.AddArmor(-Mathf.RoundToInt(this.shopItem.values[0]));
		base.Clear();
	}
}

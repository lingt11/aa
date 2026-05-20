using System;
using UnityEngine;

// Token: 0x02000161 RID: 353
public class MedicineLuck : MedicineBase
{
	// Token: 0x060006E9 RID: 1769 RVA: 0x0002A713 File Offset: 0x00028913
	public override void Init(ShopItem shopItemValue, PlayerBase playerBaseValue)
	{
		base.Init(shopItemValue, playerBaseValue);
		this.playerBase.CmdUpdateLucky(Mathf.RoundToInt(this.shopItem.values[0]));
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x0002A73A File Offset: 0x0002893A
	public override void Clear()
	{
		this.playerBase.CmdUpdateLucky(-Mathf.RoundToInt(this.shopItem.values[0]));
		base.Clear();
	}
}

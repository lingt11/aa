using System;

// Token: 0x02000210 RID: 528
public class RelicDiscount : RelicBase
{
	// Token: 0x06000999 RID: 2457 RVA: 0x00033B84 File Offset: 0x00031D84
	public override void Enter()
	{
		this.lastShopDiscount = base.GetValue(0, 0.9f);
		this.playerBase.AddShopDiscount(this.lastShopDiscount);
		Game.UI.GetUI<UI_Shop>().OnBtnItemClick();
	}

	// Token: 0x0600099A RID: 2458 RVA: 0x00033BB8 File Offset: 0x00031DB8
	public override void Exit()
	{
		this.playerBase.RemoveShopDiscount(this.lastShopDiscount);
		Game.UI.GetUI<UI_Shop>().OnBtnItemClick();
	}

	// Token: 0x0600099B RID: 2459 RVA: 0x00033BDC File Offset: 0x00031DDC
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.RemoveShopDiscount(this.lastShopDiscount);
		this.lastShopDiscount = base.GetValue(0, 0.9f);
		this.playerBase.AddShopDiscount(this.lastShopDiscount);
		Game.UI.GetUI<UI_Shop>().OnBtnItemClick();
	}

	// Token: 0x04000BC5 RID: 3013
	private float lastShopDiscount;
}

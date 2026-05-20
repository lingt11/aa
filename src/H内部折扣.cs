using System;

// Token: 0x020001BD RID: 445
public class H内部折扣 : PasssiveSkill
{
	// Token: 0x0600083B RID: 2107 RVA: 0x0002F427 File Offset: 0x0002D627
	public override void Enter()
	{
		GameHelperClient.localPlayer.AddShopDiscount(this.skillValues[0] * 0.01f);
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x0002F441 File Offset: 0x0002D641
	public override void Exit()
	{
		GameHelperClient.localPlayer.RemoveShopDiscount(this.skillValues[0] * 0.01f);
	}
}

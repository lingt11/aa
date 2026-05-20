using System;

// Token: 0x020001B3 RID: 435
public class D微小吸收 : PasssiveSkill
{
	// Token: 0x06000819 RID: 2073 RVA: 0x0002ED74 File Offset: 0x0002CF74
	public override void Enter()
	{
		this.totalName = Game.Language.Get(PathDefine.Concat("p_", this.skillId, StringDefine.Total), "");
		this.totals = new int[1];
		PlayerBase roleBase = this.roleBase;
		roleBase.buyItemEvent = (PlayerBase.BuyItem)Delegate.Combine(roleBase.buyItemEvent, new PlayerBase.BuyItem(this.BuyItemEvent));
	}

	// Token: 0x0600081A RID: 2074 RVA: 0x0002EDDE File Offset: 0x0002CFDE
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.buyItemEvent = (PlayerBase.BuyItem)Delegate.Remove(roleBase.buyItemEvent, new PlayerBase.BuyItem(this.BuyItemEvent));
	}

	// Token: 0x0600081B RID: 2075 RVA: 0x0002EE08 File Offset: 0x0002D008
	private void BuyItemEvent()
	{
		int num = 5;
		GameHelperClient.localPlayer.AddMaxMp(num);
		this.totals[0] += num;
	}
}

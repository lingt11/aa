using System;

// Token: 0x02000218 RID: 536
public class RelicForgingAdd : RelicBase
{
	// Token: 0x060009BC RID: 2492 RVA: 0x000343DB File Offset: 0x000325DB
	public override void Enter()
	{
		base.Enter();
		EntityStatic.Get<ShopManager>().forgingManager.UpdateForgingAdd(base.GetValue(0, 0.8f));
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x000343FE File Offset: 0x000325FE
	public override void Exit()
	{
		base.Exit();
		EntityStatic.Get<ShopManager>().forgingManager.UpdateForgingAdd(-base.GetValue(0, 0.8f));
	}

	// Token: 0x060009BE RID: 2494 RVA: 0x00034422 File Offset: 0x00032622
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		EntityStatic.Get<ShopManager>().forgingManager.UpdateForgingAdd(base.GetLevelValueDelta(0, 0.35f, oldLevel, newLevel));
	}
}

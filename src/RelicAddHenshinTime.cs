using System;

// Token: 0x020001F4 RID: 500
public class RelicAddHenshinTime : RelicBase
{
	// Token: 0x060008FF RID: 2303 RVA: 0x00031F28 File Offset: 0x00030128
	public override void Enter()
	{
		this.playerBase.UpdateAddHenshinTime(base.GetValue(0, 0.5f));
	}

	// Token: 0x06000900 RID: 2304 RVA: 0x00031F41 File Offset: 0x00030141
	public override void Exit()
	{
		this.playerBase.UpdateAddHenshinTime(-base.GetValue(0, 0.5f));
	}

	// Token: 0x06000901 RID: 2305 RVA: 0x00031F5B File Offset: 0x0003015B
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.UpdateAddHenshinTime(base.GetLevelValueDelta(0, 0.5f, oldLevel, newLevel));
	}
}

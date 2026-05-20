using System;

// Token: 0x02000222 RID: 546
public class RelicLightMan : RelicBase
{
	// Token: 0x060009E4 RID: 2532 RVA: 0x00034AFC File Offset: 0x00032CFC
	public override void Enter()
	{
		this.playerBase.skillLightingAdd += base.GetValue(0, 0.35f);
	}

	// Token: 0x060009E5 RID: 2533 RVA: 0x00034B1C File Offset: 0x00032D1C
	public override void Exit()
	{
		this.playerBase.skillLightingAdd -= base.GetValue(0, 0.35f);
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x00034B3C File Offset: 0x00032D3C
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.skillLightingAdd += base.GetLevelValueDelta(0, 0.35f, oldLevel, newLevel);
	}
}

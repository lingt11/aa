using System;

// Token: 0x02000228 RID: 552
public class RelicMagicXiXue : RelicBase
{
	// Token: 0x060009FC RID: 2556 RVA: 0x00034ED1 File Offset: 0x000330D1
	public override void Enter()
	{
		this.playerBase.magicXiXue += base.GetValue(0, 0.025f);
	}

	// Token: 0x060009FD RID: 2557 RVA: 0x00034EF1 File Offset: 0x000330F1
	public override void Exit()
	{
		this.playerBase.magicXiXue -= base.GetValue(0, 0.025f);
	}

	// Token: 0x060009FE RID: 2558 RVA: 0x00034F11 File Offset: 0x00033111
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.magicXiXue += base.GetLevelValueDelta(0, 0.025f, oldLevel, newLevel);
	}
}

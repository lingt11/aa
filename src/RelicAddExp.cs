using System;

// Token: 0x020001F0 RID: 496
public class RelicAddExp : RelicBase
{
	// Token: 0x060008F0 RID: 2288 RVA: 0x00031DBC File Offset: 0x0002FFBC
	public override void Enter()
	{
		this.playerBase.addExp += base.GetValue(0, 0.25f);
	}

	// Token: 0x060008F1 RID: 2289 RVA: 0x00031DDC File Offset: 0x0002FFDC
	public override void Exit()
	{
		this.playerBase.addExp -= base.GetValue(0, 0.25f);
	}

	// Token: 0x060008F2 RID: 2290 RVA: 0x00031DFC File Offset: 0x0002FFFC
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.addExp += base.GetLevelValueDelta(0, 0.25f, oldLevel, newLevel);
	}
}

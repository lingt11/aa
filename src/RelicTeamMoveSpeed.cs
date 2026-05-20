using System;

// Token: 0x0200024B RID: 587
public class RelicTeamMoveSpeed : RelicBase
{
	// Token: 0x06000A8C RID: 2700 RVA: 0x000366FA File Offset: 0x000348FA
	public override void Enter()
	{
		this.playerBase.AddMoveSpeed(base.GetValue(0, 1f));
	}

	// Token: 0x06000A8D RID: 2701 RVA: 0x00036713 File Offset: 0x00034913
	public override void Exit()
	{
		this.playerBase.AddMoveSpeed(-base.GetValue(0, 1f));
	}

	// Token: 0x06000A8E RID: 2702 RVA: 0x0003672D File Offset: 0x0003492D
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.AddMoveSpeed(base.GetLevelValueDelta(0, 1f, oldLevel, newLevel));
	}
}

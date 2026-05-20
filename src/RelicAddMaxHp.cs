using System;

// Token: 0x020001F6 RID: 502
public class RelicAddMaxHp : RelicBase
{
	// Token: 0x06000908 RID: 2312 RVA: 0x000320D5 File Offset: 0x000302D5
	public override void Enter()
	{
		this.playerBase.CmdUpdateMaxHp((long)base.GetIntValue(0, 750), this.playerBase.netId);
	}

	// Token: 0x06000909 RID: 2313 RVA: 0x000320FA File Offset: 0x000302FA
	public override void Exit()
	{
		this.playerBase.CmdUpdateMaxHp((long)(-(long)base.GetIntValue(0, 750)), this.playerBase.netId);
	}

	// Token: 0x0600090A RID: 2314 RVA: 0x00032120 File Offset: 0x00030320
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.CmdUpdateMaxHp((long)base.GetLevelIntValueDelta(0, 750, oldLevel, newLevel), this.playerBase.netId);
	}
}

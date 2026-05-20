using System;

// Token: 0x020001FC RID: 508
public class RelicArmedAdd : RelicBase
{
	// Token: 0x06000920 RID: 2336 RVA: 0x000323B7 File Offset: 0x000305B7
	public override void Enter()
	{
		this.playerBase.armedAdd += base.GetValue(0, 0.3f);
	}

	// Token: 0x06000921 RID: 2337 RVA: 0x000323D7 File Offset: 0x000305D7
	public override void Exit()
	{
		this.playerBase.armedAdd -= base.GetValue(0, 0.3f);
	}

	// Token: 0x06000922 RID: 2338 RVA: 0x000323F7 File Offset: 0x000305F7
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.armedAdd += base.GetLevelValueDelta(0, 0.3f, oldLevel, newLevel);
	}
}

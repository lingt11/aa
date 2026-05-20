using System;

// Token: 0x02000251 RID: 593
public class RelicTimeRedMoney : RelicBase
{
	// Token: 0x06000AA6 RID: 2726 RVA: 0x00036BDC File Offset: 0x00034DDC
	public override void Enter()
	{
		this.totals = new int[]
		{
			base.GetIntValue(0, 6000)
		};
		this.playerBase.AddGold(this.playerBase.GetHeadUIPos(), base.GetIntValue(0, 6000), true);
		MySystemEvent.Instance.RegisterMessage(38, new Action<Body>(this.OnWaveLevelUp));
	}

	// Token: 0x06000AA7 RID: 2727 RVA: 0x00036C40 File Offset: 0x00034E40
	private void OnWaveLevelUp(Body body)
	{
		this.playerBase.AddGold(this.playerBase.GetHeadUIPos(), -base.GetIntValue(1, 500), true);
		this.totals[0] -= base.GetIntValue(1, 500);
	}

	// Token: 0x06000AA8 RID: 2728 RVA: 0x00036C8E File Offset: 0x00034E8E
	public override void Exit()
	{
		base.Exit();
		MySystemEvent.Instance.UnregisterMessage(38, new Action<Body>(this.OnWaveLevelUp));
	}

	// Token: 0x06000AA9 RID: 2729 RVA: 0x00036CB0 File Offset: 0x00034EB0
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		int levelIntValueDelta = base.GetLevelIntValueDelta(0, 500, oldLevel, newLevel);
		this.playerBase.AddGold(this.playerBase.GetHeadUIPos(), levelIntValueDelta, true);
		this.totals[0] += levelIntValueDelta;
	}
}

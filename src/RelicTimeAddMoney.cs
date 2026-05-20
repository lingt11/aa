using System;

// Token: 0x02000250 RID: 592
public class RelicTimeAddMoney : RelicBase
{
	// Token: 0x06000AA1 RID: 2721 RVA: 0x00036AA8 File Offset: 0x00034CA8
	public override void Enter()
	{
		this.totals = new int[1];
		int intValue = base.GetIntValue(0, 500);
		this.playerBase.AddGold(this.playerBase.GetHeadUIPos(), intValue, true);
		this.totals[0] += intValue;
		MySystemEvent.Instance.RegisterMessage(38, new Action<Body>(this.OnWaveLevelUp));
	}

	// Token: 0x06000AA2 RID: 2722 RVA: 0x00036B10 File Offset: 0x00034D10
	private void OnWaveLevelUp(Body body)
	{
		this.addTime++;
		int num = base.GetIntValue(0, 500) + this.addTime * base.GetIntValue(1, 100);
		this.playerBase.AddGold(this.playerBase.GetHeadUIPos(), num, true);
		this.totals[0] += num;
	}

	// Token: 0x06000AA3 RID: 2723 RVA: 0x00036B73 File Offset: 0x00034D73
	public override void Exit()
	{
		base.Exit();
		MySystemEvent.Instance.UnregisterMessage(38, new Action<Body>(this.OnWaveLevelUp));
	}

	// Token: 0x06000AA4 RID: 2724 RVA: 0x00036B94 File Offset: 0x00034D94
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		int levelIntValueDelta = base.GetLevelIntValueDelta(0, 500, oldLevel, newLevel);
		this.playerBase.AddGold(this.playerBase.GetHeadUIPos(), levelIntValueDelta, true);
		this.totals[0] += levelIntValueDelta;
	}

	// Token: 0x04000BEB RID: 3051
	private int addTime;
}

using System;
using UnityEngine;

// Token: 0x02000208 RID: 520
public class RelicBrokeWind : RelicBase
{
	// Token: 0x06000977 RID: 2423 RVA: 0x00033544 File Offset: 0x00031744
	public override void Enter()
	{
		this.isTotalPercent = true;
		this.totals = new int[]
		{
			-Mathf.RoundToInt(base.GetValue(0, 0.25f) * 100f)
		};
		this.playerBase.addDamagePercent -= base.GetValue(0, 0.25f);
		MySystemEvent.Instance.RegisterMessage(38, new Action<Body>(this.OnWaveLevelUp));
	}

	// Token: 0x06000978 RID: 2424 RVA: 0x000335B8 File Offset: 0x000317B8
	private void OnWaveLevelUp(Body body)
	{
		this.playerBase.addDamagePercent += base.GetValue(1, 0.05f);
		this.totals[0] += Mathf.RoundToInt(base.GetValue(1, 0.05f) * 100f);
	}

	// Token: 0x06000979 RID: 2425 RVA: 0x0003360A File Offset: 0x0003180A
	public override void Exit()
	{
		base.Exit();
		MySystemEvent.Instance.UnregisterMessage(38, new Action<Body>(this.OnWaveLevelUp));
	}

	// Token: 0x0600097A RID: 2426 RVA: 0x0003362C File Offset: 0x0003182C
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.addDamagePercent += base.GetLevelValueDelta(0, 0.25f, oldLevel, newLevel);
		this.totals[0] += Mathf.RoundToInt(base.GetLevelValueDelta(0, 0.25f, oldLevel, newLevel) * 100f);
	}
}

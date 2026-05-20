using System;
using UnityEngine;

// Token: 0x0200020E RID: 526
public class RelicDeathContract : RelicBase
{
	// Token: 0x06000991 RID: 2449 RVA: 0x00033994 File Offset: 0x00031B94
	public override void Enter()
	{
		this.isTotalPercent = true;
		this.totals = new int[]
		{
			Mathf.RoundToInt(base.GetValue(0, 0.5f) * 100f)
		};
		this.playerBase.addDamagePercent += base.GetValue(0, 0.5f);
		MySystemEvent.Instance.RegisterMessage(38, new Action<Body>(this.OnWaveLevelUp));
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x00033A04 File Offset: 0x00031C04
	private void OnWaveLevelUp(Body body)
	{
		this.playerBase.addDamagePercent -= base.GetValue(1, 0.05f);
		this.totals[0] -= Mathf.RoundToInt(base.GetValue(1, 0.05f) * 100f);
	}

	// Token: 0x06000993 RID: 2451 RVA: 0x00033A56 File Offset: 0x00031C56
	public override void Exit()
	{
		base.Exit();
		MySystemEvent.Instance.UnregisterMessage(38, new Action<Body>(this.OnWaveLevelUp));
	}

	// Token: 0x06000994 RID: 2452 RVA: 0x00033A78 File Offset: 0x00031C78
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.addDamagePercent += base.GetLevelValueDelta(0, 0.5f, oldLevel, newLevel);
		this.totals[0] += Mathf.RoundToInt(base.GetLevelValueDelta(0, 0.5f, oldLevel, newLevel) * 100f);
	}
}

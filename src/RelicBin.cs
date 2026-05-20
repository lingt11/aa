using System;
using UnityEngine;

// Token: 0x02000206 RID: 518
public class RelicBin : RelicBase
{
	// Token: 0x0600096D RID: 2413 RVA: 0x000330AE File Offset: 0x000312AE
	public override void Enter()
	{
		this.playerBase.AddCritical(base.GetValue(0, 0.5f));
		this.isTotalPercent = true;
		this.totals = new int[1];
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x000330DC File Offset: 0x000312DC
	public override void Update()
	{
		base.Update();
		if (this.checkTime < 0.5f)
		{
			this.checkTime += Time.deltaTime;
			return;
		}
		this.checkTime = 0f;
		if (Mathf.Approximately(this.changeValue, 0f))
		{
			if (this.playerBase.critical > base.GetValue(1, 1f))
			{
				this.changeValue = this.playerBase.critical - base.GetValue(1, 1f);
				this.playerBase.AddCritical(-this.changeValue);
				this.playerBase.AddCriticalDamage(this.changeValue);
				this.totals[0] += Mathf.RoundToInt(this.changeValue * 100f);
				return;
			}
		}
		else if (this.changeValue > 0f)
		{
			if (this.playerBase.critical - base.GetValue(1, 1f) < -0.01f)
			{
				float num = base.GetValue(1, 1f) - this.playerBase.critical;
				num = Mathf.Min(num, this.changeValue);
				this.changeValue -= num;
				this.playerBase.AddCritical(num);
				this.playerBase.AddCriticalDamage(-num);
				this.totals[0] += Mathf.RoundToInt(-num * 100f);
				return;
			}
			if (this.playerBase.critical > base.GetValue(1, 1f))
			{
				float num2 = this.playerBase.critical - base.GetValue(1, 1f);
				this.changeValue += num2;
				this.playerBase.AddCritical(-num2);
				this.playerBase.AddCriticalDamage(num2);
				this.totals[0] += Mathf.RoundToInt(num2 * 100f);
			}
		}
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x000332BC File Offset: 0x000314BC
	public override void Exit()
	{
		this.playerBase.AddCritical(-base.GetValue(0, 0.5f));
		if (this.changeValue > 0f)
		{
			this.playerBase.AddCritical(this.changeValue);
			this.playerBase.AddCriticalDamage(-this.changeValue);
		}
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x00033311 File Offset: 0x00031511
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.playerBase.AddCritical(base.GetLevelValueDelta(0, 0.35f, oldLevel, newLevel));
	}

	// Token: 0x04000BBE RID: 3006
	private float checkTime;

	// Token: 0x04000BBF RID: 3007
	private float changeValue;
}

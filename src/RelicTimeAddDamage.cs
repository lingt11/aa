using System;
using UnityEngine;

// Token: 0x0200024F RID: 591
public class RelicTimeAddDamage : RelicBase
{
	// Token: 0x06000A9C RID: 2716 RVA: 0x000368D8 File Offset: 0x00034AD8
	public override void Enter()
	{
		this.isTotalPercent = true;
		this.totals = new int[1];
		MySystemEvent.Instance.RegisterMessage(38, new Action<Body>(this.OnWaveLevelUp));
	}

	// Token: 0x06000A9D RID: 2717 RVA: 0x00036908 File Offset: 0x00034B08
	public override void Update()
	{
		base.Update();
		if (GameHelperClient.isReady)
		{
			if (this.addDamage > 0f)
			{
				this.playerBase.addDamagePercent -= this.addDamage;
				this.addDamage = 0f;
				this.totals[0] = Mathf.RoundToInt(this.addDamage * 100f);
			}
			return;
		}
		this.timer += Time.deltaTime;
		if (this.timer >= (float)this.addNum)
		{
			this.addNum++;
			float value = base.GetValue(0, 0.005f);
			this.playerBase.addDamagePercent += value;
			this.addDamage += value;
			this.totals[0] = Mathf.RoundToInt(this.addDamage * 100f);
		}
	}

	// Token: 0x06000A9E RID: 2718 RVA: 0x000369E4 File Offset: 0x00034BE4
	private void OnWaveLevelUp(Body body)
	{
		this.timer = 0f;
		this.addNum = 1;
		if (this.addDamage > 0f)
		{
			this.playerBase.addDamagePercent -= this.addDamage;
			this.addDamage = 0f;
			this.totals[0] = Mathf.RoundToInt(this.addDamage * 100f);
		}
	}

	// Token: 0x06000A9F RID: 2719 RVA: 0x00036A4C File Offset: 0x00034C4C
	public override void Exit()
	{
		base.Exit();
		MySystemEvent.Instance.UnregisterMessage(38, new Action<Body>(this.OnWaveLevelUp));
		if (this.addDamage > 0f)
		{
			this.playerBase.addDamagePercent -= this.addDamage;
			this.addDamage = 0f;
		}
	}

	// Token: 0x04000BE8 RID: 3048
	private float addDamage;

	// Token: 0x04000BE9 RID: 3049
	private float timer;

	// Token: 0x04000BEA RID: 3050
	private int addNum;
}

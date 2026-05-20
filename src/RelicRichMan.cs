using System;
using UnityEngine;

// Token: 0x02000234 RID: 564
public class RelicRichMan : RelicBase
{
	// Token: 0x06000A30 RID: 2608 RVA: 0x00034441 File Offset: 0x00032641
	public override void Enter()
	{
		base.Enter();
		this.isTotalPercent = true;
		this.totals = new int[1];
	}

	// Token: 0x06000A31 RID: 2609 RVA: 0x0003585C File Offset: 0x00033A5C
	public override void Update()
	{
		base.Update();
		if (this.checkTime < 0.3f)
		{
			this.checkTime += Time.deltaTime;
			return;
		}
		this.playerBase.addDamagePercent -= this.addDamage;
		this.addDamage = (float)this.playerBase.gold / base.GetValue(0, 500f) * base.GetValue(1, 0.01f);
		this.playerBase.addDamagePercent += this.addDamage;
		this.checkTime = 0f;
		this.totals[0] = Mathf.RoundToInt(this.addDamage * 100f);
	}

	// Token: 0x06000A32 RID: 2610 RVA: 0x0003590F File Offset: 0x00033B0F
	public override void Exit()
	{
		this.playerBase.addDamagePercent -= this.addDamage;
	}

	// Token: 0x04000BD8 RID: 3032
	private float addDamage;

	// Token: 0x04000BD9 RID: 3033
	private float checkTime;
}

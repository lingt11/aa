using System;
using UnityEngine;

// Token: 0x0200024E RID: 590
public class RelicThreeTerms : RelicBase
{
	// Token: 0x06000A98 RID: 2712 RVA: 0x00002D1D File Offset: 0x00000F1D
	public override void Enter()
	{
	}

	// Token: 0x06000A99 RID: 2713 RVA: 0x00036814 File Offset: 0x00034A14
	public override void Update()
	{
		base.Update();
		if (this.checkTime < 0.3f)
		{
			this.checkTime += Time.deltaTime;
			return;
		}
		this.checkTime = 0f;
		this.playerBase.extraDamage -= this.addDamage;
		this.addDamage = Mathf.RoundToInt((float)(this.playerBase.STA + this.playerBase.STR + this.playerBase.AGI) * base.GetValue(0, 0.35f));
		this.playerBase.extraDamage += this.addDamage;
	}

	// Token: 0x06000A9A RID: 2714 RVA: 0x000368BE File Offset: 0x00034ABE
	public override void Exit()
	{
		this.playerBase.extraDamage -= this.addDamage;
	}

	// Token: 0x04000BE6 RID: 3046
	private int addDamage;

	// Token: 0x04000BE7 RID: 3047
	private float checkTime;
}

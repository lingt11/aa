using System;
using UnityEngine;

// Token: 0x02000219 RID: 537
public class RelicFullyArmed : RelicBase
{
	// Token: 0x060009C0 RID: 2496 RVA: 0x00034441 File Offset: 0x00032641
	public override void Enter()
	{
		base.Enter();
		this.isTotalPercent = true;
		this.totals = new int[1];
	}

	// Token: 0x060009C1 RID: 2497 RVA: 0x0003445C File Offset: 0x0003265C
	public override void Update()
	{
		base.Update();
		if (this.checkTime < 0.3f)
		{
			this.checkTime += Time.deltaTime;
			return;
		}
		this.checkTime = 0f;
		this.playerBase.addDamagePercent -= this.addDamage;
		this.addDamage = (float)this.playerBase.GetBrotatoWeaponCount() * base.GetValue(0, 0.15f);
		this.playerBase.addDamagePercent += this.addDamage;
		this.totals[0] = Mathf.RoundToInt(this.addDamage * 100f);
	}

	// Token: 0x060009C2 RID: 2498 RVA: 0x00034502 File Offset: 0x00032702
	public override void Exit()
	{
		this.playerBase.addDamagePercent -= this.addDamage;
	}

	// Token: 0x04000BCB RID: 3019
	private float addDamage;

	// Token: 0x04000BCC RID: 3020
	private float checkTime;
}

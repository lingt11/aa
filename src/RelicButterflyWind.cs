using System;
using UnityEngine;

// Token: 0x0200020A RID: 522
public class RelicButterflyWind : RelicBase
{
	// Token: 0x06000980 RID: 2432 RVA: 0x0003370E File Offset: 0x0003190E
	public override void Enter()
	{
		base.Enter();
		if (this.playerBase.roleType == RoleType.King)
		{
			this.addDamage = base.GetValue(1, 0.25f);
		}
	}

	// Token: 0x06000981 RID: 2433 RVA: 0x00033738 File Offset: 0x00031938
	public override void Update()
	{
		base.Update();
		if (this.checkTime < 0.1f)
		{
			this.checkTime += Time.deltaTime;
			return;
		}
		this.checkTime = 0f;
		if ((float)this.playerBase.hp / (float)this.playerBase.maxHp > base.GetValue(0, 0.9f))
		{
			this.playerBase.addDamagePercent -= this.addDamage;
			this.addDamage = base.GetValue(1, 0.25f);
			this.playerBase.addDamagePercent += this.addDamage;
			return;
		}
		this.playerBase.addDamagePercent -= this.addDamage;
		this.addDamage = 0f;
	}

	// Token: 0x06000982 RID: 2434 RVA: 0x00033803 File Offset: 0x00031A03
	public override void Exit()
	{
		this.playerBase.addDamagePercent -= this.addDamage;
	}

	// Token: 0x04000BC2 RID: 3010
	private float addDamage;

	// Token: 0x04000BC3 RID: 3011
	private float checkTime;
}

using System;
using UnityEngine;

// Token: 0x02000237 RID: 567
public class RelicScopes : RelicBase
{
	// Token: 0x06000A3C RID: 2620 RVA: 0x00035C14 File Offset: 0x00033E14
	public override void Enter()
	{
		base.Enter();
		this.isTotalPercent = true;
		this.totals = new int[1];
		if (this.playerBase.roleType == RoleType.King)
		{
			this.addDamage = this.playerBase.exAttackDistance * base.GetValue(0, 0.1f);
		}
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x00035C68 File Offset: 0x00033E68
	public override void Update()
	{
		base.Update();
		if (this.checkTime < 0.3f)
		{
			this.checkTime += Time.deltaTime;
			return;
		}
		this.checkTime = 0f;
		this.playerBase.normalAttackAddDamage -= this.addDamage;
		this.addDamage = this.playerBase.exAttackDistance * base.GetValue(0, 0.1f);
		this.playerBase.normalAttackAddDamage += this.addDamage;
		this.totals[0] = Mathf.RoundToInt(this.addDamage * 100f);
	}

	// Token: 0x06000A3E RID: 2622 RVA: 0x00035D0D File Offset: 0x00033F0D
	public override void Exit()
	{
		this.playerBase.normalAttackAddDamage -= this.addDamage;
	}

	// Token: 0x04000BE0 RID: 3040
	private float addDamage;

	// Token: 0x04000BE1 RID: 3041
	private float checkTime;
}

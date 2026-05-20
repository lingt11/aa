using System;
using UnityEngine;

// Token: 0x0200023D RID: 573
public class RelicSpeedMan : RelicBase
{
	// Token: 0x06000A54 RID: 2644 RVA: 0x00035F80 File Offset: 0x00034180
	public override void Enter()
	{
		base.Enter();
		this.isTotalPercent = true;
		this.totals = new int[1];
		if (this.playerBase.roleType == RoleType.King)
		{
			this.addDamage = Mathf.Max(this.playerBase.GetMoveSpeed() - base.GetValue(0, 5f), 0f) * base.GetValue(1, 0.1f);
		}
	}

	// Token: 0x06000A55 RID: 2645 RVA: 0x00035FEC File Offset: 0x000341EC
	public override void Update()
	{
		base.Update();
		if (this.checkTime < 0.3f)
		{
			this.checkTime += Time.deltaTime;
			return;
		}
		this.playerBase.addDamagePercent -= this.addDamage;
		this.addDamage = Mathf.Max(this.playerBase.GetMoveSpeed() - base.GetValue(0, 5f), 0f) * base.GetValue(1, 0.1f);
		this.playerBase.addDamagePercent += this.addDamage;
		this.checkTime = 0f;
		this.totals[0] = Mathf.RoundToInt(this.addDamage * 100f);
	}

	// Token: 0x06000A56 RID: 2646 RVA: 0x000360A8 File Offset: 0x000342A8
	public override void Exit()
	{
		this.playerBase.addDamagePercent -= this.addDamage;
	}

	// Token: 0x04000BE2 RID: 3042
	private float addDamage;

	// Token: 0x04000BE3 RID: 3043
	private float checkTime;
}

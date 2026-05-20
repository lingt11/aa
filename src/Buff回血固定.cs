using System;
using UnityEngine;

// Token: 0x02000179 RID: 377
public class Buff回血固定 : RoleBuff
{
	// Token: 0x0600074D RID: 1869 RVA: 0x0002BEA2 File Offset: 0x0002A0A2
	public override void OnInit()
	{
		this.icon = "Shop/hpPotion";
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x0002BEB0 File Offset: 0x0002A0B0
	public override void Update()
	{
		this.time += Time.deltaTime;
		if (this.time >= 1f)
		{
			this.time = 0f;
			this.roleBase.AddPlayerHp((long)this.addValue);
		}
		base.Update();
	}

	// Token: 0x04000B37 RID: 2871
	private float time;

	// Token: 0x04000B38 RID: 2872
	public int addValue;
}

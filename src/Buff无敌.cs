using System;
using UnityEngine;

// Token: 0x0200017E RID: 382
public class Buff无敌 : RoleBuff
{
	// Token: 0x0600075D RID: 1885 RVA: 0x0002C126 File Offset: 0x0002A326
	public override void OnInit()
	{
		this.roleBase.SetWuDi(true);
		this.icon = "Shop/wudiyaoshui";
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x0002C13F File Offset: 0x0002A33F
	public override void Update()
	{
		this.lifeTime -= Time.deltaTime;
		if (this.lifeTime <= 0f)
		{
			this.roleBase.SetWuDi(false);
			base.Clear();
		}
	}
}

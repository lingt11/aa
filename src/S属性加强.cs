using System;
using UnityEngine;

// Token: 0x020001D1 RID: 465
public class S属性加强 : PasssiveSkill
{
	// Token: 0x06000882 RID: 2178 RVA: 0x00030760 File Offset: 0x0002E960
	public override void Enter()
	{
		int num = Mathf.RoundToInt(this.skillValues[0]);
		this.roleBase.AddSTA(num);
		this.roleBase.AddAGI(num);
		this.roleBase.AddSTR(num);
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x000307A0 File Offset: 0x0002E9A0
	public override void Exit()
	{
		int num = Mathf.RoundToInt(this.skillValues[0]);
		this.roleBase.AddSTA(-num);
		this.roleBase.AddAGI(-num);
		this.roleBase.AddSTR(-num);
	}
}

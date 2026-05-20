using System;
using UnityEngine;

// Token: 0x020001B2 RID: 434
public class D属性加强 : PasssiveSkill
{
	// Token: 0x06000816 RID: 2070 RVA: 0x0002ECF0 File Offset: 0x0002CEF0
	public override void Enter()
	{
		int num = Mathf.RoundToInt(this.skillValues[0]);
		this.roleBase.AddSTA(num);
		this.roleBase.AddAGI(num);
		this.roleBase.AddSTR(num);
	}

	// Token: 0x06000817 RID: 2071 RVA: 0x0002ED30 File Offset: 0x0002CF30
	public override void Exit()
	{
		int num = Mathf.RoundToInt(this.skillValues[0]);
		this.roleBase.AddSTA(-num);
		this.roleBase.AddAGI(-num);
		this.roleBase.AddSTR(-num);
	}
}

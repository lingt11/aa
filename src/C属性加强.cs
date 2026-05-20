using System;
using UnityEngine;

// Token: 0x020001A2 RID: 418
public class C属性加强 : PasssiveSkill
{
	// Token: 0x060007DA RID: 2010 RVA: 0x0002E010 File Offset: 0x0002C210
	public override void Enter()
	{
		int num = Mathf.RoundToInt(this.skillValues[0]);
		this.roleBase.AddSTA(num);
		this.roleBase.AddAGI(num);
		this.roleBase.AddSTR(num);
	}

	// Token: 0x060007DB RID: 2011 RVA: 0x0002E050 File Offset: 0x0002C250
	public override void Exit()
	{
		int num = Mathf.RoundToInt(this.skillValues[0]);
		this.roleBase.AddSTA(-num);
		this.roleBase.AddAGI(-num);
		this.roleBase.AddSTR(-num);
	}
}

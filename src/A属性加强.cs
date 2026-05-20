using System;
using UnityEngine;

// Token: 0x0200016E RID: 366
public class A属性加强 : PasssiveSkill
{
	// Token: 0x06000726 RID: 1830 RVA: 0x0002B4CC File Offset: 0x000296CC
	public override void Enter()
	{
		int num = Mathf.RoundToInt(this.skillValues[0]);
		this.roleBase.AddSTA(num);
		this.roleBase.AddAGI(num);
		this.roleBase.AddSTR(num);
	}

	// Token: 0x06000727 RID: 1831 RVA: 0x0002B50C File Offset: 0x0002970C
	public override void Exit()
	{
		int num = Mathf.RoundToInt(this.skillValues[0]);
		this.roleBase.AddSTA(-num);
		this.roleBase.AddAGI(-num);
		this.roleBase.AddSTR(-num);
	}
}

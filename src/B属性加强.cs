using System;
using UnityEngine;

// Token: 0x0200018F RID: 399
public class B属性加强 : PasssiveSkill
{
	// Token: 0x06000791 RID: 1937 RVA: 0x0002CEB0 File Offset: 0x0002B0B0
	public override void Enter()
	{
		int num = Mathf.RoundToInt(this.skillValues[0]);
		this.roleBase.AddSTA(num);
		this.roleBase.AddAGI(num);
		this.roleBase.AddSTR(num);
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x0002CEF0 File Offset: 0x0002B0F0
	public override void Exit()
	{
		int num = Mathf.RoundToInt(this.skillValues[0]);
		this.roleBase.AddSTA(-num);
		this.roleBase.AddAGI(-num);
		this.roleBase.AddSTR(-num);
	}
}

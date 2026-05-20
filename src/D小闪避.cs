using System;
using UnityEngine;

// Token: 0x020001B0 RID: 432
public class D小闪避 : PasssiveSkill
{
	// Token: 0x0600080F RID: 2063 RVA: 0x0002EB5C File Offset: 0x0002CD5C
	public override void Enter()
	{
		int num = Mathf.RoundToInt(this.skillValues[0]);
		this.roleBase.doge += num;
		this.roleBase.CmdDoge(this.roleBase.doge);
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x0002EBA0 File Offset: 0x0002CDA0
	public override void Exit()
	{
		int num = Mathf.RoundToInt(this.skillValues[0]);
		this.roleBase.doge -= num;
		this.roleBase.CmdDoge(this.roleBase.doge);
	}
}

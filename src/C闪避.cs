using System;
using UnityEngine;

// Token: 0x020001A9 RID: 425
public class C闪避 : PasssiveSkill
{
	// Token: 0x060007F4 RID: 2036 RVA: 0x0002E4E4 File Offset: 0x0002C6E4
	public override void Enter()
	{
		int num = Mathf.RoundToInt(this.skillValues[0]);
		this.roleBase.doge += num;
		this.roleBase.CmdDoge(this.roleBase.doge);
	}

	// Token: 0x060007F5 RID: 2037 RVA: 0x0002E528 File Offset: 0x0002C728
	public override void Exit()
	{
		int num = Mathf.RoundToInt(this.skillValues[0]);
		this.roleBase.doge -= num;
		this.roleBase.CmdDoge(this.roleBase.doge);
	}
}

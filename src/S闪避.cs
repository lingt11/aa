using System;
using UnityEngine;

// Token: 0x020001D5 RID: 469
public class S闪避 : PasssiveSkill
{
	// Token: 0x06000890 RID: 2192 RVA: 0x00030A90 File Offset: 0x0002EC90
	public override void Enter()
	{
		int num = Mathf.RoundToInt(this.skillValues[0]);
		this.roleBase.doge += num;
		this.roleBase.CmdDoge(this.roleBase.doge);
	}

	// Token: 0x06000891 RID: 2193 RVA: 0x00030AD4 File Offset: 0x0002ECD4
	public override void Exit()
	{
		int num = Mathf.RoundToInt(this.skillValues[0]);
		this.roleBase.doge -= num;
		this.roleBase.CmdDoge(this.roleBase.doge);
	}
}

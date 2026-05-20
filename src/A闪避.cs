using System;
using UnityEngine;

// Token: 0x02000174 RID: 372
public class A闪避 : PasssiveSkill
{
	// Token: 0x0600073C RID: 1852 RVA: 0x0002BAD8 File Offset: 0x00029CD8
	public override void Enter()
	{
		int num = Mathf.RoundToInt(this.skillValues[0]);
		this.roleBase.doge += num;
		this.roleBase.CmdDoge(this.roleBase.doge);
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x0002BB1C File Offset: 0x00029D1C
	public override void Exit()
	{
		int num = Mathf.RoundToInt(this.skillValues[0]);
		this.roleBase.doge -= num;
		this.roleBase.CmdDoge(this.roleBase.doge);
	}
}

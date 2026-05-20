using System;
using UnityEngine;

// Token: 0x02000196 RID: 406
public class B闪避 : PasssiveSkill
{
	// Token: 0x060007AB RID: 1963 RVA: 0x0002D378 File Offset: 0x0002B578
	public override void Enter()
	{
		int num = Mathf.RoundToInt(this.skillValues[0]);
		this.roleBase.doge += num;
		this.roleBase.CmdDoge(this.roleBase.doge);
	}

	// Token: 0x060007AC RID: 1964 RVA: 0x0002D3BC File Offset: 0x0002B5BC
	public override void Exit()
	{
		int num = Mathf.RoundToInt(this.skillValues[0]);
		this.roleBase.doge -= num;
		this.roleBase.CmdDoge(this.roleBase.doge);
	}
}

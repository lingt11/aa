using System;
using UnityEngine;

// Token: 0x0200016C RID: 364
public class A多重攻击 : PasssiveSkill
{
	// Token: 0x0600071F RID: 1823 RVA: 0x0002B2A0 File Offset: 0x000294A0
	public override void Enter()
	{
		this.roleBase.attackNum += Mathf.RoundToInt(this.skillValues[0]);
		if (this.roleBase.hasAuthority)
		{
			this.roleBase.CmdAttackNum(this.roleBase.attackNum);
		}
	}

	// Token: 0x06000720 RID: 1824 RVA: 0x0002B2F0 File Offset: 0x000294F0
	public override void Exit()
	{
		this.roleBase.attackNum -= Mathf.RoundToInt(this.skillValues[0]);
		if (this.roleBase.hasAuthority)
		{
			this.roleBase.CmdAttackNum(this.roleBase.attackNum);
		}
	}
}

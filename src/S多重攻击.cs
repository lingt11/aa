using System;
using UnityEngine;

// Token: 0x020001D0 RID: 464
public class S多重攻击 : PasssiveSkill
{
	// Token: 0x0600087F RID: 2175 RVA: 0x000306C0 File Offset: 0x0002E8C0
	public override void Enter()
	{
		this.roleBase.attackNum += Mathf.RoundToInt(this.skillValues[0]);
		if (this.roleBase.hasAuthority)
		{
			this.roleBase.CmdAttackNum(this.roleBase.attackNum);
		}
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x00030710 File Offset: 0x0002E910
	public override void Exit()
	{
		this.roleBase.attackNum -= Mathf.RoundToInt(this.skillValues[0]);
		if (this.roleBase.hasAuthority)
		{
			this.roleBase.CmdAttackNum(this.roleBase.attackNum);
		}
	}
}

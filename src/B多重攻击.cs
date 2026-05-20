using System;
using UnityEngine;

// Token: 0x02000189 RID: 393
public class B多重攻击 : PasssiveSkill
{
	// Token: 0x0600077A RID: 1914 RVA: 0x0002C72C File Offset: 0x0002A92C
	public override void Enter()
	{
		this.roleBase.attackNum += Mathf.RoundToInt(this.skillValues[0]);
		if (this.roleBase.hasAuthority)
		{
			this.roleBase.CmdAttackNum(this.roleBase.attackNum);
		}
	}

	// Token: 0x0600077B RID: 1915 RVA: 0x0002C77C File Offset: 0x0002A97C
	public override void Exit()
	{
		this.roleBase.attackNum -= Mathf.RoundToInt(this.skillValues[0]);
		if (this.roleBase.hasAuthority)
		{
			this.roleBase.CmdAttackNum(this.roleBase.attackNum);
		}
	}
}

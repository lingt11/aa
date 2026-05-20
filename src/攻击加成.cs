using System;
using UnityEngine;

// Token: 0x020001DB RID: 475
public class 攻击加成 : PasssiveSkill
{
	// Token: 0x060008A2 RID: 2210 RVA: 0x00030E00 File Offset: 0x0002F000
	public override void Enter()
	{
		this.roleBase.AddAttackPower(Mathf.RoundToInt(this.skillValues[0]));
		this.roleBase.exAttackDistance += (float)Mathf.RoundToInt(this.skillValues[1]);
		this.roleBase.AddAttackSpeed(-this.skillValues[2] * 0.01f);
	}

	// Token: 0x060008A3 RID: 2211 RVA: 0x00030E60 File Offset: 0x0002F060
	public override void Exit()
	{
		this.roleBase.AddAttackPower(-Mathf.RoundToInt(this.skillValues[0]));
		this.roleBase.exAttackDistance -= (float)Mathf.RoundToInt(this.skillValues[1]);
		this.roleBase.AddAttackSpeed(this.skillValues[2] * 0.01f);
	}
}

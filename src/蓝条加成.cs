using System;
using UnityEngine;

// Token: 0x020001EC RID: 492
public class 蓝条加成 : PasssiveSkill
{
	// Token: 0x060008E1 RID: 2273 RVA: 0x00031C3C File Offset: 0x0002FE3C
	public override void Enter()
	{
		this.roleBase.AddMaxMp(Mathf.RoundToInt(this.skillValues[0]));
		this.roleBase.AddMpAddSec(Mathf.RoundToInt(this.skillValues[1]));
	}

	// Token: 0x060008E2 RID: 2274 RVA: 0x00031C6E File Offset: 0x0002FE6E
	public override void Exit()
	{
		this.roleBase.AddMaxMp(-Mathf.RoundToInt(this.skillValues[0]));
		this.roleBase.AddMpAddSec(-Mathf.RoundToInt(this.skillValues[1]));
	}
}

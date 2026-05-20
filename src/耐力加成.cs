using System;
using UnityEngine;

// Token: 0x020001EA RID: 490
public class 耐力加成 : PasssiveSkill
{
	// Token: 0x060008D9 RID: 2265 RVA: 0x000319FB File Offset: 0x0002FBFB
	public override void Enter()
	{
		this.roleBase.AddSTA(Mathf.RoundToInt(this.skillValues[0]));
	}

	// Token: 0x060008DA RID: 2266 RVA: 0x00031A15 File Offset: 0x0002FC15
	public override void Exit()
	{
		this.roleBase.AddSTA(-Mathf.RoundToInt(this.skillValues[0]));
	}
}

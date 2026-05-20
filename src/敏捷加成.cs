using System;
using UnityEngine;

// Token: 0x020001DE RID: 478
public class 敏捷加成 : PasssiveSkill
{
	// Token: 0x060008AD RID: 2221 RVA: 0x00031117 File Offset: 0x0002F317
	public override void Enter()
	{
		this.roleBase.AddAGI(Mathf.RoundToInt(this.skillValues[0]));
	}

	// Token: 0x060008AE RID: 2222 RVA: 0x00031131 File Offset: 0x0002F331
	public override void Exit()
	{
		this.roleBase.AddAGI(-Mathf.RoundToInt(this.skillValues[0]));
	}
}

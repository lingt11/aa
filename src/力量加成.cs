using System;
using UnityEngine;

// Token: 0x020001D7 RID: 471
public class 力量加成 : PasssiveSkill
{
	// Token: 0x06000896 RID: 2198 RVA: 0x00030B8B File Offset: 0x0002ED8B
	public override void Enter()
	{
		this.roleBase.AddSTR(Mathf.RoundToInt(this.skillValues[0]));
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x00030BA5 File Offset: 0x0002EDA5
	public override void Exit()
	{
		this.roleBase.AddSTR(-Mathf.RoundToInt(this.skillValues[0]));
	}
}

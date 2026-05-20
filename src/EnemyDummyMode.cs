using System;
using UnityEngine;

// Token: 0x02000262 RID: 610
public class EnemyDummyMode : EnemyModeBase
{
	// Token: 0x06000B31 RID: 2865 RVA: 0x0003A2A8 File Offset: 0x000384A8
	public override void OnClientInitEnemy()
	{
		base.OnClientInitEnemy();
		Buff护盾神符 buff = new Buff护盾神符();
		this.roleBase.roleBuffManager.AddOneBuff("Buff护盾神符", 20f, buff);
		this.createTime = Time.time;
	}

	// Token: 0x06000B32 RID: 2866 RVA: 0x00002D1D File Offset: 0x00000F1D
	public override void MoveUpdate()
	{
	}

	// Token: 0x04000C15 RID: 3093
	private float createTime;
}

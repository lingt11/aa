using System;
using UnityEngine;

// Token: 0x020001E1 RID: 481
public class 暴击加成 : PasssiveSkill
{
	// Token: 0x060008B8 RID: 2232 RVA: 0x00031324 File Offset: 0x0002F524
	public override void Enter()
	{
		if (this.roleBase.roleType != RoleType.King)
		{
			this.roleBase.AddCritical(this.skillValues[0] * 0.01f);
		}
		PlayerBase roleBase = this.roleBase;
		roleBase.criticalEvent = (RoleBase.Critical)Delegate.Combine(roleBase.criticalEvent, new RoleBase.Critical(this.CriticalEvent));
		this.addMp = Mathf.RoundToInt(this.skillValues[1]);
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x00031394 File Offset: 0x0002F594
	public override void Exit()
	{
		if (this.roleBase.roleType != RoleType.King)
		{
			this.roleBase.AddCritical(-this.skillValues[0] * 0.01f);
		}
		PlayerBase roleBase = this.roleBase;
		roleBase.criticalEvent = (RoleBase.Critical)Delegate.Remove(roleBase.criticalEvent, new RoleBase.Critical(this.CriticalEvent));
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x000313F0 File Offset: 0x0002F5F0
	private void CriticalEvent(RoleBase hurtRole, long damage)
	{
		this.roleBase.AddMp(this.addMp);
	}

	// Token: 0x04000B97 RID: 2967
	private int addMp;
}

using System;
using UnityEngine;

// Token: 0x020001C0 RID: 448
public class H复仇电锯 : PasssiveSkill
{
	// Token: 0x0600084B RID: 2123 RVA: 0x0002FB0C File Offset: 0x0002DD0C
	public override void Enter()
	{
		this.totalName = Game.Language.Get(PathDefine.Concat("p_", this.skillId, StringDefine.Total), "");
		this.totals = new int[1];
		PlayerBase roleBase = this.roleBase;
		roleBase.dieEvent = (RoleBase.DieEvent)Delegate.Combine(roleBase.dieEvent, new RoleBase.DieEvent(this.DieEvent));
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x0002FB76 File Offset: 0x0002DD76
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.dieEvent = (RoleBase.DieEvent)Delegate.Remove(roleBase.dieEvent, new RoleBase.DieEvent(this.DieEvent));
	}

	// Token: 0x0600084D RID: 2125 RVA: 0x0002FBA0 File Offset: 0x0002DDA0
	private void DieEvent(RoleBase role)
	{
		if (GameHelperClient.isReady)
		{
			return;
		}
		int num = Mathf.RoundToInt(this.skillValues[0]);
		role.AddSTR(num);
		role.AddSTA(num);
		role.AddAGI(num);
		this.totals[0] += num;
	}
}

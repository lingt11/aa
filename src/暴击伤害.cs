using System;

// Token: 0x020001E0 RID: 480
public class 暴击伤害 : PasssiveSkill
{
	// Token: 0x060008B4 RID: 2228 RVA: 0x00031234 File Offset: 0x0002F434
	public override void Enter()
	{
		if (this.roleBase.roleType != RoleType.King)
		{
			this.roleBase.AddCriticalDamage(this.skillValues[0] * 0.01f);
		}
		PlayerBase roleBase = this.roleBase;
		roleBase.criticalEvent = (RoleBase.Critical)Delegate.Combine(roleBase.criticalEvent, new RoleBase.Critical(this.CriticalEvent));
	}

	// Token: 0x060008B5 RID: 2229 RVA: 0x00031290 File Offset: 0x0002F490
	public override void Exit()
	{
		if (this.roleBase.roleType != RoleType.King)
		{
			this.roleBase.AddCriticalDamage(-this.skillValues[0] * 0.01f);
		}
		PlayerBase roleBase = this.roleBase;
		roleBase.criticalEvent = (RoleBase.Critical)Delegate.Remove(roleBase.criticalEvent, new RoleBase.Critical(this.CriticalEvent));
	}

	// Token: 0x060008B6 RID: 2230 RVA: 0x000312EC File Offset: 0x0002F4EC
	private void CriticalEvent(RoleBase hurtRole, long damage)
	{
		if (hurtRole.localRoleBuffDic.ContainsKey(LocalBuffType.Frost))
		{
			return;
		}
		GameHelperClient.localPlayer.CmdAddBuff(hurtRole.netId, this.roleBase.netId, LocalBuffType.Frost, 0.35f, 3f, 1);
	}
}

using System;

// Token: 0x02000188 RID: 392
public class B吸血鬼 : PasssiveSkill
{
	// Token: 0x06000776 RID: 1910 RVA: 0x0002C634 File Offset: 0x0002A834
	public override void Enter()
	{
		if (this.roleBase.roleType != RoleType.King)
		{
			this.roleBase.xiXue += this.skillValues[0];
			this.roleBase.xiXueLv += this.skillValues[1] * 0.01f;
		}
		PlayerBase roleBase = this.roleBase;
		roleBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Combine(roleBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEnemyEvent));
	}

	// Token: 0x06000777 RID: 1911 RVA: 0x0002C6B0 File Offset: 0x0002A8B0
	public override void Exit()
	{
		if (this.roleBase.roleType != RoleType.King)
		{
			this.roleBase.xiXue -= this.skillValues[0];
			this.roleBase.xiXueLv -= this.skillValues[1] * 0.01f;
		}
		PlayerBase roleBase = this.roleBase;
		roleBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Remove(roleBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEnemyEvent));
	}

	// Token: 0x06000778 RID: 1912 RVA: 0x0002B280 File Offset: 0x00029480
	private void KillEnemyEvent(RoleBase attackRole, RoleBase hurtRole)
	{
		GameHelperClient.localPlayer.StartHealthHp((double)((float)attackRole.STA * this.skillValues[2]), attackRole);
	}
}

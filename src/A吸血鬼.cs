using System;

// Token: 0x0200016B RID: 363
public class A吸血鬼 : PasssiveSkill
{
	// Token: 0x0600071B RID: 1819 RVA: 0x0002B188 File Offset: 0x00029388
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

	// Token: 0x0600071C RID: 1820 RVA: 0x0002B204 File Offset: 0x00029404
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

	// Token: 0x0600071D RID: 1821 RVA: 0x0002B280 File Offset: 0x00029480
	private void KillEnemyEvent(RoleBase attackRole, RoleBase hurtRole)
	{
		GameHelperClient.localPlayer.StartHealthHp((double)((float)attackRole.STA * this.skillValues[2]), attackRole);
	}
}

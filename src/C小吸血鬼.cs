using System;

// Token: 0x0200019E RID: 414
public class C小吸血鬼 : PasssiveSkill
{
	// Token: 0x060007CA RID: 1994 RVA: 0x0002DB4C File Offset: 0x0002BD4C
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

	// Token: 0x060007CB RID: 1995 RVA: 0x0002DBC8 File Offset: 0x0002BDC8
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

	// Token: 0x060007CC RID: 1996 RVA: 0x0002B280 File Offset: 0x00029480
	private void KillEnemyEvent(RoleBase attackRole, RoleBase hurtRole)
	{
		GameHelperClient.localPlayer.StartHealthHp((double)((float)attackRole.STA * this.skillValues[2]), attackRole);
	}
}

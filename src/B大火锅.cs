using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200018D RID: 397
public class B大火锅 : PasssiveSkill
{
	// Token: 0x06000789 RID: 1929 RVA: 0x0002CB35 File Offset: 0x0002AD35
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Combine(roleBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
	}

	// Token: 0x0600078A RID: 1930 RVA: 0x0002CB5E File Offset: 0x0002AD5E
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Remove(roleBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
	}

	// Token: 0x0600078B RID: 1931 RVA: 0x0002CB87 File Offset: 0x0002AD87
	public override void Update()
	{
		if (this.cdTime > 0f)
		{
			this.cdTime -= Time.deltaTime;
		}
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x0002CBA8 File Offset: 0x0002ADA8
	private void KillEvent(RoleBase attackrole, RoleBase hurtrole)
	{
		if (this.cdTime > 0f)
		{
			return;
		}
		this.cdTime = 0.1f;
		float num = (float)attackrole.STR * this.skillValues[1] + this.skillValues[0];
		num = (float)Util.GetPassSkillDamage(this.roleBase, this.skillAttribute, (double)num, false);
		this.KillEnemy(hurtrole, num);
		this.roleBase.CmdPlayEffectObstruction(EffectDefine.HotPotBoom, 3f, hurtrole.transform.position, 1.5f * (1f + this.roleBase.skillRange));
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x0002CC40 File Offset: 0x0002AE40
	private void KillEnemy(RoleBase hurtRole, float num)
	{
		B大火锅.<KillEnemy>d__5 <KillEnemy>d__;
		<KillEnemy>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
		<KillEnemy>d__.<>4__this = this;
		<KillEnemy>d__.hurtRole = hurtRole;
		<KillEnemy>d__.num = num;
		<KillEnemy>d__.<>1__state = -1;
		<KillEnemy>d__.<>t__builder.Start<B大火锅.<KillEnemy>d__5>(ref <KillEnemy>d__);
	}

	// Token: 0x04000B41 RID: 2881
	private new float cdTime;
}

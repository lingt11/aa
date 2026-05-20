using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200019F RID: 415
public class C小火锅 : PasssiveSkill
{
	// Token: 0x060007CE RID: 1998 RVA: 0x0002DC44 File Offset: 0x0002BE44
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Combine(roleBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
	}

	// Token: 0x060007CF RID: 1999 RVA: 0x0002DC6D File Offset: 0x0002BE6D
	public override void Update()
	{
		if (this.cdTime > 0f)
		{
			this.cdTime -= Time.deltaTime;
		}
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x0002DC8E File Offset: 0x0002BE8E
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Remove(roleBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
	}

	// Token: 0x060007D1 RID: 2001 RVA: 0x0002DCB8 File Offset: 0x0002BEB8
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
		this.roleBase.CmdPlayEffectObstruction(EffectDefine.HotPotBoom, 3f, hurtrole.transform.position, 1f * (1f + this.roleBase.skillRange));
	}

	// Token: 0x060007D2 RID: 2002 RVA: 0x0002DD50 File Offset: 0x0002BF50
	private void KillEnemy(RoleBase hurtRole, float num)
	{
		C小火锅.<KillEnemy>d__5 <KillEnemy>d__;
		<KillEnemy>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
		<KillEnemy>d__.<>4__this = this;
		<KillEnemy>d__.hurtRole = hurtRole;
		<KillEnemy>d__.num = num;
		<KillEnemy>d__.<>1__state = -1;
		<KillEnemy>d__.<>t__builder.Start<C小火锅.<KillEnemy>d__5>(ref <KillEnemy>d__);
	}

	// Token: 0x04000B57 RID: 2903
	private new float cdTime;
}

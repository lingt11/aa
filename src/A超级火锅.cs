using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000172 RID: 370
public class A超级火锅 : PasssiveSkill
{
	// Token: 0x06000734 RID: 1844 RVA: 0x0002B75B File Offset: 0x0002995B
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Combine(roleBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x0002B784 File Offset: 0x00029984
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Remove(roleBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x0002B7AD File Offset: 0x000299AD
	public override void Update()
	{
		if (this.cdTime > 0f)
		{
			this.cdTime -= Time.deltaTime;
		}
	}

	// Token: 0x06000737 RID: 1847 RVA: 0x0002B7D0 File Offset: 0x000299D0
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
		this.roleBase.CmdPlayEffectObstruction(EffectDefine.HotPotBoom, 3f, hurtrole.transform.position, 3f * (1f + this.roleBase.skillRange));
	}

	// Token: 0x06000738 RID: 1848 RVA: 0x0002B868 File Offset: 0x00029A68
	private void KillEnemy(RoleBase hurtRole, float num)
	{
		A超级火锅.<KillEnemy>d__5 <KillEnemy>d__;
		<KillEnemy>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
		<KillEnemy>d__.<>4__this = this;
		<KillEnemy>d__.hurtRole = hurtRole;
		<KillEnemy>d__.num = num;
		<KillEnemy>d__.<>1__state = -1;
		<KillEnemy>d__.<>t__builder.Start<A超级火锅.<KillEnemy>d__5>(ref <KillEnemy>d__);
	}

	// Token: 0x04000B23 RID: 2851
	private new float cdTime;
}

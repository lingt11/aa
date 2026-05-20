using System;
using UnityEngine;

// Token: 0x020001E2 RID: 482
public class 杀人书 : PasssiveSkill
{
	// Token: 0x060008BC RID: 2236 RVA: 0x00031404 File Offset: 0x0002F604
	public override void Enter()
	{
		this.totalName = Game.Language.Get(PathDefine.Concat("p_", this.skillId, StringDefine.Total), "");
		this.totals = new int[1];
		this.addAttack = Mathf.RoundToInt(this.skillValues[0]);
		this.maxAttack = Mathf.RoundToInt(this.skillValues[1]);
		PlayerBase roleBase = this.roleBase;
		roleBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Combine(roleBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
	}

	// Token: 0x060008BD RID: 2237 RVA: 0x00031494 File Offset: 0x0002F694
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Remove(roleBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x000314C0 File Offset: 0x0002F6C0
	private void KillEvent(RoleBase attackrole, RoleBase hurtrole)
	{
		if (this.curAdd >= this.maxAttack)
		{
			return;
		}
		attackrole.AddAttackPower(this.addAttack);
		this.curAdd += this.addAttack;
		this.totals[0] += this.addAttack;
	}

	// Token: 0x04000B98 RID: 2968
	public int curAdd;

	// Token: 0x04000B99 RID: 2969
	private int addAttack;

	// Token: 0x04000B9A RID: 2970
	private int maxAttack;
}

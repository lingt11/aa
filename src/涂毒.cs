using System;

// Token: 0x020001E4 RID: 484
public class 涂毒 : PasssiveSkill
{
	// Token: 0x060008C3 RID: 2243 RVA: 0x00031587 File Offset: 0x0002F787
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x060008C4 RID: 2244 RVA: 0x000315B0 File Offset: 0x0002F7B0
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x000315DC File Offset: 0x0002F7DC
	private float AttackEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		if (hurtrole.IsDead() || this.roleBase.wudi)
		{
			return damage;
		}
		float buffValue = this.skillValues[0] + this.skillValues[1] * (float)attackrole.AGI;
		GameHelperClient.localPlayer.CmdAddBuff(hurtrole.netId, attackrole.netId, LocalBuffType.Poison, buffValue, 5f, 1);
		return damage;
	}
}

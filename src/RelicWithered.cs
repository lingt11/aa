using System;

// Token: 0x02000254 RID: 596
public class RelicWithered : RelicBase
{
	// Token: 0x06000AB3 RID: 2739 RVA: 0x00036E56 File Offset: 0x00035056
	public override void Enter()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(playerBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}

	// Token: 0x06000AB4 RID: 2740 RVA: 0x00036E80 File Offset: 0x00035080
	private float AttackEnemyEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		if (!hurtrole.IsDead() && !hurtrole.localRoleBuffDic.ContainsKey(LocalBuffType.ReAttack) && !hurtrole.wudi)
		{
			GameHelperClient.localPlayer.CmdAddBuff(hurtrole.netId, attackrole.netId, LocalBuffType.ReAttack, base.GetValue(0, 0.25f), (float)base.GetIntValue(1, 5), base.GetIntValue(2, 1));
		}
		return damage;
	}

	// Token: 0x06000AB5 RID: 2741 RVA: 0x00036EE1 File Offset: 0x000350E1
	public override void Exit()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(playerBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}
}

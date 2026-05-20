using System;

// Token: 0x0200023A RID: 570
public class RelicSkillCD : RelicBase
{
	// Token: 0x06000A48 RID: 2632 RVA: 0x00035DF5 File Offset: 0x00033FF5
	public override void Enter()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(playerBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}

	// Token: 0x06000A49 RID: 2633 RVA: 0x00035E20 File Offset: 0x00034020
	private float AttackEnemyEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		for (int i = 0; i < this.playerBase.roleSkillList.Count; i++)
		{
			SkillBase skillBase = this.playerBase.roleSkillList[i];
			if (!(skillBase is PasssiveSkill))
			{
				skillBase.updateCd *= base.GetValue(0, 0.95f);
			}
		}
		if (this.playerBase.roleType == RoleType.King)
		{
			this.playerBase.PlayerKingAI.UpdateSkillCd(base.GetValue(0, 0.95f));
		}
		return damage;
	}

	// Token: 0x06000A4A RID: 2634 RVA: 0x00035EA7 File Offset: 0x000340A7
	public override void Exit()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(playerBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}
}

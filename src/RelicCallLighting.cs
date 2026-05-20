using System;
using UnityEngine;

// Token: 0x0200020B RID: 523
public class RelicCallLighting : RelicBase
{
	// Token: 0x06000984 RID: 2436 RVA: 0x0003381D File Offset: 0x00031A1D
	public override void Enter()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(playerBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x00033848 File Offset: 0x00031A48
	private float AttackEnemyEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		if (this.cd > 0f)
		{
			return 0f;
		}
		if (Random.value < base.GetValue(0, 0.1f))
		{
			this.cd = base.GetValue(1, 0.5f);
			this.playerBase.CmdCreateSkill(ActiveSkillEnum.D_SpellThunder, hurtrole.MyTransform.position, 0f, -1, 0);
		}
		return damage;
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x000338AD File Offset: 0x00031AAD
	public override void Update()
	{
		base.Update();
		if (this.cd > 0f)
		{
			this.cd -= Time.deltaTime;
		}
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x000338D4 File Offset: 0x00031AD4
	public override void Exit()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(playerBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}

	// Token: 0x04000BC4 RID: 3012
	private float cd;
}

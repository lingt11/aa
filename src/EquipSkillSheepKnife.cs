using System;

// Token: 0x020000F7 RID: 247
public class EquipSkillSheepKnife : EquipSkillBase
{
	// Token: 0x06000516 RID: 1302 RVA: 0x0001E1D5 File Offset: 0x0001C3D5
	public override void Init()
	{
		base.Init();
		PlayerBase playerBase = this.playerBase;
		playerBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(playerBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x0001E204 File Offset: 0x0001C404
	private float AttackEnemyEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		this.count++;
		if (this.count > 3)
		{
			this.count = 0;
			long num = hurtrole.OnHit(attackrole, 0.0, Util.GetV2Angle(hurtrole.MyTransform.position, attackrole.MyTransform.position), AttackType.Normal, false);
			if (this.playerBase.isLocalPlayer && num > 0L)
			{
				Game.UI.GetUI<UI_PlayerState>().ShowDamageNum(num, hurtrole.GetAttackPos(), false, AttackType.Normal);
			}
		}
		return damage;
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x0001E289 File Offset: 0x0001C489
	public override void Clear()
	{
		base.Clear();
		PlayerBase playerBase = this.playerBase;
		playerBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(playerBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEnemyEvent));
	}

	// Token: 0x0400047F RID: 1151
	private int count = 1;
}

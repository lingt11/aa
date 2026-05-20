using System;

// Token: 0x020001C5 RID: 453
public class H橡胶果实 : PasssiveSkill
{
	// Token: 0x06000858 RID: 2136 RVA: 0x0002FCAA File Offset: 0x0002DEAA
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.damageEvent = (RoleBase.DamageEnemy)Delegate.Combine(roleBase.damageEvent, new RoleBase.DamageEnemy(this.DamageEvent));
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x0002FCD3 File Offset: 0x0002DED3
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.damageEvent = (RoleBase.DamageEnemy)Delegate.Remove(roleBase.damageEvent, new RoleBase.DamageEnemy(this.DamageEvent));
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x0002FCFC File Offset: 0x0002DEFC
	private float DamageEvent(RoleBase attackRole, RoleBase hurtRole, AttackType attackType, ref float damage)
	{
		damage = this.skillValues[0] * 0.01f * damage;
		return damage;
	}
}

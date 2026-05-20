using System;

// Token: 0x020001C2 RID: 450
public class H对死者的供奉 : PasssiveSkill
{
	// Token: 0x06000852 RID: 2130 RVA: 0x0002FBE9 File Offset: 0x0002DDE9
	public override void Enter()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.dieEvent = (RoleBase.DieEvent)Delegate.Combine(roleBase.dieEvent, new RoleBase.DieEvent(this.DieEvent));
	}

	// Token: 0x06000853 RID: 2131 RVA: 0x0002FC12 File Offset: 0x0002DE12
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.dieEvent = (RoleBase.DieEvent)Delegate.Remove(roleBase.dieEvent, new RoleBase.DieEvent(this.DieEvent));
	}

	// Token: 0x06000854 RID: 2132 RVA: 0x0002FC3C File Offset: 0x0002DE3C
	private void DieEvent(RoleBase role)
	{
		float num = (float)(role.AGI + role.STR) * this.skillValues[0];
		num = (float)Util.GetPassSkillDamage(this.roleBase, this.skillAttribute, (double)num, false);
		GameHelperClient.AOEDamage(this.roleBase, num, role.transform.position, base.Distance, EffectDefine.BoneFieldRandom, 1f + this.roleBase.skillRange);
	}
}

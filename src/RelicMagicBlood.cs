using System;

// Token: 0x02000226 RID: 550
public class RelicMagicBlood : RelicBase
{
	// Token: 0x060009F2 RID: 2546 RVA: 0x00034C16 File Offset: 0x00032E16
	public override void Enter()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.useSkillEvent = (RoleBase.UseSkillEvent)Delegate.Combine(playerBase.useSkillEvent, new RoleBase.UseSkillEvent(this.UseSkillEvent));
	}

	// Token: 0x060009F3 RID: 2547 RVA: 0x00034C3F File Offset: 0x00032E3F
	private ActiveSkillEnum UseSkillEvent(ActiveSkillEnum activeSkillEnum)
	{
		this.playerBase.AddPlayerHp((double)((float)this.playerBase.maxHp * base.GetValue(0, 0.05f)));
		return activeSkillEnum;
	}

	// Token: 0x060009F4 RID: 2548 RVA: 0x00034C67 File Offset: 0x00032E67
	public override void Exit()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.useSkillEvent = (RoleBase.UseSkillEvent)Delegate.Remove(playerBase.useSkillEvent, new RoleBase.UseSkillEvent(this.UseSkillEvent));
	}
}

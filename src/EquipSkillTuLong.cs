using System;

// Token: 0x02000100 RID: 256
public class EquipSkillTuLong : EquipSkillBase
{
	// Token: 0x06000543 RID: 1347 RVA: 0x0001EC4B File Offset: 0x0001CE4B
	public override void Init()
	{
		base.Init();
		PlayerBase playerBase = this.playerBase;
		playerBase.criticalEvent = (RoleBase.Critical)Delegate.Combine(playerBase.criticalEvent, new RoleBase.Critical(this.CriticalEvent));
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x0001EC7C File Offset: 0x0001CE7C
	private void CriticalEvent(RoleBase hurtRole, long damage)
	{
		float num = ((float)this.equipNum * this.skillValueAry[0] + this.skillValueUpAry[0] * (float)this.strengLevel) * 0.01f;
		if ((hurtRole.IsFromRoleType(RoleType.King) && this.playerBase.IsFromRoleType(RoleType.Player)) || (hurtRole.IsFromRoleType(RoleType.Player) && this.playerBase.IsFromRoleType(RoleType.King)))
		{
			num *= GameHelperClient.GetKingBattleDamageLevel() * GameHelperClient.GetKingBattleAttackPercentAddHpLevel();
		}
		this.playerBase.AddPlayerHp((double)((float)damage * num));
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x0001ED03 File Offset: 0x0001CF03
	public override void Clear()
	{
		base.Clear();
		PlayerBase playerBase = this.playerBase;
		playerBase.criticalEvent = (RoleBase.Critical)Delegate.Remove(playerBase.criticalEvent, new RoleBase.Critical(this.CriticalEvent));
	}
}

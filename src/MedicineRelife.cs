using System;

// Token: 0x02000162 RID: 354
public class MedicineRelife : MedicineBase
{
	// Token: 0x060006EC RID: 1772 RVA: 0x0002A760 File Offset: 0x00028960
	public override void Init(ShopItem shopItemValue, PlayerBase playerBaseValue)
	{
		base.Init(shopItemValue, playerBaseValue);
		if (this.playerBase.IsDead())
		{
			this.UseMedicine(this.playerBase);
			return;
		}
		PlayerBase playerBase = this.playerBase;
		playerBase.dieEvent = (RoleBase.DieEvent)Delegate.Combine(playerBase.dieEvent, new RoleBase.DieEvent(this.DieEvent));
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x0002A7B6 File Offset: 0x000289B6
	public override void Clear()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.dieEvent = (RoleBase.DieEvent)Delegate.Remove(playerBase.dieEvent, new RoleBase.DieEvent(this.DieEvent));
		base.Clear();
	}

	// Token: 0x060006EE RID: 1774 RVA: 0x0002A7E5 File Offset: 0x000289E5
	private void DieEvent(RoleBase role)
	{
		this.UseMedicine(role);
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x0002A7F0 File Offset: 0x000289F0
	private void UseMedicine(RoleBase role)
	{
		if (role.hasAuthority)
		{
			float num = GameHelperClient.IsFinalKingBattle() ? GameHelperClient.GetKingBattleAttackAddHpLevel() : 1f;
			if (num > 0f)
			{
				role.CmdRelifeByHp(ConstDefine.ClampBattleValue((double)((float)role.maxHp * num)));
				if (role.isLocalPlayer && this.roleBuff != null)
				{
					Util.ShowTipsNoLanguage(this.roleBuff.buffName);
				}
			}
		}
		this.playerBase.playerAttribute.RemoveMedicine(this);
	}
}

using System;
using UnityEngine;

// Token: 0x020000DA RID: 218
public class EquipEventHolySword : EquipEventBase
{
	// Token: 0x06000476 RID: 1142 RVA: 0x0001B624 File Offset: 0x00019824
	public override void Init(EquipBase equipBaseValue)
	{
		base.Init(equipBaseValue);
		PlayerBase playerBase = this.playerBase;
		playerBase.dieEvent = (RoleBase.DieEvent)Delegate.Combine(playerBase.dieEvent, new RoleBase.DieEvent(this.DieEvent));
		this.checkTime = -1f;
	}

	// Token: 0x06000477 RID: 1143 RVA: 0x0001B65F File Offset: 0x0001985F
	public override void Clear()
	{
		base.Clear();
		PlayerBase playerBase = this.playerBase;
		playerBase.dieEvent = (RoleBase.DieEvent)Delegate.Remove(playerBase.dieEvent, new RoleBase.DieEvent(this.DieEvent));
	}

	// Token: 0x06000478 RID: 1144 RVA: 0x0001B690 File Offset: 0x00019890
	public override void OnUpdate()
	{
		base.OnUpdate();
		if (this.checkTime > 0f && Time.time > this.checkTime)
		{
			this.checkTime = -1f;
			if (this.playerBase.IsDead())
			{
				Util.ShowTips("圣剑掉落了");
				this.playerBase.playerAttribute.SellEquip(this.equipBase, true);
			}
		}
	}

	// Token: 0x06000479 RID: 1145 RVA: 0x0001B6F6 File Offset: 0x000198F6
	private void DieEvent(RoleBase role)
	{
		if (this.playerBase.isLocalPlayer)
		{
			this.checkTime = Time.time + Mathf.Min(GameHelperClient.CountDownTime, 1f);
		}
	}

	// Token: 0x04000407 RID: 1031
	private float checkTime;
}

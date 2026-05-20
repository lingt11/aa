using System;
using UnityEngine;

// Token: 0x020000DB RID: 219
public class EquipEventPiggyBank : EquipEventBase
{
	// Token: 0x0600047B RID: 1147 RVA: 0x0001B720 File Offset: 0x00019920
	public override void Init(EquipBase equipBaseValue)
	{
		base.Init(equipBaseValue);
		PlayerBase playerBase = this.playerBase;
		playerBase.dieEvent = (RoleBase.DieEvent)Delegate.Combine(playerBase.dieEvent, new RoleBase.DieEvent(this.DieEvent));
		this.createTime = 0f;
		this.equipBase.totals = new int[1];
	}

	// Token: 0x0600047C RID: 1148 RVA: 0x0001B778 File Offset: 0x00019978
	public override void Clear()
	{
		base.Clear();
		PlayerBase playerBase = this.playerBase;
		playerBase.dieEvent = (RoleBase.DieEvent)Delegate.Remove(playerBase.dieEvent, new RoleBase.DieEvent(this.DieEvent));
		if (!this.isAdMoney)
		{
			this.isAdMoney = true;
			Util.ShowTips("猪猪存钱罐碎了");
			float num = this.skillValueAry[0] + this.skillValueUpAry[0] * (float)this.strengLevel;
			this.playerBase.AddGold(this.playerBase.GetHeadUIPos(), (int)(this.createTime * num), true);
		}
	}

	// Token: 0x0600047D RID: 1149 RVA: 0x0001B808 File Offset: 0x00019A08
	public override void OnUpdate()
	{
		base.OnUpdate();
		if (this.checkTime > 0f && Time.time > this.checkTime)
		{
			this.checkTime = -1f;
			if (this.playerBase.IsDead())
			{
				this.isAdMoney = true;
				float num = this.skillValueAry[0] + this.skillValueUpAry[0] * (float)this.strengLevel;
				this.playerBase.AddGold(this.playerBase.GetHeadUIPos(), (int)(this.createTime * num), true);
				Util.ShowTips("猪猪存钱罐碎了");
				this.playerBase.playerAttribute.SellEquip(this.equipBase, true);
			}
		}
		if (GameHelperClient.isReady)
		{
			return;
		}
		this.createTime += Time.deltaTime;
		float num2 = this.skillValueAry[0] + this.skillValueUpAry[0] * (float)this.strengLevel;
		this.equipBase.totals[0] = (int)(this.createTime * num2);
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x0001B8FD File Offset: 0x00019AFD
	private void DieEvent(RoleBase role)
	{
		if (this.playerBase.isLocalPlayer)
		{
			this.checkTime = Time.time + Mathf.Min(GameHelperClient.CountDownTime, 1f);
		}
	}

	// Token: 0x04000408 RID: 1032
	private float createTime;

	// Token: 0x04000409 RID: 1033
	private bool isAdMoney;

	// Token: 0x0400040A RID: 1034
	private float checkTime;
}

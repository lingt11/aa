using System;
using UnityEngine;

// Token: 0x02000233 RID: 563
public class RelicRevenge : RelicBase
{
	// Token: 0x06000A2B RID: 2603 RVA: 0x000356F6 File Offset: 0x000338F6
	public override void Enter()
	{
		this.playerBase.CmdUpdateAddRelifeTime(-base.GetIntValue(0, 5));
		PlayerBase playerBase = this.playerBase;
		playerBase.onPlayerRelife = (Action)Delegate.Combine(playerBase.onPlayerRelife, new Action(this.OnPlayerRelife));
	}

	// Token: 0x06000A2C RID: 2604 RVA: 0x00035734 File Offset: 0x00033934
	private void OnPlayerRelife()
	{
		this.playerBase.addDamagePercent -= this.addValue;
		this.addValue = base.GetValue(1, 0.5f);
		this.playerBase.addDamagePercent += this.addValue;
		this.buffTime = base.GetValue(2, 10f);
	}

	// Token: 0x06000A2D RID: 2605 RVA: 0x00035798 File Offset: 0x00033998
	public override void Update()
	{
		base.Update();
		if (this.buffTime > 0f)
		{
			this.buffTime -= Time.deltaTime;
			if (this.buffTime <= 0f)
			{
				this.playerBase.addDamagePercent -= this.addValue;
				this.addValue = 0f;
			}
		}
	}

	// Token: 0x06000A2E RID: 2606 RVA: 0x000357FC File Offset: 0x000339FC
	public override void Exit()
	{
		this.playerBase.CmdUpdateAddRelifeTime(base.GetIntValue(0, 5));
		PlayerBase playerBase = this.playerBase;
		playerBase.onPlayerRelife = (Action)Delegate.Remove(playerBase.onPlayerRelife, new Action(this.OnPlayerRelife));
		this.playerBase.addDamagePercent -= this.addValue;
	}

	// Token: 0x04000BD6 RID: 3030
	private float buffTime;

	// Token: 0x04000BD7 RID: 3031
	private float addValue;
}

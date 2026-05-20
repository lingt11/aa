using System;
using UnityEngine;

// Token: 0x020001F5 RID: 501
public class RelicAddLevelDamage : RelicBase
{
	// Token: 0x06000903 RID: 2307 RVA: 0x00031F78 File Offset: 0x00030178
	public override void Enter()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.onPlayerLevelUp = (Action)Delegate.Combine(playerBase.onPlayerLevelUp, new Action(this.OnPlayerLevelUp));
		this.isTotalPercent = true;
		this.addDamage = (float)this.playerBase.Level * base.GetValue(0, 0.01f);
		this.playerBase.addDamagePercent += this.addDamage;
		this.totals = new int[]
		{
			Mathf.RoundToInt(this.addDamage * 100f)
		};
	}

	// Token: 0x06000904 RID: 2308 RVA: 0x0003200C File Offset: 0x0003020C
	public override void Exit()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.onPlayerLevelUp = (Action)Delegate.Remove(playerBase.onPlayerLevelUp, new Action(this.OnPlayerLevelUp));
		this.playerBase.addDamagePercent -= this.addDamage;
	}

	// Token: 0x06000905 RID: 2309 RVA: 0x00032058 File Offset: 0x00030258
	private void OnPlayerLevelUp()
	{
		this.playerBase.addDamagePercent -= this.addDamage;
		this.addDamage = (float)this.playerBase.Level * base.GetValue(0, 0.01f);
		this.playerBase.addDamagePercent += this.addDamage;
		this.totals[0] = Mathf.RoundToInt(this.addDamage * 100f);
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x000320CD File Offset: 0x000302CD
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.OnPlayerLevelUp();
	}

	// Token: 0x04000BA4 RID: 2980
	private float addDamage;
}

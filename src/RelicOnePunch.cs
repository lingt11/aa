using System;
using UnityEngine;

// Token: 0x0200022F RID: 559
public class RelicOnePunch : RelicBase
{
	// Token: 0x06000A19 RID: 2585 RVA: 0x000353E6 File Offset: 0x000335E6
	public override void Enter()
	{
		this.isTotalPercent = true;
		this.totals = new int[1];
		this.CheckAttackSpeed();
	}

	// Token: 0x06000A1A RID: 2586 RVA: 0x00035404 File Offset: 0x00033604
	private void CheckAttackSpeed()
	{
		float attackSpeed = this.playerBase.GetAttackSpeed();
		float value = base.GetValue(0, 1f);
		if (!Mathf.Approximately(attackSpeed, value))
		{
			float num = value - attackSpeed;
			this.playerBase.AddAttackSpeed(num);
			this.playerBase.UpdateAttackPercent(num * -base.GetValue(1, 1f));
			this.updateAttackSpeed += num;
			this.totals[0] += Mathf.RoundToInt(num * -100f * base.GetValue(1, 1f));
		}
	}

	// Token: 0x06000A1B RID: 2587 RVA: 0x00035494 File Offset: 0x00033694
	public override void Update()
	{
		base.Update();
		this.CheckAttackSpeed();
	}

	// Token: 0x06000A1C RID: 2588 RVA: 0x000354A2 File Offset: 0x000336A2
	public override void Exit()
	{
		base.Exit();
		this.playerBase.AddAttackSpeed(-this.updateAttackSpeed);
		this.playerBase.UpdateAttackPercent(-this.updateAttackSpeed * -base.GetValue(1, 1f));
	}

	// Token: 0x06000A1D RID: 2589 RVA: 0x000354DC File Offset: 0x000336DC
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		float num = this.updateAttackSpeed * -base.GetLevelValueDelta(1, 1f, oldLevel, newLevel);
		this.playerBase.UpdateAttackPercent(num);
		this.totals[0] += Mathf.RoundToInt(num * 100f);
	}

	// Token: 0x04000BD4 RID: 3028
	private float updateAttackSpeed;
}

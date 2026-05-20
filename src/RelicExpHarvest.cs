using System;
using UnityEngine;

// Token: 0x02000213 RID: 531
public class RelicExpHarvest : RelicBase
{
	// Token: 0x060009A7 RID: 2471 RVA: 0x00033EBB File Offset: 0x000320BB
	public override void Enter()
	{
		this.totals = new int[1];
		PlayerBase playerBase = this.playerBase;
		playerBase.nearEnemyDeadEvent = (PlayerBase.NearEnemyDead)Delegate.Combine(playerBase.nearEnemyDeadEvent, new PlayerBase.NearEnemyDead(this.NearEnemyDeadEvent));
	}

	// Token: 0x060009A8 RID: 2472 RVA: 0x00033EF0 File Offset: 0x000320F0
	private void NearEnemyDeadEvent(RoleBase deadRole)
	{
		if (GameHelperClient.isReady)
		{
			return;
		}
		if (Vector3.Distance(deadRole.MyTransform.position, this.playerBase.MyTransform.position) < 20f)
		{
			int num = this.playerBase.GainExp(base.GetIntValue(0, 5));
			this.totals[0] += num;
		}
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x00033F51 File Offset: 0x00032151
	public override void Exit()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.nearEnemyDeadEvent = (PlayerBase.NearEnemyDead)Delegate.Remove(playerBase.nearEnemyDeadEvent, new PlayerBase.NearEnemyDead(this.NearEnemyDeadEvent));
	}
}

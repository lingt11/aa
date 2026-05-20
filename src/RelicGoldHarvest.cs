using System;
using UnityEngine;

// Token: 0x0200021B RID: 539
public class RelicGoldHarvest : RelicBase
{
	// Token: 0x060009C8 RID: 2504 RVA: 0x000345BE File Offset: 0x000327BE
	public override void Enter()
	{
		this.totals = new int[1];
		PlayerBase playerBase = this.playerBase;
		playerBase.nearEnemyDeadEvent = (PlayerBase.NearEnemyDead)Delegate.Combine(playerBase.nearEnemyDeadEvent, new PlayerBase.NearEnemyDead(this.NearEnemyDeadEvent));
	}

	// Token: 0x060009C9 RID: 2505 RVA: 0x000345F4 File Offset: 0x000327F4
	private void NearEnemyDeadEvent(RoleBase deadRole)
	{
		if (GameHelperClient.isReady)
		{
			return;
		}
		if (Vector3.Distance(deadRole.MyTransform.position, this.playerBase.MyTransform.position) < 20f)
		{
			int num = this.playerBase.AddGold(deadRole.GetHeadUIPos(), base.GetIntValue(0, 4), true);
			this.totals[0] += num;
		}
	}

	// Token: 0x060009CA RID: 2506 RVA: 0x0003465C File Offset: 0x0003285C
	public override void Exit()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.nearEnemyDeadEvent = (PlayerBase.NearEnemyDead)Delegate.Remove(playerBase.nearEnemyDeadEvent, new PlayerBase.NearEnemyDead(this.NearEnemyDeadEvent));
	}
}

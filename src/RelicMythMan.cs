using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200022D RID: 557
public class RelicMythMan : RelicBase
{
	// Token: 0x06000A10 RID: 2576 RVA: 0x00035234 File Offset: 0x00033434
	public override void Enter()
	{
		this.isTotalPercent = true;
		this.totals = new int[1];
		PlayerBase playerBase = this.playerBase;
		playerBase.onEquipChange = (RoleBase.OnEquipChange)Delegate.Combine(playerBase.onEquipChange, new RoleBase.OnEquipChange(this.OnEquipChange));
		this.OnEquipChange();
	}

	// Token: 0x06000A11 RID: 2577 RVA: 0x00035284 File Offset: 0x00033484
	private void OnEquipChange()
	{
		List<EquipBase> equipList = this.playerBase.playerAttribute.equipList;
		int num = 0;
		if (equipList.Count > 0)
		{
			for (int i = 0; i < equipList.Count; i++)
			{
				if (equipList[i].IsMyth())
				{
					num++;
				}
			}
		}
		this.playerBase.addDamagePercent -= this.curAddDamage;
		this.curAddDamage = (float)num * base.GetValue(0, 0.1f);
		this.playerBase.addDamagePercent += this.curAddDamage;
		this.totals[0] = Mathf.RoundToInt(this.curAddDamage * 100f);
	}

	// Token: 0x06000A12 RID: 2578 RVA: 0x00035330 File Offset: 0x00033530
	public override void Exit()
	{
		PlayerBase playerBase = this.playerBase;
		playerBase.onEquipChange = (RoleBase.OnEquipChange)Delegate.Remove(playerBase.onEquipChange, new RoleBase.OnEquipChange(this.OnEquipChange));
		this.playerBase.addDamagePercent -= this.curAddDamage;
	}

	// Token: 0x06000A13 RID: 2579 RVA: 0x0003537C File Offset: 0x0003357C
	protected override void OnLevelChanged(int oldLevel, int newLevel)
	{
		this.OnEquipChange();
	}

	// Token: 0x04000BD3 RID: 3027
	public float curAddDamage;
}

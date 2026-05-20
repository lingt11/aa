using System;
using UnityEngine;

// Token: 0x020000E2 RID: 226
public class EquipSkillDemonMask : EquipSkillBase
{
	// Token: 0x060004AE RID: 1198 RVA: 0x0001C608 File Offset: 0x0001A808
	public override void Init()
	{
		base.Init();
		this.equipBase.totals = new int[2];
		this.equipBase.isTotalsPercent = new TotalNumType[]
		{
			TotalNumType.PercentNum,
			TotalNumType.PointNum
		};
		this.equipDamageValue = this.skillValueAry[0] * 0.01f;
		this.equipDamageUpValue = this.skillValueUpAry[0] * 0.01f;
		this.equipMoveValue = this.skillValueAry[1];
		this.equipMoveUpValue = this.skillValueUpAry[1];
		this.AddHpFill(1f - (float)this.playerBase.hp / (float)this.playerBase.maxHp);
		this.checkTime = Time.time;
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x0001C6BC File Offset: 0x0001A8BC
	public override void OnUpdate()
	{
		base.OnUpdate();
		if (Time.time > this.checkTime)
		{
			this.checkTime += 0.15f;
			float num = 1f - (float)this.playerBase.hp / (float)this.playerBase.maxHp;
			if (!Mathf.Approximately(num, this.curRemoveHpFill))
			{
				this.RemoveHpFill();
				this.AddHpFill(num);
			}
		}
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x0001C729 File Offset: 0x0001A929
	public override void Clear()
	{
		base.Clear();
		this.RemoveHpFill();
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x0001C738 File Offset: 0x0001A938
	private void AddHpFill(float hpFill)
	{
		this.curRemoveHpFill = hpFill;
		this.curMoveSpeedValue = this.curRemoveHpFill * (this.equipMoveValue + this.equipMoveUpValue * (float)this.strengLevel) * (float)this.equipNum;
		this.curDamageValue = this.curRemoveHpFill * (this.equipDamageValue + this.equipDamageUpValue * (float)this.strengLevel) * (float)this.equipNum;
		this.playerBase.AddMoveSpeed(this.curMoveSpeedValue);
		this.playerBase.skillExDamage += this.curDamageValue;
		this.equipBase.totals[0] = Mathf.RoundToInt(this.curDamageValue * 100f);
		this.equipBase.totals[1] = Mathf.RoundToInt(this.curMoveSpeedValue * 10f);
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x0001C805 File Offset: 0x0001AA05
	private void RemoveHpFill()
	{
		this.playerBase.AddMoveSpeed(-this.curMoveSpeedValue);
		this.playerBase.skillExDamage -= this.curDamageValue;
	}

	// Token: 0x04000453 RID: 1107
	private float curRemoveHpFill;

	// Token: 0x04000454 RID: 1108
	private float checkTime;

	// Token: 0x04000455 RID: 1109
	private float equipDamageValue;

	// Token: 0x04000456 RID: 1110
	private float equipDamageUpValue;

	// Token: 0x04000457 RID: 1111
	private float equipMoveValue;

	// Token: 0x04000458 RID: 1112
	private float equipMoveUpValue;

	// Token: 0x04000459 RID: 1113
	private float curDamageValue;

	// Token: 0x0400045A RID: 1114
	private float curMoveSpeedValue;
}

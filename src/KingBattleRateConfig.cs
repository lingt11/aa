using System;
using UnityEngine;

// Token: 0x02000095 RID: 149
[Serializable]
public struct KingBattleRateConfig
{
	// Token: 0x0600031E RID: 798 RVA: 0x000154A4 File Offset: 0x000136A4
	public KingBattleRateConfig(float baseValue, float changeStartTime, float changeEndTime, float finalValue)
	{
		this.BaseValue = baseValue;
		this.ChangeStartTime = changeStartTime;
		this.ChangeEndTime = changeEndTime;
		this.FinalValue = finalValue;
	}

	// Token: 0x0600031F RID: 799 RVA: 0x000154C4 File Offset: 0x000136C4
	public float GetValue(float elapsedTime, float totalTime)
	{
		if (elapsedTime <= this.ChangeStartTime)
		{
			return this.BaseValue;
		}
		float num = Mathf.Max(this.ChangeStartTime, totalTime - this.ChangeEndTime);
		if (num <= this.ChangeStartTime)
		{
			return this.FinalValue;
		}
		float t = Mathf.InverseLerp(this.ChangeStartTime, num, elapsedTime);
		return Mathf.Lerp(this.BaseValue, this.FinalValue, t);
	}

	// Token: 0x040002EB RID: 747
	public float BaseValue;

	// Token: 0x040002EC RID: 748
	public float ChangeStartTime;

	// Token: 0x040002ED RID: 749
	public float ChangeEndTime;

	// Token: 0x040002EE RID: 750
	public float FinalValue;
}

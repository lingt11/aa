using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003AA RID: 938
public class ShopItem
{
	// Token: 0x0600157B RID: 5499 RVA: 0x00085E29 File Offset: 0x00084029
	public void Update()
	{
		if (this.cd > 0f)
		{
			this.cd -= Time.deltaTime;
		}
	}

	// Token: 0x0600157C RID: 5500 RVA: 0x00085E4C File Offset: 0x0008404C
	public bool ApplyPriceGrowth()
	{
		int num = this.gold;
		int num2 = this.gem;
		this.gold = this.ApplyPriceGrowthValue(this.gold, this.goldAdd, this.goldMax);
		this.gem = this.ApplyPriceGrowthValue(this.gem, this.gemAdd, this.gemMax);
		return num != this.gold || num2 != this.gem;
	}

	// Token: 0x0600157D RID: 5501 RVA: 0x00085EB8 File Offset: 0x000840B8
	private int ApplyPriceGrowthValue(int current, int add, int max)
	{
		if (add == 0)
		{
			return current;
		}
		if (max <= 0 || add < 0)
		{
			return current + add;
		}
		if (current < max)
		{
			return Mathf.Min(current + add, max);
		}
		return current;
	}

	// Token: 0x0400142B RID: 5163
	public string id;

	// Token: 0x0400142C RID: 5164
	public float cd;

	// Token: 0x0400142D RID: 5165
	public float cdSet;

	// Token: 0x0400142E RID: 5166
	public int gold;

	// Token: 0x0400142F RID: 5167
	public int goldAdd;

	// Token: 0x04001430 RID: 5168
	public int goldMax;

	// Token: 0x04001431 RID: 5169
	public int gem;

	// Token: 0x04001432 RID: 5170
	public int gemAdd;

	// Token: 0x04001433 RID: 5171
	public int gemMax;

	// Token: 0x04001434 RID: 5172
	public Image cdImage;

	// Token: 0x04001435 RID: 5173
	public string iconPath;

	// Token: 0x04001436 RID: 5174
	public string[] strValues;

	// Token: 0x04001437 RID: 5175
	public float[] values;

	// Token: 0x04001438 RID: 5176
	public int times;

	// Token: 0x04001439 RID: 5177
	public string type;

	// Token: 0x0400143A RID: 5178
	public string specialBuy;
}

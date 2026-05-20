using System;
using UnityEngine;

// Token: 0x02000056 RID: 86
public class StatisticDataManager
{
	// Token: 0x06000181 RID: 385 RVA: 0x000099B7 File Offset: 0x00007BB7
	public void MoDaiAdd(int num)
	{
		this.moDaiAdd += num;
		Debug.Log("魔袋累计增加三维" + this.moDaiAdd.ToString());
	}

	// Token: 0x06000182 RID: 386 RVA: 0x00002D1D File Offset: 0x00000F1D
	public void AddBookStr()
	{
	}

	// Token: 0x06000183 RID: 387 RVA: 0x00002D1D File Offset: 0x00000F1D
	public void AddBookDex()
	{
	}

	// Token: 0x06000184 RID: 388 RVA: 0x00002D1D File Offset: 0x00000F1D
	public void AddBookSta()
	{
	}

	// Token: 0x040001DE RID: 478
	private int moDaiAdd;

	// Token: 0x040001DF RID: 479
	private int bookAddSTR;

	// Token: 0x040001E0 RID: 480
	private int bookAddDEX;

	// Token: 0x040001E1 RID: 481
	private int bookAddSTA;
}

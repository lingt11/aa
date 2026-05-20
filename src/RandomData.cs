using System;

// Token: 0x0200007D RID: 125
public class RandomData
{
	// Token: 0x06000275 RID: 629 RVA: 0x0000CC6D File Offset: 0x0000AE6D
	public RandomData()
	{
		this.random = new Random();
	}

	// Token: 0x06000276 RID: 630 RVA: 0x0000CC80 File Offset: 0x0000AE80
	public RandomData(int hash, int count = 0)
	{
		this.hash = hash;
		this.count = count;
		this.random = new Random(hash);
		for (int i = 0; i < count; i++)
		{
			this.random.Next();
		}
	}

	// Token: 0x06000277 RID: 631 RVA: 0x0000CCC5 File Offset: 0x0000AEC5
	public int Next(int min, int max)
	{
		this.count++;
		return this.random.Next(min, max);
	}

	// Token: 0x0400026B RID: 619
	public Random random;

	// Token: 0x0400026C RID: 620
	public int count;

	// Token: 0x0400026D RID: 621
	public int hash;
}

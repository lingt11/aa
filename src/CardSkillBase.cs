using System;
using System.Collections.Generic;

// Token: 0x020000BC RID: 188
public class CardSkillBase
{
	// Token: 0x0600036E RID: 878 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void Enter()
	{
	}

	// Token: 0x0600036F RID: 879 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void Update()
	{
	}

	// Token: 0x06000370 RID: 880 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void Exit()
	{
	}

	// Token: 0x06000371 RID: 881 RVA: 0x00002D1D File Offset: 0x00000F1D
	public virtual void CheckExCardData(List<int> cardIds, bool isTeamCard, ref CardEntries cardEntries)
	{
	}

	// Token: 0x04000369 RID: 873
	public PlayerBase playerBase;

	// Token: 0x0400036A RID: 874
	public int cardId;
}

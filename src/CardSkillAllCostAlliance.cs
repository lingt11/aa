using System;
using System.Collections.Generic;

// Token: 0x020000BA RID: 186
public class CardSkillAllCostAlliance : CardSkillBase
{
	// Token: 0x0600036B RID: 875 RVA: 0x00016A6C File Offset: 0x00014C6C
	public override void CheckExCardData(List<int> cardIds, bool isTeamCard, ref CardEntries cardEntries)
	{
		if (isTeamCard)
		{
			return;
		}
		foreach (int key in cardIds)
		{
			CardData cardData;
			if (Game.GameData.CardDataDic.TryGetValue(key, out cardData) && this.checkList.Contains(cardData.quality))
			{
				this.checkList.Remove(cardData.quality);
			}
		}
		if (this.checkList.Count == 0)
		{
			this.isTrigger = true;
			EntityStatic.Get<ShopManager>().forgingManager.UpdateForgingAdd(0.25f);
		}
	}

	// Token: 0x04000367 RID: 871
	private bool isTrigger;

	// Token: 0x04000368 RID: 872
	private List<int> checkList = new List<int>
	{
		0,
		1,
		2,
		3,
		4
	};
}

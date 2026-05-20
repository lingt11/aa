using System;
using System.Collections.Generic;

// Token: 0x020000BE RID: 190
public class CardSkillChicken : CardSkillBase
{
	// Token: 0x06000373 RID: 883 RVA: 0x00016B50 File Offset: 0x00014D50
	public override void CheckExCardData(List<int> cardIds, bool isTeamCard, ref CardEntries cardEntries)
	{
		foreach (int num in cardIds)
		{
			CardData cardData;
			if (Game.GameData.CardDataDic.TryGetValue(num, out cardData) && cardData.quality == 0)
			{
				cardEntries = CardManager.AddCardEntries(num, cardEntries);
			}
		}
	}
}

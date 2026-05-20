using System;
using System.Collections.Generic;

// Token: 0x020000CA RID: 202
public class CardSkillUniform : CardSkillBase
{
	// Token: 0x06000394 RID: 916 RVA: 0x00017320 File Offset: 0x00015520
	public override void CheckExCardData(List<int> cardIds, bool isTeamCard, ref CardEntries cardEntries)
	{
		if (isTeamCard)
		{
			return;
		}
		this.isTrigger = true;
		int num = -1;
		foreach (int key in cardIds)
		{
			CardData cardData;
			if (Game.GameData.CardDataDic.TryGetValue(key, out cardData) && cardData.cardSkill != CardSkillType.Uniform)
			{
				if (num == -1)
				{
					num = cardData.quality;
				}
				else if (num != cardData.quality)
				{
					this.isTrigger = false;
					break;
				}
			}
		}
		if (this.isTrigger)
		{
			foreach (int cardId in cardIds)
			{
				cardEntries = CardManager.AddCardEntriesByLevel(cardId, cardEntries, 0.4f);
			}
		}
	}

	// Token: 0x04000386 RID: 902
	private bool isTrigger;
}

using System;
using System.Collections.Generic;

// Token: 0x020000C0 RID: 192
public class CardSkillGoblinElder : CardSkillBase
{
	// Token: 0x06000378 RID: 888 RVA: 0x00016D0C File Offset: 0x00014F0C
	public override void CheckExCardData(List<int> cardIds, bool isTeamCard, ref CardEntries cardEntries)
	{
		foreach (int num in cardIds)
		{
			CardData cardData;
			if (Game.GameData.CardDataDic.TryGetValue(num, out cardData) && cardData.unlockType == UnlockType.Drop && cardData.unlockData.Equals(EnemyType.Goblin_Elder_0.ToString()))
			{
				cardEntries = CardManager.AddCardEntries(num, cardEntries);
			}
		}
	}
}

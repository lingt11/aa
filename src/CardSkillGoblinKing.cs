using System;
using System.Collections.Generic;

// Token: 0x020000C2 RID: 194
public class CardSkillGoblinKing : CardSkillBase
{
	// Token: 0x0600037C RID: 892 RVA: 0x00016E3C File Offset: 0x0001503C
	public override void CheckExCardData(List<int> cardIds, bool isTeamCard, ref CardEntries cardEntries)
	{
		foreach (int num in cardIds)
		{
			CardData cardData;
			if (Game.GameData.CardDataDic.TryGetValue(num, out cardData) && cardData.unlockType == UnlockType.Drop && cardData.unlockData.Equals(EnemyType.Goblin_Boss_0.ToString()))
			{
				cardEntries = CardManager.AddCardEntries(num, cardEntries);
			}
		}
	}
}

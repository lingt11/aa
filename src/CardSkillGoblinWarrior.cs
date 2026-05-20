using System;
using System.Collections.Generic;

// Token: 0x020000C3 RID: 195
public class CardSkillGoblinWarrior : CardSkillBase
{
	// Token: 0x0600037E RID: 894 RVA: 0x00016ED0 File Offset: 0x000150D0
	public override void CheckExCardData(List<int> cardIds, bool isTeamCard, ref CardEntries cardEntries)
	{
		foreach (int num in cardIds)
		{
			CardData cardData;
			if (Game.GameData.CardDataDic.TryGetValue(num, out cardData) && cardData.unlockType == UnlockType.Drop && cardData.unlockData.Equals(EnemyType.Goblin_Warrior_0.ToString()))
			{
				cardEntries = CardManager.AddCardEntries(num, cardEntries);
			}
		}
	}
}

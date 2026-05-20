using System;
using System.Collections.Generic;

// Token: 0x020000C1 RID: 193
public class CardSkillGoblinGeneral : CardSkillBase
{
	// Token: 0x0600037A RID: 890 RVA: 0x00016DA4 File Offset: 0x00014FA4
	public override void CheckExCardData(List<int> cardIds, bool isTeamCard, ref CardEntries cardEntries)
	{
		foreach (int num in cardIds)
		{
			CardData cardData;
			if (Game.GameData.CardDataDic.TryGetValue(num, out cardData) && cardData.unlockType == UnlockType.Drop && cardData.unlockData.Equals(EnemyType.Goblin_General_0.ToString()))
			{
				cardEntries = CardManager.AddCardEntries(num, cardEntries);
			}
		}
	}
}

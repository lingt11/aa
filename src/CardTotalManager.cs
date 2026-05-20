using System;
using System.Collections.Generic;

// Token: 0x020000CB RID: 203
public class CardTotalManager
{
	// Token: 0x06000396 RID: 918 RVA: 0x00017408 File Offset: 0x00015608
	public void Init()
	{
		MySystemEvent.Instance.RegisterMessage<int>(29, new Action<Body, int>(this.OnForging));
		MySystemEvent.Instance.RegisterMessage(32, new Action<Body>(this.OnGameWin));
		MySystemEvent.Instance.RegisterMessage(37, new Action<Body>(this.NPCKingTaskComp));
		MySystemEvent.Instance.RegisterMessage<bool>(41, new Action<Body, bool>(this.OnKingChallengeResult));
		MySystemEvent.Instance.RegisterMessage(42, new Action<Body>(this.OnBuyArtifact));
	}

	// Token: 0x06000397 RID: 919 RVA: 0x00017490 File Offset: 0x00015690
	public void Clear()
	{
		MySystemEvent.Instance.UnregisterMessage<int>(29, new Action<Body, int>(this.OnForging));
		MySystemEvent.Instance.UnregisterMessage(32, new Action<Body>(this.OnGameWin));
		MySystemEvent.Instance.UnregisterMessage(37, new Action<Body>(this.NPCKingTaskComp));
		MySystemEvent.Instance.UnregisterMessage<bool>(41, new Action<Body, bool>(this.OnKingChallengeResult));
		MySystemEvent.Instance.UnregisterMessage(42, new Action<Body>(this.OnBuyArtifact));
	}

	// Token: 0x06000398 RID: 920 RVA: 0x00017518 File Offset: 0x00015718
	public void AddTotalEvent(int cardId, string eventType)
	{
		if (eventType.Equals("Forging"))
		{
			this.forgingCards.Add(cardId);
			return;
		}
		if (eventType.Equals("NPC_King"))
		{
			this.npcKingCards.Add(cardId);
			return;
		}
		if (eventType.Equals("KingChallengeWin"))
		{
			this.kingChallengeWin.Add(cardId);
			return;
		}
		if (eventType.Equals("KingChallengeWin"))
		{
			this.kingChallengeWin.Add(cardId);
			return;
		}
		if (eventType.Equals("BuyArtifact"))
		{
			this.buyArtifactCards.Add(cardId);
		}
	}

	// Token: 0x06000399 RID: 921 RVA: 0x000175A8 File Offset: 0x000157A8
	public void OnGameStartCheck()
	{
		foreach (CardData cardData in Game.GameData.CardDataDic.Values)
		{
			if (cardData.unlockType == UnlockType.Total)
			{
				if (cardData.unlockData.Contains("HeroWin"))
				{
					string[] array = cardData.unlockData.Split("_", StringSplitOptions.None);
					int num = int.Parse(array[array.Length - 1]);
					if (GameHelperClient.localPlayer.heroType == (HeroType)num)
					{
						this.winCards.Add(cardData.id);
					}
				}
				else if (cardData.unlockData.Contains("GameWin"))
				{
					string[] array2 = cardData.unlockData.Split("_", StringSplitOptions.None);
					if (array2.Length > 1)
					{
						string[] array3 = array2;
						int num2 = int.Parse(array3[array3.Length - 1]);
						if (GameHelperClient.MapLevel != num2)
						{
							continue;
						}
					}
					this.winCards.Add(cardData.id);
				}
			}
		}
	}

	// Token: 0x0600039A RID: 922 RVA: 0x000176B0 File Offset: 0x000158B0
	private void OnForging(Body body, int updateValue)
	{
		foreach (int cardId in this.forgingCards)
		{
			EntityStatic.Get<CardManager>().AddCardTotal(cardId, updateValue);
		}
	}

	// Token: 0x0600039B RID: 923 RVA: 0x00017708 File Offset: 0x00015908
	private void OnBuyArtifact(Body body)
	{
		foreach (int cardId in this.buyArtifactCards)
		{
			EntityStatic.Get<CardManager>().AddCardTotal(cardId, 1);
		}
	}

	// Token: 0x0600039C RID: 924 RVA: 0x00017760 File Offset: 0x00015960
	private void OnGameWin(Body body)
	{
		foreach (int cardId in this.winCards)
		{
			EntityStatic.Get<CardManager>().AddCardTotal(cardId, 1);
		}
	}

	// Token: 0x0600039D RID: 925 RVA: 0x000177B8 File Offset: 0x000159B8
	private void NPCKingTaskComp(Body body)
	{
		foreach (int cardId in this.npcKingCards)
		{
			EntityStatic.Get<CardManager>().AddCardTotal(cardId, 1);
		}
	}

	// Token: 0x0600039E RID: 926 RVA: 0x00017810 File Offset: 0x00015A10
	private void OnKingChallengeResult(Body body, bool isWin)
	{
		if (isWin)
		{
			foreach (int cardId in this.kingChallengeWin)
			{
				EntityStatic.Get<CardManager>().AddCardTotal(cardId, 1);
			}
		}
	}

	// Token: 0x04000387 RID: 903
	private List<int> forgingCards = new List<int>();

	// Token: 0x04000388 RID: 904
	private List<int> winCards = new List<int>();

	// Token: 0x04000389 RID: 905
	private List<int> npcKingCards = new List<int>();

	// Token: 0x0400038A RID: 906
	private List<int> kingChallengeWin = new List<int>();

	// Token: 0x0400038B RID: 907
	private List<int> buyArtifactCards = new List<int>();
}

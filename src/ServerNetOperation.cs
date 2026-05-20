using System;

// Token: 0x020003E1 RID: 993
public enum ServerNetOperation : byte
{
	// Token: 0x0400156E RID: 5486
	CreatePlayer,
	// Token: 0x0400156F RID: 5487
	SelectHero,
	// Token: 0x04001570 RID: 5488
	OnBuyShop,
	// Token: 0x04001571 RID: 5489
	EnterDungeon,
	// Token: 0x04001572 RID: 5490
	Ready,
	// Token: 0x04001573 RID: 5491
	CreateKing,
	// Token: 0x04001574 RID: 5492
	KingChallenge,
	// Token: 0x04001575 RID: 5493
	GameOverResult,
	// Token: 0x04001576 RID: 5494
	KingBattleResult,
	// Token: 0x04001577 RID: 5495
	ReportCoronationCheat,
	// Token: 0x04001578 RID: 5496
	LobbyChat
}

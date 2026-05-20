using System;

// Token: 0x020003E4 RID: 996
public enum ClientNetOperation : byte
{
	// Token: 0x04001581 RID: 5505
	StartSelectHero,
	// Token: 0x04001582 RID: 5506
	SelectHero,
	// Token: 0x04001583 RID: 5507
	OnPlayerDisconnect,
	// Token: 0x04001584 RID: 5508
	OnStartGame,
	// Token: 0x04001585 RID: 5509
	OnGameOver,
	// Token: 0x04001586 RID: 5510
	OnStartReady,
	// Token: 0x04001587 RID: 5511
	LobbyPlayerData,
	// Token: 0x04001588 RID: 5512
	EnterDungeon,
	// Token: 0x04001589 RID: 5513
	EnemyEnterTip,
	// Token: 0x0400158A RID: 5514
	OnStartRest,
	// Token: 0x0400158B RID: 5515
	UpdateReady,
	// Token: 0x0400158C RID: 5516
	OnStartKing,
	// Token: 0x0400158D RID: 5517
	GameOverResult,
	// Token: 0x0400158E RID: 5518
	KingBattleResult,
	// Token: 0x0400158F RID: 5519
	LobbyChat
}

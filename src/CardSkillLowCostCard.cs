using System;

// Token: 0x020000C4 RID: 196
public class CardSkillLowCostCard : CardSkillBase
{
	// Token: 0x06000380 RID: 896 RVA: 0x00016F68 File Offset: 0x00015168
	public override void Enter()
	{
		this.addValue = SaveLoadManager.gameSaveData.equipCards.Count * 2;
		GameHelperClient.localPlayer.AddSTR(this.addValue);
		GameHelperClient.localPlayer.AddSTA(this.addValue);
		GameHelperClient.localPlayer.AddAGI(this.addValue);
	}

	// Token: 0x06000381 RID: 897 RVA: 0x00016FBC File Offset: 0x000151BC
	public override void Exit()
	{
		GameHelperClient.localPlayer.AddSTR(-this.addValue);
		GameHelperClient.localPlayer.AddSTA(-this.addValue);
		GameHelperClient.localPlayer.AddAGI(-this.addValue);
	}

	// Token: 0x04000380 RID: 896
	private int addValue;
}

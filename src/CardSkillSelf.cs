using System;

// Token: 0x020000C8 RID: 200
public class CardSkillSelf : CardSkillBase
{
	// Token: 0x0600038D RID: 909 RVA: 0x000171E9 File Offset: 0x000153E9
	public override void Enter()
	{
		GameHelperClient.localPlayer.STRAdd += 2;
		GameHelperClient.localPlayer.AGIAdd += 2;
		GameHelperClient.localPlayer.STAAdd += 2;
	}

	// Token: 0x0600038E RID: 910 RVA: 0x00017221 File Offset: 0x00015421
	public override void Exit()
	{
		GameHelperClient.localPlayer.STRAdd -= 2;
		GameHelperClient.localPlayer.AGIAdd -= 2;
		GameHelperClient.localPlayer.STAAdd -= 2;
	}
}

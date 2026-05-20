using System;

// Token: 0x0200020F RID: 527
public class RelicDemonContract : RelicBase
{
	// Token: 0x06000996 RID: 2454 RVA: 0x00033ACE File Offset: 0x00031CCE
	public override void Enter()
	{
		base.Enter();
		GameHelperClient.localPlayer.CmdAddBuff(GameHelperClient.localPlayer.netId, GameHelperClient.localPlayer.netId, LocalBuffType.DemonContract, 0f, 99999f, 0);
	}

	// Token: 0x06000997 RID: 2455 RVA: 0x00033B00 File Offset: 0x00031D00
	public override void Exit()
	{
		base.Exit();
		bool flag = true;
		foreach (RelicBase relicBase in GameHelperClient.localPlayer.playerAttribute.relicList)
		{
			if (relicBase != this && relicBase is RelicDemonContract)
			{
				flag = false;
			}
		}
		if (flag)
		{
			GameHelperClient.localPlayer.CmdRemoveuff(GameHelperClient.localPlayer.netId, LocalBuffType.DemonContract);
		}
	}
}
